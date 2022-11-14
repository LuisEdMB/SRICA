using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.US;

namespace API.SRICA.Aplicacion.Implementacion.MapeoDTO
{
    /// <summary>
    /// Implementación para el servicio de mapeo del usuario autenticado a un DTO
    /// </summary>
    public class ServicioMapeoUsuarioAutenticadoADto : IServicioMapeoUsuarioAutenticadoADto
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        public ServicioMapeoUsuarioAutenticadoADto(IServicioEncriptador servicioEncriptador)
        {
            _servicioEncriptador = servicioEncriptador;
        }
        /// <summary>
        /// Método que mapea la entidad usuario a un DTO
        /// </summary>
        /// <param name="usuario">Usuario autenticado a mapear</param>
        /// <param name="token">Token del usuario</param>
        /// <returns>DTO con los datos mapeados</returns>
        public DtoUsuario MapearADTO(Usuario usuario, string token)
        {
            return new DtoUsuario
            {
                CodigoUsuario = _servicioEncriptador.Encriptar(usuario.CodigoUsuario.ToString()),
                Usuario = usuario.UsuarioAcceso,
                Nombre = usuario.NombreUsuario,
                Apellido = usuario.ApellidoUsuario,
                EsAdministrador = usuario.RolUsuario.EsAdministrador,
                EsUsuarioBasico = usuario.RolUsuario.EsUsuarioBasico,
                EsCorreoElectronicoPorDefecto = usuario.EsCorreoElectronicoPorDefecto,
                EsContrasenaPorDefecto = usuario.EsContrasenaPorDefecto,
                Token = token
            };
        }
    }
}
