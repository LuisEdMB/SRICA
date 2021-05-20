using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.EB;

namespace API.SRICA.Aplicacion.Implementacion.MapeoDTO
{
    /// <summary>
    /// Implementación para el servicio de mapeo del equipo biométrico a un DTO
    /// </summary>
    public class ServicioMapeoEquipoBiometicoADto : IServicioMapeoEquipoBiometicoADto
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        public ServicioMapeoEquipoBiometicoADto(IServicioEncriptador servicioEncriptador)
        {
            _servicioEncriptador = servicioEncriptador;
        }
        /// <summary>
        /// Método que mapea la entidad equipo biométrico a un DTO
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        public DtoEquipoBiometrico MapearADTO(EquipoBiometrico equipoBiometrico)
        {
            return new DtoEquipoBiometrico
            {
                CodigoEquipoBiometrico = _servicioEncriptador.Encriptar(
                    equipoBiometrico.CodigoEquipoBiometrico),
                CodigoNomenclatura = _servicioEncriptador.Encriptar(equipoBiometrico.CodigoNomenclatura),
                IndicadorRegistroNomenclaturaParaSinAsignacion =
                    equipoBiometrico.Nomenclatura.IndicadorRegistroParaSinAsignacion,
                DescripcionNomenclatura = equipoBiometrico.Nomenclatura.DescripcionNomenclatura,
                NombreEquipoBiometrico = equipoBiometrico.NombreEquipoBiometrico,
                DireccionRedEquipoBiometrico = equipoBiometrico.DireccionRedEquipoBiometrico,
                CodigoArea = _servicioEncriptador.Encriptar(equipoBiometrico.CodigoArea),
                IndicadorRegistroAreaParaSinAsignacion = 
                    equipoBiometrico.Area.IndicadorRegistroParaSinAsignacion,
                DescripcionArea = equipoBiometrico.Area.DescripcionArea,
                CodigoSede = _servicioEncriptador.Encriptar(equipoBiometrico.Area.Sede.CodigoSede),
                IndicadorRegistroSedeParaSinAsignacion = 
                    equipoBiometrico.Area.Sede.IndicadorRegistroParaSinAsignacion,
                DescripcionSede = equipoBiometrico.Area.Sede.DescripcionSede,
                DireccionFisicaEquipoBiometrico = equipoBiometrico.DireccionFisicaEquipoBiometrico,
                IndicadorEstado = equipoBiometrico.IndicadorEstado
            };
        }
        /// <summary>
        /// Método que mapea algunos datos (nomenclatura, dirección de red, nombre del equipo
        /// biométrico, dirección MAC) a un DTO
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura del equipo biométrico</param>
        /// <param name="direccionRed">Dirección de red del equipo biométrico</param>
        /// <param name="nombreEquipoBiometrico">Nombre del equipo biométrico</param>
        /// <param name="direccionMAC">Dirección MAC del equipo biométrico</param>
        /// <returns>DTO con los datos mapeados</returns>
        public DtoEquipoBiometrico MapearADTO(string nomenclatura, string direccionRed,
            string nombreEquipoBiometrico, string direccionMAC)
        {
            return new DtoEquipoBiometrico
            {
                DescripcionNomenclatura = nomenclatura,
                DireccionRedEquipoBiometrico = direccionRed,
                NombreEquipoBiometrico = nombreEquipoBiometrico,
                DireccionFisicaEquipoBiometrico = direccionMAC
            };
        }
    }
}
