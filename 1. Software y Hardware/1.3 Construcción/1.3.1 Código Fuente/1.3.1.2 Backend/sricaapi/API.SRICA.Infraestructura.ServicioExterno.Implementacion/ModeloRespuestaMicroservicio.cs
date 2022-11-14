namespace API.SRICA.Infraestructura.ServicioExterno.Implementacion
{
    /// <summary>
    /// Clase que representa al modelo de respuesta de los microservicios
    /// </summary>
    public class ModeloRespuestaMicroservicio
    {
        /// <summary>
        /// Datos de respuesta
        /// </summary>
        public object Datos { get; private set; }
        /// <summary>
        /// Error de respuesta
        /// </summary>
        public bool Error { get; private set; }
        /// <summary>
        /// Mensaje de respuesta
        /// </summary>
        public string Mensaje { get; private set; }
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="datos">Datos de respuesta</param>
        /// <param name="error">Error de respuesta</param>
        /// <param name="mensaje">Mensaje de respuesta</param>
        public ModeloRespuestaMicroservicio(object datos, bool error, string mensaje)
        {
            Datos = datos;
            Error = error;
            Mensaje = mensaje;
        }
    }
}
