using API.SRICA.Dominio.Excepcion;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace API.SRICA.Infraestructura.ServicioExterno.Implementacion
{
    /// <summary>
    /// Base de microservicios
    /// </summary>
    public class BaseMicroservicio
    {
        /// <summary>
        /// Método que procesa la petición al microservicio indicado
        /// </summary>
        /// <param name="peticion">Contenedor del modelo de petición al microservicio</param>
        /// <param name="mensajeErrorSinConexion">Mensaje de error para microservicios sin conexión</param>
        /// <param name="mensajeError">Mensaje de error para microservicios</param>
        /// <param name="esAccionPorEquipoBiometrico">Si el proceso es generado por el
        /// equipo biométrico</param>
        /// <returns>Respuesta del proceso de petición al microservicio indicado</returns>
        protected ModeloRespuestaMicroservicio ProcesarMicroservicio(ModeloPeticionMicroservicio peticion,
            string mensajeErrorSinConexion, string mensajeError, bool esAccionPorEquipoBiometrico = false)
        {
            try
            {
                using var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls 
                                                       | SecurityProtocolType.Tls11 
                                                       |SecurityProtocolType.Tls12;
                var request = new HttpRequestMessage(HttpMethod.Post, peticion.URL)
                {
                    Content = new StringContent(peticion.Datos, Encoding.UTF8, "application/json")
                };
                using var client = new HttpClient(handler) {BaseAddress = new Uri(peticion.URL)};
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var result = client.SendAsync(request).Result;
                if (!result.IsSuccessStatusCode) throw new Exception(result.Content.ToString());
                var resultado = JsonConvert.DeserializeObject<ModeloRespuestaMicroservicio>(
                    result.Content.ReadAsStringAsync().Result);
                if (!resultado.Error) return resultado;
                if (esAccionPorEquipoBiometrico)
                    throw new ExcepcionEquipoBiometricoPersonalizada(mensajeError,
                        new ExcepcionEquipoBiometricoPersonalizada(resultado.Mensaje));
                throw new ExcepcionPersonalizada(mensajeError,
                    ExcepcionPersonalizada.CodigoExcepcionPersonalizado.ErrorServicio,
                    new ExcepcionPersonalizada(resultado.Mensaje));
            }
            catch (Exception ex)
            {
                if (esAccionPorEquipoBiometrico) 
                    throw new ExcepcionEquipoBiometricoPersonalizada(mensajeErrorSinConexion, ex);
                throw new ExcepcionPersonalizada(mensajeErrorSinConexion,
                    ExcepcionPersonalizada.CodigoExcepcionPersonalizado.ErrorServicio,
                    ex);
            }
        }
    }
}
