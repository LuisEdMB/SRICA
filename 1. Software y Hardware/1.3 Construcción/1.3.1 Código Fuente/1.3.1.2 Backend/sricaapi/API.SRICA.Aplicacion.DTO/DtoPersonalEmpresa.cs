using System.Collections.Generic;

namespace API.SRICA.Aplicacion.DTO
{
    /// <summary>
    /// DTO para el personal de la empresa
    /// </summary>
    public class DtoPersonalEmpresa
    {
        /// <summary>
        /// Código del personal de la empresa
        /// </summary>
        public string CodigoPersonalEmpresa { get; set; }
        /// <summary>
        /// DNI del personal de la empresa
        /// </summary>
        public string DNIPersonalEmpresa { get; set; }
        /// <summary>
        /// Nombre del personal de la empresa
        /// </summary>
        public string NombrePersonalEmpresa { get; set; }
        /// <summary>
        /// Apellido del personal de la empresa
        /// </summary>
        public string ApellidoPersonalEmpresa { get; set; }
        /// <summary>
        /// Descripción de las sedes de las áreas del personal de la empresa
        /// </summary>
        public string DescripcionSedes { get; set; }
        /// <summary>
        /// Descripción de las áreas del personal de la empresa
        /// </summary>
        public string DescripcionAreas { get; set; }
        /// <summary>
        /// Indicador de estado del personal de la empresa (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; set; }
        /// <summary>
        /// Imagen del iris del personal (en formato base64)
        /// </summary>
        public string ImagenIris { get; set; }
        /// <summary>
        /// Archivo que contiene el listado de personal a registrar de forma masiva
        /// </summary>
        public string ArchivoRegistroMasivo { get; set; }
        /// <summary>
        /// Si la operación de guardado es de registro masivo
        /// </summary>
        public bool EsRegistroMasivo { get; set; }
        /// <summary>
        /// Si el personal tiene registrado el iris
        /// </summary>
        public bool TieneIrisRegistrado { get; set; }
        /// <summary>
        /// Áreas asignadas al personal de la empresa
        /// </summary>
        public List<DtoPersonalEmpresaXArea> Areas { get; set; }
    }
}
