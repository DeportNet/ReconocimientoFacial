using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.SDKHikvision;
using DeportNetReconocimiento.Utils;
using System.Diagnostics;
using static DeportNetReconocimiento.SDK.Hik_SDK;

namespace DeportNetReconocimiento
{
    internal class Programa
    {

        [STAThread]
        static void Main(string[] args)
        {




            ApplicationConfiguration.Initialize();

            //Utilizamos la instancia de singleton para que solo se cree una vez la ventana principal
            Application.Run(WFPrincipal.ObtenerInstancia);

            //"admin", "Facundo2024*", "8000", "192.168.0.207"


        }



    }
}