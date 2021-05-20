namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de consultas de roles de usuario
    /// </summary>
    public interface IServicioRolUsuario
    {
        /// <summary>
        /// Método que obtiene el listado de roles de usuario, tanto activos como inactivos
        /// </summary>
        /// <returns>Resultado encriptado con el listado de los roles de usuario</returns>
        string ObtenerListadoDeRolesDeUsuario();
    }
}
