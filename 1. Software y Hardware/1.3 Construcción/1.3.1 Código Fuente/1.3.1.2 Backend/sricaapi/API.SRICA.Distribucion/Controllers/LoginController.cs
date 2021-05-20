using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Distribucion.VariableConstante;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace API.SRICA.Distribucion.Controllers
{
    /// <summary>
    /// Controlador para el inicio de sesión de los usuarios del sistema
    /// </summary>
    [Route("api/token")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : BaseController
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
        public LoginController(IServicioAutenticacion servicioAutenticacion,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema) 
            : base(servicioBitacoraAccionSistema, null)
        {
            _servicioAutenticacion = servicioAutenticacion;
        }
        /// <summary>
        /// Método que comprueba el usuario y contraseña del usuario que intenta
        /// iniciar sesión en el sistema
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el usuario,
        /// contraseña y audiencia permitida para el inicio de sesión del usuario. Así mismo,
        /// contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado del proceso de inicio de sesión</returns>
        [HttpPost]
        public IActionResult IniciarSesion([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g => 
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g => 
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioAutenticacion.ComprobarUsuario(datos), bitacoraAccionSistema);
        }
    }
}