using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.SRICA.Dominio.ServicioExterno.Interfaz
{
    /// <summary>
    /// Interfaz del microservicio de correo para el envío de alertas
    /// </summary>
    public interface IMicroservicioCorreo
    {
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
        void EnviarCorreo(string urlServicio, List<string> correosDestino, string asunto, 
            string cuerpo, string adjunto, bool esAccionPorEquipoBiometrico = false);
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
        Task EnviarCorreoAsync(string urlServicio, List<string> correosDestino, string asunto, 
            string cuerpo, string adjunto, bool esAccionPorEquipoBiometrico = false);
    }
}
