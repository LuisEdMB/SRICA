using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Distribucion.VariableConstante;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace API.SRICA.Distribucion.Controllers
{
    /// <summary>
    /// Controlador para las consultas y operaciones de usuarios
    /// </summary>
    [Route("api/usuarios")]
    [ApiController]
    [Authorize]
    public class UsuarioController : BaseController
    {
        /// <summary>
        /// Servicio para consultas y operaciones de usuarios
        /// </summary>
        private readonly IServicioUsuario _servicioUsuario;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioUsuario">Servicio para consultas y operaciones de usuarios. Así mismo, 
        /// es usado para verificar el estado del usuario obtenido desde el token</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        public UsuarioController(IServicioUsuario servicioUsuario,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema)
            : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioUsuario = servicioUsuario;
        }
        /// <summary>
        /// Método que modifica los datos del usuario. Las operaciones que se pueden realizar son:
        /// Cambiar datos por defecto, actualizar contraseña, modificar perfil de usuario,
        /// modificar datos del usuario
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los valores a modificar. Así mismo,
        /// contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con los datos del usuario y sus valores modificados</returns>
        [HttpPatch]
        public IActionResult ModificarUsuario([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioUsuario.ModificarUsuario(datos), bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que obtiene un usuario en base a su código de usuario
        /// </summary>
        /// <param name="codigoUsuarioEncriptado">Código del usuario a obtener (encriptado)</param>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con los datos del usuario encontrado</returns>
        [HttpGet]
        [Route("{codigoUsuarioEncriptado}")]
        public IActionResult ObtenerUsuario(string codigoUsuarioEncriptado,
            string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioUsuario.ObtenerUsuario(
                Uri.UnescapeDataString(codigoUsuarioEncriptado)), 
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
        /// <summary>
        /// Método que obtiene el listado de usuarios, tanto activos como inactivos
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con el listado de usuarios</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDeUsuarios(string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioUsuario.ObtenerListadoDeUsuarios(),
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
        /// <summary>
        /// Método que guarda un nuevo usuario
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del usuario a guardar. 
        /// Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con los datos del usuario guardado</returns>
        [HttpPost]
        public IActionResult GuardarUsuario([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioUsuario.GuardarUsuario(datos), bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que inhabilita a los usuarios
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de usuarios a 
        /// inhabilitar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con el listado de usuarios inhabilitados</returns>
        [HttpPatch]
        [Route("inhabilitar")]
        public IActionResult InhabilitarUsuarios([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioUsuario.InhabilitarUsuarios(datos), bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que habilita a los usuarios
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de usuarios a 
        /// habilitar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con el listado de usuarios habilitados</returns>
        [HttpPatch]
        [Route("habilitar")]
        public IActionResult HabilitarUsuarios([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioUsuario.HabilitarUsuarios(datos), bitacoraAccionSistema);
        }
    }
}