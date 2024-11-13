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

        public void InicializarEstructuraTarjetaRecord(ref Hik_SDK.NET_DVR_CARD_RECORD tarjetaRecord, ref IntPtr ptrTarjetaRecord, int nuevoNumeroDeTarjeta, string nuevoNombreRelacionadoTarjeta)
        {
            tarjetaRecord.Init();
            tarjetaRecord.dwSize = (uint)Marshal.SizeOf(tarjetaRecord);
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
            tarjetaRecord.dwEmployeeNo = (uint) nuevoNumeroDeTarjeta;

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

        public void InicializarTarjetaSendData(ref Hik_SDK.NET_DVR_CARD_SEND_DATA tarjetaSendData,ref IntPtr ptrTarjetaSendData)
        {
            tarjetaSendData.Init();
            tarjetaSendData.dwSize = (uint)Marshal.SizeOf(tarjetaSendData);

            //ACS_CARD_NO_LEN es una const que es 32
            byte[] byTempCardNo = new byte[Hik_SDK.ACS_CARD_NO_LEN];

            for (int i = 0; i < byTempCardNo.Length; i++)
            {
                tarjetaSendData.byCardNo[i] = byTempCardNo[i];
            }

            IntPtr ptrStruSendData = Marshal.AllocHGlobal((int)tarjetaSendData.dwSize);
            Marshal.StructureToPtr(tarjetaSendData, ptrStruSendData, false);
        }


        //get tarjeta 
        private Hik_Resultado ObtenerUnaTarjeta(int nroTarjetaABuscar)
        {
            Hik_Resultado hik_Resultado = new Hik_Resultado();

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

            InicializarEstructuraTarjetaCond(ref tarjetaCond, ref ptrTarjetaCond);
          
            
            Hik_SDK.NET_DVR_CARD_RECORD tarjetaRecord = new Hik_SDK.NET_DVR_CARD_RECORD();
            IntPtr ptrTarjetaRecord = IntPtr.Zero;

            InicializarEstructuraTarjetaRecord(ref tarjetaRecord, ref ptrTarjetaRecord, nroTarjetaABuscar);


            Hik_SDK.NET_DVR_CARD_SEND_DATA tarjetaSendData = new Hik_SDK.NET_DVR_CARD_SEND_DATA();
            IntPtr ptrTarjetaSendData = IntPtr.Zero;

            InicializarTarjetaSendData(ref tarjetaSendData, ref ptrTarjetaSendData);


            //Hik_SDK.NET_DVR_GET_CARD es una constante que vale 2560
            getCardCfgHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.IdUsuario, Hik_SDK.NET_DVR_GET_CARD, ptrStruCond, (int)struCond.dwSize, null, this.Handle);
            
            if (getCardCfgHandle < 0)
            {
                hik_Resultado.Exito = false;
                hik_Resultado.Mensaje = "Error al obtener la tarjeta: ";
                hik_Resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();

               
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

                    //!!!!!! este metodo convierte el ptrTarjetaRecord a un objeto tarjetaRecord y despues lo castea, meterlo en un try catch si se rompe o directamente sacarlo
                    tarjetaRecord = (Hik_SDK.NET_DVR_CARD_RECORD)Marshal.PtrToStructure(ptrTarjetaRecord, typeof(Hik_SDK.NET_DVR_CARD_RECORD));

                    resultadosBucle = VerificarEstadoGetTarjeta(ref flag,ref dwState, tarjetaRecord);
                }
                //asigno el resultado final al resultado que retorno de la funcion mayor
                hik_Resultado = resultadosBucle;

                //si todo salio bien liberamos memoria
                Hik_SDK.NET_DVR_StopRemoteConfig(GetCardCfgHandle);
                GetCardCfgHandle = -1;
                Marshal.FreeHGlobal(ptrTarjetaSendData);
                Marshal.FreeHGlobal(ptrTarjetaRecord);
            }
            return hik_Resultado;
        }


        public Hik_Resultado VerificarEstadoGetTarjeta(ref bool flag,ref int dwState,Hik_SDK.NET_DVR_CARD_RECORD tarjetaRecord)
        {
            Hik_Resultado hik_Resultado = new Hik_Resultado();



            switch (dwState)
            {
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_NEEDWAIT:
                    //esperamos
                    Thread.Sleep(2);
                    break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FAILED:
                    //fallo
                    flag = false;
                    hik_Resultado.Exito = false;
                    hik_Resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                    hik_Resultado.Mensaje = "Error al obtener la informacion de la tarjeta";

                    break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS:
                    //exito
                    flag = false;
                    hik_Resultado.Exito = true;
                    hik_Resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                    hik_Resultado.Mensaje = "Se pudo obtener la informacion de la tarjeta de forma exitosa!";
                break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FINISH:
                    //finalizo
                    flag = false;
                    hik_Resultado.Exito = true;
                    hik_Resultado.Mensaje = "Obtener informacion de la tarjeta finalizo";
                    Console.WriteLine("NET_DVR_GET_CARD finalizo");
                break;
                case (int)Hik_SDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_EXCEPTION:
                    //exception
                    hik_Resultado.Exito = false;
                    hik_Resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                    hik_Resultado.Mensaje = "Se produjo una excepcion NET_SDK_CONFIG_STATUS_EXCEPTION";
                break;
                default:
                    //error desconocido, no se pudo obtener tarjeta
                    hik_Resultado.Exito = false;
                    hik_Resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                    hik_Resultado.Mensaje = "error desconocido, no se pudo obtener la tarjeta";
                    break;
            }
            
            return hik_Resultado;
        }


        //del tarjeta


        // private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (m_lDelCardCfgHandle != -1)
        //    {
        //        if (CHCNetSDK.NET_DVR_StopRemoteConfig(m_lDelCardCfgHandle))
        //        {
        //            m_lDelCardCfgHandle = -1;
        //        }
        //    }
        //    CHCNetSDK.NET_DVR_CARD_COND struCond = new CHCNetSDK.NET_DVR_CARD_COND();
        //    struCond.Init();
        //    struCond.dwSize = (uint)Marshal.SizeOf(struCond);
        //    struCond.dwCardNum = 1;
        //    IntPtr ptrStruCond = Marshal.AllocHGlobal((int)struCond.dwSize);
        //    Marshal.StructureToPtr(struCond, ptrStruCond, false);

        //    CHCNetSDK.NET_DVR_CARD_SEND_DATA struSendData = new CHCNetSDK.NET_DVR_CARD_SEND_DATA();
        //    struSendData.Init();
        //    struSendData.dwSize = (uint)Marshal.SizeOf(struSendData);
        //    byte[] byTempCardNo = new byte[CHCNetSDK.ACS_CARD_NO_LEN];
        //    byTempCardNo = System.Text.Encoding.UTF8.GetBytes(textBoxCardNo.Text);
        //    for (int i = 0; i < byTempCardNo.Length; i++)
        //    {
        //        struSendData.byCardNo[i] = byTempCardNo[i];
        //    }
        //    IntPtr ptrStruSendData = Marshal.AllocHGlobal((int)struSendData.dwSize);
        //    Marshal.StructureToPtr(struSendData, ptrStruSendData, false);

        //    CHCNetSDK.NET_DVR_CARD_STATUS struStatus = new CHCNetSDK.NET_DVR_CARD_STATUS();
        //    struStatus.Init();
        //    struStatus.dwSize = (uint)Marshal.SizeOf(struStatus);
        //    IntPtr ptrdwState = Marshal.AllocHGlobal((int)struStatus.dwSize);
        //    Marshal.StructureToPtr(struStatus, ptrdwState, false);

        //    m_lGetCardCfgHandle = CHCNetSDK.NET_DVR_StartRemoteConfig(m_UserID, CHCNetSDK.NET_DVR_DEL_CARD, ptrStruCond, (int)struCond.dwSize, null, this.Handle);
        //    if (m_lGetCardCfgHandle < 0)
        //    {
        //        MessageBox.Show("NET_DVR_DEL_CARD error:" + CHCNetSDK.NET_DVR_GetLastError());
        //        Marshal.FreeHGlobal(ptrStruCond);
        //        return;
        //    }
        //    else
        //    {
        //        int dwState = (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS;
        //        uint dwReturned = 0;
        //        while (true)
        //        {
        //            dwState = CHCNetSDK.NET_DVR_SendWithRecvRemoteConfig(m_lGetCardCfgHandle, ptrStruSendData, struSendData.dwSize, ptrdwState, struStatus.dwSize, ref dwReturned);
        //            if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_NEEDWAIT)
        //            {
        //                Thread.Sleep(10);
        //                continue;
        //            }
        //            else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FAILED)
        //            {
        //                MessageBox.Show("NET_DVR_DEL_CARD fail error: " + CHCNetSDK.NET_DVR_GetLastError());
        //            }
        //            else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS)
        //            {
        //                MessageBox.Show("NET_DVR_DEL_CARD success");
        //            }
        //            else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FINISH)
        //            {
        //                MessageBox.Show("NET_DVR_DEL_CARD finish");
        //                break;
        //            }
        //            else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_EXCEPTION)
        //            {
        //                MessageBox.Show("NET_DVR_DEL_CARD exception error: " + CHCNetSDK.NET_DVR_GetLastError());
        //                break;
        //            }
        //            else
        //            {
        //                MessageBox.Show("unknown status error: " + CHCNetSDK.NET_DVR_GetLastError());
        //                break;
        //            }
        //        }
        //    }
        //    CHCNetSDK.NET_DVR_StopRemoteConfig(m_lDelCardCfgHandle);
        //    m_lDelCardCfgHandle = -1;
        //    Marshal.FreeHGlobal(ptrStruSendData);
        //    Marshal.FreeHGlobal(ptrdwState);
        //}

    }
}
