namespace API.SRICA.Dominio.Entidad.SI
{
    /// <summary>
    /// Entidad Resultado Acceso que representa a la tabla SI_RESULTADO_ACCESO
    /// </summary>
    public class ResultadoAcceso
    {
        /// <summary>
        /// Código interno del resultado de acceso (primary key)
        /// </summary>
        public int CodigoResultadoAcceso { get; private set; }
        /// <summary>
        /// Descripción del resultado de acceso
        /// </summary>
        public string DescripcionResultadoAcceso { get; private set; }
        /// <summary>
        /// Constante para el código de resultado de accceso "Concedido"
        /// </summary>
        public const int Concedido = 1;
        /// <summary>
        /// Constante para el código de resultado de accceso "Denegado"
        /// </summary>
        public const int Denegado = 2;
        /// <summary>
        /// Constante para el código de resultado de accceso "Error"
        /// </summary>
        public const int Error = 3;
    }
}
