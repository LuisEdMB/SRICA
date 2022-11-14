using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.BT;
using API.SRICA.Dominio.Entidad.SI;
using API.SRICA.Dominio.Entidad.US;
using API.SRICA.Dominio.Interfaz;
using API.SRICA.Dominio.Servicio.Interfaz;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación del servicio de consultas y operaciones de bitácoras de acciones del sistema
    /// </summary>
    public class ServicioBitacoraAccionSistema : IServicioBitacoraAccionSistema
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
        /// Servicio de dominio de la bitácora de acción del sistema
        /// </summary>
        private readonly IServicioDominioBitacoraAccionSistema _servicioDominioBitacoraAccionSistema;
        /// <summary>
        /// Servicio de mapeo de la bitácora de acción del sistema a DTO
        /// </summary>
        private readonly IServicioMapeoBitacoraAccionSistemaADto _servicioMapeoBitacoraAccionSistemaADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioDesencriptador">Servicio para la desencriptación de datos</param>
        /// <param name="repositorioConsulta">Repositorio de consultas a la base de datos</param>
        /// <param name="repositorioOperacion">Repositorio de operaciones a la base de datos</param>
        /// <param name="servicioDominioBitacoraAccionSistema">Servicio de dominio de la bitácora 
        /// de acción del sistema</param>
        /// <param name="servicioMapeoBitacoraAccionSistemaADTO">Servicio de mapeo de la bitácora 
        /// de acción del sistema a DTO</param>
        public ServicioBitacoraAccionSistema(IServicioEncriptador servicioEncriptador,
            IServicioDesencriptador servicioDesencriptador,
            IRepositorioConsulta repositorioConsulta,
            IRepositorioOperacion repositorioOperacion,
            IServicioDominioBitacoraAccionSistema servicioDominioBitacoraAccionSistema,
            IServicioMapeoBitacoraAccionSistemaADto servicioMapeoBitacoraAccionSistemaADTO)
        {
            _servicioEncriptador = servicioEncriptador;
            _servicioDesencriptador = servicioDesencriptador;
            _repositorioConsulta = repositorioConsulta;
            _repositorioOperacion = repositorioOperacion;
            _servicioDominioBitacoraAccionSistema = servicioDominioBitacoraAccionSistema;
            _servicioMapeoBitacoraAccionSistemaADTO = servicioMapeoBitacoraAccionSistemaADTO;
        }
        /// <summary>
        /// Método que guarda una bitácora de acción del sistema
        /// </summary>
        /// <param name="bitacoraAccion">Datos encriptados que contiene los datos de la bitácora 
        /// de acción del sistema a guardar (encriptado)</param>
        /// <param name="validacion">Si el tipo de evento de la acción es "Validación" (por defecto está 
        /// inicializado al valor "FALSE")</param>
        /// <param name="error">Si el tipo de evento de la acción es "Error" (por defecto está 
        /// inicializado al valor "FALSE")</param>
        /// <param name="mensajeValidacion">Mensaje de validación de la acción (por defecto está 
        /// inicializado al valor "")</param>
        /// <param name="mensajeError">Mensaje de error de la acción (por defecto está 
        /// inicializado al valor "")</param>
        /// <returns>Resultado encriptado con los datos de la bitácora guardada</returns>
        public string GuardarBitacoraDeAccionDelSistema(JToken bitacoraAccion, bool validacion = false,
            bool error = false, string mensajeValidacion = "", string mensajeError = "")
        {
            var bitacoraDTO = _servicioDesencriptador.Desencriptar<DtoBitacoraAccionSistema>(
                bitacoraAccion.ToString());
            var codigoTipoEventoSistema = string.IsNullOrEmpty(bitacoraDTO.CodigoTipoEventoSistema) 
                ? ObtenerTipoDeEventoDelSistema(validacion, error) 
                : _servicioDesencriptador.Desencriptar<int>(bitacoraDTO.CodigoTipoEventoSistema);
            bitacoraDTO.DescripcionResultadoAccion = ObtenerDescripcionDelResultadoDeAccion(
                bitacoraDTO.DescripcionResultadoAccion, mensajeValidacion, mensajeError);
            var usuarioPorCodigoUsuario = _repositorioConsulta.ObtenerPorCodigo<Usuario>(
                string.IsNullOrEmpty(bitacoraDTO.CodigoUsuario)
                    ? 0
                    : _servicioDesencriptador.Desencriptar<int>(bitacoraDTO.CodigoUsuario));
            var usuarioPorUsuarioAcceso = _repositorioConsulta.ObtenerPorExpresionLimite<Usuario>(g =>
                g.UsuarioAcceso.Equals(bitacoraDTO.UsuarioAcceso.Trim())).FirstOrDefault();
            var moduloSistema = _repositorioConsulta.ObtenerPorCodigo<ModuloSistema>(
                _servicioDesencriptador.Desencriptar<int>(bitacoraDTO.CodigoModuloSistema));
            var recursoSistema = _repositorioConsulta.ObtenerPorCodigo<RecursoSistema>(
                _servicioDesencriptador.Desencriptar<int>(bitacoraDTO.CodigoRecursoSistema));
            var tipoEventoSistema = _repositorioConsulta.ObtenerPorCodigo<TipoEventoSistema>(
                string.IsNullOrEmpty(bitacoraDTO.CodigoTipoEventoSistema) 
                    ? codigoTipoEventoSistema 
                    : _servicioDesencriptador.Desencriptar<int>(bitacoraDTO.CodigoTipoEventoSistema));
            var accionSistema = _repositorioConsulta.ObtenerPorCodigo<AccionSistema>(
                _servicioDesencriptador.Desencriptar<int>(bitacoraDTO.CodigoAccionSistema));
            var bitacora = _servicioDominioBitacoraAccionSistema.CrearBitacora(
                usuarioPorCodigoUsuario ?? usuarioPorUsuarioAcceso,
                moduloSistema, recursoSistema, tipoEventoSistema, accionSistema,
                bitacoraDTO.DescripcionResultadoAccion, bitacoraDTO.ValorAnterior.ToString(), 
                bitacoraDTO.ValorActual.ToString());
            _repositorioOperacion.Agregar(bitacora);
            _repositorioOperacion.GuardarCambios();
            var bitacoraNuevo = _repositorioConsulta.ObtenerPorCodigo<BitacoraAccionSistema>(
                bitacora.CodigoBitacora);
            var bitacoraNuevoDTO = _servicioMapeoBitacoraAccionSistemaADTO.MapearADTO(bitacoraNuevo);
            return _servicioEncriptador.Encriptar(bitacoraNuevoDTO);
        }
        /// <summary>
        /// Método que obtiene el listado de bitácora de acciones del sistema según un rango de fechas
        /// </summary>
        /// <returns>Resultado encriptado con el listado de bitácora de acciones del sistema
        /// encontrados</returns>
        public string ObtenerListadoDeBitacoraDeAccionesDelSistema()
        {
            var bitacora = _repositorioConsulta.ObtenerPorExpresionLimite<BitacoraAccionSistema>()
                .ToList();
            var bitacoraDTO = bitacora.Select(g => _servicioMapeoBitacoraAccionSistemaADTO.MapearADTO(g))
                .ToList();
            return _servicioEncriptador.Encriptar(bitacoraDTO);
        }
        #region Métodos privados
        /// <summary>
        /// Método que obtiene el tipo de evento del sistema según la acción realizada
        /// </summary>
        /// <param name="validacion">Si el tipo de evento es "Validación"</param>
        /// <param name="error">Si el tipo de evento es "Error"</param>
        /// <returns>Código del tipo de evento del sistema de la acción realizada</returns>
        private int ObtenerTipoDeEventoDelSistema(bool validacion, bool error)
        {
            if (validacion)
                return TipoEventoSistema.CodigoTipoEventoValidacion;
            return error ? TipoEventoSistema.CodigoTipoEventoError : TipoEventoSistema.CodigoTipoEventoCorrecto;
        }
        /// <summary>
        /// Método que obtiene el mensaje del resultado de la acción realizada
        /// </summary>
        /// <param name="mensajeCorrecto">Mensaje del resultado de acción para 
        /// proceso correcto</param>
        /// <param name="mensajeValidacion">Mensaje del resultado de acción para 
        /// proceso de validación</param>
        /// <param name="mensajeError">Mensaje del resultado de acción para 
        /// proceso de error</param>
        /// <returns>Descripción del resultado de la acción realizada</returns>
        private string ObtenerDescripcionDelResultadoDeAccion(string mensajeCorrecto, 
            string mensajeValidacion, string mensajeError)
        {
            if (!string.IsNullOrEmpty(mensajeError))
                return mensajeError;
            if (!string.IsNullOrEmpty(mensajeValidacion))
                return mensajeValidacion;
            return mensajeCorrecto;
        }
        #endregion
    }
}
