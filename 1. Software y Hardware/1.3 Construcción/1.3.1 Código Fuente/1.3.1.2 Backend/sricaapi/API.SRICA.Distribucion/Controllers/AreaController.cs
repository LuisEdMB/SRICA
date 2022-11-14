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
    /// Controlador para las consultas y operaciones de áreas
    /// </summary>
    [Route("api/areas")]
    [ApiController]
    [Authorize]
    public class AreaController : BaseController
    {
        /// <summary>
        /// Servicio para consultas y operaciones de áreas
        /// </summary>
        private readonly IServicioArea _servicioArea;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioArea">Servicio para consultas y operaciones de áreas</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario obtenido 
        /// desde el token</param>
        public AreaController(IServicioArea servicioArea,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema, IServicioUsuario servicioUsuario)
            : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioArea = servicioArea;
        }
        /// <summary>
        /// Método que obtiene el listado de las áreas registradas, tanto activos como inactivos.
        /// Así mismo, se puede obtener el listado de áreas según una sede, tanto activos como inactivos
        /// </summary>
        /// <param name="codigoSedeEncriptado">Código de la sede (encriptado) (opcional)</param>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con el listado de las áreas encontradas</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDeAreas(string codigoSedeEncriptado = "",
            string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioArea.ObtenerListadoDeAreas(
                Uri.UnescapeDataString(codigoSedeEncriptado)),
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
        /// <summary>
        /// Método que obtiene un área en base a su código de área
        /// </summary>
        /// <param name="codigoAreaEncriptado">Código del área a obtener (encriptado)</param>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con los datos del área encontrada</returns>
        [HttpGet]
        [Route("{codigoAreaEncriptado}")]
        public IActionResult ObtenerArea(string codigoAreaEncriptado,
            string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioArea.ObtenerArea(
                Uri.UnescapeDataString(codigoAreaEncriptado)), 
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
        /// <summary>
        /// Método que guarda una nueva área
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del área a guardar. Así mismo,
        /// contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con los datos del área guardada</returns>
        [HttpPost]
        public IActionResult GuardarArea([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioArea.GuardarArea(datos), bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que modifica un área
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del área a modificar. 
        /// Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con los datos del área modificada</returns>
        [HttpPatch]
        public IActionResult ModificarArea([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioArea.ModificarArea(datos), bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que inhabilita a las áreas
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las áreas a 
        /// inhabilitar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con el listado de áreas inhabilitadas</returns>
        [HttpPatch]
        [Route("inhabilitar")]
        public IActionResult InhabilitarAreas([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioArea.InhabilitarAreas(datos), bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que habilita a las áreas
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las áreas a 
        /// habilitar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con el listado de áreas habilitadas</returns>
        [HttpPatch]
        [Route("habilitar")]
        public IActionResult HabilitarAreas([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioArea.HabilitarAreas(datos), bitacoraAccionSistema);
        }
    }
}