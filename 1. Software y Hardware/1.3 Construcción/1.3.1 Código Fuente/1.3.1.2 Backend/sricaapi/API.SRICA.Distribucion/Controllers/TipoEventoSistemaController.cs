using API.SRICA.Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.SRICA.Distribucion.Controllers
{
    /// <summary>
    /// Controlador para las consultas de los tipos de eventos del sistema
    /// </summary>
    [Route("api/sistemas/tipos-eventos")]
    [ApiController]
    [Authorize]
    public class TipoEventoSistemaController : BaseController
    {
        /// <summary>
        /// Servicio para las consultas de los tipos de eventos del sistema
        /// </summary>
        private readonly IServicioTipoEventoSistema _servicioTipoEventoSistema;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioTipoEventoSistema">Servicio para las consultas de los tipos 
        /// de eventos del sistema</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario obtenido 
        /// desde el token</param>
        public TipoEventoSistemaController(IServicioTipoEventoSistema servicioTipoEventoSistema,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema,
            IServicioUsuario servicioUsuario) : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioTipoEventoSistema = servicioTipoEventoSistema;
        }
        /// <summary>
        /// Método que obtiene el listado de tipos de eventos del sistema
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con el listado de tipos de eventos del sistema 
        /// encontrados</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDeTiposDeEventosDelSistema(string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioTipoEventoSistema.ObtenerListadoDeTiposDeEventosDelSistema(),
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
    }
}
