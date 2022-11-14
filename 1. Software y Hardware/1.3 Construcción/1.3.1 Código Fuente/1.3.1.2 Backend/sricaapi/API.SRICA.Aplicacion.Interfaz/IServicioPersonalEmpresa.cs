using Newtonsoft.Json.Linq;

namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de consultas y operaciones de personal de la empresa
    /// </summary>
    public interface IServicioPersonalEmpresa
    {
        /// <summary>
        /// Método que obtiene el listado del personal de la empresa registrado, tanto activos 
        /// como inactivos
        /// </summary>
        /// <returns>Resultado encriptado con el listado del personal de la empresa</returns>
        string ObtenerListadoDePersonalDeLaEmpresa();
        /// <summary>
        /// Método que obtiene un personal de la empresa en base a su código de personal
        /// de la empresa
        /// </summary>
        /// <param name="codigoPersonalEmpresaEncriptado">Código del personal de la empresa 
        /// a obtener (encriptado)</param>
        /// <returns>Resultado encriptado con los datos del personal de la empresa encontrado</returns>
        string ObtenerPersonalDeLaEmpresa(string codigoPersonalEmpresaEncriptado);
        /// <summary>
        /// Método que guarda un personal de la empresa, o registra masivamente al personal en base
        /// a un archivo
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del personal de 
        /// la empresa a guardar, o el archivo con el listado del personal a guardar</param>
        /// <returns>Resultado encriptado con los datos del personal de la empresa guardado(s)</returns>
        string GuardarPersonalEmpresa(JToken encriptado);
        /// <summary>
        /// Método que modifica un personal de la empresa
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del personal de 
        /// la empresa a modificar</param>
        /// <returns>Resultado encriptado con los datos del personal de la empresa modificado</returns>
        string ModificarPersonalEmpresa(JToken encriptado);
        /// <summary>
        /// Método que inhabilita un listado de personal de la empresa
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado del personal de la empresa
        /// a inhabilitar</param>
        /// <returns>Resultado encriptado con el listado del personal de la empresa inhabilitados</returns>
        string InhabilitarListadoDePersonalEmpresa(JToken encriptado);
        /// <summary>
        /// Método que habilita un listado de personal de la empresa
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado del personal de la empresa
        /// a habilitar</param>
        /// <returns>Resultado encriptado con el listado del personal de la empresa habilitados</returns>
        string HabilitarListadoDePersonalEmpresa(JToken encriptado);
    }
}
