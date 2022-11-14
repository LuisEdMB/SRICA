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
    /// Controlador para las consultas y operaciones de bitácoras de acción del sistema
    /// </summary>
    [Route("api/bitacoras/acciones-sistemas")]
    [ApiController]
    [Authorize]
    public class BitacoraAccionSistemaController : BaseController
    {
        /// <summary>
        /// Servicio para consultas y operaciones de bitácoras de acciones del sistema
        /// </summary>
        private readonly IServicioBitacoraAccionSistema _servicioBitacoraAccionSistema;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones de 
        /// bitácoras de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario obtenido 
        /// desde el token</param>
        public BitacoraAccionSistemaController(IServicioBitacoraAccionSistema servicioBitacoraAccionSistema,
            IServicioUsuario servicioUsuario) 
            : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioBitacoraAccionSistema = servicioBitacoraAccionSistema;
        }
        /// <summary>
        /// Método que guarda una bitácora de acción del sistema
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos de la bitácora de 
        /// acción del sistema a guardar</param>
        /// <returns>Resultado encriptado con los datos de la bitácora de acción del sistema
        /// guardada</returns>
        [HttpPost]
        public IActionResult GuardarBitacoraDeAccionDelSistema([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioBitacoraAccionSistema.GuardarBitacoraDeAccionDelSistema(datos), 
                string.Empty, false);
        }
        /// <summary>
        /// Método que obtiene el listado de bitácora de acciones del sistema
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <param name="guardarBitacora">Si se desea guardar la bitacora</param>
        /// <returns>Resultado encriptado con el listado de bitácora de acciones del sistema
        /// encontrados</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDeBitacoraDeAccionesDelSistema(
            string bitacoraAccionSistema = "", bool guardarBitacora = false)
            => Ejecutar(() => _servicioBitacoraAccionSistema.ObtenerListadoDeBitacoraDeAccionesDelSistema(),
                Uri.UnescapeDataString(bitacoraAccionSistema), guardarBitacora);
    }
}