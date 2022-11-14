using API.SRICA.Dominio.Entidad.US;
using API.SRICA.Dominio.Servicio.Implementacion;
using NUnit.Framework;

namespace API.SRICA.Dominio.PruebaUnitaria
{
    /// <summary>
    /// Clase de prueba para los métodos de extensiones de tipos de datos
    /// </summary>
    public class ExtensionPrueba
    {
        /// <summary>
        /// Método de prueba que valida una cadena de texto según un rango de caracteres
        /// </summary>
        /// <param name="cadena">Cadena a validar</param>
        /// <param name="cantidadMinimaCaracteres">Cantidad mínima de caracteres</param>
        /// <param name="cantidadMaximaCaracteres">Cantidad máxima de caracteres</param>
        /// <param name="resultadoEsperado">Resultado esperado</param>
        [Test]
        [TestCase("texto", 1, 5, true)]
        [TestCase("texto", 1, 2, false)]
        [TestCase("texto123", 1, 4, false)]
        [TestCase("", 1, 5, false)]
        [TestCase("", 0, 5, true)]
        [TestCase(null, 0, 5, true)]
        [TestCase(null, 1, 5, false)]
        public void PruebaValidarCantidadCaracteres(string cadena, int cantidadMinimaCaracteres,
            int cantidadMaximaCaracteres, bool resultadoEsperado)
        {
            var respuesta = cadena.ValidarCantidadCaracteres(cantidadMinimaCaracteres,
                cantidadMaximaCaracteres);
            Assert.AreEqual(resultadoEsperado, respuesta);
        }
        /// <summary>
        /// Método de prueba que valida una cadena de texto con solo números
        /// </summary>
        /// <param name="cadena">Cadena a validar</param>
        /// <param name="resultadoEsperado">Resultado esperado</param>
        [Test]
        [TestCase("texto", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("text1", false)]
        [TestCase("1234 ", true)]
        [TestCase("1234", true)]
        public void PruebaValidarCadenaDeTextoSoloNumeros(string cadena, bool resultadoEsperado)
        {
            var respuesta = cadena.ValidarCadenaDeTextoSoloNumeros();
            Assert.AreEqual(resultadoEsperado, respuesta);
        }
        /// <summary>
        /// Método de prueba que valida una cadena de texto con solo texto
        /// </summary>
        /// <param name="cadena">Cadena a validar</param>
        /// <param name="resultadoEsperado">Resultado esperado</param>
        [Test]
        [TestCase("texto", true)]
        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("texto123", false)]
        [TestCase("123 ", false)]
        [TestCase("123", false)]
        [TestCase("texto--", false)]
        public void PruebaValidarCadenaDeTextoSoloLetras(string cadena, bool resultadoEsperado)
        {
            var respuesta = cadena.ValidarCadenaDeTextoSoloLetras();
            Assert.AreEqual(resultadoEsperado, respuesta);
        }
        /// <summary>
        /// Método de prueba que valida una cadena de texto con solo texto, números y/o guiones
        /// </summary>
        /// <param name="cadena">Cadena a validar</param>
        /// <param name="resultadoEsperado">Resultado esperado</param>
        [Test]
        [TestCase("texto", true)]
        [TestCase("texto123", true)]
        [TestCase("texto12-", true)]
        [TestCase("123´+{", false)]
        [TestCase("+´++{+", false)]
        [TestCase("", false)]
        [TestCase("123tr ", false)]
        [TestCase(null, false)]
        public void PruebaValidarCadenaDeTextoSoloLetrasNumerosYOGuiones(string cadena,
            bool resultadoEsperado)
        {
            var respuesta = cadena.ValidarCadenaDeTextoSoloLetrasNumerosYOGuiones();
            Assert.AreEqual(resultadoEsperado, respuesta);
        }
        /// <summary>
        /// Método de prueba que valida una cadena de texto con solo texto y sin espacios
        /// </summary>
        /// <param name="cadena">Cadena a validar</param>
        /// <param name="resultadoEsperado">Resultado esperado</param>
        [Test]
        [TestCase("texto", true)]
        [TestCase("123", false)]
        [TestCase("texto ", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("texto123", false)]
        public void PruebaValidarCadenaDeTextoSoloLetrasYSinEspacios(string cadena,
            bool resultadoEsperado)
        {
            var respuesta = cadena.ValidarCadenaDeTextoSoloLetrasYSinEspacios();
            Assert.AreEqual(resultadoEsperado, respuesta);
        }
        /// <summary>
        /// Método de prueba que valida un correo electrónico
        /// </summary>
        /// <param name="correo">Correo electrónico a validar</param>
        /// <param name="resultadoEsperado">Resultado esperado</param>
        [Test]
        [TestCase("texto", false)]
        [TestCase("texto@gmail", false)]
        [TestCase("texto@gmail.com", true)]
        [TestCase("texto123_23@gmail.com", true)]
        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("texto 2@gmail.com", false)]
        public void PruebaValidarCorreoElectronico(string correo, bool resultadoEsperado)
        {
            var respuesta = correo.ValidarCorreoElectronico();
            Assert.AreEqual(resultadoEsperado, respuesta);
        }
        /// <summary>
        /// Método de prueba que valida una dirección de red IP
        /// </summary>
        /// <param name="direccionRed">Dirección IP a validar</param>
        /// <param name="resultadoEsperado">Resultado esperado</param>
        [Test]
        [TestCase("texto", false)]
        [TestCase("123", false)]
        [TestCase("123.123", false)]
        [TestCase("123.123.254", false)]
        [TestCase("123.123.254.123", true)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void PruebaValidarCadenaTextoConFormatoIP(string direccionRed, bool resultadoEsperado)
        {
            var respuesta = direccionRed.ValidarCadenaTextoConFormatoIP();
            Assert.AreEqual(resultadoEsperado, respuesta);
        }
        /// <summary>
        /// Método de prueba que remueve el prefijo base64 de una cadena de texto
        /// </summary>
        /// <param name="cadena">Cadena de texto a utilizar</param>
        /// <param name="resultadoEsperado">Resultado esperado</param>
        [Test]
        [TestCase("prueba", "")]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("base64,asdawd", "asdawd")]
        [TestCase("64b,qwd23+'", "qwd23+'")]
        public void PruebaRemoverPrefijoDeBase64(string cadena, string resultadoEsperado)
        {
            var respuesta = cadena.RemoverPrefijoDeBase64();
            Assert.AreEqual(resultadoEsperado, respuesta);
        }
        /// <summary>
        /// Método de prueba que obtiene la puerta de enlace en base a una dirección IP
        /// </summary>
        /// <param name="direccionRed">Dirección de red a utilizar</param>
        /// <param name="resultadoEsperado">Resultado esperado</param>
        [Test]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("123456", "")]
        [TestCase("12.254.63", "")]
        [TestCase("12.254.63.63", "12.254.63.1")]
        public void PruebaObtenerPuertaEnlace(string direccionRed, string resultadoEsperado)
        {
            var respuesta = direccionRed.ObtenerPuertaEnlace();
            Assert.AreEqual(resultadoEsperado, respuesta);
        }
        /// <summary>
        /// Método de prueba que calcula el nivel de fortaleza de una contraseña
        /// </summary>
        /// <param name="contrasena">Contraseña a validar</param>
        /// <param name="resultadoEsperado">Resultado esperado</param>
        [Test]
        [TestCase("123", Usuario.EnumNivelFortalezaContrasena.Bajo)]
        [TestCase("123.", Usuario.EnumNivelFortalezaContrasena.MedioBajo)]
        [TestCase("123.-", Usuario.EnumNivelFortalezaContrasena.MedioBajo)]
        [TestCase("123a.-", Usuario.EnumNivelFortalezaContrasena.Medio)]
        [TestCase("123aS.-", Usuario.EnumNivelFortalezaContrasena.MedioAlto)]
        [TestCase("123aSSa*.-", Usuario.EnumNivelFortalezaContrasena.Alto)]
        [TestCase("", Usuario.EnumNivelFortalezaContrasena.Bajo)]
        [TestCase(null, Usuario.EnumNivelFortalezaContrasena.Bajo)]
        public void PruebaCalcularNivelFortalezaDeContrasena(string contrasena,
            Usuario.EnumNivelFortalezaContrasena resultadoEsperado)
        {
            var respuesta = contrasena.CalcularNivelFortalezaDeContrasena();
            Assert.AreEqual(resultadoEsperado, respuesta);
        }
    }
}