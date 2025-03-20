using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.Utils;
using System.Runtime.InteropServices;


namespace DeportNetReconocimiento.SDKHikvision
{
    public class Hik_Controladora_Tarjetas
    {

        public static Hik_Controladora_Tarjetas? instancia;
        




        private int getCardCfgHandle;
        private int setCardCfgHandle;
        private int delCardCfgHandle;




        private Hik_Controladora_Tarjetas()
        {
            getCardCfgHandle = -1;
            setCardCfgHandle = -1;
            delCardCfgHandle = -1;
        }
        public static Hik_Controladora_Tarjetas ObtenerInstancia
        {
            get
            {
                if (instancia == null)
                {
                    return new Hik_Controladora_Tarjetas();
                }
                return instancia;

            }

        }

        public int GetCardCfgHandle
        { get => getCardCfgHandle; set => getCardCfgHandle = value; }
        public int SetCardCfgHandle
        { get => setCardCfgHandle; set => setCardCfgHandle = value; }
        public int DelCardCfgHandle
        { get => delCardCfgHandle; set => delCardCfgHandle = value; }

        //set card
        public Hik_Resultado EstablecerUnaTarjeta(int nuevoNumeroTarjeta, string nuevoNombreRelacionadoTarjeta)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            //si el controlador no esta en -1 significa que el dispositivo esta reconociendo una tarjeta, por lo tanto se cancela la operacion y volvemos a empezar
            if (SetCardCfgHandle != -1)
            {
                if (Hik_SDK.NET_DVR_StopRemoteConfig(SetCardCfgHandle))
                {
                    SetCardCfgHandle = -1;
                }
                else
                {
                    //hubo un error al cancelar la operacion
                    resultado.ActualizarResultado(true, "Error al cancelar la operacion de establecer una tarjeta", Hik_SDK.NET_DVR_GetLastError().ToString());
                    return resultado;
                }
            }

            //se establece la configuracion de la tarjeta

            Hik_SDK.NET_DVR_CARD_COND tarjetaCond = new Hik_SDK.NET_DVR_CARD_COND();
            IntPtr ptrTarjetaCond = IntPtr.Zero;

            InicializarEstructuraTarjetaCond(ref tarjetaCond, ref ptrTarjetaCond,(uint) nuevoNumeroTarjeta);

            SetCardCfgHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.InstanciaControladoraGeneral.IdUsuario, Hik_SDK.NET_DVR_SET_CARD, ptrTarjetaCond, (int)tarjetaCond.dwSize, null, IntPtr.Zero);

            if (SetCardCfgHandle >= 0)
            {
                resultado = EnviarInformacionTarjeta(nuevoNumeroTarjeta, nuevoNombreRelacionadoTarjeta);
            }
            else
            {
                resultado.ActualizarResultado(false, "Error al establecer la tarjeta", Hik_SDK.NET_DVR_GetLastError().ToString());

            }

            return resultado;
        }
        public Hik_Resultado EnviarInformacionTarjeta(int nuevoNumeroTarjeta, string nuevoNombreRelacionadoTarjeta)
        {


            //tarjeta record
            Hik_SDK.NET_DVR_CARD_RECORD tarjetaRecord = new Hik_SDK.NET_DVR_CARD_RECORD();
            IntPtr ptrTarjetaRecord = IntPtr.Zero;
            InicializarEstructuraTarjetaRecord(ref tarjetaRecord, ref ptrTarjetaRecord, nuevoNumeroTarjeta, nuevoNombreRelacionadoTarjeta);

            //tarjeta status
            Hik_SDK.NET_DVR_CARD_STATUS tarjetaStatus = new Hik_SDK.NET_DVR_CARD_STATUS();
            IntPtr ptrTarjetaStatus = IntPtr.Zero;
            InicializarEstructuraTarjetaStatus(ref tarjetaStatus, ref ptrTarjetaStatus);


            //enviamos la informacion de la tarjeta
            bool flag = true;
            int dwEstado = 0;
            uint dwRetornado = 0;
            Hik_Resultado resultado = new Hik_Resultado();

            while (flag)
            {
                dwEstado = Hik_SDK.NET_DVR_SendWithRecvRemoteConfigTarjeta(SetCardCfgHandle, ptrTarjetaRecord, tarjetaRecord.dwSize, ptrTarjetaStatus, tarjetaStatus.dwSize, ref dwRetornado);
                resultado = VerificarEstadoSetTarjeta(ref flag, dwEstado, ref tarjetaStatus);

            }

            Hik_SDK.NET_DVR_StopRemoteConfig(SetCardCfgHandle);
            SetCardCfgHandle = -1;
            Marshal.FreeHGlobal(ptrTarjetaStatus);
            Marshal.FreeHGlobal(ptrTarjetaRecord);
            return resultado;
        }
        public void InicializarEstructuraTarjetaCond(ref Hik_SDK.NET_DVR_CARD_COND tarjetaCond, ref IntPtr ptrTarjetaCond, uint nuevoNumeroDeTarjeta)
        {
            tarjetaCond.Init();
            tarjetaCond.dwSize = (uint)Marshal.SizeOf(tarjetaCond);
            tarjetaCond.dwCardNum = nuevoNumeroDeTarjeta;
            ptrTarjetaCond = Marshal.AllocHGlobal((int)tarjetaCond.dwSize);
            Marshal.StructureToPtr(tarjetaCond, ptrTarjetaCond, false);
        }
        public void InicializarEstructuraTarjetaStatus(ref Hik_SDK.NET_DVR_CARD_STATUS tarjetaStatus, ref IntPtr ptrTarjetaStatus)
        {
            tarjetaStatus.Init();
            tarjetaStatus.dwSize = (uint)Marshal.SizeOf(tarjetaStatus);
            ptrTarjetaStatus = Marshal.AllocHGlobal((int)tarjetaStatus.dwSize);
            Marshal.StructureToPtr(tarjetaStatus, ptrTarjetaStatus, false);
        }
        public void InicializarEstructuraTarjetaRecord(ref Hik_SDK.NET_DVR_CARD_RECORD tarjetaRecord, ref IntPtr ptrTarjetaRecord, int nuevoNumeroDeTarjeta, string nuevoNombreRelacionadoTarjeta)
        {
            ConfiguracionEstilos configuracionEstilos = ConfiguracionEstilos.LeerJsonConfiguracion();
            tarjetaRecord.Init();
            tarjetaRecord.dwSize = (uint)Marshal.SizeOf(tarjetaRecord);
            //1-normal card (default); 2-disabled card;
            tarjetaRecord.byCardType = 1;

            byte[] byTempCardNo = new byte[Hik_SDK.ACS_CARD_NO_LEN];

            //asignamos el numero de tarjeta nuevo a la estructura record
            byTempCardNo = System.Text.Encoding.UTF8.GetBytes(nuevoNumeroDeTarjeta.ToString());
            for (int i = 0; i < byTempCardNo.Length; i++)
            {
                tarjetaRecord.byCardNo[i] = byTempCardNo[i];
            }

            //asignamos los horarios de la tarjeta (1= all day template, que es por default)
            tarjetaRecord.wCardRightPlan[0] = 1;

            //asignamos el id de la persona relacionada a la tarjeta
            tarjetaRecord.dwEmployeeNo = (uint)nuevoNumeroDeTarjeta;

            //asignamos el nombre relacionado a la estructura record
            byte[] byTempName = new byte[Hik_SDK.NAME_LEN];
            byTempName = System.Text.Encoding.Default.GetBytes(nuevoNombreRelacionadoTarjeta);
            for (int i = 0; i < byTempName.Length; i++)
            {
                tarjetaRecord.byName[i] = byTempName[i];
            }

            //asignamos la fecha de inicio y vencimiento de la tarjeta
            AsignarFechaDeInicioYVencimientoTarjeta(ref tarjetaRecord);

            //asignamos los permisos de la tarjeta (1 = default osea todos, 0= ninguno)
            tarjetaRecord.byDoorRight[0] = configuracionEstilos.PermisosDeTarjeta;     

            //asignamos la estructura record a un puntero
            ptrTarjetaRecord = Marshal.AllocHGlobal((int)tarjetaRecord.dwSize);
            Marshal.StructureToPtr(tarjetaRecord, ptrTarjetaRecord, false);

        }
        //metodo sobrecargado, debido a que hace lo mismo pero hasta cierto punto, ademas de que usa las misma variables
        public void InicializarEstructuraTarjetaRecord(ref Hik_SDK.NET_DVR_CARD_RECORD tarjetaRecord, ref IntPtr ptrTarjetaRecord, int numeroDeTarjeta)
        {
            tarjetaRecord.Init();
            tarjetaRecord.dwSize = (uint)Marshal.SizeOf(tarjetaRecord);
            tarjetaRecord.byCardType = 1;

            byte[] byTempCardNo = new byte[Hik_SDK.ACS_CARD_NO_LEN];

            //asignamos el numero de tarjeta nuevo a la estructura record
            byTempCardNo = System.Text.Encoding.UTF8.GetBytes(numeroDeTarjeta.ToString());
            for (int i = 0; i < byTempCardNo.Length; i++)
            {
                tarjetaRecord.byCardNo[i] = byTempCardNo[i];
            }

            ptrTarjetaRecord = Marshal.AllocHGlobal((int)tarjetaRecord.dwSize);
            Marshal.StructureToPtr(tarjetaRecord, ptrTarjetaRecord, false);

        }
        public void AsignarFechaDeInicioYVencimientoTarjeta(ref Hik_SDK.NET_DVR_CARD_RECORD tarjetaRecord)
        {
            ushort anioActual = (ushort)DateTime.Now.Year;
            byte mesActual = (byte)DateTime.Now.Month;
            byte diaActual = (byte)DateTime.Now.Day;
            byte horaActual = (byte)DateTime.Now.Hour;
            ushort anioSiguiente = (ushort)(DateTime.Now.Year + 5);


            //asignamos que exista una fecha de inicio y vencimiento
            tarjetaRecord.struValid.byEnable = 1;

            //asignamos la fecha de inicio de la tarjeta
            tarjetaRecord.struValid.struBeginTime.wYear = anioActual;
            tarjetaRecord.struValid.struBeginTime.byMonth = mesActual;
            tarjetaRecord.struValid.struBeginTime.byDay = diaActual;
            tarjetaRecord.struValid.struBeginTime.byHour = 0;
            tarjetaRecord.struValid.struBeginTime.byMinute = 0;
            tarjetaRecord.struValid.struBeginTime.bySecond = 0;

            //asignamos la fecha de vencimiento de la tarjeta
            tarjetaRecord.struValid.struEndTime.wYear = anioSiguiente;
            tarjetaRecord.struValid.struEndTime.byMonth = mesActual;
            tarjetaRecord.struValid.struEndTime.byDay = diaActual;
            tarjetaRecord.struValid.struEndTime.byHour = 0;
            tarjetaRecord.struValid.struEndTime.byMinute = 0;
            tarjetaRecord.struValid.struEndTime.bySecond = 0;


        }
        public Hik_Resultado VerificarEstadoSetTarjeta(ref bool flag, int dwEstado, ref Hik_SDK.NET_DVR_CARD_STATUS tarjetaStatus)
        {
            Hik_Resultado resultado = new Hik_Resultado();


            switch (dwEstado)
            {
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_NEEDWAIT:
                    //esperamos


                    break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FAILED:
                    //fallo
                    flag = false;
                    resultado.ActualizarResultado(false, "Error al enviar la informacion de la tarjeta", Hik_SDK.NET_DVR_GetLastError().ToString());

                    break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS:
                    //exito
                    flag = false;
                    if (tarjetaStatus.dwErrorCode != 0)
                    {
                        /*Error code: 
                         * 1904-incorrect card No., 
                         * 1908-no more card can be applied, 
                         * 1917-no more access point can be linked to access controller, 
                         * 1920-one employee ID cannot be applied to multiple cards. This member is valid when the value of byStatus is "0".*/

                        resultado.ActualizarResultado(false, "Hubo un error al verificar el estado de la tarjeta", tarjetaStatus.dwErrorCode.ToString());
                    }
                    else
                    {
                        resultado.ActualizarResultado(true, "Se pudo establecer una tarjeta de forma exitosa!", Hik_SDK.NET_DVR_GetLastError().ToString());
                    }

                    break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FINISH:
                    //finalizo
                    break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_EXCEPTION:
                    //exception

                    resultado.ActualizarResultado(false, "Se arrojo una excepcion, no se pudo settear tarjeta", Hik_SDK.NET_DVR_GetLastError().ToString());

                    break;
                default:
                    //error desconocido, no se pudo settear tarjeta
                    resultado.ActualizarResultado(false, "error desconocido, no se pudo settear tarjeta", Hik_SDK.NET_DVR_GetLastError().ToString());
                    break;
            }

            return resultado;
        }
        public void InicializarTarjetaSendData(ref Hik_SDK.NET_DVR_CARD_SEND_DATA tarjetaSendData, ref IntPtr ptrTarjetaSendData, int nroTarjeta)
        {
            tarjetaSendData.Init();
            tarjetaSendData.dwSize = (uint)Marshal.SizeOf(tarjetaSendData);

            //ACS_CARD_NO_LEN es una const que es 32
            byte[] byTempCardNo = new byte[Hik_SDK.ACS_CARD_NO_LEN];
            byTempCardNo = System.Text.Encoding.UTF8.GetBytes(nroTarjeta.ToString());
            for (int i = 0; i < byTempCardNo.Length; i++)
            {
                tarjetaSendData.byCardNo[i] = byTempCardNo[i];
            }

            ptrTarjetaSendData = Marshal.AllocHGlobal((int)tarjetaSendData.dwSize);
            Marshal.StructureToPtr(tarjetaSendData, ptrTarjetaSendData, false);
        }


        //get tarjeta 
        public Hik_Resultado ObtenerUnaTarjeta(int nroTarjetaABuscar)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            //si hay una operacion de obtener tarjeta en curso, se cancela
            if (GetCardCfgHandle != -1)
            {
                if (Hik_SDK.NET_DVR_StopRemoteConfig(GetCardCfgHandle))
                {
                    GetCardCfgHandle = -1;
                }
            }

            //inicializamos estructuras necesarias

            Hik_SDK.NET_DVR_CARD_COND tarjetaCond = new Hik_SDK.NET_DVR_CARD_COND();
            IntPtr ptrTarjetaCond = IntPtr.Zero;

            InicializarEstructuraTarjetaCond(ref tarjetaCond, ref ptrTarjetaCond, (uint) nroTarjetaABuscar);


            Hik_SDK.NET_DVR_CARD_RECORD tarjetaRecord = new Hik_SDK.NET_DVR_CARD_RECORD();
            IntPtr ptrTarjetaRecord = IntPtr.Zero;

            InicializarEstructuraTarjetaRecord(ref tarjetaRecord, ref ptrTarjetaRecord, nroTarjetaABuscar);


            Hik_SDK.NET_DVR_CARD_SEND_DATA tarjetaSendData = new Hik_SDK.NET_DVR_CARD_SEND_DATA();
            IntPtr ptrTarjetaSendData = IntPtr.Zero;

            InicializarTarjetaSendData(ref tarjetaSendData, ref ptrTarjetaSendData, nroTarjetaABuscar);

            IntPtr ptrUserData = IntPtr.Zero;
            //Hik_SDK.NET_DVR_GET_CARD es una constante que vale 2560
            getCardCfgHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.InstanciaControladoraGeneral.IdUsuario, Hik_SDK.NET_DVR_GET_CARD, ptrTarjetaCond, (int)tarjetaCond.dwSize, null, ptrUserData);

            if (getCardCfgHandle < 0)
            {
                resultado.ActualizarResultado(false, "Error al obtener la tarjeta", Hik_SDK.NET_DVR_GetLastError().ToString());

            }
            else
            {
                Hik_Resultado resultadosBucle = new Hik_Resultado();
                int dwState = (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS;
                uint dwReturned = 0;
                bool flag = true;


                while (flag)
                {
                    dwState = Hik_SDK.NET_DVR_SendWithRecvRemoteConfigTarjeta(GetCardCfgHandle, ptrTarjetaSendData, tarjetaSendData.dwSize, ptrTarjetaRecord, tarjetaRecord.dwSize, ref dwReturned);

                  
                    tarjetaRecord = (Hik_SDK.NET_DVR_CARD_RECORD)Marshal.PtrToStructure(ptrTarjetaRecord, typeof(Hik_SDK.NET_DVR_CARD_RECORD));

                    resultadosBucle = VerificarEstadoTarjeta(ref flag, ref dwState);

                }
                //asigno el resultado final al resultado que retorno de la funcion mayor
                resultado = resultadosBucle;

                //si todo salio bien liberamos memoria
                Hik_SDK.NET_DVR_StopRemoteConfig(GetCardCfgHandle);
                GetCardCfgHandle = -1;
                Marshal.FreeHGlobal(ptrTarjetaSendData);
                Marshal.FreeHGlobal(ptrTarjetaRecord);
            }
            return resultado;
        }
        public Hik_Resultado VerificarEstadoTarjeta(ref bool flag, ref int dwState)

        {
            Hik_Resultado resultado = new Hik_Resultado();



            switch (dwState)
            {
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_NEEDWAIT:
                    //esperamos
                    Thread.Sleep(2);
                    break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FAILED:
                    //fallo
                    flag = false;
                    resultado.ActualizarResultado(false, "Error al obtener la informacion de la tarjeta", Hik_SDK.NET_DVR_GetLastError().ToString());

                    break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS:
                    //exito
                    resultado.ActualizarResultado(true, "Se pudo obtener la informacion de la tarjeta de forma exitosa!", Hik_SDK.NET_DVR_GetLastError().ToString());
                    flag = false;

                    break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FINISH:
                    //finalizo
                    flag = false;
                    resultado.ActualizarResultado(true, "Obtener informacion de la tarjeta finalizo", Hik_SDK.NET_DVR_GetLastError().ToString());

                    break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_EXCEPTION:
                    //exception
                    flag = false;
                    resultado.ActualizarResultado(false, "Se produjo una excepcion. Puede que no exista la tarjeta.", Hik_SDK.NET_DVR_GetLastError().ToString());

                    break;
                default:
                    flag = false;
                    //error desconocido, no se pudo obtener tarjeta
                    resultado.ActualizarResultado(false, "error desconocido, no se pudo obtener la tarjeta", Hik_SDK.NET_DVR_GetLastError().ToString());

                    break;
            }

            return resultado;
        }


        //del tarjeta
        public Hik_Resultado EliminarTarjetaPorId(int idTarjeta)
        {
            Hik_Resultado resultado = new Hik_Resultado();
            
            if (DelCardCfgHandle != -1)
            {
                if (Hik_SDK.NET_DVR_StopRemoteConfig(DelCardCfgHandle))
                {
                    DelCardCfgHandle = -1;
                }
            }

            //tarjeta cond
            Hik_SDK.NET_DVR_CARD_COND tarjetaCond = new Hik_SDK.NET_DVR_CARD_COND();
            IntPtr ptrTarjetaCond = IntPtr.Zero;

            InicializarEstructuraTarjetaCond(ref tarjetaCond, ref ptrTarjetaCond, (uint)idTarjeta);


            //tarjeta send data
            Hik_SDK.NET_DVR_CARD_SEND_DATA tarjetaSendData = new Hik_SDK.NET_DVR_CARD_SEND_DATA();
            IntPtr ptrTarjetaSendData = IntPtr.Zero;

            InicializarTarjetaSendData(ref tarjetaSendData, ref ptrTarjetaSendData, idTarjeta);

            //tarjeta status
            Hik_SDK.NET_DVR_CARD_STATUS tarjetaStatus = new Hik_SDK.NET_DVR_CARD_STATUS();
            IntPtr ptrTarjetaStatus = IntPtr.Zero;

            //puntero datos del usuario
            IntPtr ptrUserData = IntPtr.Zero;

            InicializarEstructuraTarjetaStatus(ref tarjetaStatus, ref ptrTarjetaStatus);



            DelCardCfgHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.InstanciaControladoraGeneral.IdUsuario, Hik_SDK.NET_DVR_DEL_CARD, ptrTarjetaCond, (int)tarjetaCond.dwSize, null, ptrUserData);
            if (DelCardCfgHandle < 0)
            {
                //ocurrio un error:
                resultado.ActualizarResultado(false, "Error al eliminar la tarjeta: " , Hik_SDK.NET_DVR_GetLastError().ToString());
            }
            else
            {
                Hik_Resultado hik_resultadoBucle = new Hik_Resultado();
                bool flag = true;
                int dwState = 0;
                uint dwReturned = 0;
               
                while (flag)
                {

                    dwState = Hik_SDK.NET_DVR_SendWithRecvRemoteConfigTarjeta(DelCardCfgHandle, ptrTarjetaSendData, tarjetaSendData.dwSize, ptrTarjetaStatus, tarjetaStatus.dwSize, ref dwReturned);
                    hik_resultadoBucle = VerificarEstadoTarjeta(ref flag, ref dwState);
                }
                //asigno el resultado final al resultado que retorno de la funcion mayor
                resultado = hik_resultadoBucle;


            }

            //si todo salio bien liberamos memoria
            Hik_SDK.NET_DVR_StopRemoteConfig(DelCardCfgHandle);
            DelCardCfgHandle = -1;
            Marshal.FreeHGlobal(ptrTarjetaStatus);
            Marshal.FreeHGlobal(ptrTarjetaSendData);
            Marshal.FreeHGlobal(ptrTarjetaCond);


            return resultado;
        }

    }
}

        