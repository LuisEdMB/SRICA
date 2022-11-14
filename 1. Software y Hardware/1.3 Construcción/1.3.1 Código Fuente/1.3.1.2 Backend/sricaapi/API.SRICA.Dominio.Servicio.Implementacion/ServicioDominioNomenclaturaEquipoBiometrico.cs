using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Excepcion;
using API.SRICA.Dominio.Servicio.Interfaz;
using System.Collections.Generic;
using System.Linq;

namespace API.SRICA.Dominio.Servicio.Implementacion
{
    /// <summary>
    /// Implementación del servicio de dominio de las nomenclaturas para equipos biométricos
    /// </summary>
    public class ServicioDominioNomenclaturaEquipoBiometrico : IServicioDominioNomenclaturaEquipoBiometrico
    {
        /// <summary>
        /// Método que crea la entidad nomenclatura para equipos biométricos
        /// </summary>
        /// <param name="nomenclaturasRegistradas">Listado de nomenclaturas registrados, tanto activos
        /// como inactivos (usado para validación de duplicidad)</param>
        /// <param name="descripcionNomenclatura">Descripción de la nomenclatura</param>
        /// <returns>Nomenclatura creada</returns>
        public NomenclaturaEquipoBiometrico CrearNomenclatura(
            List<NomenclaturaEquipoBiometrico> nomenclaturasRegistradas,
            string descripcionNomenclatura)
        {
            ValidarDescripcionDeLaNomenclatura(nomenclaturasRegistradas, descripcionNomenclatura);
            return NomenclaturaEquipoBiometrico.CrearNomenclatura(descripcionNomenclatura);
        }
        /// <summary>
        /// Método que modifica la entidad nomenclatura para equipos biométricos
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura a modificar</param>
        /// <param name="nomenclaturasRegistradas">Listado de nomenclaturas registrados, tanto activos
        /// como inactivos (usado para validación de duplicidad)</param>
        /// <param name="descripcionNomenclatura">Descripción de la nomenclatura</param>
        /// <returns>Nomenclatura modificada</returns>
        public NomenclaturaEquipoBiometrico ModificarNomenclatura(
            NomenclaturaEquipoBiometrico nomenclatura,
            List<NomenclaturaEquipoBiometrico> nomenclaturasRegistradas,
            string descripcionNomenclatura)
        {
            nomenclaturasRegistradas = nomenclaturasRegistradas.Where(g => 
                g.CodigoNomenclatura != nomenclatura.CodigoNomenclatura).ToList();
            ValidarDescripcionDeLaNomenclatura(nomenclaturasRegistradas, descripcionNomenclatura);
            nomenclatura.ModificarNomenclatura(descripcionNomenclatura);
            return nomenclatura;
        }
        /// <summary>
        /// Método que valida que se haya seleccionado, por lo menos, una nomenclatura para ser
        /// inhabilitada o habilitada
        /// </summary>
        /// <param name="cantidadNomenclaturasSeleccionadas">Cantidad de nomenclaturas seleccionadas</param>
        public void ValidarNomenclaturasSeleccionadas(int cantidadNomenclaturasSeleccionadas)
        {
            if (cantidadNomenclaturasSeleccionadas == 0)
                throw new ExcepcionAplicacionPersonalizada("Debe seleccionar, al menos, una nomenclatura de " +
                    "la lista.");
        }
        /// <summary>
        /// Método que inhabilita la entidad nomenclatura para equipos biométricos
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura a inhabilitar</param>
        /// <returns>Nomenclatura inhabilitada</returns>
        public NomenclaturaEquipoBiometrico InhabilitarNomenclatura(
            NomenclaturaEquipoBiometrico nomenclatura)
        {
            nomenclatura.InhabilitarNomenclatura();
            return nomenclatura;
        }
        /// <summary>
        /// Método que habilita la entidad nomenclatura para equipos biométricos
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura a habilitar</param>
        /// <returns>Nomenclatura habilitada</returns>
        public NomenclaturaEquipoBiometrico HabilitarNomenclatura(
            NomenclaturaEquipoBiometrico nomenclatura)
        {
            nomenclatura.HabilitarNomenclatura();
            return nomenclatura;
        }
        /// <summary>
        /// Método que asigna el registro "Sin asignación" de nomenclatura
        /// a un equipo biométrico
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a modificar</param>
        /// <returns>Equipo biométrico modificado</returns>
        public EquipoBiometrico AsignarNomenclaturaSinAsignacionAEquipoBiometrico(
            EquipoBiometrico equipoBiometrico)
        {
            equipoBiometrico.AsignarNomenclaturaSinAsignacion();
            return equipoBiometrico;
        }
        #region Métodos privados
        /// <summary>
        /// Método que valida la descripción de la nomenclatura
        /// </summary>
        /// <param name="nomenclaturasRegistradas">Listado de nomenclaturas registrados, tanto activos
        /// como inactivos (usado para validación de duplicidad)</param>
        /// <param name="descripcionNomenclatura">Descripción de nomenclatura a validar</param>
        private void ValidarDescripcionDeLaNomenclatura(
            List<NomenclaturaEquipoBiometrico> nomenclaturasRegistradas, 
            string descripcionNomenclatura)
        {
            if (!descripcionNomenclatura.ValidarCantidadCaracteres(3, 3))
                throw new ExcepcionAplicacionPersonalizada("La nomenclatura debe tener 3 caracteres.");
            if (!descripcionNomenclatura.ValidarCadenaDeTextoSoloLetrasYSinEspacios())
                throw new ExcepcionAplicacionPersonalizada("La nomenclatura debe estar compuesta solo por letras.");
            if (nomenclaturasRegistradas.Where(g => 
                    g.DescripcionNomenclatura.Equals(descripcionNomenclatura.ToUpper())).Any())
                throw new ExcepcionAplicacionPersonalizada("La nomenclatura ingresada ya existe. Verifique el " +
                    "listado de nomenclaturas en estado activo o inactivo para encontrar la nomenclatura " +
                    "duplicada.");
        }
        #endregion
    }
}
