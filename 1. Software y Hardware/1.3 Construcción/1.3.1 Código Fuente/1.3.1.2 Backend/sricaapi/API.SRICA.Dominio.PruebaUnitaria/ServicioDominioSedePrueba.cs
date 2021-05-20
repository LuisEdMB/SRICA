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
    /// Clase de prueba del servicio de dominio de sedes
    /// </summary>
    public class ServicioDominioSedePrueba
    {
        /// <summary>
        /// Servicio de dominio de sedes
        /// </summary>
        private IServicioDominioSede _servicioDominioSede;
        /// <summary>
        /// Método que inicializa las pruebas
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _servicioDominioSede = new ServicioDominioSede();
        }
        /// <summary>
        /// Método de prueba que crea la entidad sede, de forma correcta
        /// </summary>
        [Test]
        public void PruebaCrearSedeCorrecto()
        {
            var sede = _servicioDominioSede.CrearSede("SedeUno");
            Assert.IsNotNull(sede);
            Assert.AreEqual("SedeUno", sede.DescripcionSede);
        }
        /// <summary>
        /// Método de prueba que crea la entidad sede, donde la descripción
        /// de la sede está vacía
        /// </summary>
        [Test]
        public void PruebaCrearSedeConDescripcionDeSedeVacia()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioSede.CrearSede(string.Empty)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para la sede " +
                "es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad sede, donde la descripción
        /// de la sede tiene más de 40 caracteres
        /// </summary>
        [Test]
        public void PruebaCrearSedeConDescripcionDeSedeMayorCuarentaCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioSede.CrearSede("asdasd asd asd as das a asdasdasdasdasda" +
                    "sdasdasasdasdasda sda sda sdasdasdasdasd asdasd asdasdasdasdas dasda" +
                    "asdasdasdasdasdasdasd asd asdasdasdasdasdasd asdasdasdasdasdasdasdasd")).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para la sede " +
                "es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad sede, de forma correcta
        /// </summary>
        [Test]
        public void PruebaModificarSedeCorrecto()
        {
            var sede = _servicioDominioSede.ModificarSede(new Sede(), "SedeDos");
            Assert.IsNotNull(sede);
            Assert.AreEqual("SedeDos", sede.DescripcionSede);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad sede, donde la descripción
        /// de la sede está vacía
        /// </summary>
        [Test]
        public void PruebaModificarSedeConDescripcionDeSedeVacia()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioSede.ModificarSede(new Sede(), string.Empty)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para la sede " +
                "es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad sede, donde la descripción
        /// de la sede tiene más de 40 caracteres
        /// </summary>
        [Test]
        public void PruebaModificarSedeConDescripcionDeSedeMayorCuarentaCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioSede.ModificarSede(new Sede(), 
                    "asdasd asd asd as das a asdasdasdasdasdasdasdasa" +
                    "sdasdasda sda sda sdasdasdasdasd asdasd asdasdasdasdas dasda" +
                    "asdasdasdasdasdasdasd asd asdasdasdasdasdasd asdasdasdasdasdasdasdasd")).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para la sede " +
                "es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida la selección de sedes para los procesos de
        /// activación o inactivación, de forma correcta
        /// </summary>
        [Test]
        public void PruebaValidarSedesSeleccionadasCorrecto()
        {
            _servicioDominioSede.ValidarSedesSeleccionadas(1);
        }
        /// <summary>
        /// Método de prueba que valida la selección de sedes para los procesos de
        /// activación o inactivación, sin registros seleccionados
        /// </summary>
        [Test]
        public void PruebaValidarSedesSeleccionadasIncorrecto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioSede.ValidarSedesSeleccionadas(0)).Message;
            Assert.AreEqual("Debe seleccionar, al menos, una sede de la lista.", mensaje);
        }
        /// <summary>
        /// Método de prueba que inhabilita una sede
        /// </summary>
        [Test]
        public void PruebaInhabilitarSede()
        {
            var sede = _servicioDominioSede.InhabilitarSede(new Sede());
            Assert.AreEqual(false, sede.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que habilita una sede
        /// </summary>
        [Test]
        public void PruebaHabilitarSede()
        {
            var sede = _servicioDominioSede.HabilitarSede(new Sede());
            Assert.AreEqual(true, sede.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que asigna una sede "sin asignación" a un área
        /// </summary>
        [Test]
        public void PruebaAsignarSedeSinAsignacionAArea()
        {
            var area = _servicioDominioSede.AsignarSedeSinAsignacionAArea(new Area());
            Assert.AreEqual(Sede.CodigoSedeNoAsignado, area.CodigoSede);
        }
        /// <summary>
        /// Método de prueba que asigna un área para "sin asignación" a un equipo
        /// biométrico
        /// </summary>
        [Test]
        public void PruebaAsignarAreaSinAsignacionAEquipoBiometrico()
        {
            var equipoBiometrico = _servicioDominioSede.AsignarAreaSinAsignacionAEquipoBiometrico(
                new EquipoBiometrico());
            Assert.AreEqual(Area.CodigoAreaNoAsignado, equipoBiometrico.CodigoArea);
        }
    }
}