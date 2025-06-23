using DeportNetReconocimiento.Api.Dtos.Request;
using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.Utils;
using Serilog;
using Windows.Media.Protection.PlayReady;


namespace DeportNetReconocimiento.Api.Services
{
    public class ReconocimientoService : IDeportnetReconocimientoService
    {
        private static int? idSucursal;

        public ReconocimientoService()
        {
            
            idSucursal = null;
            
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


            if (Hik_Controladora_General.Instancia.IdUsuario == -1)
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
                                   new RespuestaAltaBajaCliente(
                                       clienteRequest.IdSucursal.ToString(),
                                   clienteRequest.IdCliente.ToString(),
                                   "El idSucursal del dispositivo no coincide con el idSucursal del cliente.",
                                   "F")
                               );
                return "F";
            }
            
            if (!DispositivoEnUsoUtils.EstaLibre())
            {
                Log.Information($"No se procesa la peticion de alta con idCliente {clienteRequest.IdCliente} debido a que el dispositivo esta ocupado.");

                MensajeDeErrorAltaBajaCliente(
                    new RespuestaAltaBajaCliente(
                        clienteRequest.IdSucursal.ToString(),
                        clienteRequest.IdCliente.ToString(),
                        "El dispositivo se encuentra ocupado.",
                        "F"
                        )
                );

                return "F";

            }
            DispositivoEnUsoUtils.Ocupar("Alta facial cliente");
 
            _ = AltaClienteDeportnet(clienteRequest); //asincronico no se espera

            return "T";
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
            //verificar conexion con el dispositivo

            bool conexionConDisp = Hik_Controladora_General.Instancia.VerificarEstadoDispositivo();

            if (!conexionConDisp) { 
                Log.Error("No se pudo conectar con el dispositivo de reconocimiento facial. Verifique que este conectado y que las credenciales sean correctas.");
                return;
            }
            
            Hik_Resultado resAlta = Hik_Controladora_General.Instancia.AltaCliente(altaFacialClienteRequest.IdCliente.ToString(), altaFacialClienteRequest.NombreCliente);

            //si no hubo exito
            if (!resAlta.Exito)
            {
                MensajeDeErrorAltaBajaCliente(
                   new RespuestaAltaBajaCliente(altaFacialClienteRequest.IdSucursal.ToString(),
                   altaFacialClienteRequest.IdCliente.ToString(),
                   resAlta.Mensaje,
                   "F")
                );

                Log.Error("Hubo un Error en alta facial: " + resAlta.Mensaje);
                DispositivoEnUsoUtils.Desocupar();

                return;
            }
            //  Envio la foto del socio si esta la config activa

            ConfiguracionEstilos configuracionEstilos = ConfiguracionEstilos.LeerJsonConfiguracion();
            string? imagenSocioBase64 = null;

            if (configuracionEstilos.EnviarFotoSocioADx)
            {
                imagenSocioBase64 = BuscarImagenSocioUtils.BuscarImagenSocio(
                    altaFacialClienteRequest.NombreCliente,
                    altaFacialClienteRequest.IdCliente.ToString()
                    );
            }

            //si hubo exito
            RespuestaAltaBajaCliente respuestaAlta = new RespuestaAltaBajaCliente(
                altaFacialClienteRequest.IdSucursal.ToString(),
                altaFacialClienteRequest.IdCliente.ToString(),
                "Alta facial cliente exitosa",
                "T",
                imagenSocioBase64
            );

            string mensaje = await WebServicesDeportnet.AltaClienteDeportnet(respuestaAlta.ToJson());
                
            Log.Information("Se ha dado de alta el cliente facial con id: " + altaFacialClienteRequest.IdCliente + " y nombre: " + altaFacialClienteRequest.NombreCliente);

            IniciarTiempoEspera();
            
            DispositivoEnUsoUtils.Desocupar();
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

            if (Hik_Controladora_General.Instancia.IdUsuario == -1)
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

            if (!DispositivoEnUsoUtils.EstaLibre())
            {
                MensajeDeErrorAltaBajaCliente(
                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                   clienteRequest.IdCliente.ToString(),
                   "El dispositivo ya está en uso.",
                   "F")
               );

             
                return "F";
               
            }

            DispositivoEnUsoUtils.Ocupar("Baja cliente");

            //asincronico no se espera
            _ = BajaClienteDeportnet(clienteRequest);

            return "T";
        }

        private async Task BajaClienteDeportnet(BajaFacialClienteRequest clienteRequest)
        {
            Hik_Resultado resBaja = Hik_Controladora_General.Instancia.BajaCliente(clienteRequest.IdCliente.ToString());

            if (!resBaja.Exito)
            {

                MensajeDeErrorAltaBajaCliente(
                    new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                    clienteRequest.IdCliente.ToString(),
                    resBaja.Mensaje,
                    "F")
                );
                Console.WriteLine("Hubo un Error en Baja facial: " + resBaja.Mensaje);
                return;

            }

            RespuestaAltaBajaCliente respuestaAlta = new RespuestaAltaBajaCliente(
                clienteRequest.IdSucursal.ToString(),
                clienteRequest.IdCliente.ToString(),
                "Baja facial cliente exitosa", 
                "T"
            );
            string mensaje = await WebServicesDeportnet.BajaClienteDeportnet(respuestaAlta.ToJson());
                
            Log.Information("Se ha dado de baja el cliente facial con id: " + clienteRequest.IdCliente);

            DispositivoEnUsoUtils.Desocupar();
        }

        private void MensajeDeErrorAltaBajaCliente(RespuestaAltaBajaCliente respuestaAlta)
        {
            //RespuestaAltaBajaCliente respuestaAlta = new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(), clienteRequest.IdCliente.ToString(), mensaje, "F");

            _ = WebServicesDeportnet.AltaClienteDeportnet(respuestaAlta.ToJson());
        }



    }
}
