using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de consultas y operaciones de usuarios
    /// </summary>
    public interface IServicioUsuario
    {
        /// <summary>
        /// Método que modifica los datos del usuario. Las operaciones que se pueden realizar son:
        /// Cambiar datos por defecto, actualizar contraseña, modificar perfil de usuario,
        /// modificar datos del usuario
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los valores a modificar</param>
        /// <returns>Resultado encriptado con los datos del usuario y sus valores modificados</returns>
        string ModificarUsuario(JToken encriptado);
        /// <summary>
        /// Método que obtiene un usuario en base a su código de usuario
        /// </summary>
        /// <param name="codigoUsuarioEncriptado">Código del usuario a obtener (encriptado)</param>
        /// <returns>Resultado encriptado con los datos del usuario encontrado</returns>
        string ObtenerUsuario(string codigoUsuarioEncriptado);
        /// <summary>
        /// Método que obtiene el listado de usuarios, tanto activos como inactivos
        /// </summary>
        /// <returns>Resultado encriptado con el listado de usuarios</returns>
        string ObtenerListadoDeUsuarios();
        /// <summary>
        /// Método que guarda un nuevo usuario
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del usuario a guardar</param>
        /// <returns>Resultado encriptado con los datos del usuario guardado</returns>
        string GuardarUsuario(JToken encriptado);
        /// <summary>
        /// Método que inhabilita a los usuarios
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de usuarios a 
        /// inhabilitar</param>
        /// <returns>Resultado encriptado con el listado de usuarios inhabilitados</returns>
        string InhabilitarUsuarios(JToken encriptado);
        /// <summary>
        /// Método que habilita a los usuarios
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de usuarios a 
        /// habilitar</param>
        /// <returns>Resultado encriptado con el listado de usuarios habilitados</returns>
        string HabilitarUsuarios(JToken encriptado);
        /// <summary>
        /// Método que verifica que el usuario autenticado (claims token) se encuentre en estado
        /// activo
        /// </summary>
        /// <param name="usuarioToken">Usuario obtenido desde claims token</param>
        void VerificarUsuarioDelTokenActivo(Claim usuarioToken);
    }
}
