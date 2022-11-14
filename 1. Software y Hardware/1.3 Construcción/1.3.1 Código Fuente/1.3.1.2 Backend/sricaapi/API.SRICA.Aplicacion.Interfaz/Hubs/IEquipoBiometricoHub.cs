using System.Threading.Tasks;

namespace API.SRICA.Aplicacion.Interfaz.Hubs
{
    /// <summary>
    /// Interfaz para acciones de equipos biométricos utilizando Hubs, en tiempo real
    /// </summary>
    public interface IEquipoBiometricoHub
    {
        /// <summary>
        /// Método asíncrono que envía (el cliente recibe) el listado de equipos biométricos 
        /// encontrados de la red empresarial a todos los clientes
        /// </summary>
        /// <param name="equiposBiometricosEncriptado">Listado de equipos biométricos encontrados 
        /// (encriptado)</param>
        /// <returns>Tarea asíncrona ejecutada</returns>
        Task RecibirListadoDeEquiposBiometricosDeLaRedEmpresarial(object equiposBiometricosEncriptado);
        /// <summary>
        /// Método asíncrono que envía (el cliente recibe) el listado de equipos biométricos 
        /// registrados (tanto activo como inactivos) a todos los clientes
        /// </summary>
        /// <param name="equiposBiometricosEncriptado">Listado de equipos biométricos registrados 
        /// (encriptado)</param>
        /// <returns>Tarea asíncrona ejecutada</returns>
        Task RecibirListadoDeEquiposBiometricosRegistrados(object equiposBiometricosEncriptado);
    }
}
