namespace API.SRICA.Aplicacion.DTO
{
    /// <summary>
    /// DTO de la nomenclatura para equipos biométricos
    /// </summary>
    public class DtoNomenclaturaEquipoBiometrico
    {
        /// <summary>
        /// Código de la nomenclatura
        /// </summary>
        public string CodigoNomenclatura { get; set; }
        /// <summary>
        /// Decripción de la nomenclatura
        /// </summary>
        public string DescripcionNomenclatura { get; set; }
        /// <summary>
        /// Indicador que representa la nomenclatura que se asignará a sus relaciones cuando ésta sea
        /// inhabilitada
        /// </summary>
        public bool IndicadorRegistroParaSinAsignacion { get; set; }
        /// <summary>
        /// Indicador de estado de la nomenclatura (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; set; }
    }
}
