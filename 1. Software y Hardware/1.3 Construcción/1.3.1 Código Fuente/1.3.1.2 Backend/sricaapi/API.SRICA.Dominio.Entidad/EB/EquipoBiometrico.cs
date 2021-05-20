using API.SRICA.Dominio.Entidad.AR;

namespace API.SRICA.Dominio.Entidad.EB
{
    /// <summary>
    /// Entidad Equipo Biométrico que representa a la tabla EB_EQUIPO_BIOMETRICO
    /// </summary>
    public class EquipoBiometrico
    {
        /// <summary>
        /// Código interno del equipo biométrico (primary key)
        /// </summary>
        public int CodigoEquipoBiometrico { get; private set; }
        /// <summary>
        /// Código de la nomenclatura asignado al equipo biométrico (relación con 
        /// EB_NOMENCLATURA_EQUIPO_BIOMETRICO)
        /// </summary>
        public int CodigoNomenclatura { get; private set; }
        /// <summary>
        /// Nombre del equipo biométrico
        /// </summary>
        public string NombreEquipoBiometrico { get; private set; }
        /// <summary>
        /// Nombre anterior del equipo biométrico
        /// </summary>
        public string NombreEquipoBiometricoAnterior { get; private set; }
        /// <summary>
        /// Dirección de red del equipo biométrico
        /// </summary>
        public string DireccionRedEquipoBiometrico { get; private set; }
        /// <summary>
        /// Dirección de red anterior del equipo biométrico
        /// </summary>
        public string DireccionRedEquipoBiometricoAnterior { get; private set; }
        /// <summary>
        /// Código del área asignado al equipo biométrico (relación con 
        /// AR_AREA)
        /// </summary>
        public int CodigoArea { get; private set; }
        /// <summary>
        /// Dirección física (MAC) del equipo biométrico
        /// </summary>
        public string DireccionFisicaEquipoBiometrico { get; private set; }
        /// <summary>
        /// Indicador de estado del equipo biométrico (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; private set; }
        /// <summary>
        /// Nomenclatura que está relacionada con el equipo biométrico
        /// </summary>
        public virtual NomenclaturaEquipoBiometrico Nomenclatura { get; private set; }
        /// <summary>
        /// Área que está relacionada con el equipo biométrico
        /// </summary>
        public virtual Area Area { get; private set; }
        /// <summary>
        /// Si el equipo biométrico tiene el estado ACTIVO
        /// </summary>
        public bool EsEquipoBiometricoActivo
        {
            get
            {
                return IndicadorEstado;
            }
        }
        /// <summary>
        /// Si el equipo biométrico tiene el estado INACTIVO
        /// </summary>
        public bool EsEquipoBiometricoInactivo
        {
            get
            {
                return !IndicadorEstado;
            }
        }
        /// <summary>
        /// Si el nombre del equipo biométrico ha cambiado
        /// </summary>
        public bool EsNombreEquipoBiometricoCambiado
        {
            get
            {
                return !NombreEquipoBiometrico.Equals(NombreEquipoBiometricoAnterior);
            }
        }
        /// <summary>
        /// Si la dirección de red del equipo biométrico ha cambiado
        /// </summary>
        public bool EsDireccionRedEquipoBiometricoCambiado
        {
            get
            {
                return !DireccionRedEquipoBiometrico.Equals(DireccionRedEquipoBiometricoAnterior);
            }
        }
        /// <summary>
        /// Método estático que crea la entidad equipo biométrico
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura del equipo biométrico</param>
        /// <param name="nombreEquipoBiometrico">Nombre del equipo biométrico</param>
        /// <param name="direccionRed">Dirección IP del equipo biométrico</param>
        /// <param name="direccionMAC">Dirección MAC del equipo biométrico (encriptado)</param>
        /// <returns>Equipo biométrico creado</returns>
        public static EquipoBiometrico CrearEquipoBiometrico(NomenclaturaEquipoBiometrico nomenclatura,
            string nombreEquipoBiometrico, string direccionRed, string direccionMAC)
        {
            return new EquipoBiometrico
            {
                CodigoNomenclatura = nomenclatura.CodigoNomenclatura,
                CodigoArea = Area.CodigoAreaNoAsignado,
                NombreEquipoBiometrico = nombreEquipoBiometrico,
                DireccionRedEquipoBiometrico = direccionRed,
                DireccionFisicaEquipoBiometrico = direccionMAC,
                IndicadorEstado = true
            };
        }
        /// <summary>
        /// Método que modifica el equipo biométrico
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura del equipo biométrico</param>
        /// <param name="nombreEquipoBiometrico">Nombre del equipo biométrico</param>
        /// <param name="direccionRed">Dirección IP del equipo biométrico</param>
        /// <param name="area">Área del equipo biométrico</param>
        public void ModificarEquipoBiometrico(NomenclaturaEquipoBiometrico nomenclatura, string nombreEquipoBiometrico, 
            string direccionRed, Area area)
        {
            CodigoNomenclatura = nomenclatura.CodigoNomenclatura;
            NombreEquipoBiometricoAnterior = NombreEquipoBiometrico;
            NombreEquipoBiometrico = nomenclatura.DescripcionNomenclatura + "-" + nombreEquipoBiometrico;
            DireccionRedEquipoBiometricoAnterior = DireccionRedEquipoBiometrico;
            DireccionRedEquipoBiometrico = direccionRed;
            CodigoArea = area.CodigoArea;
        }
        /// <summary>
        /// Método que inhabilita el equipo biométrico
        /// </summary>
        public void InhabilitarEquipoBiometrico()
        {
            IndicadorEstado = false;
        }
        /// <summary>
        /// Método que habilita el equipo biométrico
        /// </summary>
        public void HabilitarEquipoBiometrico()
        {
            IndicadorEstado = true;
        }
        /// <summary>
        /// Método que asigna el registro "Sin asignación" de área al equipo biométrico
        /// </summary>
        public void AsignarAreaSinAsignacion()
        {
            CodigoArea = Area.CodigoAreaNoAsignado;
        }
        /// <summary>
        /// Método que asigna el registro "Sin asignación" de nomenclatura al equipo biométrico
        /// </summary>
        public void AsignarNomenclaturaSinAsignacion()
        {
            CodigoNomenclatura = NomenclaturaEquipoBiometrico.CodigoNomenclaturaNoAsignado;
        }
    }
}
