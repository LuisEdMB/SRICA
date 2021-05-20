using API.SRICA.Dominio.Entidad.AR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.AR
{
    /// <summary>
    /// Clase para la configuración entre la entidad Area con la tabla AR_AREA
    /// </summary>
    public class AreaConfiguracion : IEntityTypeConfiguration<Area>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Area con la tabla AR_AREA</param>
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.ToTable("AR_AREA");
            builder.HasKey(g => g.CodigoArea);

            builder.Property(g => g.CodigoArea).HasColumnName("COD_AREA");
            builder.Property(g => g.CodigoSede).HasColumnName("COD_SEDE");
            builder.Property(g => g.DescripcionArea).HasColumnName("DES_AREA");
            builder.Property(g => g.IndicadorRegistroParaSinAsignacion)
                .HasColumnName("IND_REGISTRO_PARA_SIN_ASIGNACION");
            builder.Property(g => g.IndicadorEstado).HasColumnName("IND_ESTADO");

            builder.HasMany(g => g.EquiposBiometricos).WithOne(g => g.Area).HasForeignKey(g => g.CodigoArea);
            builder.HasOne(g => g.Sede).WithMany(g => g.Areas).HasForeignKey(g => g.CodigoSede);

            builder.Ignore(g => g.EsAreaActivo);
            builder.Ignore(g => g.EsAreaInactivo);
        }
    }
}
