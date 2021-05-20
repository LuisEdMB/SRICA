using System.Threading.Tasks;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.PE;
using Newtonsoft.Json.Linq;

namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz del servicio para envíos de alertas (envío de correos electrónicos)
    /// </summary>
    public interface IServicioAlerta
    {
        /// <summary>
        /// Método que envía la alerta (correo electrónico) al usuario para que pueda recuperar su contraseña
        /// olvidada
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el usuario a quien se le enviará la 
        /// alerta</param>
        /// <returns>Resultado encriptado con el resultado del proceso de envío de alerta</returns>
        string EnviarAlertaDeRecuperacionDeContrasenaOlvidada(JToken encriptado);
        /// <summary>
        /// Método que envía la alerta de acceso denegado a todos los usuarios del sistema
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa que realiza el proceso
        /// de reconocimiento</param>
        /// <param name="equipoBiometrico">Equipo biométrico origen</param>
        /// <param name="fotoPersonalNoRegistrado">Foto a adjuntar en la alerta</param>
        Task EnviarAlertaDeAccesosDenegados(PersonalEmpresa personalEmpresa, 
            EquipoBiometrico equipoBiometrico, string fotoPersonalNoRegistrado);
    }
}
