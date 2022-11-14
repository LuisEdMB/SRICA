using System;

namespace API.SRICA.Dominio.Excepcion
{
    /// <summary>
    /// Excepción personalizada para los ApplicationException del sistema
    /// </summary>
    public class ExcepcionAplicacionPersonalizada : Exception
    {
        /// <summary>
        /// Código de la excepción
        /// </summary>
        public CodigoExcepcionPersonalizado CodigoExcepcion { get; }
        /// <summary>
        /// Listado de códigos de excepción disponibles
        /// </summary>
        public enum CodigoExcepcionPersonalizado
        {
            AdvertenciaSimple,
            AdvertenciaSimpleConLogOutUsuario
        }
        /// <summary>
        /// Constructor de la clase por defecto
        /// </summary>
        public ExcepcionAplicacionPersonalizada() { }
        /// <summary>
        /// Constructor de la clase indicando el código de excepción
        /// </summary>
        /// <param name="mensaje">Mensaje de excepción</param>
        /// <param name="codigo">Código de excepción</param>
        public ExcepcionAplicacionPersonalizada(string mensaje, CodigoExcepcionPersonalizado codigo = 0) 
            : base(mensaje) 
        {
            CodigoExcepcion = codigo;
        }
        /// <summary>
        /// Constructor de la clase por defecto
        /// </summary>
        /// <param name="mensaje">Mensaje de excepción</param>
        /// <param name="inner">InnerException</param>
        public ExcepcionAplicacionPersonalizada(string mensaje, Exception inner) 
            : base(mensaje, inner) { }
    }
}
