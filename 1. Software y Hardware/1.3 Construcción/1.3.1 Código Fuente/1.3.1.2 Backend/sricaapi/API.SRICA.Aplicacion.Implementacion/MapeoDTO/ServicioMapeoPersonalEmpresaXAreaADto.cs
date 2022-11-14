using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.PE;

namespace API.SRICA.Aplicacion.Implementacion.MapeoDTO
{
    /// <summary>
    /// Implementación para el servicio de mapeo del personal de empresa X área a un DTO
    /// </summary>
    public class ServicioMapeoPersonalEmpresaXAreaADto : IServicioMapeoPersonalEmpresaXAreaADto
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        public ServicioMapeoPersonalEmpresaXAreaADto(IServicioEncriptador servicioEncriptador)
        {
            _servicioEncriptador = servicioEncriptador;
        }
        /// <summary>
        /// Método que mapea la entidad personal de empresa X área a un DTO
        /// </summary>
        /// <param name="personalEmpresaXArea">Personal de empresa X área a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        public DtoPersonalEmpresaXArea MapearADTO(PersonalEmpresaXArea personalEmpresaXArea)
        {
            return new DtoPersonalEmpresaXArea
            {
                CodigoPersonalEmpresaXArea = _servicioEncriptador.Encriptar(
                    personalEmpresaXArea.CodigoPersonalEmpresaXArea),
                CodigoPersonalEmpresa = _servicioEncriptador.Encriptar(
                    personalEmpresaXArea.CodigoPersonalEmpresa),
                CodigoArea = _servicioEncriptador.Encriptar(
                    personalEmpresaXArea.CodigoArea),
                CodigoSede = _servicioEncriptador.Encriptar(
                    personalEmpresaXArea.Area.CodigoSede),
                DescripcionArea = personalEmpresaXArea.Area.DescripcionArea,
                DescripcionSede = personalEmpresaXArea.Area.Sede.DescripcionSede,
                IndicadorEstado = personalEmpresaXArea.IndicadorEstado,
                Seleccionado = personalEmpresaXArea.IndicadorEstado,
                Nuevo = false
            };
        }
    }
}
