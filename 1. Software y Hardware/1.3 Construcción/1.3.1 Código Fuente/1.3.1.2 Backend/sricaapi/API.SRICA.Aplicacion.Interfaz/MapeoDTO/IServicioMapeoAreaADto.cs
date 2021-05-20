using API.SRICA.Aplicacion.DTO;
using API.SRICA.Dominio.Entidad.AR;

namespace API.SRICA.Aplicacion.Interfaz.MapeoDTO
{
    /// <summary>
    /// Interfaz para el servicio de mapeo del área a un DTO
    /// </summary>
    public interface IServicioMapeoAreaADto
    {
        /// <summary>
        /// Método que mapea la entidad área a un DTO
        /// </summary>
        /// <param name="area">Área a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        DtoArea MapearADTO(Area area);
    }
}
