namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz para el servicio de consultas de los recursos del sistema
    /// </summary>
    public interface IServicioRecursoSistema
    {
        /// <summary>
        /// Método que obtiene el listado de recursos del sistema
        /// </summary>
        /// <returns>Resultado encriptado con el listado de recursos del sistema encontrados</returns>
        string ObtenerListadoDeRecursosDelSistema();
    }
}
