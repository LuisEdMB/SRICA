using System.Collections.Generic;

namespace API.SRICA.Dominio.Interfaz
{
    /// <summary>
    /// Interfaz del repositorio de operación
    /// </summary>
    public interface IRepositorioOperacion
    {
        /// <summary>
        /// Método que modifica la entidad al estado "Agregado"
        /// </summary>
        /// <typeparam name="T">T entidad</typeparam>
        /// <param name="entidad">Entidad a modificar</param>
        void Agregar<T>(T entidad) where T : class;
        /// <summary>
        /// Método que modifica un listado de entidades al estado "Agregado"
        /// </summary>
        /// <typeparam name="T">T entidad</typeparam>
        /// <param name="entidades">Listado de entidades a modificar</param>
        void AgregarLista<T>(List<T> entidades) where T : class;
        /// <summary>
        /// Método que modifica la entidad al estado "Modificado"
        /// </summary>
        /// <typeparam name="T">T entidad</typeparam>
        /// <param name="entidad">Entidad a modificar</param>
        void Modificar<T>(T entidad) where T : class;
        /// <summary>
        /// Método para guardar los cambios realizados
        /// </summary>
        void GuardarCambios();
    }
}
