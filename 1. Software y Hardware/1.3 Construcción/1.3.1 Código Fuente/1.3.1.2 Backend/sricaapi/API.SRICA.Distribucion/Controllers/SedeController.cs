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
    /// Controlador para las consultas y operaciones de sedes
    /// </summary>
    [Route("api/sedes")]
    [ApiController]
    [Authorize]
    public class SedeController : BaseController
    {
        /// <summary>
        /// Servicio para consultas y operaciones de sedes
        /// </summary>
        private readonly IServicioSede _servicioSede;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioSede">Servicio para consultas y operaciones de sedes</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario obtenido 
        /// desde el token</param>
        public SedeController(IServicioSede servicioSede,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema, IServicioUsuario servicioUsuario)
            : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioSede = servicioSede;
        }
        /// <summary>
        /// Método que obtiene el listado de las sedes registradas, tanto activos como inactivos
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con el listado de las sedes</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDeSedes(string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioSede.ObtenerListadoDeSedes(), 
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
        /// <summary>
        /// Método que obtiene una sede en base a su código de sede
        /// </summary>
        /// <param name="codigoSedeEncriptado">Código de la sede a obtener (encriptado)</param>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con los datos de la sede encontrada</returns>
        [HttpGet]
        [Route("{codigoSedeEncriptado}")]
        public IActionResult ObtenerSede(string codigoSedeEncriptado,
            string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioSede.ObtenerSede(Uri.UnescapeDataString(codigoSedeEncriptado)),
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
        /// <summary>
        /// Método que guarda una nueva sede
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos de la sede a guardar. 
        /// Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con los datos de la sede guardada</returns>
        [HttpPost]
        public IActionResult GuardarSede([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioSede.GuardarSede(datos), bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que modifica una sede
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos de la sede a modificar.
        /// Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con los datos de la sede modificada</returns>
        [HttpPatch]
        public IActionResult ModificarSede([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioSede.ModificarSede(datos), bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que inhabilita a las sedes
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las sedes a 
        /// inhabilitar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con el listado de sedes inhabilitadas</returns>
        [HttpPatch]
        [Route("inhabilitar")]
        public IActionResult InhabilitarSedes([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioSede.InhabilitarSedes(datos), bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que habilita a las sedes
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las sedes a 
        /// habilitar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con el listado de sedes habilitadas</returns>
        [HttpPatch]
        [Route("habilitar")]
        public IActionResult HabilitarSedes([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioSede.HabilitarSedes(datos), bitacoraAccionSistema);
        }
    }
}