using API.SRICA.Aplicacion.DTO;
using API.SRICA.Dominio.Entidad.EB;

namespace API.SRICA.Aplicacion.Interfaz.MapeoDTO
{
    /// <summary>
    /// Interfaz para el servicio de mapeo del equipo biométrico a un DTO
    /// </summary>
    public interface IServicioMapeoEquipoBiometicoADto
    {
        /// <summary>
        /// Método que mapea la entidad equipo biométrico a un DTO
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        DtoEquipoBiometrico MapearADTO(EquipoBiometrico equipoBiometrico);
        /// <summary>
        /// Método que mapea algunos datos (nomenclatura, dirección de red, nombre del equipo
        /// biométrico, dirección MAC) a un DTO
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura del equipo biométrico</param>
        /// <param name="direccionRed">Dirección de red del equipo biométrico</param>
        /// <param name="nombreEquipoBiometrico">Nombre del equipo biométrico</param>
        /// <param name="direccionMAC">Dirección MAC del equipo biométrico</param>
        /// <returns>DTO con los datos mapeados</returns>
        DtoEquipoBiometrico MapearADTO(string nomenclatura, string direccionRed,
            string nombreEquipoBiometrico, string direccionMAC);
    }
}
