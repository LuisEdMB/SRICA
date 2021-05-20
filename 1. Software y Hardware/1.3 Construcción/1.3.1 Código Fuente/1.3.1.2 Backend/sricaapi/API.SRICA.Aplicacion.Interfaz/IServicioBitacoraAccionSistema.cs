using Newtonsoft.Json.Linq;

namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de consultas y operaciones de bitácoras de acciones del sistema
    /// </summary>
    public interface IServicioBitacoraAccionSistema
    {
        /// <summary>
        /// Método que guarda una bitácora de acción del sistema
        /// </summary>
        /// <param name="bitacoraAccion">Datos encriptados que contiene los datos de la bitácora 
        /// de acción del sistema a guardar (encriptado)</param>
        /// <param name="validacion">Si el tipo de evento de la acción es "Validación" (por defecto está 
        /// inicializado al valor "FALSE")</param>
        /// <param name="error">Si el tipo de evento de la acción es "Error" (por defecto está 
        /// inicializado al valor "FALSE")</param>
        /// <param name="mensajeValidacion">Mensaje de validación de la acción (por defecto está 
        /// inicializado al valor "")</param>
        /// <param name="mensajeError">Mensaje de error de la acción (por defecto está 
        /// inicializado al valor "")</param>
        /// <returns>Resultado encriptado con los datos de la bitácora guardada</returns>
        string GuardarBitacoraDeAccionDelSistema(JToken bitacoraAccion, bool validacion = false, 
            bool error = false, string mensajeValidacion = "", string mensajeError = "");
        /// <summary>
        /// Método que obtiene el listado de bitácora de acciones del sistema
        /// </summary>
        /// <returns>Resultado encriptado con el listado de bitácora de acciones del sistema
        /// encontrados</returns>
        string ObtenerListadoDeBitacoraDeAccionesDelSistema();
    }
}
