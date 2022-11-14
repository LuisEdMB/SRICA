using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.US;
using API.SRICA.Dominio.Interfaz;
using System.Linq;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación del servicio de consultas de roles de usuario
    /// </summary>
    public class ServicioRolUsuario : IServicioRolUsuario
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
        /// Servicio de mapeo del rol de usuario a DTO
        /// </summary>
        private readonly IServicioMapeoRolUsuarioADto _servicioMapeoRolUsuarioADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="repositorioConsulta">Repositorio de consultas a la base de datos</param>
        /// <param name="servicioMapeoRolUsuarioADTO">Servicio de mapeo del rol de usuario a DTO</param>
        public ServicioRolUsuario(IServicioEncriptador servicioEncriptador, 
            IRepositorioConsulta repositorioConsulta,
            IServicioMapeoRolUsuarioADto servicioMapeoRolUsuarioADTO) 
        {
            _servicioEncriptador = servicioEncriptador;
            _repositorioConsulta = repositorioConsulta;
            _servicioMapeoRolUsuarioADTO = servicioMapeoRolUsuarioADTO;
        }
        /// <summary>
        /// Método que obtiene el listado de roles de usuario, tanto activos como inactivos
        /// </summary>
        /// <returns>Resultado encriptado con el listado de los roles de usuario</returns>
        public string ObtenerListadoDeRolesDeUsuario()
        {
            var rolesUsuario = _repositorioConsulta.ObtenerPorExpresionLimite<RolUsuario>().ToList();
            var rolesUsuarioDTO = rolesUsuario.Select(g => 
                _servicioMapeoRolUsuarioADTO.MapearADTO(g)).ToList();
            return _servicioEncriptador.Encriptar(rolesUsuarioDTO);
        }
    }
}
