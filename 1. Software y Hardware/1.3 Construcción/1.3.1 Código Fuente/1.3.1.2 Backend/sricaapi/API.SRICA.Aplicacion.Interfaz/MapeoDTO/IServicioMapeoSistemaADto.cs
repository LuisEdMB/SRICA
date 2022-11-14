using API.SRICA.Aplicacion.DTO;

namespace API.SRICA.Aplicacion.Interfaz.MapeoDTO
{
    /// <summary>
    /// Interfaz para el servicio de mapeo de datos del sistema a un DTO
    /// </summary>
    public interface IServicioMapeoSistemaADto
    {
        /// <summary>
        /// Método que mapea los datos del sistema a un DTO
        /// </summary>
        /// <param name="codigo">Código del registro</param>
        /// <param name="descripcion">Descripción del registro</param>
        /// <returns>DTO con los datos mapeados</returns>
        DtoSistema MapearADTO(int codigo, string descripcion);
    }
}
