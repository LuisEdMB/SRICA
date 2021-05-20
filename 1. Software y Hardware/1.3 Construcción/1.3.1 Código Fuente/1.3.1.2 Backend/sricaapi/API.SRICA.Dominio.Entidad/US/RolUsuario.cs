namespace API.SRICA.Dominio.Entidad.US
{
    /// <summary>
    /// Entidad Rol Usuario que representa a la tabla US_ROL_USUARIO
    /// </summary>
    public class RolUsuario
    {
        /// <summary>
        /// Código interno del rol de usuario (primary key)
        /// </summary>
        public int CodigoRolUsuario { get; private set; }
        /// <summary>
        /// Descripción del rol de usuario
        /// </summary>
        public string DescripcionRolUsuario { get; private set; }
        /// <summary>
        /// Indicador de estado del rol de usuario (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; private set; }
        /// <summary>
        /// Código del rol de usuario "Administrador"
        /// </summary>
        public const int Administrador = 1;
        /// <summary>
        /// Código del rol de usuario "Usuario Básico"
        /// </summary>
        public const int UsuarioBasico = 2;
        /// <summary>
        /// Código del rol de usuario "Sin Rol"
        /// </summary>
        public const int SinRol = 3;
        /// <summary>
        /// Si el usuario es administrador del sistema
        /// </summary>
        public bool EsAdministrador
        {
            get
            {
                return CodigoRolUsuario == Administrador;
            }
        }
        /// <summary>
        /// Si el usuario es usuario básico del sistema
        /// </summary>
        public bool EsUsuarioBasico
        {
            get
            {
                return CodigoRolUsuario == UsuarioBasico;
            }
        }
        /// <summary>
        /// Si el rol de usuario tiene el estado ACTIVO
        /// </summary>
        public bool EsUsuarioActivo
        {
            get
            {
                return IndicadorEstado;
            }
        }
        /// <summary>
        /// Si el rol de usuario tiene el estado INACTIVO
        /// </summary>
        public bool EsUsuarioInactivo
        {
            get
            {
                return !IndicadorEstado;
            }
        }
    }
}
