using API.SRICA.Dominio.Entidad.SI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.SI
{
    /// <summary>
    /// Clase para la configuración entre la entidad Recurso Sistema con la tabla SI_RECURSO_SISTEMA
    /// </summary>
    class RecursoSistemaConfiguracion : IEntityTypeConfiguration<RecursoSistema>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Recurso Sistema con la tabla 
        /// SI_RECURSO_SISTEMA</param>
        public void Configure(EntityTypeBuilder<RecursoSistema> builder)
        {
            builder.ToTable("SI_RECURSO_SISTEMA");
            builder.HasKey(g => g.CodigoRecursoSistema);

            builder.Property(g => g.CodigoRecursoSistema).HasColumnName("COD_RECURSO_SISTEMA");
            builder.Property(g => g.DescripcionRecursoSistema).HasColumnName("DES_RECURSO_SISTEMA");
        }
    }
}
