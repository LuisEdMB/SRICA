using API.SRICA.Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.SRICA.Distribucion.Controllers
{
    /// <summary>
    /// Controlador para las consultas de las acciones del sistema
    /// </summary>
    [Route("api/sistemas/acciones")]
    [ApiController]
    [Authorize]
    public class AccionSistemaController : BaseController
    {
        /// <summary>
        /// Servicio para las consultas de las acciones del sistema
        /// </summary>
        private readonly IServicioAccionSistema _servicioAccionSistema;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioAccionSistema">Servicio para las consultas de las acciones del 
        /// sistema</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario obtenido 
        /// desde el token</param>
        public AccionSistemaController(IServicioAccionSistema servicioAccionSistema,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema,
            IServicioUsuario servicioUsuario) : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioAccionSistema = servicioAccionSistema;
        }
        /// <summary>
        /// Método que obtiene el listado de acciones del sistema
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con el listado de acciones del sistema encontrados</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDeAccionesDelSistema(string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioAccionSistema.ObtenerListadoDeAccionesDelSistema(),
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
    }
}
