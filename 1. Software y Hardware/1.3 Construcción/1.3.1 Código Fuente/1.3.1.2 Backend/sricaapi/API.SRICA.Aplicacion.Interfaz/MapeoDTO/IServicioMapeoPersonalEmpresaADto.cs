using API.SRICA.Aplicacion.DTO;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.PE;

namespace API.SRICA.Aplicacion.Interfaz.MapeoDTO
{
    /// <summary>
    /// Interfaz para el servicio de mapeo del personal de la empresa a un DTO
    /// </summary>
    public interface IServicioMapeoPersonalEmpresaADto
    {
        /// <summary>
        /// Método que mapea la entidad personal de la empresa a un DTO
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        DtoPersonalEmpresa MapearADTO(PersonalEmpresa personalEmpresa);
        /// <summary>
        /// Método que mapea, a DTO, la entidad personal empresa y equipo biométrico,
        /// provenientes del proceso de reconocimiento de personal
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa reconocido</param>
        /// <param name="equipoBiometrico">Equipo biométrico de donde se realiza el proceso
        /// de reconocimiento</param>
        /// <returns>Datos del reconocimiento realizado al personal</returns>
        DtoPersonalEmpresaReconocimiento MapearADTO(PersonalEmpresa personalEmpresa,
            EquipoBiometrico equipoBiometrico);
    }
}
