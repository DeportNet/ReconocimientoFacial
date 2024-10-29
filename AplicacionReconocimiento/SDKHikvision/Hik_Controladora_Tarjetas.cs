using DeportNetReconocimiento.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace DeportNetReconocimiento.SDKHikvision
{
    public class Hik_Controladora_Tarjetas
    {
        private int getCardCfgHandle;
        private int setCardCfgHandle;
        private int delCardCfgHandle;


        public Hik_Controladora_Tarjetas()
        {
            getCardCfgHandle = -1;
            setCardCfgHandle = -1;
            delCardCfgHandle = -1;
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
                    resultado.Exito = false;
                    resultado.Mensaje = "Error al cancelar la operacion de establecer una tarjeta";
                    resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                    return resultado;
                }
            }

            //se establece la configuracion de la tarjeta

            Hik_SDK.NET_DVR_CARD_COND tarjetaCond = new Hik_SDK.NET_DVR_CARD_COND();
            IntPtr ptrTarjetaCond = IntPtr.Zero;

            InicializarEstructuraTarjetaCond(ref tarjetaCond, ref ptrTarjetaCond);

            SetCardCfgHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.IdUsuario, Hik_SDK.NET_DVR_SET_CARD, ptrTarjetaCond, (int)tarjetaCond.dwSize, null, IntPtr.Zero);

            if(SetCardCfgHandle >= 0)
            {
                resultado = EnviarInformacionTarjeta(nuevoNumeroTarjeta, nuevoNombreRelacionadoTarjeta);

                

            }
            else
            {
                resultado.Exito = false;
                resultado.Mensaje = "Error al establecer la tarjeta";
                resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
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
            Hik_Resultado resultado= new Hik_Resultado();

            while (flag)
            {
                dwEstado = Hik_SDK.NET_DVR_SendWithRecvRemoteConfigTarjeta(SetCardCfgHandle, ptrTarjetaRecord, tarjetaRecord.dwSize, ptrTarjetaStatus, tarjetaStatus.dwSize,ref dwRetornado);
                resultado= VerificarEstadoSetTarjeta(ref flag,dwEstado, ref tarjetaStatus);

            }

            Hik_SDK.NET_DVR_StopRemoteConfig(SetCardCfgHandle);
            SetCardCfgHandle = -1;
            Marshal.FreeHGlobal(ptrTarjetaStatus);
            Marshal.FreeHGlobal(ptrTarjetaRecord);
            return resultado;
        }

        public void InicializarEstructuraTarjetaCond(ref Hik_SDK.NET_DVR_CARD_COND tarjetaCond,ref IntPtr ptrTarjetaCond)
        {
            tarjetaCond.Init();
            tarjetaCond.dwSize = (uint)Marshal.SizeOf(tarjetaCond);
            tarjetaCond.dwCardNum = 1;
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

        public void InicializarEstructuraTarjetaRecord(ref Hik_SDK.NET_DVR_CARD_RECORD tarjetaRecord, ref IntPtr ptrTarjetaRecord, int nuevoNumeroTarjeta, string nuevoNombreRelacionadoTarjeta)
        {
            tarjetaRecord.Init();
            tarjetaRecord.dwSize = (uint)Marshal.SizeOf(tarjetaRecord);
            tarjetaRecord.byCardType = 1;

            byte[] byTempCardNo = new byte[Hik_SDK.ACS_CARD_NO_LEN];

            //asignamos el numero de tarjeta nuevo a la estructura record
            byTempCardNo = System.Text.Encoding.UTF8.GetBytes(nuevoNumeroTarjeta.ToString());
            for (int i = 0; i < byTempCardNo.Length; i++)
            {
                tarjetaRecord.byCardNo[i] = byTempCardNo[i];
            }

            //asignamos los horarios de la tarjeta (1= all day template, que es por default)
            tarjetaRecord.wCardRightPlan[0] = 1;

            //asignamos el id de la persona relacionada a la tarjeta
            tarjetaRecord.dwEmployeeNo = (uint) nuevoNumeroTarjeta;

            //asignamos el nombre relacionado a la estructura record
            byte[] byTempName = new byte[Hik_SDK.NAME_LEN];
            byTempName = System.Text.Encoding.Default.GetBytes(nuevoNombreRelacionadoTarjeta);
            for (int i = 0; i < byTempName.Length; i++)
            {
                tarjetaRecord.byName[i] = byTempName[i];
            }

            //asignamos la fecha de inicio y vencimiento de la tarjeta
            asignarFechaDeInicioYVencimientoTarjeta(ref tarjetaRecord);

            //asignamos los permisos de la tarjeta (1 = default osea todos)
            tarjetaRecord.byDoorRight[0] = 1;

            //asignamos la estructura record a un puntero
            ptrTarjetaRecord = Marshal.AllocHGlobal((int)tarjetaRecord.dwSize);
            Marshal.StructureToPtr(tarjetaRecord, ptrTarjetaRecord, false);



        }

        public void asignarFechaDeInicioYVencimientoTarjeta(ref Hik_SDK.NET_DVR_CARD_RECORD tarjetaRecord)
        {
            ushort anioActual = (ushort)DateTime.Now.Year;
            byte mesActual = (byte)DateTime.Now.Month;
            byte diaActual = (byte)DateTime.Now.Day;
            byte horaActual = (byte)DateTime.Now.Hour;

            //asignamos que exista una fecha de inicio y vencimiento
            tarjetaRecord.struValid.byEnable = 1;

            //asignamos la fecha de inicio de la tarjeta
            tarjetaRecord.struValid.struBeginTime.wYear = anioActual;
            tarjetaRecord.struValid.struBeginTime.byMonth = mesActual;
            tarjetaRecord.struValid.struBeginTime.byDay = diaActual;
            tarjetaRecord.struValid.struBeginTime.byHour = horaActual;
            tarjetaRecord.struValid.struBeginTime.byMinute = 11;
            tarjetaRecord.struValid.struBeginTime.bySecond = 11;

            //asignamos la fecha de vencimiento de la tarjeta
            tarjetaRecord.struValid.struEndTime.wYear = anioActual;
            tarjetaRecord.struValid.struEndTime.byMonth = mesActual;
            tarjetaRecord.struValid.struEndTime.byDay = diaActual;
            tarjetaRecord.struValid.struEndTime.byHour = horaActual;
            tarjetaRecord.struValid.struEndTime.byMinute = 11;
            tarjetaRecord.struValid.struEndTime.bySecond = 11;
        }


        public Hik_Resultado VerificarEstadoSetTarjeta(ref bool flag,int dwEstado, ref Hik_SDK.NET_DVR_CARD_STATUS tarjetaStatus)
        {
            Hik_Resultado resultado = new Hik_Resultado();


            switch (dwEstado) {
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_NEEDWAIT:
                    //esperamos


                    break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FAILED:
                    //fallo
                    flag= false;
                    resultado.Exito = false;
                    resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                    resultado.Mensaje = "Error al enviar la informacion de la tarjeta";

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
                        resultado.Exito = false;
                        resultado.Codigo = tarjetaStatus.dwErrorCode.ToString();
                        resultado.Mensaje = "Llega a Succes pero no se pudo establecer la tarjeta, revisar el codigo dwErrorCode de tarjetaStatus";

                      
                    }
                    else
                    {
                        resultado.Exito = true;
                        resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                        resultado.Mensaje = "Se pudo establecer una tarjeta de forma exitosa!";
                    
                    }

                    break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FINISH:
                    //finalizo
                    Console.WriteLine("NET_DVR_SET_CARD finalizo");
                    break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_EXCEPTION:
                    //exception
                    resultado.Exito = false;
                    resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                    resultado.Mensaje = "Se arrojo una excepcion, no se pudo settear tarjeta";

                    break;
                default:
                    //error desconocido, no se pudo settear tarjeta
                    resultado.Exito = false;
                    resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                    resultado.Mensaje = "error desconocido, no se pudo settear tarjeta";
                    break;
            }

            return resultado;
        }


        //get tarjeta 


        //del tarjeta

    }
}
