using API.SRICA.Aplicacion.Implementacion.Hubs;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace API.SRICA.Distribucion.Hubs
{
    /// <summary>
    /// Clase que representa al WORKER (trabajador) que inicia los servicios de hubs para 
    /// equipos biométrico, en el background
    /// </summary>
    public class EquipoBiometricoWorker : BackgroundService
    {
        /// <summary>
        /// Contexto del hub de equipos biométricos
        /// </summary>
        private readonly IHubContext<EquipoBiometricoHub, IEquipoBiometricoHub> _equipoBiometricoHub;
        /// <summary>
        /// Servicio de consultas y operaciones de equipos biométricos
        /// </summary>
        private readonly IServicioEquipoBiometrico _servicioEquipoBiometrico;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="equipoBiometricoHub">Contexto del hub de equipos biométricos</param>
        /// <param name="serviceProvider">Proveedor de servicios</param>
        public EquipoBiometricoWorker(
            IHubContext<EquipoBiometricoHub, IEquipoBiometricoHub> equipoBiometricoHub, 
            IServiceProvider serviceProvider)
        {
            _equipoBiometricoHub = equipoBiometricoHub;
            _servicioEquipoBiometrico = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IServicioEquipoBiometrico>();
        }
        /// <summary>
        /// Método abstracto de BackgroundService
        /// </summary>
        /// <param name="stoppingToken">Token de parada</param>
        /// <returns>Tarea asíncrona ejecutada</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Run(async () =>
                    {
                        await RecibirListadoDeEquiposBiometricosRegistrados();
                    });
                    await Task.Run(async () =>
                    {
                        await RecibirListadoDeEquiposBiometricosDeLaRedEmpresarial();
                    });
                    await Task.Delay(1000);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
        #region Métodos privados
        /// <summary>
        /// Método asíncrono para el servicio hub de envío (clientes reciben) de equipos
        /// biométricos registrados
        /// </summary>
        /// <returns>Tarea asíncrona ejecutada</returns>
        private async Task RecibirListadoDeEquiposBiometricosRegistrados()
        {
            var equiposBiometricosRegistrados = _servicioEquipoBiometrico
                .ObtenerListadoDeEquiposBiometricosRegistrados();
            await _equipoBiometricoHub.Clients.All.RecibirListadoDeEquiposBiometricosRegistrados(
                equiposBiometricosRegistrados);
        }
        /// <summary>
        /// Método asíncrono para el servicio hub de envío (clientes reciben) de equipos
        /// biométricos presentes en la red empresarial
        /// </summary>
        /// <returns>Tarea asíncrona ejecutada</returns>
        private async Task RecibirListadoDeEquiposBiometricosDeLaRedEmpresarial()
        {
            var equiposBiometricos = _servicioEquipoBiometrico
                .ObtenerListadoEquiposBiometricosDeLaRedEmpresarial();
            await _equipoBiometricoHub.Clients.All
                .RecibirListadoDeEquiposBiometricosDeLaRedEmpresarial(equiposBiometricos);
        }
        #endregion
    }
}
