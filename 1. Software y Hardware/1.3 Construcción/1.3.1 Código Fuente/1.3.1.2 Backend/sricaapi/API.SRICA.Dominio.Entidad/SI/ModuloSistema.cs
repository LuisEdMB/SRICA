namespace API.SRICA.Dominio.Entidad.SI
{
    /// <summary>
    /// Entidad Módulo Sistema que representa a la tabla SI_MODULO_SISTEMA
    /// </summary>
    public class ModuloSistema
    {
        /// <summary>
        /// Código interno del módulo de sistema (primary key)
        /// </summary>
        public int CodigoModuloSistema { get; private set; }
        /// <summary>
        /// Descripción del módulo de sistema
        /// </summary>
        public string DescripcionModuloSistema { get; private set; }
    }
}
