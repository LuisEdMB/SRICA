using API.SRICA.Dominio.Entidad.AR;
using System.Collections.Generic;

namespace API.SRICA.Dominio.Entidad.SE
{
    /// <summary>
    /// Entidad Sede que representa a la tabla SE_SEDE
    /// </summary>
    public class Sede
    {
        /// <summary>
        /// Código interno de la sede (primary key)
        /// </summary>
        public int CodigoSede { get; private set; }
        /// <summary>
        /// Decripción de la sede
        /// </summary>
        public string DescripcionSede { get; private set; }
        /// <summary>
        /// Indicador que representa la sede que se asignará a sus relaciones cuando ésta sea
        /// inhabilitada
        /// </summary>
        public bool IndicadorRegistroParaSinAsignacion { get; private set; }
        /// <summary>
        /// Indicador de estado del personal de la empresa (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; private set; }
        /// <summary>
        /// Áreas que están relacionadas con la sede
        /// </summary>
        public virtual List<Area> Areas { get; private set; }
        /// <summary>
        /// Código de sede que representa a una relación no asignada (se asigna a sus relaciones
        /// cuando la sede es inhabilitada).
        /// </summary>
        public const int CodigoSedeNoAsignado = 1;
        /// <summary>
        /// Si la sede tiene el estado ACTIVO
        /// </summary>
        public bool EsSedeActivo
        {
            get
            {
                return IndicadorEstado;
            }
        }
        /// <summary>
        /// Si la sede tiene el estado INACTIVO
        /// </summary>
        public bool EsSedeInactivo
        {
            get
            {
                return !IndicadorEstado;
            }
        }
        /// <summary>
        /// Método estático que crea la entidad sede
        /// </summary>
        /// <param name="descripcionSede">Descripción de la sede</param>
        /// <returns>Sede creada</returns>
        public static Sede CrearSede(string descripcionSede)
        {
            return new Sede
            {
                DescripcionSede = descripcionSede,
                IndicadorRegistroParaSinAsignacion = false,
                IndicadorEstado = true
            };
        }
        /// <summary>
        /// Método que modifica la entidad sede
        /// </summary>
        /// <param name="descripcionSede">Descripción de la sede</param>
        public void ModificarSede(string descripcionSede)
        {
            DescripcionSede = descripcionSede;
        }
        /// <summary>
        /// Método que inhabilita la entidad sede
        /// </summary>
        public void InhabilitarSede()
        {
            IndicadorEstado = false;
        }
        /// <summary>
        /// Método que habilita la entidad sede
        /// </summary>
        public void HabilitarSede()
        {
            IndicadorEstado = true;
        }
    }
}
