using API.SRICA.Dominio.Servicio.Implementacion;
using API.SRICA.Dominio.Servicio.Interfaz;
using NUnit.Framework;

namespace API.SRICA.Dominio.PruebaUnitaria
{
    /// <summary>
    /// Clase de prueba del servicio de encriptación AES
    /// </summary>
    public class ServicioCryptoAESPrueba
    {
        /// <summary>
        /// Servicio de encriptación AES
        /// </summary>
        private IServicioCryptoAES _servicioCryptoAes;
        /// <summary>
        /// Clave secreta para encriptación
        /// </summary>
        private const string CLAVE_SECRETA = "V@7S6fAl-rkVwxs_HsFZL-oJB.i@jSDt";
        /// <summary>
        /// Vector IV para encriptación
        /// </summary>
        private const string VECTOR_IV = "m2PCs0Ju9a1u2bIPkO3RUQ==";
        /// <summary>
        /// Método que inicializa las pruebas
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _servicioCryptoAes = new ServicioCryptoAES();
        }
        /// <summary>
        /// Método de prueba que realiza el proceso de encriptación de forma correcta
        /// </summary>
        [Test]
        public void PruebaEncriptarCorrecto()
        {
            var valorEsperado = "6N7zn1vIhOnr+7DUH3DWcQ==";
            var resultado = _servicioCryptoAes.Encriptar("hola mundo", CLAVE_SECRETA, VECTOR_IV);
            Assert.AreEqual(valorEsperado, resultado);
        }
        /// <summary>
        /// Método de prueba que realiza el proceso de desencriptación de forma correcta
        /// </summary>
        [Test]
        public void PruebaDesencriptarCorrecto()
        {
            var valorEsperado = "hola mundo";
            var resultado = _servicioCryptoAes.Desencriptar("6N7zn1vIhOnr+7DUH3DWcQ==", CLAVE_SECRETA, VECTOR_IV);
            Assert.AreEqual(valorEsperado, resultado);
        }
    }
}