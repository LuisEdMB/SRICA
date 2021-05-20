using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace API.SRICA.Dominio.Interfaz
{
    /// <summary>
    /// Interfaz del repositorio de consulta
    /// </summary>
    public interface IRepositorioConsulta
    {
        /// <summary>
        /// Método que obtiene una entidad T por sus llaves
        /// </summary>
        /// <typeparam name="T">T clase</typeparam>
        /// <param name="llaves">Llaves a buscar</param>
        /// <returns>Entidad T encontrada</returns>
        T ObtenerPorCodigo<T>(params object[] llaves) where T : class;
        /// <summary>
        /// Método que obtiene un listado de entidad T según filtros
        /// </summary>
        /// <typeparam name="T">T clase</typeparam>
        /// <param name="valor">Filtros a usar</param>
        /// <returns>Listado de entidad T encontrado</returns>
        ICollection<T> ObtenerPorExpresionLimite<T>(Expression<Func<T, bool>> valor = null)
            where T : class;
        /// <summary>
        /// Método que obtiene un listado de entidad T según filtros (no tracking)
        /// </summary>
        /// <typeparam name="T">T clase</typeparam>
        /// <param name="valor">Filtros a usar</param>
        /// <returns>Listado de entidad T encontrado</returns>
        ICollection<T> ObtenerPorExpresionLimiteNoTracking<T>(Expression<Func<T, bool>> valor = null)
            where T : class;
    }
}
