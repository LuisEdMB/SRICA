using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.AR;
using System.Linq;

namespace API.SRICA.Aplicacion.Implementacion.MapeoDTO
{
    /// <summary>
    /// Implementación para el servicio de mapeo del área a un DTO
    /// </summary>
    public class ServicioMapeoAreaADto : IServicioMapeoAreaADto
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Servicio de mapeo del equipo biométrico a DTO
        /// </summary>
        private readonly IServicioMapeoEquipoBiometicoADto _servicioMapeoEquipoBiometicoADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioMapeoEquipoBiometicoADTO">Servicio de mapeo del equipo 
        /// biométrico a DTO</param>
        public ServicioMapeoAreaADto(IServicioEncriptador servicioEncriptador,
            IServicioMapeoEquipoBiometicoADto servicioMapeoEquipoBiometicoADTO)
        {
            _servicioEncriptador = servicioEncriptador;
            _servicioMapeoEquipoBiometicoADTO = servicioMapeoEquipoBiometicoADTO;
        }
        /// <summary>
        /// Método que mapea la entidad área a un DTO
        /// </summary>
        /// <param name="area">Área a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        public DtoArea MapearADTO(Area area)
        {
            return new DtoArea
            {
                CodigoArea = _servicioEncriptador.Encriptar(area.CodigoArea),
                CodigoSede = _servicioEncriptador.Encriptar(area.CodigoSede),
                DescripcionSede = area.Sede.DescripcionSede,
                IndicadorRegistroSedeParaSinAsignacion = area.Sede.IndicadorRegistroParaSinAsignacion,
                DescripcionArea = area.DescripcionArea,
                IndicadorRegistroParaSinAsignacion = area.IndicadorRegistroParaSinAsignacion,
                IndicadorEstado = area.IndicadorEstado,
                EquiposBiometricos = area.EquiposBiometricos.Select(g => 
                    _servicioMapeoEquipoBiometicoADTO.MapearADTO(g)).ToList(),
                Seleccionado = false,
                Nuevo = true
            };
        }
    }
}
