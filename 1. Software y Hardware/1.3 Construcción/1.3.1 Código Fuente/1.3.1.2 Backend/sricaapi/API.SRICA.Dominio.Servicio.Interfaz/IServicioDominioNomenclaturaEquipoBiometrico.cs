using API.SRICA.Dominio.Entidad.EB;
using System.Collections.Generic;

namespace API.SRICA.Dominio.Servicio.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de dominio de las nomenclaturas para equipos biométricos
    /// </summary>
    public interface IServicioDominioNomenclaturaEquipoBiometrico
    {
        /// <summary>
        /// Método que crea la entidad nomenclatura para equipos biométricos
        /// </summary>
        /// <param name="nomenclaturasRegistradas">Listado de nomenclaturas registrados, tanto activos
        /// como inactivos (usado para validación de duplicidad)</param>
        /// <param name="descripcionNomenclatura">Descripción de la nomenclatura</param>
        /// <returns>Nomenclatura creada</returns>
        NomenclaturaEquipoBiometrico CrearNomenclatura(
            List<NomenclaturaEquipoBiometrico> nomenclaturasRegistradas, 
            string descripcionNomenclatura);
        /// <summary>
        /// Método que modifica la entidad nomenclatura para equipos biométricos
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura a modificar</param>
        /// <param name="nomenclaturasRegistradas">Listado de nomenclaturas registrados, tanto activos
        /// como inactivos (usado para validación de duplicidad)</param>
        /// <param name="descripcionNomenclatura">Descripción de la nomenclatura</param>
        /// <returns>Nomenclatura modificada</returns>
        NomenclaturaEquipoBiometrico ModificarNomenclatura(
            NomenclaturaEquipoBiometrico nomenclatura,
            List<NomenclaturaEquipoBiometrico> nomenclaturasRegistradas,
            string descripcionNomenclatura);
        /// <summary>
        /// Método que valida que se haya seleccionado, por lo menos, una nomenclatura para ser
        /// inhabilitada o habilitada
        /// </summary>
        /// <param name="cantidadNomenclaturasSeleccionadas">Cantidad de nomenclaturas seleccionadas</param>
        void ValidarNomenclaturasSeleccionadas(int cantidadNomenclaturasSeleccionadas);
        /// <summary>
        /// Método que inhabilita la entidad nomenclatura para equipos biométricos
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura a inhabilitar</param>
        /// <returns>Nomenclatura inhabilitada</returns>
        NomenclaturaEquipoBiometrico InhabilitarNomenclatura(NomenclaturaEquipoBiometrico nomenclatura);
        /// <summary>
        /// Método que habilita la entidad nomenclatura para equipos biométricos
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura a habilitar</param>
        /// <returns>Nomenclatura habilitada</returns>
        NomenclaturaEquipoBiometrico HabilitarNomenclatura(NomenclaturaEquipoBiometrico nomenclatura);
        /// <summary>
        /// Método que asigna el registro "Sin asignación" de nomenclatura
        /// a un equipo biométrico
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a modificar</param>
        /// <returns>Equipo biométrico modificado</returns>
        EquipoBiometrico AsignarNomenclaturaSinAsignacionAEquipoBiometrico(
            EquipoBiometrico equipoBiometrico);
    }
}
