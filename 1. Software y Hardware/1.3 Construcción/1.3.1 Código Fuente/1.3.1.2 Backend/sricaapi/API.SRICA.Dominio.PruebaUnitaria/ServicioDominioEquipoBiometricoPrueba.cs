using System.Collections.Generic;
using System.Linq;
using API.SRICA.Dominio.Entidad.AR;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.SE;
using API.SRICA.Dominio.Excepcion;
using API.SRICA.Dominio.Servicio.Implementacion;
using API.SRICA.Dominio.Servicio.Interfaz;
using NUnit.Framework;

namespace API.SRICA.Dominio.PruebaUnitaria
{
    /// <summary>
    /// Clase de prueba del servicio de dominio de equipos biométricos
    /// </summary>
    public class ServicioDominioEquipoBiometricoPrueba
    {
        /// <summary>
        /// Servicio de dominio de equipos biométricos
        /// </summary>
        private IServicioDominioEquipoBiometrico _servicioDominioEquipoBiometrico;
        /// <summary>
        /// Lista fake de equipos biométricos
        /// </summary>
        private IList<EquipoBiometrico> _equiposBiometricos;
        /// <summary>
        /// Nomenclatura de equipos biométrico fake en estado activo
        /// </summary>
        private NomenclaturaEquipoBiometrico _nomenclaturaActiva;
        /// <summary>
        /// Nomenclatura de equipos biométrico fake en estado inactivo
        /// </summary>
        private NomenclaturaEquipoBiometrico _nomenclaturaInactiva;
        /// <summary>
        /// Sede fake en estado activo
        /// </summary>
        private Sede _sedeActiva;
        /// <summary>
        /// Sede fake en estado inactivo
        /// </summary>
        private Sede _sedeInactiva;
        /// <summary>
        /// Área fake en estado activo
        /// </summary>
        private Area _areaActiva;
        /// <summary>
        /// Área fake en estado inactivo
        /// </summary>
        private Area _areaInactiva;
        /// <summary>
        /// Equipo biométrico fake
        /// </summary>
        private EquipoBiometrico _equipoBiometrico;
        /// <summary>
        /// Método que inicializa las pruebas
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _servicioDominioEquipoBiometrico = new ServicioDominioEquipoBiometrico(null);

            var equipoBiometricoType = typeof(EquipoBiometrico);
            var equipoBiometrico = new EquipoBiometrico();
            _equiposBiometricos = new List<EquipoBiometrico>();
            equipoBiometricoType.GetProperty("CodigoEquipoBiometrico").SetValue(equipoBiometrico, 1);
            equipoBiometricoType.GetProperty("NombreEquipoBiometrico").SetValue(equipoBiometrico, 
                "SRI-EQUI02");
            equipoBiometricoType.GetProperty("DireccionRedEquipoBiometrico").SetValue(equipoBiometrico, 
                "192.168.1.1");
            equipoBiometricoType.GetProperty("DireccionFisicaEquipoBiometrico").SetValue(equipoBiometrico, 
                "12345");
            equipoBiometricoType.GetProperty("IndicadorEstado").SetValue(equipoBiometrico, true);
            _equiposBiometricos.Add(equipoBiometrico);
            equipoBiometrico = new EquipoBiometrico();
            equipoBiometricoType.GetProperty("CodigoEquipoBiometrico").SetValue(equipoBiometrico, 2);
            equipoBiometricoType.GetProperty("NombreEquipoBiometrico").SetValue(equipoBiometrico, 
                "SRI-EQUI03");
            equipoBiometricoType.GetProperty("DireccionRedEquipoBiometrico").SetValue(equipoBiometrico, 
                "192.168.1.2");
            equipoBiometricoType.GetProperty("DireccionFisicaEquipoBiometrico").SetValue(equipoBiometrico, 
                "12346");
            _equiposBiometricos.Add(equipoBiometrico);
            equipoBiometrico = new EquipoBiometrico();
            equipoBiometricoType.GetProperty("CodigoEquipoBiometrico").SetValue(equipoBiometrico, 3);
            equipoBiometricoType.GetProperty("NombreEquipoBiometrico").SetValue(equipoBiometrico, 
                "SRI-EQUI04");
            equipoBiometricoType.GetProperty("DireccionRedEquipoBiometrico").SetValue(equipoBiometrico, 
                "192.168.1.3");
            equipoBiometricoType.GetProperty("DireccionFisicaEquipoBiometrico").SetValue(equipoBiometrico, 
                "12347");
            _equiposBiometricos.Add(equipoBiometrico);
            
            _nomenclaturaActiva = new NomenclaturaEquipoBiometrico();
            _nomenclaturaInactiva = new NomenclaturaEquipoBiometrico();
            var nomenclaturaType = typeof(NomenclaturaEquipoBiometrico);
            nomenclaturaType.GetProperty("DescripcionNomenclatura").SetValue(_nomenclaturaActiva, 
                "SRI");
            nomenclaturaType.GetProperty("IndicadorEstado").SetValue(_nomenclaturaActiva, true);
            nomenclaturaType.GetProperty("DescripcionNomenclatura").SetValue(_nomenclaturaInactiva, 
                "SRU");
            nomenclaturaType.GetProperty("IndicadorEstado").SetValue(_nomenclaturaInactiva, false);
            
            _sedeActiva = new Sede();
            _sedeInactiva = new Sede();
            var sedeType = typeof(Sede);
            sedeType.GetProperty("IndicadorEstado").SetValue(_sedeActiva, true);
            sedeType.GetProperty("DescripcionSede").SetValue(_sedeInactiva, "Sede1");
            sedeType.GetProperty("IndicadorEstado").SetValue(_sedeInactiva, false);
            
            _areaActiva = new Area();
            _areaInactiva = new Area();
            var areaType = typeof(Area);
            areaType.GetProperty("IndicadorEstado").SetValue(_areaActiva, true);
            areaType.GetProperty("Sede").SetValue(_areaActiva, _sedeActiva);
            areaType.GetProperty("DescripcionArea").SetValue(_areaInactiva, "Area1");
            areaType.GetProperty("IndicadorEstado").SetValue(_areaInactiva, false);

            _equipoBiometrico = new EquipoBiometrico();
            equipoBiometricoType.GetProperty("IndicadorEstado").SetValue(_equipoBiometrico, true);
            equipoBiometricoType.GetProperty("Area").SetValue(_equipoBiometrico, _areaActiva);
        }
        /// <summary>
        /// Método de prueba que crea la entidad equipo biométrico, de forma correcta
        /// </summary>
        [Test]
        public void PruebaCrearEquipoBiometricoCorrecto()
        {
            var equipoBiometrico = _servicioDominioEquipoBiometrico.CrearEquipoBiometrico(
                _equiposBiometricos.ToList(), _nomenclaturaActiva, "EQUI-01", "192.168.0.1", "12344");
            Assert.IsNotNull(equipoBiometrico);
            Assert.AreEqual(true, equipoBiometrico.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que crea la entidad equipo biométrico, donde la MAC del equipo
        /// biométrico a registrar ya se encuentra registrada en el sistema
        /// </summary>
        [Test]
        public void PruebaCrearEquipoBiometricoConMacExistente()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.CrearEquipoBiometrico(_equiposBiometricos.ToList(),
                    _nomenclaturaActiva, "EQUI-01", "192.168.0.1", "12345")).Message;
            Assert.AreEqual("El equipo biométrico ya se encuentra " +
                "registrado. Verifique el listado de equipos biométricos registrados en estado " +
                "activo o inactivo para encontrar el equipo biométrico duplicado.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad equipo biométrico, de forma correcta
        /// </summary>
        [Test]
        public void PruebaModificarEquipoBiometricoCorrecto()
        {
            var equipoBiometrico = _servicioDominioEquipoBiometrico.ModificarEquipoBiometrico(
                new EquipoBiometrico(), _equiposBiometricos.ToList(), _nomenclaturaActiva,
                "SREQ", "192.168.6.9", _sedeActiva, _areaActiva);
            Assert.IsNotNull(equipoBiometrico);
            Assert.AreEqual("192.168.6.9", equipoBiometrico.DireccionRedEquipoBiometrico);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad equipo biométrico, donde la nomenclatura
        /// se encuentra inactiva
        /// </summary>
        [Test]
        public void PruebaModificarEquipoBiometricoConNomenclaturaInactiva()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ModificarEquipoBiometrico(new EquipoBiometrico(),
                    _equiposBiometricos.ToList(), _nomenclaturaInactiva, "SREQ", "192.168.6.9",
                    _sedeActiva, _areaActiva)).Message;
            var mensajeEsperado = "La nomenclatura seleccionada \"{0}\" se encuentra en estado INACTIVO."
                .Replace("{0}", _nomenclaturaInactiva.DescripcionNomenclatura);
            Assert.AreEqual(mensajeEsperado, mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad equipo biométrico, donde el nombre de equipo
        /// está vacío
        /// </summary>
        [Test]
        public void PruebaModificarEquipoBiometricoConNombreDeEquipoVacio()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ModificarEquipoBiometrico(new EquipoBiometrico(),
                    _equiposBiometricos.ToList(), _nomenclaturaActiva, string.Empty, 
                    "192.168.6.9", _sedeActiva, _areaActiva)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el " +
                "nombre de equipo es de 11 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad equipo biométrico, donde el nombre de equipo
        /// contiene más de 11 caracteres 
        /// </summary>
        [Test]
        public void PruebaModificarEquipoBiometricoConNombreDeEquipoMayorOnceCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ModificarEquipoBiometrico(new EquipoBiometrico(),
                    _equiposBiometricos.ToList(), _nomenclaturaActiva, "asasdadasdadasdwasdwa sdwa", 
                    "192.168.6.9", _sedeActiva, _areaActiva)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el " +
                "nombre de equipo es de 11 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad equipo biométrico, donde el nombre de equipo
        /// no contiene solo alfanumérico y/o guiones
        /// </summary>
        [Test]
        public void PruebaModificarEquipoBiometricoConNombreDeEquipoNoAlfanumericoYOGuiones()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ModificarEquipoBiometrico(new EquipoBiometrico(),
                    _equiposBiometricos.ToList(), _nomenclaturaActiva, "sdw1323_", 
                    "192.168.6.9", _sedeActiva, _areaActiva)).Message;
            Assert.AreEqual("El nombre de equipo debe estar compuesto por " +
                "caracteres alfanuméricos y guiones (opcional).", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad equipo biométrico, donde el nombre de equipo
        /// ya existe
        /// </summary>
        [Test]
        public void PruebaModificarEquipoBiometricoConNombreDeEquipoExistente()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ModificarEquipoBiometrico(new EquipoBiometrico(),
                    _equiposBiometricos.ToList(), _nomenclaturaActiva, "EQUI02", 
                    "192.168.6.9", _sedeActiva, _areaActiva)).Message;
            Assert.AreEqual("El nombre de equipo ingresado ya existe. " +
                "Verifique la red empresarial o el listado de equipos biométricos registrados en " +
                "estado activo o inactivo para encontrar el nombre de equipo duplicado.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad equipo biométrico, con dirección de red vacía
        /// </summary>
        [Test]
        public void PruebaModificarEquipoBiometricoConDireccionRedVacia()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ModificarEquipoBiometrico(new EquipoBiometrico(),
                    _equiposBiometricos.ToList(), _nomenclaturaActiva, "EQUBIO", 
                    string.Empty, _sedeActiva, _areaActiva)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para " +
                "la dirección de red es de 15 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad equipo biométrico, con dirección de red
        /// mayor a 15 caracteres
        /// </summary>
        [Test]
        public void PruebaModificarEquipoBiometricoConDireccionRedMayorQuinceCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ModificarEquipoBiometrico(new EquipoBiometrico(),
                    _equiposBiometricos.ToList(), _nomenclaturaActiva, "EQUBIO", 
                    "255.255.255.2551", _sedeActiva, _areaActiva)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para " +
                "la dirección de red es de 15 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad equipo biométrico, con dirección de red
        /// sin formato IP
        /// </summary>
        [Test]
        public void PruebaModificarEquipoBiometricoConDireccionRedSinFormatoIp()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ModificarEquipoBiometrico(new EquipoBiometrico(),
                    _equiposBiometricos.ToList(), _nomenclaturaActiva, "EQUBIO", 
                    "2356.885", _sedeActiva, _areaActiva)).Message;
            Assert.AreEqual("La dirección de red debe tener un " +
                "formato válido para IP.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad equipo biométrico, con dirección de red
        /// existente
        /// </summary>
        [Test]
        public void PruebaModificarEquipoBiometricoConDireccionRedExistente()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ModificarEquipoBiometrico(new EquipoBiometrico(),
                    _equiposBiometricos.ToList(), _nomenclaturaActiva, "EQUBIO", 
                    "192.168.1.1", _sedeActiva, _areaActiva)).Message;
            Assert.AreEqual("La dirección de red ingresada ya existe. " +
                "Verifique la red empresarial o el listado de equipos biométricos registrados en " +
                "estado activo o inactivo para encontrar la dirección de red duplicada.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad equipo biométrico, donde la sede se encuentra
        /// inactiva 
        /// </summary>
        [Test]
        public void PruebaModificarEquipoBiometricoConSedeInactiva()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ModificarEquipoBiometrico(new EquipoBiometrico(),
                    _equiposBiometricos.ToList(), _nomenclaturaActiva, "EQUBIO",
                    "192.168.2.6", _sedeInactiva, _areaActiva)).Message;
            var mensajeEsperado = "La sede seleccionada \"{0}\" se encuentra en estado INACTIVO."
                .Replace("{0}", _sedeInactiva.DescripcionSede);
            Assert.AreEqual(mensajeEsperado, mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad equipo biométrico, donde el área se encuentra
        /// inactiva
        /// </summary>
        [Test]
        public void PruebaModificarEquipoBiometricoConAreaInactiva()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ModificarEquipoBiometrico(new EquipoBiometrico(),
                    _equiposBiometricos.ToList(), _nomenclaturaActiva, "EQUBIO",
                    "192.168.2.6", _sedeActiva, _areaInactiva)).Message;
            var mensajeEsperado = "El área seleccionada \"{0}\" se encuentra en estado INACTIVO."
                .Replace("{0}", _areaInactiva.DescripcionArea);
            Assert.AreEqual(mensajeEsperado, mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica datos propios del equipos biométrico, donde no existe
        /// conexión al propio equipo
        /// </summary>
        [Test]
        public void PruebaModificarDatosPropiosDelEquipoBiometricoConFalloEnConexion()
        {
            var mensaje = Assert.Throws<ExcepcionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ModificarDatosPropiosDelEquipoBiometrico(
                    new EquipoBiometrico(), "user", "pass")).Message;
            Assert.AreEqual("No se pudo conectar con el equipo biométrico " +
                "para el cambio de sus propiedades. Verifique la existencia y comunicación " +
                "con el equipo biométrico.", mensaje);
        }
        /// <summary>
        /// Método de prueba que abre la puerta de acceso, donde no existe conexión al equipo
        /// biométrico
        /// </summary>
        [Test]
        public void PruebaAbrirPuertaDeAccesoDelEquipoBiometricoConFalloEnConexion()
        {
            var mensaje = Assert.Throws<ExcepcionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.AbrirPuertaDeAccesoDelEquipoBiometrico(
                    _equiposBiometricos.First(), "user", "pass")).Message;
            Assert.AreEqual("No se pudo conectar al equipo biométrico " +
                "para su manipulación. Verifique la existencia y comunicación con el " +
                "equipo biométrico.", mensaje);
        }
        /// <summary>
        /// Método de prueba que envía una señal al equipos biométrico, donde no existe
        /// conexión al propio equipo
        /// </summary>
        [Test]
        public void PruebaEnviarSenalAEquipoBiometricoConFalloEnConexion()
        {
            var mensaje = Assert.Throws<ExcepcionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.EnviarSenalAEquipoBiometrico(
                    _equiposBiometricos.First(), "user", "pass")).Message;
            Assert.AreEqual("No se pudo conectar al equipo biométrico " +
                "para su manipulación. Verifique la existencia y comunicación con el " +
                "equipo biométrico.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida equipos biométricos seleccionados para activación
        /// o inactivación, de forma correcta
        /// </summary>
        [Test]
        public void PruebaValidarEquiposBiometricosSeleccionadosCorrecto()
        {
            _servicioDominioEquipoBiometrico.ValidarEquiposBiometricosSeleccionados(1);
        }
        /// <summary>
        /// Método de prueba que valida equipos biométricos seleccionados para activación
        /// o inactivación, donde no existen equipos seleccionados
        /// </summary>
        [Test]
        public void PruebaValidarEquiposBiometricosSeleccionadosIncorrecto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ValidarEquiposBiometricosSeleccionados(0)).Message;
            Assert.AreEqual("Debe seleccionar, al menos, un equipo biométrico de la lista.", mensaje);
        }
        /// <summary>
        /// Método de prueba que inhabilita un equipo biométrico
        /// </summary>
        [Test]
        public void PruebaInhabilitarEquipoBiometrico()
        {
            var equipoBiometrico = _servicioDominioEquipoBiometrico.InhabilitarEquipoBiometrico(
                new EquipoBiometrico());
            Assert.AreEqual(false, equipoBiometrico.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que habilita un equipo biométrico
        /// </summary>
        [Test]
        public void PruebaHabilitarEquipoBiometrico()
        {
            var equipoBiometrico = _servicioDominioEquipoBiometrico.HabilitarEquipoBiometrico(
                new EquipoBiometrico());
            Assert.AreEqual(true, equipoBiometrico.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que valida un equipo biométrico para el proceso de reconocimiento,
        /// de forma correcta
        /// </summary>
        [Test]
        public void PruebaValidarEquipoBiometricoParaReconocimientoCorrecto()
        {
            _servicioDominioEquipoBiometrico.ValidarEquipoBiometricoParaReconocimientoDePersonal(
                _equipoBiometrico, string.Empty);
        }
        /// <summary>
        /// Método de prueba que valida un equipo biométrico para el proceso de reconocimiento,
        /// donde el equipo biométrico no existe o no está registrado en el sistema
        /// </summary>
        [Test]
        public void PruebaValidarEquipoBiometricoParaReconocimientoSinEquipoBiometrico()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionEquipoBiometricoPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ValidarEquipoBiometricoParaReconocimientoDePersonal(
                    null, "123456")).Message;
            var mensajeEsperado = "El equipo biométrico no está registrado: MAC {0}."
                .Replace("{0}", "123456");
            Assert.AreEqual(mensajeEsperado, mensaje);
        }
        /// <summary>
        /// Método de prueba que valida un equipo biométrico para el proceso de reconocimiento,
        /// donde el equipo biométrico se encuentra en estado inactivo
        /// </summary>
        [Test]
        public void PruebaValidarEquipoBiometricoParaReconocimientoConEquipoInactivo()
        {
            var equipoType = typeof(EquipoBiometrico);
            equipoType.GetProperty("IndicadorEstado").SetValue(_equipoBiometrico, false);
            var mensaje = Assert.Throws<ExcepcionAplicacionEquipoBiometricoPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ValidarEquipoBiometricoParaReconocimientoDePersonal(
                    _equipoBiometrico, string.Empty)).Message;
            Assert.AreEqual("El equipo biométrico no se encuentra activo.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida un equipo biométrico para el proceso de reconocimiento,
        /// donde la sede del equipo biométrico se encuentra inactiva
        /// </summary>
        [Test]
        public void PruebaValidarEquipoBiometricoParaReconocimientoConSedeInactiva()
        {
            var areaType = typeof(Area);
            areaType.GetProperty("Sede").SetValue(_equipoBiometrico.Area, _sedeInactiva);
            var mensaje = Assert.Throws<ExcepcionAplicacionEquipoBiometricoPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ValidarEquipoBiometricoParaReconocimientoDePersonal(
                    _equipoBiometrico, string.Empty)).Message;
            Assert.AreEqual("La sede del equipo biométrico no se encuentra activa.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida un equipo biométrico para el proceso de reconocimiento,
        /// donde el área del equipo biométrico se encuentra inactiva
        /// </summary>
        [Test]
        public void PruebaValidarEquipoBiometricoParaReconocimientoConAreaInactiva()
        {
            var areaType = typeof(Area);
            areaType.GetProperty("IndicadorEstado").SetValue(_equipoBiometrico.Area, false);
            var mensaje = Assert.Throws<ExcepcionAplicacionEquipoBiometricoPersonalizada>(() =>
                _servicioDominioEquipoBiometrico.ValidarEquipoBiometricoParaReconocimientoDePersonal(
                    _equipoBiometrico, string.Empty)).Message;
            Assert.AreEqual("El área del equipo biométrico no se encuentra activa.", mensaje);
        }
    }
}