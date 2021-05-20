using System.Collections.Generic;
using System.Linq;
using API.SRICA.Dominio.Entidad.US;
using API.SRICA.Dominio.Excepcion;
using API.SRICA.Dominio.Servicio.Implementacion;
using API.SRICA.Dominio.Servicio.Interfaz;
using NUnit.Framework;

namespace API.SRICA.Dominio.PruebaUnitaria
{
    /// <summary>
    /// Clase de prueba del servicio de dominio de usuarios
    /// </summary>
    public class ServicioDominioUsuarioPrueba
    {
        /// <summary>
        /// Servicio de dominio de usuarios
        /// </summary>
        private IServicioDominioUsuario _servicioDominioUsuario;
        /// <summary>
        /// Listado de usuarios fake
        /// </summary>
        private IList<Usuario> _usuarios;
        /// <summary>
        /// Método que inicializa las pruebas
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _servicioDominioUsuario = new ServicioDominioUsuario();
            
            var usuario = new Usuario();
            var usuarioType = typeof(Usuario);
            _usuarios = new List<Usuario>();
            usuarioType.GetProperty("CodigoUsuario").SetValue(usuario, 1);
            usuarioType.GetProperty("CorreoElectronico").SetValue(usuario, "bedregale@gmail.com");
            usuarioType.GetProperty("UsuarioAcceso").SetValue(usuario, "12345678");
            _usuarios.Add(usuario);
            usuario = new Usuario();
            usuarioType.GetProperty("CodigoUsuario").SetValue(usuario, 2);
            usuarioType.GetProperty("CorreoElectronico").SetValue(usuario, "nada");
            usuarioType.GetProperty("UsuarioAcceso").SetValue(usuario, "12345679");
            _usuarios.Add(usuario);
        }
        /// <summary>
        /// Método de prueba que valida a un usuario para el proceso de autenticación
        /// en el sistema, de forma correcta
        /// </summary>
        [Test]
        public void PruebaValidarUsuarioParaAutenticacionCorrecto()
        {
            var usuario = new Usuario();
            var usuarioType = typeof(Usuario);
            usuarioType.GetProperty("IndicadorEstado").SetValue(usuario, true);
            usuarioType.GetProperty("ContrasenaAcceso").SetValue(usuario, "1234");
            _servicioDominioUsuario.ValidarUsuarioParaAutenticacion(usuario, "1234");
        }
        /// <summary>
        /// Método de prueba que valida a un usuario para el proceso de autenticación
        /// en el sistema, donde el usuario no existe
        /// </summary>
        [Test]
        public void PruebaValidarUsuarioParaAutenticacionSinUsuarioExistente()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ValidarUsuarioParaAutenticacion(null, string.Empty)).Message;
            Assert.AreEqual("El usuario ingresado no existe.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida a un usuario para el proceso de autenticación
        /// en el sistema, donde el usuario se encuentra inactivo
        /// </summary>
        [Test]
        public void PruebaValidarUsuarioParaAutenticacionConUsuarioInactivo()
        {
            var usuario = new Usuario();
            var usuarioType = typeof(Usuario);
            usuarioType.GetProperty("IndicadorEstado").SetValue(usuario, false);
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ValidarUsuarioParaAutenticacion(usuario, string.Empty)).Message;
            Assert.AreEqual("El usuario no está habilitado en el sistema.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida a un usuario para el proceso de autenticación
        /// en el sistema, donde la contraseña ingresada no es igual a la base de datos
        /// </summary>
        [Test]
        public void PruebaValidarUsuarioParaAutenticacionConContrasenaIncorrecta()
        {
            var usuario = new Usuario();
            var usuarioType = typeof(Usuario);
            usuarioType.GetProperty("IndicadorEstado").SetValue(usuario, true);
            usuarioType.GetProperty("ContrasenaAcceso").SetValue(usuario, "1234");
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ValidarUsuarioParaAutenticacion(usuario, "123")).Message;
            Assert.AreEqual("La contraseña es incorrecta.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida un usuario para el proceso de recuperación de contraseña,
        /// de forma correcta
        /// </summary>
        [Test]
        public void PruebaValidarUsuarioParaRecuperacionDeContrasenaOlvidadaCorrecto()
        {
            var rol = new RolUsuario();
            var rolType = typeof(RolUsuario);
            rolType.GetProperty("CodigoRolUsuario").SetValue(rol, RolUsuario.Administrador);
            var usuario = new Usuario();
            var usuarioType = typeof(Usuario);
            usuarioType.GetProperty("RolUsuario").SetValue(usuario, rol);
            usuarioType.GetProperty("IndicadorEstado").SetValue(usuario, true);
            _servicioDominioUsuario.ValidarUsuarioParaRecuperacionDeContrasenaOlvidada(usuario);
        }
        /// <summary>
        /// Método de prueba que valida un usuario para el proceso de recuperación de contraseña,
        /// sin usuario existente
        /// </summary>
        [Test]
        public void PruebaValidarUsuarioParaRecuperacionDeContrasenaOlvidadaSinUsuarioExistente()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ValidarUsuarioParaRecuperacionDeContrasenaOlvidada(null)).Message;
            Assert.AreEqual("El usuario ingresado no existe.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida un usuario para el proceso de recuperación de contraseña,
        /// donde el usuario no tiene el rol de administrador
        /// </summary>
        [Test]
        public void PruebaValidarUsuarioParaRecuperacionDeContrasenaOlvidadaSinUsuarioAdministrador()
        {
            var rol = new RolUsuario();
            var rolType = typeof(RolUsuario);
            rolType.GetProperty("CodigoRolUsuario").SetValue(rol, RolUsuario.UsuarioBasico);
            var usuario = new Usuario();
            var usuarioType = typeof(Usuario);
            usuarioType.GetProperty("RolUsuario").SetValue(usuario, rol);
            usuarioType.GetProperty("IndicadorEstado").SetValue(usuario, true);
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ValidarUsuarioParaRecuperacionDeContrasenaOlvidada(usuario)).Message;
            Assert.AreEqual("El usuario ingresado no tiene el rol ADMINISTRADOR. " +
                "Si desea recuperar su contraseña, contáctese con el administrador del sistema.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida un usuario para el proceso de recuperación de contraseña,
        /// donde el usuario no tiene el estado activo
        /// </summary>
        [Test]
        public void PruebaValidarUsuarioParaRecuperacionDeContrasenaOlvidadaSinUsuarioActivo()
        {
            var rol = new RolUsuario();
            var rolType = typeof(RolUsuario);
            rolType.GetProperty("CodigoRolUsuario").SetValue(rol, RolUsuario.Administrador);
            var usuario = new Usuario();
            var usuarioType = typeof(Usuario);
            usuarioType.GetProperty("RolUsuario").SetValue(usuario, rol);
            usuarioType.GetProperty("IndicadorEstado").SetValue(usuario, false);
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ValidarUsuarioParaRecuperacionDeContrasenaOlvidada(usuario)).Message;
            Assert.AreEqual("El usuario no está habilitado en el sistema.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida que un usuario esté habilitado en el sistema antes
        /// de realizar alguna acción en ella, de forma correcta
        /// </summary>
        [Test]
        public void PruebaValidarUsuarioDeAccionHabilitadoCorrecto()
        {
            var usuario = new Usuario();
            var usuarioType = typeof(Usuario);
            usuarioType.GetProperty("IndicadorEstado").SetValue(usuario, true);
            _servicioDominioUsuario.ValidarUsuarioDeAccionHabilitado(usuario);
        }
        /// <summary>
        /// Método de prueba que valida que un usuario esté habilitado en el sistema antes
        /// de realizar alguna acción en ella, donde el usuario tiene el estado inactivo
        /// </summary>
        [Test]
        public void PruebaValidarUsuarioDeAccionHabilitadoIncorrecto()
        {
            var usuario = new Usuario();
            var usuarioType = typeof(Usuario);
            usuarioType.GetProperty("IndicadorEstado").SetValue(usuario, false);
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ValidarUsuarioDeAccionHabilitado(usuario)).Message;
            Assert.AreEqual("Su usuario ha sido inhabilitado en el sistema.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica el correo y contraseña de un usuario, donde solo
        /// se modifica el correo y contraseña del usuario
        /// </summary>
        [Test]
        public void PruebaModificarCorreoYContrasenaDelUsuarioConCambiosEnCorreoYContrasena()
        {
            var usuario = _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(
                new Usuario(), "bedregale@gmail.com", "123.-SRICa", "123.-SRICa",
                "123.-SRICa", false, false);
            Assert.IsNotNull(usuario);
            Assert.AreEqual("123.-SRICa", usuario.ContrasenaAcceso);
        }
        /// <summary>
        /// Método de prueba que modifica el correo y contraseña de un usuario, donde solo
        /// se modifica el correo y contraseña del usuario con validaciones
        /// </summary>
        [Test]
        public void PruebaModificarCorreoYContrasenaDelUsuarioConCambiosEnCorreoYContrasenaConValidaciones()
        {
            var mensajeUno = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(new Usuario(),
                    "asdasd", string.Empty, string.Empty, string.Empty,
                    false, false)).Message;
            Assert.AreEqual("El correo electrónico no tiene el formato adecuado.", mensajeUno);

            var mensajeDos = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(new Usuario(),
                    "asdasfalksdjkjalkjalksjdlakjsdlakjsdlakjsdlakjsdlk2314alkjlakjdkaj" +
                    "aksdjakjflaksdjfaksjdajsdlakcjsdiajcsdahcsdhakfjhsdkfjahsdlkajlfkajsdladsasdasd" +
                    "asdkajlsdkjalskdjalksdjalkjdclakwjdclakdjlakwjdclakdjwclawkjdcalwkdcjalwkdcjawldckjaw" +
                    "añsdkjalkdalkjdlaksjdalksdjalksjdlakdjalksdjalksdjalksdjalksdjalkdjalskdjalskdjal" +
                    "askdjaldjalkdjcawdahwudhawudhawudhakwjdhkajsdkajnsdkajdnkajwdkajsndkajwdkajwndkaj" +
                    "alkjsdaljsdalkjdlakjsdlcakjsdclkajskd@gmail.com",
                    string.Empty, string.Empty, string.Empty, false, false)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el correo " +
                "electrónico es de 64 caracteres.", mensajeDos);

            var mensajeTres = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(new Usuario(),
                    "bedregale@gmail.com", "SRICa.-123", "SRICa.-1223",
                    "SRICa.-123", false, false)).Message;
            Assert.AreEqual("Las contraseñas no coinciden.", mensajeTres);
            
            var mensajeCuatro = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(new Usuario(),
                    "bedregale@gmail.com", "123", "123",
                    "123", false, false)).Message;
            Assert.AreEqual("La cantidad de caracteres que debe poseer la nueva " +
                "contraseña debe estar en el rango de 8 a 30 caracteres.", mensajeCuatro);
            
            var mensajeCinco = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(new Usuario(),
                    "bedregale@gmail.com", "123234234eqweqweqweqweqwe12e2q2eq2eawe2ea", 
                    "123234234eqweqweqweqweqwe12e2q2eq2eawe2ea", "123234234eqweqweqweqweqwe12e2q2eq2eawe2ea", 
                    false, false)).Message;
            Assert.AreEqual("La cantidad de caracteres que debe poseer la nueva " +
                "contraseña debe estar en el rango de 8 a 30 caracteres.", mensajeCinco);
            
            var mensajeSeis = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(new Usuario(),
                    "bedregale@gmail.com", "asdadwa1234", "asdadwa1234", "asdadwa1234", 
                    false, false)).Message;
            Assert.AreEqual("El nivel de fortaleza de la nueva contraseña " +
                "es: MEDIO BAJO. La nueva contraseña debe estar compuesto por " +
                "letras mayúsculas y minúsculas, números y caracteres especiales, presentando " +
                "un nivel de fortaleza Medio-Alto o Alto.", mensajeSeis);
        }
        /// <summary>
        /// Método de prueba que modifica el correo y contraseña de un usuario, donde solo
        /// se modifica la contraseña
        /// </summary>
        [Test]
        public void PruebaModificarCorreoYContrasenaDelUsuarioConCambiosSoloEnContrasena()
        {
            var usuario = _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(
                new Usuario(), string.Empty, "123.-SRICa", "123.-SRICa",
                "123.-SRICa", true, false);
            Assert.IsNotNull(usuario);
            Assert.AreEqual("123.-SRICa", usuario.ContrasenaAcceso);
        }
        /// <summary>
        /// Método de prueba que modifica el correo y contraseña de un usuario, donde solo
        /// se modifica la contraseña con validaciones
        /// </summary>
        [Test]
        public void PruebaModificarCorreoYContrasenaDelUsuarioConCambiosSoloEnContrasenaConValidaciones()
        {
            var mensajeUno = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(new Usuario(),
                    string.Empty, "SRICa.-123", "SRICa.-1223",
                    "SRICa.-123", true, false)).Message;
            Assert.AreEqual("Las contraseñas no coinciden.", mensajeUno);
            
            var mensajeDos = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(new Usuario(),
                    string.Empty, "123", "123",
                    "123", true, false)).Message;
            Assert.AreEqual("La cantidad de caracteres que debe poseer la nueva " +
                "contraseña debe estar en el rango de 8 a 30 caracteres.", mensajeDos);
            
            var mensajeTres = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(new Usuario(),
                    string.Empty, "123234234eqweqweqweqweqwe12e2q2eq2eawe2ea", 
                    "123234234eqweqweqweqweqwe12e2q2eq2eawe2ea", "123234234eqweqweqweqweqwe12e2q2eq2eawe2ea", 
                    true, false)).Message;
            Assert.AreEqual("La cantidad de caracteres que debe poseer la nueva " +
                "contraseña debe estar en el rango de 8 a 30 caracteres.", mensajeTres);
            
            var mensajeCuatro = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(new Usuario(),
                    string.Empty, "asdadwa1234", "asdadwa1234", "asdadwa1234", 
                    true, false)).Message;
            Assert.AreEqual("El nivel de fortaleza de la nueva contraseña " +
                "es: MEDIO BAJO. La nueva contraseña debe estar compuesto por " +
                "letras mayúsculas y minúsculas, números y caracteres especiales, presentando " +
                "un nivel de fortaleza Medio-Alto o Alto.", mensajeCuatro);
        }
        /// <summary>
        /// Método de prueba que modifica el correo y contraseña de un usuario, donde solo
        /// se modifica el correo
        /// </summary>
        [Test]
        public void PruebaModificarCorreoYContrasenaDelUsuarioConCambiosSoloEnCorreo()
        {
            var usuario = _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(
                new Usuario(), "bedregale@gmail.com", string.Empty, string.Empty, 
                string.Empty, false, true);
            Assert.IsNotNull(usuario);
            Assert.AreEqual("bedregale@gmail.com", usuario.CorreoElectronico);
        }
        /// <summary>
        /// Método de prueba que modifica el correo y contraseña de un usuario, donde solo
        /// se modifica el correo con validaciones
        /// </summary>
        [Test]
        public void PruebaModificarCorreoYContrasenaDelUsuarioConCambiosSoloEnCorreoConValidaciones()
        {
            var mensajeUno = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(new Usuario(),
                    "asdasd", string.Empty, string.Empty, string.Empty,
                    false, true)).Message;
            Assert.AreEqual("El correo electrónico no tiene el formato adecuado.", mensajeUno);

            var mensajeDos = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(new Usuario(),
                    "asdasfalksdjkjalkjalksjdlakjsdlakjsdlakjsdlakjsdlk2314alkjlakjdkaj" +
                    "aksdjakjflaksdjfaksjdajsdlakcjsdiajcsdahcsdhakfjhsdkfjahsdlkajlfkajsdladsasdasd" +
                    "asdkajlsdkjalskdjalksdjalkjdclakwjdclakdjlakwjdclakdjwclawkjdcalwkdcjalwkdcjawldckjaw" +
                    "añsdkjalkdalkjdlaksjdalksdjalksjdlakdjalksdjalksdjalksdjalksdjalkdjalskdjalskdjal" +
                    "askdjaldjalkdjcawdahwudhawudhawudhakwjdhkajsdkajnsdkajdnkajwdkajsndkajwdkajwndkaj" +
                    "alkjsdaljsdalkjdlakjsdlcakjsdclkajskd@gmail.com",
                    string.Empty, string.Empty, string.Empty, false, true)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el correo " +
                "electrónico es de 64 caracteres.", mensajeDos);
        }
        /// <summary>
        /// Método de prueba que crea la entidad usuario, de forma correcta
        /// </summary>
        [Test]
        public void PruebaCrearUsuarioCorrecto()
        {
            var usuario = _servicioDominioUsuario.CrearUsuario(_usuarios.ToList(), new RolUsuario(), 
                "11234567", "1234567", "prueba", "prueba");
            Assert.IsNotNull(usuario);
            Assert.AreEqual("11234567", usuario.UsuarioAcceso);
        }
        /// <summary>
        /// Método de prueba que crea la entidad usuario, donde el usuario no contiene solo números
        /// </summary>
        [Test]
        public void PruebaCrearUsuarioConUsuarioSinSoloNumeros()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.CrearUsuario(_usuarios.ToList(), new RolUsuario(),
                    "ewe2", "1234567", "prueba", "prueba")).Message;
            Assert.AreEqual("El usuario debe ser de tipo numérico.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad usuario, donde el usuario no contiene 8 dígitos
        /// </summary>
        [Test]
        public void PruebaCrearUsuarioConUsuarioNoIgualAOchoCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.CrearUsuario(_usuarios.ToList(), new RolUsuario(),
                    "1234", "1234567", "prueba", "prueba")).Message;
            Assert.AreEqual("La cantidad de dígitos del usuario debe ser 8 dígitos.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad usuario, donde el usuario ya existe
        /// </summary>
        [Test]
        public void PruebaCrearUsuarioConUsuarioExistente()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.CrearUsuario(_usuarios.ToList(), new RolUsuario(),
                    "12345678", "1234567", "prueba", "prueba")).Message;
            Assert.AreEqual("El usuario ingresado ya existe. Verifique el listado de " +
                "usuarios en estado activo o inactivo para encontrar el usuario duplicado.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad usuario, donde el nombre de usuario es vacío
        /// </summary>
        [Test]
        public void PruebaCrearUsuarioConNombreVacio()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.CrearUsuario(_usuarios.ToList(), new RolUsuario(),
                    "11234567", "1234567", string.Empty, "prueba")).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el nombre " +
                "es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad usuario, donde el nombre de usuario es mayor
        /// a 40 caracteres
        /// </summary>
        [Test]
        public void PruebaCrearUsuarioConNombreMayorCuarentaCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.CrearUsuario(_usuarios.ToList(), new RolUsuario(),
                    "11234567", "1234567", 
                    "asdasdasdaksdaksdakjs kajs dlaksjdalksdjalksdjalkjsdalsdkja" +
                    "aksjdaksdjlaksjdlaksjdlakjsdalksdjalksjdalksjfalksdjalksdj alksdjalksdj" +
                    "lajsakjslakjsdlakjdlakjsdlaksj dalksdj aksdjalksdjlakjsdkajsd" +
                    "ñaksjdlaksjflakjdalkjdwidjaliwdjalksdjiwdjalksdjaliwdjalsdjaliwdj", "prueba")).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el nombre " +
                            "es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad usuario, donde el nombre del usuario no es solo texto
        /// </summary>
        [Test]
        public void PruebaCrearUsuarioConNombreSinSoloTexto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.CrearUsuario(_usuarios.ToList(), new RolUsuario(),
                    "11234567", "1234567", 
                    "asdasdasdaksdaksdakjs kajs d131--.-*", "prueba")).Message;
            Assert.AreEqual("El nombre ingresado debe contener solo letras.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad usuario, donde el apellido del usuario es vacío
        /// </summary>
        [Test]
        public void PruebaCrearUsuarioConApellidoVacio()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.CrearUsuario(_usuarios.ToList(), new RolUsuario(),
                    "11234567", "1234567", "prueba", string.Empty)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el apellido " +
                "es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad usuario, donde el apellido del usuario es de
        /// más de 40 caracteres
        /// </summary>
        [Test]
        public void PruebaCrearUsuarioConApellidoMayorCuarentaCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.CrearUsuario(_usuarios.ToList(), new RolUsuario(),
                    "11234567", "1234567", 
                    "prueba", "asdasdasdaksdaksdakjs kajs dlaksjdalksdjalksdjalkjsdalsdkja" +
                              "aksjdaksdjlaksjdlaksjdlakjsdalksdjalksjdalksjfalksdjalksdj alksdjalksdj" +
                              "lajsakjslakjsdlakjdlakjsdlaksj dalksdj aksdjalksdjlakjsdkajsd" +
                              "ñaksjdlaksjflakjdalkjdwidjaliwdjalksdjiwdjalksdjaliwdjalsdjaliwdj")).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el apellido " +
                "es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que crea la entidad usuario, donde el apellido del usuario no
        /// es solo texto
        /// </summary>
        [Test]
        public void PruebaCrearUsuarioConApellidoSinSoloTexto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.CrearUsuario(_usuarios.ToList(), new RolUsuario(),
                    "11234567", "1234567", 
                    "prueba", "asdasdasdaksdaksdakjs kajs d131--.-*")).Message;
            Assert.AreEqual("El apellido ingresado debe contener solo letras.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad usuario, de forma correcta
        /// </summary>
        [Test]
        public void PruebaModificarUsuarioCorrecto()
        {
            var usuario = _servicioDominioUsuario.ModificarUsuario(_usuarios.ToList(), new Usuario(), 
                new RolUsuario(), "11234567", "1234567", "prueba", "prueba",
                false);
            Assert.IsNotNull(usuario);
            Assert.AreEqual("11234567", usuario.UsuarioAcceso);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad usuario, de forma correcta con contraseña por defecto
        /// </summary>
        [Test]
        public void PruebaModificarUsuarioConContrasenaPorDefectoCorrecto()
        {
            var usuario = _servicioDominioUsuario.ModificarUsuario(_usuarios.ToList(), new Usuario(), 
                new RolUsuario(), "11234567", "1234567", "prueba", "prueba",
                true);
            Assert.IsNotNull(usuario);
            Assert.AreEqual("1234567", usuario.ContrasenaAcceso);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad usuario, donde el usuario no contiene solo números
        /// </summary>
        [Test]
        public void PruebaModificarUsuarioConUsuarioSinSoloNumeros()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarUsuario(_usuarios.ToList(), new Usuario(), 
                    new RolUsuario(), "ewe2", "1234567", "prueba", "prueba", false))
                .Message;
            Assert.AreEqual("El usuario debe ser de tipo numérico.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad usuario, donde el usuario no contiene 8 dígitos
        /// </summary>
        [Test]
        public void PruebaModificarUsuarioConUsuarioNoIgualAOchoCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarUsuario(_usuarios.ToList(), new Usuario(), 
                    new RolUsuario(), "1234", "1234567", "prueba", "prueba", false))
                .Message;
            Assert.AreEqual("La cantidad de dígitos del usuario debe ser 8 dígitos.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad usuario, donde el usuario ya existe
        /// </summary>
        [Test]
        public void PruebaModificarUsuarioConUsuarioExistente()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarUsuario(_usuarios.ToList(), new Usuario(), 
                    new RolUsuario(), "12345678", "1234567", "prueba", "prueba", false))
                .Message;
            Assert.AreEqual("El usuario ingresado ya existe. Verifique el listado de " +
                "usuarios en estado activo o inactivo para encontrar el usuario duplicado.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad usuario, donde el nombre de usuario es vacío
        /// </summary>
        [Test]
        public void PruebaModificarUsuarioConNombreVacio()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarUsuario(_usuarios.ToList(), new Usuario(), 
                    new RolUsuario(), "11234567", "1234567", string.Empty, "prueba", false))
                .Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el nombre " +
                "es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad usuario, donde el nombre de usuario es mayor
        /// a 40 caracteres
        /// </summary>
        [Test]
        public void PruebaModificarUsuarioConNombreMayorCuarentaCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarUsuario(_usuarios.ToList(), new Usuario(), 
                    new RolUsuario(), "11234567", "1234567", 
                    "asdasdasdaksdaksdakjs kajs dlaksjdalksdjalksdjalkjsdalsdkja" +
                    "aksjdaksdjlaksjdlaksjdlakjsdalksdjalksjdalksjfalksdjalksdj alksdjalksdj" +
                    "lajsakjslakjsdlakjdlakjsdlaksj dalksdj aksdjalksdjlakjsdkajsd" +
                    "ñaksjdlaksjflakjdalkjdwidjaliwdjalksdjiwdjalksdjaliwdjalsdjaliwdj", "prueba", false))
                .Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el nombre " +
                "es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad usuario, donde el nombre del usuario no es solo texto
        /// </summary>
        [Test]
        public void PruebaModificarUsuarioConNombreSinSoloTexto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarUsuario(_usuarios.ToList(), new Usuario(), 
                    new RolUsuario(), "11234567", "1234567", 
                    "asdasdasdaksdaksdakjs kajs d131--.-*", "prueba", false)).Message;
            Assert.AreEqual("El nombre ingresado debe contener solo letras.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad usuario, donde el apellido del usuario es vacío
        /// </summary>
        [Test]
        public void PruebaModificarUsuarioConApellidoVacio()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarUsuario(_usuarios.ToList(),new Usuario(),  
                    new RolUsuario(), "11234567", "1234567", "prueba", string.Empty, false))
                .Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el apellido " +
                "es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad usuario, donde el apellido del usuario es de
        /// más de 40 caracteres
        /// </summary>
        [Test]
        public void PruebaModificarUsuarioConApellidoMayorCuarentaCaracteres()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarUsuario(_usuarios.ToList(), new Usuario(), 
                    new RolUsuario(), "11234567", "1234567", 
                    "prueba", "asdasdasdaksdaksdakjs kajs dlaksjdalksdjalksdjalkjsdalsdkja" +
                              "aksjdaksdjlaksjdlaksjdlakjsdalksdjalksjdalksjfalksdjalksdj alksdjalksdj" +
                              "lajsakjslakjsdlakjdlakjsdlaksj dalksdj aksdjalksdjlakjsdkajsd" +
                              "ñaksjdlaksjflakjdalkjdwidjaliwdjalksdjiwdjalksdjaliwdjalsdjaliwdj", 
                    false)).Message;
            Assert.AreEqual("La cantidad máxima de caracteres para el apellido " +
                "es de 40 caracteres.", mensaje);
        }
        /// <summary>
        /// Método de prueba que modifica la entidad usuario, donde el apellido del usuario no
        /// es solo texto
        /// </summary>
        [Test]
        public void PruebaModificarUsuarioConApellidoSinSoloTexto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ModificarUsuario(_usuarios.ToList(), new Usuario(), 
                    new RolUsuario(), "11234567", "1234567", 
                    "prueba", "asdasdasdaksdaksdakjs kajs d131--.-*", false)).Message;
            Assert.AreEqual("El apellido ingresado debe contener solo letras.", mensaje);
        }
        /// <summary>
        /// Método de prueba que valida el listado de usuarios seleccionados para los procesos
        /// de activación o inactivación, de forma correcta
        /// </summary>
        [Test]
        public void PruebaValidarUsuariosSeleccionadosCorrecto()
        {
            _servicioDominioUsuario.ValidarUsuariosSeleccionados(1);
        }
        /// <summary>
        /// Método de prueba que valida el listado de usuarios seleccionados para los procesos
        /// de activación o inactivación, donde no se han seleccionado registros
        /// </summary>
        [Test]
        public void PruebaValidarUsuariosSeleccionadosIncorrecto()
        {
            var mensaje = Assert.Throws<ExcepcionAplicacionPersonalizada>(() =>
                _servicioDominioUsuario.ValidarUsuariosSeleccionados(0)).Message;
            Assert.AreEqual("Debe seleccionar, al menos, un usuario de la lista.", mensaje);
        }
        /// <summary>
        /// Método de prueba que inhabilita un usuario
        /// </summary>
        [Test]
        public void PruebaInhabilitarUsuario()
        {
            var usuario = _servicioDominioUsuario.InhabilitarUsuario(new Usuario());
            Assert.AreEqual(false, usuario.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que habilita un usuario
        /// </summary>
        [Test]
        public void PruebaHabilitarUsuario()
        {
            var usuario = _servicioDominioUsuario.HabilitarUsuario(new Usuario());
            Assert.AreEqual(true, usuario.IndicadorEstado);
        }
        /// <summary>
        /// Método de prueba que realiza un filtrado a una lista de usuarios solo
        /// con correos válidos
        /// </summary>
        [Test]
        public void PruebaFiltrarUsuariosConCorreosValidos()
        {
            var cantidadRegistrosEsperado = 1;
            var resultado = _servicioDominioUsuario.FiltrarUsuariosConCorreosValidos(_usuarios.ToList());
            Assert.AreEqual(cantidadRegistrosEsperado, resultado.Count);
        }
    }
}