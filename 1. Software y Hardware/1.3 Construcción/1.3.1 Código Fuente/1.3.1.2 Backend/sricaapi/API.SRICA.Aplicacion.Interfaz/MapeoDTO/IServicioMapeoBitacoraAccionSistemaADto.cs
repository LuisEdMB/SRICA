using API.SRICA.Aplicacion.DTO;
using API.SRICA.Dominio.Entidad.BT;

namespace API.SRICA.Aplicacion.Interfaz.MapeoDTO
{
    /// <summary>
    /// Interfaz para el servicio de mapeo de la bitácora de acción del sistema a un DTO
    /// </summary>
    public interface IServicioMapeoBitacoraAccionSistemaADto
    {
        /// <summary>
        /// Método que mapea la entidad bitácora de acción del sistema a un DTO
        /// </summary>
        /// <param name="bitacoraAccionSistema">Bitácora de acción del sistema a mapear</param>
        /// <returns>DTO con los datos mapeados</returns>
        DtoBitacoraAccionSistema MapearADTO(BitacoraAccionSistema bitacoraAccionSistema);
    }
}
