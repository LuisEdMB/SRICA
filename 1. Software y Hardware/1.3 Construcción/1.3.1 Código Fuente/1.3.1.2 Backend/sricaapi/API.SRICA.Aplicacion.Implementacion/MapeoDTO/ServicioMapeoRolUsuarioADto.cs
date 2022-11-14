using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.US;

namespace API.SRICA.Aplicacion.Implementacion.MapeoDTO
{
    /// <summary>
    /// Implementación para el servicio de mapeo del rol de usuario a un DTO
    /// </summary>
    public class ServicioMapeoRolUsuarioADto : IServicioMapeoRolUsuarioADto
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        public ServicioMapeoRolUsuarioADto(IServicioEncriptador servicioEncriptador)
        {
            _servicioEncriptador = servicioEncriptador;
        }
        /// <summary>
        /// Método que mapea la entidad rol de usuario a un DTO
        /// </summary>
        /// <param name="rolUsuario">Rol de usuario a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        public DtoRolUsuario MapearADTO(RolUsuario rolUsuario)
        {
            return new DtoRolUsuario
            {
                CodigoRolUsuario = _servicioEncriptador.Encriptar(rolUsuario.CodigoRolUsuario),
                DescripcionRolUsuario = rolUsuario.DescripcionRolUsuario,
                IndicadorEstado = rolUsuario.IndicadorEstado
            };
        }
    }
}
