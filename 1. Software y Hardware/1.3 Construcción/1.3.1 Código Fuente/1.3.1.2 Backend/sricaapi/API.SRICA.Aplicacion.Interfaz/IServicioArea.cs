using Newtonsoft.Json.Linq;

namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de consultas y operaciones de áreas
    /// </summary>
    public interface IServicioArea
    {
        /// <summary>
        /// Método que obtiene el listado de las áreas registradas, tanto activos como inactivos.
        /// Así mismo, se puede obtener el listado de áreas según una sede, tanto activos como inactivos
        /// </summary>
        /// <param name="codigoSedeEncriptado">Código de la sede (encriptado) (opcional)</param>
        /// <returns>Resultado encriptado con el listado de las áreas encontradas</returns>
        string ObtenerListadoDeAreas(string codigoSedeEncriptado);
        /// <summary>
        /// Método que obtiene un área en base a su código de área
        /// </summary>
        /// <param name="codigoAreaEncriptado">Código del área a obtener (encriptado)</param>
        /// <returns>Resultado encriptado con los datos del área encontrada</returns>
        string ObtenerArea(string codigoAreaEncriptado);
        /// <summary>
        /// Método que guarda una nueva área
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del área a guardar</param>
        /// <returns>Resultado encriptado con los datos del área guardada</returns>
        string GuardarArea(JToken encriptado);
        /// <summary>
        /// Método que modifica un área
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del área a modificar</param>
        /// <returns>Resultado encriptado con los datos del área modificada</returns>
        string ModificarArea(JToken encriptado);
        /// <summary>
        /// Método que inhabilita a las áreas
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las áreas a 
        /// inhabilitar</param>
        /// <returns>Resultado encriptado con el listado de áreas inhabilitadas</returns>
        string InhabilitarAreas(JToken encriptado);
        /// <summary>
        /// Método que habilita a las áreas
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las áreas a 
        /// habilitar</param>
        /// <returns>Resultado encriptado con el listado de áreas habilitadas</returns>
        string HabilitarAreas(JToken encriptado);
    }
}
