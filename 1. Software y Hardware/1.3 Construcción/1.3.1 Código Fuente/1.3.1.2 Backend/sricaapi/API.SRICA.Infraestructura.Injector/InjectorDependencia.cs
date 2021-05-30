using API.SRICA.Aplicacion.Implementacion;
using API.SRICA.Aplicacion.Implementacion.MapeoDTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Interfaz;
using API.SRICA.Dominio.Servicio.Implementacion;
using API.SRICA.Dominio.Servicio.Interfaz;
using API.SRICA.Dominio.ServicioExterno.Interfaz;
using API.SRICA.Infraestructura.Configuracion.Contexto;
using API.SRICA.Infraestructura.Configuracion.Repositorio;
using API.SRICA.Infraestructura.ServicioExterno.Implementacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.SRICA.Infraestructura.Injector
{
    /// <summary>
    /// Clase para la injección de dependencias del proyecto
    /// </summary>
    public class InjectorDependencia
    {
        /// <summary>
        /// Interfaz Service Collection
        /// </summary>
        private readonly IServiceCollection _servicios;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicios">Interfaz Service Collection</param>
        public InjectorDependencia(IServiceCollection servicios)
        {
            _servicios = servicios;
        }
        /// <summary>
        /// Contenedor de dependencias del proyecto
        /// </summary>
        public void ContenedorDependencia(string conexionBaseDatos)
        {
            _servicios.AddDbContext<ContextoConsulta>(g => g.UseMySQL(conexionBaseDatos)
                .UseLazyLoadingProxies(), ServiceLifetime.Singleton);
            _servicios.AddDbContext<ContextoOperacion>(g => g.UseMySQL(conexionBaseDatos)
                .UseLazyLoadingProxies(), ServiceLifetime.Singleton);

            _servicios.AddScoped<ContextoConsulta>();
            _servicios.AddScoped<ContextoOperacion>();
            _servicios.AddScoped<IRepositorioConsulta, RepositorioConsulta>();
            _servicios.AddScoped<IRepositorioOperacion, RepositorioOperacion>();

            _servicios.AddScoped<IServicioEncriptador, ServicioEncriptador>();
            _servicios.AddScoped<IServicioDesencriptador, ServicioDesencriptador>();
            _servicios.AddScoped<IServicioAutenticacion, ServicioAutenticacion>();
            _servicios.AddScoped<IServicioRolUsuario, ServicioRolUsuario>();
            _servicios.AddScoped<IServicioUsuario, ServicioUsuario>();
            _servicios.AddScoped<IServicioSede, ServicioSede>();
            _servicios.AddScoped<IServicioArea, ServicioArea>();
            _servicios.AddScoped<IServicioNomenclaturaEquipoBiometrico, 
                ServicioNomenclaturaEquipoBiometrico>();
            _servicios.AddScoped<IServicioEquipoBiometrico, ServicioEquipoBiometrico>();
            _servicios.AddScoped<IServicioPersonalEmpresa, ServicioPersonalEmpresa>();
            _servicios.AddScoped<IServicioBitacoraAccionSistema, ServicioBitacoraAccionSistema>();
            _servicios.AddScoped<IServicioBitacoraAccionEquipoBiometrico, 
                ServicioBitacoraAccionEquipoBiometrico>();
            _servicios.AddScoped<IServicioModuloSistema, ServicioModuloSistema>();
            _servicios.AddScoped<IServicioRecursoSistema, ServicioRecursoSistema>();
            _servicios.AddScoped<IServicioTipoEventoSistema, ServicioTipoEventoSistema>();
            _servicios.AddScoped<IServicioAccionSistema, ServicioAccionSistema>();
            _servicios.AddScoped<IServicioResultadoAcceso, ServicioResultadoAcceso>();
            _servicios.AddScoped<IServicioIris, ServicioIris>();
            _servicios.AddScoped<IServicioAlerta, ServicioAlerta>();
            _servicios.AddScoped<IServicioDireccionRed, ServicioDireccionRed>();
            _servicios.AddScoped<IServicioArchivo, ServicioArchivo>();

            _servicios.AddScoped<IServicioMapeoUsuarioAutenticadoADto, 
                ServicioMapeoUsuarioAutenticadoADto>();
            _servicios.AddScoped<IServicioMapeoRolUsuarioADto, ServicioMapeoRolUsuarioADto>();
            _servicios.AddScoped<IServicioMapeoUsuarioADto, ServicioMapeoUsuarioADto>();
            _servicios.AddScoped<IServicioMapeoSedeADto, ServicioMapeoSedeADto>();
            _servicios.AddScoped<IServicioMapeoAreaADto, ServicioMapeoAreaADto>();
            _servicios.AddScoped<IServicioMapeoNomenclaturaEquipoBiometricoADto, 
                ServicioMapeoNomenclaturaEquipoBiometricoADto>();
            _servicios.AddScoped<IServicioMapeoEquipoBiometicoADto, ServicioMapeoEquipoBiometicoADto>();
            _servicios.AddScoped<IServicioMapeoPersonalEmpresaADto, ServicioMapeoPersonalEmpresaADto>();
            _servicios.AddScoped<IServicioMapeoPersonalEmpresaXAreaADto, 
                ServicioMapeoPersonalEmpresaXAreaADto>();
            _servicios.AddScoped<IServicioMapeoSistemaADto, ServicioMapeoSistemaADto>();
            _servicios.AddScoped<IServicioMapeoBitacoraAccionSistemaADto, 
                ServicioMapeoBitacoraAccionSistemaADto>();
            _servicios.AddScoped<IServicioMapeoBitacoraAccionEquipoBiometricoADto,
                ServicioMapeoBitacoraAccionEquipoBiometricoADto>();

            _servicios.AddScoped<IServicioCryptoAES, ServicioCryptoAES>();
            _servicios.AddScoped<IServicioToken, ServicioToken>();
            _servicios.AddScoped<IServicioPingHost, ServicioPingHost>();
            _servicios.AddScoped<IServicioDominioUsuario, ServicioDominioUsuario>();
            _servicios.AddScoped<IServicioDominioSede, ServicioDominioSede>();
            _servicios.AddScoped<IServicioDominioArea, ServicioDominioArea>();
            _servicios.AddScoped<IServicioDominioNomenclaturaEquipoBiometrico, 
                ServicioDominioNomenclaturaEquipoBiometrico>();
            _servicios.AddScoped<IServicioDominioEquipoBiometrico, ServicioDominioEquipoBiometrico>();
            _servicios.AddScoped<IServicioDominioPersonalEmpresa, ServicioDominioPersonalEmpresa>();
            _servicios.AddScoped<IServicioDominioBitacoraAccionSistema, 
                ServicioDominioBitacoraAccionSistema>();
            _servicios.AddScoped<IServicioDominioBitacoraAccionEquipoBiometrico,
                ServicioDominioBitacoraAccionEquipoBiometrico>();

            _servicios.AddScoped<IMicroservicioCorreo, MicroservicioCorreo>();
            _servicios.AddScoped<IMicroservicioSegmentacionIris, MicroservicioSegmentacionIris>();
            _servicios.AddScoped<IMicroservicioCodificacionIris, MicroservicioCodificacionIris>();
            _servicios.AddScoped<IMicroservicioReconocimientoIris, MicroservicioReconocimientoIris>();
        }
    }
}
