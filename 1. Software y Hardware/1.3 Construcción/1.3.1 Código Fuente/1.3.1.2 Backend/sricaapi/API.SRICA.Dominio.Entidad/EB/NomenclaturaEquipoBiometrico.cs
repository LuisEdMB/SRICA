namespace API.SRICA.Dominio.Entidad.EB
{
    /// <summary>
    /// Entidad Nomenclatura Equipo Biométrico que representa a la tabla 
    /// EB_NOMENCLATURA_EQUIPO_BIOMETRICO
    /// </summary>
    public class NomenclaturaEquipoBiometrico
    {
        /// <summary>
        /// Código interno de la nomenclatura (primary key)
        /// </summary>
        public int CodigoNomenclatura { get; private set; }
        /// <summary>
        /// Decripción de la nomenclatura
        /// </summary>
        public string DescripcionNomenclatura { get; private set; }
        /// <summary>
        /// Indicador que representa la nomenclatura que se asignará a sus relaciones cuando ésta sea
        /// inhabilitada
        /// </summary>
        public bool IndicadorRegistroParaSinAsignacion { get; private set; }
        /// <summary>
        /// Indicador de estado de la nomenclatura (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; private set; }
        /// <summary>
        /// Código de nomenclatura que representa a una relación no asignada (se asigna a sus relaciones
        /// cuando la nomenclatura es inhabilitada).
        /// </summary>
        public const int CodigoNomenclaturaNoAsignado = 1;
        /// <summary>
        /// Si la nomenclatura tiene el estado ACTIVO
        /// </summary>
        public bool EsNomenclaturaActivo
        {
            get
            {
                return IndicadorEstado;
            }
        }
        /// <summary>
        /// Si la nomenclatura tiene el estado INACTIVO
        /// </summary>
        public bool EsNomenclaturaInactivo
        {
            get
            {
                return !IndicadorEstado;
            }
        }
        /// <summary>
        /// Método estático que crea la entidad nomenclatura para equipos biométricos
        /// </summary>
        /// <param name="descripcionNomenclatura">Descripción de la nomenclatura</param>
        /// <returns>Nomenclatura creada</returns>
        public static NomenclaturaEquipoBiometrico CrearNomenclatura(string descripcionNomenclatura)
        {
            return new NomenclaturaEquipoBiometrico
            {
                DescripcionNomenclatura = descripcionNomenclatura.ToUpper(),
                IndicadorRegistroParaSinAsignacion = false,
                IndicadorEstado = true
            };
        }
        /// <summary>
        /// Método que modifica la entidad nomenclatura para equipos biométricos
        /// </summary>
        /// <param name="descripcionNomenclatura">Descripción de la nomenclatura</param>
        public void ModificarNomenclatura(string descripcionNomenclatura)
        {
            DescripcionNomenclatura = descripcionNomenclatura.ToUpper();
        }
        public void InhabilitarNomenclatura()
        {
            IndicadorEstado = false;
        }
        public void HabilitarNomenclatura()
        {
            IndicadorEstado = true;
        }
    }
}
