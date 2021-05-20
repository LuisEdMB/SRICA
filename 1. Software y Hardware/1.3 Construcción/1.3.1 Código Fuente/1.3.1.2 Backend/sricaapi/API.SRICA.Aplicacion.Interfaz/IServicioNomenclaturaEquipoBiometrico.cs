using Newtonsoft.Json.Linq;

namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de consultas y operaciones de nomenclaturas para equipos biométricos
    /// </summary>
    public interface IServicioNomenclaturaEquipoBiometrico
    {
        /// <summary>
        /// Método que obtiene el listado de nomenclaturas registradas, tanto activos como inactivos
        /// </summary>
        /// <returns>Resultado encriptado con el listado de las nomenclaturas encontradas</returns>
        string ObtenerListadoDeNomenclaturas();
        /// <summary>
        /// Método que obtiene una nomenclatura en base a su código de nomenclatura
        /// </summary>
        /// <param name="codigoNomenclaturaEncriptado">Código de la nomenclatura a obtener 
        /// (encriptado)</param>
        /// <returns>Resultado encriptado con los datos de la nomenclatura encontrada</returns>
        string ObtenerNomenclatura(string codigoNomenclaturaEncriptado);
        /// <summary>
        /// Método que guarda una nueva nomenclatura
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos de la nomenclatura 
        /// a guardar</param>
        /// <returns>Resultado encriptado con los datos de la nomenclatura guardada</returns>
        string GuardarNomenclatura(JToken encriptado);
        /// <summary>
        /// Método que modifica una nomenclatura
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos de la nomenclatura 
        /// a modificar</param>
        /// <returns>Resultado encriptado con los datos de la nomenclatura modificada</returns>
        string ModificarNomenclatura(JToken encriptado);
        /// <summary>
        /// Método que inhabilita a las nomenclaturas
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las nomenclaturas a 
        /// inhabilitar</param>
        /// <returns>Resultado encriptado con el listado de nomenclaturas inhabilitadas</returns>
        string InhabilitarNomenclaturas(JToken encriptado);
        /// <summary>
        /// Método que habilita a las nomenclaturas
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las nomenclaturas a 
        /// habilitar</param>
        /// <returns>Resultado encriptado con el listado de nomenclaturas habilitadas</returns>
        string HabilitarNomenclaturas(JToken encriptado);
    }
}
