using System;

namespace API.SRICA.Dominio.Excepcion
{
    /// <summary>
    /// Excepción personalizada para los Exception del sistema
    /// </summary>
    public class ExcepcionPersonalizada : Exception
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
            ErrorServicio
        }
        /// <summary>
        /// Constructor de la clase por defecto
        /// </summary>
        public ExcepcionPersonalizada() { }
        /// <summary>
        /// Constructor de la clase indicando el código de excepción
        /// </summary>
        /// <param name="mensaje">Mensaje de excepción</param>
        /// <param name="codigo">Código de excepción</param>
        /// <param name="inner">InnerException</param>
        public ExcepcionPersonalizada(string mensaje, CodigoExcepcionPersonalizado codigo = 0, 
            Exception inner = null)
            : base(mensaje, inner)
        {
            CodigoExcepcion = codigo;
        }
    }
}
