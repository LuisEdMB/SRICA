using API.SRICA.Dominio.Entidad.EB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.SRICA.Infraestructura.Configuracion.MapeoEntidad.EB
{
    /// <summary>
    /// Clase para la configuración entre la entidad Nomenclatura Equipo Biometrico con 
    /// la tabla EB_NOMENCLATURA_EQUIPO_BIOMETRICO
    /// </summary>
    public class NomenclaturaEquipoBiometricoConfiguracion : 
        IEntityTypeConfiguration<NomenclaturaEquipoBiometrico>
    {
        /// <summary>
        /// Método de configuración
        /// </summary>
        /// <param name="builder">Constructor de la entidad Nomenclatura Equipo Biometrico 
        /// con la tabla EB_NOMENCLATURA_EQUIPO_BIOMETRICO</param>
        public void Configure(EntityTypeBuilder<NomenclaturaEquipoBiometrico> builder)
        {
            builder.ToTable("EB_NOMENCLATURA_EQUIPO_BIOMETRICO");
            builder.HasKey(g => g.CodigoNomenclatura);

            builder.Property(g => g.CodigoNomenclatura).HasColumnName("COD_NOMENCLATURA");
            builder.Property(g => g.DescripcionNomenclatura).HasColumnName("DES_NOMENCLATURA");
            builder.Property(g => g.IndicadorRegistroParaSinAsignacion)
                .HasColumnName("IND_REGISTRO_PARA_SIN_ASIGNACION");
            builder.Property(g => g.IndicadorEstado).HasColumnName("IND_ESTADO");

            builder.Ignore(g => g.EsNomenclaturaActivo);
            builder.Ignore(g => g.EsNomenclaturaInactivo);
        }
    }
}
