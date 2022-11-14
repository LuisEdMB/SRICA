using Newtonsoft.Json.Linq;

namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de consultas y operaciones de sedes
    /// </summary>
    public interface IServicioSede
    {
        /// <summary>
        /// Método que obtiene el listado de las sedes registradas, tanto activos como inactivos
        /// </summary>
        /// <returns>Resultado encriptado con el listado de las sedes</returns>
        string ObtenerListadoDeSedes();
        /// <summary>
        /// Método que obtiene una sede en base a su código de sede
        /// </summary>
        /// <param name="codigoSedeEncriptado">Código de la sede a obtener (encriptado)</param>
        /// <returns>Resultado encriptado con los datos de la sede encontrada</returns>
        string ObtenerSede(string codigoSedeEncriptado);
        /// <summary>
        /// Método que guarda una nueva sede
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos de la sede a guardar</param>
        /// <returns>Resultado encriptado con los datos de la sede guardada</returns>
        string GuardarSede(JToken encriptado);
        /// <summary>
        /// Método que modifica una sede
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos de la sede a modificar</param>
        /// <returns>Resultado encriptado con los datos de la sede modificada</returns>
        string ModificarSede(JToken encriptado);
        /// <summary>
        /// Método que inhabilita a las sedes
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las sedes a 
        /// inhabilitar</param>
        /// <returns>Resultado encriptado con el listado de sedes inhabilitadas</returns>
        string InhabilitarSedes(JToken encriptado);
        /// <summary>
        /// Método que habilita a las sedes
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las sedes a 
        /// habilitar</param>
        /// <returns>Resultado encriptado con el listado de sedes habilitadas</returns>
        string HabilitarSedes(JToken encriptado);
    }
}
