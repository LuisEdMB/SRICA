using System.Text.Json;
using API.SRICA.Dominio.ServicioExterno.Interfaz;

namespace API.SRICA.Infraestructura.ServicioExterno.Implementacion
{
    /// <summary>
    /// Implementación del microservicio de segmentación de imágenes de iris
    /// </summary>
    public class MicroservicioSegmentacionIris : BaseMicroservicio, IMicroservicioSegmentacionIris
    {
        /// <summary>
        /// Mensaje de error de sin conexión
        /// </summary>
        private const string MensajeErrorSinConexion = "No se ha podido conectar al servicio de " +
            "segmentación de imágenes de iris. Verifique la bitácora de acciones del sistema para " +
            "obtener más detalles.";
        /// <summary>
        /// Mensaje de error
        /// </summary>
        private const string MensajeError = "El servicio de segmentación de imágenes de iris ha " +
            "fallado. Verifique la bitácora de acciones del sistema para obtener más detalles.";
        /// <summary>
        /// Método que segmenta la imagen de iris
        /// </summary>
        /// <param name="urlServicio">URL del microservicio</param>
        /// <param name="imagenOjoBase64">Imagen del ojo en formato base64</param>
        /// <param name="esAccionPorEquipoBiometrico">Si el proceso es generado por el
        /// equipo biométrico</param>
        /// <returns>Imagen del iris segmentado en base64</returns>
        public string SegmentarIrisEnImagen(string urlServicio, string imagenOjoBase64, 
            bool esAccionPorEquipoBiometrico = false)
        {
            var datos = JsonSerializer.Serialize(new
            {
                ImagenOjo = imagenOjoBase64
            });
            var peticion = new ModeloPeticionMicroservicio(urlServicio + "/segmentaciones-iris", "POST",
                datos);
            var resultadoPeticion = ProcesarMicroservicio(peticion, MensajeErrorSinConexion, 
                MensajeError);
            return resultadoPeticion.Datos.ToString();
        }
    }
}