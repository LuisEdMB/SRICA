using API.SRICA.Dominio.Entidad.SI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.SI
{
    /// <summary>
    /// Clase para la configuración entre la entidad Módulo Sistema con la tabla SI_MODULO_SISTEMA
    /// </summary>
    public class ModuloSistemaConfiguracion : IEntityTypeConfiguration<ModuloSistema>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Módulo Sistema con la tabla 
        /// SI_MODULO_SISTEMA</param>
        public void Configure(EntityTypeBuilder<ModuloSistema> builder)
        {
            builder.ToTable("SI_MODULO_SISTEMA");
            builder.HasKey(g => g.CodigoModuloSistema);

            builder.Property(g => g.CodigoModuloSistema).HasColumnName("COD_MODULO_SISTEMA");
            builder.Property(g => g.DescripcionModuloSistema).HasColumnName("DES_MODULO_SISTEMA");
        }
    }
}
