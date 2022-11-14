using System.Collections.Generic;

namespace API.SRICA.Dominio.Entidad.PE
{
    /// <summary>
    /// Entidad Personal Empresa que representa a la tabla PE_PERSONAL_EMPRESA
    /// </summary>
    public class PersonalEmpresa
    {
        /// <summary>
        /// Código interno del personal de la empresa (primary key)
        /// </summary>
        public int CodigoPersonalEmpresa { get; private set; }
        /// <summary>
        /// DNI del personal de la empresa
        /// </summary>
        public string DNIPersonalEmpresa { get; private set; }
        /// <summary>
        /// Nombre del personal de la empresa
        /// </summary>
        public string NombrePersonalEmpresa { get; private set; }
        /// <summary>
        /// Apellido del personal de la empresa
        /// </summary>
        public string ApellidoPersonalEmpresa { get; private set; }
        /// <summary>
        /// Imagen del iris codificado del personal que será usado para el proceso de 
        /// reconocimiento
        /// </summary>
        public byte[] ImagenIrisCodificado { get; private set; }
        /// <summary>
        /// Indicador de estado del personal de la empresa (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; private set; }
        /// <summary>
        /// Asignaciones de áreas del personal de la empresa
        /// </summary>
        public virtual List<PersonalEmpresaXArea> AreasAsignadas { get; private set; }
        /// <summary>
        /// Mime type para archivos excel de versiones anteriores (.xls)
        /// </summary>
        public const string ExcelVersionAnterior = "application/vnd.ms-excel";
        /// <summary>
        /// Mime type para archivos excel de versiones actuales (.xlsx)
        /// </summary>
        public const string ExcelVersionActual = 
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        /// <summary>
        /// Constante columna "dni"
        /// </summary>
        public const string ColumnaDNI = "dni";
        /// <summary>
        /// Constante columna "nombres"
        /// </summary>
        public const string ColumnaNombre = "nombres";
        /// <summary>
        /// Constante columna "apellidos"
        /// </summary>
        public const string ColumnaApellido = "apellidos";
        /// <summary>
        /// Si el personal de la empresa tiene el estado ACTIVO
        /// </summary>
        public bool EsPersonalEmpresaActivo
        {
            get
            {
                return IndicadorEstado;
            }
        }
        /// <summary>
        /// Si el personal de la empresa tiene el estado INACTIVO
        /// </summary>
        public bool EsPersonalEmpresaInactivo
        {
            get
            {
                return !IndicadorEstado;
            }
        }
        /// <summary>
        /// Método estático que crea la entidad personal de empresa
        /// </summary>
        /// <param name="dni">DNI del personal de la empresa</param>
        /// <param name="nombre">Nombre del personal de la empresa</param>
        /// <param name="apellido">Apellido del personal de la empresa</param>
        /// <returns>Personal de la empresa creado</returns>
        public static PersonalEmpresa CrearPersonalEmpresa(string dni, string nombre, string apellido)
        {
            return new PersonalEmpresa
            {
                DNIPersonalEmpresa = dni,
                NombrePersonalEmpresa = nombre,
                ApellidoPersonalEmpresa = apellido,
                IndicadorEstado = true
            };
        }
        /// <summary>
        /// Método que agrega la imagen del iris codificado a la entidad personal de
        /// la empresa
        /// </summary>
        /// <param name="imagenIrisCodificado">Imagen del iris codificado</param>
        public void AgregarImagenDeIrisCodificada(byte[] imagenIrisCodificado)
        {
            ImagenIrisCodificado = imagenIrisCodificado;
        }
        /// <summary>
        /// Método que modifica la entidad personal de la empresa
        /// </summary>
        /// <param name="dni">DNI del personal de la empresa</param>
        /// <param name="nombre">Nombre del personal de la empresa</param>
        /// <param name="apellido">Apellido del personal de la empresa</param>
        public void ModificarPersonalEmpresa(string dni, string nombre, string apellido)
        {
            DNIPersonalEmpresa = dni;
            NombrePersonalEmpresa = nombre;
            ApellidoPersonalEmpresa = apellido;
        }
        /// <summary>
        /// Método que inhabilita la entidad personal de la empresa
        /// </summary>
        public void InhabilitarPersonalEmpresa()
        {
            IndicadorEstado = false;
        }
        /// <summary>
        /// Método que habilita la entidad personal de la empresa
        /// </summary>
        public void HabilitarPersonalEmpresa()
        {
            IndicadorEstado = true;
        }
    }
}
