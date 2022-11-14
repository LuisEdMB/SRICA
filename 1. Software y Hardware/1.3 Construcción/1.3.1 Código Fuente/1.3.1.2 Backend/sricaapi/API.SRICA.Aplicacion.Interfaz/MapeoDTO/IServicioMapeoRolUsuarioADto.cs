using API.SRICA.Aplicacion.DTO;
using API.SRICA.Dominio.Entidad.US;

namespace API.SRICA.Aplicacion.Interfaz.MapeoDTO
{
    /// <summary>
    /// Interfaz para el servicio de mapeo del rol de usuario a un DTO
    /// </summary>
    public interface IServicioMapeoRolUsuarioADto
    {
        /// <summary>
        /// Método que mapea la entidad rol de usuario a un DTO
        /// </summary>
        /// <param name="rolUsuario">Rol de usuario a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        DtoRolUsuario MapearADTO(RolUsuario rolUsuario);
    }
}
