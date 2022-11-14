using API.SRICA.Aplicacion.DTO;
using API.SRICA.Dominio.Entidad.EB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz para el servicio de tratamientos de direcciones de red
    /// </summary>
    public interface IServicioDireccionRed
    {
        /// <summary>
        /// Método que obtiene el listado de IP's según una subred indicada, o un nombre de dominio
        /// indicado
        /// </summary>
        /// <param name="subred">Subred (obligatorio si se desea obtener las IP's mediante una 
        /// subred)</param>
        /// <param name="mascaraSubred">Máscara de subred (obligatorio cuando se declara la subred)</param>
        /// <param name="nombreDominio">Nombre de dominio (obligatorio si se desea obtener las IP's
        /// mediante un nombre de dominio)</param>
        /// <returns>Listado de IP's encontradas</returns>
        List<string> ObtenerListadoDeHosts(string subred, string mascaraSubred, string nombreDominio);
        /// <summary>
        /// Método asíncrono que realiza el proceso de verificación de IP's
        /// </summary>
        /// <param name="hosts">Listado de IP's a verificar (ping)</param>
        /// <returns>Listado de IP's válidos</returns>
        Task<List<DtoEquipoBiometrico>> PingAsync(List<string> hosts);
    }
}
