using DeportNetReconocimiento.Api.Data.Dtos.Request;
using DeportNetReconocimiento.Api.Data.Dtos.Response;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.Utils;

namespace DeportNetReconocimiento.Api.Services
{
    public class ReconocimientoService : IDeportnetReconocimientoService
    {
        private static Hik_Controladora_General hik_Controladora;
    
        private static int? idSucursal;
        
        public static bool EnUso { get; set; }

        public ReconocimientoService()
        {
            EnUso = false;
            idSucursal = null;
            hik_Controladora = Hik_Controladora_General.InstanciaControladoraGeneral;

            int.TryParse(CredencialesUtils.LeerCredencialEspecifica(4), out int idSucursalOut);
            idSucursal = idSucursalOut;
        }

        //Validaciones
        public string ValidarValores(AltaFacialClienteRequest clienteRequest)
        {
            if (idSucursal == null)
            {
                Console.WriteLine("Id sucursal es null en AltaFacial");
                MensajeDeErrorAltaBajaCliente(
                                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                                   clienteRequest.IdCliente.ToString(),
                                   "El idSucursal es nulo, debido a que todavia no se ingresaron las credenciales correspondientes y se esta queriendo realizar una accion desde Deportnet.",
                                   "F"),
                                   true
                               );
                return "F";
            }


            if (hik_Controladora.IdUsuario == -1)
            {
                Console.WriteLine("Id usuario dispositivo es -1 en AltaFacial");
                MensajeDeErrorAltaBajaCliente(
                                    new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                                    clienteRequest.IdCliente.ToString(),
                                    "El idUsuario del dispositivo de reconocimiento facial es -1. El dispositivo no esta conectado.",
                                    "F"),
                                   true
                                );
                return "F";
            }



            if (idSucursal != clienteRequest.IdSucursal)
            {
                Console.WriteLine("El IdSucursal recibido no es igual al local en AltaFacial");
                MensajeDeErrorAltaBajaCliente(
                                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                                   clienteRequest.IdCliente.ToString(),
                                   "El idSucursal del dispositivo no coincide con el idSucursal del cliente.",
                                   "F"),
                                   true
                               );
                return "F";
            }

            
            if (EnUso)
            {
                Console.WriteLine("El dispositivo esta en uso en AltaFacial");
                MensajeDeErrorAltaBajaCliente(
                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                   clienteRequest.IdCliente.ToString(),
                   "El dispositivo ya está en uso.",
                   "F"),
                    true
               );

                return "F";
            }

            EnUso = true;
            //asincronico no se espera
            _ = AltaClienteDeportnet(clienteRequest);
           
            return "T";
        }
        public string ValidarValores(BajaFacialClienteRequest clienteRequest)
        {
            if (idSucursal == null)
            {
                Console.WriteLine("Id sucursal es null en BajaFacial");
                MensajeDeErrorAltaBajaCliente(
                                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                                   clienteRequest.IdCliente.ToString(),
                                   "El idSucursal es nulo, debido a que todavia no se ingresaron las credenciales correspondientes y se esta queriendo realizar una accion desde Deportnet.",
                                   "F"),
                                   true
                               );
                return "F";
            }


            if (hik_Controladora.IdUsuario == -1)
            {
                Console.WriteLine("Id usuario dispositivo es -1 en BajaFacial");
                MensajeDeErrorAltaBajaCliente(
                                    new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                                    clienteRequest.IdCliente.ToString(),
                                    "El idUsuario del dispositivo de reconocimiento facial es -1. El dispositivo no esta conectado.",
                                    "F"),
                                   true
                                );
                return "F";
            }



            if (idSucursal != clienteRequest.IdSucursal)
            {
                Console.WriteLine("El IdSucursal recibido no es igual al local en BajaFacial");
                MensajeDeErrorAltaBajaCliente(
                                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                                   clienteRequest.IdCliente.ToString(),
                                   "El idSucursal del dispositivo no coincide con el idSucursal del cliente.",
                                   "F"),
                                   true
                               );
                return "F";
            }

            if (EnUso)
            {
                Console.WriteLine("El dispositivo esta en uso en BajaFacial");

                MensajeDeErrorAltaBajaCliente(
                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                   clienteRequest.IdCliente.ToString(),
                   "El dispositivo ya está en uso.",
                   "F"),
                    true
               );

                return "F";
            }

            return "T";
        }


        public string AltaFacialCliente(AltaFacialClienteRequest clienteRequest) 
        {
            string resultado = ValidarValores(clienteRequest);

            if(resultado == "T")
            {
                //asincronico no se espera
                _ = AltaClienteDeportnet(clienteRequest);
            }
            return resultado;

        }

        private static int TiempoRetrasoLuegoDeUnAlta;
        public static bool EstaEsperandoLuegoDeUnAlta;

        public void IniciarTiempoEspera()
        {
            TiempoRetrasoLuegoDeUnAlta = ConfiguracionEstilos.LeerJsonConfiguracion().TiempoDeRetrasoAltaCliente;

            EstaEsperandoLuegoDeUnAlta = true;
            Thread.Sleep(TiempoRetrasoLuegoDeUnAlta * 1000);
            EstaEsperandoLuegoDeUnAlta = false;

        }

        public async Task AltaClienteDeportnet(AltaFacialClienteRequest altaFacialClienteRequest)
        {
            EnUso = true;
            Hik_Resultado resAlta = hik_Controladora.AltaCliente(altaFacialClienteRequest.IdCliente.ToString(), altaFacialClienteRequest.NombreCliente);

            //si no hubo exito
            if (!resAlta.Exito)
            {
                MensajeDeErrorAltaBajaCliente(
                    new RespuestaAltaBajaCliente(altaFacialClienteRequest.IdSucursal.ToString(),
                    altaFacialClienteRequest.IdCliente.ToString(),
                    resAlta.Mensaje,
                    "F"),
                    true
                );

                Console.WriteLine("Hubo un Error en alta facial: " + resAlta.Mensaje);
                DispositivoEnUsoUtils.Liberar();

                return;
            }

            //si hubo exito
            RespuestaAltaBajaCliente respuestaAlta = new RespuestaAltaBajaCliente(
                altaFacialClienteRequest.IdSucursal.ToString(),
                altaFacialClienteRequest.IdCliente.ToString(),
                "Alta facial cliente exitosa",
                "T"
            );

            string mensaje = await WebServicesDeportnet.AltaFacialClienteDeportnet(respuestaAlta);

                
            Console.WriteLine("Se ha dado de alta el cliente facial con id: " + altaFacialClienteRequest.IdCliente + " y nombre: " + altaFacialClienteRequest.NombreCliente);
            IniciarTiempoEspera();
            
            
            EnUso = false;
            DispositivoEnUsoUtils.Liberar();
        }

        public string BajaFacialCliente(BajaFacialClienteRequest clienteRequest)
        {

            string resultado = ValidarValores(clienteRequest);

            if(resultado == "T")
            {
                //asincronico no se espera
                _ = BajaClienteDeportnet(clienteRequest);
                 return resultado;
            }

            if (hik_Controladora.IdUsuario == -1)
            {
                MensajeDeErrorAltaBajaCliente(
                    new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                    clienteRequest.IdCliente.ToString(),
                    "El idUsuario del dispositivo de reconocimiento facial es -1. El dispositivo no esta conectado.",
                    "F"),
                    false
                );

                return "F";
            }

            if (idSucursal != clienteRequest.IdSucursal)
            {
                MensajeDeErrorAltaBajaCliente(
                    new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                    clienteRequest.IdCliente.ToString(),
                    "El idSucursal del dispositivo no coincide con el idSucursal del cliente.",
                    "F"),
                    false
                );

               
                return "F";
            }

            if (EnUso)
            {
                MensajeDeErrorAltaBajaCliente(
                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                   clienteRequest.IdCliente.ToString(),
                   "El dispositivo ya está en uso.",
                   "F"),
                   false
               );

             
                return "F";
               
            }

            //asincronico no se espera
            _ = BajaClienteDeportnet(clienteRequest);
            return "T";

        }

        private async Task BajaClienteDeportnet(BajaFacialClienteRequest clienteRequest)
        {
            EnUso = true;
            Hik_Resultado resBaja = hik_Controladora.BajaCliente(clienteRequest.IdCliente.ToString());

            if (!resBaja.Exito)
            {

                MensajeDeErrorAltaBajaCliente(
                    new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                    clienteRequest.IdCliente.ToString(),
                    resBaja.Mensaje,
                    "F"),
                    false
                );
                Console.WriteLine("Hubo un Error en Baja facial: " + resBaja.Mensaje);
                return;

            }
            else
            {
                RespuestaAltaBajaCliente respuestaAlta = new RespuestaAltaBajaCliente(
                    clienteRequest.IdSucursal.ToString(),
                    clienteRequest.IdCliente.ToString(),
                    "Baja facial cliente exitosa",
                    "T"
                );
                string mensaje = await WebServicesDeportnet.BajaFacialClienteDeportnet(respuestaAlta);

                Console.WriteLine("Se ha dado de baja el cliente facial con id: " + clienteRequest.IdCliente);

            }
                EnUso = false;
        }

        private void MensajeDeErrorAltaBajaCliente(RespuestaAltaBajaCliente rta, bool isAlta)
        {

            if (isAlta)
            {
                _ = WebServicesDeportnet.AltaFacialClienteDeportnet(rta);
            }
            else
            {
                _ = WebServicesDeportnet.BajaFacialClienteDeportnet(rta);
            }

        }

        //public string BajaMasivaFacialCliente(BajaFacialClienteRequest clienteRequest)
        //{
        //    string[] arregloResultados = [];

        //    if (enUso)
        //    {
        //        return "F";
        //    }

        //    for (int i=0; i < clienteRequest.ArregloIdClientes.Length; i++)
        //    {
        //        arregloResultados[i] = BajaFacialCliente(clienteRequest);
        //    }

        //    return "T";
        //}
    }
}
