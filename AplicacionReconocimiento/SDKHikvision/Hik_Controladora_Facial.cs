
using System.Runtime.InteropServices;
using System.Text;


namespace DeportNetReconocimiento.SDK
{
public class Hik_Controladora_Facial
{

    private static Hik_Controladora_Facial? instancia;



    //atributos facial

    //private bool soportaFacial


    private int getFaceCfgHandle;
    private int setFaceCfgHandle;
    private int capFaceCfgHandle;


    //constructores 

    private Hik_Controladora_Facial()
    {
        getFaceCfgHandle = -1;
        setFaceCfgHandle = -1;
        capFaceCfgHandle = -1;
    }


    //propiedades facial
    public static Hik_Controladora_Facial ObtenerInstancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new Hik_Controladora_Facial();
            }
            return instancia;
        }
    }

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

    //Obtener una cara desde el dispositivo
    public Hik_Resultado ObtenerCara(int cardReaderNumber, string cardNumber)
    {

        Hik_Resultado resultado = new Hik_Resultado();

        //validaciones

        //si ya esta inicializado el handle, lo detiene
        if (GetFaceCfgHandle != -1)
        {
            Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
            GetFaceCfgHandle = -1;
        }

        //Cargamos toda la estructura NET_DVR_FACE_COND
        Hik_SDK.NET_DVR_FACE_COND struCond = new Hik_SDK.NET_DVR_FACE_COND();
        int dwSize = 0;
        IntPtr ptrStruCond = IntPtr.Zero;

        InicializarFaceCond(ref struCond, ref dwSize, (uint)cardReaderNumber, cardNumber, ref ptrStruCond);

        GetFaceCfgHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.Instancia.IdUsuario, Hik_SDK.NET_DVR_GET_FACE, ptrStruCond, dwSize, null, IntPtr.Zero);


        // revisamos el valor del handler, si sale mal, libera la memoria y muestra un mensaje de error
        if (GetFaceCfgHandle == -1)
        {
            resultado.ActualizarResultado(false, "Error al obtener la cara", Hik_SDK.NET_DVR_GetLastError().ToString());

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

                resultado.ActualizarResultado(true, "Se obtuvo la cara de forma exitosa", Hik_SDK.NET_DVR_GetLastError().ToString());
                break;
            case (int)Hik_SDK.NET_SDK_GET_NEXT_STATUS_NEED_WAIT:

                //Esperamos, y si el resultado final es esperamos, signifca que no se tomo ningun dato
                resultado.ActualizarResultado(false, "El cliente no posiciono la cara frente al dispositivo", Hik_SDK.NET_DVR_GetLastError().ToString());
                
                break;
            case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FAILED:
                //Detenemos el proceso si hubo un fallo

                Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);
                flag = false;
                resultado.ActualizarResultado(false, "Error a la hora de obtener el estado", Hik_SDK.NET_DVR_GetLastError().ToString());

                break;
            case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FINISH:
                //Terminamos el proceso si se finalizo correctamente

                Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);

                flag = false;
                resultado.ActualizarResultado(true, "El proceso terminó con exito", Hik_SDK.NET_DVR_GetLastError().ToString());

                break;
            default:
                //Si no se conoce el estado del sistema, se detiene el proceso

                Hik_SDK.NET_DVR_StopRemoteConfig(GetFaceCfgHandle);

                flag = false;
                resultado.ActualizarResultado(false, "No se conoce el estado del sistema", Hik_SDK.NET_DVR_GetLastError().ToString());
                break;

        }

        return resultado;
    }
    //Recoge y almacena la foto encontrada en el dispositivo
    private void ProcesarInformacionFacialRecord(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, ref Boolean Flag)
    {
        string strpath = null;
        DateTime dt = DateTime.Now;
        strpath = Path.Combine(Directory.GetCurrentDirectory(), "FacePicture.jpg");

            //si la longitud de la cara es 0, no hace nada y volvemos para atras
        if (struRecord.dwFaceLen != 0)
        {

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
                    
            }
            catch
            {
                //Este catch se puede dar si la foto ya esta en el box de la foto 
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
    private void InicializarFaceCond(ref Hik_SDK.NET_DVR_FACE_COND struCond, ref int dwSize, uint cardReaderNumber, string cardNumber, ref IntPtr ptrStruCond)
    {
        struCond.Init();
        struCond.dwSize = Marshal.SizeOf(struCond);
        dwSize = (int)struCond.dwSize;

        //se elige el cardreader, si no se elige, se pone en 0
        //Se asigna el valor de dwEnableReaderNo

        struCond.dwEnableReaderNo = (int)cardReaderNumber;
        struCond.dwFaceNum = 1;

        try
        {
            //Se pasa byte por byte para evitar errores de desbordamiento
            byte[] byTemp = Encoding.UTF8.GetBytes(cardNumber);
            for (int i = 0; i < byTemp.Length; i++)
            {
                struCond.byCardNo[i] = byTemp[i];
            }

        }
        catch(Exception ex)
        {
                Console.WriteLine("Excepcion en Inicializar FaceCond", ex.Message);
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


        Hik_SDK.NET_DVR_CAPTURE_FACE_COND struCapCond = new Hik_SDK.NET_DVR_CAPTURE_FACE_COND();
        IntPtr ptrCapCond = IntPtr.Zero;
        int dwInBufferSize = 0;

        InicializarCaptureFaceCond(ref struCapCond, ref ptrCapCond, ref dwInBufferSize);

        CapFaceCfgHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.Instancia.IdUsuario, Hik_SDK.NET_DVR_CAPTURE_FACE_INFO, ptrCapCond, dwInBufferSize, null, IntPtr.Zero);


        //Si hubo un error
        if (CapFaceCfgHandle == -1)
        {
            Marshal.FreeHGlobal(ptrCapCond);

            resultado.ActualizarResultado(false, "Error al capturar datos faciales", Hik_SDK.NET_DVR_GetLastError().ToString());


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
                resultado.ActualizarResultado(true, "Se capturó la cara de forma exitosa", Hik_SDK.NET_DVR_GetLastError().ToString());

                capturaExitosa = true;
                    
                break;
            case Hik_SDK.NET_SDK_GET_NEXT_STATUS_NEED_WAIT: //1001

                break;
            case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FAILED: //1003  
                Hik_SDK.NET_DVR_StopRemoteConfig(CapFaceCfgHandle);

                flag = false;

                resultado.ActualizarResultado(false, "Error al capturar la cara", Hik_SDK.NET_DVR_GetLastError().ToString());

                break;
            case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FINISH: //1002

                Hik_SDK.NET_DVR_StopRemoteConfig(CapFaceCfgHandle);
                flag = false;

                if (!capturaExitosa)
                {
                    resultado.ActualizarResultado(true, "El proceso termino", Hik_SDK.NET_DVR_GetLastError().ToString());
                }
           
                    break;

            case Hik_SDK.NET_SDK_GET_NEXT_STATUS_TIMEOUT: //1004

                Hik_SDK.NET_DVR_StopRemoteConfig(CapFaceCfgHandle);
                flag = false;

                resultado.ActualizarResultado(false, "Se agotó el tiempo de espera", Hik_SDK.NET_DVR_GetLastError().ToString());

                break;
            default:

                Hik_SDK.NET_DVR_StopRemoteConfig(CapFaceCfgHandle);
                flag = false;
                resultado.ActualizarResultado(false, "Se Desconoce el error", Hik_SDK.NET_DVR_GetLastError().ToString());

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
        lpOutBuff = Marshal.AllocHGlobal((int)dwOutBuffSize);
        Marshal.StructureToPtr(struFaceCfg, lpOutBuff, false); //Convierto la estructura en puntero
    }
    private void InicializarCaptureFaceCond(ref Hik_SDK.NET_DVR_CAPTURE_FACE_COND strucCapCond, ref IntPtr ptrCapCond, ref int dwInBufferSize)
    {
        strucCapCond.Init(); //inicializamos 
        strucCapCond.dwSize = Marshal.SizeOf(strucCapCond); // asignamos el tam de strucCapCond
        dwInBufferSize = (int)strucCapCond.dwSize; //asignamos el tam de strucCapCond
        ptrCapCond = Marshal.AllocHGlobal(dwInBufferSize); //reservamos memoria para el puntero de strucCapCond
        Marshal.StructureToPtr(strucCapCond, ptrCapCond, false); //pasamos la estructura de datos strucCapCond a un puntero

    }



    //Establecer una cara en el dispositivo
    public Hik_Resultado EstablecerUnaCara(uint cardReaderNumber, string cardNumber)
    {
        Hik_Resultado resultado = new Hik_Resultado();

        Hik_SDK.NET_DVR_FACE_COND struCond = new Hik_SDK.NET_DVR_FACE_COND();
        int dwInBufferSize = 0;
        IntPtr ptrStruCond = IntPtr.Zero;

        InicializarFaceCond(ref struCond, ref dwInBufferSize, cardReaderNumber, cardNumber, ref ptrStruCond);

        SetFaceCfgHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.Instancia.IdUsuario, Hik_SDK.NET_DVR_SET_FACE, ptrStruCond, dwInBufferSize, null, IntPtr.Zero);

        if (SetFaceCfgHandle == -1)
        {

            resultado.ActualizarResultado(false, "Error al establecer la cara", Hik_SDK.NET_DVR_GetLastError().ToString());
        }
        else
        {

            Hik_SDK.NET_DVR_FACE_RECORD struRecord = new Hik_SDK.NET_DVR_FACE_RECORD();

            InicializarFaceRecordSet(ref struRecord, cardNumber);

            resultado = BuscarFotoParaIngresar(ref struRecord, Path.Combine(Directory.GetCurrentDirectory(), "captura.jpg"));  
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
                    resultado = VerificarEstadoEstablecerCara(ref struStatus, dwStatus, ref flag);
                }
            }
        }

        Marshal.FreeHGlobal(ptrStruCond);

        return resultado;
    }
    private Hik_Resultado VerificarEstadoEstablecerCara(ref Hik_SDK.NET_DVR_FACE_STATUS struStatus, int dwStatus, ref bool flag)
    {
        Hik_Resultado resultado = new Hik_Resultado();
        bool caraEstabelcida = false;

        switch (dwStatus)
        {
            case Hik_SDK.NET_SDK_GET_NEXT_STATUS_SUCCESS: //1000
                resultado = ProcesarEstablecerCara(ref struStatus, ref flag);
                break;

            case Hik_SDK.NET_SDK_GET_NEXT_STATUS_NEED_WAIT: //1001

                resultado.ActualizarResultado(false, "Se necesita esperar", Hik_SDK.NET_DVR_GetLastError().ToString());


                break;

            case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FAILED: //1003

                Hik_SDK.NET_DVR_StopRemoteConfig(SetFaceCfgHandle);
                flag = false;

                resultado.ActualizarResultado(false, "Error al establecer la cara", Hik_SDK.NET_DVR_GetLastError().ToString());

                break;

            case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FINISH: //1002

                Hik_SDK.NET_DVR_StopRemoteConfig(SetFaceCfgHandle);
                flag = false;
                if (!caraEstabelcida)
                {
                    resultado.ActualizarResultado(true, "El proceso termino", Hik_SDK.NET_DVR_GetLastError().ToString());
                }
                break;

            default:
                Hik_SDK.NET_DVR_StopRemoteConfig(SetFaceCfgHandle);
                flag = false;

                resultado.ActualizarResultado(false, "Error desconocido", Hik_SDK.NET_DVR_GetLastError().ToString());

                break;
        }
        return resultado;
    }
    private Hik_Resultado ProcesarEstablecerCara(ref Hik_SDK.NET_DVR_FACE_STATUS struStatus, ref bool flag)
    {
        Hik_Resultado resultado = new Hik_Resultado();


        if (struStatus.byRecvStatus == 1)
        {
            resultado.ActualizarResultado(true, "Se estableció la información facial de forma exitosa", Hik_SDK.NET_DVR_GetLastError().ToString());
        }
        else
        {
            flag = false;
            resultado.ActualizarResultado(false, "Hubo un error al establecer la información facial ", struStatus.byRecvStatus.ToString());
        }

        return resultado;
    }
    //Evalua si la foto seleccionada cumple con los requisitos
    private Hik_Resultado BuscarFotoParaIngresar(ref Hik_SDK.NET_DVR_FACE_RECORD struRecord, String ubicacionArchivo)
    {
        Hik_Resultado resultado = new Hik_Resultado();

        if (!File.Exists(ubicacionArchivo))
        {
            //la foto no existe
            resultado.ActualizarResultado(false, "La foto de la cara no existe", Hik_SDK.NET_DVR_GetLastError().ToString());
            return resultado;
        }
        

        FileStream fileStr = new FileStream(ubicacionArchivo, FileMode.OpenOrCreate);

        if (fileStr.Length == 0) //la foto es 0k
        {
            resultado.ActualizarResultado(false, "La foto de la cara es de 0k, por favor ingrese otra foto", Hik_SDK.NET_DVR_GetLastError().ToString());
            return resultado;
        }

        if (200 * 1024 < fileStr.Length)//la foto es 200k
        {
            resultado.ActualizarResultado(false, "La foto de la cara es mayor a 200k, por favor ingrese otra foto", Hik_SDK.NET_DVR_GetLastError().ToString());
            return resultado;
        }
       
        resultado = ProcesarFotoEncontrada(ref struRecord, fileStr); 
        
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
            resultado.ActualizarResultado(true, "Se leyó la cara de la foto de forma exitosa", Hik_SDK.NET_DVR_GetLastError().ToString());

        }
        catch
        {
            fileStr.Close();
            resultado.ActualizarResultado(false, "Fallo al leer la información facial, intentelo de nuevo", Hik_SDK.NET_DVR_GetLastError().ToString());

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
   

        Hik_SDK.NET_DVR_FACE_PARAM_CTRL_CARDNO struCardNo = new Hik_SDK.NET_DVR_FACE_PARAM_CTRL_CARDNO();

        int dwSize = 0;
        IntPtr lpInBuffer = IntPtr.Zero;
        InicializarParamControlCardNo(ref struCardNo, ref dwSize, cardNumber, ref lpInBuffer, cardReaderNumber);

        if ( false == Hik_SDK.NET_DVR_RemoteControl(Hik_Controladora_General.Instancia.IdUsuario, Hik_SDK.NET_DVR_DEL_FACE_PARAM_CFG, ref  struCardNo, (int)dwSize))
        {
            resultado.ActualizarResultado(false, "Error al eliminar la estructura", Hik_SDK.NET_DVR_GetLastError().ToString());
        }
        else
        {
            resultado.ActualizarResultado(true, "Se eliminó la cara del id: " + cardNumber + " de manera correcta", Hik_SDK.NET_DVR_GetLastError().ToString());

        }

        return resultado;
    }

    private void InicializarParamControlCardNo(ref Hik_SDK.NET_DVR_FACE_PARAM_CTRL_CARDNO struCardNo,ref int dwSize, string cardNumber, ref IntPtr lpInBuffer, int cardReaderNumber)
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
