using API.SRICA.Infraestructura.Configuracion.MapeoEntidad.AR;
using API.SRICA.Infraestructura.Configuracion.MapeoEntidad.BT;
using API.SRICA.Infraestructura.Configuracion.MapeoEntidad.EB;
using API.SRICA.Infraestructura.Configuracion.MapeoEntidad.PE;
using API.SRICA.Infraestructura.Configuracion.MapeoEntidad.SE;
using API.SRICA.Infraestructura.Configuracion.MapeoEntidad.SI;
using API.SRICA.Infraestructura.Configuracion.MapeoEntidad.US;
using Microsoft.EntityFrameworkCore;

namespace API.SRICA.Infraestructura.Configuracion.Contexto
{
    /// <summary>
    /// Contexto de consultas del proyecto
    /// </summary>
    public class ContextoConsulta : DbContext
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="contextoConsulta">Contexto de consultas</param>
        public ContextoConsulta(DbContextOptions<ContextoConsulta> contextoConsulta) 
            : base(contextoConsulta)
        {

        }
        /// <summary>
        /// Creación del modelo
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RolUsuarioConfiguracion());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
            modelBuilder.ApplyConfiguration(new SedeConfiguracion());
            modelBuilder.ApplyConfiguration(new AreaConfiguracion());
            modelBuilder.ApplyConfiguration(new NomenclaturaEquipoBiometricoConfiguracion());
            modelBuilder.ApplyConfiguration(new EquipoBiometricoConfiguracion());
            modelBuilder.ApplyConfiguration(new PersonalEmpresaConfiguracion());
            modelBuilder.ApplyConfiguration(new PersonalEmpresaXAreaConfiguracion());
            modelBuilder.ApplyConfiguration(new BitacoraAccionSistemaConfiguracion());
            modelBuilder.ApplyConfiguration(new BitacoraAccionEquipoBiometricoConfiguracion());
            modelBuilder.ApplyConfiguration(new AccionSistemaConfiguracion());
            modelBuilder.ApplyConfiguration(new ModuloSistemaConfiguracion());
            modelBuilder.ApplyConfiguration(new RecursoSistemaConfiguracion());
            modelBuilder.ApplyConfiguration(new TipoEventoSistemaConfiguracion());
            modelBuilder.ApplyConfiguration(new ResultadoAccesoConfiguracion());
            base.OnModelCreating(modelBuilder);
        }
    }
}
