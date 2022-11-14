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
    /// Controlador para las consultas y operaciones de equipo biométricos
    /// </summary>
    [Route("api/equipos-biometricos")]
    [ApiController]
    [Authorize]
    public class EquipoBiometricoController : BaseController
    {
        /// <summary>
        /// Servicio para consultas y operaciones de equipos biométricos
        /// </summary>
        private readonly IServicioEquipoBiometrico _servicioEquipoBiometrico;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEquipoBiometrico">Servicio para consultas y operaciones de equipos 
        /// biométricos</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario obtenido 
        /// desde el token</param>
        public EquipoBiometricoController(IServicioEquipoBiometrico servicioEquipoBiometrico,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema, IServicioUsuario servicioUsuario)
            : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioEquipoBiometrico = servicioEquipoBiometrico;
        }
        /// <summary>
        /// Método que obtiene el listado de los equipos biométricos registrados (tanto
        /// activos como inactivos)
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <param name="guardarBitacora">Si se desea guardar la bitacora</param>
        /// <returns>Resultado encriptado con el listado de equipos biométricos 
        /// registrados encontrados</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDeEquiposBiometricos(string bitacoraAccionSistema = "",
            bool guardarBitacora = false)
            => Ejecutar(() => _servicioEquipoBiometrico.ObtenerListadoDeEquiposBiometricosRegistrados(),
                Uri.UnescapeDataString(bitacoraAccionSistema), guardarBitacora);
        /// <summary>
        /// Método que obtiene un equipo biométrico en base a su código de equipo biométrico
        /// </summary>
        /// <param name="codigoEquipoBiometricoEncriptado">Código del equipo biométrico a obtener 
        /// (encriptado)</param>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con los datos del equipo biométrico encontrado</returns>
        [HttpGet]
        [Route("{codigoEquipoBiometricoEncriptado}")]
        public IActionResult ObtenerEquipoBiometrico(string codigoEquipoBiometricoEncriptado,
            string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioEquipoBiometrico.ObtenerEquipoBiometrico(
                Uri.UnescapeDataString(codigoEquipoBiometricoEncriptado)),
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
        /// <summary>
        /// Método que guarda un nuevo equipo biométrico
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del equipo biométrico 
        /// a guardar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con los datos del equipo biométrico guardado</returns>
        [HttpPost]
        public IActionResult GuardarEquipoBiometrico([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioEquipoBiometrico.GuardarEquipoBiometrico(datos),
                bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que modifica un equipo biométrico
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del equipo biométrico 
        /// a modificar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con los datos del equipo biométrico modificado</returns>
        [HttpPatch]
        public IActionResult ModificarEquipoBiometrico([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioEquipoBiometrico.ModificarEquipoBiometrico(datos),
                bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que inhabilita a los equipos biométricos
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de los equipos biométricos a 
        /// inhabilitar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con el listado de equipos biométricos inhabilitados</returns>
        [HttpPatch]
        [Route("inhabilitar")]
        public IActionResult InhabilitarEquiposBiometricos([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioEquipoBiometrico.InhabilitarEquiposBiometricos(datos), 
                bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que habilita a los equipos biométricos
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de los equipos biométricos a 
        /// habilitar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con el listado de equipos biométricos habilitados</returns>
        [HttpPatch]
        [Route("habilitar")]
        public IActionResult HabilitarEquiposBiometricos([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioEquipoBiometrico.HabilitarEquiposBiometricos(datos),
                bitacoraAccionSistema);
        }

        /// <summary>
        /// Método que se comunica con el equipo biométrico para abrir la puerta de acceso
        /// </summary>
        /// <param name="codigoEquipoBiometricoEncriptado">Código encriptado del equipo biométrico</param>
        /// <param name="encriptado">Datos encriptados que contiene los datos de bitácora
        /// de acción correspondiente</param>
        /// <returns>Resultado encriptado con el éxito o fracaso de la operación</returns>
        [HttpPatch]
        [Route("{codigoEquipoBiometricoEncriptado}/aperturas-puertas")]
        public IActionResult AbrirPuertaDeAccesoDelEquipoBiometrico(string codigoEquipoBiometricoEncriptado,
            [FromBody]JObject encriptado)
        {
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioEquipoBiometrico.AbrirPuertaDeAccesoDelEquipoBiometrico(
                    codigoEquipoBiometricoEncriptado), bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que envía una señal el equipo biométrico (pitidos)
        /// </summary>
        /// <param name="codigoEquipoBiometricoEncriptado">Código encriptado del equipo biométrico</param>
        /// <param name="encriptado">Datos encriptados que contiene los datos de bitácora
        /// de acción correspondiente</param>
        /// <returns>Resultado encriptado con el éxito o fracaso de la operación</returns>
        [HttpPatch]
        [Route("{codigoEquipoBiometricoEncriptado}/senales")]
        public IActionResult EnviarSenalAEquipoBiometrico(string codigoEquipoBiometricoEncriptado,
            [FromBody] JObject encriptado)
        {
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioEquipoBiometrico.EnviarSenalAEquipoBiometrico(
                codigoEquipoBiometricoEncriptado), bitacoraAccionSistema);
        }
    }
}