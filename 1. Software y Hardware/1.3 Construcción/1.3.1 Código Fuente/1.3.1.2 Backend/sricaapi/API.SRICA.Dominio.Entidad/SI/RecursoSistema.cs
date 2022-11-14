namespace API.SRICA.Dominio.Entidad.SI
{
    /// <summary>
    /// Entidad Recurso Sistema que representa a la tabla SI_RECURSO_SISTEMA
    /// </summary>
    public class RecursoSistema
    {
        /// <summary>
        /// Código interno del recurso de sistema (primary key)
        /// </summary>
        public int CodigoRecursoSistema { get; private set; }
        /// <summary>
        /// Descripción del recurso de sistema
        /// </summary>
        public string DescripcionRecursoSistema { get; private set; }
    }
}
