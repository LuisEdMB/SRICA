using API.SRICA.Dominio.Interfaz;
using API.SRICA.Infraestructura.Configuracion.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.SRICA.Infraestructura.Configuracion.Repositorio
{
    /// <summary>
    /// Implementación del repositorio de operación
    /// </summary>
    public class RepositorioOperacion : IRepositorioOperacion
    {
        /// <summary>
        /// Contexto de operación del proyecto
        /// </summary>
        private readonly ContextoOperacion _contextoOperacion;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="contextoOperacion">Contexto de operación del proyecto</param>
        public RepositorioOperacion(ContextoOperacion contextoOperacion)
        {
            _contextoOperacion = contextoOperacion;
        }
        /// <summary>
        /// Método que modifica la entidad al estado "Agregado"
        /// </summary>
        /// <typeparam name="T">T entidad</typeparam>
        /// <param name="entidad">Entidad a modificar</param>
        public void Agregar<T>(T entidad) where T : class
        {
            _contextoOperacion.Entry(entidad).State = EntityState.Added;
        }
        /// <summary>
        /// Método que modifica un listado de entidades al estado "Agregado"
        /// </summary>
        /// <typeparam name="T">T entidad</typeparam>
        /// <param name="entidades">Listado de entidades a modificar</param>
        public void AgregarLista<T>(List<T> entidades) where T : class
        {
            foreach(var entidad in entidades)
            {
                _contextoOperacion.Entry(entidad).State = EntityState.Added;
            }
        }
        /// <summary>
        /// Método que modifica la entidad al estado "Modificado"
        /// </summary>
        /// <typeparam name="T">T entidad</typeparam>
        /// <param name="entidad">Entidad a modificar</param>
        public void Modificar<T>(T entidad) where T : class
        {
            _contextoOperacion.Entry(entidad).State = EntityState.Modified;
        }
        /// <summary>
        /// Método para guardar los cambios realizados
        /// </summary>
        public void GuardarCambios()
        {
            _contextoOperacion.SaveChanges();
        }
    }
}
