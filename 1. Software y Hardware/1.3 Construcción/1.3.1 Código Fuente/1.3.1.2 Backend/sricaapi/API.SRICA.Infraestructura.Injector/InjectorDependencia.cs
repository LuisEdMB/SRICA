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

            _servicios.AddTransient<ContextoConsulta>();
            _servicios.AddTransient<ContextoOperacion>();
            _servicios.AddTransient<IRepositorioConsulta, RepositorioConsulta>();
            _servicios.AddTransient<IRepositorioOperacion, RepositorioOperacion>();

            _servicios.AddTransient<IServicioEncriptador, ServicioEncriptador>();
            _servicios.AddTransient<IServicioDesencriptador, ServicioDesencriptador>();
            _servicios.AddTransient<IServicioAutenticacion, ServicioAutenticacion>();
            _servicios.AddTransient<IServicioRolUsuario, ServicioRolUsuario>();
            _servicios.AddTransient<IServicioUsuario, ServicioUsuario>();
            _servicios.AddTransient<IServicioSede, ServicioSede>();
            _servicios.AddTransient<IServicioArea, ServicioArea>();
            _servicios.AddTransient<IServicioNomenclaturaEquipoBiometrico, 
                ServicioNomenclaturaEquipoBiometrico>();
            _servicios.AddTransient<IServicioEquipoBiometrico, ServicioEquipoBiometrico>();
            _servicios.AddTransient<IServicioPersonalEmpresa, ServicioPersonalEmpresa>();
            _servicios.AddTransient<IServicioBitacoraAccionSistema, ServicioBitacoraAccionSistema>();
            _servicios.AddTransient<IServicioBitacoraAccionEquipoBiometrico, 
                ServicioBitacoraAccionEquipoBiometrico>();
            _servicios.AddTransient<IServicioModuloSistema, ServicioModuloSistema>();
            _servicios.AddTransient<IServicioRecursoSistema, ServicioRecursoSistema>();
            _servicios.AddTransient<IServicioTipoEventoSistema, ServicioTipoEventoSistema>();
            _servicios.AddTransient<IServicioAccionSistema, ServicioAccionSistema>();
            _servicios.AddTransient<IServicioResultadoAcceso, ServicioResultadoAcceso>();
            _servicios.AddTransient<IServicioIris, ServicioIris>();
            _servicios.AddTransient<IServicioAlerta, ServicioAlerta>();
            _servicios.AddTransient<IServicioDireccionRed, ServicioDireccionRed>();
            _servicios.AddTransient<IServicioArchivo, ServicioArchivo>();

            _servicios.AddTransient<IServicioMapeoUsuarioAutenticadoADto, 
                ServicioMapeoUsuarioAutenticadoADto>();
            _servicios.AddTransient<IServicioMapeoRolUsuarioADto, ServicioMapeoRolUsuarioADto>();
            _servicios.AddTransient<IServicioMapeoUsuarioADto, ServicioMapeoUsuarioADto>();
            _servicios.AddTransient<IServicioMapeoSedeADto, ServicioMapeoSedeADto>();
            _servicios.AddTransient<IServicioMapeoAreaADto, ServicioMapeoAreaADto>();
            _servicios.AddTransient<IServicioMapeoNomenclaturaEquipoBiometricoADto, 
                ServicioMapeoNomenclaturaEquipoBiometricoADto>();
            _servicios.AddTransient<IServicioMapeoEquipoBiometicoADto, ServicioMapeoEquipoBiometicoADto>();
            _servicios.AddTransient<IServicioMapeoPersonalEmpresaADto, ServicioMapeoPersonalEmpresaADto>();
            _servicios.AddTransient<IServicioMapeoPersonalEmpresaXAreaADto, 
                ServicioMapeoPersonalEmpresaXAreaADto>();
            _servicios.AddTransient<IServicioMapeoSistemaADto, ServicioMapeoSistemaADto>();
            _servicios.AddTransient<IServicioMapeoBitacoraAccionSistemaADto, 
                ServicioMapeoBitacoraAccionSistemaADto>();
            _servicios.AddTransient<IServicioMapeoBitacoraAccionEquipoBiometricoADto,
                ServicioMapeoBitacoraAccionEquipoBiometricoADto>();

            _servicios.AddTransient<IServicioCryptoAES, ServicioCryptoAES>();
            _servicios.AddTransient<IServicioToken, ServicioToken>();
            _servicios.AddTransient<IServicioPingHost, ServicioPingHost>();
            _servicios.AddTransient<IServicioDominioUsuario, ServicioDominioUsuario>();
            _servicios.AddTransient<IServicioDominioSede, ServicioDominioSede>();
            _servicios.AddTransient<IServicioDominioArea, ServicioDominioArea>();
            _servicios.AddTransient<IServicioDominioNomenclaturaEquipoBiometrico, 
                ServicioDominioNomenclaturaEquipoBiometrico>();
            _servicios.AddTransient<IServicioDominioEquipoBiometrico, ServicioDominioEquipoBiometrico>();
            _servicios.AddTransient<IServicioDominioPersonalEmpresa, ServicioDominioPersonalEmpresa>();
            _servicios.AddTransient<IServicioDominioBitacoraAccionSistema, 
                ServicioDominioBitacoraAccionSistema>();
            _servicios.AddTransient<IServicioDominioBitacoraAccionEquipoBiometrico,
                ServicioDominioBitacoraAccionEquipoBiometrico>();

            _servicios.AddTransient<IMicroservicioCorreo, MicroservicioCorreo>();
            _servicios.AddTransient<IMicroservicioSegmentacionIris, MicroservicioSegmentacionIris>();
            _servicios.AddTransient<IMicroservicioCodificacionIris, MicroservicioCodificacionIris>();
            _servicios.AddTransient<IMicroservicioReconocimientoIris, MicroservicioReconocimientoIris>();
        }
    }
}
