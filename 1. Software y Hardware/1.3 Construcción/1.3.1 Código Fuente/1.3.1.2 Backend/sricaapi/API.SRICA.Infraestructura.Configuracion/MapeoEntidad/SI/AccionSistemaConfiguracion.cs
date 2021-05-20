using API.SRICA.Dominio.Entidad.SI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.SI
{
    /// <summary>
    /// Clase para la configuración entre la entidad Acción Sistema con la tabla SI_ACCION_SISTEMA
    /// </summary>
    public class AccionSistemaConfiguracion : IEntityTypeConfiguration<AccionSistema>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Acción Sistema con la tabla 
        /// SI_ACCION_SISTEMA</param>
        public void Configure(EntityTypeBuilder<AccionSistema> builder)
        {
            builder.ToTable("SI_ACCION_SISTEMA");
            builder.HasKey(g => g.CodigoAccionSistema);

            builder.Property(g => g.CodigoAccionSistema).HasColumnName("COD_ACCION_SISTEMA");
            builder.Property(g => g.DescripcionAccionSistema).HasColumnName("DES_ACCION_SISTEMA");
        }
    }
}
