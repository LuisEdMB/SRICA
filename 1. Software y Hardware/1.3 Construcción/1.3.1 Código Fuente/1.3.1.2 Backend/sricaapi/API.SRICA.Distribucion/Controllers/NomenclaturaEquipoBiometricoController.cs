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
    /// Controlador para las consultas y operaciones de nomenclaturas para equipos biométricos
    /// </summary>
    [Route("api/nomenclaturas")]
    [ApiController]
    [Authorize]
    public class NomenclaturaEquipoBiometricoController : BaseController
    {
        /// <summary>
        /// Servicio para consultas y operaciones de nomenclaturas para equipos biométricos
        /// </summary>
        private readonly IServicioNomenclaturaEquipoBiometrico _servicioNomenclaturaEquipoBiometrico;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioNomenclaturaEquipoBiometrico">Servicio para consultas y operaciones de 
        /// nomenclaturas para equipos biométricos</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario obtenido 
        /// desde el token</param>
        public NomenclaturaEquipoBiometricoController(
            IServicioNomenclaturaEquipoBiometrico servicioNomenclaturaEquipoBiometrico,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema, IServicioUsuario servicioUsuario)
            : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioNomenclaturaEquipoBiometrico = servicioNomenclaturaEquipoBiometrico;
        }
        /// <summary>
        /// Método que obtiene el listado de nomenclaturas registradas, tanto activos como inactivos
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con el listado de las nomenclaturas encontradas</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDeNomenclaturas(string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioNomenclaturaEquipoBiometrico.ObtenerListadoDeNomenclaturas(),
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
        /// <summary>
        /// Método que obtiene una nomenclatura en base a su código de nomenclatura
        /// </summary>
        /// <param name="codigoNomenclaturaEncriptado">Código de la nomenclatura a obtener 
        /// (encriptado)</param>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con los datos de la nomenclatura encontrada</returns>
        [HttpGet]
        [Route("{codigoNomenclaturaEncriptado}")]
        public IActionResult ObtenerNomenclatura(string codigoNomenclaturaEncriptado,
            string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioNomenclaturaEquipoBiometrico.ObtenerNomenclatura(
                Uri.UnescapeDataString(codigoNomenclaturaEncriptado)),
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
        /// <summary>
        /// Método que guarda una nueva nomenclatura
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos de la nomenclatura 
        /// a guardar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con los datos de la nomenclatura guardada</returns>
        [HttpPost]
        public IActionResult GuardarNomenclatura([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioNomenclaturaEquipoBiometrico.GuardarNomenclatura(datos),
                bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que modifica una nomenclatura
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos de la nomenclatura 
        /// a modificar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con los datos de la nomenclatura modificada</returns>
        [HttpPatch]
        public IActionResult ModificarNomenclatura([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioNomenclaturaEquipoBiometrico.ModificarNomenclatura(datos),
                bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que inhabilita a las nomenclaturas
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las nomenclaturas a 
        /// inhabilitar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con el listado de nomenclaturas inhabilitadas</returns>
        [HttpPatch]
        [Route("inhabilitar")]
        public IActionResult InhabilitarNomenclaturas([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioNomenclaturaEquipoBiometrico.InhabilitarNomenclaturas(datos),
                bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que habilita a las nomenclaturas
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las nomenclaturas a 
        /// habilitar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con el listado de nomenclaturas habilitadas</returns>
        [HttpPatch]
        [Route("habilitar")]
        public IActionResult HabilitarNomenclaturas([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioNomenclaturaEquipoBiometrico.HabilitarNomenclaturas(datos),
                bitacoraAccionSistema);
        }
    }
}