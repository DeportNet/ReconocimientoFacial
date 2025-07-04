using DeportNetReconocimiento.Api.Data.Dtos.Request;
using DeportNetReconocimiento.Api.Data.Dtos.Response;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.Hikvision.SDKHikvision;
using DeportNetReconocimiento.Utils;
using Serilog;

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
            hik_Controladora = Hik_Controladora_General.Instancia;

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
                                   "F",
                                   lector: ConfiguracionGeneralUtils.ObtenerLectorActual()
                                   ),
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
                                    "F",
                                    lector: ConfiguracionGeneralUtils.ObtenerLectorActual()

                                    ),
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
                                   "F",
                                   lector: ConfiguracionGeneralUtils.ObtenerLectorActual()),
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
                   "F",
                   lector: ConfiguracionGeneralUtils.ObtenerLectorActual()),
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
                Log.Warning("Id sucursal es null en BajaFacial");
                MensajeDeErrorAltaBajaCliente(
                                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                                   clienteRequest.IdCliente.ToString(),
                                   "El idSucursal es nulo, debido a que todavia no se ingresaron las credenciales correspondientes y se esta queriendo realizar una accion desde Deportnet.",
                                   "F",
                                   lector: ConfiguracionGeneralUtils.ObtenerLectorActual()),
                                   true
                               );
                return "F";
            }


            if (Hik_Controladora_General.Instancia.IdUsuario == -1)
            {
                Log.Warning("Id usuario dispositivo es -1 en BajaFacial");
                MensajeDeErrorAltaBajaCliente(
                                    new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                                    clienteRequest.IdCliente.ToString(),
                                    "El idUsuario del dispositivo de reconocimiento facial es -1. El dispositivo no esta conectado.",
                                    "F",
                                    lector: ConfiguracionGeneralUtils.ObtenerLectorActual()),
                                   true
                                );
                return "F";
            }



            if (idSucursal != clienteRequest.IdSucursal)
            {
                Log.Warning("El IdSucursal recibido no es igual al local en BajaFacial");
                MensajeDeErrorAltaBajaCliente(
                                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                                   clienteRequest.IdCliente.ToString(),
                                   "El idSucursal del dispositivo no coincide con el idSucursal del cliente.",
                                   "F",
                                   lector: ConfiguracionGeneralUtils.ObtenerLectorActual()),
                                   true
                               );
                return "F";
            }

            if (!DispositivoEnUsoUtils.EstaLibre())
            {
                Log.Warning("El dispositivo esta en uso en BajaFacial");

                MensajeDeErrorAltaBajaCliente(
                   new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                   clienteRequest.IdCliente.ToString(),
                   "El dispositivo ya está en uso.",
                   "F",
                   lector: ConfiguracionGeneralUtils.ObtenerLectorActual()),
                    true
               );

                return "F";
            }

            return "T";
        }


        public string AltaFacialCliente(AltaFacialClienteRequest clienteRequest)
        {
            string resultado = ValidarValores(clienteRequest);

            if (resultado == "T")
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
            //verificar conexion con el dispositivo

            bool conexionConDisp = Hik_Controladora_General.Instancia.VerificarEstadoDispositivo();

            if (!conexionConDisp)
            {
                Log.Error("Se intento hacer un alta facial pero no se pudo conectar con el dispositivo. Verifique que este conectado y que las credenciales sean correctas.");
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
                   "F",
                   ConfiguracionGeneralUtils.ObtenerLectorActual()
                   ),
                   true
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
                ConfiguracionGeneralUtils.ObtenerLectorActual(),
                imagenSocioBase64
            );

            string mensaje = await WebServicesDeportnet.AltaFacialClienteDeportnet(respuestaAlta);

            Log.Information("Se ha dado de alta el cliente facial con id: " + altaFacialClienteRequest.IdCliente + " y nombre: " + altaFacialClienteRequest.NombreCliente);

            IniciarTiempoEspera();

            DispositivoEnUsoUtils.Desocupar();
        }



        public string BajaFacialCliente(BajaFacialClienteRequest clienteRequest)
        {

            string resultado = ValidarValores(clienteRequest);

            if (resultado == "T")
            {
                DispositivoEnUsoUtils.Ocupar("Baja cliente");
                //asincronico no se espera
                _ = BajaClienteDeportnet(clienteRequest);
            }

            return resultado;

        }

        private async Task BajaClienteDeportnet(BajaFacialClienteRequest clienteRequest)
        {
            Hik_Resultado resBaja = hik_Controladora.BajaCliente(clienteRequest.IdCliente.ToString());

            if (!resBaja.Exito)
            {

                MensajeDeErrorAltaBajaCliente(
                    new RespuestaAltaBajaCliente(clienteRequest.IdSucursal.ToString(),
                    clienteRequest.IdCliente.ToString(),
                    resBaja.Mensaje,
                    "F",
                    lector: ConfiguracionGeneralUtils.ObtenerLectorActual()),
                    false
                );
                Log.Error($"Hubo un Error en Baja facial: {resBaja.Mensaje}");
                DispositivoEnUsoUtils.Desocupar();
                return;

            }

            RespuestaAltaBajaCliente respuestaAlta = new RespuestaAltaBajaCliente(
                clienteRequest.IdSucursal.ToString(),
                clienteRequest.IdCliente.ToString(),
                "Baja facial cliente exitosa",
                "T",
                ConfiguracionGeneralUtils.ObtenerLectorActual()
            );

            string mensaje = await WebServicesDeportnet.BajaFacialClienteDeportnet(respuestaAlta);

            Log.Information($"Se ha dado de baja el cliente facial con id: {clienteRequest.IdCliente} ");
            DispositivoEnUsoUtils.Desocupar();

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
