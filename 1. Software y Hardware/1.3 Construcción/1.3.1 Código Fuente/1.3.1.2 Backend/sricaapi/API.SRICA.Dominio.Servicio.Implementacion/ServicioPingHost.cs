using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.SRICA.Dominio.Servicio.Interfaz;
using ArpLookup;

namespace API.SRICA.Dominio.Servicio.Implementacion
{
    /// <summary>
    /// Implementación del servicio de ping
    /// </summary>
    public class ServicioPingHost : IServicioPingHost
    {
        /// <summary>
        /// Método que realiza el ping a una dirección de red específica
        /// </summary>
        /// <param name="host">Dirección de red</param>
        /// <param name="esNombreEquipo">Si el host es un nombre de equipo</param>
        /// <returns>Diccionario con los valores: nombre de equipo, dirección de red,
        /// dirección MAC</returns>
        public async Task<Dictionary<string, string>> PingHost(string host, bool esNombreEquipo = false)
        {
            string data = "PCK|SCAN|5025066840471";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            PingOptions options = new PingOptions(64, true);
            var equipo = new Dictionary<string, string>
            {
                {"host", string.Empty},
                {"name", string.Empty},
                {"macAddress", string.Empty}
            };
            try
            {
                var pingresult = await new Ping().SendPingAsync(host, 3 * 1000, buffer, options);
                if (pingresult.Status == IPStatus.Success)
                {
                    if (!esNombreEquipo)
                    {
                        string name;
                        string macAddress;
                        try
                        {
                            IPHostEntry hostEntry = await Dns.GetHostEntryAsync(host).ConfigureAwait(false);
                            PhysicalAddress macHost = await Arp.LookupAsync(hostEntry.AddressList.First(g => 
                                    g.AddressFamily == AddressFamily.InterNetwork))
                                .ConfigureAwait(false);
                            name = hostEntry.HostName.RemoverPrefijoDeNombreDeEquipoDeRaspberry();
                            macAddress = Regex.Replace(
                                macHost.ToString(),
                                ".{2}", "$0:").Substring(0, 17);
                            equipo = new Dictionary<string, string>
                            {
                                {"host", host},
                                {"name", name},
                                {"macAddress", macAddress}
                            };
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                    else
                    {
                        equipo = new Dictionary<string, string>
                        {
                            {"host", host},
                            {"name", string.Empty},
                            {"macAddress", string.Empty}
                        };
                    }
                }

            }
            catch
            {
                // ignored
            }
            return equipo;
        }
    }
}