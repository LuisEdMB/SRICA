using API.SRICA.Dominio.Entidad.AR;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.SE;

namespace API.SRICA.Dominio.Servicio.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de dominio para las sedes
    /// </summary>
    public interface IServicioDominioSede
    {
        /// <summary>
        /// Método que crea la entidad sede
        /// </summary>
        /// <param name="descripcionSede">Descripción de la sede</param>
        /// <returns>Sede creada</returns>
        Sede CrearSede(string descripcionSede);
        /// <summary>
        /// Método que modifica la entidad sede
        /// </summary>
        /// <param name="sede">Sede a modificar</param>
        /// <param name="descripcionSede">Descripción de la sede</param>
        /// <returns>Sede modificada</returns>
        Sede ModificarSede(Sede sede, string descripcionSede);
        /// <summary>
        /// Método que valida que se haya seleccionado, por lo menos, una sede para ser
        /// inhabilitada o habilitada
        /// </summary>
        /// <param name="cantidadSedesSeleccionadas">Cantidad de sedes seleccionadas</param>
        void ValidarSedesSeleccionadas(int cantidadSedesSeleccionadas);
        /// <summary>
        /// Método que inhabilita la entidad sede
        /// </summary>
        /// <param name="sede">Sede a inhabilitar</param>
        /// <returns>Sede inhabilitada</returns>
        Sede InhabilitarSede(Sede sede);
        /// <summary>
        /// Método que habilita la entidad sede
        /// </summary>
        /// <param name="sede">Sede a habilitar</param>
        /// <returns>Sede habilitada</returns>
        Sede HabilitarSede(Sede sede);
        /// <summary>
        /// Método que asigna el registro "Sin asignación" de sede a un área
        /// </summary>
        /// <param name="area">Área a modificar</param>
        /// <returns>Área modificada</returns>
        Area AsignarSedeSinAsignacionAArea(Area area);
        /// <summary>
        /// Método que asigna el registro "Sin asignación" de área (que pertenece a una sede) 
        /// a un equipo biométrico
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a modificar</param>
        /// <returns>Equipo biométrico modificado</returns>
        EquipoBiometrico AsignarAreaSinAsignacionAEquipoBiometrico(EquipoBiometrico equipoBiometrico);
    }
}
