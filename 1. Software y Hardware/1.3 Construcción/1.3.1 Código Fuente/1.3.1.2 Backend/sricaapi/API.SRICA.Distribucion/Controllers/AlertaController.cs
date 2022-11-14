using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Distribucion.VariableConstante;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace API.SRICA.Distribucion.Controllers
{
    /// <summary>
    /// Controlador para el envío de alertas (correos electrónicos)
    /// </summary>
    [Route("api/alertas")]
    [ApiController]
    [AllowAnonymous]
    public class AlertaController : BaseController
    {
        /// <summary>
        /// Servicio para el envío de alertas (envío de correos electrónicos)
        /// </summary>
        private readonly IServicioAlerta _servicioAlerta;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioAlerta">Servicio para el envío de alertas (envío de correos 
        /// electrónicos)</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones 
        /// de bitácoras de acciones del sistema</param>
        public AlertaController(IServicioAlerta servicioAlerta,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema)
            : base(servicioBitacoraAccionSistema, null)
        {
            _servicioAlerta = servicioAlerta;
        }
        /// <summary>
        /// Método que envía la alerta (correo electrónico) al usuario para que pueda recuperar su contraseña
        /// olvidada
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el usuario a quien se le enviará la 
        /// alerta. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con el resultado del proceso de envío de alerta</returns>
        [HttpPost]
        [Route("contrasenas-olvidadas")]
        public IActionResult EnviarAlertaDeRecuperacionDeContrasenaOlvidada([FromBody]JObject encriptado)
        {
            var datos = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.Datos)).FirstOrDefault().Value;
            var bitacoraAccionSistema = encriptado.Properties().Where(g =>
                g.Name.Equals(Constante.BitacoraAccionSistema)).FirstOrDefault().Value;
            return Ejecutar(() => _servicioAlerta.EnviarAlertaDeRecuperacionDeContrasenaOlvidada(datos), 
                bitacoraAccionSistema);
        }
    }
}