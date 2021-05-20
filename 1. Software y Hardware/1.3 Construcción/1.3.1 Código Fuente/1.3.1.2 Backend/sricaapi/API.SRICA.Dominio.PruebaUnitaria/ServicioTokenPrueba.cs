using System;
using API.SRICA.Dominio.Entidad.US;
using API.SRICA.Dominio.Servicio.Implementacion;
using API.SRICA.Dominio.Servicio.Interfaz;
using NUnit.Framework;

namespace API.SRICA.Dominio.PruebaUnitaria
{
    /// <summary>
    /// Clase de prueba del servicio de tokens
    /// </summary>
    public class ServicioTokenPrueba
    {
        /// <summary>
        /// Servicio de tokens
        /// </summary>
        private IServicioToken _servicioToken;
        /// <summary>
        /// Audiencia del cliente
        /// </summary>
        private string CLIENTE_AUDIENCIA = "qVc_R7u7u@IB-dFaV@a6rgc-s4_aC7Fu";
        /// <summary>
        /// Audiencia del servicio API
        /// </summary>
        private string API_AUDIENCIA = "qVc_R7u7u@IB-dFaV@a6rgc-s4_aC7Fu";
        /// <summary>
        /// Clave secreta para encriptación
        /// </summary>
        private const string CLAVE_SECRETA = "V@7S6fAl-rkVwxs_HsFZL-oJB.i@jSDt";
        /// <summary>
        /// Método que inicializa las pruebas
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _servicioToken = new ServicioToken();
            
        }
        /// <summary>
        /// Método de prueba que valida la audiencia del cliente con el servicio API,
        /// de forma correcta
        /// </summary>
        [Test]
        public void PruebaValidarAudienciaCorrecto()
        {
            _servicioToken.ValidarAudiencia(CLIENTE_AUDIENCIA, API_AUDIENCIA);
        }
        /// <summary>
        /// Método de prueba que valida la audiencia del cliente con el servicio API,
        /// donde la audiencia del cliente y del servicio API no son iguales
        /// </summary>
        [Test]
        public void PruebaValidarAudienciaIncorrecto()
        {
            var mensaje = Assert.Throws<Exception>(() =>
                _servicioToken.ValidarAudiencia("adasd", API_AUDIENCIA)).Message;
            Assert.AreEqual("La audiencia del cliente no está permitida.", mensaje);
        }
        /// <summary>
        /// Método de prueba que genera un token
        /// </summary>
        [Test]
        public void PruebaGenerarToken()
        {
            var rolUsuario = new RolUsuario();
            var rolUsuarioType = typeof(RolUsuario);
            rolUsuarioType.GetProperty("DescripcionRolUsuario").SetValue(rolUsuario, "Prueba");
            var usuario = new Usuario();
            var usuarioType = typeof(Usuario);
            usuarioType.GetProperty("CodigoUsuario").SetValue(usuario, 1);
            usuarioType.GetProperty("RolUsuario").SetValue(usuario, rolUsuario);
            var token = _servicioToken.GenerarToken(usuario, CLAVE_SECRETA, "test",
                "audiencia");
            Assert.IsNotEmpty(token);
        }
        /// <summary>
        /// Método de prueba que refresca un token vencido
        /// </summary>
        [Test]
        public void PruebaRefrescarToken()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIzMjJkY2IxYS05Z" +
                "TU0LTQ1NGEtODQ1MS0zYmM2MDZlYjRkNDIiLCJVc3VhcmlvIjoiMSIsImh0dHA6Ly9zY2hlbWFz" +
                "Lm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlBSVUVCQS" +
                "IsIm5iZiI6MTYxMzc4MjQ3OCwiZXhwIjoxNjEzNzgyNTk4LCJpc3MiOiJ0ZXN0IiwiYXVkIjoi" +
                "YXVkaWVuY2lhIn0.FdTzoBEKPiSjTI9vxJDU2z0iyOLb6Rix9mXDxHLsLsM";
            var tokenRefresh = _servicioToken.RefrescarToken(token, CLAVE_SECRETA, "test",
                "audiencia");
            Assert.IsNotEmpty(tokenRefresh);
        }
    }
}