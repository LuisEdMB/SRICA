using System.Text.Json;
using API.SRICA.Dominio.ServicioExterno.Interfaz;

namespace API.SRICA.Infraestructura.ServicioExterno.Implementacion
{
    /// <summary>
    /// Implementación del microservicio de reconocimiento de imágenes de iris
    /// </summary>
    public class MicroservicioReconocimientoIris : BaseMicroservicio, IMicroservicioReconocimientoIris
    {
        /// <summary>
        /// Mensaje de error de sin conexión
        /// </summary>
        private const string MensajeErrorSinConexion = "No se ha podido conectar al servicio de " +
            "reconocimiento de imágenes de iris.";
        /// <summary>
        /// Mensaje de error
        /// </summary>
        private const string MensajeError = "El servicio de reconocimiento de imágenes de iris " +
            "ha fallado.";
        /// <summary>
        /// Método que reconoce la imagen de iris del personal
        /// </summary>
        /// <param name="urlServicio">URL del servicio de reconocimiento</param>
        /// <param name="imagenIris">Imagen codificado del iris del personal (formato base64)</param>
        /// <returns>Código del personal reconocido</returns>
        public string ReconocerIrisDePersonal(string urlServicio, string imagenIris)
        {
            var datos = JsonSerializer.Serialize(new
            {
                IrisCodificado = imagenIris
            });
            var peticion = new ModeloPeticionMicroservicio(urlServicio + "/reconocimientos-iris", "POST",
                datos);
            var resultadoPeticion = ProcesarMicroservicio(peticion, MensajeErrorSinConexion, 
                MensajeError, true);
            return resultadoPeticion.Datos?.ToString() ?? string.Empty;
        }
    }
}