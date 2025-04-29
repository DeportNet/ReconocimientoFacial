using DeportNetReconocimiento.Api.Data.Domain;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.BD
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
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Credenciales> Credenciales { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        public BdContext(DbContextOptions<BdContext> options) : base(options)
        {
        }
        public static BdContext CrearContexto()
        {

            //*
            //using (var command = connection.CreateCommand())
            //{
            //    command.CommandText = "PRAGMA key = '!MiClaveSegura123!';"; // Tu clave
            //    command.ExecuteNonQuery();
            //}
            //*


            var optionsBuilder = new DbContextOptionsBuilder<BdContext>();

            string rutaDb = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "DeportnetReconocimiento", "dbDx.sqlite");

            optionsBuilder.UseSqlite($"Data Source={rutaDb}");

            var context = new BdContext(optionsBuilder.Options);

            context.Database.EnsureCreated();

            return context;

        }

        public static void InicializarBd()
        {
            // Ruta de la carpeta y la base de datos
            string rutaCarpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DeportnetReconocimiento");
            string rutaDb = Path.Combine(rutaCarpeta, "dbDx.sqlite");

            // Si la carpeta y la base de datos ya existen, no hacer nada
            if (Directory.Exists(rutaCarpeta) && File.Exists(rutaDb))
            {
                return;
            }

            // Si la carpeta no existe, la crea
            CrearYOcultarCarpetaDb(rutaCarpeta);

            // Si la base de datos no existe, la crea
            CrearYOcultarArchivoDb(rutaDb);

            Console.WriteLine("Base de datos creada y oculta correctamente.");
        }

        private static void CrearYOcultarCarpetaDb(string rutaCarpeta)
        {


            // Crear la carpeta si no existe
            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);

                // Ocultar la carpeta
                DirectoryInfo directoryInfo = new DirectoryInfo(rutaCarpeta);
                directoryInfo.Attributes |= FileAttributes.Hidden; // Es equivalente a: directoryInfo.Attributes = directoryInfo.Attributes | FileAttributes.Hidden;
            }

        }

        private static void CrearYOcultarArchivoDb(string rutaDb)
        {

            // Crear el archivo de la base de datos si no existe
            if (!File.Exists(rutaDb))
            {
                File.Create(rutaDb).Close(); // Crea el archivo vacío y lo cierra

                // Ocultar el archivo dbDx.data
                FileInfo fileInfo = new FileInfo(rutaDb);
                fileInfo.Attributes |= FileAttributes.Hidden;
            }

        }

        public static bool BdInicializada()
        {
            // Ruta de la carpeta y la base de datos
            string rutaCarpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DeportnetReconocimiento");
            string rutaDb = Path.Combine(rutaCarpeta, "dbDx.sqlite");

            // Si la carpeta y la base de datos ya existen, no hacer nada
            if (Directory.Exists(rutaCarpeta) && File.Exists(rutaDb))
            {
                return true;
            }
            return false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Socio>().ToTable("socios");
            modelBuilder.Entity<Articulo>().ToTable("articulos");
            modelBuilder.Entity<Membresia>().ToTable("membresias");
            modelBuilder.Entity<Empleado>().ToTable("empleados");
            modelBuilder.Entity<Credenciales>().ToTable("credenciales");
            modelBuilder.Entity<ConfiguracionGeneral>().ToTable("configuracion_general");
            modelBuilder.Entity<ConfiguracionDeAcceso>().ToTable("configuracion_de_acceso");
            modelBuilder.Entity<Venta>().ToTable("ventas");

            // Relación 1:N (Acceso tiene muchos AccesoSocio)
            modelBuilder.Entity<Acceso>()
                .ToTable("accesos")
                .HasMany(unAcceso => unAcceso.MemberAccess) // Un Acceso tiene muchos AccesoSocio
                .WithOne(accsoc => accsoc.Acceso) // Un AccesoSocio pertenece a un Acceso
                .HasForeignKey(accsoc => accsoc.AccesoId) // Clave foránea en AccesoSocio
                .IsRequired(false); // Permitir que AccesoId sea NULL inicialmente
                
            modelBuilder.Entity<AccesoSocio>().ToTable("accesos_socios");
        }
    }
}
