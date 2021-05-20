using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Dominio.Servicio.Interfaz;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación para el servicio que encripta datos usando el servicio de 
    /// dominio IServicioCryptoAES
    /// </summary>
    public class ServicioEncriptador : IServicioEncriptador
    {
        /// <summary>
        /// Configuración del proyecto
        /// </summary>
        private readonly IConfiguration _configuracion;
        /// <summary>
        /// Servicio Crypto con AES
        /// </summary>
        private readonly IServicioCryptoAES _servicioCryptoAES;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="configuracion">Configuración del proyecto</param>
        /// <param name="servicioCryptoAES">Servicio Crypto con AES</param>
        public ServicioEncriptador(IConfiguration configuracion, IServicioCryptoAES servicioCryptoAES)
        {
            _configuracion = configuracion;
            _servicioCryptoAES = servicioCryptoAES;
        }
        /// <summary>
        /// Método que encripta datos
        /// </summary>
        /// <typeparam name="T">T dato</typeparam>
        /// <param name="datosDesencriptados">Datos a encriptar</param>
        /// <returns>Datos encriptados</returns>
        public string Encriptar<T>(T datosDesencriptados)
        {
            var datosJson = JsonSerializer.Serialize(datosDesencriptados);
            var datosEncriptados = _servicioCryptoAES.Encriptar(datosJson,
                _configuracion["SEGURIDAD_CLAVE_SECRETA"],
                _configuracion["SEGURIDAD_IV"]);
            return datosEncriptados;
        }
    }
}
