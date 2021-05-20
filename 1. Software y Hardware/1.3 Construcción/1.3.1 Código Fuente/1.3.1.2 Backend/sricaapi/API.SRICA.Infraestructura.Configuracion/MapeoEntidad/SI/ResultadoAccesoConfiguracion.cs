using API.SRICA.Dominio.Entidad.SI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.SI
{
    /// <summary>
    /// Clase para la configuración entre la entidad Resultado Acceso con la tabla SI_RESULTADO_ACCESO
    /// </summary>
    public class ResultadoAccesoConfiguracion : IEntityTypeConfiguration<ResultadoAcceso>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Resultado Acceso con la tabla 
        /// SI_RESULTADO_ACCESO</param>
        public void Configure(EntityTypeBuilder<ResultadoAcceso> builder)
        {
            builder.ToTable("SI_RESULTADO_ACCESO");
            builder.HasKey(g => g.CodigoResultadoAcceso);

            builder.Property(g => g.CodigoResultadoAcceso).HasColumnName("COD_RESULTADO_ACCESO");
            builder.Property(g => g.DescripcionResultadoAcceso).HasColumnName("DES_RESULTADO_ACCESO");
        }
    }
}
