using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.AR;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.SE;
using API.SRICA.Dominio.Interfaz;
using API.SRICA.Dominio.Servicio.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Interfaz para el servicio de consultas y operaciones de equipos biométricos
    /// </summary>
    public class ServicioEquipoBiometrico : IServicioEquipoBiometrico
    {
        /// <summary>
        /// Configuración del proyecto
        /// </summary>
        private readonly IConfiguration _configuracion;
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
        /// Servicio para direcciones de red
        /// </summary>
        private readonly IServicioDireccionRed _servicioDireccionRed;
        /// <summary>
        /// Servicio de dominio del equipo biométrico
        /// </summary>
        private readonly IServicioDominioEquipoBiometrico _servicioDominioEquipoBiometrico;
        /// <summary>
        /// Servicio de mapeo del equipo biométrico a DTO
        /// </summary>
        private readonly IServicioMapeoEquipoBiometicoADto _servicioMapeoEquipoBiometicoADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="configuracion">Configuración del proyecto</param>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioDesencriptador">Servicio para la desencriptación de datos</param>
        /// <param name="repositorioConsulta">Repositorio de consultas a la base de datos</param>
        /// <param name="repositorioOperacion">Repositorio de operaciones a la base de datos</param>
        /// <param name="servicioDireccionRed">Servicio para direcciones de red</param>
        /// <param name="servicioDominioEquipoBiometrico">Servicio de dominio del equipo biométrico</param>
        /// <param name="servicioMapeoEquipoBiometicoADTO">Servicio de mapeo del equipo biométrico a 
        /// DTO</param>
        public ServicioEquipoBiometrico(IConfiguration configuracion,
            IServicioEncriptador servicioEncriptador, IServicioDesencriptador servicioDesencriptador, 
            IRepositorioConsulta repositorioConsulta, IRepositorioOperacion repositorioOperacion,
            IServicioDireccionRed servicioDireccionRed, 
            IServicioDominioEquipoBiometrico servicioDominioEquipoBiometrico,
            IServicioMapeoEquipoBiometicoADto servicioMapeoEquipoBiometicoADTO)
        {
            _configuracion = configuracion;
            _servicioEncriptador = servicioEncriptador;
            _servicioDesencriptador = servicioDesencriptador;
            _repositorioConsulta = repositorioConsulta;
            _repositorioOperacion = repositorioOperacion;
            _servicioDireccionRed = servicioDireccionRed;
            _servicioDominioEquipoBiometrico = servicioDominioEquipoBiometrico;
            _servicioMapeoEquipoBiometicoADTO = servicioMapeoEquipoBiometicoADTO;
        }
        /// <summary>
        /// Método que obtiene el listado de los equipos biométricos presentes en la red
        /// empresarial
        /// </summary>
        /// <returns>Listado de equipos biométricos encontrados en la red empresarial</returns>
        public string ObtenerListadoEquiposBiometricosDeLaRedEmpresarial()
        {
            List<string> listaHosts = _servicioDireccionRed.ObtenerListadoDeHosts(
                _configuracion["EQUIPO_BIOMETRICO_IP"],
                _configuracion["EQUIPO_BIOMETRICO_MASCARA_SUBRED"],
                _configuracion["EQUIPO_BIOMETRICO_DOMINIO"]);
            var nomenclaturas =
                _repositorioConsulta.ObtenerPorExpresionLimite<NomenclaturaEquipoBiometrico>(g =>
                    g.IndicadorEstado && !g.IndicadorRegistroParaSinAsignacion).ToList();
            var equiposBiometricosRegistrados = 
                _repositorioConsulta.ObtenerPorExpresionLimite<EquipoBiometrico>().ToList();
            var hostsValidados = _servicioDireccionRed.PingAsync(listaHosts);
            var equiposBiometricosDTO = FiltrarEquipoBiometricosConNomenclaturas(hostsValidados.Result, 
                nomenclaturas);
            equiposBiometricosDTO = FiltrarEquipoBiometricosConEquiposBiometricosRegistrados(
                equiposBiometricosDTO, equiposBiometricosRegistrados);
            return _servicioEncriptador.Encriptar(equiposBiometricosDTO);
        }
        /// <summary>
        /// Método que obtiene el listado de los equipos biométricos registrados (tanto
        /// activos como inactivos)
        /// </summary>
        /// <returns>Resultado encriptado con el listado de equipos biométricos 
        /// registrados encontrados</returns>
        public string ObtenerListadoDeEquiposBiometricosRegistrados()
        {
            return ObtenerEquiposBiometricosDTOEncriptados();
        }
        /// <summary>
        /// Método que obtiene un equipo biométrico en base a su código de equipo biométrico
        /// </summary>
        /// <param name="codigoEquipoBiometricoEncriptado">Código del equipo biométrico a obtener 
        /// (encriptado)</param>
        /// <returns>Resultado encriptado con los datos del equipo biométrico encontrado</returns>
        public string ObtenerEquipoBiometrico(string codigoEquipoBiometricoEncriptado)
        {
            var codigoEquipoBiometrico = 
                _servicioDesencriptador.Desencriptar<int>(codigoEquipoBiometricoEncriptado);
            return ObtenerEquipoBiometricoDTOEncriptado(codigoEquipoBiometrico);
        }
        /// <summary>
        /// Método que guarda un nuevo equipo biométrico
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del equipo biométrico 
        /// a guardar. Así mismo, contiene los datos de bitácora de acción correspondiente</param>
        /// <returns>Resultado encriptado con los datos del equipo biométrico guardado</returns>
        public string GuardarEquipoBiometrico(JToken encriptado)
        {
            var equipoBiometricoDTO = _servicioDesencriptador.Desencriptar<DtoEquipoBiometrico>(
                encriptado.ToString());
            var equiposBiometricosRegistrados =
                _repositorioConsulta.ObtenerPorExpresionLimite<EquipoBiometrico>().ToList();
            var nomenclatura = _repositorioConsulta.ObtenerPorCodigo<NomenclaturaEquipoBiometrico>(
                _servicioDesencriptador.Desencriptar<int>(equipoBiometricoDTO.CodigoNomenclatura));
            var direccionMAC = _servicioEncriptador.Encriptar(
                equipoBiometricoDTO.DireccionFisicaEquipoBiometrico);
            var equipoBiometrico = _servicioDominioEquipoBiometrico.CrearEquipoBiometrico(
                equiposBiometricosRegistrados, nomenclatura, equipoBiometricoDTO.NombreEquipoBiometrico,
                equipoBiometricoDTO.DireccionRedEquipoBiometrico, direccionMAC);
            _repositorioOperacion.Agregar(equipoBiometrico);
            _repositorioOperacion.GuardarCambios();
            return ObtenerEquipoBiometricoDTOEncriptado(equipoBiometrico.CodigoEquipoBiometrico);
        }
        /// <summary>
        /// Método que modifica un equipo biométrico
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del equipo biométrico 
        /// a modificar</param>
        /// <returns>Resultado encriptado con los datos del equipo biométrico modificado</returns>
        public string ModificarEquipoBiometrico(JToken encriptado)
        {
            var equipoBiometricoDTO = _servicioDesencriptador.Desencriptar<DtoEquipoBiometrico>(
                encriptado.ToString());
            return ModificarDatosDelEquiposBiometrico(equipoBiometricoDTO);
        }
        /// <summary>
        /// Método que inhabilita a los equipos biométricos
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de los equipos biométricos a 
        /// inhabilitar</param>
        /// <returns>Resultado encriptado con el listado de equipos biométricos inhabilitados</returns>
        public string InhabilitarEquiposBiometricos(JToken encriptado)
        {
            var equiposBiometricosDTO = _servicioDesencriptador.Desencriptar<List<DtoEquipoBiometrico>>(
                encriptado.ToString());
            _servicioDominioEquipoBiometrico.ValidarEquiposBiometricosSeleccionados(
                equiposBiometricosDTO.Count);
            InhabilitarListadoDeEquiposBiometricos(equiposBiometricosDTO);
            _repositorioOperacion.GuardarCambios();
            return ObtenerEquiposBiometricosDTOEncriptados(true, false);
        }
        /// <summary>
        /// Método que habilita a los equipos biométricos
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de los equipos biométricos a 
        /// habilitar</param>
        /// <returns>Resultado encriptado con el listado de equipos biométricos habilitados</returns>
        public string HabilitarEquiposBiometricos(JToken encriptado)
        {
            var equiposBiometricosDTO = _servicioDesencriptador.Desencriptar<List<DtoEquipoBiometrico>>(
                encriptado.ToString());
            _servicioDominioEquipoBiometrico.ValidarEquiposBiometricosSeleccionados(
                equiposBiometricosDTO.Count);
            HabilitarListadoDeEquiposBiometricos(equiposBiometricosDTO);
            _repositorioOperacion.GuardarCambios();
            return ObtenerEquiposBiometricosDTOEncriptados(true);
        }
        /// <summary>
        /// Método que se comunica con el equipo biométrico para abrir la puerta de acceso
        /// </summary>
        /// <param name="codigoEquipoBiometricoEncriptado">Código encriptado del equipo biométrico</param>
        /// <returns>Resultado encriptado con el éxito o fracaso de la operación</returns>
        public string AbrirPuertaDeAccesoDelEquipoBiometrico(string codigoEquipoBiometricoEncriptado)
        {
            var codigoEquipoBiometrico = 
                _servicioDesencriptador.Desencriptar<int>(codigoEquipoBiometricoEncriptado);
            var equipoBiometrico = _repositorioConsulta.ObtenerPorCodigo<EquipoBiometrico>(
                codigoEquipoBiometrico);
            _servicioDominioEquipoBiometrico.AbrirPuertaDeAccesoDelEquipoBiometrico(equipoBiometrico,
                _configuracion["EQUIPO_BIOMETRICO_USUARIO_SSH"],
                _configuracion["EQUIPO_BIOMETRICO_CLAVE_SSH"]);
            return _servicioEncriptador.Encriptar(true);
        }

        /// <summary>
        /// Método que envía una señal el equipo biométrico (pitidos)
        /// </summary>
        /// <param name="codigoEquipoBiometricoEncriptado">Código encriptado del equipo biométrico</param>
        /// <returns>Resultado encriptado con el éxito o fracaso de la operación</returns>
        public string EnviarSenalAEquipoBiometrico(string codigoEquipoBiometricoEncriptado)
        {
            var codigoEquipoBiometrico = 
                _servicioDesencriptador.Desencriptar<int>(codigoEquipoBiometricoEncriptado);
            var equipoBiometrico = _repositorioConsulta.ObtenerPorCodigo<EquipoBiometrico>(
                codigoEquipoBiometrico);
            _servicioDominioEquipoBiometrico.EnviarSenalAEquipoBiometrico(equipoBiometrico,
                _configuracion["EQUIPO_BIOMETRICO_USUARIO_SSH"],
                _configuracion["EQUIPO_BIOMETRICO_CLAVE_SSH"]);
            return _servicioEncriptador.Encriptar(true);
        }
        #region Métodos privados
        /// <summary>
        /// Método que filtra los hosts (supuestos equipos biométricos hallados en la red empresarial) 
        /// según las nomenclaturas registradas (en estado activo)
        /// </summary>
        /// <param name="equiposBiometricos">Equipos biométricos hallados</param>
        /// <param name="nomenclaturas">Nomenclaturas activas</param>
        /// <returns>Listado (DTO) de equipos biométricos filtrados</returns>
        private List<DtoEquipoBiometrico> FiltrarEquipoBiometricosConNomenclaturas(
            List<DtoEquipoBiometrico> equiposBiometricos,
            List<NomenclaturaEquipoBiometrico> nomenclaturas)
        {
            var equiposBiometricosDTO = new List<DtoEquipoBiometrico>();
            foreach (var equipoBiometrico in equiposBiometricos)
            {
                if (!string.IsNullOrEmpty(equipoBiometrico.DireccionRedEquipoBiometrico))
                {
                    var nomenclatura = nomenclaturas.FirstOrDefault(g => equipoBiometrico.NombreEquipoBiometrico.ToUpper()
                        .StartsWith((g.DescripcionNomenclatura + "-")));
                    if (nomenclatura != null)
                    {
                        equipoBiometrico.CodigoNomenclatura = _servicioEncriptador.Encriptar(
                            nomenclatura.CodigoNomenclatura);
                        equipoBiometrico.DescripcionNomenclatura = nomenclatura.DescripcionNomenclatura;
                        equiposBiometricosDTO.Add(equipoBiometrico);
                    }
                }
            }
            return equiposBiometricosDTO;
        }
        /// <summary>
        /// Método que filtra los hosts (supuestos equipos biométricos hallados en la red empresarial) 
        /// que no se encuentren ya registrados (validación por MAC)
        /// </summary>
        /// <param name="equiposBiometricosDTO">Equipos biométricos hallados</param>
        /// <param name="equiposBiometricos">Equipos biométricos registados (tanto activos
        /// como inactivos)</param>
        /// <returns>Listado (DTO) de equipos biométricos filtrados</returns>
        private List<DtoEquipoBiometrico> FiltrarEquipoBiometricosConEquiposBiometricosRegistrados(
            List<DtoEquipoBiometrico> equiposBiometricosDTO,
            List<EquipoBiometrico> equiposBiometricos)
        {
            var equiposBiometricosFiltradosDTO = new List<DtoEquipoBiometrico>();
            foreach (var equipoBiometricoDTO in equiposBiometricosDTO)
            {
                var direccionMacEncriptado = _servicioEncriptador.Encriptar(
                    equipoBiometricoDTO.DireccionFisicaEquipoBiometrico);
                if (!equiposBiometricos.Any(g =>
                    g.DireccionFisicaEquipoBiometrico.Equals(direccionMacEncriptado)))
                    equiposBiometricosFiltradosDTO.Add(equipoBiometricoDTO);
            }
            return equiposBiometricosFiltradosDTO;
        }
        /// <summary>
        /// Método que obtiene un equipo biométrico DTO encriptado en base a su código de 
        /// equipo biométrico
        /// </summary>
        /// <param name="codigoEquipoBiometrico">Código del equipo biométrico a obtener</param>
        /// <returns>Resultado encriptado con los datos del equipo biométrico encontrado</returns>
        private string ObtenerEquipoBiometricoDTOEncriptado(int codigoEquipoBiometrico)
        {
            var equipoBiometrico = _repositorioConsulta.ObtenerPorCodigo<EquipoBiometrico>(
                codigoEquipoBiometrico);
            var equipoBiometricoDTO = _servicioMapeoEquipoBiometicoADTO.MapearADTO(equipoBiometrico);
            return _servicioEncriptador.Encriptar(equipoBiometricoDTO);
        }
        /// <summary>
        /// Método que modifica los datos de un equipo biométrico
        /// </summary>
        /// <param name="equipoBiometricoDTO">Datos del equipo biométrico a modificar</param>
        /// <returns>Resultado encriptado con los datos del equipo biométrico modificado</returns>
        private string ModificarDatosDelEquiposBiometrico(DtoEquipoBiometrico equipoBiometricoDTO)
        {
            using var transaccion = new TransactionScope();
            var equipoBiometrico = _repositorioConsulta.ObtenerPorCodigo<EquipoBiometrico>(
                _servicioDesencriptador.Desencriptar<int>(equipoBiometricoDTO.CodigoEquipoBiometrico));
            var equiposBiometricosRegistrados =
                _repositorioConsulta.ObtenerPorExpresionLimite<EquipoBiometrico>().ToList();
            var nomenclatura = _repositorioConsulta.ObtenerPorCodigo<NomenclaturaEquipoBiometrico>(
                _servicioDesencriptador.Desencriptar<int>(equipoBiometricoDTO.CodigoNomenclatura));
            var sede = _repositorioConsulta.ObtenerPorCodigo<Sede>(
                _servicioDesencriptador.Desencriptar<int>(equipoBiometricoDTO.CodigoSede));
            var area = _repositorioConsulta.ObtenerPorCodigo<Area>(
                _servicioDesencriptador.Desencriptar<int>(equipoBiometricoDTO.CodigoArea));
            equipoBiometrico = _servicioDominioEquipoBiometrico.ModificarEquipoBiometrico(
                equipoBiometrico, equiposBiometricosRegistrados, nomenclatura,
                equipoBiometricoDTO.NombreEquipoBiometrico, equipoBiometricoDTO.DireccionRedEquipoBiometrico,
                sede, area);
            _repositorioOperacion.Modificar(equipoBiometrico);
            _repositorioOperacion.GuardarCambios();
            _servicioDominioEquipoBiometrico.ModificarDatosPropiosDelEquipoBiometrico(equipoBiometrico,
                _configuracion["EQUIPO_BIOMETRICO_USUARIO_SSH"],
                _configuracion["EQUIPO_BIOMETRICO_CLAVE_SSH"]);
            transaccion.Complete();
            transaccion.Dispose();
            return ObtenerEquipoBiometricoDTOEncriptado(equipoBiometrico.CodigoEquipoBiometrico);
        }
        /// <summary>
        /// Método que inhabilita el listado de equipos biométricos
        /// </summary>
        /// <param name="equiposBiometricosDTO">Listado de equipos biométricos a inhabilitar</param>
        private void InhabilitarListadoDeEquiposBiometricos(List<DtoEquipoBiometrico> equiposBiometricosDTO)
        {
            foreach (var equipoBiometricoDTO in equiposBiometricosDTO)
            {
                var equipoBiometrico = _repositorioConsulta.ObtenerPorCodigo<EquipoBiometrico>(
                    _servicioDesencriptador.Desencriptar<int>(equipoBiometricoDTO.CodigoEquipoBiometrico));
                equipoBiometrico = _servicioDominioEquipoBiometrico.InhabilitarEquipoBiometrico(
                    equipoBiometrico);
                _repositorioOperacion.Modificar(equipoBiometrico);
            }
        }
        /// <summary>
        /// Método que habilita el listado de equipos biométricos
        /// </summary>
        /// <param name="equiposBiometricosDTO">Listado de equipos biométricos a habilitar</param>
        private void HabilitarListadoDeEquiposBiometricos(List<DtoEquipoBiometrico> equiposBiometricosDTO)
        {
            foreach (var equipoBiometricoDTO in equiposBiometricosDTO)
            {
                var equipoBiometrico = _repositorioConsulta.ObtenerPorCodigo<EquipoBiometrico>(
                    _servicioDesencriptador.Desencriptar<int>(equipoBiometricoDTO.CodigoEquipoBiometrico));
                equipoBiometrico = _servicioDominioEquipoBiometrico.HabilitarEquipoBiometrico(
                    equipoBiometrico);
                _repositorioOperacion.Modificar(equipoBiometrico);
            }
        }
        /// <summary>
        /// Método que obtiene un listado de equipos biométricos DTO encriptados
        /// </summary>
        /// <param name="filtrarPorEstado">Si se desea filtrar por el indicador de estado
        /// del equipo biométrico (por defecto está inicializado con el valor FALSE)</param>
        /// <param name="indicadorEstado">Indicador de estado a filtrar (por defecto está inicializado
        /// con el valor TRUE)</param>
        /// <returns>Resultado encriptado con el listado de equipos biométricos encontrados</returns>
        private string ObtenerEquiposBiometricosDTOEncriptados(bool filtrarPorEstado = false,
            bool indicadorEstado = true)
        {
            var equiposBiometricos = _repositorioConsulta.ObtenerPorExpresionLimite<EquipoBiometrico>(g =>
                !filtrarPorEstado || g.IndicadorEstado == indicadorEstado).ToList();
            var equiposBiometricosDTO = equiposBiometricos.Select(g => 
                _servicioMapeoEquipoBiometicoADTO.MapearADTO(g)).ToList();
            return _servicioEncriptador.Encriptar(equiposBiometricosDTO);
        }
        #endregion
    }
}
