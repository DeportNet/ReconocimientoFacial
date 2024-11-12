using DeportNetReconocimiento.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.SDKHikvision
{
    internal class Hik_Controladora_Puertas
    {
        //atributos
        private int idUsuario = -1;


        public Hik_Controladora_Puertas()
        {
            //InitializeComponent(); UI para poder controlar las puertas de forma manual
            //if (Hik_SDK.NET_DVR_Init() == false)
            //{
            //    MessageBox.Show("NET_DVR_Init error!");
            //    return;
            //}
            //comboBoxLanguage.SelectedIndex = 0;
        }

        //0-close, 1-open, 2-stay open, 3-stay close
        //private void btnLogin_Click(object sender, EventArgs e)
        //{
        //    AddDevice dlg = new AddDevice();
        //    dlg.ShowDialog();
        //    dlg.Dispose();
        //}

        //private void btnOpen_Click(object sender, EventArgs e)
        //{
        //    if (CHCNetSDK.NET_DVR_ControlGateway(m_UserID, 1, 1))
        //    {
        //        MessageBox.Show("NET_DVR_ControlGateway: open door succeed");
        //    }
        //    else
        //    {
        //        MessageBox.Show("NET_DVR_ControlGateway: open door failed error:" + CHCNetSDK.NET_DVR_GetLastError());
        //    }
        //}

        //private void btnClose_Click(object sender, EventArgs e)
        //{
        //    if (CHCNetSDK.NET_DVR_ControlGateway(m_UserID, 1, 0))
        //    {
        //        MessageBox.Show("NET_DVR_ControlGateway: close door succeed");
        //    }
        //    else
        //    {
        //        MessageBox.Show("NET_DVR_ControlGateway: close door failed error:" + CHCNetSDK.NET_DVR_GetLastError());
        //    }
        //}

        //private void btnStayOpen_Click(object sender, EventArgs e)
        //{
        //    if (CHCNetSDK.NET_DVR_ControlGateway(m_UserID, 1, 3))
        //    {
        //        MessageBox.Show("NET_DVR_ControlGateway: stay close door succeed");
        //    }
        //    else
        //    {
        //        MessageBox.Show("NET_DVR_ControlGateway:  stay close door failed error:" + CHCNetSDK.NET_DVR_GetLastError());
        //    }
        //}

        //private void btnStayClose_Click(object sender, EventArgs e)
        //{
        //    if (CHCNetSDK.NET_DVR_ControlGateway(m_UserID, 1, 2))
        //    {
        //        MessageBox.Show("NET_DVR_ControlGateway: stay open door succeed");
        //    }
        //    else
        //    {
        //        MessageBox.Show("NET_DVR_ControlGateway:  stay open door failed error:" + CHCNetSDK.NET_DVR_GetLastError());
        //    }
        //}





    }
}
