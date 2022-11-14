using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.BT;
using API.SRICA.Dominio.Entidad.US;
using System.Collections.Generic;
using System.Linq;

namespace API.SRICA.Aplicacion.Implementacion.MapeoDTO
{
    /// <summary>
    /// Implementación para el servicio de mapeo del usuario a un DTO
    /// </summary>
    public class ServicioMapeoUsuarioADto : IServicioMapeoUsuarioADto
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        public ServicioMapeoUsuarioADto(IServicioEncriptador servicioEncriptador)
        {
            _servicioEncriptador = servicioEncriptador;
        }
        /// <summary>
        /// Método que mapea la entidad usuario a un DTO
        /// </summary>
        /// <param name="usuario">Usuario a mapear</param>
        /// <param name="accesos">Accesos del usuario en el sistema (opcional)</param>
        /// <returns>DTO con los datos mapeados</returns>
        public DtoUsuario MapearADTO(Usuario usuario, List<BitacoraAccionSistema> accesos = null)
        {
            return new DtoUsuario
            {
                CodigoUsuario = _servicioEncriptador.Encriptar(usuario.CodigoUsuario.ToString()),
                Usuario = usuario.UsuarioAcceso,
                CorreoElectronico = usuario.CorreoElectronico,
                Nombre = usuario.NombreUsuario,
                Apellido = usuario.ApellidoUsuario,
                CodigoRolUsuario = _servicioEncriptador.Encriptar(usuario.CodigoRolUsuario),
                EsAdministrador = usuario.RolUsuario.EsAdministrador,
                EsUsuarioBasico = usuario.RolUsuario.EsUsuarioBasico,
                EsCorreoElectronicoPorDefecto = usuario.EsCorreoElectronicoPorDefecto,
                EsContrasenaPorDefecto = usuario.EsContrasenaPorDefecto,
                IndicadorEstado = usuario.IndicadorEstado,
                RolUsuario = usuario.RolUsuario.DescripcionRolUsuario,
                UltimosAccesos = accesos != null 
                    ? accesos.Select(g => g.FechaAccion.ToString("dd/MM/yyyy HH:mm:ss")).ToList()
                    : new List<string>()
            };
        }
    }
}
