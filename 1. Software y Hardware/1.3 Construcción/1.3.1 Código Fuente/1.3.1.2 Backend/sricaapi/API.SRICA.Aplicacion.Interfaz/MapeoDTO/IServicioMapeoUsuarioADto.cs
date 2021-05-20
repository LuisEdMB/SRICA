using API.SRICA.Aplicacion.DTO;
using API.SRICA.Dominio.Entidad.BT;
using API.SRICA.Dominio.Entidad.US;
using System.Collections.Generic;

namespace API.SRICA.Aplicacion.Interfaz.MapeoDTO
{
    /// <summary>
    /// Interfaz para el servicio de mapeo del usuario a un DTO
    /// </summary>
    public interface IServicioMapeoUsuarioADto
    {
        /// <summary>
        /// Método que mapea la entidad usuario a un DTO
        /// </summary>
        /// <param name="usuario">Usuario a mapear</param>
        /// <param name="accesos">Accesos del usuario en el sistema (opcional)</param>
        /// <returns>DTO con los datos mapeados</returns>
        DtoUsuario MapearADTO(Usuario usuario, List<BitacoraAccionSistema> accesos = null);
    }
}
