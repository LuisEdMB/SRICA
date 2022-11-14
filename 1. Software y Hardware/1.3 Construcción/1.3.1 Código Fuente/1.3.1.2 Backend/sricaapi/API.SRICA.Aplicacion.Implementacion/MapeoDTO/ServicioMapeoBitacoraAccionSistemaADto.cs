using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.BT;

namespace API.SRICA.Aplicacion.Implementacion.MapeoDTO
{
    /// <summary>
    /// Implementación para el servicio de mapeo de la bitácora de acción del sistema a un DTO
    /// </summary>
    public class ServicioMapeoBitacoraAccionSistemaADto : IServicioMapeoBitacoraAccionSistemaADto
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        public ServicioMapeoBitacoraAccionSistemaADto(IServicioEncriptador servicioEncriptador)
        {
            _servicioEncriptador = servicioEncriptador;
        }
        /// <summary>
        /// Método que mapea la entidad bitácora de acción del sistema a un DTO
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        public DtoBitacoraAccionSistema MapearADTO(BitacoraAccionSistema bitacoraAccionSistema)
        {
            return new DtoBitacoraAccionSistema
            {
                CodigoBitacora = _servicioEncriptador.Encriptar(bitacoraAccionSistema.CodigoBitacora),
                CodigoUsuario = _servicioEncriptador.Encriptar(bitacoraAccionSistema.CodigoUsuario),
                UsuarioAcceso = bitacoraAccionSistema.UsuarioAcceso,
                NombreUsuario = bitacoraAccionSistema.NombreUsuario,
                ApellidoUsuario = bitacoraAccionSistema.ApellidoUsuario,
                CodigoRolUsuario = _servicioEncriptador.Encriptar(bitacoraAccionSistema.CodigoRolUsuario),
                DescripcionRolUsuario = bitacoraAccionSistema.RolUsuario.DescripcionRolUsuario,
                CodigoModuloSistema = _servicioEncriptador.Encriptar(
                    bitacoraAccionSistema.CodigoModuloSistema),
                DescripcionModuloSistema = bitacoraAccionSistema.ModuloSistema.DescripcionModuloSistema,
                CodigoRecursoSistema = _servicioEncriptador.Encriptar(
                    bitacoraAccionSistema.CodigoRecursoSistema),
                DescripcionRecursoSistema = bitacoraAccionSistema.RecursoSistema.DescripcionRecursoSistema,
                CodigoTipoEventoSistema = _servicioEncriptador.Encriptar(
                    bitacoraAccionSistema.CodigoTipoEventoSistema),
                DescripcionTipoEventoSistema = 
                    bitacoraAccionSistema.TipoEventoSistema.DescripcionTipoEventoSistema,
                CodigoAccionSistema = _servicioEncriptador.Encriptar(
                    bitacoraAccionSistema.CodigoAccionSistema),
                DescripcionAccionSistema = bitacoraAccionSistema.AccionSistema.DescripcionAccionSistema,
                DescripcionResultadoAccion = bitacoraAccionSistema.DescripcionResultadoAccion,
                ValorAnterior = bitacoraAccionSistema.ValorAnterior,
                ValorActual = bitacoraAccionSistema.ValorActual,
                FechaAccion = bitacoraAccionSistema.FechaAccion.ToString("dd/MM/yyyy HH:mm:ss")
            };
        }
    }
}
