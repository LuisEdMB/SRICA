using API.SRICA.Dominio.Entidad.BT;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.BT
{
    /// <summary>
    /// Clase para la configuración entre la entidad Bitácora Acción Sistema con la tabla 
    /// BT_BITACORA_ACCION_SISTEMA
    /// </summary>
    public class BitacoraAccionSistemaConfiguracion : IEntityTypeConfiguration<BitacoraAccionSistema>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Bitácora Acción Sistema con la tabla 
        /// BT_BITACORA_ACCION_SISTEMA</param>
        public void Configure(EntityTypeBuilder<BitacoraAccionSistema> builder)
        {
            builder.ToTable("BT_BITACORA_ACCION_SISTEMA");
            builder.HasKey(g => g.CodigoBitacora);

            builder.Property(g => g.CodigoBitacora).HasColumnName("COD_BITACORA");
            builder.Property(g => g.CodigoUsuario).HasColumnName("COD_USUARIO");
            builder.Property(g => g.UsuarioAcceso).HasColumnName("USU_USUARIO");
            builder.Property(g => g.NombreUsuario).HasColumnName("NOM_USUARIO");
            builder.Property(g => g.ApellidoUsuario).HasColumnName("APE_USUARIO");
            builder.Property(g => g.CodigoRolUsuario).HasColumnName("COD_ROL_USUARIO");
            builder.Property(g => g.CodigoModuloSistema).HasColumnName("COD_MODULO_SISTEMA");
            builder.Property(g => g.CodigoRecursoSistema).HasColumnName("COD_RECURSO_SISTEMA");
            builder.Property(g => g.CodigoTipoEventoSistema).HasColumnName("COD_TIPO_EVENTO_SISTEMA");
            builder.Property(g => g.CodigoAccionSistema).HasColumnName("COD_ACCION_SISTEMA");
            builder.Property(g => g.DescripcionResultadoAccion).HasColumnName("DES_RESULTADO_ACCION");
            builder.Property(g => g.ValorAnterior).HasColumnName("VAL_ANTERIOR");
            builder.Property(g => g.ValorActual).HasColumnName("VAL_ACTUAL");
            builder.Property(g => g.FechaAccion).HasColumnName("FEC_ACCION");

            builder.HasOne(g => g.RolUsuario).WithMany().HasForeignKey(g => g.CodigoRolUsuario);
            builder.HasOne(g => g.ModuloSistema).WithMany().HasForeignKey(g => g.CodigoModuloSistema);
            builder.HasOne(g => g.RecursoSistema).WithMany().HasForeignKey(g => g.CodigoRecursoSistema);
            builder.HasOne(g => g.TipoEventoSistema).WithMany().HasForeignKey(g => 
                g.CodigoTipoEventoSistema);
            builder.HasOne(g => g.AccionSistema).WithMany().HasForeignKey(g => g.CodigoAccionSistema);
        }
    }
}
