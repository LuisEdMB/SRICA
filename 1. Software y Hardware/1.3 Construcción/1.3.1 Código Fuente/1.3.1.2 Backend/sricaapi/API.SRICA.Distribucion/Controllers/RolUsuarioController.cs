using API.SRICA.Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.SRICA.Distribucion.Controllers
{
    /// <summary>
    /// Controlador para las consultas de roles de usuario
    /// </summary>
    [Route("api/roles-usuarios")]
    [ApiController]
    [Authorize]
    public class RolUsuarioController : BaseController
    {
        /// <summary>
        /// Servicio para consultas de roles de usuario
        /// </summary>
        private readonly IServicioRolUsuario _servicioRolUsuario;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioRolUsuario">Servicio para consultas de roles de usuario</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario obtenido 
        /// desde el token</param>
        public RolUsuarioController(IServicioRolUsuario servicioRolUsuario,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema, IServicioUsuario servicioUsuario)
            : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioRolUsuario = servicioRolUsuario;
        }
        /// <summary>
        /// Método que obtiene el listado de roles de usuario, tanto activos como inactivos
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con el listado de los roles de usuario</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDeRolesDeUsuario(string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioRolUsuario.ObtenerListadoDeRolesDeUsuario(),
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
    }
}