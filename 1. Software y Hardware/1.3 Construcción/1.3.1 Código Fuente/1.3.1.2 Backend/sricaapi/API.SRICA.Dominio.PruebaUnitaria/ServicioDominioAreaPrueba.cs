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
    /// Clase de prueba del servicio de dominio de áreas
    /// </summary>
    public class ServicioDominioAreaPrueba
    {
        /// <summary>
        /// Servicio de dominio de áreas
        /// </summary>
        private IServicioDominioArea _servicioDominioArea;
        /// <summary>
        /// Entidad fake de sede activa
        /// </summary>
        private Sede _sedeActivo;
        /// <summary>
        /// Entidad fake de sede inactiva
        /// </summary>
        private Sede _sedeInactivo;
        /// <summary>
        /// Mensaje de excepción para la descripción del área
        /// </summary>
        private const string MENSAJE_EXCEPCION_ESPERADO_DESCRIPCION_AREA = "La cantidad " +
            "máxima de caracteres para el área es de 40 caracteres.";
        /// <summary>
        /// Mensaje de excepción para sedes inactivas
        /// </summary>
        private const string MENSAJE_EXCEPCION_ESPERADO_SEDE_INACTIVA = "La sede " +
            "seleccionada \"{0}\" se encuentra en estado INACTIVO.";
        /// <summary>
        /// Método que inicializa las pruebas
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _servicioDominioArea = new ServicioDominioArea();
            
            var sedeType = typeof(Sede);
            _sedeActivo = new Sede();
            _sedeInactivo = new Sede();
            sedeType.GetProperty("DescripcionSede").SetValue(_sedeActivo, "Sede Uno");
            sedeType.GetProperty("IndicadorEstado").SetValue(_sedeActivo, true);
            sedeType.GetProperty("DescripcionSede").SetValue(_sedeInactivo, "Sede Dos");
            sedeType.GetProperty("IndicadorEstado").SetValue(_sedeInactivo, false);
        }
        /// <summary>
        /// Método de prueba que crea la entidad área, de forma correcta
        /// </summary>
        [Test]
        public void PruebaCrearAreaCorrecto()
        {
            var area = _servicioDominioArea.CrearArea(_sedeActivo, "Area Uno");
            Assert.IsNotNull(area);
            Assert.AreEqual(true, area.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que crea la entidad área, donde no se declara la descripción
        /// del área
        /// </summary>
        [Test]
        public void PruebaCrearAreaConDescripcionVacia()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioArea.CrearArea(_sedeActivo, string.Empty)).Message;
            Assert.AreEqual(MENSAJE_EXCEPCION_ESPERADO_DESCRIPCION_AREA, mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad área, donde la descripción del área tiene más de 40
        /// caracteres
        /// </summary>
        [Test]
        public void PruebaCrearAreaConDescripcionMayorACuarentaCaracteres()
        {
            var descripcionArea = "asdasdasdasdasdwdasdsadwdasdwdasdawdawdawd awdaw dawd awdaw";
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioArea.CrearArea(_sedeActivo, descripcionArea)).Message;
            Assert.AreEqual(MENSAJE_EXCEPCION_ESPERADO_DESCRIPCION_AREA, mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad área, donde la sede del área se encuentra inactiva 
        /// </summary>
        [Test]
        public void PruebaCrearAreaConSedeInactiva()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioArea.CrearArea(_sedeInactivo, "Area Uno")).Message;
            var resultado = MENSAJE_EXCEPCION_ESPERADO_SEDE_INACTIVA.Replace("{0}", 
                _sedeInactivo.DescripcionSede);
            Assert.AreEqual(resultado, mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad área, de forma correcta
        /// </summary>
        [Test]
        public void PruebaModificarAreaCorrecto()
        {
            var descripcionArea = "Area Dos";
            var area = _servicioDominioArea.ModificarArea(new Area(), _sedeActivo, descripcionArea);
            Assert.IsNotNull(area);
            Assert.AreEqual(descripcionArea, area.DescripcionArea);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad área, donde no se declara la descripción
        /// del área
        /// </summary>
        [Test]
        public void PruebaModificarAreaConDescripcionVacia()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioArea.ModificarArea(new Area(), _sedeActivo, string.Empty)).Message;
            Assert.AreEqual(MENSAJE_EXCEPCION_ESPERADO_DESCRIPCION_AREA, mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad área, donde la descripción del área tiene más de 40
        /// caracteres
        /// </summary>
        [Test]
        public void PruebaModificarAreaConDescripcionMayorACuarentaCaracteres()
        {
            var descripcionArea = "asdasdasdasdasdwdasdsadwdasdwdasdawdawdawd awdaw dawd awdaw";
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioArea.ModificarArea(new Area(), _sedeActivo, descripcionArea)).Message;
            Assert.AreEqual(MENSAJE_EXCEPCION_ESPERADO_DESCRIPCION_AREA, mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad área, donde la sede del área se encuentra inactiva 
        /// </summary>
        [Test]
        public void PruebaModificarAreaConSedeInactiva()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioArea.ModificarArea(new Area(), _sedeInactivo, "Area Uno")).Message;
            var resultado = MENSAJE_EXCEPCION_ESPERADO_SEDE_INACTIVA.Replace("{0}", 
                _sedeInactivo.DescripcionSede);
            Assert.AreEqual(resultado, mensaje);
        }
        /// <summary>
        /// Método de prueba que valida que existan áreas seleccionadas para el proceso de
        /// activación o inactivación de áreas 
        /// </summary>
        [Test]
        public void PruebaValidarAreasSeleccionadasCorrecto()
        {
            _servicioDominioArea.ValidarAreasSeleccionadas(20);
        }
        /// <summary>
        /// Método de prueba que valida que no existan áreas seleccionadas para el proceso de
        /// activación o inactivación de áreas
        /// </summary>
        [Test]
        public void PruebaValidarAreasSeleccionadasIncorrecto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioArea.ValidarAreasSeleccionadas(0)).Message;
            Assert.AreEqual("Debe seleccionar, al menos, un área de la lista.", mensaje);
        }
        /// <summary>
        /// Método de prueba que inhabilita un área
        /// </summary>
        [Test]
        public void PruebaInhabilitarArea()
        {
            var area = _servicioDominioArea.InhabilitarArea(new Area());
            Assert.AreEqual(false, area.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que habilita un área
        /// </summary>
        [Test]
        public void PruebaHabilitarArea()
        {
            var area = _servicioDominioArea.HabilitarArea(new Area());
            Assert.AreEqual(true, area.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que asigna un área para "sin asignación" a un equipo biométrico
        /// </summary>
        [Test]
        public void PruebaAsignarAreaSinAsignacionParaEquiposBiometricos()
        {
            var equipoBiometrico =
                _servicioDominioArea.AsignarAreaSinAsignacionAEquipoBiometrico(new EquipoBiometrico());
            Assert.AreEqual(Area.CodigoAreaNoAsignado, equipoBiometrico.CodigoArea);
        }
    }
}