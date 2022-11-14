using API.SRICA.Dominio.Entidad.PE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.PE
{
    /// <summary>
    /// Clase para la configuración entre la entidad Personal Empresa con la tabla PE_PERSONAL_EMPRESA
    /// </summary>
    public class PersonalEmpresaConfiguracion : IEntityTypeConfiguration<PersonalEmpresa>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Personal Empresa con la tabla 
        /// PE_PERSONAL_EMPRESA</param>
        public void Configure(EntityTypeBuilder<PersonalEmpresa> builder)
        {
            builder.ToTable("PE_PERSONAL_EMPRESA");
            builder.HasKey(g => g.CodigoPersonalEmpresa);

            builder.Property(g => g.CodigoPersonalEmpresa).HasColumnName("COD_PERSONAL");
            builder.Property(g => g.DNIPersonalEmpresa).HasColumnName("NUM_DNI_PERSONAL");
            builder.Property(g => g.NombrePersonalEmpresa).HasColumnName("NOM_PERSONAL");
            builder.Property(g => g.ApellidoPersonalEmpresa).HasColumnName("APE_PERSONAL");
            builder.Property(g => g.ImagenIrisCodificado).HasColumnName("IMG_IRIS_CODIFICADO");
            builder.Property(g => g.IndicadorEstado).HasColumnName("IND_ESTADO");

            builder.HasMany(g => g.AreasAsignadas).WithOne(g => g.PersonalEmpresa)
                .HasForeignKey(g => g.CodigoPersonalEmpresa);

            builder.Ignore(g => g.EsPersonalEmpresaActivo);
            builder.Ignore(g => g.EsPersonalEmpresaInactivo);
        }
    }
}
