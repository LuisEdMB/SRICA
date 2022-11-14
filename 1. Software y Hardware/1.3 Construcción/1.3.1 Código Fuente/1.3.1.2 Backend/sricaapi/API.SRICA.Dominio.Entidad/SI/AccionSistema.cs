namespace API.SRICA.Dominio.Entidad.SI
{
    /// <summary>
    /// Entidad Acción Sistema que representa a la tabla SI_ACCION_SISTEMA
    /// </summary>
    public class AccionSistema
    {
        /// <summary>
        /// Código interno de la acción de sistema (primary key)
        /// </summary>
        public int CodigoAccionSistema { get; private set; }
        /// <summary>
        /// Descripción de la acción de sistema
        /// </summary>
        public string DescripcionAccionSistema { get; private set; }
        /// <summary>
        /// Código que representa al registro de acción de tipo "Acceso al Sistema"
        /// </summary>
        public const int CodigoAccionAccesoSistema = 1;
    }
}
