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

        public static InformacionSocioTabla SocioToInformacionTablaSocio(Socio socio)
        {
            return new InformacionSocioTabla
            {
                Id = socio.IdDx,
                NombreYApellido = socio.FirstName + " " + socio.LastName,
                NroTarjeta = socio.CardNumber,
                DNI = socio.IdNumber,
                Email = socio.Email,
                Edad = CalcularEdad(socio.BirthDate),
                Sexo = socio.Gender,
                Celular = socio.Cellphone,
                Estado = CalcularEstado(socio.IsActive),
                Direccion = socio.Address,
                Piso = socio.AddressFloor
            };
        }

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
            return int.Parse(estado) == 1 ? "Activo" : "Inactivo";
        }


    }
}
