using API.SRICA.Dominio.Entidad.AR;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.SE;

namespace API.SRICA.Dominio.Servicio.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de dominio para las áreas
    /// </summary>
    public interface IServicioDominioArea
    {
        /// <summary>
        /// Método que crea la entidad área
        /// </summary>
        /// <param name="sede">Sede del área a crear</param>
        /// <param name="descripcionArea">Descripción del área</param>
        /// <returns>Área creada</returns>
        Area CrearArea(Sede sede, string descripcionArea);
        /// <summary>
        /// Método que modifica la entidad área
        /// </summary>
        /// <param name="area">Área a modificar</param>
        /// <param name="sede">Sede del área a modificar</param>
        /// <param name="descripcionArea">Descripción del área</param>
        /// <returns>Área modificada</returns>
        Area ModificarArea(Area area, Sede sede, string descripcionArea);
        /// <summary>
        /// Método que valida que se haya seleccionado, por lo menos, un área para ser
        /// inhabilitada o habilitada
        /// </summary>
        /// <param name="cantidadAreasSeleccionadas">Cantidad de áreas seleccionadas</param>
        void ValidarAreasSeleccionadas(int cantidadAreasSeleccionadas);
        /// <summary>
        /// Método que inhabilita la entidad área
        /// </summary>
        /// <param name="area">Área a inhabilitar</param>
        /// <returns>Área inhabilitada</returns>
        Area InhabilitarArea(Area area);
        /// <summary>
        /// Método que habilita la entidad área
        /// </summary>
        /// <param name="area">Área a habilitar</param>
        /// <returns>Área habilitada</returns>
        Area HabilitarArea(Area area);
        /// <summary>
        /// Método que asigna el registro "Sin asignación" de área
        /// a un equipo biométrico
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a modificar</param>
        /// <returns>Equipo biométrico modificado</returns>
        EquipoBiometrico AsignarAreaSinAsignacionAEquipoBiometrico(EquipoBiometrico equipoBiometrico);
    }
}
