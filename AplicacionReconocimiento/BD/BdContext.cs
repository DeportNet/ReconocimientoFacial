using DeportNetReconocimiento.BD.Entidades;
using DeportNetReconocimiento.Modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.BD
{
    public class BdContext : DbContext
    {
        protected readonly IConfiguration configuration;

        public BdContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Socio>().ToTable("socios");
            modelBuilder.Entity<Articulo>().ToTable("articulos");
            modelBuilder.Entity<Membresia>().ToTable("membresias");
            modelBuilder.Entity<Acceso>().ToTable("accesos");
            modelBuilder.Entity<AccesoSocio>().ToTable("accesos_socios");
            modelBuilder.Entity<ConfiguracionGeneral>().ToTable("configuracion_general");
            modelBuilder.Entity<ConfiguracionDeAcceso>().ToTable("configuracion_de_acceso");
        }
    }
}
