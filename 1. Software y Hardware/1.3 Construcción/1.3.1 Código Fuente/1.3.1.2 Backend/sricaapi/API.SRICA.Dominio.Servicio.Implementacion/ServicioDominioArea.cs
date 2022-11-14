using API.SRICA.Dominio.Entidad.AR;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.SE;
using API.SRICA.Dominio.Excepcion;
using API.SRICA.Dominio.Servicio.Interfaz;

namespace API.SRICA.Dominio.Servicio.Implementacion
{
    /// <summary>
    /// Implementación del servicio de dominio para las áreas
    /// </summary>
    public class ServicioDominioArea : IServicioDominioArea
    {
        /// <summary>
        /// Método que crea la entidad área
        /// </summary>
        /// <param name="sede">Sede del área a crear</param>
        /// <param name="descripcionArea">Descripción del área</param>
        /// <returns>Área creada</returns>
        public Area CrearArea(Sede sede, string descripcionArea)
        {
            ValidarDescripcionDelArea(descripcionArea);
            ValidarSedeDelArea(sede);
            return Area.CrearArea(sede, descripcionArea);
        }
        /// <summary>
        /// Método que modifica la entidad área
        /// </summary>
        /// <param name="area">Área a modificar</param>
        /// <param name="sede">Sede del área a modificar</param>
        /// <param name="descripcionArea">Descripción del área</param>
        /// <returns>Área modificada</returns>
        public Area ModificarArea(Area area, Sede sede, string descripcionArea)
        {
            ValidarDescripcionDelArea(descripcionArea);
            ValidarSedeDelArea(sede);
            area.ModificarArea(sede, descripcionArea);
            return area;
        }
        /// <summary>
        /// Método que valida que se haya seleccionado, por lo menos, un área para ser
        /// inhabilitada o habilitada
        /// </summary>
        /// <param name="cantidadAreasSeleccionadas">Cantidad de áreas seleccionadas</param>
        public void ValidarAreasSeleccionadas(int cantidadAreasSeleccionadas)
        {
            if (cantidadAreasSeleccionadas == 0)
                throw new ExcepcionAplicacionPersonalizada("Debe seleccionar, al menos, un área de " +
                    "la lista.");
        }
        /// <summary>
        /// Método que inhabilita la entidad área
        /// </summary>
        /// <param name="area">Área a inhabilitar</param>
        /// <returns>Área inhabilitada</returns>
        public Area InhabilitarArea(Area area)
        {
            area.InhabilitarArea();
            return area;
        }
        /// <summary>
        /// Método que habilita la entidad área
        /// </summary>
        /// <param name="area">Área a habilitar</param>
        /// <returns>Área habilitada</returns>
        public Area HabilitarArea(Area area)
        {
            area.HabilitarArea();
            return area;
        }
        /// <summary>
        /// Método que asigna el registro "Sin asignación" de área
        /// a un equipo biométrico
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a modificar</param>
        /// <returns>Equipo biométrico modificado</returns>
        public EquipoBiometrico AsignarAreaSinAsignacionAEquipoBiometrico(EquipoBiometrico equipoBiometrico)
        {
            equipoBiometrico.AsignarAreaSinAsignacion();
            return equipoBiometrico;
        }
        #region Métodos privados
        /// <summary>
        /// Método que valida la descripción del área
        /// </summary>
        /// <param name="descripcionArea">Descripción de área a validar</param>
        private void ValidarDescripcionDelArea(string descripcionArea)
        {
            if (!descripcionArea.ValidarCantidadCaracteres(1, 40))
                throw new ExcepcionAplicacionPersonalizada("La cantidad máxima de caracteres para el área " +
                    "es de 40 caracteres.");
        }
        /// <summary>
        /// Método que valida la sede del área
        /// </summary>
        /// <param name="sede">Sede a validar</param>
        private void ValidarSedeDelArea(Sede sede)
        {
            if (sede.EsSedeInactivo)
                throw new ExcepcionAplicacionPersonalizada("La sede seleccionada \"" + sede.DescripcionSede + 
                    "\" se encuentra en estado INACTIVO.");
        }
        #endregion
    }
}
