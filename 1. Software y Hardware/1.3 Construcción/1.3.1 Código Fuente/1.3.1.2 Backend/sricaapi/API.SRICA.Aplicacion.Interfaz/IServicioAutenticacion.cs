using API.SRICA.Aplicacion.DTO;
using Newtonsoft.Json.Linq;

namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Intefaz para el servicio de autenticación del inicio de sesión del usuario
    /// que intenta iniciar sesión en el sistema
    /// </summary>
    public interface IServicioAutenticacion
    {
        /// <summary>
        /// Método que comprueba el usuario y contraseña del usuario que intenta
        /// iniciar sesión en el sistema
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el usuario,
        /// contraseña y audiencia permitida para el inicio de sesión del usuario</param>
        /// <returns>Resultado encriptado del proceso de inicio de sesión</returns>
        string ComprobarUsuario(JToken encriptado);
        /// <summary>
        /// Método que refresca el token del usuario
        /// </summary>
        /// <param name="encriptado">Datos encriptado que contiene el token expirado para generar 
        /// el nuevo token</param>
        /// <returns>Resultado encriptado del proceso de inicio de sesión</returns>
        string RefrescarTokenDelUsuario(JToken encriptado);
    }
}
