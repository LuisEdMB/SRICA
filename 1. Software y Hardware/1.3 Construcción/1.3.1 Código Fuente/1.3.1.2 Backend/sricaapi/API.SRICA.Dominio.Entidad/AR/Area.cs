using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.PE;
using API.SRICA.Dominio.Entidad.SE;
using System.Collections.Generic;

namespace API.SRICA.Dominio.Entidad.AR
{
    /// <summary>
    /// Entidad Area que representa a la tabla AR_AREA
    /// </summary>
    public class Area
    {
        /// <summary>
        /// Código interno del área (primary key)
        /// </summary>
        public int CodigoArea { get; private set; }
        /// <summary>
        /// Código de la sede asignado al área (relación con SE_SEDE)
        /// </summary>
        public int CodigoSede { get; private set; }
        /// <summary>
        /// Decripción del área
        /// </summary>
        public string DescripcionArea { get; private set; }
        /// <summary>
        /// Indicador que representa el área que se asignará a sus relaciones cuando ésta sea
        /// inhabilitada
        /// </summary>
        public bool IndicadorRegistroParaSinAsignacion { get; private set; }
        /// <summary>
        /// Indicador de estado del área (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; private set; }
        /// <summary>
        /// Sede que está relacionada con el área
        /// </summary>
        public virtual Sede Sede { get; private set; }
        /// <summary>
        /// Equipo biométricos que están relacionados con el área
        /// </summary>
        public virtual List<EquipoBiometrico> EquiposBiometricos { get; private set; }
        /// <summary>
        /// Asignaciones de áreas al personal de la empresa
        /// </summary>
        public virtual List<PersonalEmpresaXArea> PersonalAsignado { get; private set; }
        /// <summary>
        /// Código de área que representa a una relación no asignada (se asigna a sus relaciones
        /// cuando el área es inhabilitada).
        /// </summary>
        public const int CodigoAreaNoAsignado = 1;
        /// <summary>
        /// Si el área tiene el estado ACTIVO
        /// </summary>
        public bool EsAreaActivo
        {
            get
            {
                return IndicadorEstado;
            }
        }
        /// <summary>
        /// Si el área tiene el estado INACTIVO
        /// </summary>
        public bool EsAreaInactivo
        {
            get
            {
                return !IndicadorEstado;
            }
        }
        /// <summary>
        /// Método estático que crea la entidad área
        /// </summary>
        /// <param name="sede">Sede del área a crear</param>
        /// <param name="descripcionArea">Descripción del área</param>
        /// <returns>Área creada</returns>
        public static Area CrearArea(Sede sede, string descripcionArea)
        {
            return new Area
            {
                CodigoSede = sede.CodigoSede,
                DescripcionArea = descripcionArea,
                IndicadorRegistroParaSinAsignacion = false,
                IndicadorEstado = true
            };
        }
        /// <summary>
        /// Método que modifica la entidad área
        /// </summary>
        /// <param name="sede">Sede del área a modificar</param>
        /// <param name="descripcionArea">Descripción del área</param>
        public void ModificarArea(Sede sede, string descripcionArea)
        {
            CodigoSede = sede.CodigoSede;
            DescripcionArea = descripcionArea;
        }
        /// <summary>
        /// Método que inhabilita la entidad área
        /// </summary>
        public void InhabilitarArea()
        {
            IndicadorEstado = false;
        }
        /// <summary>
        /// Método que habilita la entidad área
        /// </summary>
        public void HabilitarArea()
        {
            IndicadorEstado = true;
        }
        /// <summary>
        /// Método que asigna el registro "Sin asignación" de sede al área
        /// </summary>
        public void AsignarSedeSinAsignacion()
        {
            CodigoSede = Sede.CodigoSedeNoAsignado;
        }
    }
}
