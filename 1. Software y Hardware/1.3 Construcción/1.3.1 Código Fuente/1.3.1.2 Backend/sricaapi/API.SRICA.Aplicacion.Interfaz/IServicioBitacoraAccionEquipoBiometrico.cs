
using System.Threading.Tasks;

namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de consultas y operaciones de bitácoras de acciones del sistema
    /// </summary>
    public interface IServicioBitacoraAccionEquipoBiometrico
    {
        /// <summary>
        /// Método que obtiene el listado de bitácora de acciones de equipos biométricos
        /// </summary>
        /// <returns>Resultado encriptado con el listado de bitácora de acciones de equipos biométricos
        /// encontrados</returns>
        string ObtenerListadoDeBitacoraDeAccionesDeEquiposBiometricos();
        /// <summary>
        /// Método que guarda la bitácora de la acción de un equipo biométrico
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa quien realiza la acción
        /// mediante el equipo biométrico</param>
        /// <param name="equipoBiometrico">Equipo biométrico origen</param>
        /// <param name="accesoDenegadoConFoto">Si la bitácora es producida por un acceso denegado,
        /// donde el personal no está registrado en el sistema</param>
        /// <param name="accesoDenegadoSinFoto">Si la bitácora es producida por un acceso denegado,
        /// donde el personal está registrado en el sistema</param>
        /// <param name="mensajeAccion">Mensaje de la acción realizada</param>
        /// <param name="fotoPersonalNoRegistrado">Foto del personal no registrado en
        /// el sistema</param>
        void GuardarBitacoraDeAccionDelEquipoBiometrico(object personalEmpresa, 
            object equipoBiometrico, bool accesoDenegadoConFoto, bool accesoDenegadoSinFoto,
            string mensajeAccion, string fotoPersonalNoRegistrado);
    }
}
