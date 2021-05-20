using API.SRICA.Dominio.Entidad.SE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.SE
{
    /// <summary>
    /// Clase para la configuración entre la entidad Sede con la tabla SE_SEDE
    /// </summary>
    public class SedeConfiguracion : IEntityTypeConfiguration<Sede>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Sede con la tabla SE_SEDE</param>
        public void Configure(EntityTypeBuilder<Sede> builder)
        {
            builder.ToTable("SE_SEDE");
            builder.HasKey(g => g.CodigoSede);

            builder.Property(g => g.CodigoSede).HasColumnName("COD_SEDE");
            builder.Property(g => g.DescripcionSede).HasColumnName("DES_SEDE");
            builder.Property(g => g.IndicadorRegistroParaSinAsignacion)
                .HasColumnName("IND_REGISTRO_PARA_SIN_ASIGNACION");
            builder.Property(g => g.IndicadorEstado).HasColumnName("IND_ESTADO");

            builder.HasMany(g => g.Areas).WithOne().HasForeignKey(g => g.CodigoSede);

            builder.Ignore(g => g.EsSedeActivo);
            builder.Ignore(g => g.EsSedeInactivo);
        }
    }
}
