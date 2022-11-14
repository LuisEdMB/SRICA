namespace API.SRICA.Dominio.Entidad.SI
{
    /// <summary>
    /// Entidad Tipo Evento Sistema que representa a la tabla SI_TIPO_EVENTO_SISTEMA
    /// </summary>
    public class TipoEventoSistema
    {
        /// <summary>
        /// Código interno del tipo de evento de sistema (primary key)
        /// </summary>
        public int CodigoTipoEventoSistema { get; private set; }
        /// <summary>
        /// Descripción del tipo de evento de sistema
        /// </summary>
        public string DescripcionTipoEventoSistema { get; private set; }
        /// <summary>
        /// Código del tipo de evento del sistema "Error"
        /// </summary>
        public const int CodigoTipoEventoError = 1;
        /// <summary>
        /// Código del tipo de evento del sistema "Validación"
        /// </summary>
        public const int CodigoTipoEventoValidacion = 2;
        /// <summary>
        /// Código del tipo de evento del sistema "Correcto"
        /// </summary>
        public const int CodigoTipoEventoCorrecto = 3;
    }
}
