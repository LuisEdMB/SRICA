namespace API.SRICA.Aplicacion.DTO
{
    /// <summary>
    /// DTO para el rol de usuario
    /// </summary>
    public class DtoRolUsuario
    {
        /// <summary>
        /// Código del rol de usuario
        /// </summary>
        public string CodigoRolUsuario { get; set; }
        /// <summary>
        /// Descripción del rol de usuario
        /// </summary>
        public string DescripcionRolUsuario { get; set; }
        /// <summary>
        /// Indicador de estado del rol de usuario (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; set; }
    }
}
