using API.SRICA.Dominio.ServicioExterno.Interfaz;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.SRICA.Infraestructura.ServicioExterno.Implementacion
{
    /// <summary>
    /// Implementación del microservicio de correo para el envío de alertas
    /// </summary>
    public class MicroservicioCorreo : BaseMicroservicio, IMicroservicioCorreo
    {
        /// <summary>
        /// Mensaje de error de sin conexión
        /// </summary>
        private const string MensajeErrorSinConexion = "No se ha podido conectar al servicio de correo. " +
            "Verifique la bitácora de acciones del sistema para obtener más detalles.";
        /// <summary>
        /// Mensaje de error
        /// </summary>
        private const string MensajeError = "El servicio de correo ha fallado. Verifique la bitácora de " +
            "acciones del sistema para obtener más detalles.";
        /// <summary>
        /// Método que envía el correo correspondiente
        /// </summary>
        /// <param name="urlServicio">URL del microservicio</param>
        /// <param name="correosDestino">Listado de correos destinos a enviar</param>
        /// <param name="asunto">Asunto del correo</param>
        /// <param name="cuerpo">Cuerpo del correo</param>
        /// <param name="adjunto">Adjunto del correo en base64</param>
        /// <param name="esAccionPorEquipoBiometrico">Si el proceso es generado por el
        /// equipo biométrico</param>
        public void EnviarCorreo(string urlServicio, List<string> correosDestino, string asunto, 
            string cuerpo, string adjunto, bool esAccionPorEquipoBiometrico = false)
        {
            var datos = JsonSerializer.Serialize(new { 
                CorreosDestino = correosDestino, 
                Asunto = asunto,
                Cuerpo = cuerpo,
                Adjunto = adjunto
            });
            var peticion = new ModeloPeticionMicroservicio(urlServicio + "/correos", "POST",
                datos);
            ProcesarMicroservicio(peticion, MensajeErrorSinConexion, MensajeError);
        }
        /// <summary>
        /// Método que envía el correo correspondiente
        /// </summary>
        /// <param name="urlServicio">URL del microservicio</param>
        /// <param name="correosDestino">Listado de correos destinos a enviar</param>
        /// <param name="asunto">Asunto del correo</param>
        /// <param name="cuerpo">Cuerpo del correo</param>
        /// <param name="adjunto">Adjunto del correo en base64</param>
        /// <param name="esAccionPorEquipoBiometrico">Si el proceso es generado por el
        /// equipo biométrico</param>
        public async Task EnviarCorreoAsync(string urlServicio, List<string> correosDestino, string asunto,
            string cuerpo, string adjunto, bool esAccionPorEquipoBiometrico = false)
        {
            var datos = JsonSerializer.Serialize(new { 
                CorreosDestino = correosDestino, 
                Asunto = asunto,
                Cuerpo = cuerpo,
                Adjunto = adjunto
            });
            var peticion = new ModeloPeticionMicroservicio(urlServicio + "/correos", "POST",
                datos);
            Task.Run(() => ProcesarMicroservicio(peticion, MensajeErrorSinConexion, MensajeError));
        }
    }
}
