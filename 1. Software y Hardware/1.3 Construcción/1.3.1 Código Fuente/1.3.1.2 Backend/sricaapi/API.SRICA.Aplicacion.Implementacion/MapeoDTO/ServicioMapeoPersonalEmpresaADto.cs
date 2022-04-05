using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.PE;
using System.Collections.Generic;
using System.Linq;
using API.SRICA.Dominio.Entidad.EB;

namespace API.SRICA.Aplicacion.Implementacion.MapeoDTO
{
    /// <summary>
    /// Implementación para el servicio de mapeo del personal de la empresa a un DTO
    /// </summary>
    public class ServicioMapeoPersonalEmpresaADto : IServicioMapeoPersonalEmpresaADto
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Servicio de mapeo de personal de empresa X área a DTO
        /// </summary>
        private readonly IServicioMapeoPersonalEmpresaXAreaADto _servicioMapeoPersonalEmpresaXAreaADTO;
        /// <summary>
        /// Servicio de mapeo de equipo biométrico a DTO
        /// </summary>
        private readonly IServicioMapeoEquipoBiometicoADto _servicioMapeoEquipoBiometicoADto;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioMapeoPersonalEmpresaXAreaADTO">Servicio de mapeo de personal de empresa 
        /// X área a DTO</param>
        /// <param name="servicioMapeoPersonalEmpresaXAreaADTO">Servicio de mapeo de equipo
        /// biométrico a DTO</param>
        public ServicioMapeoPersonalEmpresaADto(IServicioEncriptador servicioEncriptador,
            IServicioMapeoPersonalEmpresaXAreaADto servicioMapeoPersonalEmpresaXAreaADTO,
            IServicioMapeoEquipoBiometicoADto servicioMapeoEquipoBiometicoADto)
        {
            _servicioEncriptador = servicioEncriptador;
            _servicioMapeoPersonalEmpresaXAreaADTO = servicioMapeoPersonalEmpresaXAreaADTO;
            _servicioMapeoEquipoBiometicoADto = servicioMapeoEquipoBiometicoADto;
        }
        /// <summary>
        /// Método que mapea la entidad personal de la empresa a un DTO
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        public DtoPersonalEmpresa MapearADTO(PersonalEmpresa personalEmpresa)
        {
            return new DtoPersonalEmpresa
            {
                CodigoPersonalEmpresa = _servicioEncriptador.Encriptar(
                    personalEmpresa?.CodigoPersonalEmpresa ?? 0),
                DNIPersonalEmpresa = personalEmpresa?.DNIPersonalEmpresa ?? "",
                NombrePersonalEmpresa = personalEmpresa?.NombrePersonalEmpresa ?? "",
                ApellidoPersonalEmpresa = personalEmpresa?.ApellidoPersonalEmpresa ?? "",
                DescripcionSedes = string.Join("; ", personalEmpresa?.AreasAsignadas?.Where(g => 
                    g.EsPersonalEmpresaXAreaActivo).Select(g => 
                    g.Area.Sede.DescripcionSede).Distinct().ToList() ?? new List<string>()),
                DescripcionAreas = string.Join("; ", personalEmpresa?.AreasAsignadas?.Where(g =>
                    g.EsPersonalEmpresaXAreaActivo).Select(g =>
                    g.Area.DescripcionArea).ToList() ?? new List<string>()),
                TieneIrisRegistrado = personalEmpresa?.ImagenIrisCodificado != null,
                IndicadorEstado = personalEmpresa?.IndicadorEstado ?? false,
                Areas = personalEmpresa?.AreasAsignadas?.Select(g => 
                    _servicioMapeoPersonalEmpresaXAreaADTO.MapearADTO(g)).ToList()
                    ?? new List<DtoPersonalEmpresaXArea>()
            };
        }
        /// <summary>
        /// Método que mapea, a DTO, la entidad personal empresa y equipo biométrico,
        /// provenientes del proceso de reconocimiento de personal
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa reconocido</param>
        /// <param name="equipoBiometrico">Equipo biométrico de donde se realiza el proceso
        /// de reconocimiento</param>
        /// <returns>Datos del reconocimiento realizado al personal</returns>
        public DtoPersonalEmpresaReconocimiento MapearADTO(PersonalEmpresa personalEmpresa,
            EquipoBiometrico equipoBiometrico)
        {
            return new DtoPersonalEmpresaReconocimiento
            {
                PersonalEmpresa = MapearADTO(personalEmpresa),
                EquipoBiometrico = equipoBiometrico != null ? _servicioMapeoEquipoBiometicoADto.MapearADTO(equipoBiometrico) : null
            };
        }
    }
}
