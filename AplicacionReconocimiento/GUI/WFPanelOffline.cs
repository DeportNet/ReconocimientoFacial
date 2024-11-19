using DeportNetReconocimiento.Modelo;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.SDKHikvision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DeportNetReconocimiento.SDK.Hik_SDK;

namespace DeportNetReconocimiento.GUI
{

    
    public partial class WFPanelOffline : Form
    {
        private Hik_Controladora_Eventos controladora_Eventos;
        private Hik_Controladora_General controladora_General;
        private Hik_SDK.MSGCallBack m_falarmData = null;

        public WFPanelOffline()
        {
            InitializeComponent();

            //ConnectToRealTimeEvents();
        }

        //private void ConnectToRealTimeEvents()
        //{
        //    System.Console.WriteLine("Entro aca por lo menos");
        //    //Hik_Controladora_General.Login("admin", "Facundo2024*", "8000", "192.168.0.207");
        //    controladora_Eventos.SetupAlarm();

        //    m_falarmData = new Hik_SDK.MSGCallBack(MsgCallback);

        //    if (!Hik_SDK.NET_DVR_SetDVRMessageCallBack_V50(0, m_falarmData, IntPtr.Zero))
        //    {
        //        System.Console.WriteLine("Error al asociar callback");
        //    }
        //}


        //private void MsgCallback(int lCommand, ref Hik_SDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        //{

        //    Evento EventInfo = controladora_Eventos.ProcessAlarm(lCommand, ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
        //    if (EventInfo.Success)
        //        System.Console.WriteLine(EventInfo.Time.ToString() + " " + EventInfo.Minor_Type_Description + " Tarjeta: " + EventInfo.Card_Number + " Puerta: " + EventInfo.Door_Number);
        //    else
        //        System.Console.WriteLine(EventInfo.Exception);
        //}

        private void WFPanelOffline_Load(object sender, EventArgs e)
        {

        }

        //private void timerCheckStatus_Tick(object sender, EventArgs e)
        //{
        //    if (!controladora_Eventos.VerificarEstadoDispositivo())
        //    {
        //        System.Console.WriteLine("Error al conectar con la controladora intentar Reconectar");
        //        ConnectToRealTimeEvents();
        //    }
        //    else
        //    {
        //        System.Console.WriteLine("Conexión con la controladora OK");
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            //ConnectToRealTimeEvents();

        }
    }
}
