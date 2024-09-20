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
        private void ObtenerCara()
        {

            //validaciones

            //si ya esta inicializado el handle, lo detiene
            if (getFaceCfgHandle != -1)
            {
                Hik_SDK.NET_DVR_StopRemoteConfig(getFaceCfgHandle);
                getFaceCfgHandle = -1;
            }

            //si ya hay una imagen en el picturebox, la elimina
            if (pictureBoxFace.Image != null)
            {
                pictureBoxFace.Image.Dispose();
                pictureBoxFace.Image = null;
            }







            //reservamos el tamaño de la estructura de datos struCond 

            //aca
            Hik_SDK.NET_DVR_FACE_COND struCond = new Hik_SDK.NET_DVR_FACE_COND();



            struCond.Init();
            struCond.dwSize = (uint)Marshal.SizeOf(struCond);
            int dwSize = (int) struCond.dwSize;

            //se elige el cardreader, si no se elige, se pone en 0
            if (textBoxCardReaderNo.Text.ToString() == "")
            {
                struCond.dwEnableReaderNo = 0;
            }
            else
            {
                int.TryParse(textBoxCardReaderNo.Text.ToString(), out struCond.dwEnableReaderNo);
            }


            struCond.dwFaceNum = 1;//人脸数量是1
            //esto se tendria que pasar por parametro y realizar las respectivas validaciones
            //esto es el numero de tarjeta que me piden
            byte[] byTemp = System.Text.Encoding.UTF8.GetBytes(textBoxCardNo.Text); //pasar por paramentro el numero de tarjeta y parsear a bytes
            for (int i = 0; i < byTemp.Length; i++)
            {
                struCond.byCardNo[i] = byTemp[i];
            }

            IntPtr ptrStruCond = Marshal.AllocHGlobal(dwSize);
            Marshal.StructureToPtr(struCond, ptrStruCond, false);

            //hasta aca



            GetFaceCfgHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.IdUsuario, Hik_SDK.NET_DVR_GET_FACE, ptrStruCond, dwSize, null, IntPtr.Zero);

            


            // revisamos el valor del handler, si sale mal, libera la memoria y muestra un mensaje de error
            if (GetFaceCfgHandle == -1)
            {
                Marshal.FreeHGlobal(ptrStruCond);
                MessageBox.Show("NET_DVR_GET_FACE_FAIL, ERROR CODE" + Hik_SDK.NET_DVR_GetLastError().ToString(), "Error", MessageBoxButtons.OK);

                //ver si pasar un hik_resultado


                return;
            }



            //si sale bien, se inicializa la estructura de datos de la cara
            bool flag = true;
            int dwStatus = 0;

            Hik_SDK.NET_DVR_FACE_RECORD struRecord = new Hik_SDK.NET_DVR_FACE_RECORD(); //creamos una estructura de datos struRecord

            IntPtr ptrOutBuff = IntPtr.Zero; //puntero a la estructura de datos struRecord inicializado en 0

            uint dwOutBuffSize = 0; //tamaño de la estructura de datos struRecord inicializado en 0

            InicializarStruRecord(ref struRecord,ref ptrOutBuff,ref dwOutBuffSize);


            //ver si se puede modularizar

            while (flag)
            {
                //NET_DVR_GetNextRemoteConfig(int lHandle, IntPtr lpOutBuff, uint dwOutBuffSize);
                dwStatus = Hik_SDK.NET_DVR_GetNextRemoteConfig(GetFaceCfgHandle, ptrOutBuff, dwOutBuffSize);

                
                switch (dwStatus)
                {
                    case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_SUCCESS://成功读取到数据，处理完本次数据后需调用next

                        ProcessFaceData(ref struRecord, ref flag);

                        break;
                    case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NETX_STATUS_NEED_WAIT:

                        //waiteamos

                        break;
                    case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_FAILED:

                         
                        Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                        MessageBox.Show("NET_SDK_GET_NEXT_STATUS_FAILED" + Hik_SDK.NET_DVR_GetLastError().ToString(), "Error", MessageBoxButtons.OK);
                        flag = false;
                        
                        break;
                    case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS.NET_SDK_GET_NEXT_STATUS_FINISH:


                        MessageBox.Show("NET_SDK_GET_NEXT_STATUS_FINISH", "Tips", MessageBoxButtons.OK);
                        Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                        flag = false;


                        break;
                    default:


                        MessageBox.Show("NET_SDK_GET_STATUS_UNKOWN" + Hik_SDK.NET_DVR_GetLastError().ToString(), "Error", MessageBoxButtons.OK);
                        flag = false;
                        Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                        break;
                }
            }

            //limpiamos memoria
            Marshal.FreeHGlobal(ptrStruCond);
            Marshal.FreeHGlobal(ptrOutBuff);
        }



        private void InicializarStruRecord(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, ref IntPtr ptrOutBuff, ref uint dwOutBuffSize)
        {
            struRecord.Init(); // inicializamos la estructura de datos struRecord

            struRecord.dwSize = (uint)Marshal.SizeOf(struRecord); //decimos el tamaño de la estructura de datos struRecord

            ptrOutBuff = Marshal.AllocHGlobal((int)struRecord.dwSize); //reservamos (alloc) memoria para la estructura de datos struRecord

            Marshal.StructureToPtr(struRecord, ptrOutBuff, false); //pasamos la estructura de datos struRecord a un puntero

            dwOutBuffSize = struRecord.dwSize; //le decimos el tamaño de la estructura de datos struRecord

        }


        private void ProcessFaceData(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, ref Boolean Flag)
        {
            string strpath = null;
            DateTime dt = DateTime.Now;
            strpath = string.Format("FacePicture.jpg");

            //si la longitud de la cara es 0, no hace nada y volvemos para atras
            if (0 == struRecord.dwFaceLen)
            {
                return;
            }

            //limpiamos la imagen del picturebox
            if (pictureBoxFace.Image != null)
            {
                pictureBoxFace.Image.Dispose();
                pictureBoxFace.Image = null;
            }

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
                CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetFaceCfgHandle);
                MessageBox.Show("ProcessFaceData failed", "Error", MessageBoxButtons.OK);
            }
        }



        //set face
        //cap face
        //del face


    }
}
