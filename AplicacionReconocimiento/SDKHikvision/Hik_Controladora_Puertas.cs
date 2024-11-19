using DeportNetReconocimiento.SDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace DeportNetReconocimiento.SDKHikvision
{
    internal class Hik_Controladora_Puertas
    {
        //atributos
        

        public Hik_Controladora_Puertas()
        {
            
        }

        //0-close, 1-open, 2-stay open, 3-stay close

        public static Hik_Resultado OperadorPuerta(int operacion)
        {
            Hik_Resultado resultado = new Hik_Resultado();
            int idUsuario = Hik_Controladora_General.InstanciaControladoraGeneral.IdUsuario;
            if(idUsuario == -1)
            {
                resultado.Exito = false;
                resultado.Mensaje = "No se ha logueado el usuario.";
                return resultado;
            }

            switch (operacion) {

                case 0:
                    //0-close
                    if (Hik_SDK.NET_DVR_ControlGateway(idUsuario, 1, 0))
                    {
                        resultado.Exito = true;
                        resultado.Mensaje = "Puerta cerrada con exito";
                    }
                    else
                    {
                        resultado.Exito = false;
                        resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                        resultado.Mensaje = "Error al cerrar la puerta";
                    }
                break;
                case 1:
                    //1 - open
                    if(Hik_SDK.NET_DVR_ControlGateway(idUsuario, 1, 1))
                    {
                        resultado.Exito = true;
                        resultado.Mensaje = "Puerta abierta con exito";
                    }
                    else
                    {
                        resultado.Exito = false;
                        resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                        resultado.Mensaje = "Error al abrir la puerta";
                    }
                break;
                case 2:
                    //2 - stay open

                    if(Hik_SDK.NET_DVR_ControlGateway(idUsuario, 1, 2))
                    {
                        resultado.Exito = true;
                        resultado.Mensaje = "Puerta abierta y se mantiene abierta";
                    }
                    else
                    {
                        resultado.Exito = false;
                        resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                        resultado.Mensaje = "Error al mantener la puerta abierta";
                    }

                break;
                case 3:
                    //3 - stay close

                    if (Hik_SDK.NET_DVR_ControlGateway(idUsuario, 1, 3))
                    {
                        resultado.Exito = true;
                        resultado.Mensaje = "Puerta cerrada y se mantiene cerrada";
                    }
                    else
                    {
                        resultado.Exito = false;
                        resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                        resultado.Mensaje = "Error al mantener la puerta cerrada";
                    }
                break;
                default:
                    resultado.Exito = false;
                    resultado.Mensaje = "Operacion no valida, operaciones del 0 al 3";
                break;


            }

            return resultado;
        }



    }
}
