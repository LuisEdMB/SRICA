using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.BT;
using System;

namespace API.SRICA.Aplicacion.Implementacion.MapeoDTO
{
    /// <summary>
    /// Implementación para el servicio de mapeo de la bitácora de acción de equipos 
    /// biométricos a un DTO
    /// </summary>
    public class ServicioMapeoBitacoraAccionEquipoBiometricoADto 
        : IServicioMapeoBitacoraAccionEquipoBiometricoADto
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        public ServicioMapeoBitacoraAccionEquipoBiometricoADto(IServicioEncriptador servicioEncriptador)
        {
            _servicioEncriptador = servicioEncriptador;
        }
        /// <summary>
        /// Método que mapea la entidad bitácora de acción de equipos biométricos a un DTO
        /// </summary>
        /// <param name="bitacoraAccionEquipoBiometrico">Bitácora de acción de equipos biométricos 
        /// a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        public DtoBitacoraAccionEquipoBiometrico MapearADTO(
            BitacoraAccionEquipoBiometrico bitacoraAccionEquipoBiometrico)
        {
            return new DtoBitacoraAccionEquipoBiometrico
            {
                CodigoBitacora = _servicioEncriptador.Encriptar(
                    bitacoraAccionEquipoBiometrico.CodigoBitacora),
                CodigoPersonalEmpresa = _servicioEncriptador.Encriptar(
                    bitacoraAccionEquipoBiometrico.CodigoPersonalEmpresa),
                DNIPersonalEmpresa = bitacoraAccionEquipoBiometrico.DNIPersonalEmpresa,
                NombrePersonalEmpresa = bitacoraAccionEquipoBiometrico.NombrePersonalEmpresa,
                ApellidoPersonalEmpresa = bitacoraAccionEquipoBiometrico.ApellidoPersonalEmpresa,
                CodigoSede = _servicioEncriptador.Encriptar(bitacoraAccionEquipoBiometrico.CodigoSede),
                DescripcionSede = bitacoraAccionEquipoBiometrico.DescripcionSede,
                CodigoArea = _servicioEncriptador.Encriptar(bitacoraAccionEquipoBiometrico.CodigoArea),
                DescripcionArea = bitacoraAccionEquipoBiometrico.DescripcionArea,
                NombreEquipoBiometrico = bitacoraAccionEquipoBiometrico.NombreEquipoBiometrico,
                CodigoResultadoAcceso = _servicioEncriptador.Encriptar(
                    bitacoraAccionEquipoBiometrico.CodigoResultadoAcceso),
                DescripcionResultadoAcceso = bitacoraAccionEquipoBiometrico.ResultadoAcceso
                    .DescripcionResultadoAcceso,
                DescripcionResultadoAccion = bitacoraAccionEquipoBiometrico.DescripcionResultadoAccion,
                FechaAcceso = bitacoraAccionEquipoBiometrico.FechaAcceso.ToString("dd/MM/yyyy HH:mm:ss"),
                ImagenPersonalNoRegistrado = 
                    bitacoraAccionEquipoBiometrico.ImagenPersonalNoRegistrado == null 
                        ? ""
                        : Convert.ToBase64String(bitacoraAccionEquipoBiometrico.ImagenPersonalNoRegistrado)
            };
        }
    }
}
