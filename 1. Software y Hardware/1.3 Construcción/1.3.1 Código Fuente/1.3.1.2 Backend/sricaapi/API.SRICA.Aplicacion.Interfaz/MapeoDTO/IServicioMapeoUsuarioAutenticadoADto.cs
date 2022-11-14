using API.SRICA.Aplicacion.DTO;
using API.SRICA.Dominio.Entidad.US;

namespace API.SRICA.Aplicacion.Interfaz.MapeoDTO
{
    /// <summary>
    /// Interfaz para el servicio de mapeo del usuario autenticado a un DTO
    /// </summary>
    public interface IServicioMapeoUsuarioAutenticadoADto
    {
        /// <summary>
        /// Método que mapea la entidad usuario a un DTO
        /// </summary>
        /// <param name="usuario">Usuario autenticado a mapear</param>
        /// <param name="token">Token del usuario</param>
        /// <returns>DTO con los datos mapeados</returns>
        DtoUsuario MapearADTO(Usuario usuario, string token);
    }
}
