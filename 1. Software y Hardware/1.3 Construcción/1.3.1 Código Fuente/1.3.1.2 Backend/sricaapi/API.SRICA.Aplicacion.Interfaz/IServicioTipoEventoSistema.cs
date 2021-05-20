namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz para el servicio de consultas de los tipos de eventos del sistema
    /// </summary>
    public interface IServicioTipoEventoSistema
    {
        /// <summary>
        /// Método que obtiene el listado de tipos de eventos del sistema
        /// </summary>
        /// <returns>Resultado encriptado con el listado de tipos de eventos del sistema 
        /// encontrados</returns>
        string ObtenerListadoDeTiposDeEventosDelSistema();
    }
}
