using API.SRICA.Aplicacion.DTO;
using API.SRICA.Dominio.Entidad.BT;

namespace API.SRICA.Aplicacion.Interfaz.MapeoDTO
{
    /// <summary>
    /// Interfaz para el servicio de mapeo de la bitácora de acción de equipos biométricos a un DTO
    /// </summary>
    public interface IServicioMapeoBitacoraAccionEquipoBiometricoADto
    {
        /// <summary>
        /// Método que mapea la entidad bitácora de acción de equipos biométricos a un DTO
        /// </summary>
        /// <param name="bitacoraAccionEquipoBiometrico">Bitácora de acción de equipos biométricos 
        /// a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        DtoBitacoraAccionEquipoBiometrico MapearADTO(
            BitacoraAccionEquipoBiometrico bitacoraAccionEquipoBiometrico);
    }
}
