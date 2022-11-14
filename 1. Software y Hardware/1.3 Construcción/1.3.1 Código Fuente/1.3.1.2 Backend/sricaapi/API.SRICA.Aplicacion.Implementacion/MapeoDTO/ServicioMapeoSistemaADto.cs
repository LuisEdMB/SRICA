using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;

namespace API.SRICA.Aplicacion.Implementacion.MapeoDTO
{
    /// <summary>
    /// Implementación para el servicio de mapeo de datos del sistema a un DTO
    /// </summary>
    public class ServicioMapeoSistemaADto : IServicioMapeoSistemaADto
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        public ServicioMapeoSistemaADto(IServicioEncriptador servicioEncriptador)
        {
            _servicioEncriptador = servicioEncriptador;
        }
        /// <summary>
        /// Método que mapea los datos del sistema a un DTO
        /// </summary>
        /// <param name="codigo">Código del registro</param>
        /// <param name="descripcion">Descripción del registro</param>
        /// <returns>DTO con los datos mapeados</returns>
        public DtoSistema MapearADTO(int codigo, string descripcion)
        {
            return new DtoSistema
            {
                Codigo = _servicioEncriptador.Encriptar(codigo),
                Descripcion = descripcion
            };
        }
    }
}
