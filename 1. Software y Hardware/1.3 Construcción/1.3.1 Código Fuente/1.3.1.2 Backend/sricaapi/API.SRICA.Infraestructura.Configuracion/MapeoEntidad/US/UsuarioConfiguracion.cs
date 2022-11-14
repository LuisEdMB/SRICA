using API.SRICA.Dominio.Entidad.US;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.US
{
    /// <summary>
    /// Clase para la configuración entre la entidad Usuario con la tabla US_USUARIO
    /// </summary>
    public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Usuario con la tabla US_USUARIO</param>
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("US_USUARIO");
            builder.HasKey(g => g.CodigoUsuario);

            builder.Property(g => g.CodigoUsuario).HasColumnName("COD_USUARIO");
            builder.Property(g => g.CodigoRolUsuario).HasColumnName("COD_ROL_USUARIO");
            builder.Property(g => g.UsuarioAcceso).HasColumnName("USU_USUARIO");
            builder.Property(g => g.ContrasenaAcceso).HasColumnName("USU_CONTRASENA");
            builder.Property(g => g.NombreUsuario).HasColumnName("NOM_USUARIO");
            builder.Property(g => g.ApellidoUsuario).HasColumnName("APE_USUARIO");
            builder.Property(g => g.CorreoElectronico).HasColumnName("DES_CORREO_ELECTRONICO");
            builder.Property(g => g.EsCorreoElectronicoPorDefecto).HasColumnName("IND_CORREO_POR_DEFECTO");
            builder.Property(g => g.EsContrasenaPorDefecto).HasColumnName("IND_CONTRASENA_POR_DEFECTO");
            builder.Property(g => g.IndicadorEstado).HasColumnName("IND_ESTADO");

            builder.HasOne(g => g.RolUsuario).WithMany().HasForeignKey(g => g.CodigoRolUsuario);

            builder.Ignore(g => g.EsUsuarioActivo);
            builder.Ignore(g => g.EsUsuarioInactivo);
        }
    }
}
