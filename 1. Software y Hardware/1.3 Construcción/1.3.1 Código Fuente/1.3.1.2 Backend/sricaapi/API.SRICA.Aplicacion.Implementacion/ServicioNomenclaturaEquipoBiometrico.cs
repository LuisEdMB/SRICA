using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Interfaz;
using API.SRICA.Dominio.Servicio.Interfaz;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación del servicio de consultas y operaciones de nomenclaturas para equipos biométricos
    /// </summary>
    public class ServicioNomenclaturaEquipoBiometrico : IServicioNomenclaturaEquipoBiometrico
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
        /// Servicio de dominio de la nomenclatura para equipos biométricos
        /// </summary>
        private readonly IServicioDominioNomenclaturaEquipoBiometrico
            _servicioDominioNomenclaturaEquipoBiometrico;
        /// <summary>
        /// Servicio de mapeo de la nomenclatura para equipos biométricos a DTO
        /// </summary>
        private readonly IServicioMapeoNomenclaturaEquipoBiometricoADto 
            _servicioMapeoNomenclaturaEquipoBiometricoADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioDesencriptador">Servicio para la desencriptación de datos</param>
        /// <param name="repositorioConsulta">Repositorio de consultas a la base de datos</param>
        /// <param name="repositorioOperacion">Repositorio de operaciones a la base de datos</param>
        /// <param name="servicioDominioNomenclaturaEquipoBiometrico">Servicio de dominio de la 
        /// nomenclatura para equipos biométricos</param>
        /// <param name="servicioMapeoNomenclaturaEquipoBiometricoADTO">Servicio de mapeo de la 
        /// nomenclatura para equipos biométricos a DTO</param>
        public ServicioNomenclaturaEquipoBiometrico(
            IServicioEncriptador servicioEncriptador,
            IServicioDesencriptador servicioDesencriptador,
            IRepositorioConsulta repositorioConsulta,
            IRepositorioOperacion repositorioOperacion,
            IServicioDominioNomenclaturaEquipoBiometrico servicioDominioNomenclaturaEquipoBiometrico,
            IServicioMapeoNomenclaturaEquipoBiometricoADto servicioMapeoNomenclaturaEquipoBiometricoADTO)
        {
            _servicioEncriptador = servicioEncriptador;
            _servicioDesencriptador = servicioDesencriptador;
            _repositorioConsulta = repositorioConsulta;
            _repositorioOperacion = repositorioOperacion;
            _servicioDominioNomenclaturaEquipoBiometrico = servicioDominioNomenclaturaEquipoBiometrico;
            _servicioMapeoNomenclaturaEquipoBiometricoADTO = servicioMapeoNomenclaturaEquipoBiometricoADTO;
        }
        /// <summary>
        /// Método que obtiene el listado de nomenclaturas registradas, tanto activos como inactivos
        /// </summary>
        /// <returns>Resultado encriptado con el listado de las nomenclaturas encontradas</returns>
        public string ObtenerListadoDeNomenclaturas()
        {
            return ObtenerNomenclaturasDTOEncriptados();
        }
        /// <summary>
        /// Método que obtiene una nomenclatura en base a su código de nomenclatura
        /// </summary>
        /// <param name="codigoNomenclaturaEncriptado">Código de la nomenclatura a obtener 
        /// (encriptado)</param>
        /// <returns>Resultado encriptado con los datos de la nomenclatura encontrada</returns>
        public string ObtenerNomenclatura(string codigoNomenclaturaEncriptado)
        {
            var codigoNomenclatura = _servicioDesencriptador.Desencriptar<int>(codigoNomenclaturaEncriptado);
            return ObtenerNomenclaturaDTOEncriptado(codigoNomenclatura);
        }
        /// <summary>
        /// Método que guarda una nueva nomenclatura
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos de la nomenclatura 
        /// a guardar</param>
        /// <returns>Resultado encriptado con los datos de la nomenclatura guardada</returns>
        public string GuardarNomenclatura(JToken encriptado)
        {
            var nomenclaturaDTO = _servicioDesencriptador.Desencriptar<DtoNomenclaturaEquipoBiometrico>(
                encriptado.ToString());
            var nomenclaturasRegistradas = 
                _repositorioConsulta.ObtenerPorExpresionLimite<NomenclaturaEquipoBiometrico>(g => 
                    !g.IndicadorRegistroParaSinAsignacion).ToList();
            var nomenclatura = _servicioDominioNomenclaturaEquipoBiometrico.CrearNomenclatura(
                nomenclaturasRegistradas, nomenclaturaDTO.DescripcionNomenclatura);
            _repositorioOperacion.Agregar(nomenclatura);
            _repositorioOperacion.GuardarCambios();
            return ObtenerNomenclaturaDTOEncriptado(nomenclatura.CodigoNomenclatura);
        }
        /// <summary>
        /// Método que modifica una nomenclatura
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos de la nomenclatura 
        /// a modificar</param>
        /// <returns>Resultado encriptado con los datos de la nomenclatura modificada</returns>
        public string ModificarNomenclatura(JToken encriptado)
        {
            var nomenclaturaDTO = _servicioDesencriptador.Desencriptar<DtoNomenclaturaEquipoBiometrico>(
                encriptado.ToString());
            var nomenclatura = _repositorioConsulta.ObtenerPorCodigo<NomenclaturaEquipoBiometrico>(
                _servicioDesencriptador.Desencriptar<int>(nomenclaturaDTO.CodigoNomenclatura));
            var nomenclaturasRegistradas =
                _repositorioConsulta.ObtenerPorExpresionLimite<NomenclaturaEquipoBiometrico>(g =>
                    !g.IndicadorRegistroParaSinAsignacion).ToList();
            nomenclatura = _servicioDominioNomenclaturaEquipoBiometrico.ModificarNomenclatura(nomenclatura,
                nomenclaturasRegistradas, nomenclaturaDTO.DescripcionNomenclatura);
            _repositorioOperacion.Modificar(nomenclatura);
            _repositorioOperacion.GuardarCambios();
            return ObtenerNomenclaturaDTOEncriptado(nomenclatura.CodigoNomenclatura);
        }
        /// <summary>
        /// Método que inhabilita a las nomenclaturas
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las nomenclaturas a 
        /// inhabilitar</param>
        /// <returns>Resultado encriptado con el listado de nomenclaturas inhabilitadas</returns>
        public string InhabilitarNomenclaturas(JToken encriptado)
        {
            var nomenclaturasDTO =
                _servicioDesencriptador.Desencriptar<List<DtoNomenclaturaEquipoBiometrico>>(
                    encriptado.ToString());
            _servicioDominioNomenclaturaEquipoBiometrico.ValidarNomenclaturasSeleccionadas(
                nomenclaturasDTO.Count);
            InhabilitarListadoDeNomenclaturas(nomenclaturasDTO);
            _repositorioOperacion.GuardarCambios();
            return ObtenerNomenclaturasDTOEncriptados(true, false);
        }
        /// <summary>
        /// Método que habilita a las nomenclaturas
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las nomenclaturas a 
        /// habilitar</param>
        /// <returns>Resultado encriptado con el listado de nomenclaturas habilitadas</returns>
        public string HabilitarNomenclaturas(JToken encriptado)
        {
            var nomenclaturasDTO =
                _servicioDesencriptador.Desencriptar<List<DtoNomenclaturaEquipoBiometrico>>(
                    encriptado.ToString());
            _servicioDominioNomenclaturaEquipoBiometrico.ValidarNomenclaturasSeleccionadas(
                nomenclaturasDTO.Count);
            HabilitarListadoDeNomenclaturas(nomenclaturasDTO);
            _repositorioOperacion.GuardarCambios();
            return ObtenerNomenclaturasDTOEncriptados(true);
        }
        #region Métodos privados
        /// <summary>
        /// Método que inhabilita el listado de nomenclaturas
        /// </summary>
        /// <param name="nomenclaturasDTO">Listado de nomenclaturas a inhabilitar</param>
        private void InhabilitarListadoDeNomenclaturas(
            List<DtoNomenclaturaEquipoBiometrico> nomenclaturasDTO)
        {
            foreach (var nomenclaturaDTO in nomenclaturasDTO)
            {
                var nomenclatura = 
                    _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<NomenclaturaEquipoBiometrico>(
                        g => g.CodigoNomenclatura == _servicioDesencriptador.Desencriptar<int>(
                            nomenclaturaDTO.CodigoNomenclatura)).FirstOrDefault();
                nomenclatura = _servicioDominioNomenclaturaEquipoBiometrico.InhabilitarNomenclatura(
                    nomenclatura);
                _repositorioOperacion.Modificar(nomenclatura);
                RemoverRelacionEntreLaNomenclaturaYLosEquipoBiometricos(nomenclatura);
            }
        }
        /// <summary>
        /// Método que remueve todas las relaciones hechas entre la nomenclatura a inhabilitar y los
        /// equipos biométricos
        /// </summary>
        /// <param name="nomenclatura">Nomenclatura inhabilitada</param>
        private void RemoverRelacionEntreLaNomenclaturaYLosEquipoBiometricos(
            NomenclaturaEquipoBiometrico nomenclatura)
        {
            var equiposBiometricos =
                _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<EquipoBiometrico>(g =>
                    g.CodigoNomenclatura == nomenclatura.CodigoNomenclatura).ToList();
            foreach (var equipoBiometrico in equiposBiometricos)
            {
                var equipoBiometricoSinAsignacion =
                    _servicioDominioNomenclaturaEquipoBiometrico
                        .AsignarNomenclaturaSinAsignacionAEquipoBiometrico(equipoBiometrico);
                _repositorioOperacion.Modificar(equipoBiometricoSinAsignacion);
            }
        }
        /// <summary>
        /// Método que habilita el listado de nomenclaturas
        /// </summary>
        /// <param name="nomenclaturasDTO">Listado de nomenclaturas a habilitar</param>
        private void HabilitarListadoDeNomenclaturas(
            List<DtoNomenclaturaEquipoBiometrico> nomenclaturasDTO)
        {
            foreach (var nomenclaturaDTO in nomenclaturasDTO)
            {
                var nomenclatura = _repositorioConsulta.ObtenerPorCodigo<NomenclaturaEquipoBiometrico>(
                    _servicioDesencriptador.Desencriptar<int>(nomenclaturaDTO.CodigoNomenclatura));
                nomenclatura = _servicioDominioNomenclaturaEquipoBiometrico.HabilitarNomenclatura(
                    nomenclatura);
                _repositorioOperacion.Modificar(nomenclatura);
            }
        }
        /// <summary>
        /// Método que obtiene una nomenclatura DTO encriptado en base a su código de nomenclatura
        /// </summary>
        /// <param name="codigoNomenclatura">Código de la nomenclatura a obtener</param>
        /// <returns>Resultado encriptado con los datos de la nomenclatura encontrada</returns>
        private string ObtenerNomenclaturaDTOEncriptado(int codigoNomenclatura)
        {
            var nomenclatura = _repositorioConsulta.ObtenerPorCodigo<NomenclaturaEquipoBiometrico>(
                codigoNomenclatura);
            var nomenclaturaDTO = _servicioMapeoNomenclaturaEquipoBiometricoADTO.MapearADTO(nomenclatura);
            return _servicioEncriptador.Encriptar(nomenclaturaDTO);
        }
        /// <summary>
        /// Método que obtiene un listado de nomenclaturas DTO encriptados
        /// </summary>
        /// <param name="filtrarPorEstado">Si se desea filtrar por el indicador de estado
        /// de la nomenclatura (por defecto está inicializado con el valor FALSE)</param>
        /// <param name="indicadorEstado">Indicador de estado a filtrar (por defecto está inicializado
        /// con el valor TRUE)</param>
        /// <returns>Resultado encriptado con el listado de nomenclaturas encontradas</returns>
        private string ObtenerNomenclaturasDTOEncriptados(bool filtrarPorEstado = false, 
            bool indicadorEstado = true)
        {
            var nomenclaturas =
                _repositorioConsulta.ObtenerPorExpresionLimite<NomenclaturaEquipoBiometrico>(g => 
                    filtrarPorEstado ? g.IndicadorEstado == indicadorEstado : true).ToList();
            var nomenclaturasDTO = nomenclaturas.Select(g =>
                _servicioMapeoNomenclaturaEquipoBiometricoADTO.MapearADTO(g)).ToList();
            return _servicioEncriptador.Encriptar(nomenclaturasDTO);
        }
        #endregion
    }
}
