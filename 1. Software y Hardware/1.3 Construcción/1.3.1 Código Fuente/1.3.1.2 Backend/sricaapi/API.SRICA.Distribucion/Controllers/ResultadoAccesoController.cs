using API.SRICA.Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.SRICA.Distribucion.Controllers
{
    /// <summary>
    /// Controlador para las consultas de los resultados (tipos) de acceso de los equipos biométricos
    /// </summary>
    [Route("api/sistemas/resultados-accesos")]
    [ApiController]
    [Authorize]
    public class ResultadoAccesoController : BaseController
    {
        /// <summary>
        /// Servicio para las consultas de los resultados (tipos) de acceso de los equipos biométricos
        /// </summary>
        private readonly IServicioResultadoAcceso _servicioResultadoAcceso;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioResultadoAcceso">Servicio para las consultas de los resultados (tipos) 
        /// de acceso de los equipos biométricos</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario obtenido 
        /// desde el token</param>
        public ResultadoAccesoController(IServicioResultadoAcceso servicioResultadoAcceso,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema,
            IServicioUsuario servicioUsuario) : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioResultadoAcceso = servicioResultadoAcceso;
        }
        /// <summary>
        /// Método que obtiene el listado de resultados (tipos) de acceso de los equipos biométricos
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con el listado de resultados (tipos) de acceso de los 
        /// equipos biométricos encontrados</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDeResultadosDeAcceso(string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioResultadoAcceso.ObtenerListadoDeResultadosDeAcceso(),
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
    }
}
