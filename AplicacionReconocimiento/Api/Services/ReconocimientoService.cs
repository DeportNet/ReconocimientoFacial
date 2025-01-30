using DeportNetReconocimiento.Api.Dtos.Request;
using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.Api.GlobalExceptionHandler;
using DeportNetReconocimiento.Api.GlobalExceptionHandler.Exceptions;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.Utils;
using Microsoft.AspNetCore.Http;
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
            ConservarImagenSocio(clienteRequest.NombreCliente, clienteRequest.IdCliente);
           
            return "T";

        }

        public void ConservarImagenSocio(string nombreCompletoSocio, int idSocio)
        {
            //Leo las configuraciones
            ConfiguracionEstilos configuracionEstilos = ConfiguracionEstilos.LeerJsonConfiguracion("ConfiguracionEstilos");

            if (!configuracionEstilos.AlmacenarFotoSocio)
            {
                return;
            }
            
            //Obtengo las rutas necesarias
            string rutaOriginal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "captura.jpg");
            string rutaNueva = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuracionEstilos.RutaCarpeta);


            if (string.IsNullOrEmpty(rutaNueva))
            {
                return;
            }



            try
            {
                //Si no existe el directorio, lo creo 
                if (Directory.Exists(rutaNueva))
                {
                    Directory.CreateDirectory(rutaNueva);
                }

                //Configuro el nombre de la foto
                string nuevoNombre = CambiarNombreFoto(nombreCompletoSocio, idSocio);
                string rutaDestino = Path.Combine(rutaNueva, nuevoNombre);

                //Hago la copia de un directorio a otro
                File.Copy(rutaOriginal, rutaDestino, overwrite: true);
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static string CambiarNombreFoto(string nombreCompletoSocio, int idSocio)
        {
            string aux = Regex.Replace(nombreCompletoSocio, "'", "");
            return Regex.Replace(aux, " ", "_") + "_" + idSocio.ToString() + ".jpg";
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
                Console.WriteLine(mensaje);
            }
            enUso = false;
            Console.WriteLine("Se ha dado de baja el cliente facial con id: " + clienteRequest.IdCliente);
        }

        private void MensajeDeErrorAltaBajaCliente(RespuestaAltaBajaCliente respuestaAlta)
        {
            //RespuestaAltaBajaCliente respuestaAlta = new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(), clienteRequest.IdCliente.ToString(), mensaje, "F");

            _ = WebServicesDeportnet.AltaClienteDeportnet(respuestaAlta.ToJson());
        }


        public async Task AltaClienteDeportnet(AltaFacialClienteRequest altaFacialClienteRequest)
        {
            enUso = true;
            Hik_Resultado resAlta= hik_Controladora.AltaCliente(altaFacialClienteRequest.IdCliente.ToString(), altaFacialClienteRequest.NombreCliente);

            if (!resAlta.Exito)
            {
                MensajeDeErrorAltaBajaCliente(
                    new RespuestaAltaBajaCliente(altaFacialClienteRequest.IdSucursal.ToString(),
                    altaFacialClienteRequest.IdCliente.ToString(),
                    resAlta.Mensaje,
                    "F")
                );
             
                    
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
                Console.WriteLine(mensaje);
            }
            enUso = false;
            Console.WriteLine("Se ha dado de alta el cliente facial con id: " + altaFacialClienteRequest.IdCliente + " y nombre: " + altaFacialClienteRequest.NombreCliente);
        }


    }
}
