using System.Collections.Generic;
using System.Linq;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Excepcion;
using API.SRICA.Dominio.Servicio.Implementacion;
using API.SRICA.Dominio.Servicio.Interfaz;
using NUnit.Framework;

namespace API.SRICA.Dominio.PruebaUnitaria
{
    /// <summary>
    /// Clase de prueba del servicio de dominio de nomenclaturas de equipos biométricos
    /// </summary>
    public class ServicioDominioNomenclaturaEquipoBiometricoPrueba
    {
        /// <summary>
        /// Servicio de dominio de nomenclaturas de equipos biométricos
        /// </summary>
        private IServicioDominioNomenclaturaEquipoBiometrico _servicioDominioNomenclaturaEquipoBiometrico;
        /// <summary>
        /// Lista de nomenclaturas fake
        /// </summary>
        private IList<NomenclaturaEquipoBiometrico> _nomenclaturas;
        /// <summary>
        /// Método que inicializa las pruebas
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _servicioDominioNomenclaturaEquipoBiometrico = new ServicioDominioNomenclaturaEquipoBiometrico();

            var nomenclaturaType = typeof(NomenclaturaEquipoBiometrico);
            var nomenclatura = new NomenclaturaEquipoBiometrico();
            _nomenclaturas = new List<NomenclaturaEquipoBiometrico>();
            nomenclaturaType.GetProperty("CodigoNomenclatura").SetValue(nomenclatura, 1);
            nomenclaturaType.GetProperty("DescripcionNomenclatura").SetValue(nomenclatura, "SRI");
            _nomenclaturas.Add(nomenclatura);
            nomenclatura = new NomenclaturaEquipoBiometrico();
            nomenclaturaType.GetProperty("CodigoNomenclatura").SetValue(nomenclatura, 2);
            nomenclaturaType.GetProperty("DescripcionNomenclatura").SetValue(nomenclatura, "SRU");
            _nomenclaturas.Add(nomenclatura);
            nomenclatura = new NomenclaturaEquipoBiometrico();
            nomenclaturaType.GetProperty("CodigoNomenclatura").SetValue(nomenclatura, 3);
            nomenclaturaType.GetProperty("DescripcionNomenclatura").SetValue(nomenclatura, "SRP");
            _nomenclaturas.Add(nomenclatura);
        }
        /// <summary>
        /// Método de prueba que crea la entida nomenclatura, de forma correcta
        /// </summary>
        [Test]
        public void PruebaCrearNomenclaturaCorrecto()
        {
            var nomenclatura = _servicioDominioNomenclaturaEquipoBiometrico.CrearNomenclatura(
                _nomenclaturas.ToList(), "RUT");
            Assert.IsNotNull(nomenclatura);
            Assert.AreEqual(true, nomenclatura.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que crea la entida nomenclatura, donde la descripción de
        /// nomenclatura no contiene 3 caracteres
        /// </summary>
        [Test]
        public void PruebaCrearNomenclaturaSinDescripcionDeTresCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioNomenclaturaEquipoBiometrico.CrearNomenclatura(_nomenclaturas.ToList(),
                    "WS")).Message;
            Assert.AreEqual("La nomenclatura debe tener 3 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entida nomenclatura, donde la descripción de
        /// la nomenclatura contiene números y/o espacios
        /// </summary>
        [Test]
        public void PruebaCrearNomenclaturaConDescripcionSinSoloTextoyEspacios()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioNomenclaturaEquipoBiometrico.CrearNomenclatura(_nomenclaturas.ToList(),
                    "2w ")).Message;
            Assert.AreEqual("La nomenclatura debe estar compuesta solo por letras.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entida nomenclatura, donde la descripción
        /// de la nomenclatura ya existe
        /// </summary>
        [Test]
        public void PruebaCrearNomenclaturaConDescripcionExistente()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioNomenclaturaEquipoBiometrico.CrearNomenclatura(_nomenclaturas.ToList(),
                    "SRI")).Message;
            Assert.AreEqual("La nomenclatura ingresada ya existe. Verifique el " +
                "listado de nomenclaturas en estado activo o inactivo para encontrar la nomenclatura " +
                "duplicada.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entida nomenclatura, de forma correcta
        /// </summary>
        [Test]
        public void PruebaModificarNomenclaturaCorrecto()
        {
            var nomenclatura = _servicioDominioNomenclaturaEquipoBiometrico.ModificarNomenclatura(
                new NomenclaturaEquipoBiometrico(), _nomenclaturas.ToList(), "URT");
            Assert.IsNotNull(nomenclatura);
            Assert.AreEqual("URT", nomenclatura.DescripcionNomenclatura);
        }
        /// <summary>
        /// Método de prueba que modifica la entida nomenclatura, donde la descripción de
        /// nomenclatura no contiene 3 caracteres
        /// </summary>
        [Test]
        public void PruebaModificarNomenclaturaSinDescripcionDeTresCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioNomenclaturaEquipoBiometrico.ModificarNomenclatura(
                    new NomenclaturaEquipoBiometrico(), _nomenclaturas.ToList(), "WS")).Message;
            Assert.AreEqual("La nomenclatura debe tener 3 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entida nomenclatura, donde la descripción de
        /// la nomenclatura contiene números y/o espacios
        /// </summary>
        [Test]
        public void PruebaModificarNomenclaturaConDescripcionSinSoloTextoyEspacios()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioNomenclaturaEquipoBiometrico.ModificarNomenclatura(
                    new NomenclaturaEquipoBiometrico(), _nomenclaturas.ToList(), "2w ")).Message;
            Assert.AreEqual("La nomenclatura debe estar compuesta solo por letras.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entida nomenclatura, donde la descripción
        /// de la nomenclatura ya existe
        /// </summary>
        [Test]
        public void PruebaModificarNomenclaturaConDescripcionExistente()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioNomenclaturaEquipoBiometrico.ModificarNomenclatura(
                    new NomenclaturaEquipoBiometrico(), _nomenclaturas.ToList(), "SRI")).Message;
            Assert.AreEqual("La nomenclatura ingresada ya existe. Verifique el " +
                "listado de nomenclaturas en estado activo o inactivo para encontrar la nomenclatura " +
                "duplicada.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida el listado de nomenclaturas seleccionadas para el proceso
        /// de activación o inactivación de nomenclaturas, de forma correcta
        /// </summary>
        [Test]
        public void PruebaValidarNomenclaturasSeleccionadasCorrecto()
        {
            _servicioDominioNomenclaturaEquipoBiometrico.ValidarNomenclaturasSeleccionadas(1);
        }
        /// <summary>
        /// Método de prueba que valida el listado de nomenclaturas seleccionadas para el proceso
        /// de activación o inactivación de nomenclaturas, donde no se han seleccionado registros
        /// </summary>
        [Test]
        public void PruebaValidarNomenclaturasSeleccionadasIncorrecto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioNomenclaturaEquipoBiometrico.ValidarNomenclaturasSeleccionadas(0)).Message;
            Assert.AreEqual("Debe seleccionar, al menos, una nomenclatura de la lista.", mensaje);
        }
        /// <summary>
        /// Método de prueba que inhabilita una nomenclatura
        /// </summary>
        [Test]
        public void PruebaInhabilitarNomenclatura()
        {
            var nomenclatura =
                _servicioDominioNomenclaturaEquipoBiometrico
                    .InhabilitarNomenclatura(new NomenclaturaEquipoBiometrico());
            Assert.AreEqual(false, nomenclatura.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que habilita una nomenclatura
        /// </summary>
        [Test]
        public void PruebaHabilitarNomenclatura()
        {
            var nomenclatura =
                _servicioDominioNomenclaturaEquipoBiometrico
                    .HabilitarNomenclatura(new NomenclaturaEquipoBiometrico());
            Assert.AreEqual(true, nomenclatura.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que asigna una nomenclatura para "sin asignación" a un
        /// equipo biométrico
        /// </summary>
        [Test]
        public void PruebaAsignarNomenclaturaSinAsignacionAEquipoBiometrico()
        {
            var equipoBiometrico = _servicioDominioNomenclaturaEquipoBiometrico
                .AsignarNomenclaturaSinAsignacionAEquipoBiometrico(new EquipoBiometrico());
            Assert.AreEqual(NomenclaturaEquipoBiometrico.CodigoNomenclaturaNoAsignado, 
                equipoBiometrico.CodigoNomenclatura);
        }
    }
}