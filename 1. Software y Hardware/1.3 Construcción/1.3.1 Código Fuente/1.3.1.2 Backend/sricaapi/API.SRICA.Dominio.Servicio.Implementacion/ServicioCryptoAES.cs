using API.SRICA.Dominio.Servicio.Interfaz;
using System;
using System.Security.Cryptography;
using System.Text;

namespace API.SRICA.Dominio.Servicio.Implementacion
{
    /// <summary>
    /// Implementación del servicio de seguridad para el encriptado y desencriptado de datos
    /// (con AES)
    /// </summary>
    public class ServicioCryptoAES : IServicioCryptoAES
    {
        /// <summary>
        /// Proveedor del servicio crypto AES
        /// </summary>
        private AesCryptoServiceProvider _crypto;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public ServicioCryptoAES()
        {
            _crypto = new AesCryptoServiceProvider();
            _crypto.BlockSize = 128;
            _crypto.KeySize = 256;
            _crypto.Mode = CipherMode.CBC;
            _crypto.Padding = PaddingMode.PKCS7;
        }
        /// <summary>
        /// Método que encripta los datos
        /// </summary>
        /// <param name="datosDesencriptados">Datos a encriptar</param>
        /// <param name="claveSecreta">Clave secreta</param>
        /// <param name="vectorIV">Vector IV</param>
        /// <returns>Datos encriptados</returns>
        public string Encriptar(string datosDesencriptados, string claveSecreta, string vectorIV)
        {
            var clave = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(
                claveSecreta));
            var IV = Convert.FromBase64String(vectorIV);
            _crypto.Key = clave;
            _crypto.IV = IV;
            var cryptoTransform = _crypto.CreateEncryptor();
            var encriptado = cryptoTransform.TransformFinalBlock(
                Encoding.UTF8.GetBytes(datosDesencriptados), 0, datosDesencriptados.Length);
            return Convert.ToBase64String(encriptado);
        }
        /// <summary>
        /// Método que desencripta los datos
        /// </summary>
        /// <param name="datosEncriptados">Datos a desencriptar</param>
        /// <param name="claveSecreta">Clave secreta</param>
        /// <param name="vectorIV">Vector IV</param>
        /// <returns>Datos desencriptados</returns>
        public string Desencriptar(string datosEncriptados, string claveSecreta, string vectorIV)
        {
            var clave = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(
                claveSecreta));
            var IV = Convert.FromBase64String(vectorIV);
            _crypto.Key = clave;
            _crypto.IV = IV;
            var cryptoTransform = _crypto.CreateDecryptor();
            var encriptadoBytes = Convert.FromBase64String(datosEncriptados);
            var desencriptado = cryptoTransform.TransformFinalBlock(
                encriptadoBytes, 0, encriptadoBytes.Length);
            return Encoding.UTF8.GetString(desencriptado);
        }
    }
}
