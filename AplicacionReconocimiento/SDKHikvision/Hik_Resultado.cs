

using Serilog;

namespace DeportNetReconocimiento.SDK
{
    public class Hik_Resultado
    {
        private bool exito = false;
        private string mensaje = "";
        private string codigo = "";

        public Hik_Resultado()
        {
            this.exito = false;
            this.mensaje = "";
            this.codigo = "";
        }

        public bool Exito
        {
            get { return exito; }
            set { lock (this) { exito = value; } }
        }

        public string Mensaje
        {
            get { return mensaje; }
            set { lock (this) { mensaje = value; } }
        }

        public string Codigo
        {
            get { return codigo; }
            set { lock (this) { codigo = value; } }
        }

        public  void EscribirResultado(string titulo)
        {
            Console.WriteLine("- - - - - - " + titulo + " - - - - - -");
            Console.WriteLine("Exito: " + Exito);
            Console.WriteLine("Mensaje: " + Mensaje);
            Console.WriteLine("Codigo: " + Codigo);
        }
        public void MessageBoxResultado(string titulo)
        {
            if(Exito == false)
            MessageBox.Show($"Exito: {Exito} \nMensaje: {Mensaje}\nCodigo: {Codigo}", titulo,MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            MessageBox.Show($"Exito: {Exito} \nMensaje: {Mensaje}\nCodigo: {Codigo}", titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ActualizarResultado(bool exito, string mensaje, string codigo)
        {
            this.exito = exito;
            this.mensaje = mensaje;
            this.codigo = codigo;
        }

        public static bool InicializarLogsHikvsion()
        {
            //Log level: 0-disable log (default), 1-output error log only, 2-output error and debug log, 3-output all logs (i.e., error, debug, and information).
            return Hik_SDK.NET_DVR_SetLogToFile(3, @"logsHikvision", false);
        }
    }
}
