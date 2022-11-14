using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Dominio.Excepcion;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Security.Claims;

namespace API.SRICA.Distribucion.Controllers
{
    /// <summary>
    /// Controlador base
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Servicio para operaciones de bitácoras de acciones del sistema
        /// </summary>
        private readonly IServicioBitacoraAccionSistema _servicioBitacoraAccionSistema;
        /// <summary>
        /// Servicio para operaciones de bitácora de acciones del equipo biométrico
        /// </summary>
        private readonly IServicioBitacoraAccionEquipoBiometrico _servicioBitacoraAccionEquipoBiometrico;
        /// <summary>
        /// Servicio para verificar el estado del usuario obtenido desde el token
        /// </summary>
        private readonly IServicioUsuario _servicioUsuario;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioBitacoraAccionSistema">Servicio para operaciones de bitácoras 
        /// de acciones del sistema</param>
        /// <param name="servicioUsuario">Servicio para verificar el estado del usuario 
        /// obtenido desde el token</param>
        public BaseController(IServicioBitacoraAccionSistema servicioBitacoraAccionSistema,
            IServicioUsuario servicioUsuario)
        {
            _servicioBitacoraAccionSistema = servicioBitacoraAccionSistema;
            _servicioUsuario = servicioUsuario;
        }
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioBitacoraAccionEquipoBiometrico">Servicio para operaciones de bitácora
        /// de acciones del equipo biométrico</param>
        public BaseController(IServicioBitacoraAccionEquipoBiometrico servicioBitacoraAccionEquipoBiometrico)
        {
            _servicioBitacoraAccionEquipoBiometrico = servicioBitacoraAccionEquipoBiometrico;
        }
        /// <summary>
        /// Método para la ejecución de las consultas u operaciones
        /// </summary>
        /// <typeparam name="T">T acción</typeparam>
        /// <param name="accion">Acción a realizar</param>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema</param>
        /// <param name="guardarBitacoraAccionSistemaExitoso">Si se desea guardar la bitácora 
        /// de acción del sistema cuando ésta sea exitosa (por defecto está inicializado 
        /// con el valor TRUE)</param>
        /// <returns>Resultado de la acción realizada</returns>
        public IActionResult Ejecutar<T>(Func<T> accion, JToken bitacoraAccionSistema,
            bool guardarBitacoraAccionSistemaExitoso = true)
        {
            T respuesta;
            object resultadoExito;
            try
            {
                VerificarUsuarioDelTokenActivo();
                respuesta = accion();
                resultadoExito = new
                {
                    Datos = respuesta,
                    Error = false,
                    Validacion = false,
                    Mensaje = ""
                };
            }
            catch(ExcepcionAplicacionPersonalizada excepcion)
            {
                if (!string.IsNullOrEmpty(bitacoraAccionSistema.ToString()))
                    _servicioBitacoraAccionSistema.GuardarBitacoraDeAccionDelSistema(bitacoraAccionSistema, 
                        true, false, $"{excepcion.Message}. {excepcion.InnerException?.Message}");
                return Ok(new
                {
                    Datos = "",
                    Error = false,
                    Validacion = true,
                    Mensaje = excepcion.Message,
                    excepcion.CodigoExcepcion
                });
            }
            catch (ExcepcionPersonalizada excepcion)
            {
                if (!string.IsNullOrEmpty(bitacoraAccionSistema.ToString()))
                    _servicioBitacoraAccionSistema.GuardarBitacoraDeAccionDelSistema(bitacoraAccionSistema,
                        false, true, string.Empty, 
                        $"{excepcion.Message}. {excepcion.InnerException?.Message}");
                return Ok(new
                {
                    Datos = "",
                    Error = true,
                    Validacion = false,
                    Mensaje = excepcion.Message,
                    excepcion.CodigoExcepcion
                });
            }
            catch (Exception excepcion)
            {
                if (!string.IsNullOrEmpty(bitacoraAccionSistema.ToString()))
                    _servicioBitacoraAccionSistema.GuardarBitacoraDeAccionDelSistema(bitacoraAccionSistema,
                        false, true, string.Empty, 
                        $"{excepcion.Message}. {excepcion.InnerException?.Message}");
                return Ok(new
                {
                    Datos = "",
                    Error = true,
                    Validacion = false,
                    Mensaje = excepcion.Message
                });
            }

            try
            {
                if (!string.IsNullOrEmpty(bitacoraAccionSistema.ToString()) &&
                    guardarBitacoraAccionSistemaExitoso)
                    _servicioBitacoraAccionSistema.GuardarBitacoraDeAccionDelSistema(bitacoraAccionSistema);
            }
            catch
            {
                // ignored
            }
            return Ok(resultadoExito);
        }
        /// <summary>
        /// Método para la ejecución de las consultas u operaciones procedentes del equipo biométrico
        /// </summary>
        /// <typeparam name="T">T acción</typeparam>
        /// <param name="accion">Acción a realizar</param>
        /// <returns>Resultado de la acción realizada</returns>
        public IActionResult Ejecutar<T>(Func<T> accion)
        {
            T respuesta;
            object resultadoExito;
            try
            {
                respuesta = accion();
                resultadoExito = new
                {
                    Datos = respuesta,
                    Mensaje = "",
                    CodigoExcepcion = ""
                };
            }
            catch (ExcepcionAplicacionEquipoBiometricoPersonalizada excepcion)
            {
                _servicioBitacoraAccionEquipoBiometrico.GuardarBitacoraDeAccionDelEquipoBiometrico(
                    excepcion.PersonalEmpresaExcepcion, excepcion.EquipoBiometricoExcepcion,
                    excepcion.EsExcepcionPorAccesoDenegadoConFoto,
                    excepcion.EsExcepcionPorAccesoDenegadoSinFoto,
                    $"{excepcion.Message}. {excepcion.InnerException?.Message}",
                    excepcion.FotoPersonalNoRegistrado);
                return Ok(new
                {
                    Datos = "",
                    Mensaje = excepcion.Message,
                    excepcion.CodigoExcepcion
                });
            }
            catch (ExcepcionEquipoBiometricoPersonalizada excepcion)
            {
                _servicioBitacoraAccionEquipoBiometrico.GuardarBitacoraDeAccionDelEquipoBiometrico(
                    excepcion.PersonalEmpresaExcepcion, excepcion.EquipoBiometricoExcepcion,
                    false, false, $"{excepcion.Message}. {excepcion.InnerException?.Message}", 
                    string.Empty);
                return Ok(new
                {
                    Datos = "",
                    Mensaje = excepcion.Message,
                    excepcion.CodigoExcepcion
                });
            }
            catch (Exception excepcion)
            {
                _servicioBitacoraAccionEquipoBiometrico.GuardarBitacoraDeAccionDelEquipoBiometrico(
                    null, null, false, false, 
                    $"{excepcion.Message}. {excepcion.InnerException?.Message}", string.Empty);
                return Ok(new
                {
                    Datos = "",
                    Mensaje = excepcion.Message,
                    CodigoExcepcion = "0"
                });
            }

            try
            {
                _servicioBitacoraAccionEquipoBiometrico.GuardarBitacoraDeAccionDelEquipoBiometrico(
                    respuesta, null, false, false, string.Empty, string.Empty);
            }
            catch
            {
                // ignored
            }
            return Ok(resultadoExito);
        }
        #region Métodos privados
        /// <summary>
        /// Método que verifica que el usuario autenticado (claims token) se encuentre en estado
        /// activo
        /// </summary>
        private void VerificarUsuarioDelTokenActivo()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            var usuario = claims.Claims.Where(g => g.Type.Equals("Usuario")).FirstOrDefault();
            if (_servicioUsuario != null)
                _servicioUsuario.VerificarUsuarioDelTokenActivo(usuario);
        }
        #endregion
    }
}