using API.SRICA.Dominio.Entidad.SI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.SI
{
    /// <summary>
    /// Clase para la configuración entre la entidad Tipo Evento Sistema con la tabla SI_TIPO_EVENTO_SISTEMA
    /// </summary>
    class TipoEventoSistemaConfiguracion : IEntityTypeConfiguration<TipoEventoSistema>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Tipo Evento Sistema con la tabla 
        /// SI_TIPO_EVENTO_SISTEMA</param>
        public void Configure(EntityTypeBuilder<TipoEventoSistema> builder)
        {
            builder.ToTable("SI_TIPO_EVENTO_SISTEMA");
            builder.HasKey(g => g.CodigoTipoEventoSistema);

            builder.Property(g => g.CodigoTipoEventoSistema).HasColumnName("COD_TIPO_EVENTO_SISTEMA");
            builder.Property(g => g.DescripcionTipoEventoSistema).HasColumnName("DES_TIPO_EVENTO_SISTEMA");
        }
    }
}
