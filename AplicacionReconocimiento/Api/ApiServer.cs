using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.Api.Services;
using DeportNetReconocimiento.BD;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using static System.Windows.Forms.Design.AxImporter;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.Sqlite;


namespace DeportNetReconocimiento.Api
{
    public class ApiServer
    {
        private IHost host;
        public ApiServer()
        {
        }

        public void Start()
        {
            // Crear y configurar el servidor HTTP
            host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddControllers(); // Agregar soporte para controladores

                        services.AddDbContext<BdContext>(options =>
                        {
                            BdContext.InicializarBd();
                            string dbRuta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DeportnetReconocimiento", "dbDx.dt");
                            var connection = new SqliteConnection($"Data Source={dbRuta}");
                            connection.Open();

                            using (var command = connection.CreateCommand())
                            {
                                command.CommandText = "PRAGMA key = '!MiClaveSegura123!';"; // Configurar contraseña
                                command.ExecuteNonQuery();
                            }

                            connection.Close();

                            //Configurar SQLite
                            options.UseSqlite(connection);

                        });

                        services.AddScoped<IDeportnetReconocimientoService,ReconocimientoService>();

                        // Configurar CORS
                        services.AddCors(options =>
                        {
                            options.AddPolicy("AllowAll", builder =>
                            {
                                builder
                                    .AllowAnyOrigin()    // Permitir cualquier origen
                                    .AllowAnyMethod()    // Permitir cualquier método (GET, POST, etc.)
                                    .AllowAnyHeader();   // Permitir cualquier cabecera
                            });

                            //origenes especificos

                            //options.AddPolicy("AllowSpecificOrigins", builder =>
                            //{
                            //    builder.WithOrigins("http://localhost:5000", "https://mi-dominio.com")
                            //           .AllowAnyMethod()
                            //           .AllowAnyHeader();
                            //});
                        });

                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen(); // Agregar Swagger
                    });

                    webBuilder.Configure(app =>
                    {
                        app.UseSwagger();
                        app.UseSwaggerUI();

                        // Middleware global para manejar excepciones
                        app.UseMiddleware<GlobalExceptionHandler.GlobalExceptionHandler>();

                        app.UseCors("AllowAll");//AllowSpecificOrigins

                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers(); // Mapear controladores automáticamente
                        });

                    });
                    webBuilder.UseUrls("http://localhost:5000");
                })
                .Build();

            // Iniciar el servidor en un hilo separado
            _ = host.RunAsync();
            
            Console.WriteLine("Servidor API iniciado en http://localhost:5000");
        }

        public void Stop()
        {
            // Detener el servidor
            host?.StopAsync().Wait();
            Console.WriteLine("Servidor API detenido.");
        }


        public void ObtenerTablasDelDia()
        {
            //verificar si ya se hizo la peticion

            //si no se obtuvieron las tablas del dia hago la peticion

            //guardo las tablas en la base de datos

            //tambien verifico si quedo alguna tabla sin sincronizar
        }

        public void SincronizarTablas()
        {
            //verificar si tengo conexion

            //verificar si tengo tablas que sincronizar

            //hago la peticion

            //verifico si se sincronizaron las tablas enviadas haciendo otra peticion a dx

            //si salio todo bien, elimino las tablas locales
        }


    }
}
