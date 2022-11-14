using API.SRICA.Dominio.Entidad.AR;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.SE;
using System.Collections.Generic;

namespace API.SRICA.Dominio.Servicio.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de dominio para los equipos biométricos
    /// </summary>
    public interface IServicioDominioEquipoBiometrico
    {
        /// <summary>
        /// Método que crea la entidad equipo biométrico
        /// </summary>
        /// <param name="equiposBiometricosRegistrados">Listado de equipos biométricos registrados
        /// para validación de duplicidad</param>
        /// <param name="nomenclatura">Nomenclatura del equipo biométrico</param>
        /// <param name="nombreEquipoBiometrico">Nombre del equipo biométrico</param>
        /// <param name="direccionRed">Dirección IP del equipo biométrico</param>
        /// <param name="direccionMAC">Dirección MAC del equipo biométrico (encriptado)</param>
        /// <returns>Equipo biométrico creado</returns>
        EquipoBiometrico CrearEquipoBiometrico(List<EquipoBiometrico> equiposBiometricosRegistrados,
            NomenclaturaEquipoBiometrico nomenclatura, string nombreEquipoBiometrico, string direccionRed,
            string direccionMAC);
        /// <summary>
        /// Método que modifica la entidad equipo biométrico
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a modificar</param>
        /// <param name="equiposBiometricosRegistrados">Listado de equipos biométricos registrados
        /// para validación de duplicidad</param>
        /// <param name="nomenclatura">Nomenclatura del equipo biométrico</param>
        /// <param name="nombreEquipoBiometrico">Nombre del equipo biométrico</param>
        /// <param name="direccionRed">Dirección IP del equipo biométrico</param>
        /// <param name="sede">Sede del área del equipo biométrico</param>
        /// <param name="area">Área del equipo biométrico</param>
        /// <returns>Equipo biométrico modificado</returns>
        EquipoBiometrico ModificarEquipoBiometrico(EquipoBiometrico equipoBiometrico,
            List<EquipoBiometrico> equiposBiometricosRegistrados, NomenclaturaEquipoBiometrico nomenclatura,
            string nombreEquipoBiometrico, string direccionRed, Sede sede, Area area);
        /// <summary>
        /// Método que modifica datos propios del equipo biométrico
        /// (IP, nombre de equipo)
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico que contiene los datos a utilizar</param>
        /// <param name="usuarioSSH">Usuario SSH para la conexión al equipo biométrico</param>
        /// <param name="contrasenaUsuarioSSH">Contraseña del usuario SSH para la conexión al
        /// equipo biométrico</param>
        void ModificarDatosPropiosDelEquipoBiometrico(EquipoBiometrico equipoBiometrico,
            string usuarioSSH, string contrasenaUsuarioSSH);
        /// <summary>
        /// Método que se comunica con el equipo biométrico para abrir la puerta de acceso
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico que contiene los datos a utilizar
        /// para el proceso de apertura</param>
        /// <param name="usuarioSSH">Usuario SSH para la conexión al equipo biométrico</param>
        /// <param name="contrasenaUsuarioSSH">Contraseña del usuario SSH para la conexión al
        /// equipo biométrico</param>
        void AbrirPuertaDeAccesoDelEquipoBiometrico(EquipoBiometrico equipoBiometrico,
            string usuarioSSH, string contrasenaUsuarioSSH);
        /// <summary>
        /// Método que envía una señal al equipo biométrico (pitidos)
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico que contiene los datos a utilizar
        /// para el proceso de envío de señal</param>
        /// <param name="usuarioSSH">Usuario SSH para la conexión al equipo biométrico</param>
        /// <param name="contrasenaUsuarioSSH">Contraseña del usuario SSH para la conexión al
        /// equipo biométrico</param>
        void EnviarSenalAEquipoBiometrico(EquipoBiometrico equipoBiometrico,
            string usuarioSSH, string contrasenaUsuarioSSH);
        /// <summary>
        /// Método que valida que se haya seleccionado, por lo menos, un equipo biométrico para ser
        /// inhabilitado o habilitado
        /// </summary>
        /// <param name="cantidadEquiposBiometricosSeleccionados">Cantidad de equipos biométricos 
        /// seleccionados</param>
        void ValidarEquiposBiometricosSeleccionados(int cantidadEquiposBiometricosSeleccionados);
        /// <summary>
        /// Método que inhabilita la entidad equipo biométrico
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a inhabilitar</param>
        /// <returns>Equipo biométrico inhabilitado</returns>
        EquipoBiometrico InhabilitarEquipoBiometrico(EquipoBiometrico equipoBiometrico);
        /// <summary>
        /// Método que habilita la entidad equipo biométrico
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a habilitar</param>
        /// <returns>Equipo biométrico habilitado</returns>
        EquipoBiometrico HabilitarEquipoBiometrico(EquipoBiometrico equipoBiometrico);
        /// <summary>
        /// Método que valida el equipo biométrico para reconocimiento de personal
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a validar</param>
        /// <param name="direccionMac">Dirección MAC del equipo biométrico a validar</param>
        void ValidarEquipoBiometricoParaReconocimientoDePersonal(EquipoBiometrico equipoBiometrico,
            string direccionMac);
    }
}
