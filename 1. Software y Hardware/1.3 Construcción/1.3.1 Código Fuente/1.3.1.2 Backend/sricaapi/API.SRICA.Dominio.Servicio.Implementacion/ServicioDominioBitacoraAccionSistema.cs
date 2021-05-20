using API.SRICA.Dominio.Entidad.BT;
using API.SRICA.Dominio.Entidad.SI;
using API.SRICA.Dominio.Entidad.US;
using API.SRICA.Dominio.Servicio.Interfaz;
using System.Collections.Generic;
using System.Linq;

namespace API.SRICA.Dominio.Servicio.Implementacion
{
    /// <summary>
    /// Implementación del servicio de dominio para las bitácoras de acción del sistema
    /// </summary>
    public class ServicioDominioBitacoraAccionSistema : IServicioDominioBitacoraAccionSistema
    {
        /// <summary>
        /// Método que crea la entidad bitácora de acción del sistema
        /// </summary>
        /// <param name="usuario">Usuario de acción</param>
        /// <param name="moduloSistema">Modulo de acción del sistema</param>
        /// <param name="recursoSistema">Recurso de acción del sistema</param>
        /// <param name="tipoEventoSistema">Tipo de evento de acción del sistema</param>
        /// <param name="accionSistema">Acción del sistema</param>
        /// <param name="descripcionResultado">Descripción del resultado de la acción</param>
        /// <param name="valorAnterior">Valor anterior</param>
        /// <param name="valorActual">Valor actual</param>
        /// <returns>Bitácora de acción del sistema creada</returns>
        public BitacoraAccionSistema CrearBitacora(Usuario usuario, ModuloSistema moduloSistema, 
            RecursoSistema recursoSistema, TipoEventoSistema tipoEventoSistema,
            AccionSistema accionSistema, string descripcionResultado, string valorAnterior,
            string valorActual)
        {
            return BitacoraAccionSistema.CrearBitacora(usuario, moduloSistema, recursoSistema, 
                tipoEventoSistema, accionSistema, descripcionResultado, valorAnterior, valorActual);
        }
        /// <summary>
        /// Método que filtra un listado de bitácora de acción del sistema segun una acción
        /// del sistema
        /// </summary>
        /// <param name="bitacora">Listado de bitácora a filtrar</param>
        /// <param name="accionSistema">Acción del sistema a considerar</param>
        /// <returns>Listado de bitácora filtrado</returns>
        public List<BitacoraAccionSistema> FiltrarBitacoraDeAccionSegunAccionDelSistema(
            List<BitacoraAccionSistema> bitacora, AccionSistema accionSistema)
        {
            return bitacora.Where(g => g.CodigoAccionSistema == accionSistema.CodigoAccionSistema).ToList();
        }
        /// <summary>
        /// Método que filtra un listado de bitácora de acción del sistema según un tipo
        /// de evento del sistema
        /// </summary>
        /// <param name="bitacora">Listado de bitácora a filtrar</param>
        /// <param name="tipoEventoSistema">Tipo de evento del sistema a considerar</param>
        /// <returns>Listado de bitácora filtrado</returns>
        public List<BitacoraAccionSistema> FiltrarBitacoraDeAccionSegunTipoDeEvento(
            List<BitacoraAccionSistema> bitacora, TipoEventoSistema tipoEventoSistema)
        {
            return bitacora.Where(g => 
                g.CodigoTipoEventoSistema == tipoEventoSistema.CodigoTipoEventoSistema).ToList();
        }
        /// <summary>
        /// Método que ordena un listado de bitácora de acción del sistema por fecha de acción.
        /// Opcionalmente, se puede ordenar de forma ascendente o descendente, y considerar cierta cantidad
        /// de registros del listado resultante
        /// </summary>
        /// <param name="bitacora">Listado de bitácora a filtrar</param>
        /// <param name="ascendente">Ordenar de forma ascendente (TRUE) o descendente (FALSE)
        /// (inicializado en TRUE)</param>
        /// <param name="limite">Cantidad de registros a tomar del listado resultante (opcional)</param>
        /// <returns>Listado de bitácora ordenado</returns>
        public List<BitacoraAccionSistema> OrdenarBitacoraDeAccionPorFecha(
            List<BitacoraAccionSistema> bitacora, bool ascendente = true, int limite = -1)
        {
            if (ascendente)
                return bitacora.OrderBy(g => g.FechaAccion).Take(limite == -1 
                    ? bitacora.Count() 
                    : limite).ToList();
            return bitacora.OrderByDescending(g => g.FechaAccion).Take(limite == -1
                ? bitacora.Count()
                : limite).ToList();
        }
    }
}
