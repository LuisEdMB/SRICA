using API.SRICA.Aplicacion.DTO;
using API.SRICA.Dominio.Entidad.PE;

namespace API.SRICA.Aplicacion.Interfaz.MapeoDTO
{
    /// <summary>
    /// Interfaz para el servicio de mapeo del personal de empresa X área a un DTO
    /// </summary>
    public interface IServicioMapeoPersonalEmpresaXAreaADto
    {
        /// <summary>
        /// Método que mapea la entidad personal de empresa X área a un DTO
        /// </summary>
        /// <param name="personalEmpresaXArea">Personal de empresa X área a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        DtoPersonalEmpresaXArea MapearADTO(PersonalEmpresaXArea personalEmpresaXArea);
    }
}
