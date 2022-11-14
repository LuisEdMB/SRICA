using API.SRICA.Dominio.Entidad.US;

namespace API.SRICA.Dominio.Servicio.Interfaz
{
    /// <summary>
    /// Interfaz para el servicio de token de seguridad
    /// </summary>
    public interface IServicioToken
    {
        /// <summary>
        /// Método que valida las audiencias del cliente y API
        /// </summary>
        /// <param name="audienciaPermitidaCliente">Audiencia del cliente</param>
        /// <param name="audienciaPermitidaAPI">Audiencia del API</param>
        void ValidarAudiencia(string audienciaPermitidaCliente, string audienciaPermitidaAPI);
        /// <summary>
        /// Método que genera el token para el usuario
        /// </summary>
        /// <param name="usuario">Usuario a quien se le generará el token</param>
        /// <param name="claveSecreta">Clave secreta</param>
        /// <param name="issuer">Issuer</param>
        /// <param name="audienciaPermitida">Audiencia del API</param>
        /// <returns>Token generado</returns>
        string GenerarToken(Usuario usuario, string claveSecreta,
            string issuer, string audienciaPermitida);
        /// <summary>
        /// Método que refresca el token del usuario
        /// </summary>
        /// <param name="tokenAnterior">Token expirado del usuario</param>
        /// <param name="claveSecreta">Clave secreta</param>
        /// <param name="issuer">Issuer</param>
        /// <param name="audienciaPermitida">Audiencia del API</param>
        /// <returns>Token generado</returns>
        string RefrescarToken(string tokenAnterior, string claveSecreta, 
            string issuer, string audienciaPermitida);
    }
}
