using DeportnetOffline.Data.Dto.Table;
using DeportNetReconocimiento.Api.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportnetOffline.Data.Mapper
{
    public class TablaMapper
    {
        //Cobros

        //Venta a InformacionTablaCobro
        public static InformacionTablaCobro CobroToInformacionTablaCobro(Venta venta)
        {
            return new InformacionTablaCobro
            {
                Id = venta.Id,
                IdSocio = venta.Socio.Id,
                IsSaleItem = venta.IsSaleItem,
                FullNameSocio = venta.Socio.FirstName + " " + venta.Socio.LastName,
                ItemName = venta.Name,
                Amount = venta.Amount,
                SaleDate = venta.Date,
                Synchronized = venta.Synchronized,
                SyncronizedDate = venta.SyncronizedDate
            };
        }

        //Listado ventas a Listado InformacionTablaCobro
        public static List<InformacionTablaCobro> ListaCobroToListaInformacionTablaCobro(List<Venta> ventas)
        {

            List<InformacionTablaCobro> cobroTabla = [];

            foreach (Venta unaVenta in ventas)
            {
                cobroTabla.Add(CobroToInformacionTablaCobro(unaVenta));
            }

            return cobroTabla;
        }

        //Nuevos Socios

        //public static InformacionTablaNuevoLegajo NuevoLegajoToInformacionTablaNuevoLegajo(Socio nuevoSocio)
        //{
            
        //}

        //public static List<InformacionTablaNuevoLegajo> ListadoNuevosLegajosToListadoInformacionNuevoLegajos(List<Socio> nuevosSocios)
        //{

        //}



        //Socios

        //Socio a InformacionTablaSocio
        public static InformacionSocioTabla SocioToInformacionTablaSocio(Socio socio)
        {
            return new InformacionSocioTabla
            {
                Id = socio.IdDx,
                NombreYApellido = socio.FirstName + " " + socio.LastName,
                NroTarjeta = socio.CardNumber,
                Dni = socio.IdNumber,
                Email = socio.Email,
                Edad = CalcularEdad(socio.BirthDate),
                Sexo = socio.Gender,
                Celular = socio.Cellphone,
                Estado = CalcularEstado(socio.IsActive),
                Direccion = socio.Address,
                Piso = socio.AddressFloor,
                
            };
        }

        //Lista Socios a Informacion Socio
        public static List<InformacionSocioTabla> ListaSocioToListaInformacionTablaSocio(List<Socio> socios)
        {

            List<InformacionSocioTabla> socioTabla = []; 

            foreach(Socio socio in socios)
            {
                socioTabla.Add(SocioToInformacionTablaSocio(socio));
            }

            return socioTabla;
        }

        public static string CalcularEdad(DateTime fecha)
        {

            int anio = fecha.Year;
            int anioActual = DateTime.Now.Year;

            return (anioActual - anio).ToString();

        }

        public static string CalcularEstado(string estado)
        {
            if(estado != null)
            {
                return int.Parse(estado) == 1 ? "Activo" : "Inactivo";
            }
            return "Inactivo";
        }


    }
}
