using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.SDK
{
    public class Hik_Resultado
    {
        private bool exito = false;
        private string mensaje = "";
        private string codigo = "";


        // Propiedades (getters y setters)
        // Se usa lock para evitar problemas de concurrencia
        /* 
         * Ej de uso:
         Hik_Resultado resultado = new Hik_resultado(); 
         
            ...Logica dentro de un metodo...

            if (resultado.Exito)
            {
                resultado.Mensaje= "Loggeo exitoso";
            }
            else
            {
                resultado.Mensaje= "Loggeo fallido";
            }
            
            return resultado;
         */

        public bool Exito 
        {
            get { return exito; }
            set { lock (this) { exito = value; } }
        }

        public string Mensaje
        {
            get { return mensaje; }
            set { lock (this) { mensaje= value; } }
        }

        public string Codigo
        {
            get { return codigo; }
            set { lock (this) { codigo = value; } }
        }

        public static bool EscribirLog()
        {
            
           return Hik_SDK.NET_DVR_SetLogToFile(3, "..\\LogsAplicacion", false);
        }
    }
}
