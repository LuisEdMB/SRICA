using System;

namespace API.SRICA.Dominio.Excepcion
{
    /// <summary>
    /// Excepción personalizada para los Exception del equipo biométrico
    /// </summary>
    public class ExcepcionEquipoBiometricoPersonalizada : Exception
    {
        /// <summary>
        /// Listado de códigos de excepción disponibles
        /// </summary>
        public enum CodigoExcepcionPersonalizado
        {
            Error = 0
        }
        /// <summary>
        /// Código de la excepción
        /// </summary>
        public CodigoExcepcionPersonalizado CodigoExcepcion => CodigoExcepcionPersonalizado.Error;
        /// <summary>
        /// Datos del personal que obtiene la excepción
        /// </summary>
        public object PersonalEmpresaExcepcion { get; }
        /// <summary>
        /// Datos del equipo biométrico que obtiene la excepción
        /// </summary>
        public object EquipoBiometricoExcepcion { get; }
        /// <summary>
        /// Constructor de la clase por defecto
        /// </summary>
        public ExcepcionEquipoBiometricoPersonalizada() { }
        /// <summary>
        /// Constructor de la clase indicando el código de excepción
        /// </summary>
        /// <param name="mensaje">Mensaje de excepción</param>
        /// <param name="inner">InnerException</param>
        public ExcepcionEquipoBiometricoPersonalizada(string mensaje, Exception inner = null) 
            : base(mensaje, inner)
        { }
        /// <summary>
        /// Constructor de la clase indicando el código de excepción
        /// </summary>
        /// <param name="mensaje">Mensaje de excepción</param>
        /// <param name="personalEmpresaExcepcion">Datos del personal que obtiene la
        /// excepción</param>
        /// <param name="equipoBiometricoExcepcion">Datos del equipo biométrico que obtiene
        /// la excepción</param>
        /// <param name="inner">InnerException</param>
        public ExcepcionEquipoBiometricoPersonalizada(string mensaje, object personalEmpresaExcepcion,
            object equipoBiometricoExcepcion, Exception inner = null) : base(mensaje, inner)
        {
            PersonalEmpresaExcepcion = personalEmpresaExcepcion;
            EquipoBiometricoExcepcion = equipoBiometricoExcepcion;
        }
    }
}