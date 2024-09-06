using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.SDK
{
    public class Hik_Resultado
    {
        bool exito = false; 
        string mensajeDeExito = "";
        string mensajeDeError = "";
        string numeroDeError = "";


        // Propiedades (getters y setters)
        // Se usa lock para evitar problemas de concurrencia
        /* Ej de uso:
         Hik_Resultado resultado = new Hik_resultado(); 
         
            ...Logica dentro de un metodo...

            if (resultado.Exito)
            {
                resultado.MensajeDeExito= "Loggeo exitoso";
            }
            else
            {
                resultado.MensajeDeError= "Loggeo fallido";
            }

         */
        public bool Exito 
        {
            get { return exito; }
            set { lock (this) { exito = value; } }
        }

        public string MensajeDeExito
        {
            get { return mensajeDeExito; }
            set { lock (this) { mensajeDeExito = value; } }
        }

        public string MensajeDeError
        {
            get { return mensajeDeError; }
            set { lock (this) { mensajeDeError = value; } }
        }

        public string NumeroDeError
        {
            get { return numeroDeError; }
            set { lock (this) { numeroDeError = value; } }
        }
    }
}
