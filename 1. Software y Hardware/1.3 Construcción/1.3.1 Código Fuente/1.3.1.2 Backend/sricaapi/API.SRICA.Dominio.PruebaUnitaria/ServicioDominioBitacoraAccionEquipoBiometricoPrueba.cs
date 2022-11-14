using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.PE;
using API.SRICA.Dominio.Entidad.SI;
using API.SRICA.Dominio.Servicio.Implementacion;
using API.SRICA.Dominio.Servicio.Interfaz;
using NUnit.Framework;

namespace API.SRICA.Dominio.PruebaUnitaria
{
    /// <summary>
    /// Clase de prueba del servicio de dominio de bitácora de acciones de equipos biométricos
    /// </summary>
    public class ServicioDominioBitacoraAccionEquipoBiometricoPrueba
    {
        /// <summary>
        /// Servicio de dominio de acciones de equipos biométricos
        /// </summary>
        private IServicioDominioBitacoraAccionEquipoBiometrico _servicioDominioBitacoraAccionEquipoBiometrico;
        /// <summary>
        /// Método que inicializa las pruebas
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _servicioDominioBitacoraAccionEquipoBiometrico = 
                new ServicioDominioBitacoraAccionEquipoBiometrico();
        }
        /// <summary>
        /// Método de prueba que crea la entidad bitácora de acción de equipos biométricos
        /// </summary>
        [Test]
        public void PruebaCrearBitacoraAccionEquipoBiometrico()
        {
            var bitacora = _servicioDominioBitacoraAccionEquipoBiometrico.CrearBitacora(
                new PersonalEmpresa(), new EquipoBiometrico(), new ResultadoAcceso(), "Prueba", 
                string.Empty);
            Assert.IsNotNull(bitacora);
        }
    }
}