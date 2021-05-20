namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz para el servicio de consultas de las acciones del sistema
    /// </summary>
    public interface IServicioAccionSistema
    {
        /// <summary>
        /// Método que obtiene el listado de acciones del sistema
        /// </summary>
        /// <returns>Resultado encriptado con el listado de acciones del sistema encontrados</returns>
        string ObtenerListadoDeAccionesDelSistema();
    }
}
