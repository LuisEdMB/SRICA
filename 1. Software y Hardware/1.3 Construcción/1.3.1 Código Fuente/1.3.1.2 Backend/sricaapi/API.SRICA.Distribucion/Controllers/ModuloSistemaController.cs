using API.SRICA.Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.SRICA.Distribucion.Controllers
{
    /// <summary>
    /// Controlador para las consultas de los módulos del sistema
    /// </summary>
    [Route("api/sistemas/modulos")]
    [ApiController]
    [Authorize]
    public class ModuloSistemaController : BaseController
    {
        /// <summary>
        /// Servicio para las consultas de los módulos del sistema
        /// </summary>
        private readonly IServicioModuloSistema _servicioModuloSistema;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioModuloSistema">Servicio para las consultas de los módulos del 
        /// sistema</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario obtenido 
        /// desde el token</param>
        public ModuloSistemaController(IServicioModuloSistema servicioModuloSistema,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema, 
            IServicioUsuario servicioUsuario) : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioModuloSistema = servicioModuloSistema;
        }
        /// <summary>
        /// Método que obtiene el listado de módulos del sistema
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con el listado de módulos del sistema encontrados</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDeModulosDelSistema(string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioModuloSistema.ObtenerListadoDeModulosDelSistema(),
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
    }
}
