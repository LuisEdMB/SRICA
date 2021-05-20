using API.SRICA.Dominio.Entidad.AR;

namespace API.SRICA.Dominio.Entidad.PE
{
    /// <summary>
    /// Entidad Personal Empresa X Área que representa a la tabla PE_PERSONAL_EMPRESA_X_AREA
    /// </summary>
    public class PersonalEmpresaXArea
    {
        /// <summary>
        /// Código interno del personal de empresa por área (primary key)
        /// </summary>
        public int CodigoPersonalEmpresaXArea { get; private set; }
        /// <summary>
        /// Código del personal de la empresa (relación con PE_PERSONAL_EMPRESA)
        /// </summary>
        public int CodigoPersonalEmpresa { get; private set; }
        /// <summary>
        /// Código del área (relación con AR_AREA)
        /// </summary>
        public int CodigoArea { get; private set; }
        /// <summary>
        /// Indicador de estado del registro (True: Activo - False: Inactivo)
        /// </summary>
        public bool IndicadorEstado { get; private set; }
        /// <summary>
        /// Personal de la empresa que se usa en el registro
        /// </summary>
        public virtual PersonalEmpresa PersonalEmpresa { get; private set; }
        /// <summary>
        /// Área que se usa en el registro
        /// </summary>
        public virtual Area Area { get; private set; }
        /// <summary>
        /// Si el registro tiene el estado ACTIVO
        /// </summary>
        public bool EsPersonalEmpresaXAreaActivo
        {
            get
            {
                return IndicadorEstado;
            }
        }
        /// <summary>
        /// Si el registro tiene el estado INACTIVO
        /// </summary>
        public bool EsPersonalEmpresaXAreaInactivo
        {
            get
            {
                return !IndicadorEstado;
            }
        }
        /// <summary>
        /// Método estático que crea la entidad que relaciona el personal de la empresa con sus áreas
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa a quién pertenece el área</param>
        /// <param name="area">Área seleccionada</param>
        /// <returns>Relación creada</returns>
        public static PersonalEmpresaXArea CrearPersonalEmpresaXArea(PersonalEmpresa personalEmpresa, 
            Area area)
        {
            return new PersonalEmpresaXArea
            {
                CodigoPersonalEmpresa = personalEmpresa.CodigoPersonalEmpresa,
                CodigoArea = area.CodigoArea,
                IndicadorEstado = true
            };
        }
        /// <summary>
        /// Método que inactiva la entidad
        /// </summary>
        public void Inactivar()
        {
            IndicadorEstado = false;
        }
    }
}
