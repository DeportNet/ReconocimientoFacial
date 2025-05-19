using DeportNetReconocimiento.Api.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportnetOffline.Data.Filtros
{
    public class FiltrosSocio
    {
        public static IQueryable<Socio> FiltrarPorNroTarjetaODni(string? nroTarjeta, IQueryable<Socio> query)
        {

            if (!string.IsNullOrEmpty(nroTarjeta))
            {
                query = query.Where(p =>
                p.CardNumber.Contains(nroTarjeta) ||
                p.IdNumber.ToLower().Contains(nroTarjeta));

            }

            return query;
        }

        public static IQueryable<Socio> FiltrarPorEmail(string? email, IQueryable<Socio> query)
        {

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(p => p.Email.Contains(email));
            }

            return query;
        }

        public static IQueryable<Socio> FiltrarPorNombreYApellido(string? apellidoNombre, IQueryable<Socio> query)
        {

            if (!string.IsNullOrEmpty(apellidoNombre))
            {
                var nombres = apellidoNombre.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var nombre in nombres)
                {

                    string nombreActual = nombre.ToLower();
                    query = query.Where(p =>
                        p.FirstName.ToLower().Contains(nombreActual) ||
                        p.LastName.ToLower().Contains(nombreActual));
                }
            }

            return query;
        }

        public static IQueryable<Socio> FiltrarPorIsActive(string estado, IQueryable<Socio> query)
        {
            switch (estado)
            {
                case "1":
                    query = query.Where(p => p.IsActive.Contains(estado));
                    break;
                case "0":
                    query = query.Where(p => p.IsActive == null);
                    break;
                default:
                    break;
            }
            return query;
        }

        public static IQueryable<Socio> FiltrarPorIsValid(string estado, IQueryable<Socio> query)
        {
            switch (estado)
            {
                case "1":
                    query = query.Where(p => p.IsValid.Contains("1"));
                    break;
                case "0":
                    query = query.Where(p => p.IsValid.Contains("0"));
                    break;
                default:
                    break;
            }
            return query;
        }


        public static IQueryable<Socio> FiltrarPorNuevosSocios(IQueryable<Socio> query)
        {
            return query.Where(p => p.IdDx == null);
        }


    }
}
