using API.SRICA.Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.SRICA.Distribucion.Controllers
{
    /// <summary>
    /// Controlador para las consultas de los recursos del sistema
    /// </summary>
    [Route("api/sistemas/recursos")]
    [ApiController]
    [Authorize]
    public class RecursoSistemaController : BaseController
    {
        /// <summary>
        /// Servicio para las consultas de los recursos del sistema
        /// </summary>
        private readonly IServicioRecursoSistema _servicioRecursoSistema;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioRecursoSistema">Servicio para las consultas de los recursos del 
        /// sistema</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario obtenido 
        /// desde el token</param>
        public RecursoSistemaController(IServicioRecursoSistema servicioRecursoSistema,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema,
            IServicioUsuario servicioUsuario) : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioRecursoSistema = servicioRecursoSistema;
        }
        /// <summary>
        /// Método que obtiene el listado de recursos del sistema
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con el listado de recursos del sistema encontrados</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDeRecursosDelSistema(string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioRecursoSistema.ObtenerListadoDeRecursosDelSistema(),
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
    }
}
