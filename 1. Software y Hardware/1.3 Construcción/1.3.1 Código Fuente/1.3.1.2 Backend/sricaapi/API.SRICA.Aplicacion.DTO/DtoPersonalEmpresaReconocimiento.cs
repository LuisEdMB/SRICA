namespace API.SRICA.Aplicacion.DTO
{
    /// <summary>
    /// Clase Dto que contiene los datos del personal para el proceso de reconocimiento
    /// </summary>
    public class DtoPersonalEmpresaReconocimiento
    {
        /// <summary>
        /// Imagen original del personal a reconocer (formato base64)
        /// </summary>
        public string ImagenOriginal { get; set; }
        /// <summary>
        /// Imagen del ojo del personal a reconocer (formato base64)
        /// </summary>
        public string ImagenOjo { get; set; }
        /// <summary>
        /// Dirección MAC del equipo biométrico origen
        /// </summary>
        public string DireccionMacEquipoBiometrico { get; set; }
        /// <summary>
        /// Datos del personal de la empresa reconocido
        /// </summary>
        public DtoPersonalEmpresa PersonalEmpresa { get; set; }
        /// <summary>
        /// Datos del equipo biométrico de donde se realiza el proceso de reconocimiento
        /// </summary>
        public DtoEquipoBiometrico EquipoBiometrico { get; set; }
    }
}