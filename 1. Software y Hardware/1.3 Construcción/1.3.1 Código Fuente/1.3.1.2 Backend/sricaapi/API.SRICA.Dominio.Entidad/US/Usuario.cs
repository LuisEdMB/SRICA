namespace API.SRICA.Dominio.Entidad.US
{
    /// <summary>
    /// Entidad usuario que representa a la tabla US_USUARIO
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Código interno del usuario (primary key)
        /// </summary>
        public int CodigoUsuario { get; private set; }
        /// <summary>
        /// Código del rol asignado al usuario (relación con US_ROL_USUARIO)
        /// </summary>
        public int CodigoRolUsuario { get; private set; }
        /// <summary>
        /// Usuario de acceso al sistema
        /// </summary>
        public string UsuarioAcceso { get; private set; }
        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        public string ContrasenaAcceso { get; private set; }
        /// <summary>
        /// Nombres del usuario
        /// </summary>
        public string NombreUsuario { get; private set; }
        /// <summary>
        /// Apellidos del usuario
        /// </summary>
        public string ApellidoUsuario { get; private set; }
        /// <summary>
        /// Correo electrónico del usuario
        /// </summary>
        public string CorreoElectronico { get; private set; }
        /// <summary>
        /// Si el correo electrónico aún tiene el valor por defecto asignado
        /// </summary>
        public bool EsCorreoElectronicoPorDefecto { get; private set; }
        /// <summary>
        /// Si la contraseña del usuario aún tiene el valor por defecto asignado
        /// </summary>
        public bool EsContrasenaPorDefecto { get; private set; }
        /// <summary>
        /// Indicador de estado del usuario (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; private set; }
        /// <summary>
        /// Rol de usuario relacionado con el usuario
        /// </summary>
        public virtual RolUsuario RolUsuario { get; private set; }
        /// <summary>
        /// Contraseña encriptada por defecto para los usuarios (123.-SRICa)
        /// </summary>
        public const string ContrasenaPorDefecto = "123.-SRICa";
        /// <summary>
        /// Correo electrónico por defecto cuando se crea un nuevo usuario (srica@cambiarcorreo.com)
        /// </summary>
        private const string CorreoElectronicoPorDefecto = "srica@cambiarcorreo.com";
        /// <summary>
        /// Listado de los niveles de fortaleza para la contraseña del usuario
        /// </summary>
        public enum EnumNivelFortalezaContrasena
        {
            Bajo,
            MedioBajo,
            Medio,
            MedioAlto,
            Alto
        }
        /// <summary>
        /// Si el usuario tiene el estado ACTIVO
        /// </summary>
        public bool EsUsuarioActivo
        {
            get
            {
                return IndicadorEstado;
            }
        }
        /// <summary>
        /// Si el usuario tiene el estado INACTIVO
        /// </summary>
        public bool EsUsuarioInactivo
        {
            get
            {
                return !IndicadorEstado;
            }
        }
        /// <summary>
        /// Método estático que crea la entidad usuario
        /// </summary>
        /// <param name="rolUsuario">Rol de usuario del usuario</param>
        /// <param name="usuario">Usuario</param>
        /// <param name="contrasenaPorDefectoEncriptada">Contraseña (encriptada) por defecto a 
        /// asignar al usuario</param>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="apellido">Apellido del usuario</param>
        /// <returns>Usuario creado</returns>
        public static Usuario CrearUsuario(RolUsuario rolUsuario, string usuario, 
            string contrasenaPorDefectoEncriptada, string nombre, string apellido)
        {
            return new Usuario
            {
                CodigoRolUsuario = rolUsuario.CodigoRolUsuario,
                UsuarioAcceso = usuario,
                ContrasenaAcceso = contrasenaPorDefectoEncriptada,
                NombreUsuario = nombre,
                ApellidoUsuario = apellido,
                CorreoElectronico = CorreoElectronicoPorDefecto,
                EsContrasenaPorDefecto = true,
                EsCorreoElectronicoPorDefecto = true,
                IndicadorEstado = true
            };
        }
        /// <summary>
        /// Método que modifica la entidad usuario
        /// </summary>
        /// <param name="rolUsuario">Rol de usuario del usuario a modificar</param>
        /// <param name="usuario">Usuario</param>
        /// <param name="contrasenaPorDefectoEncriptada">Contraseña (encriptada) por defecto a 
        /// asignar al usuario</param>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="apellido">Apellido del usuario</param>
        /// <param name="contrasenaPorDefecto">Si se debe generar la contraseña por defecto</param>
        public void ModificarUsuario(RolUsuario rolUsuario, string usuario, 
            string contrasenaPorDefectoEncriptada, string nombre, string apellido, 
            bool contrasenaPorDefecto)
        {
            CodigoRolUsuario = rolUsuario.CodigoRolUsuario;
            UsuarioAcceso = usuario;
            NombreUsuario = nombre;
            ApellidoUsuario = apellido;
            if (contrasenaPorDefecto)
            {
                ContrasenaAcceso = contrasenaPorDefectoEncriptada;
                EsContrasenaPorDefecto = true;
            }
        }
        /// <summary>
        /// Método que modifica el correo electrónico, contraseña, y los estados por defecto de los
        /// mismos
        /// del usuario
        /// </summary>
        /// <param name="correoElectronico">Correo electrónico a modificar</param>
        /// <param name="contrasena">Contraseña a modificar</param>
        public void ModificarCorreoYContrasena(string correoElectronico, string contrasena)
        {
            CorreoElectronico = correoElectronico;
            ContrasenaAcceso = contrasena;
            EsContrasenaPorDefecto = false;
            EsCorreoElectronicoPorDefecto = false;
        }
        /// <summary>
        /// Método que modifica solo la contraseña, y el estado por defecto del mismo
        /// </summary>
        /// <param name="contrasena">Contraseña a modificar</param>
        public void ModificarSoloContrasena(string contrasena)
        {
            ContrasenaAcceso = contrasena;
            EsContrasenaPorDefecto = false;
        }
        /// <summary>
        /// Método que modifica solo el correo electrónico, y el estado por defecto del mismo
        /// </summary>
        /// <param name="correoElectronico">Correo electrónico a modificar</param>
        public void ModificarSoloCorreoElectronico(string correoElectronico)
        {
            CorreoElectronico = correoElectronico;
            EsCorreoElectronicoPorDefecto = false;
        }
        /// <summary>
        /// Método que inhabilita la entidad usuario
        /// </summary>
        public void InhabilitarUsuario()
        {
            IndicadorEstado = false;
        }
        /// <summary>
        /// Método que habilita la entidad usuario
        /// </summary>
        public void HabilitarUsuario()
        {
            IndicadorEstado = true;
        }
    }
}
