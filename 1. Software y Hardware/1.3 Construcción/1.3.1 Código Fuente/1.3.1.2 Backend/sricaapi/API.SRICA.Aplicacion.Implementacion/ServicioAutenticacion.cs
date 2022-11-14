using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.US;
using API.SRICA.Dominio.Interfaz;
using API.SRICA.Dominio.Servicio.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación del servicio de autenticación del inicio de sesión del usuario
    /// que intenta iniciar sesión en el sistema
    /// </summary>
    public class ServicioAutenticacion : IServicioAutenticacion
    {
        /// <summary>
        /// Configuración del proyecto
        /// </summary>
        private readonly IConfiguration _configuracion;
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
        /// Servicio de dominio del usuario
        /// </summary>
        private readonly IServicioDominioUsuario _servicioDominioUsuario;
        /// <summary>
        /// Servicio de generación de token
        /// </summary>
        private readonly IServicioToken _servicioToken;
        /// <summary>
        /// Servicio de mapeo de entidad a DTO
        /// </summary>
        private readonly IServicioMapeoUsuarioAutenticadoADto _servicioMapeoUsuarioAutenticadoADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="configuracion">Configuración del proyecto</param>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioDesencriptador">Servicio para la desencriptación de datos</param>
        /// <param name="repositorioConsulta">Repositorio de consultas a la base de datos</param>
        /// <param name="servicioDominioUsuario">Servicio de dominio del usuario</param>
        /// <param name="servicioToken">Servicio de generación de token</param>
        /// <param name="servicioMapeoUsuarioAutenticadoADTO">Servicio de mapeo del usuario autenticado 
        /// a DTO</param>
        public ServicioAutenticacion(IConfiguration configuracion, IServicioEncriptador servicioEncriptador, 
            IServicioDesencriptador servicioDesencriptador, IRepositorioConsulta repositorioConsulta, 
            IServicioDominioUsuario servicioDominioUsuario, IServicioToken servicioToken, 
            IServicioMapeoUsuarioAutenticadoADto servicioMapeoUsuarioAutenticadoADTO)
        {
            _configuracion = configuracion;
            _servicioEncriptador = servicioEncriptador;
            _servicioDesencriptador = servicioDesencriptador;
            _repositorioConsulta = repositorioConsulta;
            _servicioDominioUsuario = servicioDominioUsuario;
            _servicioToken = servicioToken;
            _servicioMapeoUsuarioAutenticadoADTO = servicioMapeoUsuarioAutenticadoADTO;
        }
        /// <summary>
        /// Método que comprueba el usuario y contraseña del usuario que intenta
        /// iniciar sesión en el sistema
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el usuario,
        /// contraseña y audiencia permitida para el inicio de sesión del usuario</param>
        /// <returns>Resultado encriptado del proceso de inicio de sesión</returns>
        public string ComprobarUsuario(JToken encriptado)
        {
            var inicioSesionDTO = _servicioDesencriptador.Desencriptar<DtoUsuario>(encriptado.ToString());
            _servicioToken.ValidarAudiencia(inicioSesionDTO.AudienciaPermitida,
                _configuracion["SEGURIDAD_AUDIENCIA_PERMITIDA"]);
            var usuario = _repositorioConsulta.ObtenerPorExpresionLimite<Usuario>(g =>
                g.UsuarioAcceso.Equals(inicioSesionDTO.Usuario.Trim())).FirstOrDefault();
            var contrasena = _servicioEncriptador.Encriptar(inicioSesionDTO.Contrasena);
            usuario = _servicioDominioUsuario.ValidarUsuarioParaAutenticacion(usuario, contrasena);
            var token = _servicioToken.GenerarToken(
                usuario, _configuracion["SEGURIDAD_CLAVE_SECRETA"], 
                _configuracion["SEGURIDAD_ISSUER"],
                _configuracion["SEGURIDAD_AUDIENCIA_PERMITIDA"]);
            var respuestaDTO = _servicioMapeoUsuarioAutenticadoADTO.MapearADTO(usuario, token);
            return _servicioEncriptador.Encriptar(respuestaDTO);
        }
        /// <summary>
        /// Método que refresca el token del usuario
        /// </summary>
        /// <param name="encriptado">Datos encriptado que contiene el token expirado para generar 
        /// el nuevo token</param>
        /// <returns>Resultado encriptado del proceso de inicio de sesión</returns>
        public string RefrescarTokenDelUsuario(JToken encriptado)
        {
            var refrescoTokenDTO = _servicioDesencriptador.Desencriptar<DtoUsuario>(encriptado.ToString());
            _servicioToken.ValidarAudiencia(refrescoTokenDTO.AudienciaPermitida,
                _configuracion["SEGURIDAD_AUDIENCIA_PERMITIDA"]);
            var token = _servicioToken.RefrescarToken(
                refrescoTokenDTO.Token,
                _configuracion["SEGURIDAD_CLAVE_SECRETA"],
                _configuracion["SEGURIDAD_ISSUER"],
                _configuracion["SEGURIDAD_AUDIENCIA_PERMITIDA"]);
            return _servicioEncriptador.Encriptar(token);
        }
    }
}
