using API.SRICA.Aplicacion.DTO;
using API.SRICA.Dominio.Entidad.EB;

namespace API.SRICA.Aplicacion.Interfaz.MapeoDTO
{
    /// <summary>
    /// Interfaz para el servicio de mapeo de la nomenclatura para equipos biométricos a un DTO
    /// </summary>
    public interface IServicioMapeoNomenclaturaEquipoBiometricoADto
    {
        /// <summary>
        /// Método que mapea la entidad nomenclatura para equipos biométricos a un DTO
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura para equipos biométricos a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        DtoNomenclaturaEquipoBiometrico MapearADTO(NomenclaturaEquipoBiometrico nomenclatura);
    }
}
