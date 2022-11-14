using System.Collections.Generic;

namespace API.SRICA.Aplicacion.DTO
{
    /// <summary>
    /// DTO para el área
    /// </summary>
    public class DtoArea
    {
        /// <summary>
        /// Código del área
        /// </summary>
        public string CodigoArea { get; set; }
        /// <summary>
        /// Código de la sede asignado al área (relación con SE_SEDE)
        /// </summary>
        public string CodigoSede { get; set; }
        /// <summary>
        /// Descripcion de la sede asignado al área
        /// </summary>
        public string DescripcionSede { get; set; }
        /// <summary>
        /// Indicador que representa que la sede es "Sin Asignación"
        /// </summary>
        public bool IndicadorRegistroSedeParaSinAsignacion { get; set; }
        /// <summary>
        /// Decripción del área
        /// </summary>
        public string DescripcionArea { get; set; }
        /// <summary>
        /// Indicador que representa el área que se asignará a sus relaciones cuando ésta sea
        /// inhabilitada
        /// </summary>
        public bool IndicadorRegistroParaSinAsignacion { get; set; }
        /// <summary>
        /// Indicador de estado del área (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; set; }
        /// <summary>
        /// Equipo biométricos que están relacionados con el área
        /// </summary>
        public List<DtoEquipoBiometrico> EquiposBiometricos { get; set; }
        /// <summary>
        /// Si el área ha sido seleccionada (para las áreas del personal de la empresa)
        /// </summary>
        public bool Seleccionado { get; set; }
        /// <summary>
        /// Si el registro es un objeto nuevo
        /// </summary>
        public bool Nuevo { get; set; }
    }
}
