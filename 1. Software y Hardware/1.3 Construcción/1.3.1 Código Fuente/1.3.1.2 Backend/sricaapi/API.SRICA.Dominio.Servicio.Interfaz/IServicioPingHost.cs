using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.SRICA.Dominio.Servicio.Interfaz
{
    /// <summary>
    /// Interfaz para el servicio de ping
    /// </summary>
    public interface IServicioPingHost
    {
        /// <summary>
        /// Método que realiza el ping a una dirección de red específica
        /// </summary>
        /// <param name="host">Dirección de red</param>
        /// <param name="esNombreEquipo">Si el host es un nombre de equipo</param>
        /// <returns>Diccionario con los valores: nombre de equipo, dirección de red,
        /// dirección MAC</returns>
        Task<Dictionary<string, string>> PingHost(string host, bool esNombreEquipo = false);
    }
}