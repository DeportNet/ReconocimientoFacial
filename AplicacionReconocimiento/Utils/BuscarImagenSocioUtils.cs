using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Utils
{
    public class BuscarImagenSocioUtils
    {
        public static string CambiarNombreFoto(string nombreCompletoSocio, string idSocio)
        {
            string aux = Regex.Replace(nombreCompletoSocio, "'", "");
            return Regex.Replace(aux, " ", "_") + "_" + idSocio + ".jpg";
        }
        public static string? BuscarImagenSocio(string nombreSocio, string idSocio)
        {
            if(string.IsNullOrEmpty(nombreSocio) || string.IsNullOrEmpty(idSocio))
            {
                Log.Warning("Nombre o IDSocio no pueden ser nulos o vacíos.");
                return null;
            }
         
            string nombreABuscar = CambiarNombreFoto(nombreSocio, idSocio);

            ConfiguracionEstilos configuracion = ConfiguracionEstilos.LeerJsonConfiguracion();


            if(string.IsNullOrEmpty(configuracion.RutaCarpeta) || !Directory.Exists(configuracion.RutaCarpeta))
            {
                Log.Warning("La ruta de la carpeta de imágenes no está configurada o no existe: " + configuracion.RutaCarpeta);
                return null;
            }

            string rutaImagen = Path.Combine(configuracion.RutaCarpeta, nombreABuscar);

            return ObtenerImagenBase64(rutaImagen);
        }


        public static string? ObtenerImagenBase64(string rutaImagen)
        {
            if (string.IsNullOrEmpty(rutaImagen) || !File.Exists(rutaImagen))
            {
                Log.Warning("La ruta de la imagen es nula o el archivo no existe: " + rutaImagen);
                return null;
            }

            try
            {
                byte[] imageBytes = File.ReadAllBytes(rutaImagen);
                return RedimensionarYComprimirImagen(rutaImagen, 100, 100);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al leer la imagen desde la ruta: " + rutaImagen);
                return null;
            }
        }

        private static string RedimensionarYComprimirImagen(string rutaOriginal, int anchoDeseado, int altoDeseado, long calidad = 75L)
        {
            using (var imagenOriginal = Image.FromFile(rutaOriginal))
            using (var imagenRedimensionada = new Bitmap(imagenOriginal, new Size(anchoDeseado, altoDeseado)))
            using (var ms = new MemoryStream())
            {
                var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, calidad); // calidad de 0 a 100

                imagenRedimensionada.Save(ms, encoder, encoderParams);
                byte[] imagenBytes = ms.ToArray();
                string dataImage = $"data:image/jpg;base64,{Convert.ToBase64String(imagenBytes)}";

                Log.Information(dataImage);

                return dataImage;
            }
        }


    }
}
