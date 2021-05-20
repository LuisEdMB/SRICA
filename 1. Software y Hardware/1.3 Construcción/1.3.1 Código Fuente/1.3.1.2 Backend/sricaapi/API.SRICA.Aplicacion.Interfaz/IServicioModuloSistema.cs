namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz para el servicio de consultas de los módulos del sistema
    /// </summary>
    public interface IServicioModuloSistema
    {
        /// <summary>
        /// Método que obtiene el listado de módulos del sistema
        /// </summary>
        /// <returns>Resultado encriptado con el listado de módulos del sistema encontrados</returns>
        string ObtenerListadoDeModulosDelSistema();
    }
}
