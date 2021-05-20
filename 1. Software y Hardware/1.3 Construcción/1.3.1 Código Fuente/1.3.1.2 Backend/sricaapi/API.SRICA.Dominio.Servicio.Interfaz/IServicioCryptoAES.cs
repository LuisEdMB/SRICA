namespace API.SRICA.Dominio.Servicio.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de seguridad para el encriptado y desencriptado de datos
    /// (con AES)
    /// </summary>
    public interface IServicioCryptoAES
    {
        /// <summary>
        /// Método que encripta los datos
        /// </summary>
        /// <param name="datosDesencriptados">Datos a encriptar</param>
        /// <param name="claveSecreta">Clave secreta</param>
        /// <param name="vectorIV">Vector IV</param>
        /// <returns>Datos encriptados</returns>
        string Encriptar(string datosDesencriptados, string claveSecreta, string vectorIV);
        /// <summary>
        /// Método que desencripta los datos
        /// </summary>
        /// <param name="datosEncriptados">Datos a desencriptar</param>
        /// <param name="claveSecreta">Clave secreta</param>
        /// <param name="vectorIV">Vector IV</param>
        /// <returns>Datos desencriptados</returns>
        string Desencriptar(string datosEncriptados, string claveSecreta, string vectorIV);
    }
}
