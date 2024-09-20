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
            if (pictureBoxFace.Image != null)
            {
                pictureBoxFace.Image.Dispose();
                pictureBoxFace.Image = null;
            }
            

                    

            //Cargamos toda la estructura NET_DVR_FACE_COND
            int dwSize = 0;
            Hik_SDK.NET_DVR_FACE_COND struCond = new Hik_SDK.NET_DVR_FACE_COND();
            IntPtr ptrStruCond = IntPtr.Zero;

            InicializarStruCond(ref struCond, ref dwSize, cardReaderNumber, cardNumber, ref ptrStruCond);

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

                InicializarStruRecord(ref struRecord, ref ptrOutBuff, ref dwOutBuffSize);


                while (flag)
                {
                    dwStatus = Hik_SDK.NET_DVR_GetNextRemoteConfig(GetFaceCfgHandle, ptrOutBuff, dwOutBuffSize);
                    resultado = VerificarEstado(ref struRecord, ref flag, dwStatus);

                }


                Marshal.FreeHGlobal(ptrOutBuff);
            }

            //limpiamos memoria
            Marshal.FreeHGlobal(ptrStruCond);

            return resultado;

        }



        private Hik_Resultado VerificarEstado(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, ref bool flag, int dwStatus)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            switch (dwStatus)
            {
                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_SUCCESS:

                    ProcesarInformacionFacial(ref struRecord, ref flag);
                    break;

                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NETX_STATUS_NEED_WAIT:
                    break;

                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_FAILED:
                    Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                    resultado.Exito = false;
                    resultado.MensajeDeError = "Fallo a la hora de obtener estado";
                    resultado.NumeroDeError = Hik_SDK.NET_DVR_GetLastError().ToString();
                    flag = false;
                    break;

                case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_FINISH:
                    resultado.Exito = true;
                    resultado.MensajeDeExito = "El proceso termino";
                    Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                    flag = false;
                    break;

                default:
                    resultado.Exito = false;
                    resultado.MensajeDeError = "No se conoce el estado del sistema";
                    resultado.NumeroDeError = Hik_SDK.NET_DVR_GetLastError().ToString();
                    flag = false;
                    Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                    break;

            }

            return resultado;

        }

        private void InicializarStruRecord(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, ref IntPtr ptrOutBuff, ref uint dwOutBuffSize)
        {
            struRecord.Init(); // inicializamos la estructura de datos struRecord

            struRecord.dwSize = (uint)Marshal.SizeOf(struRecord); //decimos el tamaño de la estructura de datos struRecord

            ptrOutBuff = Marshal.AllocHGlobal((int)struRecord.dwSize); //reservamos (alloc) memoria para la estructura de datos struRecord

            Marshal.StructureToPtr(struRecord, ptrOutBuff, false); //pasamos la estructura de datos struRecord a un puntero

            dwOutBuffSize = struRecord.dwSize; //le decimos el tamaño de la estructura de datos struRecord

        }

        private void InicializarStruCond(ref Hik_SDK.NET_DVR_FACE_COND struCond,ref int dwSize, uint cardReaderNumber, int cardNumber, ref IntPtr ptrStruCond)
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
        private void ProcesarInformacionFacial(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, ref Boolean Flag)
        {
            string strpath = null;
            DateTime dt = DateTime.Now;
            strpath = string.Format("FacePicture.jpg");

            //si la longitud de la cara es 0, no hace nada y volvemos para atras
            if (0 == struRecord.dwFaceLen)
            {
                return;
            }

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
                    int FaceLen = (int) struRecord.dwFaceLen;
                    byte[] by = new byte[FaceLen];
                    Marshal.Copy(struRecord.pFaceBuffer, by, 0, FaceLen);
                    fs.Write(by, 0, FaceLen);
                    fs.Close();
                }
                //y escribimos la imagen de la cara en pictureboxface
                pictureBoxFace.Image = Image.FromFile(strpath);
                textBoxFilePath.Text = string.Format("{0}\\{1}", Environment.CurrentDirectory, strpath);
            }
            catch
            {
                Flag = false;
                Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                MessageBox.Show("ProcessFaceData failed", "Error", MessageBoxButtons.OK);
            }
        }


        //set face
        //cap face
        //del face


    }
}
