namespace API.SRICA.Infraestructura.ServicioExterno.Implementacion
{
    /// <summary>
    /// Clase que representa al modelo de petición de los microservicios
    /// </summary>
    public class ModeloPeticionMicroservicio
    {
        /// <summary>
        /// URL de petición
        /// </summary>
        public string URL { get; private set; }
        /// <summary>
        /// Método de petición
        /// </summary>
        public string Metodo { get; private set; }
        /// <summary>
        /// Datos (cuerpo) de petición (serializado a JSON)
        /// </summary>
        public string Datos { get; private set; }
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="url">URL de petición</param>
        /// <param name="metodo">Método de petición</param>
        /// <param name="datos">Datos (cuerpo) de petición (serializado a JSON)</param>
        public ModeloPeticionMicroservicio(string url, string metodo, string datos)
        {
            URL = url;
            Metodo = metodo;
            Datos = datos;
        }
    }
}
