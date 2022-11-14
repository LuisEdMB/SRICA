using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using NetTools;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.SRICA.Dominio.Servicio.Interfaz;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Interfaz para el servicio de tratamientos de direcciones de red
    /// </summary>
    public class ServicioDireccionRed : IServicioDireccionRed
    {
        /// <summary>
        /// Servicio de ping a hosts
        /// </summary>
        private readonly IServicioPingHost _servicioPingHost;
        /// <summary>
        /// Servicio de mapeo del equipo biométrico a DTO
        /// </summary>
        private readonly IServicioMapeoEquipoBiometicoADto _servicioMapeoEquipoBiometicoADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioPingHost">Servicio de ping a hosts</param>
        /// <param name="servicioMapeoEquipoBiometicoADTO">Servicio de mapeo del equipo biométrico a 
        /// DTO</param>
        public ServicioDireccionRed(IServicioPingHost servicioPingHost,
            IServicioMapeoEquipoBiometicoADto servicioMapeoEquipoBiometicoADTO)
        {
            _servicioPingHost = servicioPingHost;
            _servicioMapeoEquipoBiometicoADTO = servicioMapeoEquipoBiometicoADTO;
        }
        /// <summary>
        /// Método que obtiene el listado de IP's según una subred indicada, o un nombre de dominio
        /// indicado
        /// </summary>
        /// <param name="subred">Subred (obligatorio si se desea obtener las IP's mediante una 
        /// subred)</param>
        /// <param name="mascaraSubred">Máscara de subred (obligatorio cuando se declara la subred)</param>
        /// <param name="nombreDominio">Nombre de dominio (obligatorio si se desea obtener las IP's
        /// mediante un nombre de dominio)</param>
        /// <returns>Listado de IP's encontradas</returns>
        public List<string> ObtenerListadoDeHosts(string subred, string mascaraSubred, string nombreDominio)
        {
            if (!string.IsNullOrEmpty(subred) && !string.IsNullOrEmpty(mascaraSubred))
                return ObtenerListadoDeHostsSegunSubred(subred, mascaraSubred);
            return string.IsNullOrEmpty(nombreDominio) 
                ? ObtenerListadoDeHostsSegunNombreDominio(nombreDominio) 
                : new List<string>();
        }
        /// <summary>
        /// Método asíncrono que realiza el proceso de verificación de IP's
        /// </summary>
        /// <param name="hosts">Listado de IP's a verificar (ping)</param>
        /// <returns>Listado de IP's válidos</returns>
        public async Task<List<DtoEquipoBiometrico>> PingAsync(List<string> hosts)
        {
            var watch = new Stopwatch();
            watch.Start();
            var tasksb = hosts.Select(HostName => _servicioPingHost.PingHost(HostName));
            var pinglist = await Task.WhenAll(tasksb);
            watch.Stop();
            return MapearHostsADtoEquipoBiometrico(pinglist.ToList());
        }
        #region Métodos privados
        /// <summary>
        /// Método que obtiene el listado de IP's según una subred y máscara de subred
        /// </summary>
        /// <param name="subred">Subred</param>
        /// <param name="mascaraSubred">Máscara de subred</param>
        /// <returns>Listado de IP's encontradas</returns>
        private List<string> ObtenerListadoDeHostsSegunSubred(string subred, string mascaraSubred)
        {
            List<string> hostsEncontrados = new List<string>();
            var hosts = IPAddressRange.Parse(subred + "/" + mascaraSubred);
            foreach(var host in hosts)
            {
                hostsEncontrados.Add(host.ToString());
            }
            return hostsEncontrados;
        }
        /// <summary>
        /// Método que obtiene el listado de IP's según un nombre de dominio
        /// </summary>
        /// <param name="nombreDominio">Nombre de dominio</param>
        /// <returns>Listado de IP's encontradas</returns>
        private List<string> ObtenerListadoDeHostsSegunNombreDominio(string nombreDominio)
        {
            List<string> hostsEncontrados = new List<string>();
            var hosts = Dns.GetHostAddresses(nombreDominio);
            foreach(var host in hosts)
            {
                hostsEncontrados.Add(host.ToString());
            }
            return hostsEncontrados;
        }
        /// <summary>
        /// Método que mapea los hosts encontados al objeto DTO de equipo biométrico
        /// </summary>
        /// <param name="hosts">Listado de hosts encontrados</param>
        /// <returns>Listado DTO de equipos biométricos</returns>
        private List<DtoEquipoBiometrico> MapearHostsADtoEquipoBiometrico(
            IEnumerable<Dictionary<string, string>> hosts)
        {
            return hosts.Select(host => _servicioMapeoEquipoBiometicoADTO.MapearADTO(string.Empty,
                host["host"], host["name"], host["macAddress"])).ToList();
        }
        #endregion
    }
}
