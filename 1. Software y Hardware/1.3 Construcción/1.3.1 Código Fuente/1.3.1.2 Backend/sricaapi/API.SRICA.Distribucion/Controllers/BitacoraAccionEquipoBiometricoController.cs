using API.SRICA.Aplicacion.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.SRICA.Distribucion.Controllers
{
    /// <summary>
    /// Controlador para las consultas y operaciones de bitácoras de acción de equipos biométricos
    /// </summary>
    [Route("api/bitacoras/acciones-equipos-biometricos")]
    [ApiController]
    [Authorize]
    public class BitacoraAccionEquipoBiometricoController : BaseController
    {
        /// <summary>
        /// Servicio para consultas y operaciones de bitácoras de acciones de equipos biométricos
        /// </summary>
        private readonly IServicioBitacoraAccionEquipoBiometrico _servicioBitacoraAccionEquipoBiometrico;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioBitacoraAccionEquipoBiometrico">Servicio para consultas y operaciones 
        /// de bitácoras de acciones de equipos biométricos</param>
        /// <param name="servicioBitacoraAccionSistema">Servicio para consultas y operaciones de 
        /// bitácoras de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario obtenido 
        /// desde el token</param>
        public BitacoraAccionEquipoBiometricoController(
            IServicioBitacoraAccionEquipoBiometrico servicioBitacoraAccionEquipoBiometrico,
            IServicioBitacoraAccionSistema servicioBitacoraAccionSistema,
            IServicioUsuario servicioUsuario)
            : base(servicioBitacoraAccionSistema, servicioUsuario)
        {
            _servicioBitacoraAccionEquipoBiometrico = servicioBitacoraAccionEquipoBiometrico;
        }
        /// <summary>
        /// Método que obtiene el listado de bitácora de acciones de equipos biométricos
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema (encriptado)</param>
        /// <param name="guardarBitacora">Si se desea guardar la bitacora</param>
        /// <returns>Resultado encriptado con el listado de bitácora de acciones de equipos biométricos
        /// encontrados</returns>
        [HttpGet]
        public IActionResult ObtenerListadoDeBitacoraDeAccionesDeEquiposBiometricos(
            string bitacoraAccionSistema = "", bool guardarBitacora = false)
            => Ejecutar(() => _servicioBitacoraAccionEquipoBiometrico
                .ObtenerListadoDeBitacoraDeAccionesDeEquiposBiometricos(),
                Uri.UnescapeDataString(bitacoraAccionSistema), guardarBitacora);
    }
}
