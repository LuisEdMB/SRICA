namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz para el servicio de consultas de los resultados (tipos) de acceso de los equipos biométricos
    /// </summary>
    public interface IServicioResultadoAcceso
    {
        /// <summary>
        /// Método que obtiene el listado de resultados (tipos) de acceso de los equipos biométricos
        /// </summary>
        /// <returns>Resultado encriptado con el listado de resultados (tipos) de acceso de los 
        /// equipos biométricos encontrados</returns>
        string ObtenerListadoDeResultadosDeAcceso();
    }
}
