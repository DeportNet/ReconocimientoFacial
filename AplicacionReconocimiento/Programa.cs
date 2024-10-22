using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.SDK;
using System.Diagnostics;

namespace DeportNetReconocimiento
{
    internal class Programa
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

           
            //ApplicationConfiguration.Initialize();
            //Application.Run(new WFPrincipal());
            

            /*
            Hik_Controladora_General controladora = new Hik_Controladora_General();
            Hik_Resultado resultado1 = Hik_Controladora_General.InicializarNet_DVR();

            Hik_Resultado resultado2 = controladora.Login("Aasd", "loooll", "8008", "123.123.123");
            */

            
            Hik_Controladora_General controladora_General = new Hik_Controladora_General();

             Hik_Resultado resultado =   controladora_General.InicializarPrograma("admin", "Facundo2024*", "8000", "192.168.0.207");


           

        }
    }
}