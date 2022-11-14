using System;
using System.Collections.Generic;
using System.Linq;
using API.SRICA.Dominio.Entidad.AR;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.PE;
using API.SRICA.Dominio.Entidad.SE;
using API.SRICA.Dominio.Excepcion;
using API.SRICA.Dominio.Servicio.Implementacion;
using API.SRICA.Dominio.Servicio.Interfaz;
using NUnit.Framework;

namespace API.SRICA.Dominio.PruebaUnitaria
{
    /// <summary>
    /// Clase de prueba del servicio de dominio de personal de la empresa
    /// </summary>
    public class ServicioDominioPersonalEmpresaPrueba
    {
        /// <summary>
        /// Servicio de dominio de personal de la empresa
        /// </summary>
        private IServicioDominioPersonalEmpresa _servicioDominioPersonalEmpresa;
        /// <summary>
        /// Listado de personal de la empresa fake
        /// </summary>
        private IList<PersonalEmpresa> _personalLista;
        /// <summary>
        /// Sede fake activa
        /// </summary>
        private Sede _sedeActiva;
        /// <summary>
        /// Sede fake inactiva
        /// </summary>
        private Sede _sedeInactiva;
        /// <summary>
        /// Área fake activa
        /// </summary>
        private Area _areaActiva;
        /// <summary>
        /// Área fake inactiva
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
            _servicioDominioPersonalEmpresa = new ServicioDominioPersonalEmpresa();
            
            var personal = new PersonalEmpresa();
            var personalType = typeof(PersonalEmpresa);
            _personalLista = new List<PersonalEmpresa>();
            personalType.GetProperty("CodigoPersonalEmpresa").SetValue(personal, 1);
            personalType.GetProperty("DNIPersonalEmpresa").SetValue(personal, "12345678");
            _personalLista.Add(personal);
            personal = new PersonalEmpresa();
            personalType.GetProperty("CodigoPersonalEmpresa").SetValue(personal, 2);
            personalType.GetProperty("DNIPersonalEmpresa").SetValue(personal, "12345679");
            _personalLista.Add(personal);
            
            _sedeActiva = new Sede();
            _sedeInactiva = new Sede();
            _areaActiva = new Area();
            _areaInactiva = new Area();
            var sedeType = typeof(Sede);
            var areaType = typeof(Area);
            sedeType.GetProperty("IndicadorEstado").SetValue(_sedeActiva, true);
            sedeType.GetProperty("DescripcionSede").SetValue(_sedeInactiva, "Sede1");
            sedeType.GetProperty("IndicadorEstado").SetValue(_sedeInactiva, false);
            areaType.GetProperty("IndicadorEstado").SetValue(_areaActiva, true);
            areaType.GetProperty("DescripcionArea").SetValue(_areaInactiva, "Area1");
            areaType.GetProperty("IndicadorEstado").SetValue(_areaInactiva, false);
            
            _equipoBiometrico = new EquipoBiometrico();
            var equipoBiometricoType = typeof(EquipoBiometrico);
            equipoBiometricoType.GetProperty("CodigoArea").SetValue(_equipoBiometrico, 1);
        }
        /// <summary>
        /// Método de prueba que crea la entidad personal empresa, de forma correcta
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaCorrecto()
        {
            var personal = _servicioDominioPersonalEmpresa.CrearPersonalEmpresa(
                _personalLista.ToList(), "11234567", "prueba", "prueba", 1,
                "imagen");
            Assert.IsNotNull(personal);
            Assert.AreEqual("11234567", personal.DNIPersonalEmpresa);
        }
        /// <summary>
        /// Método de prueba que crea la entidad personal empresa, donde no se han seleccionado
        /// las áreas para el personal
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaSinAreasSeleccionadas()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.CrearPersonalEmpresa(_personalLista.ToList(),
                    "11234567", "prueba", "prueba", 0, "imagen")).Message;
            Assert.AreEqual("Debe seleccionar una o varias áreas para el personal.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad personal empresa, donde no se ha capturado
        /// la respectiva imagen del iris
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaSinImagenDeIrisCapturado()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.CrearPersonalEmpresa(_personalLista.ToList(),
                    "11234567", "prueba", "prueba", 1, string.Empty)).Message;
            Assert.AreEqual("Debe capturar la respectiva imagen de iris para el personal.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad personal empresa, donde el DNI no contiene
        /// solo números
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaConDniSinSoloNumeros()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.CrearPersonalEmpresa(_personalLista.ToList(),
                    "1123456s", "prueba", "prueba", 1, "imagen")).Message;
            Assert.AreEqual("El DNI debe ser de tipo numérico.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad personal empresa, donde el DNI no contiene
        /// 8 dígitos
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaConDniSinOchoDigitos()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.CrearPersonalEmpresa(_personalLista.ToList(),
                    "1234", "prueba", "prueba", 1, "imagen")).Message;
            Assert.AreEqual("La cantidad de dígitos del DNI debe ser 8 dígitos.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad personal empresa, donde el DNI ya existe
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaConDniExistente()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.CrearPersonalEmpresa(_personalLista.ToList(),
                    "12345678", "prueba", "prueba", 1, "imagen")).Message;
            Assert.AreEqual("El DNI ingresado ya existe. " +
                "Verifique el listado del personal en estado activo o inactivo para " +
                "encontrar al personal duplicado.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad personal empresa, donde el nombre del personal
        /// es vacío
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaConNombreVacio()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.CrearPersonalEmpresa(_personalLista.ToList(),
                    "11234567", string.Empty, "prueba", 1, "imagen")).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el nombre es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad personal empresa, donde el nombre del personal
        /// tiene más de 40 caracteres
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaConNombreMayorCuarentaCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.CrearPersonalEmpresa(_personalLista.ToList(),
                    "11234567", "prueba asdasdasda sdasdasdakjfakjsdlkajsldkajsldkaj" +
                                "aksjdlaksjdlaksjdlakjdlakjlaksdjalksdjalksdjalksdjalksdjalksjd" +
                                "laksjdlakjsdlakjsdlaksjdlakjsdlahfaksdjalskdcañlskdñalskdñalksda" +
                                "lakjsdakjsdlakjsldkajsdlkajsldkajsdlkajsdlkajsdlkajsldkjasldkjaslk" +
                                "lakjsdlakjsdlakjsdlaksjd", "prueba", 1, "imagen")).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el nombre es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad personal empresa, donde el nombre del personal
        /// no contiene solo texto
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaConNombreSinSoloTexto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.CrearPersonalEmpresa(_personalLista.ToList(),
                    "11234567", "prueba12 232-¿'", "prueba", 1, "imagen")).Message;
            Assert.AreEqual("El nombre ingresado debe contener solo letras.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad personal empresa, donde el apellido del personal
        /// es vacío
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaConApellidoVacio()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.CrearPersonalEmpresa(_personalLista.ToList(),
                    "11234567", "prueba", string.Empty, 1, "imagen")).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el apellido es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad personal empresa, donde el apellido del personal
        /// tiene más de 40 caracteres
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaConApellidoMayorCuarentaCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.CrearPersonalEmpresa(_personalLista.ToList(),
                    "11234567", "prueba", "prueba asdasdasda sdasdasdakjfakjsdlkajsldkajsldkaj" +
                            "aksjdlaksjdlaksjdlakjdlakjlaksdjalksdjalksdjalksdjalksdjalksjd" +
                            "laksjdlakjsdlakjsdlaksjdlakjsdlahfaksdjalskdcañlskdñalskdñalksda" +
                            "lakjsdakjsdlakjsldkajsdlkajsldkajsdlkajsdlkajsdlkajsldkjasldkjaslk" +
                            "lakjsdlakjsdlakjsdlaksjd", 1, "imagen")).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el apellido es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad personal empresa, donde el apellido del personal
        /// no contiene solo texto
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaConApellidoSinSoloTexto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.CrearPersonalEmpresa(_personalLista.ToList(),
                    "11234567", "prueba", "prueba12 232-¿'", 1, "imagen")).Message;
            Assert.AreEqual("El apellido ingresado debe contener solo letras.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad personal empresa, de forma correcta
        /// </summary>
        [Test]
        public void PruebaModificarPersonalEmpresaCorrecto()
        {
            var personal = _servicioDominioPersonalEmpresa.ModificarPersonalEmpresa(new PersonalEmpresa(), 
                _personalLista.ToList(), "11234561", "prueba", "prueba", 1);
            Assert.IsNotNull(personal);
            Assert.AreEqual("11234561", personal.DNIPersonalEmpresa);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad personal empresa, donde no se han seleccionado
        /// las áreas para el personal
        /// </summary>
        [Test]
        public void PruebaModificarPersonalEmpresaSinAreasSeleccionadas()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ModificarPersonalEmpresa(new PersonalEmpresa(), 
                    _personalLista.ToList(), "11234567", "prueba", "prueba", 0)).Message;
            Assert.AreEqual("Debe seleccionar una o varias áreas para el personal.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad personal empresa, donde el DNI no contiene
        /// solo números
        /// </summary>
        [Test]
        public void PruebaModificarPersonalEmpresaConDniSinSoloNumeros()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ModificarPersonalEmpresa(new PersonalEmpresa(), 
                    _personalLista.ToList(), "1123456s", "prueba", "prueba", 1)).Message;
            Assert.AreEqual("El DNI debe ser de tipo numérico.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad personal empresa, donde el DNI no contiene
        /// 8 dígitos
        /// </summary>
        [Test]
        public void PruebaModificarPersonalEmpresaConDniSinOchoDigitos()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ModificarPersonalEmpresa(new PersonalEmpresa(), 
                    _personalLista.ToList(), "1234", "prueba", "prueba", 1)).Message;
            Assert.AreEqual("La cantidad de dígitos del DNI debe ser 8 dígitos.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad personal empresa, donde el DNI ya existe
        /// </summary>
        [Test]
        public void PruebaModificarPersonalEmpresaConDniExistente()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ModificarPersonalEmpresa(new PersonalEmpresa(), 
                    _personalLista.ToList(), "12345678", "prueba", "prueba", 1)).Message;
            Assert.AreEqual("El DNI ingresado ya existe. " +
                "Verifique el listado del personal en estado activo o inactivo para " +
                "encontrar al personal duplicado.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad personal empresa, donde el nombre del personal
        /// es vacío
        /// </summary>
        [Test]
        public void PruebaModificarPersonalEmpresaConNombreVacio()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ModificarPersonalEmpresa(new PersonalEmpresa(), 
                    _personalLista.ToList(), "11234567", string.Empty, "prueba", 1)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el nombre es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad personal empresa, donde el nombre del personal
        /// tiene más de 40 caracteres
        /// </summary>
        [Test]
        public void PruebaModificarPersonalEmpresaConNombreMayorCuarentaCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ModificarPersonalEmpresa(new PersonalEmpresa(), 
                    _personalLista.ToList(),
                        "11234567", "prueba asdasdasda sdasdasdakjfakjsdlkajsldkajsldkaj" + 
                            "aksjdlaksjdlaksjdlakjdlakjlaksdjalksdjalksdjalksdjalksdjalksjd" +
                            "laksjdlakjsdlakjsdlaksjdlakjsdlahfaksdjalskdcañlskdñalskdñalksda" +
                            "lakjsdakjsdlakjsldkajsdlkajsldkajsdlkajsdlkajsdlkajsldkjasldkjaslk" +
                            "lakjsdlakjsdlakjsdlaksjd", "prueba", 1)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el nombre es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad personal empresa, donde el nombre del personal
        /// no contiene solo texto
        /// </summary>
        [Test]
        public void PruebaModificarPersonalEmpresaConNombreSinSoloTexto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ModificarPersonalEmpresa(new PersonalEmpresa(), 
                    _personalLista.ToList(), "11234567", "prueba12 232-¿'", "prueba", 1)).Message;
            Assert.AreEqual("El nombre ingresado debe contener solo letras.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad personal empresa, donde el apellido del personal
        /// es vacío
        /// </summary>
        [Test]
        public void PruebaModificarPersonalEmpresaConApellidoVacio()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ModificarPersonalEmpresa(new PersonalEmpresa(), 
                    _personalLista.ToList(), "11234567", "prueba", string.Empty, 1)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el apellido es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad personal empresa, donde el apellido del personal
        /// tiene más de 40 caracteres
        /// </summary>
        [Test]
        public void PruebaModificarPersonalEmpresaConApellidoMayorCuarentaCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ModificarPersonalEmpresa(new PersonalEmpresa(), 
                    _personalLista.ToList(),
                    "11234567", "prueba", "prueba asdasdasda sdasdasdakjfakjsdlkajsldkajsldkaj" +
                        "aksjdlaksjdlaksjdlakjdlakjlaksdjalksdjalksdjalksdjalksdjalksjd" +
                        "laksjdlakjsdlakjsdlaksjdlakjsdlahfaksdjalskdcañlskdñalskdñalksda" +
                        "lakjsdakjsdlakjsldkajsdlkajsldkajsdlkajsdlkajsdlkajsldkjasldkjaslk" +
                        "lakjsdlakjsdlakjsdlaksjd", 1)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el apellido es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad personal empresa, donde el apellido del personal
        /// no contiene solo texto
        /// </summary>
        [Test]
        public void PruebaModificarPersonalEmpresaConApellidoSinSoloTexto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ModificarPersonalEmpresa(new PersonalEmpresa(), 
                    _personalLista.ToList(), "11234567", "prueba", "prueba12 232-¿'", 1)).Message;
            Assert.AreEqual("El apellido ingresado debe contener solo letras.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida que se hayan seleccionado registros de personal para los procesos
        /// de activación o inactivación de registros, de forma correcta
        /// </summary>
        [Test]
        public void PruebaValidarPersonalEmpresaSeleccionadosCorrecto()
        {
            _servicioDominioPersonalEmpresa.ValidarPersonalEmpresaSeleccionados(1);
        }
        /// <summary>
        /// Método de prueba que valida que se hayan seleccionado registros de personal para los procesos
        /// de activación o inactivación de registros, sin seleccionar los registros
        /// </summary>
        [Test]
        public void PruebaValidarPersonalEmpresaSeleccionadosIncorrecto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ValidarPersonalEmpresaSeleccionados(0)).Message;
            Assert.AreEqual("Debe seleccionar, al menos, un personal de la lista.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida la extensión del archivo que contiene los registros de personal
        /// para el proceso de registro masivo, de forma correcta
        /// </summary>
        [Test]
        public void PruebaValidarExtensionArchivoRegistroMasivoCorrecto()
        {
            _servicioDominioPersonalEmpresa.ValidarExtensionArchivoRegistroMasivo(
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        /// <summary>
        /// Método de prueba que valida la extensión del archivo que contiene los registros de personal
        /// para el proceso de registro masivo, donde el archivo no tiene el tipo de extensión necesario
        /// </summary>
        [Test]
        public void PruebaValidarExtensionArchivoRegistroMasivoConFormatoExcelIncorrecto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ValidarExtensionArchivoRegistroMasivo(
                    "application/word")).Message;
            Assert.AreEqual("El tipo o tamaño del archivo seleccionado es " +
                "incorrecto. Solo está permitido los siguientes tipos de archivos: .xls, .xlsx; " +
                "con tamaño de hasta 100mb.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida que el archivo para el registro masivo del personal, contenga
        /// las respectivas columnas válidas, de forma correcta
        /// </summary>
        [Test]
        public void PruebaValidarColumnasArchivoRegistroMasivoCorrecto()
        {
            _servicioDominioPersonalEmpresa.ValidarColumnasArchivoRegistroMasivo(
                new List<string> {"dni", "nombres", "apellidos"});
        }
        /// <summary>
        /// Método de prueba que valida que el archivo para el registro masivo del personal, contenga
        /// las respectivas columnas válidas, donde el archivo no contiene columnas válidas
        /// </summary>
        [Test]
        public void PruebaValidarColumnasArchivoRegistroMasivoSinColumnasValidas()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ValidarColumnasArchivoRegistroMasivo(
                    new List<string> {"dni", "numeros"})).Message;
            Assert.AreEqual("El formato del archivo seleccionado " +
                "es incorrecto. El archivo debe contener el siguiente formato: dni, nombres, " +
                "apellidos.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea registros de personal según un listado de archivo excel,
        /// de forma correcta
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaDesdeListadoExcelCorrecto()
        {
            var filas = new List<Tuple<string, string, string>>();
            filas.Add(new Tuple<string, string, string>("12345677", "prueba uno", "prueba uno"));
            filas.Add(new Tuple<string, string, string>("12345678", "prueba dos", "prueba dos"));
            filas.Add(new Tuple<string, string, string>("12345679", "prueba tres", "prueba tres"));
            filas.Add(new Tuple<string, string, string>("12345680", "prueba cuatro", "prueba cuatro"));
            filas.Add(new Tuple<string, string, string>("12345681", "prueba cinco", "prueba cinco"));
            var listadoPersonal = new Tuple<string, string, string, List<Tuple<string, string, string>>>(
                "dni", "nombres", "apellidos", filas);
            var resultado = _servicioDominioPersonalEmpresa.CrearPersonalEmpresaDesdeListado(
                listadoPersonal, _personalLista.ToList());
            Assert.AreEqual(3, resultado.Count);
        }
        /// <summary>
        /// Método de prueba que crea registros de personal según un listado de archivo excel,
        /// donde se presentan validaciones durante la generación de los registros
        /// </summary>
        [Test]
        public void PruebaCrearPersonalEmpresaDesdeListadoExcelConValidaciones()
        {
            var filas = new List<Tuple<string, string, string>>();
            filas.Add(new Tuple<string, string, string>("12345677", "prueba1", "prueba uno"));
            filas.Add(new Tuple<string, string, string>("12345678", "prueba dos", "prueba dos"));
            filas.Add(new Tuple<string, string, string>("12345678", "prueba tres", "prueba tres"));
            filas.Add(new Tuple<string, string, string>("12345680", "prueba cuatro", "prueba cuatro"));
            filas.Add(new Tuple<string, string, string>("12345681", "prueba cinco", "prueba cinco"));
            var listadoPersonal = new Tuple<string, string, string, List<Tuple<string, string, string>>>(
                "dni", "nombres", "apellidos", filas);
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.CrearPersonalEmpresaDesdeListado(
                    listadoPersonal, _personalLista.ToList())).Message;
            Assert.AreEqual("<p>Los siguientes registros del archivo seleccionado han sido " +
                "detectados como incorrectos:</p><br/><hr><br/><p>Validaciones " +
                "Generales:</p>-> FILA: 1 = NOMBRES: prueba1<br/><br/><hr><br/><p>Los " +
                "registros deben tener en cuenta las siguientes validaciones:</p><p>_ El " +
                "DNI no debe estar vacío, debe tener 8 dígitos, ser de tipo numérico, y " +
                "ser valor único.</p><p>_ El nombre debe cumplir la cantidad máxima de 40 " +
                "caracteres, y contener solo letras.</p><p>_ El apellido debe cumplir la " +
                "cantidad máxima de 40 caracteres, y contener solo letras.</p>", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la relación entre un área y el personal de la empresa, de forma
        /// correcta
        /// </summary>
        [Test]
        public void PruebaCrearRelacionAreaParaElPersonalCorrecto()
        {
            var relacion = _servicioDominioPersonalEmpresa.CrearRelacionArea(new PersonalEmpresa(), 
                _sedeActiva, _areaActiva);
            Assert.IsNotNull(relacion);
            Assert.AreEqual(true, relacion.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que crea la relación entre un área y el personal de la empresa,
        /// donde la sede del área se encuentra inactiva
        /// </summary>
        [Test]
        public void PruebaCrearRelacionAreaParaElPersonalConSedeInactiva()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                    _servicioDominioPersonalEmpresa.CrearRelacionArea(new PersonalEmpresa(), _sedeInactiva,
                        _areaActiva))
                .Message;
            var mensajeEsperado = "La(s) sede(s): \"{0}\", se encuentra(n) en estado INACTIVO."
                .Replace("{0}", _sedeInactiva.DescripcionSede);
            Assert.AreEqual(mensajeEsperado, mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la relación entre un área y el personal de la empresa,
        /// donde el área se encuentra inactiva
        /// </summary>
        [Test]
        public void PruebaCrearRelacionAreaParaElPersonalConAreaInactiva()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                    _servicioDominioPersonalEmpresa.CrearRelacionArea(new PersonalEmpresa(), _sedeActiva,
                        _areaInactiva))
                .Message;
            var mensajeEsperado = "El(las) área(s) seleccionada(s): " +
                $"\"{_areaInactiva.DescripcionArea}\", se encuentra(n) en estado INACTIVO.";
            Assert.AreEqual(mensajeEsperado, mensaje);
        }
        /// <summary>
        /// Método de prueba que inactiva una relación del área asociada a un personal
        /// </summary>
        [Test]
        public void PruebaInactivarRelacionAreaDelPersonal()
        {
            var relacion = _servicioDominioPersonalEmpresa.CrearRelacionArea(new PersonalEmpresa(), 
                _sedeActiva, _areaActiva);
            relacion = _servicioDominioPersonalEmpresa.InactivarRelacionArea(relacion);
            Assert.AreEqual(false, relacion.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que inhabilita un personal de la empresa
        /// </summary>
        [Test]
        public void PruebaInhabilitarPersonalEmpresa()
        {
            var personal = _servicioDominioPersonalEmpresa.InhabilitarPersonalEmpresa(
                new PersonalEmpresa());
            Assert.AreEqual(false, personal.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que habilita un personal de la empresa
        /// </summary>
        [Test]
        public void PruebaHabilitarPersonalEmpresa()
        {
            var personal = _servicioDominioPersonalEmpresa.HabilitarPersonalEmpresa(
                new PersonalEmpresa());
            Assert.AreEqual(true, personal.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que valida a un personal para el proceso de reconocimiento,
        /// de forma correcta
        /// </summary>
        [Test]
        public void PruebaValidarPersonalDeLaEmpresaParaReconocimientoDePersonalCorrecto()
        {
            var area = new PersonalEmpresaXArea();
            var areaType = typeof(PersonalEmpresaXArea);
            areaType.GetProperty("CodigoArea").SetValue(area, 1);
            areaType.GetProperty("IndicadorEstado").SetValue(area, true);
            var areas = new List<PersonalEmpresaXArea>{area};
            var personal = new PersonalEmpresa();
            var personalType = typeof(PersonalEmpresa);
            personalType.GetProperty("AreasAsignadas").SetValue(personal, areas);
            personalType.GetProperty("IndicadorEstado").SetValue(personal, true);
            _servicioDominioPersonalEmpresa.ValidarPersonalDeLaEmpresaParaReconocimientoDePersonal(
                personal, _equipoBiometrico);
        }
        /// <summary>
        /// Método de prueba que valida a un personal para el proceso de reconocimiento,
        /// donde el personal no existe
        /// </summary>
        [Test]
        public void PruebaValidarPersonalDeLaEmpresaParaReconocimientoDePersonalSinPersonalExistente()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionEquipoBiometricoPersonalizada>(() =>
                    _servicioDominioPersonalEmpresa.ValidarPersonalDeLaEmpresaParaReconocimientoDePersonal(
                        null, null)).Message;
            Assert.AreEqual("Hubo un intento de acceso por personal no registrado en el sistema.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida a un personal para el proceso de reconocimiento,
        /// donde el personal se encuentra inactivo
        /// </summary>
        [Test]
        public void PruebaValidarPersonalDeLaEmpresaParaReconocimientoDePersonalConPersonalInactivo()
        {
            var personal = new PersonalEmpresa();
            var personalType = typeof(PersonalEmpresa);
            personalType.GetProperty("IndicadorEstado").SetValue(personal, false);
            var mensaje = Assert.Throws<ExcepcionAplicacionEquipoBiometricoPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ValidarPersonalDeLaEmpresaParaReconocimientoDePersonal(
                    personal, null)).Message;
            Assert.AreEqual("El personal se encuentra inactivo.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida a un personal para el proceso de reconocimiento,
        /// donde el personal no tiene la misma área igual al equipo biométrico
        /// </summary>
        [Test]
        public void 
            PruebaValidarPersonalDeLaEmpresaParaReconocimientoDePersonalSinAreasAsignadasSegunEquipoBiometrico()
        {
            var area = new PersonalEmpresaXArea();
            var areaType = typeof(PersonalEmpresaXArea);
            areaType.GetProperty("CodigoArea").SetValue(area, 2);
            areaType.GetProperty("IndicadorEstado").SetValue(area, true);
            var areas = new List<PersonalEmpresaXArea>{area};
            var personal = new PersonalEmpresa();
            var personalType = typeof(PersonalEmpresa);
            personalType.GetProperty("AreasAsignadas").SetValue(personal, areas);
            personalType.GetProperty("IndicadorEstado").SetValue(personal, true);
            var mensaje = Assert.Throws<ExcepcionAplicacionEquipoBiometricoPersonalizada>(() =>
                _servicioDominioPersonalEmpresa.ValidarPersonalDeLaEmpresaParaReconocimientoDePersonal(
                    personal, _equipoBiometrico)).Message;
            Assert.AreEqual("El personal no tiene accesos. Verifique el(las) área(s) " +
                "asignada(s) al personal.", mensaje);
        }
    }
}