using DeportNetReconocimiento.Api.Dtos.Request;
using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.Api.GlobalExceptionHandler;
using DeportNetReconocimiento.Api.GlobalExceptionHandler.Exceptions;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.SDK;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Services
{
    public class DeportnetReconocimientoService : IDeportnetReconocimientoService
    {
        private Hik_Controladora_General hik_Controladora;
        private bool enUso;

        public bool EnUso { get => enUso; set => enUso = value; }

        public DeportnetReconocimientoService()
        {
            enUso = false;
            hik_Controladora = Hik_Controladora_General.InstanciaControladoraGeneral;
        }

        public DetallesResponse AltaFacialCliente(AltaFacialClienteRequest clienteRequest) 
        {

            if (hik_Controladora.IdUsuario == -1)
            {
                throw new HikvisionException("El idUsuario del dispositivo de reconocimiento facial es -1. El dispositivo no esta conectado.");
            }

            if (enUso)
            {
                throw new DispositivoEnUsoException("El dispositivo ya está en uso.");
            }

            //todo,FALTA, verificacion idSucursal que recibimos con el idSucursal del dispositivo

            enUso = true;
            Hik_Resultado resultadoAlta = hik_Controladora.AltaCliente(clienteRequest.IdCliente.ToString(), clienteRequest.NombreCliente);
            //resultadoAlta.ActualizarResultado(true, "Se ha dado de alta el cliente facial con id: " + clienteRequest.IdCliente + " y nombre: " + clienteRequest.NombreCliente,"200");
            enUso = false;

            if (!resultadoAlta.Exito) 
            {
                throw new HikvisionException(resultadoAlta.Mensaje);
            }
            Console.WriteLine("Se ha dado de alta el cliente facial con id: " + clienteRequest.IdCliente + " y nombre: " + clienteRequest.NombreCliente);
            return new DetallesResponse("T",200, resultadoAlta.Mensaje);

        }

        public DetallesResponse BajaFacialCliente(BajaFacialClienteRequest clienteRequest)
        {
            throw new NotImplementedException();
        }
    }
}
