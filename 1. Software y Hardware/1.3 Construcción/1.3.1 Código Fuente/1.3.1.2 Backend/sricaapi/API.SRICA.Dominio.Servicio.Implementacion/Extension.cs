using System;
using API.SRICA.Dominio.Entidad.US;
using System.Text.RegularExpressions;

namespace API.SRICA.Dominio.Servicio.Implementacion
{
    /// <summary>
    /// Clase extensión para validaciones comunes de propiedades
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// Método de extensión que valida la cantidad mínima y máxima de caracteres
        /// de una cadena de texto
        /// </summary>
        /// <param name="cadena">Cadena de texto a validar</param>
        /// <param name="cantidadMinimaCaracteres">Cantidad mínima de caracteres</param>
        /// <param name="cantidadMaximaCaracteres">Cantidad máxima de caracteres</param>
        /// <returns>True: éxito o False: fracaso</returns>
        public static bool ValidarCantidadCaracteres(this string cadena, int cantidadMinimaCaracteres,
            int cantidadMaximaCaracteres)
        {
            cadena ??= string.Empty;
            return cantidadMinimaCaracteres <= cadena.Length && cadena.Length <= cantidadMaximaCaracteres;
        }
        /// <summary>
        /// Método de extensión que valida que la cadena de texto solo esté conformado por números
        /// </summary>
        /// <param name="cadena">Cadena de texto a validar</param>
        /// <returns>True: éxito o False: fracaso</returns>
        public static bool ValidarCadenaDeTextoSoloNumeros(this string cadena)
        {
            return int.TryParse(cadena, out _);
        }
        /// <summary>
        /// Método de extensión que valida que la cadena de texto solo esté conformado por letras
        /// </summary>
        /// <param name="cadena">Cadena de texto a validar</param>
        /// <returns>True: éxito o False: fracaso</returns>
        public static bool ValidarCadenaDeTextoSoloLetras(this string cadena)
        {
            cadena ??= string.Empty;
            var expresionSoloLetras = new Regex("^[a-zA-Z ]+$");
            return expresionSoloLetras.IsMatch(cadena);
        }
        /// <summary>
        /// Método de extensión que valida que la cadena de texto solo esté conformado por letras, números 
        /// y/o guiones
        /// </summary>
        /// <param name="cadena">Cadena de texto a validar</param>
        /// <returns>True: éxito o False: fracaso</returns>
        public static bool ValidarCadenaDeTextoSoloLetrasNumerosYOGuiones(this string cadena)
        {
            cadena ??= string.Empty;
            var expresionSoloLetrasYOGuiones = new Regex("^[a-zA-Z0-9?-]+$");
            return expresionSoloLetrasYOGuiones.IsMatch(cadena);
        }
        /// <summary>
        /// Método de extensión que valida que la cadena de texto solo esté conformado por letras, y
        /// sin espacios
        /// </summary>
        /// <param name="cadena">Cadena de texto a validar</param>
        /// <returns>True: éxito o False: fracaso</returns>
        public static bool ValidarCadenaDeTextoSoloLetrasYSinEspacios(this string cadena)
        {
            cadena ??= string.Empty;
            if (string.IsNullOrEmpty(cadena)) return false;
            for (var i = 0; i < cadena.Length; i++)
                if (!char.IsLetter(cadena[i]))
                    return false;
            return true;
        }
        /// <summary>
        /// Método de extensión que valida el correo electrónico
        /// </summary>
        /// <param name="correoElectronico">Correo electrónico a validar</param>
        /// <returns>True: éxito o False: fracaso</returns>
        public static bool ValidarCorreoElectronico(this string correoElectronico)
        {
            correoElectronico ??= string.Empty;
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (!Regex.IsMatch(correoElectronico, expresion)) return false;
            return Regex.Replace(correoElectronico, expresion, string.Empty).Length == 0;
        }
        /// <summary>
        /// Método de extensión que valida el formato de dirección IP (IPv4)
        /// </summary>
        /// <param name="direccionRed">Dirección IP a validar</param>
        /// <returns>True: éxito o False: fracaso</returns>
        public static bool ValidarCadenaTextoConFormatoIP(this string direccionRed)
        {
            direccionRed ??= string.Empty;
            var expresionFormatoIPv4 = new Regex(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$");
            return expresionFormatoIPv4.IsMatch(direccionRed);
        }
        /// <summary>
        /// Método de extensión que remueve el prefijo de una cadena de base64
        /// (por ejemplo; data:image/jpeg;base64,....)
        /// </summary>
        /// <param name="base64">Imagen base64 que se modificará</param>
        /// <returns>Imagen base64 sin prefijo</returns>
        public static string RemoverPrefijoDeBase64(this string base64)
        {
            if (string.IsNullOrEmpty(base64)) return string.Empty;
            var resultado = base64.Split(',');
            return resultado.Length < 2 ? string.Empty : resultado[1];
        }
        /// <summary>
        /// Método que remueve el prefijo .local del nombre de equipo de un raspberry pi
        /// </summary>
        /// <param name="nombreEquipo">Nombre de equipo a modificar</param>
        /// <returns>Nombre de equipo sin el prefijo .local</returns>
        public static string RemoverPrefijoDeNombreDeEquipoDeRaspberry(this string nombreEquipo)
        {
            return string.IsNullOrEmpty(nombreEquipo) 
                ? string.Empty 
                : nombreEquipo.Split('.')[0];
        }
        /// <summary>
        /// Método que obtiene la puerta de enlace de una dirección de red
        /// </summary>
        /// <param name="direccionRed">Dirección de red de referencia</param>
        /// <returns>Dirección de la puerta de enlace de la dirección de red</returns>
        public static string ObtenerPuertaEnlace(this string direccionRed)
        {
            if (string.IsNullOrEmpty(direccionRed)) return string.Empty;
            var lista = direccionRed.Split('.');
            return lista.Length <= 3
                ? string.Empty 
                : $"{lista[0]}.{lista[1]}.{lista[2]}.1";
        }
        /// <summary>
        /// Método de extensión que calcula el nivel de fortaleza de la contraseña
        /// </summary>
        /// <param name="contrasena">Contraseña a calcular</param>
        /// <returns>Nivel de fortaleza de la contraseña</returns>
        public static Usuario.EnumNivelFortalezaContrasena CalcularNivelFortalezaDeContrasena(
            this string contrasena)
        {
            contrasena ??= string.Empty;
            var expresionLetraMinuscula = new Regex(@"[a-z]+");
            var expresionLetraMayuscula = new Regex(@"[A-Z]+");
            var expresionNumeros = new Regex(@"[0-9]+");
            var expresionCaracteresEspecialesLimitados = new Regex(@"[-._]+");
            var expresionCaracteresEspecialesExcepto = new Regex(@"[^-._\w]+");
            var expresionCaracteresEspeciales = new Regex(@"[\W]+");

            if (expresionLetraMinuscula.IsMatch(contrasena) &&
                !expresionLetraMayuscula.IsMatch(contrasena) &&
                !expresionNumeros.IsMatch(contrasena) &&
                !expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.Bajo;
            if (!expresionLetraMinuscula.IsMatch(contrasena) &&
                expresionLetraMayuscula.IsMatch(contrasena) &&
                !expresionNumeros.IsMatch(contrasena) &&
                !expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.Bajo;
            if (!expresionLetraMinuscula.IsMatch(contrasena) && 
                !expresionLetraMayuscula.IsMatch(contrasena) &&
                expresionNumeros.IsMatch(contrasena) &&
                !expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.Bajo;
            if (!expresionLetraMinuscula.IsMatch(contrasena) &&
                !expresionLetraMayuscula.IsMatch(contrasena) &&
                !expresionNumeros.IsMatch(contrasena) &&
                expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.Bajo;

            if (expresionLetraMinuscula.IsMatch(contrasena) &&
                expresionLetraMayuscula.IsMatch(contrasena) &&
                !expresionNumeros.IsMatch(contrasena) &&
                !expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.MedioBajo;
            if (expresionLetraMinuscula.IsMatch(contrasena) &&
                !expresionLetraMayuscula.IsMatch(contrasena) &&
                expresionNumeros.IsMatch(contrasena) &&
                !expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.MedioBajo;
            if (expresionLetraMinuscula.IsMatch(contrasena) &&
                !expresionLetraMayuscula.IsMatch(contrasena) &&
                !expresionNumeros.IsMatch(contrasena) &&
                expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.MedioBajo;
            if (!expresionLetraMinuscula.IsMatch(contrasena) &&
                expresionLetraMayuscula.IsMatch(contrasena) &&
                expresionNumeros.IsMatch(contrasena) &&
                !expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.MedioBajo;
            if (!expresionLetraMinuscula.IsMatch(contrasena) &&
                expresionLetraMayuscula.IsMatch(contrasena) &&
                !expresionNumeros.IsMatch(contrasena) &&
                expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.MedioBajo;
            if (!expresionLetraMinuscula.IsMatch(contrasena) &&
                !expresionLetraMayuscula.IsMatch(contrasena) &&
                expresionNumeros.IsMatch(contrasena) &&
                expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.MedioBajo;

            if (expresionLetraMinuscula.IsMatch(contrasena) &&
                expresionLetraMayuscula.IsMatch(contrasena) &&
                expresionNumeros.IsMatch(contrasena) &&
                !expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.Medio;
            if (expresionLetraMinuscula.IsMatch(contrasena) &&
                expresionLetraMayuscula.IsMatch(contrasena) &&
                !expresionNumeros.IsMatch(contrasena) &&
                expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.Medio;
            if (!expresionLetraMinuscula.IsMatch(contrasena) &&
                expresionLetraMayuscula.IsMatch(contrasena) &&
                expresionNumeros.IsMatch(contrasena) &&
                expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.Medio;
            if (expresionLetraMinuscula.IsMatch(contrasena) &&
                !expresionLetraMayuscula.IsMatch(contrasena) &&
                expresionNumeros.IsMatch(contrasena) &&
                expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.Medio;

            if (expresionLetraMinuscula.IsMatch(contrasena) &&
                     expresionLetraMayuscula.IsMatch(contrasena) &&
                     expresionNumeros.IsMatch(contrasena) &&
                     expresionCaracteresEspecialesLimitados.IsMatch(contrasena) &&
                     !expresionCaracteresEspecialesExcepto.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.MedioAlto;

            if (expresionLetraMinuscula.IsMatch(contrasena) &&
                     expresionLetraMayuscula.IsMatch(contrasena) &&
                     expresionNumeros.IsMatch(contrasena) &&
                     expresionCaracteresEspeciales.IsMatch(contrasena))
                return Usuario.EnumNivelFortalezaContrasena.Alto;

            return Usuario.EnumNivelFortalezaContrasena.Bajo;
        }
    }
}
