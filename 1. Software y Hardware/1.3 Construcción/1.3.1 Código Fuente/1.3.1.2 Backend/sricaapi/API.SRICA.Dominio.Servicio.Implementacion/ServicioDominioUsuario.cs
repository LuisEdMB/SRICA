using API.SRICA.Dominio.Entidad.US;
using API.SRICA.Dominio.Excepcion;
using API.SRICA.Dominio.Servicio.Interfaz;
using System.Collections.Generic;
using System.Linq;

namespace API.SRICA.Dominio.Servicio.Implementacion
{
    /// <summary>
    /// Implementación del servicio de dominio para los usuarios
    /// </summary>
    public class ServicioDominioUsuario : IServicioDominioUsuario
    {  
        /// <summary>
        /// Método que valida al usuario que intenta iniciar sesión en el sistema
        /// </summary>
        /// <param name="usuario">Usuario a validar</param>
        /// <param name="contrasena">Contraseña encriptada del usuario a validar</param>
        /// <returns>Usuario validado</returns>
        public Usuario ValidarUsuarioParaAutenticacion(Usuario usuario, string contrasena)
        {
            if (usuario == null)
                throw new ExcepcionAplicacionPersonalizada("El usuario ingresado no existe.");
            if (usuario.EsUsuarioInactivo)
                throw new ExcepcionAplicacionPersonalizada("El usuario no está habilitado en el sistema.");
            if (!usuario.ContrasenaAcceso.Equals(contrasena))
                throw new ExcepcionAplicacionPersonalizada("La contraseña es incorrecta.");
            return usuario;
        }
        /// <summary>
        /// Método que valida al usuario que intenta recuperar su contraseña olvidada
        /// </summary>
        /// <param name="usuario">Usuario a validar</param>
        public void ValidarUsuarioParaRecuperacionDeContrasenaOlvidada(Usuario usuario)
        {
            if (usuario == null)
                throw new ExcepcionAplicacionPersonalizada("El usuario ingresado no existe.");
            if (!usuario.RolUsuario.EsAdministrador)
                throw new ExcepcionAplicacionPersonalizada("El usuario ingresado no tiene el rol ADMINISTRADOR. " +
                    "Si desea recuperar su contraseña, contáctese con el administrador del sistema.");
            if (usuario.EsUsuarioInactivo)
                throw new ExcepcionAplicacionPersonalizada("El usuario no está habilitado en el sistema.");
        }
        /// <summary>
        /// Método que valida que el usuario de acción se encuentre habilitado para 
        /// realizar sus funciones
        /// </summary>
        /// <param name="usuario">Usuario a validar</param>
        public void ValidarUsuarioDeAccionHabilitado(Usuario usuario)
        {
            if (usuario.EsUsuarioInactivo)
                throw new ExcepcionAplicacionPersonalizada("Su usuario ha sido inhabilitado en el sistema.",
                    ExcepcionAplicacionPersonalizada.CodigoExcepcionPersonalizado.AdvertenciaSimpleConLogOutUsuario);
        }
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
        public Usuario ModificarCorreoYContrasenaDelUsuario(Usuario usuario,
            string correoElectronico, string contrasena, string confirmarContrasena,
            string contrasenaEncriptada, bool noValidarCorreoElectronico,
            bool noValidarContrasena)
        {
            if (!noValidarCorreoElectronico && !noValidarContrasena)
                return ModificarCorreoYContrasena(usuario, correoElectronico, contrasena,
                    confirmarContrasena, contrasenaEncriptada);
            if (noValidarCorreoElectronico && !noValidarContrasena)
                return ModificarSoloContrasena(usuario, contrasena, confirmarContrasena,
                    contrasenaEncriptada);
            if (!noValidarCorreoElectronico && noValidarContrasena)
                return ModificarSoloCorreoElectronico(usuario, correoElectronico);
            return usuario;
        }
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
        public Usuario CrearUsuario(List<Usuario> usuariosRegistrados, RolUsuario rolUsuario, string usuario,
            string contrasenaPorDefectoEncriptada, string nombre, string apellido)
        {
            ValidarUsuario(usuariosRegistrados, usuario);
            ValidarNombreDelUsuario(nombre);
            ValidarApellidoDelUsuario(apellido);
            return Usuario.CrearUsuario(rolUsuario, usuario, contrasenaPorDefectoEncriptada, nombre, 
                apellido);
        }
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
        public Usuario ModificarUsuario(List<Usuario> usuariosRegistrados, Usuario entidadUsuario,
            RolUsuario rolUsuario, string usuario, string contrasenaPorDefectoEncriptada,
            string nombre, string apellido, bool contrasenaPorDefecto)
        {
            var usuariosRegistradosSinElUsuarioActual = usuariosRegistrados.Where(
                g => g.CodigoUsuario != entidadUsuario.CodigoUsuario).ToList();
            ValidarUsuario(usuariosRegistradosSinElUsuarioActual, usuario);
            ValidarNombreDelUsuario(nombre);
            ValidarApellidoDelUsuario(apellido);
            entidadUsuario.ModificarUsuario(rolUsuario, usuario, contrasenaPorDefectoEncriptada, nombre, 
                apellido, contrasenaPorDefecto);
            return entidadUsuario;
        }
        /// <summary>
        /// Método que valida que se haya seleccionado, por lo menos, un usuario para ser
        /// inhabilitado o habilitado
        /// </summary>
        /// <param name="cantidadUsuariosSeleccionados">Cantidad de usuarios seleccionados</param>
        public void ValidarUsuariosSeleccionados(int cantidadUsuariosSeleccionados)
        {
            if (cantidadUsuariosSeleccionados == 0)
                throw new ExcepcionAplicacionPersonalizada("Debe seleccionar, al menos, un usuario de la lista.");
        }
        /// <summary>
        /// Método que inhabilita la entidad usuario
        /// </summary>
        /// <param name="usuario">Usuario a inhabilitar</param>
        /// <returns>Usuario inhabilitado</returns>
        public Usuario InhabilitarUsuario(Usuario usuario)
        {
            usuario.InhabilitarUsuario();
            return usuario;
        }
        /// <summary>
        /// Método que habilita la entidad usuario
        /// </summary>
        /// <param name="usuario">Usuario a habilitar</param>
        /// <returns>Usuario habilitado</returns>
        public Usuario HabilitarUsuario(Usuario usuario)
        {
            usuario.HabilitarUsuario();
            return usuario;
        }
        /// <summary>
        /// Método que filtra un listado de usuarios solo con correos válidos
        /// </summary>
        /// <param name="usuarios">Lista de usuarios a filtrar</param>
        /// <returns>Usuarios con correos válidos</returns>
        public List<Usuario> FiltrarUsuariosConCorreosValidos(List<Usuario> usuarios)
        {
            return usuarios.Where(g => g.CorreoElectronico.ValidarCorreoElectronico()).ToList();
        }
        #region Métodos privados
        /// <summary>
        /// Método que modifica el correo electrónico y contraseña del usuario
        /// </summary>
        /// <param name="usuario">Usuario a modificar</param>
        /// <param name="correoElectronico">Correo eletrónico modificado</param>
        /// <param name="contrasena">Contraseña modificada</param>
        /// <param name="confirmarContrasena">Confirmar contraseña/param>
        /// <param name="contrasenaEncriptada">Contraseña modificada (encriptado)</param>
        /// <returns>Usuario modificado</returns>
        private Usuario ModificarCorreoYContrasena(Usuario usuario, string correoElectronico,
            string contrasena, string confirmarContrasena, string contrasenaEncriptada)
        {
            ValidarCorreoElectronicoDelUsuario(correoElectronico.Trim());
            ValidarContrasenaDelUsuario(contrasena, confirmarContrasena);
            usuario.ModificarCorreoYContrasena(correoElectronico.Trim(),
                contrasenaEncriptada);
            return usuario;
        }
        /// <summary>
        /// Método que modifica solo la contraseña del usuario
        /// </summary>
        /// <param name="usuario">Usuario a modificar</param>
        /// <param name="contrasena">Contraseña modificada</param>
        /// <param name="confirmarContrasena">Confirmar contraseña/param>
        /// <param name="contrasenaEncriptada">Contraseña modificada (encriptado)</param>
        /// <returns>Usuario modificado</returns>
        private Usuario ModificarSoloContrasena(Usuario usuario, string contrasena, 
            string confirmarContrasena, string contrasenaEncriptada)
        {
            ValidarContrasenaDelUsuario(contrasena, confirmarContrasena);
            usuario.ModificarSoloContrasena(contrasenaEncriptada);
            return usuario;
        }
        /// <summary>
        /// Método que modifica solo el correo electrónico del usuario
        /// </summary>
        /// <param name="usuario">Usuario a modificar</param>
        /// <param name="correoElectronico">Correo eletrónico modificado</param>
        /// <returns>Usuario modificado</returns>
        private Usuario ModificarSoloCorreoElectronico(Usuario usuario, string correoElectronico)
        {
            ValidarCorreoElectronicoDelUsuario(correoElectronico.Trim());
            usuario.ModificarSoloCorreoElectronico(correoElectronico.Trim());
            return usuario;
        }
        /// <summary>
        /// Método que valida el correo electrónico del usuario
        /// </summary>
        /// <param name="correoElectronico">Correo eletrónico del usuario</param>
        private void ValidarCorreoElectronicoDelUsuario(string correoElectronico)
        {
            if (!correoElectronico.ValidarCorreoElectronico())
                throw new ExcepcionAplicacionPersonalizada("El correo electrónico no tiene el formato adecuado.");
            if (!correoElectronico.ValidarCantidadCaracteres(1, 64))
                throw new ExcepcionAplicacionPersonalizada("La cantidad máxima de caracteres para el correo " +
                    "electrónico es de 64 caracteres.");
        }
        /// <summary>
        /// Método que valida la contraseña del usuario
        /// </summary>
        /// <param name="contrasena">Contraseña modificada (encriptado)</param>
        /// <param name="confirmarContrasena">Confirmar contraseña (encriptado)</param>
        private void ValidarContrasenaDelUsuario(string contrasena, string confirmarContrasena)
        {
            if (!contrasena.Equals(confirmarContrasena))
                throw new ExcepcionAplicacionPersonalizada("Las contraseñas no coinciden.");
            if (!contrasena.ValidarCantidadCaracteres(8, 30))
                throw new ExcepcionAplicacionPersonalizada("La cantidad de caracteres que debe poseer la nueva " +
                    "contraseña debe estar en el rango de 8 a 30 caracteres.");
            var nivelFortalezaContrasena = confirmarContrasena.CalcularNivelFortalezaDeContrasena();
            if (nivelFortalezaContrasena != Usuario.EnumNivelFortalezaContrasena.MedioAlto &&
                nivelFortalezaContrasena != Usuario.EnumNivelFortalezaContrasena.Alto)
                throw new ExcepcionAplicacionPersonalizada("El nivel de fortaleza de la nueva contraseña " +
                    "es: " + ObtenerDescripcionDelNivelFortalezaContrasena(nivelFortalezaContrasena) + 
                    ". La nueva contraseña debe estar compuesto por " +
                    "letras mayúsculas y minúsculas, números y caracteres especiales, presentando " +
                    "un nivel de fortaleza Medio-Alto o Alto.");
        }
        /// <summary>
        /// Método que obtiene la descripción del nivel de fortaleza calculada de la
        /// contraseña del usuario
        /// </summary>
        /// <param name="nivelFortalezaContrasena">Nivel de fortaleza calculada de
        /// la contraseña</param>
        /// <returns>Descripción del nivel de fortaleza de la contraseña</returns>
        private string ObtenerDescripcionDelNivelFortalezaContrasena(
            Usuario.EnumNivelFortalezaContrasena nivelFortalezaContrasena)
        {
            return nivelFortalezaContrasena switch
            {
                Usuario.EnumNivelFortalezaContrasena.Bajo => "BAJO",
                Usuario.EnumNivelFortalezaContrasena.MedioBajo => "MEDIO BAJO",
                Usuario.EnumNivelFortalezaContrasena.Medio => "MEDIO",
                Usuario.EnumNivelFortalezaContrasena.MedioAlto => "MEDIO ALTO",
                Usuario.EnumNivelFortalezaContrasena.Alto => "ALTO",
                _ => string.Empty,
            };
        }
        /// <summary>
        /// Método que valida el usuario del usuario
        /// </summary>
        /// <param name="usuariosRegistrados">Listado de usuarios registrados, tanto activos 
        /// como inactivos (usado para validación de duplicidad)</param>
        /// <param name="usuario">Usuario a validar</param>
        private void ValidarUsuario(List<Usuario> usuariosRegistrados, string usuario)
        {
            if (!usuario.ValidarCadenaDeTextoSoloNumeros())
                throw new ExcepcionAplicacionPersonalizada("El usuario debe ser de tipo numérico.");
            if (!usuario.ValidarCantidadCaracteres(8,8))
                throw new ExcepcionAplicacionPersonalizada("La cantidad de dígitos del usuario debe ser 8 dígitos.");
            if (usuariosRegistrados.Where(g => g.UsuarioAcceso.Equals(usuario.Trim())).Any())
                throw new ExcepcionAplicacionPersonalizada("El usuario ingresado ya existe. Verifique el listado de " +
                    "usuarios en estado activo o inactivo para encontrar el usuario duplicado.");
        }
        /// <summary>
        /// Método que valida el nombre del usuario
        /// </summary>
        /// <param name="nombre">Nombre del usuario a validar</param>
        private void ValidarNombreDelUsuario(string nombre)
        {
            if (!nombre.ValidarCantidadCaracteres(1, 40))
                throw new ExcepcionAplicacionPersonalizada("La cantidad máxima de caracteres para el nombre " +
                    "es de 40 caracteres.");
            if (!nombre.ValidarCadenaDeTextoSoloLetras())
                throw new ExcepcionAplicacionPersonalizada("El nombre ingresado debe contener solo letras.");
        }
        /// <summary>
        /// Método que valida el apellido del usuario
        /// </summary>
        /// <param name="apellido">Apellido del usuario a validar</param>
        private void ValidarApellidoDelUsuario(string apellido)
        {
            if (!apellido.ValidarCantidadCaracteres(1, 40))
                throw new ExcepcionAplicacionPersonalizada("La cantidad máxima de caracteres para el apellido " +
                    "es de 40 caracteres.");
            if (!apellido.ValidarCadenaDeTextoSoloLetras())
                throw new ExcepcionAplicacionPersonalizada("El apellido ingresado debe contener solo letras.");
        }
        #endregion
    }
}
