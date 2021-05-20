using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Distribucion.VariableConstante;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace API.SRICA.Distribucion.Controllers
{
    /// <summary>
    /// Controlador para el refresco del token del usuario
    /// </summary>
    [Route("api/token-refresco")]
    [ApiController]
    [AllowAnonymous]
    public class LoginRefrescoController : BaseController
    {
        /// <summary>
        /// Servicio de autenticación del usuario del sistema
        /// </summary>
        private readonly IServicioAutenticacion _servicioAutenticacion;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioAutenticacion">Servicio de autenticación del usuario 
        /// del sistema</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        public LoginRefrescoController(IServicioAutenticacion servicioAutenticacion,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema)
            : base(servicioBitacoraAccionSistema, null)
        {
            _servicioAutenticacion = servicioAutenticacion;
        }
        /// <summary>
        /// Método que refresca el token del usuario
        /// </summary>
        /// <param name="encriptado">Datos encriptado que contiene el token expirado para generar 
        /// el nuevo token</param>
        /// <returns>Resultado encriptado del proceso de inicio de sesión</returns>
        [HttpPost]
        public IActionResult RefrescarTokenDelUsuario([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioAutenticacion.RefrescarTokenDelUsuario(datos), string.Empty, 
                false);
        }
    }
}