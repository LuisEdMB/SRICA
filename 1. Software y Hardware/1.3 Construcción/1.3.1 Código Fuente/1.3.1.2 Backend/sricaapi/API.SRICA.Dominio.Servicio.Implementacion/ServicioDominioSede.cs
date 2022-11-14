using API.SRICA.Dominio.Entidad.AR;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.SE;
using API.SRICA.Dominio.Excepcion;
using API.SRICA.Dominio.Servicio.Interfaz;

namespace API.SRICA.Dominio.Servicio.Implementacion
{
    /// <summary>
    /// Implementación del servicio de dominio para las sedes
    /// </summary>
    public class ServicioDominioSede : IServicioDominioSede
    {
        /// <summary>
        /// Método que crea la entidad sede
        /// </summary>
        /// <param name="descripcionSede">Descripción de la sede</param>
        /// <returns>Sede creada</returns>
        public Sede CrearSede(string descripcionSede)
        {
            ValidarDescripcionDeLaSede(descripcionSede);
            return Sede.CrearSede(descripcionSede);
        }
        /// <summary>
        /// Método que modifica la entidad sede
        /// </summary>
        /// <param name="sede">Sede a modificar</param>
        /// <param name="descripcionSede">Descripción de la sede</param>
        /// <returns>Sede modificada</returns>
        public Sede ModificarSede(Sede sede, string descripcionSede)
        {
            ValidarDescripcionDeLaSede(descripcionSede);
            sede.ModificarSede(descripcionSede);
            return sede;
        }
        /// <summary>
        /// Método que valida que se haya seleccionado, por lo menos, una sede para ser
        /// inhabilitada o habilitada
        /// </summary>
        /// <param name="cantidadSedesSeleccionadas">Cantidad de sedes seleccionadas</param>
        public void ValidarSedesSeleccionadas(int cantidadSedesSeleccionadas)
        {
            if (cantidadSedesSeleccionadas == 0)
                throw new ExcepcionAplicacionPersonalizada("Debe seleccionar, al menos, una sede de la lista.");
        }
        /// <summary>
        /// Método que inhabilita la entidad sede
        /// </summary>
        /// <param name="sede">Sede a inhabilitar</param>
        /// <returns>Sede inhabilitada</returns>
        public Sede InhabilitarSede(Sede sede)
        {
            sede.InhabilitarSede();
            return sede;
        }
        /// <summary>
        /// Método que habilita la entidad sede
        /// </summary>
        /// <param name="sede">Sede a habilitar</param>
        /// <returns>Sede habilitada</returns>
        public Sede HabilitarSede(Sede sede)
        {
            sede.HabilitarSede();
            return sede;
        }
        /// <summary>
        /// Método que asigna el registro "Sin asignación" de sede a un área
        /// </summary>
        /// <param name="area">Área a modificar</param>
        /// <returns>Área modificada</returns>
        public Area AsignarSedeSinAsignacionAArea(Area area)
        {
            area.AsignarSedeSinAsignacion();
            return area;
        }
        /// <summary>
        /// Método que asigna el registro "Sin asignación" de área (que pertenece a una sede) 
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
        /// Método que valida la descripción de la sede
        /// </summary>
        /// <param name="descripcionSede">Descripción de sede a validar</param>
        private void ValidarDescripcionDeLaSede(string descripcionSede)
        {
            if (!descripcionSede.ValidarCantidadCaracteres(1, 40))
                throw new ExcepcionAplicacionPersonalizada("La cantidad máxima de caracteres para la sede " +
                    "es de 40 caracteres.");
        }
        #endregion
    }
}
