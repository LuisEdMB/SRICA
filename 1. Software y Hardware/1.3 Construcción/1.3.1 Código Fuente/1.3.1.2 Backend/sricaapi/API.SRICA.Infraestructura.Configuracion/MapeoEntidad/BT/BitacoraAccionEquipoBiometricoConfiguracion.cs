using API.SRICA.Dominio.Entidad.BT;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.BT
{
    /// <summary>
    /// Clase para la configuración entre la entidad Bitácora Acción Equipo Biométrico con la tabla 
    /// BT_BITACORA_ACCION_EQUIPO_BIOMETRICO
    /// </summary>
    public class BitacoraAccionEquipoBiometricoConfiguracion 
        : IEntityTypeConfiguration<BitacoraAccionEquipoBiometrico>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Bitácora Acción Equipo Biométrico 
        /// con la tabla BT_BITACORA_ACCION_EQUIPO_BIOMETRICO</param>
        public void Configure(EntityTypeBuilder<BitacoraAccionEquipoBiometrico> builder)
        {
            builder.ToTable("BT_BITACORA_ACCION_EQUIPO_BIOMETRICO");
            builder.HasKey(g => g.CodigoBitacora);

            builder.Property(g => g.CodigoBitacora).HasColumnName("COD_BITACORA");
            builder.Property(g => g.CodigoPersonalEmpresa).HasColumnName("COD_PERSONAL");
            builder.Property(g => g.DNIPersonalEmpresa).HasColumnName("NUM_DNI_PERSONAL");
            builder.Property(g => g.NombrePersonalEmpresa).HasColumnName("NOM_PERSONAL");
            builder.Property(g => g.ApellidoPersonalEmpresa).HasColumnName("APE_PERSONAL");
            builder.Property(g => g.CodigoSede).HasColumnName("COD_SEDE");
            builder.Property(g => g.DescripcionSede).HasColumnName("DES_SEDE");
            builder.Property(g => g.CodigoArea).HasColumnName("COD_AREA");
            builder.Property(g => g.DescripcionArea).HasColumnName("DES_AREA");
            builder.Property(g => g.NombreEquipoBiometrico).HasColumnName("NOM_EQUIPO_BIOMETRICO");
            builder.Property(g => g.CodigoResultadoAcceso).HasColumnName("COD_RESULTADO_ACCESO");
            builder.Property(g => g.DescripcionResultadoAccion).HasColumnName("DES_RESULTADO_ACCION");
            builder.Property(g => g.FechaAcceso).HasColumnName("FEC_ACCESO");
            builder.Property(g => g.ImagenPersonalNoRegistrado).HasColumnName("IMG_PERSONAL_NO_REGISTRADO");

            builder.HasOne(g => g.ResultadoAcceso).WithMany().HasForeignKey(g => g.CodigoResultadoAcceso);
        }
    }
}
