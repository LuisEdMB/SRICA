using System;
using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.US;
using API.SRICA.Dominio.Interfaz;
using API.SRICA.Dominio.Servicio.Interfaz;
using API.SRICA.Dominio.ServicioExterno.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.PE;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación del servicio para envíos de alertas (envío de correos electrónicos)
    /// </summary>
    public class ServicioAlerta : IServicioAlerta
    {
        /// <summary>
        /// Plantilla de correo general
        /// </summary>
        private const string PlantillaCuerpoAlertaGeneral =
            @"<html>
                <body>
                    {Mensaje}
                    <br />
                    <br />
                    <br />
                    <p><b><i>'Sistema de Reconocimiento de Iris para Control de Accesos'</i><b/></p>
                    <p><b><i>© 2020 Luis Eduardo Mamani Bedregal. Todos los derechos reservados.</i><b/></p>
                </body>
            </html>";

        /// <summary>
        /// Plantilla de correo para la alerta de recuperación de contraseña olvidada
        /// </summary>
        private const string PlantillaCuerpoAlertaRecuperacionContrasena =
            @"<p>Hola {Nombre} {Apellido}, haz clic en el siguiente enlace para recuperar tu contraseña:
                <br />
                <br />
                <b>Enlace:</b> <a href='{Enlace}' target='_blank'>Haz click aquí para ir al enlace</a>
                <br />
                <br />
                <b><i>Nota: Solo tiene 2 minutos para completar el proceso de recuperación de contraseña.
                </i></b>
            </p>";
        /// <summary>
        /// Plantilla de correo para la alerta de accesos denegados
        /// </summary>
        private const string PlantillaCuerpoAlertaAccesosDenegados =
            @"<p>Hola, se ha detectado un acceso denegado:
                <br />
                <br />
                <ul>
                    <li>Sede: {DescripcionSede}</li>
                    <li>Área: {DescripcionArea}</li>
                    <li>Personal: {NombreDniPersonal}</li>
                </ul>
            </p>";
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
        /// Microservicio de correo
        /// </summary>
        private readonly IMicroservicioCorreo _microservicioCorreo;
        /// <summary>
        /// Servicio de mapeo del usuario a DTO
        /// </summary>
        private readonly IServicioMapeoUsuarioADto _servicioMapeoUsuarioADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="configuracion">Configuración del proyecto</param>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioDesencriptador">Servicio para la desencriptación de datos</param>
        /// <param name="repositorioConsulta">Repositorio de consultas a la base de datos</param>
        /// <param name="servicioDominioUsuario">Servicio de dominio del usuario</param>
        /// <param name="servicioToken">Servicio de generación de token</param>
        /// <param name="microservicioCorreo">Microservicio de correo</param>
        /// <param name="servicioMapeoUsuarioADTO">Servicio de mapeo del usuario a DTO</param>
        public ServicioAlerta(IConfiguration configuracion, IServicioEncriptador servicioEncriptador,
            IServicioDesencriptador servicioDesencriptador, IRepositorioConsulta repositorioConsulta,
            IServicioDominioUsuario servicioDominioUsuario, IServicioToken servicioToken,
            IMicroservicioCorreo microservicioCorreo, IServicioMapeoUsuarioADto servicioMapeoUsuarioADTO)
        {
            _configuracion = configuracion;
            _servicioEncriptador = servicioEncriptador;
            _servicioDesencriptador = servicioDesencriptador;
            _repositorioConsulta = repositorioConsulta;
            _servicioDominioUsuario = servicioDominioUsuario;
            _servicioToken = servicioToken;
            _microservicioCorreo = microservicioCorreo;
            _servicioMapeoUsuarioADTO = servicioMapeoUsuarioADTO;
        }
        /// <summary>
        /// Método que envía la alerta (correo electrónico) al usuario para que pueda recuperar su contraseña
        /// olvidada
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el usuario a quien se le enviará la 
        /// alerta</param>
        /// <returns>Resultado encriptado con el resultado del proceso de envío de alerta</returns>
        public string EnviarAlertaDeRecuperacionDeContrasenaOlvidada(JToken encriptado)
        {
            var usuarioAcceso = _servicioDesencriptador.Desencriptar<DtoUsuario>(encriptado.ToString())?
                .Usuario ?? string.Empty;
            var usuario = _repositorioConsulta.ObtenerPorExpresionLimite<Usuario>(g =>
                g.UsuarioAcceso.Equals(usuarioAcceso.Trim())).FirstOrDefault();
            _servicioDominioUsuario.ValidarUsuarioParaRecuperacionDeContrasenaOlvidada(usuario);
            var tokenTemporal = _servicioToken.GenerarToken(usuario,
                _configuracion["SEGURIDAD_CLAVE_SECRETA"],
                _configuracion["SEGURIDAD_ISSUER"],
                _configuracion["SEGURIDAD_AUDIENCIA_PERMITIDA"]);
            EnviarCorreoDeAlertaDeRecuperacionDeContrasenaOlvidada(usuario, tokenTemporal);
            var usuarioDTO = _servicioMapeoUsuarioADTO.MapearADTO(usuario);
            return _servicioEncriptador.Encriptar(usuarioDTO);
        }
        /// <summary>
        /// Método que envía la alerta de acceso denegado a todos los usuarios del sistema
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa que realiza el proceso
        /// de reconocimiento</param>
        /// <param name="equipoBiometrico">Equipo biométrico origen</param>
        /// <param name="fotoPersonalNoRegistrado">Foto a adjuntar en la alerta</param>
        public async Task EnviarAlertaDeAccesosDenegados(PersonalEmpresa personalEmpresa,
            EquipoBiometrico equipoBiometrico, string fotoPersonalNoRegistrado)
        {
            var usuarios =
                _repositorioConsulta.ObtenerPorExpresionLimite<Usuario>(g => g.IndicadorEstado).ToList();
            var usuariosConCorreosValidos = _servicioDominioUsuario.FiltrarUsuariosConCorreosValidos(usuarios);
            var asuntoCorreo = "SRICA - Acceso Denegado";
            var cuerpoCorreo = ArmarCuerpoDeAlertaDeAccesosDenegados(personalEmpresa, equipoBiometrico);
            _microservicioCorreo.EnviarCorreoAsync(
                _configuracion["MICROSERVICIO_CORREO_URL"],
                usuariosConCorreosValidos.Select(g => g.CorreoElectronico).ToList(),
                asuntoCorreo, cuerpoCorreo, fotoPersonalNoRegistrado, true)
                .ConfigureAwait(false);
        }
        #region Métodos privados
        /// <summary>
        /// Método que envía el correo de alerta de recuperación de contraseña al usuario 
        /// correspondiente
        /// </summary>
        /// <param name="usuario">Usuario que contiene los datos correspondientes a considerar 
        /// para el envío de la alerta</param>
        /// <param name="tokenTemporal">Token temporal generado para el usuario que intenta recuperar
        /// su contraseña</param>
        private void EnviarCorreoDeAlertaDeRecuperacionDeContrasenaOlvidada(Usuario usuario,
            string tokenTemporal)
        {
            var asuntoCorreo = "SRICA - Recuperación de Contraseña Olvidada";
            var cuerpoCorreo = ArmarCuerpoDeAlertaDeRecuperacionDeContrasenaOlvidada(usuario,
                tokenTemporal, _configuracion["CLIENTE_SRICA_URL"]);
            _microservicioCorreo.EnviarCorreo(
                _configuracion["MICROSERVICIO_CORREO_URL"],
                new List<string>() {usuario.CorreoElectronico},
                asuntoCorreo, cuerpoCorreo, null);
        }
        /// <summary>
        /// Método que arma el cuerpo de la alerta de recuperación de contraseña olvidada según los 
        /// datos del usuario y token temporal generado
        /// </summary>
        /// <param name="usuario">Usuario que contiene los datos correspondientes a considerar 
        /// para el envío de la alerta</param>
        /// <param name="tokenTemporal">Token temporal generado para el usuario que intenta recuperar
        /// su contraseña</param>
        /// <param name="urlClienteFrontend">URL del enlace del cliente frontend (web)</param>
        /// <returns>Cuerpo de la alerta</returns>
        private string ArmarCuerpoDeAlertaDeRecuperacionDeContrasenaOlvidada(Usuario usuario,
            string tokenTemporal, string urlClienteFrontend)
        {
            var enlace = urlClienteFrontend + "?id=" + tokenTemporal;
            var cuerpoMensaje = PlantillaCuerpoAlertaRecuperacionContrasena
                .Replace("{Nombre}", usuario.NombreUsuario)
                .Replace("{Apellido}", usuario.ApellidoUsuario)
                .Replace("{Enlace}", enlace);
            var mensajeFinal = PlantillaCuerpoAlertaGeneral
                .Replace("{Mensaje}", cuerpoMensaje);
            return mensajeFinal;
        }
        /// <summary>
        /// Método que arma el cuerpo de la alerta de accesos denegados
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa que realiza el proceso
        /// de reconocimiento</param>
        /// <param name="equipoBiometrico">Equipo biométrico origen</param>
        /// <returns>Cuerpo de la alerta</returns>
        private string ArmarCuerpoDeAlertaDeAccesosDenegados(PersonalEmpresa personalEmpresa,
            EquipoBiometrico equipoBiometrico)
        {
            var cuerpoMensaje = PlantillaCuerpoAlertaAccesosDenegados
                .Replace("{DescripcionSede}", equipoBiometrico?.Area?.Sede?.DescripcionSede ?? string.Empty)
                .Replace("{DescripcionArea}", equipoBiometrico?.Area?.DescripcionArea ?? string.Empty)
                .Replace("{NombreDniPersonal}", $"{personalEmpresa?.DNIPersonalEmpresa ?? string.Empty}" +
                    $" - {personalEmpresa?.NombrePersonalEmpresa ?? string.Empty}" +
                    $" {personalEmpresa?.ApellidoPersonalEmpresa ?? string.Empty}");
            var mensajeFinal = PlantillaCuerpoAlertaGeneral
                .Replace("{Mensaje}", cuerpoMensaje);
            return mensajeFinal;
        }
        #endregion
    }
}
