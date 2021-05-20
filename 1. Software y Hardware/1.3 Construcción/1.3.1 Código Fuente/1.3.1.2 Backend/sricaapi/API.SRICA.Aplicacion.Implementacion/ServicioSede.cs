using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.AR;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.PE;
using API.SRICA.Dominio.Entidad.SE;
using API.SRICA.Dominio.Interfaz;
using API.SRICA.Dominio.Servicio.Interfaz;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación del servicio de consultas y operaciones de sedes
    /// </summary>
    public class ServicioSede : IServicioSede
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
        /// Servicio de dominio de la sede
        /// </summary>
        private readonly IServicioDominioSede _servicioDominioSede;
        /// <summary>
        /// Servicio de dominio del personal de la empresa
        /// </summary>
        private readonly IServicioDominioPersonalEmpresa _servicioDominioPersonalEmpresa;
        /// <summary>
        /// Servicio de mapeo de la sede a DTO
        /// </summary>
        private readonly IServicioMapeoSedeADto _servicioMapeoSedeADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioDesencriptador">Servicio para la desencriptación de datos</param>
        /// <param name="repositorioConsulta">Repositorio de consultas a la base de datos</param>
        /// <param name="repositorioOperacion">Repositorio de operaciones a la base de datos</param>
        /// <param name="servicioDominioSede">Servicio de dominio de la sede</param>
        /// <param name="servicioDominioPersonalEmpresa">Servicio de dominio del personal de la 
        /// empresa</param>
        /// <param name="servicioMapeoSedeADTO">Servicio de mapeo de la sede a DTO</param>
        public ServicioSede(IServicioEncriptador servicioEncriptador,
            IServicioDesencriptador servicioDesencriptador,
            IRepositorioConsulta repositorioConsulta,
            IRepositorioOperacion repositorioOperacion,
            IServicioDominioSede servicioDominioSede,
            IServicioDominioPersonalEmpresa servicioDominioPersonalEmpresa,
            IServicioMapeoSedeADto servicioMapeoSedeADTO)
        {
            _servicioEncriptador = servicioEncriptador;
            _servicioDesencriptador = servicioDesencriptador;
            _repositorioConsulta = repositorioConsulta;
            _repositorioOperacion = repositorioOperacion;
            _servicioDominioSede = servicioDominioSede;
            _servicioDominioPersonalEmpresa = servicioDominioPersonalEmpresa;
            _servicioMapeoSedeADTO = servicioMapeoSedeADTO;
        }
        /// <summary>
        /// Método que obtiene el listado de las sedes registradas, tanto activos como inactivos
        /// </summary>
        /// <returns>Resultado encriptado con el listado de las sedes</returns>
        public string ObtenerListadoDeSedes()
        {
            return ObtenerSedesDTOEncriptados();
        }
        /// <summary>
        /// Método que obtiene una sede en base a su código de sede
        /// </summary>
        /// <param name="codigoSedeEncriptado">Código de la sede a obtener (encriptado)</param>
        /// <returns>Resultado encriptado con los datos de la sede encontrada</returns>
        public string ObtenerSede(string codigoSedeEncriptado)
        {
            var codigoSede = _servicioDesencriptador.Desencriptar<int>(codigoSedeEncriptado);
            return ObtenerSedeDTOEncriptado(codigoSede);
        }
        /// <summary>
        /// Método que guarda una nueva sede
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos de la sede a guardar</param>
        /// <returns>Resultado encriptado con los datos de la sede guardada</returns>
        public string GuardarSede(JToken encriptado)
        {
            var sedeDTO = _servicioDesencriptador.Desencriptar<DtoSede>(encriptado.ToString());
            var sede = _servicioDominioSede.CrearSede(sedeDTO.DescripcionSede);
            _repositorioOperacion.Agregar(sede);
            _repositorioOperacion.GuardarCambios();
            return ObtenerSedeDTOEncriptado(sede.CodigoSede);
        }
        /// <summary>
        /// Método que modifica una sede
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos de la sede a modificar</param>
        /// <returns>Resultado encriptado con los datos de la sede modificada</returns>
        public string ModificarSede(JToken encriptado)
        {
            var sedeDTO = _servicioDesencriptador.Desencriptar<DtoSede>(encriptado.ToString());
            var sede = _repositorioConsulta.ObtenerPorCodigo<Sede>(
                _servicioDesencriptador.Desencriptar<int>(sedeDTO.CodigoSede));
            sede = _servicioDominioSede.ModificarSede(sede, sedeDTO.DescripcionSede);
            _repositorioOperacion.Modificar(sede);
            _repositorioOperacion.GuardarCambios();
            return ObtenerSedeDTOEncriptado(sede.CodigoSede);
        }
        /// <summary>
        /// Método que inhabilita a las sedes
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las sedes a 
        /// inhabilitar</param>
        /// <returns>Resultado encriptado con el listado de sedes inhabilitadas</returns>
        public string InhabilitarSedes(JToken encriptado)
        {
            var sedesDTO = _servicioDesencriptador.Desencriptar<List<DtoSede>>(encriptado.ToString());
            _servicioDominioSede.ValidarSedesSeleccionadas(sedesDTO.Count);
            InhabilitarListadoDeSedes(sedesDTO);
            _repositorioOperacion.GuardarCambios();
            return ObtenerSedesDTOEncriptados(true, false);
        }
        /// <summary>
        /// Método que habilita a las sedes
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las sedes a 
        /// habilitar</param>
        /// <returns>Resultado encriptado con el listado de sedes habilitadas</returns>
        public string HabilitarSedes(JToken encriptado)
        {
            var sedesDTO = _servicioDesencriptador.Desencriptar<List<DtoSede>>(encriptado.ToString());
            _servicioDominioSede.ValidarSedesSeleccionadas(sedesDTO.Count);
            HabilitarListadoDeSedes(sedesDTO);
            _repositorioOperacion.GuardarCambios();
            return ObtenerSedesDTOEncriptados(true);
        }
        #region Métodos privados
        /// <summary>
        /// Método que inhabilita el listado de sedes
        /// </summary>
        /// <param name="sedesDTO">Listado de sedes a inhabilitar</param>
        private void InhabilitarListadoDeSedes(List<DtoSede> sedesDTO)
        {
            foreach (var sedeDTO in sedesDTO)
            {
                var sede = _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<Sede>(g => 
                    g.CodigoSede == _servicioDesencriptador.Desencriptar<int>(sedeDTO.CodigoSede))
                    .FirstOrDefault();
                sede = _servicioDominioSede.InhabilitarSede(sede);
                _repositorioOperacion.Modificar(sede);
                RemoverRelacionEntreLaSedeYSusAreas(sede);
                RemoverRelacionEntreLaSedeYLosEquipoBiometricos(sede);
                RemoverRelacionEntreLaSedeYElPersonalDeLaEmpresa(sede);
            }
        }
        /// <summary>
        /// Método que remueve todas las relaciones hechas entre la sede a inhabilitar y las áreas
        /// </summary>
        /// <param name="sede">Sede inhabilitada</param>
        private void RemoverRelacionEntreLaSedeYSusAreas(Sede sede)
        {
            var areas = _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<Area>(g =>
                g.CodigoSede == sede.CodigoSede).ToList();
            foreach (var area in areas)
            {
                var areaSinAsignacion = _servicioDominioSede.AsignarSedeSinAsignacionAArea(area);
                _repositorioOperacion.Modificar(areaSinAsignacion);
            }
        }
        /// <summary>
        /// Método que remueve todas las relaciones hechas entre la sede a inhabilitar y los
        /// equipos biométricos
        /// </summary>
        /// <param name="sede">Sede inhabilitada</param>
        private void RemoverRelacionEntreLaSedeYLosEquipoBiometricos(Sede sede)
        {
            var areas = _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<Area>(g =>
                g.CodigoSede == sede.CodigoSede).ToList();
            foreach (var area in areas)
            {
                var equiposBiometricos =
                    _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<EquipoBiometrico>(g =>
                        g.CodigoArea == area.CodigoArea).ToList();
                foreach (var equipoBiometrico in equiposBiometricos)
                {
                    var equipoBiometricoSinAsignacion =
                        _servicioDominioSede.AsignarAreaSinAsignacionAEquipoBiometrico(equipoBiometrico);
                    _repositorioOperacion.Modificar(equipoBiometricoSinAsignacion);
                }
            }
        }
        /// <summary>
        /// Método que remueve todas las relaciones hechas entre la sede a inhabilitar y el 
        /// personal de la empresa
        /// </summary>
        /// <param name="sede">Sede inhabilitada</param>
        private void RemoverRelacionEntreLaSedeYElPersonalDeLaEmpresa(Sede sede)
        {
            var areas = _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<Area>(g =>
                g.CodigoSede == sede.CodigoSede).ToList();
            foreach (var area in areas)
            {
                var personalEmpresaRelaciones =
                    _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<PersonalEmpresaXArea>(g =>
                        g.CodigoArea == area.CodigoArea).ToList();
                foreach (var personalEmpresaRelacion in personalEmpresaRelaciones)
                {
                    var personalEmpresaXAreaInactivado =
                        _servicioDominioPersonalEmpresa.InactivarRelacionArea(personalEmpresaRelacion);
                    _repositorioOperacion.Modificar(personalEmpresaXAreaInactivado);
                }
            }
        }
        /// <summary>
        /// Método que habilita el listado de sedes
        /// </summary>
        /// <param name="sedesDTO">Listado de sedes a habilitar</param>
        private void HabilitarListadoDeSedes(List<DtoSede> sedesDTO)
        {
            foreach (var sedeDTO in sedesDTO)
            {
                var sede = _repositorioConsulta.ObtenerPorCodigo<Sede>(
                    _servicioDesencriptador.Desencriptar<int>(sedeDTO.CodigoSede));
                sede = _servicioDominioSede.HabilitarSede(sede);
                _repositorioOperacion.Modificar(sede);
            }
        }
        /// <summary>
        /// Método que obtiene una sede DTO encriptado en base a su código de sede
        /// </summary>
        /// <param name="codigoSede">Código de la sede a obtener</param>
        /// <returns>Resultado encriptado con los datos de la sede encontrada</returns>
        private string ObtenerSedeDTOEncriptado(int codigoSede)
        {
            var sede = _repositorioConsulta.ObtenerPorCodigo<Sede>(codigoSede);
            var sedeDTO = _servicioMapeoSedeADTO.MapearADTO(sede);
            return _servicioEncriptador.Encriptar(sedeDTO);
        }
        /// <summary>
        /// Método que obtiene un listado de sedes DTO encriptados
        /// </summary>
        /// <param name="filtrarPorEstado">Si se desea filtrar por el indicador de estado
        /// de la sede (por defecto está inicializado con el valor FALSE)</param>
        /// <param name="indicadorEstado">Indicador de estado a filtrar (por defecto está inicializado
        /// con el valor TRUE)</param>
        /// <returns>Resultado encriptado con el listado de sedes encontradas</returns>
        private string ObtenerSedesDTOEncriptados(bool filtrarPorEstado = false, bool indicadorEstado = true)
        {
            var sedes = _repositorioConsulta.ObtenerPorExpresionLimite<Sede>(g => 
                filtrarPorEstado ? g.IndicadorEstado == indicadorEstado : true).ToList();
            var sedesDTO = sedes.Select(g => _servicioMapeoSedeADTO.MapearADTO(g)).ToList();
            return _servicioEncriptador.Encriptar(sedesDTO);
        }
        #endregion
    }
}
