using System;
using System.Collections.Generic;
using System.Linq;
using API.SRICA.Dominio.Entidad.BT;
using API.SRICA.Dominio.Entidad.SI;
using API.SRICA.Dominio.Entidad.US;
using API.SRICA.Dominio.Servicio.Implementacion;
using API.SRICA.Dominio.Servicio.Interfaz;
using NUnit.Framework;

namespace API.SRICA.Dominio.PruebaUnitaria
{
    /// <summary>
    /// Clase de prueba del servicio de dominio de bitácora de acciones del sistema
    /// </summary>
    public class ServicioDominioBitacoraAccionSistemaPrueba
    {
        /// <summary>
        /// Servicio de dominio de acciones del sistema
        /// </summary>
        private IServicioDominioBitacoraAccionSistema _servicioDominioBitacoraAccionSistema;
        /// <summary>
        /// Lista fake de bitácoras
        /// </summary>
        private IList<BitacoraAccionSistema> _bitacoras;
        /// <summary>
        /// Método que inicializa las pruebas
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _servicioDominioBitacoraAccionSistema = 
                new ServicioDominioBitacoraAccionSistema();

            _bitacoras = new List<BitacoraAccionSistema>();
            var bitacoraType = typeof(BitacoraAccionSistema);
            var bitacora =  new BitacoraAccionSistema();
            bitacoraType.GetProperty("CodigoAccionSistema").SetValue(bitacora, 1);
            bitacoraType.GetProperty("CodigoTipoEventoSistema").SetValue(bitacora, 1);
            bitacoraType.GetProperty("FechaAccion").SetValue(bitacora, DateTime.Parse("01/01/2021"));
            _bitacoras.Add(bitacora);
            bitacora =  new BitacoraAccionSistema();
            bitacoraType.GetProperty("CodigoAccionSistema").SetValue(bitacora, 1);
            bitacoraType.GetProperty("CodigoTipoEventoSistema").SetValue(bitacora, 2);
            bitacoraType.GetProperty("FechaAccion").SetValue(bitacora, DateTime.Parse("02/01/2021"));
            _bitacoras.Add(bitacora);
            bitacora =  new BitacoraAccionSistema();
            bitacoraType.GetProperty("CodigoAccionSistema").SetValue(bitacora, 2);
            bitacoraType.GetProperty("CodigoTipoEventoSistema").SetValue(bitacora, 1);
            bitacoraType.GetProperty("FechaAccion").SetValue(bitacora, DateTime.Parse("03/01/2021"));
            _bitacoras.Add(bitacora);
        }
        /// <summary>
        /// Método de prueba que crea la entidad bitácora de acción del sistema
        /// </summary>
        [Test]
        public void PruebaCrearBitacoraAccionSistema()
        {
            var bitacora = _servicioDominioBitacoraAccionSistema.CrearBitacora(new Usuario(), 
                new ModuloSistema(), new RecursoSistema(), new TipoEventoSistema(), new AccionSistema(), 
                "Prueba", string.Empty, string.Empty);
            Assert.IsNotNull(bitacora);
        }
        /// <summary>
        /// Método de prueba que filtra un listado de bitácora de acción del sistema
        /// según acción de sistema indicado
        /// </summary>
        [Test]
        public void PruebaFiltrarBitacoraAccionSistemaSegunAccionDelSistema()
        {
            var cantidadRegistrosEsperados = 2;
            var accionSistema = new AccionSistema();
            var accionSistemaType = typeof(AccionSistema);
            accionSistemaType.GetProperty("CodigoAccionSistema").SetValue(accionSistema, 1);
            var cantidad =
                _servicioDominioBitacoraAccionSistema.FiltrarBitacoraDeAccionSegunAccionDelSistema(
                    _bitacoras.ToList(), accionSistema);
            Assert.AreEqual(cantidadRegistrosEsperados, cantidad.Count);
        }
        /// <summary>
        /// Método de prueba que filtra un listado de bitácora de acción del sistema
        /// según tipo de evento de sistema indicado
        /// </summary>
        [Test]
        public void PruebaFiltrarBitacoraAccionSistemaSegunTipoEventoSistema()
        {
            var cantidadRegistrosEsperados = 2;
            var tipoEventoSistema = new TipoEventoSistema();
            var tipoEventoSistemaType = typeof(TipoEventoSistema);
            tipoEventoSistemaType.GetProperty("CodigoTipoEventoSistema").SetValue(tipoEventoSistema, 1);
            var cantidad =
                _servicioDominioBitacoraAccionSistema.FiltrarBitacoraDeAccionSegunTipoDeEvento(
                    _bitacoras.ToList(), tipoEventoSistema);
            Assert.AreEqual(cantidadRegistrosEsperados, cantidad.Count);
        }
        /// <summary>
        /// Método que ordena un listado de bitácora de acción del sistema por fecha,
        /// de forma ascendente
        /// </summary>
        [Test]
        public void PruebaOrdenarBitacoraAccionSistemaPorFechaAscendente()
        {
            var fechaEsperada = "01/01/2021";
            var bitacoras = _servicioDominioBitacoraAccionSistema.OrdenarBitacoraDeAccionPorFecha(
                _bitacoras.ToList());
            Assert.AreEqual(fechaEsperada, bitacoras.FirstOrDefault().FechaAccion.ToString("dd/MM/yyyy"));
        }
        /// <summary>
        /// Método que ordena un listado de bitácora de acción del sistema por fecha,
        /// de forma descendente
        /// </summary>
        [Test]
        public void PruebaOrdenarBitacoraAccionSistemaPorFechaDescendente()
        {
            var fechaEsperada = "03/01/2021";
            var bitacoras = _servicioDominioBitacoraAccionSistema.OrdenarBitacoraDeAccionPorFecha(
                _bitacoras.ToList(), false);
            Assert.AreEqual(fechaEsperada, bitacoras.FirstOrDefault().FechaAccion.ToString("dd/MM/yyyy"));
        }
        /// <summary>
        /// Método que ordena un listado de bitácora de acción del sistema por fecha,
        /// según límite de registros
        /// </summary>
        [Test]
        public void PruebaOrdenarBitacoraAccionSistemaPorLimiteDeRegistros()
        {
            var cantidadRegistrosEsperados = 2;
            var bitacoras = _servicioDominioBitacoraAccionSistema.OrdenarBitacoraDeAccionPorFecha(
                _bitacoras.ToList(), true, 2);
            Assert.AreEqual(cantidadRegistrosEsperados, bitacoras.Count);
        }
    }
}