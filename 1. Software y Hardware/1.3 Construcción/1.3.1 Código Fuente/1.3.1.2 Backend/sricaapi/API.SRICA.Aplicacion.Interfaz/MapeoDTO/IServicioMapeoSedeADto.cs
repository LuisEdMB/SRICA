using API.SRICA.Aplicacion.DTO;
using API.SRICA.Dominio.Entidad.SE;

namespace API.SRICA.Aplicacion.Interfaz.MapeoDTO
{
    /// <summary>
    /// Interfaz para el servicio de mapeo de la sede a un DTO
    /// </summary>
    public interface IServicioMapeoSedeADto
    {
        /// <summary>
        /// Método que mapea la entidad sede a un DTO
        /// </summary>
        /// <param name="sede">Sede a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        DtoSede MapearADTO(Sede sede);
    }
}
