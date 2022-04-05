using System.Linq;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Distribucion.VariableConstante;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace API.SRICA.Distribucion.Controllers
{
    /// <summary>
    /// Controlador para las operaciones con el iris humano
    /// </summary>
    [Route("api/iris")]
    [ApiController]
    public class IrisController : BaseController
    {
        /// <summary>
        /// Servicio para los procesos de imágenes de iris
        /// </summary>
        private readonly IServicioIris _servicioIris;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioIris">Servicio para los procesos de imágenes de iris</param>
        /// <param name="servicioBitacoraAccionEquipoBiometrico">Servicio para operaciones de bitácora
        /// de acciones del equipo biométrico</param>
        public IrisController(IServicioIris servicioIris,
            IServicioBitacoraAccionEquipoBiometrico servicioBitacoraAccionEquipoBiometrico)
            : base(servicioBitacoraAccionEquipoBiometrico)
        {
            _servicioIris = servicioIris;
        }
        /// <summary>
        /// Método que procesa el iris para reconocer al personal, desde el equipo biométrico
        /// </summary>
        /// <param name="encriptado">Objeto encriptado que contiene las imágenes de iris a utilizar
        /// en el proceso de reconocimiento, y la MAC del equipo biométrico</param>
        /// <returns>Resultado encriptado con el código del personal reconocido</returns>
        [Route("equipos-biometricos/reconocimientos")]
        [HttpPost]
        public IActionResult ReconocerPersonalPorElIrisViaEquipoBiometrico([FromBody] JObject encriptado)
        {
            var datos = encriptado.Properties().FirstOrDefault(g => 
                g.Name.Equals(Constante.Datos)).Value;
            return Ejecutar(() => _servicioIris.ReconocerPersonalPorElIrisViaEquipoBiometrico(datos));
        }
        /// <summary>
        /// Método que procesa el iris para reconocer al personal (con token), desde la web
        /// </summary>
        /// <param name="encriptado">Objeto encriptado que contiene las imágenes de iris a utilizar
        /// en el proceso de reconocimiento</param>
        /// <returns>Resultado encriptado con el código del personal reconocido</returns>
        [Authorize]
        [Route("web/reconocimientos")]
        [HttpPost]
        public IActionResult ReconocerPersonalPorElIrisViaWeb([FromBody] JObject encriptado)
        {
            var datos = encriptado.Properties().FirstOrDefault(g => 
                g.Name.Equals(Constante.Datos)).Value;
            return Ejecutar(() => _servicioIris.ReconocerPersonalPorElIrisViaWeb(datos));
        }
    }
}