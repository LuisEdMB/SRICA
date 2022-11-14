namespace API.SRICA.Aplicacion.DTO
{
    /// <summary>
    /// DTO para la bitácora de acciones de equipos biométricos
    /// </summary>
    public class DtoBitacoraAccionEquipoBiometrico
    {
        /// <summary>
        /// Código de la bitácora de acción
        /// </summary>
        public string CodigoBitacora { get; set; }
        /// <summary>
        /// Código del personal de la empresa que realiza la acción
        /// </summary>
        public string CodigoPersonalEmpresa { get; set; }
        /// <summary>
        /// DNI del personal de la empresa que realiza la acción
        /// </summary>
        public string DNIPersonalEmpresa { get; set; }
        /// <summary>
        /// Nombre del personal de la empresa que realiza la acción
        /// </summary>
        public string NombrePersonalEmpresa { get; set; }
        /// <summary>
        /// Apellido del personal de la empresa que realiza la acción
        /// </summary>
        public string ApellidoPersonalEmpresa { get; set; }
        /// <summary>
        /// Código de sede de acción
        /// </summary>
        public string CodigoSede { get; set; }
        /// <summary>
        /// Descripción de la sede de acción
        /// </summary>
        public string DescripcionSede { get; set; }
        /// <summary>
        /// Código de área de acción
        /// </summary>
        public string CodigoArea { get; set; }
        /// <summary>
        /// Descripción del área de acción
        /// </summary>
        public string DescripcionArea { get; set; }
        /// <summary>
        /// Nombre del equipo biométrico de acción
        /// </summary>
        public string NombreEquipoBiometrico { get; set; }
        /// <summary>
        /// Código del resultado de acceso
        /// </summary>
        public string CodigoResultadoAcceso { get; set; }
        /// <summary>
        /// Descripción del resultado de acceso
        /// </summary>
        public string DescripcionResultadoAcceso { get; set; }
        /// <summary>
        /// Descripción del resultado de acción
        /// </summary>
        public string DescripcionResultadoAccion { get; set; }
        /// <summary>
        /// Fecha de acceso
        /// </summary>
        public string FechaAcceso { get; set; }
        /// <summary>
        /// Imagen de la persona que intenta ingresar a un área, sin que esté registrado
        /// en el sistema (en base64)
        /// </summary>
        public string ImagenPersonalNoRegistrado { get; set; }
        /// <summary>
        /// Si existe la imagen de la persona que intenta ingresar a un área
        /// </summary>
        public bool ExisteImagen { get; set; }
    }
}
