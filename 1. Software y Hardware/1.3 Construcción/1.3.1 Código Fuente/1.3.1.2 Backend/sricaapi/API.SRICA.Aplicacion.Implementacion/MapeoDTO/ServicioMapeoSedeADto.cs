using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.SE;
using System.Linq;

namespace API.SRICA.Aplicacion.Implementacion.MapeoDTO
{
    /// <summary>
    /// Implementación para el servicio de mapeo de la sede a un DTO
    /// </summary>
    public class ServicioMapeoSedeADto : IServicioMapeoSedeADto
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Servicio de mapeo del área a DTO
        /// </summary>
        private readonly IServicioMapeoAreaADto _servicioMapeoAreaADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioMapeoAreaADTO">Servicio de mapeo del área a DTO</param>
        public ServicioMapeoSedeADto(IServicioEncriptador servicioEncriptador, 
            IServicioMapeoAreaADto servicioMapeoAreaADTO)
        {
            _servicioEncriptador = servicioEncriptador;
            _servicioMapeoAreaADTO = servicioMapeoAreaADTO;
        }
        /// <summary>
        /// Método que mapea la entidad sede a un DTO
        /// </summary>
        /// <param name="sede">Sede a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        public DtoSede MapearADTO(Sede sede)
        {
            return new DtoSede
            {
                CodigoSede = _servicioEncriptador.Encriptar(sede.CodigoSede),
                DescripcionSede = sede.DescripcionSede,
                IndicadorRegistroParaSinAsignacion = sede.IndicadorRegistroParaSinAsignacion,
                IndicadorEstado = sede.IndicadorEstado,
                Areas = sede.Areas.Select(g => _servicioMapeoAreaADTO.MapearADTO(g)).ToList()
            };
        }
    }
}
