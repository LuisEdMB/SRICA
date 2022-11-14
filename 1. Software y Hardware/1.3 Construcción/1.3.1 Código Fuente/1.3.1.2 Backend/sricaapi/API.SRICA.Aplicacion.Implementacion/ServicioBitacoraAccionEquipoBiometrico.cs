using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.BT;
using API.SRICA.Dominio.Interfaz;
using API.SRICA.Dominio.Servicio.Interfaz;
using System.Linq;
using System.Threading.Tasks;
using API.SRICA.Aplicacion.DTO;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.PE;
using API.SRICA.Dominio.Entidad.SI;
using API.SRICA.Dominio.Excepcion;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación del servicio de consultas y operaciones de bitácoras de acciones del sistema
    /// </summary>
    public class ServicioBitacoraAccionEquipoBiometrico : IServicioBitacoraAccionEquipoBiometrico
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Servicio para la desencriptación de datos
        /// </summary>
        private readonly IServicioDesencriptador _servicioDesencriptador;
        /// <summary>
        /// Repositorio de consultas a la base de datos
        /// </summary>
        private readonly IRepositorioConsulta _repositorioConsulta;
        /// <summary>
        /// Repositorio de operaciones a la base de datos
        /// </summary>
        private readonly IRepositorioOperacion _repositorioOperacion;
        /// <summary>
        /// Servicio de alertas del sistema
        /// </summary>
        private readonly IServicioAlerta _servicioAlerta;
        /// <summary>
        /// Servicio de dominio de la bitácora de acción de equipos biométricos
        /// </summary>
        private readonly IServicioDominioBitacoraAccionEquipoBiometrico 
            _servicioDominioBitacoraAccionEquipoBiometrico;
        /// <summary>
        /// Servicio de mapeo de la bitácora de acción de equipos biométricos a DTO
        /// </summary>
        private readonly IServicioMapeoBitacoraAccionEquipoBiometricoADto 
            _servicioMapeoBitacoraAccionEquipoBiometricoADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioDesencriptador">Servicio para la desencriptación de datos</param>
        /// <param name="repositorioConsulta">Repositorio de consultas a la base de datos</param>
        /// <param name="repositorioOperacion">Repositorio de operaciones a la base de datos</param>
        /// <param name="servicioAlerta">Servicio de alertas del sistema</param>
        /// <param name="servicioDominioBitacoraAccionEquipoBiometrico">Servicio de dominio de la 
        /// bitácora de acción de equipos biométricos</param>
        /// <param name="servicioMapeoBitacoraAccionEquipoBiometricoADTO">Servicio de mapeo de la 
        /// bitácora de acción de equipos biométricos a DTO</param>
        public ServicioBitacoraAccionEquipoBiometrico(IServicioEncriptador servicioEncriptador,
            IServicioDesencriptador servicioDesencriptador,
            IRepositorioConsulta repositorioConsulta,
            IRepositorioOperacion repositorioOperacion,
            IServicioAlerta servicioAlerta,
            IServicioDominioBitacoraAccionEquipoBiometrico servicioDominioBitacoraAccionEquipoBiometrico,
            IServicioMapeoBitacoraAccionEquipoBiometricoADto servicioMapeoBitacoraAccionEquipoBiometricoADTO)
        {
            _servicioEncriptador = servicioEncriptador;
            _servicioDesencriptador = servicioDesencriptador;
            _repositorioConsulta = repositorioConsulta;
            _repositorioOperacion = repositorioOperacion;
            _servicioAlerta = servicioAlerta;
            _servicioDominioBitacoraAccionEquipoBiometrico = servicioDominioBitacoraAccionEquipoBiometrico;
            _servicioMapeoBitacoraAccionEquipoBiometricoADTO = 
                servicioMapeoBitacoraAccionEquipoBiometricoADTO;
        }
        /// <summary>
        /// Método que obtiene el listado de bitácora de acciones de equipos biométricos
        /// </summary>
        /// <returns>Resultado encriptado con el listado de bitácora de acciones de equipos biométricos
        /// encontrados</returns>
        public string ObtenerListadoDeBitacoraDeAccionesDeEquiposBiometricos()
        {
            var bitacora =
                _repositorioConsulta.ObtenerPorExpresionLimite<BitacoraAccionEquipoBiometrico>()
                .ToList();
            var bitacoraDTO = bitacora.Select(g =>
                _servicioMapeoBitacoraAccionEquipoBiometricoADTO.MapearADTO(g)).ToList();
            return _servicioEncriptador.Encriptar(bitacoraDTO);
        }
        /// <summary>
        /// Método que guarda la bitácora de la acción de un equipo biométrico
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa quien realiza la acción
        /// mediante el equipo biométrico</param>
        /// <param name="equipoBiometrico">Equipo biométrico origen</param>
        /// <param name="accesoDenegadoConFoto">Si la bitácora es producida por un acceso denegado,
        /// donde el personal no está registrado en el sistema</param>
        /// <param name="accesoDenegadoSinFoto">Si la bitácora es producida por un acceso denegado,
        /// donde el personal está registrado en el sistema</param>
        /// <param name="mensajeAccion">Mensaje de la acción realizada</param>
        /// <param name="fotoPersonalNoRegistrado">Foto del personal no registrado en
        /// el sistema</param>
        public void GuardarBitacoraDeAccionDelEquipoBiometrico(object personalEmpresa, 
            object equipoBiometrico, bool accesoDenegadoConFoto, bool accesoDenegadoSinFoto,
            string mensajeAccion, string fotoPersonalNoRegistrado)
        {
            if (personalEmpresa is DtoPersonalEmpresaReconocimiento)
                GuardarBitacoraDeAccionDelEquipoBiometricoParaProcesoCorrecto(personalEmpresa)
                    .ConfigureAwait(false);
            else
                GuardarBitacoraDeAccionDelEquipoBiometricoParaExcepciones(
                    personalEmpresa, equipoBiometrico, accesoDenegadoConFoto, accesoDenegadoSinFoto,
                    mensajeAccion, fotoPersonalNoRegistrado).ConfigureAwait(false);
        }
        #region Métodos privados
        /// <summary>
        /// Método que guarda la bitácora de la acción de un equipo biométrico, con resultado correcto
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa quien realiza la acción
        /// mediante el equipo biométrico</param>
        private async Task GuardarBitacoraDeAccionDelEquipoBiometricoParaProcesoCorrecto(object personalEmpresa)
        {
            var datosReconocimiento = (DtoPersonalEmpresaReconocimiento) personalEmpresa;
            var resultadoAcceso = _repositorioConsulta.ObtenerPorCodigo<ResultadoAcceso>(
                ResultadoAcceso.Concedido);
            var codigoPersonal = _servicioDesencriptador.Desencriptar<int>(
                datosReconocimiento.PersonalEmpresa.CodigoPersonalEmpresa);
            var codigoEquipoBiometrico = _servicioDesencriptador.Desencriptar<int>(
                datosReconocimiento.EquipoBiometrico.CodigoEquipoBiometrico);
            var personal = _repositorioConsulta.ObtenerPorCodigo<PersonalEmpresa>(codigoPersonal);
            var equipoBiometrico = _repositorioConsulta.ObtenerPorCodigo<EquipoBiometrico>(
                codigoEquipoBiometrico);
            var bitacora = _servicioDominioBitacoraAccionEquipoBiometrico.CrearBitacora(personal,
                equipoBiometrico, resultadoAcceso, "Acceso concedido.", string.Empty);
            _repositorioOperacion.Agregar(bitacora);
            _repositorioOperacion.GuardarCambios();
        }
        /// <summary>
        /// Método que guarda la bitácora de la acción de un equipo biométrico, con excepciones
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa quien realiza la acción
        /// mediante el equipo biométrico</param>
        /// <param name="equipoBiometrico">Equipo biométrico origen</param>
        /// <param name="accesoDenegadoConFoto">Si la bitácora es producida por un acceso denegado,
        /// donde el personal no está registrado en el sistema</param>
        /// <param name="accesoDenegadoSinFoto">Si la bitácora es producida por un acceso denegado,
        /// donde el personal está registrado en el sistema</param>
        /// <param name="mensajeAccion">Mensaje de la acción realizada</param>
        /// <param name="fotoPersonalNoRegistrado">Foto del personal no registrado en
        /// el sistema</param>
        private async Task GuardarBitacoraDeAccionDelEquipoBiometricoParaExcepciones(object personalEmpresa,
            object equipoBiometrico, bool accesoDenegadoConFoto, bool accesoDenegadoSinFoto, 
            string mensajeAccion, string fotoPersonalNoRegistrado)
        {
            var datosPersonalEmpresa = (PersonalEmpresa) personalEmpresa;
            var datosEquipoBiometrico = (EquipoBiometrico) equipoBiometrico;
            var codigoResultadoAcceso = accesoDenegadoConFoto || accesoDenegadoSinFoto
                ? ResultadoAcceso.Denegado
                : ResultadoAcceso.Error;
            var resultadoAcceso = _repositorioConsulta.ObtenerPorCodigo<ResultadoAcceso>(
                codigoResultadoAcceso);
            var bitacora = _servicioDominioBitacoraAccionEquipoBiometrico.CrearBitacora(
                datosPersonalEmpresa, datosEquipoBiometrico, resultadoAcceso, mensajeAccion, 
                accesoDenegadoConFoto ? fotoPersonalNoRegistrado : string.Empty);
            _repositorioOperacion.Agregar(bitacora);
            _repositorioOperacion.GuardarCambios();
            try
            {
                if ((accesoDenegadoConFoto || accesoDenegadoSinFoto) && equipoBiometrico != null)
                    _servicioAlerta.EnviarAlertaDeAccesosDenegados(datosPersonalEmpresa, 
                        datosEquipoBiometrico, accesoDenegadoConFoto 
                            ? fotoPersonalNoRegistrado : string.Empty).ConfigureAwait(false);
            }
            catch (ExcepcionEquipoBiometricoPersonalizada excepcion)
            {
                GuardarBitacoraDeAccionDelEquipoBiometricoConFalloEnEnvioDeAlerta(
                    datosPersonalEmpresa, datosEquipoBiometrico, excepcion.Message);
            }
        }
        /// <summary>
        /// Método que guarda la bitácora de la acción de un equipo biométrico, donde
        /// el envío de alerta de accesos denegados ha fallado
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa quien realiza la acción
        /// mediante el equipo biométrico</param>
        /// <param name="equipoBiometrico">Equipo biométrico origen</param>
        /// <param name="mensajeAccion">Mensaje de la excepción</param>
        private void GuardarBitacoraDeAccionDelEquipoBiometricoConFalloEnEnvioDeAlerta(
            PersonalEmpresa personalEmpresa, EquipoBiometrico equipoBiometrico, string mensajeAccion)
        {
            var resultadoAcceso = _repositorioConsulta.ObtenerPorCodigo<ResultadoAcceso>(
                ResultadoAcceso.Error);
            var bitacora = _servicioDominioBitacoraAccionEquipoBiometrico.CrearBitacora(personalEmpresa,
                equipoBiometrico, resultadoAcceso, mensajeAccion, string.Empty);
            _repositorioOperacion.Agregar(bitacora);
            _repositorioOperacion.GuardarCambios();
        }
        #endregion
    }
}
