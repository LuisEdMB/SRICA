using API.SRICA.Dominio.Entidad.US;
using System.Collections.Generic;

namespace API.SRICA.Dominio.Servicio.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de dominio para los usuarios
    /// </summary>
    public interface IServicioDominioUsuario
    {
        /// <summary>
        /// Método que valida al usuario que intenta iniciar sesión en el sistema
        /// </summary>
        /// <param name="usuario">Usuario a validar</param>
        /// <param name="contrasena">Contraseña encriptada del usuario a validar</param>
        /// <returns>Usuario validado</returns>
        Usuario ValidarUsuarioParaAutenticacion(Usuario usuario, string contrasena);
        /// <summary>
        /// Método que valida al usuario que intenta recuperar su contraseña olvidada
        /// </summary>
        /// <param name="usuario">Usuario a validar</param>
        /// <returns>Usuario validado</returns>
        void ValidarUsuarioParaRecuperacionDeContrasenaOlvidada(Usuario usuario);
        /// <summary>
        /// Método que valida que el usuario de acción se encuentre habilitado para 
        /// realizar sus funciones
        /// </summary>
        /// <param name="usuario">Usuario a validar</param>
        /// <returns>Usuario validado</returns>
        void ValidarUsuarioDeAccionHabilitado(Usuario usuario);
        /// <summary>
        /// Método que modifica el correo y contraseña del usuario
        /// </summary>
        /// <param name="usuario">Usuario a modificar</param>
        /// <param name="correoElectronico">Correo eletrónico modificado</param>
        /// <param name="contrasena">Contraseña modificada</param>
        /// <param name="confirmarContrasena">Confirmar contraseña/param>
        /// <param name="contrasenaEncriptada">Contraseña modificada (encriptado)</param>
        /// <param name="noValidarCorreoElectronico">Si no se desea validar el correo
        /// electrónico del usuario</param>
        /// <param name="noValidarContrasena">Si no se desea validar la contraseña
        /// del usuario</param>
        /// <returns>Usuario modificado</returns>
        Usuario ModificarCorreoYContrasenaDelUsuario(Usuario usuario, 
            string correoElectronico, string contrasena, string confirmarContrasena, 
            string contrasenaEncriptada, bool noValidarCorreoElectronico,
            bool noValidarContrasena);
        /// <summary>
        /// Método que crea la entidad usuario
        /// </summary>
        /// <param name="usuariosRegistrados">Listado de usuarios registrados, tanto activos 
        /// como inactivos (usado para validación de duplicidad)</param>
        /// <param name="rolUsuario">Rol de usuario del usuario a crear</param>
        /// <param name="usuario">Usuario</param>
        /// <param name="contrasenaPorDefectoEncriptada">Contraseña (encriptada) por defecto a 
        /// asignar al usuario</param>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="apellido">Apellido del usuario</param>
        /// <returns>Usuario creado</returns>
        Usuario CrearUsuario(List<Usuario> usuariosRegistrados, RolUsuario rolUsuario, string usuario, 
            string contrasenaPorDefectoEncriptada, string nombre, string apellido);
        /// <summary>
        /// Método que modifica la entidad usuario
        /// </summary>
        /// <param name="usuariosRegistrados">Listado de usuarios registrados, tanto activos 
        /// como inactivos (usado para validación de duplicidad)</param>
        /// <param name="entidadUsuario">Usuario a modificar</param>
        /// <param name="rolUsuario">Rol de usuario del usuario a modificar</param>
        /// <param name="usuario">Usuario</param>
        /// <param name="contrasenaPorDefectoEncriptada">Contraseña (encriptada) por defecto a 
        /// asignar al usuario</param>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="apellido">Apellido del usuario</param>
        /// <param name="contrasenaPorDefecto">Si se debe generar la contraseña por defecto</param>
        /// <returns>Usuario modificado</returns>
        Usuario ModificarUsuario(List<Usuario> usuariosRegistrados, Usuario entidadUsuario, 
            RolUsuario rolUsuario, string usuario, string contrasenaPorDefectoEncriptada, 
            string nombre, string apellido, bool contrasenaPorDefecto);
        /// <summary>
        /// Método que valida que se haya seleccionado, por lo menos, un usuario para ser
        /// inhabilitado o habilitado
        /// </summary>
        /// <param name="cantidadUsuariosSeleccionados">Cantidad de usuarios seleccionados</param>
        void ValidarUsuariosSeleccionados(int cantidadUsuariosSeleccionados);
        /// <summary>
        /// Método que inhabilita la entidad usuario
        /// </summary>
        /// <param name="usuario">Usuario a inhabilitar</param>
        /// <returns>Usuario inhabilitado</returns>
        Usuario InhabilitarUsuario(Usuario usuario);
        /// <summary>
        /// Método que habilita la entidad usuario
        /// </summary>
        /// <param name="usuario">Usuario a habilitar</param>
        /// <returns>Usuario habilitado</returns>
        Usuario HabilitarUsuario(Usuario usuario);
        /// <summary>
        /// Método que filtra un listado de usuarios solo con correos válidos
        /// </summary>
        /// <param name="usuarios">Lista de usuarios a filtrar</param>
        /// <returns>Usuarios con correos válidos</returns>
        List<Usuario> FiltrarUsuariosConCorreosValidos(List<Usuario> usuarios);
    }
}
