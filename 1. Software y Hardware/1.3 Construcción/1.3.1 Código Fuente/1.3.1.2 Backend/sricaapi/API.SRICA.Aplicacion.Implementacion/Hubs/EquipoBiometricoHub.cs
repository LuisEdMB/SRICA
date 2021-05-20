using API.SRICA.Aplicacion.Interfaz.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace API.SRICA.Aplicacion.Implementacion.Hubs
{
    /// <summary>
    /// Implementación para acciones de equipos biométricos utilizando Hubs, en tiempo real
    /// </summary>
    public class EquipoBiometricoHub : Hub<IEquipoBiometricoHub>
    {
        /// <summary>
        /// Método asíncrono que envía (el cliente recibe) el listado de equipos biométricos 
        /// encontrados de la red empresarial a todos los clientes
        /// </summary>
        /// <param name="equiposBiometricosEncriptado">Listado de equipos biométricos encontrados 
        /// (encriptado)</param>
        /// <returns>Tarea asíncrona ejecutada</returns>
        [Authorize]
        public async Task EnviarListadoDeEquiposBiometricosDeLaRedEmpresarial(
            object equiposBiometricosEncriptado)
        {
            await Clients.All.RecibirListadoDeEquiposBiometricosDeLaRedEmpresarial(
                equiposBiometricosEncriptado);
        }
        /// <summary>
        /// Método asíncrono que envía (el cliente recibe) el listado de equipos biométricos 
        /// registrados (tanto activo como inactivos) a todos los clientes
        /// </summary>
        /// <param name="equiposBiometricosEncriptado">Listado de equipos biométricos registrados 
        /// (encriptado)</param>
        /// <returns>Tarea asíncrona ejecutada</returns>
        [Authorize]
        public async Task EnviarListadoDeEquiposBiometricosRegistrados(
            object equiposBiometricosEncriptado)
        {
            await Clients.All.RecibirListadoDeEquiposBiometricosRegistrados(equiposBiometricosEncriptado);
        }
    }
}
