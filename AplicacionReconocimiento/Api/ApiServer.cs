using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.Api.Services;


namespace DeportNetReconocimiento.Api
{
    public class ApiServer
    {
        private IHost host;

        public void Start()
        {
            // Crear y configurar el servidor HTTP
            host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddControllers(); // Agregar soporte para controladores


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
    }
}
