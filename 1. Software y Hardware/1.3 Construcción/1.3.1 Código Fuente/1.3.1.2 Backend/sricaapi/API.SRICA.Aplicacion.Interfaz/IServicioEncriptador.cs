namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz para el servicio que encripta datos usando el servicio de 
    /// dominio IServicioCryptoAES
    /// </summary>
    public interface IServicioEncriptador
    {
        /// <summary>
        /// Método que encripta datos
        /// </summary>
        /// <typeparam name="T">T dato</typeparam>
        /// <param name="datosDesencriptados">Datos a encriptar</param>
        /// <returns>Datos encriptados</returns>
        string Encriptar<T>(T datosDesencriptados);
    }
}
