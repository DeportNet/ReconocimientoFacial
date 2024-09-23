using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.SDK
{
    public class Hik_Controladora_Facial
    {
        //atributos facial

        //private bool soportaFacial


        private int getFaceCfgHandle;
        private int setFaceCfgHandle;
        private int capFaceCfgHandle;


        //propiedades facial

        public int GetFaceCfgHandle
        {
            get { return getFaceCfgHandle; }
            set { getFaceCfgHandle = value; }

        }

        public int SetFaceCfgHandle
        {
            get { return setFaceCfgHandle; }
            set { setFaceCfgHandle = value; }
        }

        public int CapFaceCfgHandle
        {
            get { return capFaceCfgHandle; }
            set { capFaceCfgHandle = value; }
        }

        //constructores 

        public Hik_Controladora_Facial()
        {
            getFaceCfgHandle = -1;
            setFaceCfgHandle = -1;
            capFaceCfgHandle = -1;
        }


        //metodos facial

        //get face
        private Hik_Resultado ObtenerCara(uint cardReaderNumber, int cardNumber)
        {

            Hik_Resultado resultado = new Hik_Resultado();

            //validaciones

            //si ya esta inicializado el handle, lo detiene
            if (GetFaceCfgHandle != -1)
            {
                Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                GetFaceCfgHandle = -1;
            }



            //si ya hay una imagen en el picturebox de la interfaz, la elimina
            //if (pictureBoxFace.Image != null)
            //{
            //    pictureBoxFace.Image.Dispose();
            //    pictureBoxFace.Image = null;
            //}
            

                    

            //Cargamos toda la estructura NET_DVR_FACE_COND
            int dwSize = 0;
            Hik_SDK.NET_DVR_FACE_COND struCond = new Hik_SDK.NET_DVR_FACE_COND();
            IntPtr ptrStruCond = IntPtr.Zero;

            InicializarFaceCond(ref struCond, ref dwSize, cardReaderNumber, cardNumber, ref ptrStruCond);

            GetFaceCfgHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.IdUsuario, Hik_SDK.NET_DVR_GET_FACE, ptrStruCond, dwSize, null, IntPtr.Zero);

            // revisamos el valor del handler, si sale mal, libera la memoria y muestra un mensaje de error
            if (GetFaceCfgHandle == -1)
            {
                resultado.Exito = false;
                resultado.MensajeDeError = "Error al obtener la cara";
                resultado.NumeroDeError = Hik_SDK.NET_DVR_GetLastError().ToString();
             } 
            else
            {

                //si sale bien, se inicializa la estructura de datos de la cara
                bool flag = true;
                int dwStatus = 0;

                //Inicializamos la estructura NET_DEVR_FACE_RECORD
                Hik_SDK.NET_DVR_FACE_RECORD struRecord = new Hik_SDK.NET_DVR_FACE_RECORD(); //creamos una estructura de datos struRecord
                IntPtr ptrOutBuff = IntPtr.Zero; //puntero a la estructura de datos struRecord inicializado en 0
                uint dwOutBuffSize = 0; //tamaño de la estructura de datos struRecord inicializado en 0

                InicializarFaceRecordGet(ref struRecord, ref ptrOutBuff, ref dwOutBuffSize);


                while (flag)
                {
                    dwStatus = Hik_SDK.NET_DVR_GetNextRemoteConfig(GetFaceCfgHandle, ptrOutBuff, dwOutBuffSize);
                    resultado = VerificarEstadoGetCara(ref struRecord, ref flag, dwStatus);

                }


                Marshal.FreeHGlobal(ptrOutBuff);
            }

            //limpiamos memoria
            Marshal.FreeHGlobal(ptrStruCond);

            return resultado;

        }

        private Hik_Resultado VerificarEstadoGetCara(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, ref bool flag, int dwStatus)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            switch (dwStatus)
            {
                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_SUCCESS:
                    //Procesamos la informacion facial

                    ProcesarInformacionFacialRecord(ref struRecord, ref flag);

                    resultado.Exito = true;
                    resultado.MensajeDeExito = "Se obtuvo la cara de forma exitosa";
                    break;
                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NETX_STATUS_NEED_WAIT:
                    //Esperamos
                    break;
                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_FAILED:
                    //Detenemos el proceso si hubo un fallo

                    Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                    resultado.Exito = false;
                    resultado.MensajeDeError = "Fallo a la hora de obtener estado";
                    resultado.NumeroDeError = Hik_SDK.NET_DVR_GetLastError().ToString();
                    flag = false;

                    break;
                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_FINISH:
                    //Terminamos el proceso si se finalizo correctamente

                    Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                    resultado.Exito = true;
                    resultado.MensajeDeExito = "El proceso termino";
                    flag = false;

                    break;
                default:
                    //Si no se conoce el estado del sistema, se detiene el proceso

                    Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                    resultado.Exito = false;
                    resultado.MensajeDeError = "No se conoce el estado del sistema";
                    resultado.NumeroDeError = Hik_SDK.NET_DVR_GetLastError().ToString();
                    flag = false;
                    break;

            }

            return resultado;
        }

        private void InicializarFaceRecordGet(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, ref IntPtr ptrOutBuff, ref uint dwOutBuffSize)
        {
            struRecord.Init(); // inicializamos la estructura de datos struRecord

            struRecord.dwSize = (uint)Marshal.SizeOf(struRecord); //decimos el tamaño de la estructura de datos struRecord

            ptrOutBuff = Marshal.AllocHGlobal((int)struRecord.dwSize); //reservamos (alloc) memoria para la estructura de datos struRecord

            Marshal.StructureToPtr(struRecord, ptrOutBuff, false); //pasamos la estructura de datos struRecord a un puntero

            dwOutBuffSize = struRecord.dwSize; //le decimos el tamaño de la estructura de datos struRecord

        }

        private void InicializarFaceCond(ref Hik_SDK.NET_DVR_FACE_COND struCond,ref int dwSize, uint cardReaderNumber, int cardNumber, ref IntPtr ptrStruCond)
        {
            struCond.Init();
            struCond.dwSize = (uint)Marshal.SizeOf(struCond);
            dwSize = (int)struCond.dwSize;

            //se elige el cardreader, si no se elige, se pone en 0
            //Se asigna el valor de dwEnableReaderNo

            struCond.dwEnableReaderNo = cardReaderNumber;
            struCond.dwFaceNum = 1;

            //Se pasa byte por byte para evitar errores de desbordamiento
            byte[] byTemp = BitConverter.GetBytes(cardNumber);
            for (int i = 0; i < byTemp.Length; i++)
            {
                struCond.byCardNo[i] = byTemp[i];
            }

            //Reservamos memoria para el puntero de struCond
            ptrStruCond = Marshal.AllocHGlobal(dwSize);

            //Convertimos la estructura a un puntero
            Marshal.StructureToPtr(struCond, ptrStruCond, false);

        }

        //Mostrar la foto en la intefaz
        private void ProcesarInformacionFacialRecord(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, ref Boolean Flag)
        {
            string strpath = null;
            DateTime dt = DateTime.Now;
            strpath = string.Format("FacePicture.jpg");



            //si la longitud de la cara es 0, no hace nada y volvemos para atras
            if (struRecord.dwFaceLen != 0)
            {

                /*
                //limpiamos la imagen del picturebox
                if (pictureBoxFace.Image != null)
                {
                    pictureBoxFace.Image.Dispose();
                    pictureBoxFace.Image = null;
                }
                */



                try
                {
                    //creamos un archivo 
                    using (FileStream fs = new FileStream(strpath, FileMode.OpenOrCreate))
                    {
                        int FaceLen = (int)struRecord.dwFaceLen;
                        byte[] by = new byte[FaceLen];
                        Marshal.Copy(struRecord.pFaceBuffer, by, 0, FaceLen);
                        fs.Write(by, 0, FaceLen);
                        fs.Close();
                    }
                    //y escribimos la imagen de la cara en pictureboxface
                    // pictureBoxFace.Image = Image.FromFile(strpath);
                    // textBoxFilePath.Text = string.Format("{0}\\{1}", Environment.CurrentDirectory, strpath);
                }
                catch
                {
                    Flag = false;
                    Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                    MessageBox.Show("ProcessFaceData failed", "Error", MessageBoxButtons.OK);
                }
            }
        }

        //cap face
        public Hik_Resultado CapturarCara()
        {
            Hik_Resultado resultado = new Hik_Resultado();

            if (CapFaceCfgHandle != -1)
            {
                //significa que ya esta capturando, por lo tanto cortamos

                Hik_SDK.NET_DVR_StopRemoteConfig(CapFaceCfgHandle);
                CapFaceCfgHandle = -1;
            }

            /*
            if (pictureBoxFace.Image != null)
            {
                pictureBoxFace.Image.Dispose();
                pictureBoxFace.Image = null;
            }*/

            Hik_SDK.NET_DVR_CAPTURE_FACE_COND struCapCond = new Hik_SDK.NET_DVR_CAPTURE_FACE_COND();
            IntPtr ptrCapCond = IntPtr.Zero;
            int dwInBufferSize = 0;

            InicializarCaptureFaceCond(ref struCapCond, ref ptrCapCond, ref dwInBufferSize);

            CapFaceCfgHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.IdUsuario, Hik_SDK.NET_DVR_CAPTURE_FACE_INFO, ptrCapCond, dwInBufferSize, null, IntPtr.Zero);

            if (CapFaceCfgHandle == -1)
            {
                Marshal.FreeHGlobal(ptrCapCond);

                //configurar HIK_Resultado

                MessageBox.Show("NET_DVR_CAP_FACE_FAIL, ERROR CODE" + Hik_SDK.NET_DVR_GetLastError().ToString(), "Error", MessageBoxButtons.OK);

                //return
            }
            else
            {
                Hik_SDK.NET_DVR_CAPTURE_FACE_CFG struFaceCfg = new Hik_SDK.NET_DVR_CAPTURE_FACE_CFG();
                int dwStatus = 0;
                bool flag = false;
                uint dwOutBuffSize = 0;
                IntPtr lpOutBuff = IntPtr.Zero; //puntero a la estructura de datos struFaceCfg inicializado en 0

                InicializarCaptureFaceConfg(ref struFaceCfg, ref dwOutBuffSize, ref lpOutBuff);

                while (flag)
                {
                    dwStatus = Hik_SDK.NET_DVR_GetNextRemoteConfig(CapFaceCfgHandle, lpOutBuff, dwOutBuffSize);
                    resultado = VerificarEstadoCapturarCara(ref flag, ref struFaceCfg, dwStatus);

                    Marshal.FreeHGlobal(lpOutBuff);
                }

            }


            Marshal.FreeHGlobal(ptrCapCond);
            return resultado;
        }

        private Hik_Resultado VerificarEstadoCapturarCara(ref bool flag, ref Hik_SDK.NET_DVR_CAPTURE_FACE_CFG struFaceCfg, int dwStatus)
        {
            Hik_Resultado resultado = new Hik_Resultado();
               
            switch (dwStatus)
            {
                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_SUCCESS://成功读取到数据，处理完本次数据后需调用next
                    //exito

                    ProcesarInformacionFacialCaptureCfg(ref struFaceCfg, ref flag);

                    resultado.Exito = true;
                    resultado.MensajeDeExito = "Se capturo la cara de forma exitosa";

                    break;
                case (int) Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_NEED_WAIT:
                    //esperamos
                    break;
                case (int) Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_FAILED:
                    //fallo
                    Hik_SDK.NET_DVR_StopRemoteConfig(CapFaceCfgHandle);

                    flag = false;

                    resultado.Exito = false;
                    resultado.MensajeDeError = "Fallo al capturar la cara";
                    resultado.NumeroDeError = Hik_SDK.NET_DVR_GetLastError().ToString();

                    break;
                case (int) Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_FINISH:
                    //termino
                    Hik_SDK.NET_DVR_StopRemoteConfig(CapFaceCfgHandle);
                    flag = false;

                    resultado.Exito = true;
                    resultado.MensajeDeExito= "El proceso termino";
                    
                    break;
                default:
                    Hik_SDK.NET_DVR_StopRemoteConfig(CapFaceCfgHandle);
                    flag = false;

                    resultado.Exito = false;
                    resultado.NumeroDeError = "Se desconoce el error";
                    resultado.NumeroDeError = Hik_SDK.NET_DVR_GetLastError().ToString();
                    break;
            }
            
            return resultado;
        }

        private void ProcesarInformacionFacialCaptureCfg(ref Hik_SDK.NET_DVR_CAPTURE_FACE_CFG struFaceCfg, ref bool flag)
        {
            if (struFaceCfg.dwFacePicSize != 0)
            {
                string strpath = null;
                DateTime dt = DateTime.Now;
                strpath = string.Format("captura.jpg", Environment.CurrentDirectory);
                try
                {
                    using (FileStream fs = new FileStream(strpath, FileMode.OpenOrCreate))
                    {
                        int FaceLen = (int) struFaceCfg.dwFacePicSize;
                        byte[] by = new byte[FaceLen];
                        Marshal.Copy(struFaceCfg.pFacePicBuffer, by, 0, FaceLen);
                        fs.Write(by, 0, FaceLen);
                        fs.Close();
                    }

                    //pictureBoxFace.Image = Image.FromFile(strpath);
                    //textBoxFilePath.Text = string.Format("{0}\\{1}", Environment.CurrentDirectory, strpath);
                    MessageBox.Show("Capture succeed", "SUCCESSFUL", MessageBoxButtons.OK);
                }
                catch
                {
                    flag = false;
                    MessageBox.Show("Informacion facial mal capturada", "Error", MessageBoxButtons.OK);
                }

            }
            

            
        }


        private void InicializarCaptureFaceConfg(ref Hik_SDK.NET_DVR_CAPTURE_FACE_CFG struFaceCfg, ref uint dwOutBuffSize, ref IntPtr lpOutBuff)
        {
            struFaceCfg.Init();

            dwOutBuffSize = (uint)Marshal.SizeOf(struFaceCfg);

            Marshal.StructureToPtr(struFaceCfg, lpOutBuff, false);
        }

        private void InicializarCaptureFaceCond(ref Hik_SDK.NET_DVR_CAPTURE_FACE_COND strucCapCond, ref IntPtr ptrCapCond, ref int dwInBufferSize)
        {
            strucCapCond.Init(); //inicializamos 

            strucCapCond.dwSize = (uint) Marshal.SizeOf(strucCapCond); // asignamos el tam de strucCapCond

            dwInBufferSize = (int)strucCapCond.dwSize; //asignamos el tam de strucCapCond
            ptrCapCond = Marshal.AllocHGlobal(dwInBufferSize); //reservamos memoria para el puntero de strucCapCond

            Marshal.StructureToPtr(strucCapCond, ptrCapCond, false); //pasamos la estructura de datos strucCapCond a un puntero

        }


        //set face
        public Hik_Resultado EstablecerUnaCara(uint cardReaderNumber, int cardNumber)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            //if (textBoxFilePath.Text == "")
            //{
            //    MessageBox.Show("Please choose human Face path");
            //    return;
            //}

            //if (pictureBoxFace.Image != null)
            //{
            //    pictureBoxFace.Image.Dispose();
            //    pictureBoxFace.Image = null;
            //}

            Hik_SDK.NET_DVR_FACE_COND struCond = new Hik_SDK.NET_DVR_FACE_COND();
            int dwInBufferSize = 0;
            IntPtr ptrStruCond = IntPtr.Zero;


            InicializarFaceCond(ref struCond, ref dwInBufferSize, cardReaderNumber, cardNumber, ref ptrStruCond);

            SetFaceCfgHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.IdUsuario, Hik_SDK.NET_DVR_SET_FACE, ptrStruCond, dwInBufferSize, null, IntPtr.Zero);
            if(SetFaceCfgHandle == -1)
            {
                resultado.Exito = false;
                resultado.MensajeDeError = "Error al iniciar establecer la cara";
                resultado.NumeroDeError = Hik_SDK.NET_DVR_GetLastError().ToString();
            }
            else
            {
                Hik_SDK.NET_DVR_FACE_RECORD struRecord = new Hik_SDK.NET_DVR_FACE_RECORD();
               
                InicializarFaceRecordSet(ref struRecord, cardNumber);

                //RECORDAR ASIGNAR UBICACION DEL ARCHIVO!
                resultado = LeerDatosFaciales(ref struRecord, "");

                if(resultado.Exito)
                {
                    //Inicializamos todo

                    //se limpia dwInBufferSize
                    dwInBufferSize = 0;
                    uint dwOutBufferSize= 0;
                    int dwStatus = 0;
                    uint dwOutDataLen = 0;
                    IntPtr lpInBuff = IntPtr.Zero;
                    IntPtr lpOutBuff = IntPtr.Zero;
                    bool flag = true;

                    Hik_SDK.NET_DVR_FACE_STATUS struStatus = new Hik_SDK.NET_DVR_FACE_STATUS();
                    //dentro de la inicializacion tambien damos valores a StruStatus
                    inicializarFaceStatus(ref struStatus, ref dwOutBufferSize, ref dwOutDataLen);

                    Marshal.StructureToPtr(struRecord, lpInBuff, false);
                    Marshal.StructureToPtr(struRecord, lpOutBuff, false);

                    while (flag)
                    {
                        dwStatus = Hik_SDK.NET_DVR_SendWithRecvRemoteConfig(SetFaceCfgHandle, lpInBuff, (uint) dwInBufferSize, lpOutBuff, dwOutBufferSize, ref dwOutDataLen);

                        resultado = verificarEstadoEstableceCara(ref struStatus, dwStatus, ref flag);

                    }

                    //liberamos memoria
                    Marshal.FreeHGlobal(lpInBuff);
                    Marshal.FreeHGlobal(lpOutBuff);
                }

            }

                Marshal.FreeHGlobal(ptrStruCond);

            return resultado;
        }

        private Hik_Resultado verificarEstadoEstableceCara(ref Hik_SDK.NET_DVR_FACE_STATUS struStatus, int dwStatus, ref bool flag)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            switch (dwStatus)
            {
                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_SUCCESS://成功读取到数据，处理完本次数据后需调用next
                    //exito
                    resultado= ProcesarEstablecerCara(ref struStatus, ref flag);
                    break;
                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_NEED_WAIT:
                    //esperamos
                    break;
                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_FAILED:
                    //fallo
                    Hik_SDK.NET_DVR_StopRemoteConfig(SetFaceCfgHandle);
                    flag = false;
                   
                    resultado.Exito = false;
                    resultado.MensajeDeError = "Fallo al establecer la cara";
                    resultado.NumeroDeError = Hik_SDK.NET_DVR_GetLastError().ToString();
                    break;
                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_FINISH:
                    //finalizo
                    Hik_SDK.NET_DVR_StopRemoteConfig(SetFaceCfgHandle);
                    flag = false;

                    resultado.Exito = true;
                    resultado.MensajeDeExito = "El proceso termino";
                    break;
                default:
                    Hik_SDK.NET_DVR_StopRemoteConfig(SetFaceCfgHandle);
                    flag = false;

                    resultado.Exito = false;
                    resultado.NumeroDeError = Hik_SDK.NET_DVR_GetLastError().ToString();
                    resultado.MensajeDeError = "Error desconocido";
                    break;
            }
            return resultado;
        }


        private Hik_Resultado ProcesarEstablecerCara(ref Hik_SDK.NET_DVR_FACE_STATUS struStatus, ref bool flag)
        {
            Hik_Resultado resultado = new Hik_Resultado();
            switch (struStatus.byRecvStatus)
            {
                case 1:
                    resultado.Exito = true;
                    resultado.MensajeDeExito = "Se pudo establecer la informacion facial de forma exitosa";
                    break;
                default:
                    flag = false;

                    resultado.Exito = false;
                    resultado.MensajeDeError = "Hubo un error en establecer la informacion facial";
                    resultado.NumeroDeError = struStatus.byRecvStatus.ToString();
                    break;
            }
            return resultado;
        }

            private void inicializarFaceStatus(ref Hik_SDK.NET_DVR_FACE_STATUS struStatus,ref uint dwOutBufferSize, ref uint dwOutDataLen)
        {
            struStatus.Init();
            struStatus.dwSize = (uint) Marshal.SizeOf(struStatus);

            dwOutBufferSize = struStatus.dwSize;
            dwOutDataLen = (uint) Marshal.AllocHGlobal(sizeof(int));

        }

        private Hik_Resultado LeerDatosFaciales(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, String ubicacionArchivo)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            if (!File.Exists(ubicacionArchivo))
            {
                //la foto no existe
                resultado.Exito = false;
                resultado.MensajeDeError = "La foto de la cara para no existe";
                
            }
            else
            {

                FileStream fileStr = new FileStream(ubicacionArchivo, FileMode.OpenOrCreate);



                if (fileStr.Length == 0)
                {
                   //la foto es 0k

                    resultado.Exito = false;
                    resultado.MensajeDeError = "La foto de la cara es de 0k, por favor ingrese otra foto";

                }else if (200 * 1024 < fileStr.Length)
                {
                    //la foto es 200k

                    resultado.Exito = false;
                    resultado.MensajeDeError = "La foto de la cara es mayor a 200k, por favor ingrese otra foto";
                }
                else
                {
                    //la foto existe, probamos

                    try
                    {
                        //leemos la foto
                        int dwFaceLenAux = (int)struRecord.dwFaceLen;
                        int.TryParse(fileStr.Length.ToString(), out dwFaceLenAux);
                        struRecord.dwFaceLen = (uint) dwFaceLenAux;

                        int iLen =(int) struRecord.dwFaceLen;
                        byte[] by = new byte[iLen];
                        struRecord.pFaceBuffer = Marshal.AllocHGlobal(iLen);
                        fileStr.Read(by, 0, iLen);
                        Marshal.Copy(by, 0, struRecord.pFaceBuffer, iLen);
                        fileStr.Close();
                        //textBoxFilePath.Text = "";

                        //resultado
                        resultado.Exito = true;
                        resultado.MensajeDeExito = "Se leyo la foto de la cara de forma exitosa";
                    }
                    catch
                    {
                        fileStr.Close();
                        resultado.Exito = false;
                        resultado.MensajeDeError = "Fallo leer la informacion facial, intentelo de nuevo";
                    }
                }
            }
            return resultado;
        }


        private void InicializarFaceRecordSet(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord,int cardNumber)
        {
          
           struRecord.Init();
           struRecord.dwSize = (uint) Marshal.SizeOf(struRecord);

            //Se pasa byte por byte para evitar errores de desbordamiento
           byte[] byRecordNo = BitConverter.GetBytes(cardNumber);
           for (int i = 0; i < byRecordNo.Length; i++)
           {
               struRecord.byCardNo[i] = byRecordNo[i];
           }

        }

        //del face


    }
}
