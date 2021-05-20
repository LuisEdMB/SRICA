using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Dominio.Servicio.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación para el servicio que desencripta datos usando el 
    /// servicio de dominio IServicioCryptoAES
    /// </summary>
    public class ServicioDesencriptador : IServicioDesencriptador
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
        public ServicioDesencriptador(IConfiguration configuracion, IServicioCryptoAES servicioCryptoAES)
        {
            _configuracion = configuracion;
            _servicioCryptoAES = servicioCryptoAES;
        }
        /// <summary>
        /// Método que desencripta datos y los transforma a un objeto en específico
        /// </summary>
        /// <typeparam name="T">T objeto a formar con los datos desencriptados</typeparam>
        /// <param name="datosEncriptados">Datos a desencriptar</param>
        /// <returns>Objeto con los datos desencriptados</returns>
        public T Desencriptar<T>(string datosEncriptados)
        {
            var datosDesencriptados = _servicioCryptoAES.Desencriptar(datosEncriptados,
                _configuracion["SEGURIDAD_CLAVE_SECRETA"],
                _configuracion["SEGURIDAD_IV"]);
            T objetoMapeado = JsonConvert.DeserializeObject<T>(datosDesencriptados);
            return objetoMapeado;
        }
    }
}
