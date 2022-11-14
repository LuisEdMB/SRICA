using System.Collections.Generic;

namespace API.SRICA.Aplicacion.DTO
{
    /// <summary>
    /// DTO para el usuario
    /// </summary>
    public class DtoUsuario
    {
        /// <summary>
        /// Código del usuario
        /// </summary>
        public string CodigoUsuario { get; set; }
        /// <summary>
        /// Usuario de acceso
        /// </summary>
        public string Usuario { get; set; }
        /// <summary>
        /// Contraseña del usuario de acceso
        /// </summary>
        public string Contrasena { get; set; }
        /// <summary>
        /// Confirmar contraseña del usuario
        /// </summary>
        public string ConfirmarContrasena { get; set; }
        /// <summary>
        /// Correo electrónico del usuario
        /// </summary>
        public string CorreoElectronico { get; set; }
        /// <summary>
        /// Audiencia permitida
        /// </summary>
        public string AudienciaPermitida { get; set; }
        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Apellido del usuario
        /// </summary>
        public string Apellido { get; set; }
        /// <summary>
        /// Código de rol de usuario
        /// </summary>
        public string CodigoRolUsuario { get; set; }
        /// <summary>
        /// Descripción del rol del usuario
        /// </summary>
        public string RolUsuario { get; set; }
        /// <summary>
        /// Si el usuario es administrador del sistema
        /// </summary>
        public bool EsAdministrador { get; set; }
        /// <summary>
        /// Si el usuario es usuario básico del sistema
        /// </summary>
        public bool EsUsuarioBasico { get; set; }
        /// <summary>
        /// Si el correo electrónico aún tiene el valor por defecto asignado
        /// </summary>
        public bool EsCorreoElectronicoPorDefecto { get; set; }
        /// <summary>
        /// Si la contraseña del usuario aún tiene el valor por defecto asignado
        /// </summary>
        public bool EsContrasenaPorDefecto { get; set; }
        /// <summary>
        /// Indicador de estado del usuario (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; set; }
        /// <summary>
        /// Si no se desea validar el correo electrónico del usuario
        /// </summary>
        public bool NoValidarCorreoElectronico { get; set; }
        /// <summary>
        /// Lista de los 5 últimos accesos del usuario
        /// </summary>
        public List<string> UltimosAccesos { get; set; }
        /// <summary>
        /// Token de inicio de sesion del usuario
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Tipo de operación a realizar
        /// </summary>
        public EnumTipoOperacion TipoOperacion { get; set; }
        /// <summary>
        /// Lista de los tipos de operaciones de los usuarios
        /// </summary>
        public enum EnumTipoOperacion
        {
            CambiarDatosPorDefecto = 1,
            ActualizarContrasena = 2,
            ModificarPerfilUsuario = 3,
            ModificarUsuario = 4
        }
    }
}
