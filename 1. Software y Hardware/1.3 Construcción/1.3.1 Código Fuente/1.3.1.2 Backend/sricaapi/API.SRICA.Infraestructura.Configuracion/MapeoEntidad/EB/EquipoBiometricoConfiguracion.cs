using API.SRICA.Dominio.Entidad.EB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.EB
{
    /// <summary>
    /// Clase para la configuración entre la entidad Equipo Biometrico con la tabla EB_EQUIPO_BIOMETRICO
    /// </summary>
    class EquipoBiometricoConfiguracion : IEntityTypeConfiguration<EquipoBiometrico>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Equipo Biometrico con la tabla 
        /// EB_EQUIPO_BIOMETRICO</param>
        public void Configure(EntityTypeBuilder<EquipoBiometrico> builder)
        {
            builder.ToTable("EB_EQUIPO_BIOMETRICO");
            builder.HasKey(g => g.CodigoEquipoBiometrico);

            builder.Property(g => g.CodigoEquipoBiometrico).HasColumnName("COD_EQUIPO_BIOMETRICO");
            builder.Property(g => g.CodigoNomenclatura).HasColumnName("COD_NOMENCLATURA");
            builder.Property(g => g.NombreEquipoBiometrico).HasColumnName("NOM_EQUIPO_BIOMETRICO");
            builder.Property(g => g.DireccionRedEquipoBiometrico).HasColumnName("DES_DIRECCION_RED");
            builder.Property(g => g.CodigoArea).HasColumnName("COD_AREA");
            builder.Property(g => g.DireccionFisicaEquipoBiometrico).HasColumnName("DES_DIRECCION_FISICA");
            builder.Property(g => g.IndicadorEstado).HasColumnName("IND_ESTADO");

            builder.HasOne(g => g.Nomenclatura).WithMany().HasForeignKey(g => g.CodigoNomenclatura);
            builder.HasOne(g => g.Area).WithMany(g => g.EquiposBiometricos).HasForeignKey(g => g.CodigoArea);

            builder.Ignore(g => g.EsEquipoBiometricoActivo);
            builder.Ignore(g => g.EsEquipoBiometricoInactivo);
            builder.Ignore(g => g.NombreEquipoBiometricoAnterior);
            builder.Ignore(g => g.DireccionRedEquipoBiometricoAnterior);
            builder.Ignore(g => g.EsNombreEquipoBiometricoCambiado);
            builder.Ignore(g => g.EsDireccionRedEquipoBiometricoCambiado);
        }
    }
}
