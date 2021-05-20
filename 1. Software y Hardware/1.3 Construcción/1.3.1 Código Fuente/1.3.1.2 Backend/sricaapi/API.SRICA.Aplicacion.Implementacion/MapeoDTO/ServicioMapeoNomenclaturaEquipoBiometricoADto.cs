using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.EB;

namespace API.SRICA.Aplicacion.Implementacion.MapeoDTO
{
    /// <summary>
    /// Implementación para el servicio de mapeo de la nomenclatura para equipos biométricos a un DTO
    /// </summary>
    public class ServicioMapeoNomenclaturaEquipoBiometricoADto : 
        IServicioMapeoNomenclaturaEquipoBiometricoADto
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        public ServicioMapeoNomenclaturaEquipoBiometricoADto(IServicioEncriptador servicioEncriptador)
        {
            _servicioEncriptador = servicioEncriptador;
        }
        /// <summary>
        /// Método que mapea la entidad nomenclatura para equipos biométricos a un DTO
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura para equipos biométricos a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        public DtoNomenclaturaEquipoBiometrico MapearADTO(NomenclaturaEquipoBiometrico nomenclatura)
        {
            return new DtoNomenclaturaEquipoBiometrico
            {
                CodigoNomenclatura = _servicioEncriptador.Encriptar(nomenclatura.CodigoNomenclatura),
                DescripcionNomenclatura = nomenclatura.DescripcionNomenclatura,
                IndicadorRegistroParaSinAsignacion = nomenclatura.IndicadorRegistroParaSinAsignacion,
                IndicadorEstado = nomenclatura.IndicadorEstado
            };
        }
    }
}
