namespace API.SRICA.Aplicacion.DTO
{
    /// <summary>
    /// DTO para el equipo biométrico
    /// </summary>
    public class DtoEquipoBiometrico
    {
        /// <summary>
        /// Código del equipo biométrico
        /// </summary>
        public string CodigoEquipoBiometrico { get; set; }
        /// <summary>
        /// Código de la nomenclatura asignado al equipo biométrico (relación con 
        /// EB_NOMENCLATURA_EQUIPO_BIOMETRICO)
        /// </summary>
        public string CodigoNomenclatura { get; set; }
        /// <summary>
        /// Indicador que representa que la nomenclatura es "Sin Asignación"
        /// </summary>
        public bool IndicadorRegistroNomenclaturaParaSinAsignacion { get; set; }
        /// <summary>
        /// Descripción de la nomenclatura asignada al equipo biométrico
        /// </summary>
        public string DescripcionNomenclatura { get; set; }
        /// <summary>
        /// Nombre del equipo biométrico
        /// </summary>
        public string NombreEquipoBiometrico { get; set; }
        /// <summary>
        /// Dirección de red del equipo biométrico
        /// </summary>
        public string DireccionRedEquipoBiometrico { get; set; }
        /// <summary>
        /// Código del área asignado al equipo biométrico (relación con 
        /// AR_AREA)
        /// </summary>
        public string CodigoArea { get; set; }
        /// <summary>
        /// Indicador que representa que el área es "Sin Asignación"
        /// </summary>
        public bool IndicadorRegistroAreaParaSinAsignacion { get; set; }
        /// <summary>
        /// Descripción del área relacionada con el equipo biométrico
        /// </summary>
        public string DescripcionArea { get; set; }
        /// <summary>
        /// Código de la sede relacionada con el área del equipo biométrico
        /// </summary>
        public string CodigoSede { get; set; }
        /// <summary>
        /// Indicador que representa que la sede es "Sin Asignación"
        /// </summary>
        public bool IndicadorRegistroSedeParaSinAsignacion { get; set; }
        /// <summary>
        /// Descripción de la sede relacionada con el área del equipo biométrico
        /// </summary>
        public string DescripcionSede { get; set; }
        /// <summary>
        /// Dirección física (MAC) del equipo biométrico
        /// </summary>
        public string DireccionFisicaEquipoBiometrico { get; set; }
        /// <summary>
        /// Indicador de estado del equipo biométrico (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; set; }
    }
}
