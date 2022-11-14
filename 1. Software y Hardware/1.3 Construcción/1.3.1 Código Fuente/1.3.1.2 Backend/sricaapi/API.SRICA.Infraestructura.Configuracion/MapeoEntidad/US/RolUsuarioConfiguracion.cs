using API.SRICA.Dominio.Entidad.US;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.US
{
    /// <summary>
    /// Clase para la configuración entre la entidad Rol Usuario con la tabla US_ROL_USUARIO
    /// </summary>
    public class RolUsuarioConfiguracion : IEntityTypeConfiguration<RolUsuario>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Usuario con la tabla US_USUARIO</param>
        public void Configure(EntityTypeBuilder<RolUsuario> builder)
        {
            builder.ToTable("US_ROL_USUARIO");
            builder.HasKey(g => g.CodigoRolUsuario);

            builder.Property(g => g.CodigoRolUsuario).HasColumnName("COD_ROL_USUARIO");
            builder.Property(g => g.DescripcionRolUsuario).HasColumnName("DES_ROL_USUARIO");
            builder.Property(g => g.IndicadorEstado).HasColumnName("IND_ESTADO");
        }
    }
}
