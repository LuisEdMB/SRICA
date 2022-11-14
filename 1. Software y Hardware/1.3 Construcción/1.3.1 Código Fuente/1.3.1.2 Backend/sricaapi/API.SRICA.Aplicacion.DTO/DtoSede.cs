using System.Collections.Generic;

namespace API.SRICA.Aplicacion.DTO
{
    /// <summary>
    /// DTO para la sede
    /// </summary>
    public class DtoSede
    {
        /// <summary>
        /// Código de la sede
        /// </summary>
        public string CodigoSede { get; set; }
        /// <summary>
        /// Decripción de la sede
        /// </summary>
        public string DescripcionSede { get; set; }
        /// <summary>
        /// Indicador que representa la sede que se asignará a sus relaciones cuando ésta sea
        /// inhabilitada
        /// </summary>
        public bool IndicadorRegistroParaSinAsignacion { get; set; }
        /// <summary>
        /// Indicador de estado del personal de la empresa (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; set; }
        /// <summary>
        /// Áreas que están relacionadas con la sede
        /// </summary>
        public List<DtoArea> Areas { get; set; }
    }
}
