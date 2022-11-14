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
    /// Controlador para las consultas y operaciones de personal de la empresa
    /// </summary>
    [Route("api/personal-empresa")]
    [ApiController]
    [Authorize]
    public class PersonalEmpresaController : BaseController
    {
        /// <summary>
        /// Servicio para consultas y operaciones de personal de la empresa
        /// </summary>
        private readonly IServicioPersonalEmpresa _servicioPersonalEmpresa;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioPersonalEmpresa">Servicio para consultas y operaciones de personal 
        /// de la empresa</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario obtenido 
        /// desde el token</param>
        public PersonalEmpresaController(IServicioPersonalEmpresa servicioPersonalEmpresa,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema, IServicioUsuario servicioUsuario)
            : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioPersonalEmpresa = servicioPersonalEmpresa;
        }
        /// <summary>
        /// Método que obtiene el listado del personal de la empresa registrado, tanto activos 
        /// como inactivos
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <param name="guardarBitacora">Si se desea guardar la bitacora</param>
        /// <returns>Resultado encriptado con el listado del personal de la empresa</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDePersonalDeLaEmpresa(string bitacoraAccionSistema = "",
            bool guardarBitacora = false)
            => Ejecutar(() => _servicioPersonalEmpresa.ObtenerListadoDePersonalDeLaEmpresa(),
                Uri.UnescapeDataString(bitacoraAccionSistema), guardarBitacora);
        /// <summary>
        /// Método que obtiene un personal de la empresa en base a su código de personal
        /// de la empresa
        /// </summary>
        /// <param name="codigoPersonalEmpresaEncriptado">Código del personal de la empresa 
        /// a obtener (encriptado)</param>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <returns>Resultado encriptado con los datos del personal de la empresa encontrado</returns>
        [HttpGet]
        [Route("{codigoPersonalEmpresaEncriptado}")]
        public IActionResult ObtenerPersonalDeLaEmpresa(string codigoPersonalEmpresaEncriptado,
            string bitacoraAccionSistema = "")
            => Ejecutar(() => _servicioPersonalEmpresa.ObtenerPersonalDeLaEmpresa(
                Uri.UnescapeDataString(codigoPersonalEmpresaEncriptado)),
                Uri.UnescapeDataString(bitacoraAccionSistema), false);
        /// <summary>
        /// Método que guarda un personal de la empresa, o registra masivamente al personal en base
        /// a un archivo
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del personal de 
        /// la empresa a guardar, o el archivo con el listado del personal a guardar. 
        /// Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con los datos del personal de la empresa guardado(s)</returns>
        [HttpPost]
        public IActionResult GuardarPersonalEmpresa([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioPersonalEmpresa.GuardarPersonalEmpresa(datos),
                bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que modifica un personal de la empresa
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del personal de 
        /// la empresa a modificar. Así mismo, contiene los datos de bitácora de acción 
        /// correspondiente</param>
        /// <returns>Resultado encriptado con los datos del personal de la empresa modificado</returns>
        [HttpPatch]
        public IActionResult ModificarPersonalEmpresa([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioPersonalEmpresa.ModificarPersonalEmpresa(datos),
                bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que inhabilita un listado de personal de la empresa
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado del personal de la empresa
        /// a inhabilitar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con el listado del personal de la empresa inhabilitados</returns>
        [HttpPatch]
        [Route("inhabilitar")]
        public IActionResult InhabilitarListadoDePersonalEmpresa([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioPersonalEmpresa.InhabilitarListadoDePersonalEmpresa(datos),
                bitacoraAccionSistema);
        }
        /// <summary>
        /// Método que habilita un listado de personal de la empresa
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado del personal de la empresa
        /// a habilitar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con el listado del personal de la empresa habilitados</returns>
        [HttpPatch]
        [Route("habilitar")]
        public IActionResult HabilitarListadoDePersonalEmpresa([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioPersonalEmpresa.HabilitarListadoDePersonalEmpresa(datos),
                bitacoraAccionSistema);
        }
    }
}