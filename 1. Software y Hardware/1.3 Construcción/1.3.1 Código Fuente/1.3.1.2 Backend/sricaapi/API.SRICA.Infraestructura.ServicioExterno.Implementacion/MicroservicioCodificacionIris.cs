using System.Text;
using System.Text.Json;
using API.SRICA.Dominio.ServicioExterno.Interfaz;

namespace API.SRICA.Infraestructura.ServicioExterno.Implementacion
{
    /// <summary>
    /// Implementación del microservicio de codificación de imágenes de iris
    /// </summary>
    public class MicroservicioCodificacionIris : BaseMicroservicio, IMicroservicioCodificacionIris
    {
        /// <summary>
        /// Mensaje de error de sin conexión
        /// </summary>
        private const string MensajeErrorSinConexion = "No se ha podido conectar al servicio de " +
            "codificación de imágenes de iris. Verifique la bitácora de acciones del sistema " +
            "para obtener más detalles.";
        /// <summary>
        /// Mensaje de error
        /// </summary>
        private const string MensajeError = "El servicio de codificación de imágenes de iris " +
            "ha fallado. Verifique la bitácora de acciones del sistema para obtener más detalles.";
        /// <summary>
        /// Mensaje de error de sin conexión
        /// </summary>
        private const string MensajeErrorSinConexionEquipo = "No se ha podido conectar al servicio de " +
            "codificación de imágenes de iris. Verifique la bitácora de acciones de equipos biométricos " +
            "para obtener más detalles.";
        /// <summary>
        /// Mensaje de error
        /// </summary>
        private const string MensajeErrorEquipo = "El servicio de codificación de imágenes de iris " +
            "ha fallado. Verifique la bitácora de acciones de equipos biométricos para obtener " +
            "más detalles.";
        /// <summary>
        /// Método que codifica la imagen de iris
        /// </summary>
        /// <param name="imagenIrisSegmentadoBase64">Imagen del iris segmentado
        /// en formato base64</param>
        /// <param name="esAccionPorEquipoBiometrico">Si el proceso es generado por el
        /// equipo biométrico</param>
        /// <returns>Imagen de iris codificado en arreglo de bytes</returns>
        public byte[] CodificarIrisEnImagen(string urlServicio, string imagenIrisSegmentadoBase64, 
            bool esAccionPorEquipoBiometrico = false)
        {
            var datos = JsonSerializer.Serialize(new
            {
                ImagenIris = imagenIrisSegmentadoBase64
            });
            var peticion = new ModeloPeticionMicroservicio(urlServicio + "/codificaciones-iris", "POST",
                datos);
            var resultadoPeticion = ProcesarMicroservicio(peticion, 
                esAccionPorEquipoBiometrico ? MensajeErrorSinConexionEquipo : MensajeErrorSinConexion, 
                esAccionPorEquipoBiometrico ? MensajeErrorEquipo : MensajeError, 
                esAccionPorEquipoBiometrico);
            return Encoding.UTF8.GetBytes(resultadoPeticion.Datos.ToString());
        }
    }
}