using Newtonsoft.Json.Linq;

namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz para el servicio de consultas y operaciones de equipos biométricos
    /// </summary>
    public interface IServicioEquipoBiometrico
    {
        /// <summary>
        /// Método que obtiene el listado de los equipos biométricos presentes en la red
        /// empresarial
        /// </summary>
        /// <returns>Listado de equipos biométricos encontrados en la red empresarial</returns>
        string ObtenerListadoEquiposBiometricosDeLaRedEmpresarial();
        /// <summary>
        /// Método que obtiene el listado de los equipos biométricos registrados (tanto
        /// activos como inactivos)
        /// </summary>
        /// <returns>Resultado encriptado con el listado de equipos biométricos 
        /// registrados encontrados</returns>
        string ObtenerListadoDeEquiposBiometricosRegistrados();
        /// <summary>
        /// Método que obtiene un equipo biométrico en base a su código de equipo biométrico
        /// </summary>
        /// <param name="codigoEquipoBiometricoEncriptado">Código del equipo biométrico a obtener 
        /// (encriptado)</param>
        /// <returns>Resultado encriptado con los datos del equipo biométrico encontrado</returns>
        string ObtenerEquipoBiometrico(string codigoquipoBiometricoEncriptado);
        /// <summary>
        /// Método que guarda un nuevo equipo biométrico
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del equipo biométrico 
        /// a guardar</param>
        /// <returns>Resultado encriptado con los datos del equipo biométrico guardado</returns>
        string GuardarEquipoBiometrico(JToken encriptado);
        /// <summary>
        /// Método que modifica un equipo biométrico
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del equipo biométrico 
        /// a modificar</param>
        /// <returns>Resultado encriptado con los datos del equipo biométrico modificado</returns>
        string ModificarEquipoBiometrico(JToken encriptado);
        /// <summary>
        /// Método que inhabilita a los equipos biométricos
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de los equipos biométricos a 
        /// inhabilitar</param>
        /// <returns>Resultado encriptado con el listado de equipos biométricos inhabilitados</returns>
        string InhabilitarEquiposBiometricos(JToken encriptado);
        /// <summary>
        /// Método que habilita a los equipos biométricos
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de los equipos biométricos a 
        /// habilitar</param>
        /// <returns>Resultado encriptado con el listado de equipos biométricos habilitados</returns>
        string HabilitarEquiposBiometricos(JToken encriptado);
        /// <summary>
        /// Método que se comunica con el equipo biométrico para abrir la puerta de acceso
        /// </summary>
        /// <param name="codigoEquipoBiometricoEncriptado">Código encriptado del equipo biométrico</param>
        /// <returns>Resultado encriptado con el éxito o fracaso de la operación</returns>
        string AbrirPuertaDeAccesoDelEquipoBiometrico(string codigoEquipoBiometricoEncriptado);
        /// <summary>
        /// Método que envía una señal el equipo biométrico (pitidos)
        /// </summary>
        /// <param name="codigoEquipoBiometricoEncriptado">Código encriptado del equipo biométrico</param>
        /// <returns>Resultado encriptado con el éxito o fracaso de la operación</returns>
        string EnviarSenalAEquipoBiometrico(string codigoEquipoBiometricoEncriptado);
    }
}
