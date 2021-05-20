using API.SRICA.Dominio.Interfaz;
using API.SRICA.Infraestructura.Configuracion.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.SRICA.Infraestructura.Configuracion.Repositorio
{
    /// <summary>
    /// Implementación del repositorio de consulta
    /// </summary>
    public class RepositorioConsulta : IRepositorioConsulta
    {
        /// <summary>
        /// Contexto de consulta del proyecto
        /// </summary>
        private readonly ContextoConsulta _contextoConsulta;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="contextoConsulta">Contexto de consulta del proyecto</param>
        public RepositorioConsulta(ContextoConsulta contextoConsulta)
        {
            _contextoConsulta = contextoConsulta;
        }
        /// <summary>
        /// Método que obtiene una entidad T por sus llaves
        /// </summary>
        /// <typeparam name="T">T clase</typeparam>
        /// <param name="llaves">Llaves a buscar</param>
        /// <returns>Entidad T encontrada</returns>
        public T ObtenerPorCodigo<T>(params object[] llaves) where T : class
        {
            return _contextoConsulta.Set<T>().Find(llaves);
        }
        /// <summary>
        /// Método que obtiene un listado de entidad T según filtros
        /// </summary>
        /// <typeparam name="T">T clase</typeparam>
        /// <param name="valor">Filtros a usar</param>
        /// <returns>Listado de entidad T encontrado</returns>
        public ICollection<T> ObtenerPorExpresionLimite<T>(Expression<Func<T, bool>> valor = null) 
            where T : class
        {
            RefrescarEntidades();
            if (valor == null) return _contextoConsulta.Set<T>().ToList();
            return _contextoConsulta.Set<T>().Where(valor).ToList();
        }
        /// <summary>
        /// Método que obtiene un listado de entidad T según filtros (no tracking)
        /// </summary>
        /// <typeparam name="T">T clase</typeparam>
        /// <param name="valor">Filtros a usar</param>
        /// <returns>Listado de entidad T encontrado</returns>
        public ICollection<T> ObtenerPorExpresionLimiteNoTracking<T>(Expression<Func<T, bool>> valor = null)
           where T : class
        {
            RefrescarEntidades();
            if (valor == null) return _contextoConsulta.Set<T>().AsNoTracking().ToList();
            return _contextoConsulta.Set<T>().AsNoTracking().Where(valor).ToList();
        }
        #region Métodos privados
        /// <summary>
        /// Método que refresca las entidades del contexto
        /// </summary>
        private void RefrescarEntidades()
        {
            foreach (var entity in _contextoConsulta.ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }
        #endregion
    }
}
