using DeportNetReconocimiento.Api.Dtos.Request;
using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.Api.GlobalExceptionHandler;
using DeportNetReconocimiento.Api.GlobalExceptionHandler.Exceptions;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Services
{
    public class ReconocimientoService : IDeportnetReconocimientoService
    {
        private Hik_Controladora_General hik_Controladora;
        private bool enUso;
        private int? idSucursal;
        
        public bool EnUso { get => enUso; set => enUso = value; }

        public ReconocimientoService()
        {
            enUso = false;
            hik_Controladora = Hik_Controladora_General.InstanciaControladoraGeneral;
            
            LeerCredencialesReconocimientoService();
        }

        private void LeerCredencialesReconocimientoService()
        {
            string[] credenciales = CredencialesUtils.LeerCredenciales();

            if(credenciales.Length == 0)
            {
                idSucursal = null;
                return;
            }
           
            try
            {
                idSucursal = int.Parse(credenciales[4]);

            }
            catch(Exception e)
            {
                Console.WriteLine("LeerCredencialesReconocimientoService Excp: " + e.ToString());
                idSucursal = null;
            }

        }


        public string AltaFacialCliente(AltaFacialClienteRequest clienteRequest) 
        {
            if(idSucursal == null)
            {
                MensajeDeErrorAltaBajaCliente(
                                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                                   clienteRequest.IdCliente.ToString(),
                                   "El idSucursal es nulo, debido a que todavia no se ingresaron las credenciales correspondientes y se esta queriendo realizar una accion desde Deportnet.",
                                   "F")
                               );
                return "F";
            }


            if (hik_Controladora.IdUsuario == -1)
            {
                MensajeDeErrorAltaBajaCliente(
                                    new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                                    clienteRequest.IdCliente.ToString(),
                                    "El idUsuario del dispositivo de reconocimiento facial es -1. El dispositivo no esta conectado.",
                                    "F")
                                );
                return "F";
            }

            

            if(idSucursal != clienteRequest.IdSucursal)
            {
                MensajeDeErrorAltaBajaCliente(
                                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                                   clienteRequest.IdCliente.ToString(),
                                   "El idSucursal del dispositivo no coincide con el idSucursal del cliente.",
                                   "F")
                               );
                return "F";
            }
            
            if (enUso)
            {
                MensajeDeErrorAltaBajaCliente(
                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                   clienteRequest.IdCliente.ToString(),
                   "El dispositivo ya está en uso.",
                   "F")
               );

                return "F";
            }

            //asincronico no se espera
            _ = AltaClienteDeportnet(clienteRequest);
            
           
            return "T";

        }

        private static int tiempoRetrasoLuegoDeUnAlta;
        public static bool estaEsperandoLuegoDeUnAlta;

        public void IniciarTiempoEspera()
        {
            tiempoRetrasoLuegoDeUnAlta = ConfiguracionEstilos.LeerJsonConfiguracion().TiempoDeRetrasoAltaCliente;

            estaEsperandoLuegoDeUnAlta = true;
            Thread.Sleep(tiempoRetrasoLuegoDeUnAlta * 1000);
            estaEsperandoLuegoDeUnAlta = false;

        }

        public async Task AltaClienteDeportnet(AltaFacialClienteRequest altaFacialClienteRequest)
        {
            enUso = true;
            Hik_Resultado resAlta= hik_Controladora.AltaCliente(altaFacialClienteRequest.IdCliente.ToString(), altaFacialClienteRequest.NombreCliente);

            IniciarTiempoEspera();

            if (!resAlta.Exito)
            {
                
                MensajeDeErrorAltaBajaCliente(
                    new RespuestaAltaBajaCliente(altaFacialClienteRequest.IdSucursal.ToString(),
                    altaFacialClienteRequest.IdCliente.ToString(),
                    resAlta.Mensaje,
                    "F")
                );

                Console.WriteLine("Hubo un Error en alta facial: "+ resAlta.Mensaje);
                //throw new HikvisionException(resAlta.Mensaje);
            }
            else
            {
                RespuestaAltaBajaCliente respuestaAlta = new RespuestaAltaBajaCliente(
                    altaFacialClienteRequest.IdSucursal.ToString(),
                    altaFacialClienteRequest.IdCliente.ToString(),
                    "Alta facial cliente exitosa",
                    "T");

                

                string mensaje = await WebServicesDeportnet.AltaClienteDeportnet(respuestaAlta.ToJson());

                
                Console.WriteLine("Se ha dado de alta el cliente facial con id: " + altaFacialClienteRequest.IdCliente + " y nombre: " + altaFacialClienteRequest.NombreCliente);
            }
            
            
            enUso = false;
        }

       

      
        public string BajaFacialCliente(BajaFacialClienteRequest clienteRequest)
        {

            if (idSucursal == null)
            {
                MensajeDeErrorAltaBajaCliente(
                                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                                   clienteRequest.IdCliente.ToString(),
                                   "El idSucursal es nulo, debido a que todavia no se ingresaron las credenciales correspondientes y se esta queriendo realizar una accion desde Deportnet.",
                                   "F")
                               );
                return "F";
            }

            if (hik_Controladora.IdUsuario == -1)
            {
                MensajeDeErrorAltaBajaCliente(
                    new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                    clienteRequest.IdCliente.ToString(),
                    "El idUsuario del dispositivo de reconocimiento facial es -1. El dispositivo no esta conectado.",
                    "F")
                );

                return "F";
            }

            if (idSucursal != clienteRequest.IdSucursal)
            {
                MensajeDeErrorAltaBajaCliente(
                    new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                    clienteRequest.IdCliente.ToString(),
                    "El idSucursal del dispositivo no coincide con el idSucursal del cliente.",
                    "F")
                );

               
                return "F";
            }

            if (enUso)
            {
                MensajeDeErrorAltaBajaCliente(
                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                   clienteRequest.IdCliente.ToString(),
                   "El dispositivo ya está en uso.",
                   "F")
               );

             
                return "F";
               
            }

            //asincronico no se espera
            _ = BajaClienteDeportnet(clienteRequest);


            return "T";

        }

        private async Task BajaClienteDeportnet(BajaFacialClienteRequest clienteRequest)
        {
            enUso = true;
            Hik_Resultado resBaja = hik_Controladora.BajaCliente(clienteRequest.IdCliente.ToString());

            if (!resBaja.Exito)
            {

                MensajeDeErrorAltaBajaCliente(
                    new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                    clienteRequest.IdCliente.ToString(),
                    resBaja.Mensaje,
                    "F")
                );
                Console.WriteLine("Hubo un Error en Baja facial: " + resBaja.Mensaje);


                //throw new HikvisionException(resAlta.Mensaje);
            }
            else
            {
                RespuestaAltaBajaCliente respuestaAlta = new RespuestaAltaBajaCliente(
                    clienteRequest.IdSucursal.ToString(),
                    clienteRequest.IdCliente.ToString(),
                    "Alta facial cliente exitosa", 
                    "T");
                string mensaje = await WebServicesDeportnet.BajaClienteDeportnet(respuestaAlta.ToJson());
                
                Console.WriteLine("Se ha dado de baja el cliente facial con id: " + clienteRequest.IdCliente);
            }
            enUso = false;
        }

        private void MensajeDeErrorAltaBajaCliente(RespuestaAltaBajaCliente respuestaAlta)
        {
            //RespuestaAltaBajaCliente respuestaAlta = new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(), clienteRequest.IdCliente.ToString(), mensaje, "F");

            _ = WebServicesDeportnet.AltaClienteDeportnet(respuestaAlta.ToJson());
        }



    }
}
