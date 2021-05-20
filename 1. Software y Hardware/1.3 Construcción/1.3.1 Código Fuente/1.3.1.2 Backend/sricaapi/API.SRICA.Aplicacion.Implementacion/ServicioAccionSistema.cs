using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.SI;
using API.SRICA.Dominio.Interfaz;
using System.Linq;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación para el servicio de consultas de las acciones del sistema
    /// </summary>
    public class ServicioAccionSistema : IServicioAccionSistema
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Repositorio de consultas a la base de datos
        /// </summary>
        private readonly IRepositorioConsulta _repositorioConsulta;
        /// <summary>
        /// Servicio de mapeo de datos del sistema a DTO
        /// </summary>
        private readonly IServicioMapeoSistemaADto _servicioMapeoSistemaADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="repositorioConsulta">Repositorio de consultas a la base de datos</param>
        /// <param name="servicioMapeoSistemaADTO">Servicio de mapeo de datos del sistema a DTO</param>
        public ServicioAccionSistema(IServicioEncriptador servicioEncriptador,
            IRepositorioConsulta repositorioConsulta,
            IServicioMapeoSistemaADto servicioMapeoSistemaADTO)
        {
            _servicioEncriptador = servicioEncriptador;
            _repositorioConsulta = repositorioConsulta;
            _servicioMapeoSistemaADTO = servicioMapeoSistemaADTO;
        }
        /// <summary>
        /// Método que obtiene el listado de acciones del sistema
        /// </summary>
        /// <returns>Resultado encriptado con el listado de acciones del sistema encontrados</returns>
        public string ObtenerListadoDeAccionesDelSistema()
        {
            var acciones = _repositorioConsulta.ObtenerPorExpresionLimite<AccionSistema>().ToList();
            var accionesDTO = acciones.Select(g => _servicioMapeoSistemaADTO.MapearADTO(
                g.CodigoAccionSistema, g.DescripcionAccionSistema)).ToList();
            return _servicioEncriptador.Encriptar(accionesDTO);
        }
    }
}
