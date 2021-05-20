using System;

namespace API.SRICA.Dominio.Excepcion
{
    /// <summary>
    /// Excepción personalizada para los ApplicationException del equipo biométrico
    /// </summary>
    public class ExcepcionAplicacionEquipoBiometricoPersonalizada : Exception
    {
        /// <summary>
        /// Código de la excepción
        /// </summary>
        public CodigoExcepcionPersonalizado CodigoExcepcion { get; }
        /// <summary>
        /// Datos del personal que obtiene la excepción
        /// </summary>
        public object PersonalEmpresaExcepcion { get; }
        /// <summary>
        /// Datos del equipo biométrico que obtiene la excepción
        /// </summary>
        public object EquipoBiometricoExcepcion { get; }
        /// <summary>
        /// Foto del personal no registrado en el sistema
        /// </summary>
        public string FotoPersonalNoRegistrado { get; }
        /// <summary>
        /// Si la excepción es producia por un acceso denegado de un personal registrado en el sistema
        /// </summary>
        public bool EsExcepcionPorAccesoDenegadoSinFoto => 
            CodigoExcepcion == CodigoExcepcionPersonalizado.DenegadoSinFoto;
        /// <summary>
        /// Si la excepción es producia por un acceso denegado de un personal no registrado en el sistema
        /// </summary>
        public bool EsExcepcionPorAccesoDenegadoConFoto => 
            CodigoExcepcion == CodigoExcepcionPersonalizado.DenegadoConFoto;
        /// <summary>
        /// Listado de códigos de excepción disponibles
        /// </summary>
        public enum CodigoExcepcionPersonalizado
        {
            DenegadoSinFoto = 1,
            DenegadoConFoto = 2
        }
        /// <summary>
        /// Constructor de la clase por defecto
        /// </summary>
        public ExcepcionAplicacionEquipoBiometricoPersonalizada() { }
        /// <summary>
        /// Constructor de la clase indicando el código de excepción
        /// </summary>
        /// <param name="mensaje">Mensaje de excepción</param>
        /// <param name="codigo">Código de excepción</param>
        public ExcepcionAplicacionEquipoBiometricoPersonalizada(string mensaje, 
            CodigoExcepcionPersonalizado codigo) : base(mensaje) 
        {
            CodigoExcepcion = codigo;
        }
        /// <summary>
        /// Constructor de la clase indicando el código de excepción, el personal y
        /// el equipo biométrico de la acción
        /// </summary>
        /// <param name="mensaje">Mensaje de excepción</param>
        /// <param name="codigo">Código de excepción</param>
        /// <param name="personalEmpresaExcepcion">Datos del personal que obtiene la
        /// excepción</param>
        /// <param name="equipoBiometricoExcepcion">Datos del equipo biométrico que obtiene
        /// la excepción</param>
        /// <param name="fotoPersonalNoRegistrado">Foto del personal no registrado en el sistema</param>
        public ExcepcionAplicacionEquipoBiometricoPersonalizada(string mensaje, 
            CodigoExcepcionPersonalizado codigo, object personalEmpresaExcepcion, 
            object equipoBiometricoExcepcion, string fotoPersonalNoRegistrado = "") : base(mensaje) 
        {
            CodigoExcepcion = codigo;
            PersonalEmpresaExcepcion = personalEmpresaExcepcion;
            EquipoBiometricoExcepcion = equipoBiometricoExcepcion;
            FotoPersonalNoRegistrado = fotoPersonalNoRegistrado;
        }
    }
}