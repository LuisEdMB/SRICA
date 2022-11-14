namespace API.SRICA.Aplicacion.DTO
{
    public class DtoPersonalEmpresaXArea
    {
        /// <summary>
        /// Código del personal de empresa por área
        /// </summary>
        public string CodigoPersonalEmpresaXArea { get; set; }
        /// <summary>
        /// Código del personal de la empresa (relación con PE_PERSONAL_EMPRESA)
        /// </summary>
        public string CodigoPersonalEmpresa { get; set; }
        /// <summary>
        /// Código del área (relación con AR_AREA)
        /// </summary>
        public string CodigoArea { get; set; }
        /// <summary>
        /// Código de la sede del área
        /// </summary>
        public string CodigoSede { get; set; }
        /// <summary>
        /// Descripción del área
        /// </summary>
        public string DescripcionArea { get; set; }
        /// <summary>
        /// Descripción de la sede del área
        /// </summary>
        public string DescripcionSede { get; set; }
        /// <summary>
        /// Indicador de estado del registro (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; set; }
        /// <summary>
        /// Si el registro ha sido seleccionada
        /// </summary>
        public bool Seleccionado { get; set; }
        /// <summary>
        /// Si el registro es un objeto nuevo
        /// </summary>
        public bool Nuevo { get; set; }
        /// <summary>
        /// Personal de la empresa que se usa en el registro
        /// </summary>
        public DtoPersonalEmpresa PersonalEmpresa { get; set; }
        /// <summary>
        /// Área que se usa en el registro
        /// </summary>
        public DtoArea Area { get; set; }
    }
}
