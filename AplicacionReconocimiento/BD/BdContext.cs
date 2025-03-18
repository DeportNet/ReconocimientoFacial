using DeportNetReconocimiento.BD.Entidades;
using DeportNetReconocimiento.Modelo;
using Microsoft.AspNetCore.Components.Forms;
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
        public DbSet<Socio> Socios { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Membresia> Membresias { get; set; }
        public DbSet<Acceso> Accesos { get; set; }
        public DbSet<AccesoSocio> AccesosSocios { get; set; }
        public DbSet<ConfiguracionGeneral> ConfiguracionGeneral { get; set; }
        public DbSet<ConfiguracionDeAcceso> ConfiguracionDeAcceso { get; set; }


        public BdContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                CrearYOcultarCarpetaDb();

                string rutaDb = CrearYOcultarArchivoDb();

                //Configurar SQLite
                optionsBuilder.UseSqlite($"Data Source={rutaDb}");
            }

            Console.WriteLine("Bd creada y ocultada correctamente");
        }

        private void CrearYOcultarCarpetaDb()
        {
            // Definir la ubicación de la base de datos en ProgramData
            string rutaCarpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DeportnetReconocimiento");

            // Crear la carpeta si no existe
            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
            }

            // Ocultar la carpeta
            DirectoryInfo directoryInfo = new DirectoryInfo(rutaCarpeta);
            directoryInfo.Attributes |= FileAttributes.Hidden; // Es equivalente a: directoryInfo.Attributes = directoryInfo.Attributes | FileAttributes.Hidden;

        }

        private string CrearYOcultarArchivoDb()
        {
            string rutaDb = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DeportnetReconocimiento", "dbDx.data");

            // Crear el archivo de la base de datos si no existe
            if (!File.Exists(rutaDb))
            {
                File.Create(rutaDb).Close(); // Crea el archivo vacío y lo cierra
            }

            // Ocultar el archivo dbDx.data
            FileInfo fileInfo = new FileInfo(rutaDb);
            fileInfo.Attributes |= FileAttributes.Hidden;

            return rutaDb;
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
