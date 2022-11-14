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
    /// Implementación del servicio de consultas y operaciones de áreas
    /// </summary>
    public class ServicioArea : IServicioArea
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
        /// Servicio de dominio del área
        /// </summary>
        private readonly IServicioDominioArea _servicioDominioArea;
        /// <summary>
        /// Servicio de dominio del personal de la empresa
        /// </summary>
        private readonly IServicioDominioPersonalEmpresa _servicioDominioPersonalEmpresa;
        /// <summary>
        /// Servicio de mapeo del área a DTO
        /// </summary>
        private readonly IServicioMapeoAreaADto _servicioMapeoAreaADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioDesencriptador">Servicio para la desencriptación de datos</param>
        /// <param name="repositorioConsulta">Repositorio de consultas a la base de datos</param>
        /// <param name="repositorioOperacion">Repositorio de operaciones a la base de datos</param>
        /// <param name="servicioDominioArea">Servicio de dominio del área</param>
        /// <param name="servicioDominioPersonalEmpresa">Servicio de dominio del personal de la 
        /// empresa</param>
        /// <param name="servicioMapeoAreaADTO">Servicio de mapeo del área a DTO</param>
        public ServicioArea(IServicioEncriptador servicioEncriptador,
            IServicioDesencriptador servicioDesencriptador,
            IRepositorioConsulta repositorioConsulta,
            IRepositorioOperacion repositorioOperacion,
            IServicioDominioArea servicioDominioArea,
            IServicioDominioPersonalEmpresa servicioDominioPersonalEmpresa,
            IServicioMapeoAreaADto servicioMapeoAreaADTO)
        {
            _servicioEncriptador = servicioEncriptador;
            _servicioDesencriptador = servicioDesencriptador;
            _repositorioConsulta = repositorioConsulta;
            _repositorioOperacion = repositorioOperacion;
            _servicioDominioArea = servicioDominioArea;
            _servicioDominioPersonalEmpresa = servicioDominioPersonalEmpresa;
            _servicioMapeoAreaADTO = servicioMapeoAreaADTO;
        }
        /// <summary>
        /// Método que obtiene el listado de las áreas registradas, tanto activos como inactivos.
        /// Así mismo, se puede obtener el listado de áreas según una sede, tanto activos como inactivos
        /// </summary>
        /// <param name="codigoSedeEncriptado">Código de la sede (encriptado) (opcional)</param>
        /// <returns>Resultado encriptado con el listado de las áreas encontradas</returns>
        public string ObtenerListadoDeAreas(string codigoSedeEncriptado)
        {
            if (string.IsNullOrEmpty(codigoSedeEncriptado))
                return ObtenerListadoDeTodasLasAreas();
            else
                return ObtenerListadoDeAreasSegunUnaSede(codigoSedeEncriptado);
        }
        /// <summary>
        /// Método que obtiene un área en base a su código de área
        /// </summary>
        /// <param name="codigoAreaEncriptado">Código del área a obtener (encriptado)</param>
        /// <returns>Resultado encriptado con los datos del área encontrada</returns>
        public string ObtenerArea(string codigoAreaEncriptado)
        {
            var codigoArea = _servicioDesencriptador.Desencriptar<int>(codigoAreaEncriptado);
            return ObtenerAreaDTOEncriptado(codigoArea);
        }
        /// <summary>
        /// Método que guarda una nueva área
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del área a guardar</param>
        /// <returns>Resultado encriptado con los datos del área guardada</returns>
        public string GuardarArea(JToken encriptado)
        {
            var areaDTO = _servicioDesencriptador.Desencriptar<DtoArea>(encriptado.ToString());
            var sede = _repositorioConsulta.ObtenerPorCodigo<Sede>(
                _servicioDesencriptador.Desencriptar<int>(areaDTO.CodigoSede));
            var area = _servicioDominioArea.CrearArea(sede, areaDTO.DescripcionArea);
            _repositorioOperacion.Agregar(area);
            _repositorioOperacion.GuardarCambios();
            return ObtenerAreaDTOEncriptado(area.CodigoArea);
        }
        /// <summary>
        /// Método que modifica un área
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del área a modificar</param>
        /// <returns>Resultado encriptado con los datos del área modificada</returns>
        public string ModificarArea(JToken encriptado)
        {
            var areaDTO = _servicioDesencriptador.Desencriptar<DtoArea>(encriptado.ToString());
            var area = _repositorioConsulta.ObtenerPorCodigo<Area>(
                _servicioDesencriptador.Desencriptar<int>(areaDTO.CodigoArea));
            var sede = _repositorioConsulta.ObtenerPorCodigo<Sede>(
                _servicioDesencriptador.Desencriptar<int>(areaDTO.CodigoSede));
            area = _servicioDominioArea.ModificarArea(area, sede, areaDTO.DescripcionArea);
            _repositorioOperacion.Modificar(area);
            _repositorioOperacion.GuardarCambios();
            return ObtenerAreaDTOEncriptado(area.CodigoArea);
        }
        /// <summary>
        /// Método que inhabilita a las áreas
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las áreas a 
        /// inhabilitar</param>
        /// <returns>Resultado encriptado con el listado de áreas inhabilitadas</returns>
        public string InhabilitarAreas(JToken encriptado)
        {
            var areasDTO = _servicioDesencriptador.Desencriptar<List<DtoArea>>(encriptado.ToString());
            _servicioDominioArea.ValidarAreasSeleccionadas(areasDTO.Count);
            InhabilitarListadoDeAreas(areasDTO);
            _repositorioOperacion.GuardarCambios();
            return ObtenerAreasDTOEncriptados(true, false);
        }
        /// <summary>
        /// Método que habilita a las áreas
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado de las áreas a 
        /// habilitar</param>
        /// <returns>Resultado encriptado con el listado de áreas habilitadas</returns>
        public string HabilitarAreas(JToken encriptado)
        {
            var areasDTO = _servicioDesencriptador.Desencriptar<List<DtoArea>>(encriptado.ToString());
            _servicioDominioArea.ValidarAreasSeleccionadas(areasDTO.Count);
            HabilitarListadoDeAreas(areasDTO);
            _repositorioOperacion.GuardarCambios();
            return ObtenerAreasDTOEncriptados(true);
        }
        #region Métodos privados
        /// <summary>
        /// Método que obtiene el listado de todas las áreas registradas, tanto activos como inactivos
        /// </summary>
        /// <returns>Resultado encriptado con el listado de las áreas encontradas</returns>
        private string ObtenerListadoDeTodasLasAreas()
        {
            return ObtenerAreasDTOEncriptados();
        }
        /// <summary>
        /// Método que obtiene el listado de las áreas de una sede, tanto activos como inactivos
        /// </summary>
        /// <param name="codigoSedeEncriptado">Código de la sede (encriptado)</param>
        /// <returns>Resultado encriptado con el listado de las áreas encontradas</returns>
        private string ObtenerListadoDeAreasSegunUnaSede(string codigoSedeEncriptado)
        {
            var codigoSede = _servicioDesencriptador.Desencriptar<int>(codigoSedeEncriptado);
            return ObtenerAreasDTOEncriptados(false, true, codigoSede);
        }
        /// <summary>
        /// Método que inhabilita el listado de áreas
        /// </summary>
        /// <param name="sedesDTO">Listado de áreas a inhabilitar</param>
        private void InhabilitarListadoDeAreas(List<DtoArea> areasDTO)
        {
            foreach (var areaDTO in areasDTO)
            {
                var area = _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<Area>(g => 
                    g.CodigoArea == _servicioDesencriptador.Desencriptar<int>(areaDTO.CodigoArea))
                    .FirstOrDefault();
                area = _servicioDominioArea.InhabilitarArea(area);
                _repositorioOperacion.Modificar(area);
                RemoverRelacionEntreElAreaYLosEquipoBiometricos(area);
                RemoverRelacionEntreElAreaYElPersonalDeLaEmpresa(area);
            }
        }
        /// <summary>
        /// Método que remueve todas las relaciones hechas entre el área a inhabilitar y los
        /// equipos biométricos
        /// </summary>
        /// <param name="area">área inhabilitada</param>
        private void RemoverRelacionEntreElAreaYLosEquipoBiometricos(Area area)
        {
            var equiposBiometricos =
                _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<EquipoBiometrico>(g =>
                    g.CodigoArea == area.CodigoArea).ToList();
            foreach (var equipoBiometrico in equiposBiometricos)
            {
                var equipoBiometricoSinAsignacion =
                    _servicioDominioArea.AsignarAreaSinAsignacionAEquipoBiometrico(equipoBiometrico);
                _repositorioOperacion.Modificar(equipoBiometricoSinAsignacion);
            }
        }
        /// <summary>
        /// Método que remueve todas las relaciones hechas entre el área a inhabilitar y el 
        /// personal de la empresa
        /// </summary>
        /// <param name="area">área inhabilitada</param>
        private void RemoverRelacionEntreElAreaYElPersonalDeLaEmpresa(Area area)
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
        /// <summary>
        /// Método que habilita el listado de áreas
        /// </summary>
        /// <param name="sedesDTO">Listado de áreas a habilitar</param>
        private void HabilitarListadoDeAreas(List<DtoArea> areasDTO)
        {
            foreach (var areaDTO in areasDTO)
            {
                var area = _repositorioConsulta.ObtenerPorCodigo<Area>(
                    _servicioDesencriptador.Desencriptar<int>(areaDTO.CodigoArea));
                area = _servicioDominioArea.HabilitarArea(area);
                _repositorioOperacion.Modificar(area);
            }
        }
        /// <summary>
        /// Método que obtiene un área DTO encriptado en base a su código de área
        /// </summary>
        /// <param name="codigoArea">Código del área a obtener</param>
        /// <returns>Resultado encriptado con los datos del área encontrada</returns>
        private string ObtenerAreaDTOEncriptado(int codigoArea)
        {
            var area = _repositorioConsulta.ObtenerPorCodigo<Area>(codigoArea);
            var areaDTO = _servicioMapeoAreaADTO.MapearADTO(area);
            return _servicioEncriptador.Encriptar(areaDTO);
        }
        /// <summary>
        /// Método que obtiene un listado de áreas DTO encriptados
        /// </summary>
        /// <param name="filtrarPorEstado">Si se desea filtrar por el indicador de estado
        /// del área (por defecto está inicializado con el valor FALSE)</param>
        /// <param name="indicadorEstado">Indicador de estado a filtrar (por defecto está inicializado
        /// con el valor TRUE)</param>
        /// <param name="codigoSede">Si se desea filtrar por el código de sede asociado 
        /// al área (por defecto está inicializado con el valor 0)</param>
        /// <returns>Resultado encriptado con el listado de áreas encontradas</returns>
        private string ObtenerAreasDTOEncriptados(bool filtrarPorEstado = false, 
            bool indicadorEstado = true, int codigoSede = 0)
        {
            var areas = _repositorioConsulta.ObtenerPorExpresionLimite<Area>(g => 
                filtrarPorEstado ? g.IndicadorEstado == indicadorEstado : true &&
                codigoSede == 0 ? true : g.CodigoSede == codigoSede).ToList();
            var areasDTO = areas.Select(g => _servicioMapeoAreaADTO.MapearADTO(g)).ToList();
            return _servicioEncriptador.Encriptar(areasDTO);
        }
        #endregion
    }
}
