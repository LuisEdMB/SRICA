using API.SRICA.Dominio.Entidad.PE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.PE
{
    /// <summary>
    /// Clase para la configuración entre la entidad Personal Empresa X Área con la tabla 
    /// PE_PERSONAL_EMPRESA_X_AREA
    /// </summary>
    public class PersonalEmpresaXAreaConfiguracion : IEntityTypeConfiguration<PersonalEmpresaXArea>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Personal Empresa X Área con la tabla 
        /// PE_PERSONAL_EMPRESA_X_AREA</param>
        public void Configure(EntityTypeBuilder<PersonalEmpresaXArea> builder)
        {
            builder.ToTable("PE_PERSONAL_EMPRESA_X_AREA");
            builder.HasKey(g => g.CodigoPersonalEmpresaXArea);

            builder.Property(g => g.CodigoPersonalEmpresaXArea).HasColumnName("COD_PERSONAL_EMPRESA_X_AREA");
            builder.Property(g => g.CodigoPersonalEmpresa).HasColumnName("COD_PERSONAL");
            builder.Property(g => g.CodigoArea).HasColumnName("COD_AREA");
            builder.Property(g => g.IndicadorEstado).HasColumnName("IND_ESTADO");

            builder.HasOne(g => g.PersonalEmpresa).WithMany(g => g.AreasAsignadas)
                .HasForeignKey(g => g.CodigoPersonalEmpresa);
            builder.HasOne(g => g.Area).WithMany(g => g.PersonalAsignado)
                .HasForeignKey(g => g.CodigoArea);

            builder.Ignore(g => g.EsPersonalEmpresaXAreaActivo);
            builder.Ignore(g => g.EsPersonalEmpresaXAreaInactivo);
        }
    }
}
