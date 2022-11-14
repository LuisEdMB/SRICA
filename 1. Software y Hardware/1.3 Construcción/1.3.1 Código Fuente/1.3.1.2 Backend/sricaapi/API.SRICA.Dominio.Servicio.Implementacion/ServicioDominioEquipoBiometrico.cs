using System;
using API.SRICA.Dominio.Entidad.AR;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.SE;
using API.SRICA.Dominio.Excepcion;
using API.SRICA.Dominio.Servicio.Interfaz;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Renci.SshNet;

namespace API.SRICA.Dominio.Servicio.Implementacion
{
    /// <summary>
    /// Implementación del servicio de dominio para los equipos biométricos
    /// </summary>
    public class ServicioDominioEquipoBiometrico : IServicioDominioEquipoBiometrico
    {
        /// <summary>
        /// Servicio de ping de hosts
        /// </summary>
        private readonly IServicioPingHost _servicioPingHost;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioPingHost">Servicio de ping de hosts</param>
        public ServicioDominioEquipoBiometrico(IServicioPingHost servicioPingHost)
        {
            _servicioPingHost = servicioPingHost;
        }
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
        public EquipoBiometrico CrearEquipoBiometrico(List<EquipoBiometrico> equiposBiometricosRegistrados,
            NomenclaturaEquipoBiometrico nomenclatura, string nombreEquipoBiometrico, string direccionRed,
            string direccionMAC)
        {
            ValidarDireccionMacExistente(equiposBiometricosRegistrados, direccionMAC);
            return EquipoBiometrico.CrearEquipoBiometrico(nomenclatura, nombreEquipoBiometrico,
                direccionRed, direccionMAC);
        }
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
        public EquipoBiometrico ModificarEquipoBiometrico(EquipoBiometrico equipoBiometrico,
            List<EquipoBiometrico> equiposBiometricosRegistrados, NomenclaturaEquipoBiometrico nomenclatura,
            string nombreEquipoBiometrico, string direccionRed, Sede sede, Area area)
        {
            var equiposBiometricosSinElActual = equiposBiometricosRegistrados.Where(g =>
                g.CodigoEquipoBiometrico != equipoBiometrico.CodigoEquipoBiometrico).ToList();
            ValidarNomenclatura(nomenclatura);
            ValidarNombreEquipoBiometrico(equiposBiometricosSinElActual, nombreEquipoBiometrico, 
                nomenclatura);
            ValidarDireccionRed(equiposBiometricosSinElActual, direccionRed);
            ValidarSede(sede);
            ValidarArea(area);
            equipoBiometrico.ModificarEquipoBiometrico(nomenclatura, nombreEquipoBiometrico, 
                direccionRed, area);
            return equipoBiometrico;
        }
        /// <summary>
        /// Método que modifica datos propios del equipo biométrico
        /// (IP, nombre de equipo)
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico que contiene los datos a utilizar</param>
        /// <param name="usuarioSSH">Usuario SSH para la conexión al equipo biométrico</param>
        /// <param name="contrasenaUsuarioSSH">Contraseña del usuario SSH para la conexión al
        /// equipo biométrico</param>
        public void ModificarDatosPropiosDelEquipoBiometrico(EquipoBiometrico equipoBiometrico,
            string usuarioSSH, string contrasenaUsuarioSSH)
        {
            try
            {
                using var cliente = new SshClient(equipoBiometrico.DireccionRedEquipoBiometricoAnterior,
                    usuarioSSH, contrasenaUsuarioSSH);
                if (!cliente.IsConnected) cliente.Connect();
                
                if (equipoBiometrico.EsDireccionRedEquipoBiometricoCambiado)
                {
                    VerificarExistenciaDeDireccionDeRedEnLaRed(equipoBiometrico.DireccionRedEquipoBiometrico);
                    EjecutarComandoEnEquipoBiometrico(cliente,
                        $"echo -e '{contrasenaUsuarioSSH}' | sudo bash " +
                        $"/home/{usuarioSSH}/ModificarDireccionRed.sh " +
                        $"{equipoBiometrico.DireccionRedEquipoBiometrico} " +
                        $"{equipoBiometrico.DireccionRedEquipoBiometrico.ObtenerPuertaEnlace()}");
                }

                if (equipoBiometrico.EsNombreEquipoBiometricoCambiado)
                {
                    VerificarExistenciaDeNombreDeEquipoEnLaRed(equipoBiometrico.NombreEquipoBiometrico);
                    EjecutarComandoEnEquipoBiometrico(cliente,
                        $"echo -e '{contrasenaUsuarioSSH}' | sudo bash " +
                        $"/home/{usuarioSSH}/ModificarNombreEquipo.sh " +
                        $"{equipoBiometrico.NombreEquipoBiometrico}");
                }

                if (equipoBiometrico.EsNombreEquipoBiometricoCambiado
                    || equipoBiometrico.EsDireccionRedEquipoBiometricoCambiado)
                {
                    EjecutarComandoEnEquipoBiometrico(cliente,
                        $"echo -e '{contrasenaUsuarioSSH}' | sudo reboot", true);
                }
            }
            catch (ExcepcionAplicacionPersonalizada)
            {
                throw;
            }
            catch (Exception excepcion)
            {
                throw new ExcepcionPersonalizada("No se pudo conectar con el equipo biométrico " +
                    "para el cambio de sus propiedades. Verifique la existencia y comunicación " +
                    "con el equipo biométrico.",
                    ExcepcionPersonalizada.CodigoExcepcionPersonalizado.ErrorServicio, excepcion);
            }
        }
        /// <summary>
        /// Método que se comunica con el equipo biométrico para abrir la puerta de acceso
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico que contiene los datos a utilizar
        /// para el proceso de apertura</param>
        /// <param name="usuarioSSH">Usuario SSH para la conexión al equipo biométrico</param>
        /// <param name="contrasenaUsuarioSSH">Contraseña del usuario SSH para la conexión al
        /// equipo biométrico</param>
        public void AbrirPuertaDeAccesoDelEquipoBiometrico(EquipoBiometrico equipoBiometrico,
            string usuarioSSH, string contrasenaUsuarioSSH)
        {
            if (equipoBiometrico.EsEquipoBiometricoInactivo) throw new ExcepcionAplicacionPersonalizada(
                "El equipo biométrico se encuentra en estado INACTIVO.");
            try
            {
                using var cliente = new SshClient(equipoBiometrico.DireccionRedEquipoBiometrico,
                    usuarioSSH, contrasenaUsuarioSSH);
                if (!cliente.IsConnected) cliente.Connect();
                EjecutarComandoEnEquipoBiometrico(cliente,
                    $"echo -e '{contrasenaUsuarioSSH}' | sudo python3 " +
                    "/home/pi/ControlarComponentes.py --modo 0");
                Thread.Sleep(3000);
                EjecutarComandoEnEquipoBiometrico(cliente,
                    $"echo -e '{contrasenaUsuarioSSH}' | sudo python3 " +
                    "/home/pi/ControlarComponentes.py --modo 1");
            }
            catch (Exception excepcion)
            {
                throw new ExcepcionPersonalizada("No se pudo conectar al equipo biométrico " +
                    "para su manipulación. Verifique la existencia y comunicación con el " +
                    "equipo biométrico.", ExcepcionPersonalizada.CodigoExcepcionPersonalizado.ErrorServicio, 
                    excepcion);
            }
        }
        /// <summary>
        /// Método que envía una señal al equipo biométrico (pitidos)
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico que contiene los datos a utilizar
        /// para el proceso de envío de señal</param>
        /// <param name="usuarioSSH">Usuario SSH para la conexión al equipo biométrico</param>
        /// <param name="contrasenaUsuarioSSH">Contraseña del usuario SSH para la conexión al
        /// equipo biométrico</param>
        public void EnviarSenalAEquipoBiometrico(EquipoBiometrico equipoBiometrico,
            string usuarioSSH, string contrasenaUsuarioSSH)
        {
            if (equipoBiometrico.EsEquipoBiometricoInactivo) throw new ExcepcionAplicacionPersonalizada(
                "El equipo biométrico se encuentra en estado INACTIVO.");
            try
            {
                using var cliente = new SshClient(equipoBiometrico.DireccionRedEquipoBiometrico,
                    usuarioSSH, contrasenaUsuarioSSH);
                if (!cliente.IsConnected) cliente.Connect();
                for (int i = 1; i <= 6; i++)
                {
                    EjecutarComandoEnEquipoBiometrico(cliente,
                        $"echo -e '{contrasenaUsuarioSSH}' | sudo python3 " +
                        "/home/pi/ControlarComponentes.py --modo 8 --archivo /home/pi/beep.mp3");
                }
            }
            catch (Exception excepcion)
            {
                throw new ExcepcionPersonalizada("No se pudo conectar al equipo biométrico " +
                    "para su manipulación. Verifique la existencia y comunicación con el " +
                    "equipo biométrico.", ExcepcionPersonalizada.CodigoExcepcionPersonalizado.ErrorServicio, 
                    excepcion);
            }
        }
        /// <summary>
        /// Método que valida que se haya seleccionado, por lo menos, un equipo biométrico para ser
        /// inhabilitado o habilitado
        /// </summary>
        /// <param name="cantidadEquiposBiometricosSeleccionados">Cantidad de equipos biométricos 
        /// seleccionados</param>
        public void ValidarEquiposBiometricosSeleccionados(int cantidadEquiposBiometricosSeleccionados)
        {
            if (cantidadEquiposBiometricosSeleccionados == 0)
                throw new ExcepcionAplicacionPersonalizada("Debe seleccionar, al menos, un equipo " +
                    "biométrico de la lista.");
        }
        /// <summary>
        /// Método que inhabilita la entidad equipo biométrico
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a inhabilitar</param>
        /// <returns>Equipo biométrico inhabilitado</returns>
        public EquipoBiometrico InhabilitarEquipoBiometrico(EquipoBiometrico equipoBiometrico)
        {
            equipoBiometrico.InhabilitarEquipoBiometrico();
            return equipoBiometrico;
        }
        /// <summary>
        /// Método que habilita la entidad equipo biométrico
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a habilitar</param>
        /// <returns>Equipo biométrico habilitado</returns>
        public EquipoBiometrico HabilitarEquipoBiometrico(EquipoBiometrico equipoBiometrico)
        {
            equipoBiometrico.HabilitarEquipoBiometrico();
            return equipoBiometrico;
        }
        /// <summary>
        /// Método que valida el equipo biométrico para reconocimiento de personal
        /// </summary>
        /// <param name="equipoBiometrico">Equipo biométrico a validar</param>
        /// <param name="direccionMac">Dirección MAC del equipo biométrico a validar</param>
        public void ValidarEquipoBiometricoParaReconocimientoDePersonal(EquipoBiometrico equipoBiometrico,
            string direccionMac)
        {
            if (equipoBiometrico == null)
                throw new ExcepcionAplicacionEquipoBiometricoPersonalizada("El equipo biométrico " +
                    $"no está registrado: MAC {direccionMac}.", 
                    ExcepcionAplicacionEquipoBiometricoPersonalizada
                        .CodigoExcepcionPersonalizado.DenegadoSinFoto);
            if (!equipoBiometrico.IndicadorEstado)
                throw new ExcepcionAplicacionEquipoBiometricoPersonalizada("El equipo biométrico no " +
                    "se encuentra activo.",
                    ExcepcionAplicacionEquipoBiometricoPersonalizada
                        .CodigoExcepcionPersonalizado.DenegadoSinFoto);
            if (!equipoBiometrico.Area.Sede.IndicadorEstado || 
                equipoBiometrico.Area.Sede.IndicadorRegistroParaSinAsignacion)
                throw new ExcepcionAplicacionEquipoBiometricoPersonalizada("La sede del equipo " +
                    "biométrico no se encuentra activa.",
                    ExcepcionAplicacionEquipoBiometricoPersonalizada
                        .CodigoExcepcionPersonalizado.DenegadoSinFoto);
            if (!equipoBiometrico.Area.IndicadorEstado ||
                equipoBiometrico.Area.IndicadorRegistroParaSinAsignacion)
                throw new ExcepcionAplicacionEquipoBiometricoPersonalizada("El área del equipo " +
                    "biométrico no se encuentra activa.",
                    ExcepcionAplicacionEquipoBiometricoPersonalizada
                        .CodigoExcepcionPersonalizado.DenegadoSinFoto);
        }
        #region Métodos privados
        /// <summary>
        /// Método que valida que solo exista un equipo biométrico registrado (validación mediante
        /// la dirección MAC)
        /// </summary>
        /// <param name="equiposBiometricosRegistrados">Listado de equipos biométricos registrados</param>
        /// <param name="direccionMAC">Dirección MAC a validar (encriptado)</param>
        private void ValidarDireccionMacExistente(List<EquipoBiometrico> equiposBiometricosRegistrados,
            string direccionMAC)
        {
            if (equiposBiometricosRegistrados.Any(g =>
                g.DireccionFisicaEquipoBiometrico.Equals(direccionMAC)))
                throw new ExcepcionAplicacionPersonalizada("El equipo biométrico ya se encuentra " +
                    "registrado. Verifique el listado de equipos biométricos registrados en estado " +
                    "activo o inactivo para encontrar el equipo biométrico duplicado.");
        }
        /// <summary>
        /// Método que valida la nomenclatura del equipo biométrico
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura a validar</param>
        private void ValidarNomenclatura(NomenclaturaEquipoBiometrico nomenclatura)
        {
            if (nomenclatura.EsNomenclaturaInactivo)
                throw new ExcepcionAplicacionPersonalizada("La nomenclatura seleccionada \"" +
                    nomenclatura.DescripcionNomenclatura + "\" se encuentra en estado INACTIVO.");
        }
        /// <summary>
        /// Método que valida el nombre del equipo biométrico
        /// </summary>
        /// <param name="equiposBiometricosRegistrados">Listado de equipos biométricos registrados
        /// (sin considerar el equipo biométrico a modificar)</param>
        /// <param name="nombreEquipoBiometrico">Nombre del equipo biométrico a validar</param>
        /// <param name="nomenclatura">Nomenclatura asignada al equipo biométrico</param>
        private void ValidarNombreEquipoBiometrico(List<EquipoBiometrico> equiposBiometricosRegistrados, 
            string nombreEquipoBiometrico, NomenclaturaEquipoBiometrico nomenclatura)
        {
            if (!nombreEquipoBiometrico.ValidarCantidadCaracteres(1, 11))
                throw new ExcepcionAplicacionPersonalizada("La cantidad máxima de caracteres para el " +
                    "nombre de equipo es de 11 caracteres.");
            if (!nombreEquipoBiometrico.ValidarCadenaDeTextoSoloLetrasNumerosYOGuiones())
                throw new ExcepcionAplicacionPersonalizada("El nombre de equipo debe estar compuesto por " +
                    "caracteres alfanuméricos y guiones (opcional).");
            if (equiposBiometricosRegistrados.Any(g =>
                g.NombreEquipoBiometrico.ToUpper().Equals(nomenclatura.DescripcionNomenclatura + "-" + 
                    nombreEquipoBiometrico.ToUpper())))
                throw new ExcepcionAplicacionPersonalizada("El nombre de equipo ingresado ya existe. " +
                    "Verifique la red empresarial o el listado de equipos biométricos registrados en " +
                    "estado activo o inactivo para encontrar el nombre de equipo duplicado.");
        }
        /// <summary>
        /// Método que valida la dirección de red del equipo biométrico
        /// </summary>
        /// <param name="equiposBiometricosRegistrados">Listado de equipos biométricos registrados
        /// (sin considerar el equipo biométrico a modificar)</param>
        /// <param name="direccionRed">Dirección de red a validar</param>
        private void ValidarDireccionRed(List<EquipoBiometrico> equiposBiometricosRegistrados,
            string direccionRed)
        {
            if (!direccionRed.ValidarCantidadCaracteres(1, 15))
                throw new ExcepcionAplicacionPersonalizada("La cantidad máxima de caracteres para " +
                    "la dirección de red es de 15 caracteres.");
            if (!direccionRed.ValidarCadenaTextoConFormatoIP())
                throw new ExcepcionAplicacionPersonalizada("La dirección de red debe tener un " +
                    "formato válido para IP.");
            if (equiposBiometricosRegistrados.Any(g =>
                    g.DireccionRedEquipoBiometrico.Equals(direccionRed)))
                throw new ExcepcionAplicacionPersonalizada("La dirección de red ingresada ya existe. " +
                    "Verifique la red empresarial o el listado de equipos biométricos registrados en " +
                    "estado activo o inactivo para encontrar la dirección de red duplicada.");
        }
        /// <summary>
        /// Método que valida la sede del área del equipo biométrico
        /// </summary>
        /// <param name="sede">Sede a validar</param>
        private void ValidarSede(Sede sede)
        {
            if (sede.EsSedeInactivo)
                throw new ExcepcionAplicacionPersonalizada("La sede seleccionada \"" +
                    sede.DescripcionSede + "\" se encuentra en estado INACTIVO.");
        }
        /// <summary>
        /// Método que valida el área del equipo biométrico
        /// </summary>
        /// <param name="area">Área a validar</param>
        private void ValidarArea(Area area)
        {
            if (area.EsAreaInactivo)
                throw new ExcepcionAplicacionPersonalizada("El área seleccionada \"" +
                    area.DescripcionArea + "\" se encuentra en estado INACTIVO.");
        }
        /// <summary>
        /// Método que ejecuta un comando en el equipo biométrico
        /// </summary>
        /// <param name="cliente">Cliente SSH conectado al equipo biométrico</param>
        /// <param name="comando">Comando a ejecutar</param>
        /// <param name="comandoReiniciar">Si el comando a ejecutar va a reiniciar el
        /// equipo biométrico</param>
        /// <exception cref="Exception">Excepción lanzada cuando exista algún error</exception>
        private void EjecutarComandoEnEquipoBiometrico(SshClient cliente, string comando, 
            bool comandoReiniciar = false)
        {
            if (!comandoReiniciar)
            {
                var comandoPi = cliente.CreateCommand(comando);
                comandoPi.CommandTimeout = TimeSpan.FromSeconds(20);
                comandoPi.Execute();
                if (comandoPi.ExitStatus != 0)
                    throw new Exception("Hubo un problema al procesar los cambios " +
                                        $"en el equipo biométrico: {comandoPi.Error}");
                comandoPi.Dispose();
            }
            else
            {
                try
                {
                    var comandoPi = cliente.CreateCommand(comando);
                    comandoPi.CommandTimeout = TimeSpan.FromSeconds(20);
                    comandoPi.Execute();
                    comandoPi.Dispose();
                }
                catch
                {
                    // ignored
                }
            }
        }
        /// <summary>
        /// Método que verifica, en la red, una dirección de red existente
        /// </summary>
        /// <param name="direccionRed">Dirección de red a verificar</param>
        private void VerificarExistenciaDeDireccionDeRedEnLaRed(string direccionRed)
        {
            var resultado = _servicioPingHost.PingHost(direccionRed).Result;
            if (!string.IsNullOrEmpty(resultado["host"]))
                throw new ExcepcionAplicacionPersonalizada("La dirección de red ingresada ya " +
                    "existe. Verifique la red empresarial o el listado de equipos biométricos " +
                    "registrados en estado activo o inactivo para encontrar la " +
                    "dirección de red duplicada.");
        }
        /// <summary>
        /// Método que verifica, en la red, un nombre de equipo existente 
        /// </summary>
        /// <param name="nombreEquipo">Nombre de equipo a verificar</param>
        private void VerificarExistenciaDeNombreDeEquipoEnLaRed(string nombreEquipo)
        {
            var resultado = _servicioPingHost.PingHost(nombreEquipo, true).Result;
            if (!string.IsNullOrEmpty(resultado["host"]))
                throw new ExcepcionAplicacionPersonalizada("El nombre de equipo ingresado ya existe. " +
                    "Verifique la red empresarial o el listado de equipos biométricos registrados en " +
                    "estado activo o inactivo para encontrar el nombre de equipo duplicado.");
        }
        #endregion
    }
}
