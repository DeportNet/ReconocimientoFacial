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
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("ConexionDbLocalDeportnet"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Socio>().ToTable("branch_member");
            modelBuilder.Entity<Articulo>().ToTable("articulos");
            modelBuilder.Entity<Acceso>().ToTable("accesos");
            modelBuilder.Entity<Configuracion>().ToTable("configuracion");
        }
    }
}
