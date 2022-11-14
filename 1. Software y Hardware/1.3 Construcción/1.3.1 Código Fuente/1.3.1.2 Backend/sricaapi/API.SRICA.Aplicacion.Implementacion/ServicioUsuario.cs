using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.BT;
using API.SRICA.Dominio.Entidad.SI;
using API.SRICA.Dominio.Entidad.US;
using API.SRICA.Dominio.Interfaz;
using API.SRICA.Dominio.Servicio.Interfaz;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación del servicio de consultas y operaciones de usuarios
    /// </summary>
    public class ServicioUsuario : IServicioUsuario
    {
        /// <summary>
        /// Servicio para la encriptación de datos
        /// </summary>
        private readonly IServicioEncriptador _servicioEncriptador;
        /// <summary>
        /// Servicio para la desencriptación de datos
        /// </summary>
        private readonly IServicioDesencriptador _servicioDesencriptador;
        /// <summary>
        /// Repositorio de consultas a la base de datos
        /// </summary>
        private readonly IRepositorioConsulta _repositorioConsulta;
        /// <summary>
        /// Repositorio de operaciones a la base de datos
        /// </summary>
        private readonly IRepositorioOperacion _repositorioOperacion;
        /// <summary>
        /// Servicio de dominio del usuario
        /// </summary>
        private readonly IServicioDominioUsuario _servicioDominioUsuario;
        /// <summary>
        /// Servicio de dominio de bitácora de acción del sistema
        /// </summary>
        private readonly IServicioDominioBitacoraAccionSistema _servicioDominioBitacoraAccionSistema;
        /// <summary>
        /// Servicio de mapeo del usuario a DTO
        /// </summary>
        private readonly IServicioMapeoUsuarioADto _servicioMapeoUsuarioADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioDesencriptador">Servicio para la desencriptación de datos</param>
        /// <param name="repositorioConsulta">Repositorio de consultas a la base de datos</param>
        /// <param name="repositorioOperacion">Repositorio de operaciones a la base de datos</param>
        /// <param name="servicioDominioUsuario">Servicio de dominio del usuario</param>
        /// <param name="servicioDominioBitacoraAccionSistema">Servicio de dominio de bitácora 
        /// de acción del sistema</param>
        /// <param name="servicioMapeoUsuarioADTO">Servicio de mapeo del usuario a DTO</param>
        public ServicioUsuario(IServicioEncriptador servicioEncriptador, 
            IServicioDesencriptador servicioDesencriptador, IRepositorioConsulta repositorioConsulta, 
            IRepositorioOperacion repositorioOperacion, IServicioDominioUsuario servicioDominioUsuario,
            IServicioDominioBitacoraAccionSistema servicioDominioBitacoraAccionSistema,
            IServicioMapeoUsuarioADto servicioMapeoUsuarioADTO)
        {
            _servicioEncriptador = servicioEncriptador;
            _servicioDesencriptador = servicioDesencriptador;
            _repositorioConsulta = repositorioConsulta;
            _repositorioOperacion = repositorioOperacion;
            _servicioDominioUsuario = servicioDominioUsuario;
            _servicioDominioBitacoraAccionSistema = servicioDominioBitacoraAccionSistema;
            _servicioMapeoUsuarioADTO = servicioMapeoUsuarioADTO;
        }
        /// <summary>
        /// Método que modifica los datos del usuario. Las operaciones que se pueden realizar son:
        /// Cambiar datos por defecto, actualizar contraseña, modificar perfil de usuario,
        /// modificar datos del usuario
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los valores a modificar</param>
        /// <returns>Resultado encriptado con los datos del usuario y sus valores modificados</returns>
        public string ModificarUsuario(JToken encriptado)
        {
            var usuarioDTO = _servicioDesencriptador.Desencriptar<DtoUsuario>(encriptado.ToString());
            if (usuarioDTO.TipoOperacion == DtoUsuario.EnumTipoOperacion.CambiarDatosPorDefecto)
                return ModificarCorreoElectronicoYContrasenaDelUsuario(usuarioDTO,
                    usuarioDTO.NoValidarCorreoElectronico, false);
            if (usuarioDTO.TipoOperacion == DtoUsuario.EnumTipoOperacion.ActualizarContrasena)
                return ModificarCorreoElectronicoYContrasenaDelUsuario(usuarioDTO,
                    true, false);
            if (usuarioDTO.TipoOperacion == DtoUsuario.EnumTipoOperacion.ModificarPerfilUsuario)
                return ModificarCorreoElectronicoYContrasenaDelUsuario(usuarioDTO,
                    false, string.IsNullOrEmpty(usuarioDTO.Contrasena));
            if (usuarioDTO.TipoOperacion == DtoUsuario.EnumTipoOperacion.ModificarUsuario)
                return ModificarDatosDelUsuario(usuarioDTO);
            return string.Empty;
        }
        /// <summary>
        /// Método que obtiene un usuario en base a su código de usuario
        /// </summary>
        /// <param name="codigoUsuarioEncriptado">Código del usuario a obtener (encriptado)</param>
        /// <returns>Resultado encriptado con los datos del usuario encontrado</returns>
        public string ObtenerUsuario(string codigoUsuarioEncriptado)
        {
            var codigoUsuario = _servicioDesencriptador.Desencriptar<int>(
                codigoUsuarioEncriptado);
            return ObtenerUsuarioDTOEncriptado(codigoUsuario);
        }
        /// <summary>
        /// Método que obtiene el listado de usuarios, tanto activos como inactivos
        /// </summary>
        /// <returns>Resultado encriptado con el listado de usuarios</returns>
        public string ObtenerListadoDeUsuarios()
        {
            return ObtenerUsuariosDTOEncriptados();
        }
        /// <summary>
        /// Método que guarda un nuevo usuario
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del usuario a guardar</param>
        /// <returns>Resultado encriptado con los datos del usuario guardado</returns>
        public string GuardarUsuario(JToken encriptado)
        {
            var usuarioDTO = _servicioDesencriptador.Desencriptar<DtoUsuario>(encriptado.ToString());
            var rolUsuario = _repositorioConsulta.ObtenerPorCodigo<RolUsuario>(
                _servicioDesencriptador.Desencriptar<int>(usuarioDTO.CodigoRolUsuario));
            var usuariosRegistrados = _repositorioConsulta.ObtenerPorExpresionLimite<Usuario>().ToList();
            var contrasenaPorDefectoEncriptada = _servicioEncriptador.Encriptar(
                Usuario.ContrasenaPorDefecto);
            var usuario = _servicioDominioUsuario.CrearUsuario(usuariosRegistrados, rolUsuario, 
                usuarioDTO.Usuario, contrasenaPorDefectoEncriptada, usuarioDTO.Nombre, usuarioDTO.Apellido);
            _repositorioOperacion.Agregar(usuario);
            _repositorioOperacion.GuardarCambios();
            return ObtenerUsuarioDTOEncriptado(usuario.CodigoUsuario);
        }
        /// <summary>
        /// Método que inhabilita a los usuarios
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de usuarios a 
        /// inhabilitar</param>
        /// <returns>Resultado encriptado con el listado de usuarios inhabilitados</returns>
        public string InhabilitarUsuarios(JToken encriptado)
        {
            var usuariosDTO = _servicioDesencriptador.Desencriptar<List<DtoUsuario>>(encriptado.ToString());
            _servicioDominioUsuario.ValidarUsuariosSeleccionados(usuariosDTO.Count);
            InhabilitarListadoDeUsuarios(usuariosDTO);
            _repositorioOperacion.GuardarCambios();
            return ObtenerUsuariosDTOEncriptados(true, false);
        }
        /// <summary>
        /// Método que habilita a los usuarios
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de usuarios a 
        /// habilitar</param>
        /// <returns>Resultado encriptado con el listado de usuarios habilitados</returns>
        public string HabilitarUsuarios(JToken encriptado)
        {
            var usuariosDTO = _servicioDesencriptador.Desencriptar<List<DtoUsuario>>(encriptado.ToString());
            _servicioDominioUsuario.ValidarUsuariosSeleccionados(usuariosDTO.Count);
            HabilitarListadoDeUsuarios(usuariosDTO);
            _repositorioOperacion.GuardarCambios();
            return ObtenerUsuariosDTOEncriptados(true);
        }
        /// <summary>
        /// Método que verifica que el usuario autenticado (claims token) se encuentre en estado
        /// activo
        /// </summary>
        /// <param name="usuarioToken">Usuario obtenido desde claims token</param>
        public void VerificarUsuarioDelTokenActivo(Claim usuarioToken)
        {
            if (usuarioToken != null && !string.IsNullOrEmpty(usuarioToken.Value))
            {
                var usuario = _repositorioConsulta.ObtenerPorCodigo<Usuario>(
                    Convert.ToInt32(usuarioToken.Value));
                _servicioDominioUsuario.ValidarUsuarioDeAccionHabilitado(usuario);
            }
        }
        #region Métodos privados
        /// <summary>
        /// Método que modifica el correo electrónico y contraseña del usuario
        /// </summary>
        /// <param name="usuarioDTO">Datos del usuario a modificar</param>
        /// <param name="noValidarCorreoElectronico">Si no se desea validar el correo electrónico</param>
        /// <param name="noValidarContrasena">Si no se desea validar la contraseña</param>
        /// <returns>Resultado encriptado con los datos del usuario y sus valores modificados</returns>
        private string ModificarCorreoElectronicoYContrasenaDelUsuario(DtoUsuario usuarioDTO, 
            bool noValidarCorreoElectronico, bool noValidarContrasena)
        {
            var codigoUsuario = _servicioDesencriptador.Desencriptar<int>(usuarioDTO.CodigoUsuario);
            var usuario = _repositorioConsulta.ObtenerPorCodigo<Usuario>(codigoUsuario);
            var contrasenaEncriptada = _servicioEncriptador.Encriptar(usuarioDTO.Contrasena);
            usuario = _servicioDominioUsuario.ModificarCorreoYContrasenaDelUsuario(usuario,
                usuarioDTO.CorreoElectronico, usuarioDTO.Contrasena, usuarioDTO.ConfirmarContrasena,
                contrasenaEncriptada, noValidarCorreoElectronico, noValidarContrasena);
            _repositorioOperacion.Modificar(usuario);
            _repositorioOperacion.GuardarCambios();
            return ObtenerUsuarioDTOEncriptado(codigoUsuario);
        }
        /// <summary>
        /// Método que modifica los datos de un usuario (usuario, nombre, apellido, rol de usuario, 
        /// contraseña)
        /// </summary>
        /// <param name="usuarioDTO">DTO con los datos del usuario a modificar</param>
        /// <returns>Resultado encriptado con los datos del usuario y sus valores modificados</returns>
        private string ModificarDatosDelUsuario(DtoUsuario usuarioDTO)
        {
            var usuario = _repositorioConsulta.ObtenerPorCodigo<Usuario>(
                _servicioDesencriptador.Desencriptar<int>(usuarioDTO.CodigoUsuario));
            var rolUsuario = _repositorioConsulta.ObtenerPorCodigo<RolUsuario>(
                _servicioDesencriptador.Desencriptar<int>(usuarioDTO.CodigoRolUsuario));
            var usuariosRegistrados = _repositorioConsulta.ObtenerPorExpresionLimite<Usuario>().ToList();
            var contrasenaPorDefectoEncriptada = _servicioEncriptador.Encriptar(
                Usuario.ContrasenaPorDefecto);
            usuario = _servicioDominioUsuario.ModificarUsuario(usuariosRegistrados, usuario,
                rolUsuario, usuarioDTO.Usuario, contrasenaPorDefectoEncriptada, usuarioDTO.Nombre, 
                usuarioDTO.Apellido, usuarioDTO.EsContrasenaPorDefecto);
            _repositorioOperacion.Modificar(usuario);
            _repositorioOperacion.GuardarCambios();
            return ObtenerUsuarioDTOEncriptado(usuario.CodigoUsuario);
        }
        /// <summary>
        /// Método que inhabilita el listado de usuarios
        /// </summary>
        /// <param name="usuariosDTO">Listado de usuarios a inhabilitar</param>
        private void InhabilitarListadoDeUsuarios(List<DtoUsuario> usuariosDTO)
        {
            foreach (var usuarioDTO in usuariosDTO)
            {
                var usuario = _repositorioConsulta.ObtenerPorCodigo<Usuario>(
                    _servicioDesencriptador.Desencriptar<int>(usuarioDTO.CodigoUsuario));
                usuario = _servicioDominioUsuario.InhabilitarUsuario(usuario);
                _repositorioOperacion.Modificar(usuario);
            }
        }
        /// <summary>
        /// Método que habilita el listado de usuarios
        /// </summary>
        /// <param name="usuariosDTO">Listado de usuarios a habilitar</param>
        private void HabilitarListadoDeUsuarios(List<DtoUsuario> usuariosDTO)
        {
            foreach (var usuarioDTO in usuariosDTO)
            {
                var usuario = _repositorioConsulta.ObtenerPorCodigo<Usuario>(
                    _servicioDesencriptador.Desencriptar<int>(usuarioDTO.CodigoUsuario));
                usuario = _servicioDominioUsuario.HabilitarUsuario(usuario);
                _repositorioOperacion.Modificar(usuario);
            }
        }
        /// <summary>
        /// Método que obtiene un usuario DTO encriptado en base a su código de usuario
        /// </summary>
        /// <param name="codigoUsuario">Código del usuario a obtener</param>
        /// <returns>Resultado encriptado con los datos del usuario encontrado</returns>
        private string ObtenerUsuarioDTOEncriptado(int codigoUsuario)
        {
            var cantidadRegistrosLimite = 5;
            var accionAccesoSistema = _repositorioConsulta.ObtenerPorCodigo<AccionSistema>(
                AccionSistema.CodigoAccionAccesoSistema);
            var tipoEventoCorrecto = _repositorioConsulta.ObtenerPorCodigo<TipoEventoSistema>(
                TipoEventoSistema.CodigoTipoEventoCorrecto);

            var usuario = _repositorioConsulta.ObtenerPorCodigo<Usuario>(codigoUsuario);
            var bitacora = _repositorioConsulta.ObtenerPorExpresionLimite<BitacoraAccionSistema>(g =>
                g.CodigoUsuario == codigoUsuario).ToList();
            var accesos = 
                _servicioDominioBitacoraAccionSistema.FiltrarBitacoraDeAccionSegunAccionDelSistema(
                    bitacora, accionAccesoSistema);
            var accesosCorrectos =
                _servicioDominioBitacoraAccionSistema.FiltrarBitacoraDeAccionSegunTipoDeEvento(
                    accesos, tipoEventoCorrecto);
            var ultimosCincoAccesos =
                _servicioDominioBitacoraAccionSistema.OrdenarBitacoraDeAccionPorFecha(
                    accesosCorrectos, false, cantidadRegistrosLimite);
            var usuarioDTO = _servicioMapeoUsuarioADTO.MapearADTO(usuario, ultimosCincoAccesos);
            return _servicioEncriptador.Encriptar(usuarioDTO);
        }
        /// <summary>
        /// Método que obtiene un listado de usuarios DTO encriptados
        /// </summary>
        /// <param name="filtrarPorEstado">Si se desea filtrar por el indicador de estado
        /// del usuario (por defecto está inicializado con el valor FALSE)</param>
        /// <param name="indicadorEstado">Indicador de estado a filtrar (por defecto está inicializado
        /// con el valor TRUE)</param>
        /// <returns>Resultado encriptado con el listado de usuarios encontrados</returns>
        private string ObtenerUsuariosDTOEncriptados(bool filtrarPorEstado = false, 
            bool indicadorEstado = true)
        {
            var usuarios = _repositorioConsulta.ObtenerPorExpresionLimite<Usuario>(g => 
                filtrarPorEstado ? g.IndicadorEstado == indicadorEstado : true).ToList();
            var usuariosDTO = usuarios.Select(g => _servicioMapeoUsuarioADTO.MapearADTO(g)).ToList();
            return _servicioEncriptador.Encriptar(usuariosDTO);
        }
        #endregion
    }
}
