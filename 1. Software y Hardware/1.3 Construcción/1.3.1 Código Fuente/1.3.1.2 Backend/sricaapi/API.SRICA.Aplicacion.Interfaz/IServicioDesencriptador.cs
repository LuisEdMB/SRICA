namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz para el servicio que desencripta datos usando el 
    /// servicio de dominio IServicioCryptoAES
    /// </summary>
    public interface IServicioDesencriptador
    {
        /// <summary>
        /// Método que desencripta datos y los transforma a un objeto en específico
        /// </summary>
        /// <typeparam name="T">T objeto a formar con los datos desencriptados</typeparam>
        /// <param name="datosEncriptados">Datos a desencriptar</param>
        /// <returns>Objeto con los datos desencriptados</returns>
        T Desencriptar<T>(string datosEncriptados);
    }
}
