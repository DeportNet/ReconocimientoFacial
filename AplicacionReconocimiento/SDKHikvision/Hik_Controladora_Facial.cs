using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static DeportNetReconocimiento.SDK.Hik_SDK;

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


        //Obtener una cara desde el dispositivo
        public Hik_Resultado ObtenerCara(int cardReaderNumber, String cardNumber)
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
            Hik_SDK.NET_DVR_FACE_COND struCond = new Hik_SDK.NET_DVR_FACE_COND();
            int dwSize = 0;
            IntPtr ptrStruCond = IntPtr.Zero;
            InicializarFaceCond(ref struCond, ref dwSize, (uint)cardReaderNumber, cardNumber, ref ptrStruCond);

            GetFaceCfgHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.IdUsuario, Hik_SDK.NET_DVR_GET_FACE, ptrStruCond, dwSize, null, IntPtr.Zero);


            // revisamos el valor del handler, si sale mal, libera la memoria y muestra un mensaje de error
            if (GetFaceCfgHandle == -1)
            {
                resultado.Exito = false;
                resultado.Mensaje = "Error al obtener la cara";
                resultado.Codigo= Hik_SDK.NET_DVR_GetLastError().ToString();
            }
            else
            {

                //si sale bien, se inicializa la estructura de datos de la cara
                bool flag = true;
                int dwStatus = 0;

                //Inicializamos la estructura NET_DEVR_FACE_RECORD
                Hik_SDK.NET_DVR_FACE_RECORD struRecord = new Hik_SDK.NET_DVR_FACE_RECORD(); 
                IntPtr ptrOutBuff = IntPtr.Zero; 
                uint dwOutBuffSize = 0; 
                InicializarFaceRecordGet(ref struRecord, ref ptrOutBuff, ref dwOutBuffSize);

                while (flag)
                {
                    dwStatus = Hik_SDK.NET_DVR_GetNextRemoteConfig_FaceRecord(GetFaceCfgHandle, ref struRecord, (int)dwOutBuffSize);
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
                case Hik_SDK.NET_SDK_GET_NEXT_STATUS_SUCCESS:
                    //Procesamos la informacion facial

                    ProcesarInformacionFacialRecord(ref struRecord, ref flag);

                    resultado.Exito = true;
                    resultado.Mensaje = "Se obtuvo la cara de forma exitosa";
                    break;
                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS_NEED_WAIT:
                    resultado.Exito = false;
                    resultado.Mensaje = "Hay que esperar";
                    resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                    //Esperamos
                    break;
                case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FAILED:
                    //Detenemos el proceso si hubo un fallo

                    Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                    resultado.Exito = false;
                    resultado.Mensaje= "Fallo a la hora de obtener estado";
                    resultado.Codigo= Hik_SDK.NET_DVR_GetLastError().ToString();
                    flag = false;

                    break;
                case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FINISH:
                    //Terminamos el proceso si se finalizo correctamente

                    Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                    resultado.Exito = true;
                    resultado.Mensaje= "El proceso termino";
                    flag = false;

                    break;
                default:
                    //Si no se conoce el estado del sistema, se detiene el proceso

                    Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                    resultado.Exito = false;
                    resultado.Mensaje= "No se conoce el estado del sistema";
                    resultado.Codigo= Hik_SDK.NET_DVR_GetLastError().ToString();
                    flag = false;
                    break;

            }

            return resultado;
        }
        //Recoge y almacena la foto encontrada en el dispositivo
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

                    //if (hay conexion)
                    //lo mando como archivo
                    //sino lo muestro con pictureBoxFace

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
        private void InicializarFaceRecordGet(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, ref IntPtr ptrOutBuff, ref uint dwOutBuffSize)
        {
            struRecord.Init(); 
            struRecord.dwSize = Marshal.SizeOf(struRecord);
            ptrOutBuff = Marshal.AllocHGlobal((int)struRecord.dwSize);
            Marshal.StructureToPtr(struRecord, ptrOutBuff, false);
            dwOutBuffSize = (uint)struRecord.dwSize;

        }
        private void InicializarFaceCond(ref Hik_SDK.NET_DVR_FACE_COND struCond, ref int dwSize, uint cardReaderNumber, String cardNumber, ref IntPtr ptrStruCond)
        {
            struCond.Init();
            struCond.dwSize = Marshal.SizeOf(struCond);
            dwSize = (int)struCond.dwSize;

            //se elige el cardreader, si no se elige, se pone en 0
            //Se asigna el valor de dwEnableReaderNo

            struCond.dwEnableReaderNo = (int)cardReaderNumber;
            struCond.dwFaceNum = 1;



            //Se pasa byte por byte para evitar errores de desbordamiento
            byte[] byTemp = Encoding.UTF8.GetBytes(cardNumber);
            for (int i = 0; i < byTemp.Length; i++)
            {
                struCond.byCardNo[i] = byTemp[i];
            }


            //Reservamos memoria para el puntero de struCond
            ptrStruCond = Marshal.AllocHGlobal(dwSize);

            //Convertimos la estructura a un puntero
            Marshal.StructureToPtr(struCond, ptrStruCond, false);

        }



        //Capturar una nueva cara 
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


            //Si hubo un error
            if (CapFaceCfgHandle == -1)
            {
                Marshal.FreeHGlobal(ptrCapCond);
                resultado.Exito = false;
                resultado.Mensaje = "Error al momento de capturar datos faciales";
                resultado.Codigo= Hik_SDK.NET_DVR_GetLastError().ToString();

            }
            else
            {
                Hik_SDK.NET_DVR_CAPTURE_FACE_CFG struFaceCfg = new Hik_SDK.NET_DVR_CAPTURE_FACE_CFG();
                bool flag = true;
                int dwStatus = 0;
                uint dwOutBuffSize = 0;
                IntPtr lpOutBuff = IntPtr.Zero; //puntero a la estructura de datos struFaceCfg inicializado en 0

                InicializarCaptureFaceConfg(ref struFaceCfg, ref dwOutBuffSize, ref lpOutBuff);

                while (flag)
                {
                    dwStatus = Hik_SDK.NET_DVR_GetNextRemoteConfig_FaceCfg(CapFaceCfgHandle, ref struFaceCfg, (int)dwOutBuffSize);
                    resultado = VerificarEstadoCapturarCara(ref flag, ref struFaceCfg, dwStatus);

                }
            }

            Marshal.FreeHGlobal(ptrCapCond);
            return resultado;
        }
        private Hik_Resultado VerificarEstadoCapturarCara(ref bool flag, ref Hik_SDK.NET_DVR_CAPTURE_FACE_CFG struFaceCfg, int dwStatus)
        {
            Hik_Resultado resultado = new Hik_Resultado();
            bool capturaExitosa = false;
            switch (dwStatus)
            {


                case Hik_SDK.NET_SDK_GET_NEXT_STATUS_SUCCESS://1000

                    ProcesarInformacionFacialCaptureCfg(ref struFaceCfg, ref flag);
                    resultado.Exito = true;
                    resultado.Mensaje = "Se capturo la cara de forma exitosa";
                    capturaExitosa = true;

                    break;
                case Hik_SDK.NET_SDK_GET_NEXT_STATUS_NEED_WAIT: //1001

                    break;
                case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FAILED: //1003  
                    Hik_SDK.NET_DVR_StopRemoteConfig(CapFaceCfgHandle);

                    flag = false;

                    resultado.Exito = false;
                    resultado.Mensaje = "Fallo al capturar la cara";
                    resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();

                    break;
                case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FINISH: //1002

                    Hik_SDK.NET_DVR_StopRemoteConfig(CapFaceCfgHandle);
                    flag = false;

                    if (!capturaExitosa)
                    {
                        resultado.Exito = true;
                        resultado.Mensaje = "El proceso termino";
                    }
           
                        break;

                case Hik_SDK.NET_SDK_GET_NEXT_STATUS_TIMEOUT: //1004

                    Hik_SDK.NET_DVR_StopRemoteConfig(CapFaceCfgHandle);
                    flag = false;
                    resultado.Exito = true;
                    resultado.Mensaje = "Se agotó el tiempo de espera";

                    break;
                default:

                    Hik_SDK.NET_DVR_StopRemoteConfig(CapFaceCfgHandle);
                    flag = false;

                    resultado.Exito = false;
                    resultado.Mensaje= "Se desconoce el error";
                    resultado.Codigo= Hik_SDK.NET_DVR_GetLastError().ToString();
                    break;
            }

            return resultado;
        }
        //Procesa y almacena la información de la foto sacada
        private void ProcesarInformacionFacialCaptureCfg(ref Hik_SDK.NET_DVR_CAPTURE_FACE_CFG struFaceCfg, ref bool flag)
        {
                //Si la estructra tiene una foto
            if (struFaceCfg.dwFacePicSize != 0)
            {
                //Almaceno la foto
                string strpath = null;
                DateTime dt = DateTime.Now;
                Console.WriteLine(Environment.CurrentDirectory);
                strpath = string.Format("captura.jpg", Environment.CurrentDirectory);
                try
                {
                    using (FileStream fs = new FileStream(strpath, FileMode.OpenOrCreate))
                    {

                        int FaceLen = (int)struFaceCfg.dwFacePicSize;
                        byte[] by = new byte[FaceLen];
                        Marshal.Copy(struFaceCfg.pFacePicBuffer, by, 0, FaceLen);
                        fs.Write(by, 0, FaceLen);
                        fs.Close();
                    }

                    //pictureBoxFace.Image = Image.FromFile(strpath);
                    //textBoxFilePath.Text = string.Format("{0}\\{1}", Environment.CurrentDirectory, strpath);
                    // MessageBox.Show("Capture succeed", "SUCCESSFUL", MessageBoxButtons.OK);
                }
                catch
                {
                    flag = false;
                    //MessageBox.Show("Informacion facial mal capturada", "Error", MessageBoxButtons.OK);
                }

            }
        }
        private void InicializarCaptureFaceConfg(ref Hik_SDK.NET_DVR_CAPTURE_FACE_CFG struFaceCfg, ref uint dwOutBuffSize, ref IntPtr lpOutBuff)
        {
            struFaceCfg.Init();
            dwOutBuffSize = (uint)Marshal.SizeOf(struFaceCfg);
            Console.WriteLine(dwOutBuffSize);
            lpOutBuff = Marshal.AllocHGlobal((int)dwOutBuffSize);
            Marshal.StructureToPtr(struFaceCfg, lpOutBuff, false); //Convierto la estructura en puntero
        }
        private void InicializarCaptureFaceCond(ref Hik_SDK.NET_DVR_CAPTURE_FACE_COND strucCapCond, ref IntPtr ptrCapCond, ref int dwInBufferSize)
        {
            strucCapCond.Init(); //inicializamos 

            strucCapCond.dwSize = Marshal.SizeOf(strucCapCond); // asignamos el tam de strucCapCond

            dwInBufferSize = (int)strucCapCond.dwSize; //asignamos el tam de strucCapCond
            Console.WriteLine(dwInBufferSize);

            ptrCapCond = Marshal.AllocHGlobal(dwInBufferSize); //reservamos memoria para el puntero de strucCapCond
            Marshal.StructureToPtr(strucCapCond, ptrCapCond, false); //pasamos la estructura de datos strucCapCond a un puntero

        }



        //Establecer una cara en el dispositivo
        //Hay que hacer algo para poder crear tarjetas y numeros de tarjetas y esos mandarlos, porque para poder agregar una cara, es necesario el numero de tarjeta.
        public Hik_Resultado EstablecerUnaCara(uint cardReaderNumber, string cardNumber)
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
            Console.WriteLine(setFaceCfgHandle);

            if (SetFaceCfgHandle == -1)
            {

                resultado.Exito = false;
                resultado.Mensaje = "Error al  establecer la cara";
                resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
            }
            else
            {

                Hik_SDK.NET_DVR_FACE_RECORD struRecord = new Hik_SDK.NET_DVR_FACE_RECORD();

                InicializarFaceRecordSet(ref struRecord, cardNumber);

                resultado = BuscarFotoParaIngresar(ref struRecord, Environment.CurrentDirectory + "/Fabri.jpg");  //Verificar el tema de la ruta, si hace falta ponerle el captura.jpg


                if (resultado.Exito)
                { 
                    //se limpia dwInBufferSize
                    dwInBufferSize = 0;
                    int dwStatus = 0;
                    bool flag = true;


                    Hik_SDK.NET_DVR_FACE_STATUS struStatus = new Hik_SDK.NET_DVR_FACE_STATUS();
                    IntPtr dwOutDataLen = IntPtr.Zero;
                    uint dwOutBufferSize = 0;

                    //dentro de la inicializacion tambien damos valores a StruStatus
                    InicializarFaceStatus(ref struStatus, ref dwOutBufferSize, ref dwOutDataLen, cardNumber, cardReaderNumber);

                    dwInBufferSize = struRecord.dwSize;

                    while (flag)
                    {

                        dwStatus = Hik_SDK.NET_DVR_SendWithRecvRemoteConfigFacial(SetFaceCfgHandle, ref struRecord, dwInBufferSize, ref struStatus, (int)dwOutBufferSize, dwOutDataLen);
                        Console.WriteLine(dwStatus);
                        resultado = verificarEstadoEstableceCara(ref struStatus, dwStatus, ref flag);

                    }
                }
            }

            Marshal.FreeHGlobal(ptrStruCond);

            return resultado;
        }
        private Hik_Resultado verificarEstadoEstableceCara(ref Hik_SDK.NET_DVR_FACE_STATUS struStatus, int dwStatus, ref bool flag)
        {
            Hik_Resultado resultado = new Hik_Resultado();
            bool caraEstabelcida = false;

            switch (dwStatus)
            {
                case Hik_SDK.NET_SDK_GET_NEXT_STATUS_SUCCESS: //1000
                    resultado = ProcesarEstablecerCara(ref struStatus, ref flag);
                    break;

                case Hik_SDK.NET_SDK_GET_NEXT_STATUS_NEED_WAIT: //1001
                    resultado.Exito = false;
                    resultado.Mensaje = "Se necesita Esperar";
                    resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                    break;

                case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FAILED: //1003

                    Hik_SDK.NET_DVR_StopRemoteConfig(SetFaceCfgHandle);
                    flag = false;
                    resultado.Exito = false;
                    resultado.Mensaje = "Fallo al establecer la cara";
                    resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                    break;

                case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FINISH: //1002

                    Hik_SDK.NET_DVR_StopRemoteConfig(SetFaceCfgHandle);
                    flag = false;
                    if (!caraEstabelcida)
                    {
                        resultado.Exito = true;
                        resultado.Mensaje = "El proceso termino";
                    }
                    break;

                default:
                    Hik_SDK.NET_DVR_StopRemoteConfig(SetFaceCfgHandle);
                    flag = false;

                    resultado.Exito = false;
                    resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                    resultado.Mensaje = "Error desconocido";
                    break;
            }
            return resultado;
        }
        private Hik_Resultado ProcesarEstablecerCara(ref Hik_SDK.NET_DVR_FACE_STATUS struStatus, ref bool flag)
        {
            Hik_Resultado resultado = new Hik_Resultado();


            if (struStatus.byRecvStatus == 1)
            {
                resultado.Exito = true;
                resultado.Mensaje = "Se estableció la informacion facial de forma exitosa";
            }
            else
            {
                flag = false;
                resultado.Exito = false;
                resultado.Mensaje = "Hubo un error en establecer la informacion facial";
                resultado.Codigo = struStatus.byRecvStatus.ToString();
            }

            return resultado;
        }
        //Evalua si la foto seleccionada cumple con los requeusitos
        private Hik_Resultado BuscarFotoParaIngresar(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, String ubicacionArchivo)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            if (!File.Exists(ubicacionArchivo))
            {
                //la foto no existe
                resultado.Exito = false;
                resultado.Mensaje = "La foto de la cara no existe";
            }
            else
            {

                FileStream fileStr = new FileStream(ubicacionArchivo, FileMode.OpenOrCreate);

                if (fileStr.Length == 0) //la foto es 0k
                {
                    resultado.Exito = false;
                    resultado.Mensaje = "La foto de la cara es de 0k, por favor ingrese otra foto";

                }
                else if (200 * 1024 < fileStr.Length)//la foto es 200k
                {
                    resultado.Exito = false;
                    resultado.Mensaje = "La foto de la cara es mayor a 200k, por favor ingrese otra foto";
                }
                else
                {
                    resultado = ProcesarFotoEncontrada(ref struRecord, fileStr); 
                }
            }
            return resultado;
        }
        private Hik_Resultado ProcesarFotoEncontrada(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, FileStream fileStr)
        {
            Hik_Resultado resultado = new Hik_Resultado();
            try
            {
                //Procesamos los datos de la foto encontrada
                int dwFaceLenAux = (int)struRecord.dwFaceLen;
                int.TryParse(fileStr.Length.ToString(), out dwFaceLenAux);
                struRecord.dwFaceLen = dwFaceLenAux;

                int iLen = (int)struRecord.dwFaceLen;
                byte[] by = new byte[iLen];
                struRecord.pFaceBuffer = Marshal.AllocHGlobal(iLen);
                fileStr.Read(by, 0, iLen);
                Marshal.Copy(by, 0, struRecord.pFaceBuffer, iLen);
                fileStr.Close();
                //textBoxFilePath.Text = "";

                //resultado
                resultado.Exito = true;
                resultado.Mensaje = "Se leyo la foto de la cara de forma exitosa";
            }
            catch
            {
                fileStr.Close();
                resultado.Exito = false;
                resultado.Mensaje = "Fallo leer la informacion facial, intentelo de nuevo";
                resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
            }
            return resultado;
        }
        private void InicializarFaceStatus(ref Hik_SDK.NET_DVR_FACE_STATUS struStatus, ref uint dwOutBufferSize, ref nint dwOutDataLen, string cardNumber, uint dwReaderNo)
        {
            struStatus.Init();
            struStatus.dwSize = Marshal.SizeOf(struStatus);

            dwOutBufferSize = (uint)struStatus.dwSize;
            dwOutDataLen = (nint)Marshal.AllocHGlobal(sizeof(int));

            byte[] byRecordNo = Encoding.UTF8.GetBytes(cardNumber);
            for (int i = 0; i < byRecordNo.Length; i++)
            {
                struStatus.byCardNo[i] = byRecordNo[i];
            }

            struStatus.dwReaderNo = (int)dwReaderNo;
        }
        private void InicializarFaceRecordSet(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, string cardNumber)
        {

            struRecord.Init();
            struRecord.dwSize = Marshal.SizeOf(struRecord);

            //Se pasa byte por byte para evitar errores de desbordamiento
            byte[] byRecordNo = Encoding.UTF8.GetBytes(cardNumber);
            for (int i = 0; i < byRecordNo.Length; i++)
            {
                struRecord.byCardNo[i] = byRecordNo[i];
            }
        }



        //Eliminar una cara 
        public Hik_Resultado EliminarCara(int cardReaderNumber,string cardNumber)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            
            //Resetea el picutreBox y el textBox
           // if (pictureBoxFace.Image != null)
          //  {
            //    pictureBoxFace.Image.Dispose();
                //pictureBoxFace.Image = null;
         //   }
           // textBoxFilePath.Text = "";
           

            Hik_SDK.NET_DVR_FACE_PARAM_CTRL_CARDNO struCardNo = new Hik_SDK.NET_DVR_FACE_PARAM_CTRL_CARDNO();

            int dwSize = 0;
            IntPtr lpInBuffer = IntPtr.Zero;
            InicilizarParamControlCardNo(ref struCardNo, ref dwSize, cardNumber, ref lpInBuffer, cardReaderNumber);

            if ( false == Hik_SDK.NET_DVR_RemoteControl(Hik_Controladora_General.IdUsuario, Hik_SDK.NET_DVR_DEL_FACE_PARAM_CFG, ref  struCardNo, (int)dwSize))
            {
                resultado.Exito = false;
                resultado.Mensaje = "Hubo un error a la hora de eliminar la estructura";
                resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
            }
            else
            {
                resultado.Exito = true;
                resultado.Mensaje = "Se eliminó la cara del id: " + cardNumber +" de manera correcta";
            }

            return resultado;
        }
        private void InicilizarParamControlCardNo(ref Hik_SDK.NET_DVR_FACE_PARAM_CTRL_CARDNO struCardNo,ref int dwSize, string cardNumber, ref IntPtr lpInBuffer, int cardReaderNumber)
        {
            struCardNo.Init();
            struCardNo.dwSize = Marshal.SizeOf(struCardNo);
            struCardNo.byMode = 0;
            dwSize = struCardNo.dwSize;

            byte[] byCardNo = Encoding.UTF8.GetBytes(cardNumber);
            for (int i = 0; i < byCardNo.Length; i++)
            {
                struCardNo.struByCard.byCardNo[i] = byCardNo[i];
            }
            struCardNo.struByCard.byEnableCardReader[cardReaderNumber - 1] = 1;

            for (int i = 0; i < Hik_SDK.MAX_FACE_NUM; ++i)
            {
                struCardNo.struByCard.byFaceID[i] = 1;//1 para eliminar la cara
            }
        }
        

    }
}
