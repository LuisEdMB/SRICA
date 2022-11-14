using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.AR;
using API.SRICA.Dominio.Entidad.PE;
using API.SRICA.Dominio.Entidad.SE;
using API.SRICA.Dominio.Interfaz;
using API.SRICA.Dominio.Servicio.Interfaz;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using API.SRICA.Dominio.Servicio.Implementacion;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación del servicio de consultas y operaciones de personal de la empresa
    /// </summary>
    public class ServicioPersonalEmpresa : IServicioPersonalEmpresa
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
        /// Servicio de dominio del personal de la empresa
        /// </summary>
        private readonly IServicioDominioPersonalEmpresa _servicioDominioPersonalEmpresa;
        /// <summary>
        /// Servicio de tratamiento de archivos
        /// </summary>
        private readonly IServicioArchivo _servicioArchivo;
        /// <summary>
        /// Servicio para el tratamiento de imágenes de iris
        /// (procesos de detección y reconocimiento de iris)
        /// </summary>
        private readonly IServicioIris _servicioIris;
        /// <summary>
        /// Servicio de mapeo del personal de la empresa a DTO
        /// </summary>
        private readonly IServicioMapeoPersonalEmpresaADto _servicioMapeoPersonalEmpresaADTO;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioDesencriptador">Servicio para la desencriptación de datos</param>
        /// <param name="repositorioConsulta">Repositorio de consultas a la base de datos</param>
        /// <param name="repositorioOperacion">Repositorio de operaciones a la base de datos</param>
        /// <param name="servicioDominioPersonalEmpresa">Servicio de dominio del personal de 
        /// la empresa</param>
        /// <param name="servicioArchivo">Servicio de tratamiento de archivos</param>
        /// <param name="servicioIris">Servicio para el tratamiento de imágenes de iris
        /// (procesos de detección y reconocimiento de iris)</param>
        /// <param name="servicioMapeoPersonalEmpresaADTO">Servicio de mapeo del personal de la 
        /// empresa a DTO</param>
        public ServicioPersonalEmpresa(IServicioEncriptador servicioEncriptador,
            IServicioDesencriptador servicioDesencriptador,
            IRepositorioConsulta repositorioConsulta,
            IRepositorioOperacion repositorioOperacion,
            IServicioDominioPersonalEmpresa servicioDominioPersonalEmpresa,
            IServicioArchivo servicioArchivo,
            IServicioIris servicioIris,
            IServicioMapeoPersonalEmpresaADto servicioMapeoPersonalEmpresaADTO)
        {
            _servicioEncriptador = servicioEncriptador;
            _servicioDesencriptador = servicioDesencriptador;
            _repositorioConsulta = repositorioConsulta;
            _repositorioOperacion = repositorioOperacion;
            _servicioDominioPersonalEmpresa = servicioDominioPersonalEmpresa;
            _servicioArchivo = servicioArchivo;
            _servicioIris = servicioIris;
            _servicioMapeoPersonalEmpresaADTO = servicioMapeoPersonalEmpresaADTO;
        }
        /// <summary>
        /// Método que obtiene el listado del personal de la empresa registrado, tanto activos 
        /// como inactivos
        /// </summary>
        /// <returns>Resultado encriptado con el listado del personal de la empresa</returns>
        public string ObtenerListadoDePersonalDeLaEmpresa()
        {
            return ObtenerPersonalEmpresaDTOEncriptados();
        }
        /// <summary>
        /// Método que obtiene un personal de la empresa en base a su código de personal
        /// de la empresa
        /// </summary>
        /// <param name="codigoPersonalEmpresaEncriptado">Código del personal de la empresa 
        /// a obtener (encriptado)</param>
        /// <returns>Resultado encriptado con los datos del personal de la empresa encontrado</returns>
        public string ObtenerPersonalDeLaEmpresa(string codigoPersonalEmpresaEncriptado)
        {
            return ObtenerPersonalEmpresaDTOEncriptado(
                _servicioDesencriptador.Desencriptar<int>(codigoPersonalEmpresaEncriptado));
        }
        /// <summary>
        /// Método que guarda un personal de la empresa, o registra masivamente al personal en base
        /// a un archivo
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del personal de 
        /// la empresa a guardar, o el archivo con el listado del personal a guardar</param>
        /// <returns>Resultado encriptado con los datos del personal de la empresa guardado(s)</returns>
        public string GuardarPersonalEmpresa(JToken encriptado)
        {
            var personalEmpresaDTO = _servicioDesencriptador.Desencriptar<DtoPersonalEmpresa>(
                encriptado.ToString());
            if (personalEmpresaDTO.EsRegistroMasivo)
                return RegistrarPersonalEmpresaMasivo(personalEmpresaDTO.ArchivoRegistroMasivo);
            return RegistrarPersonalEmpresa(personalEmpresaDTO);
        }
        /// <summary>
        /// Método que modifica un personal de la empresa
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene los datos del personal de 
        /// la empresa a modificar</param>
        /// <returns>Resultado encriptado con los datos del personal de la empresa modificado</returns>
        public string ModificarPersonalEmpresa(JToken encriptado)
        {
            var personalEmpresaDTO = _servicioDesencriptador.Desencriptar<DtoPersonalEmpresa>(
                encriptado.ToString());
            var imagenIris = personalEmpresaDTO.ImagenIris?.RemoverPrefijoDeBase64() ?? string.Empty;
            var personalEmpresaRegistrados =
                _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<PersonalEmpresa>().ToList();
            var personalEmpresa =
                _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<PersonalEmpresa>(g =>
                    g.CodigoPersonalEmpresa == _servicioDesencriptador.Desencriptar<int>(
                        personalEmpresaDTO.CodigoPersonalEmpresa)).FirstOrDefault();
            personalEmpresa = _servicioDominioPersonalEmpresa.ModificarPersonalEmpresa(personalEmpresa,
                personalEmpresaRegistrados, personalEmpresaDTO.DNIPersonalEmpresa,
                personalEmpresaDTO.NombrePersonalEmpresa, personalEmpresaDTO.ApellidoPersonalEmpresa,
                personalEmpresaDTO.Areas.Where(g => g.Seleccionado).ToList().Count);
            if (!string.IsNullOrEmpty(imagenIris))
            {
                var irisCodificado = CodificarImagenDeIris(imagenIris);
                personalEmpresa.AgregarImagenDeIrisCodificada(irisCodificado);
            }
            _repositorioOperacion.Modificar(personalEmpresa);
            GuardarAreasDelPersonalDeLaEmpresa(personalEmpresa, personalEmpresaDTO.Areas);
            InactivarAreasDelPersonalDeLaEmpresa(personalEmpresaDTO.Areas);
            _repositorioOperacion.GuardarCambios();
            return ObtenerPersonalEmpresaDTOEncriptado(personalEmpresa.CodigoPersonalEmpresa);
        }
        /// <summary>
        /// Método que inhabilita un listado de personal de la empresa
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado del personal de la empresa
        /// a inhabilitar</param>
        /// <returns>Resultado encriptado con el listado del personal de la empresa inhabilitados</returns>
        public string InhabilitarListadoDePersonalEmpresa(JToken encriptado)
        {
            var personalEmpresaDTO =
                _servicioDesencriptador.Desencriptar<List<DtoPersonalEmpresa>>(
                    encriptado.ToString());
            _servicioDominioPersonalEmpresa.ValidarPersonalEmpresaSeleccionados(
                personalEmpresaDTO.Count);
            InhabilitarListadoPersonalEmpresa(personalEmpresaDTO);
            _repositorioOperacion.GuardarCambios();
            return ObtenerPersonalEmpresaDTOEncriptados(true, false);
        }
        /// <summary>
        /// Método que habilita un listado de personal de la empresa
        /// </summary>
        /// <param name="encriptado">Datos encriptados que contiene el listado del personal de la empresa
        /// a habilitar</param>
        /// <returns>Resultado encriptado con el listado del personal de la empresa habilitados</returns>
        public string HabilitarListadoDePersonalEmpresa(JToken encriptado)
        {
            var personalEmpresaDTO =
                    _servicioDesencriptador.Desencriptar<List<DtoPersonalEmpresa>>(
                        encriptado.ToString());
            _servicioDominioPersonalEmpresa.ValidarPersonalEmpresaSeleccionados(
                personalEmpresaDTO.Count);
            HabilitarListadoPersonalEmpresa(personalEmpresaDTO);
            _repositorioOperacion.GuardarCambios();
            return ObtenerPersonalEmpresaDTOEncriptados(true, false);
        }
        #region Métodos privados
        /// <summary>
        /// Método que registra al personal de la empresa según los ingresados en el archivo
        /// </summary>
        /// <param name="archivoRegistroMasivo">Archivo en base64</param>
        /// <returns>Resultado encriptado con los datos del personal de la empresa registrados</returns>
        private string RegistrarPersonalEmpresaMasivo(string archivoRegistroMasivo)
        {
            var extension = _servicioArchivo.ObtenerExtension(archivoRegistroMasivo);
            _servicioDominioPersonalEmpresa.ValidarExtensionArchivoRegistroMasivo(extension);
            var dataset = _servicioArchivo.ConvertirBase64AExcelDataSet(archivoRegistroMasivo);
            var columnasValidas = _servicioDominioPersonalEmpresa.ValidarColumnasArchivoRegistroMasivo(
                dataset[0].Columns.Cast<DataColumn>().Select(g => g.ColumnName).ToList());
            var datos = RecorrerFilasArchivoRegistroMasivo(dataset, columnasValidas);
            var personalEmpresaRegistrados =
                _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<PersonalEmpresa>().ToList();
            var listadoPersonalEmpresa = _servicioDominioPersonalEmpresa.CrearPersonalEmpresaDesdeListado(
                datos, personalEmpresaRegistrados);
            _repositorioOperacion.AgregarLista(listadoPersonalEmpresa);
            _repositorioOperacion.GuardarCambios();
            return ObtenerPersonalEmpresaDTOEncriptadosDesdeEntidades(listadoPersonalEmpresa);
        }
        /// <summary>
        /// Método que registra un personal de la empresa
        /// </summary>
        /// <param name="personalEmpresaDTO">Datos del personal de la empresa a registrar</param>
        /// <returns>Resultado encriptado con los datos del personal de la empresa registrado</returns>
        private string RegistrarPersonalEmpresa(DtoPersonalEmpresa personalEmpresaDTO)
        {
            var personalEmpresaRegistrados =
                _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<PersonalEmpresa>().ToList();
            var imagenIris = personalEmpresaDTO.ImagenIris?.RemoverPrefijoDeBase64() ?? string.Empty;
            var personalEmpresa = _servicioDominioPersonalEmpresa.CrearPersonalEmpresa(
                personalEmpresaRegistrados,
                personalEmpresaDTO.DNIPersonalEmpresa,
                personalEmpresaDTO.NombrePersonalEmpresa, personalEmpresaDTO.ApellidoPersonalEmpresa,
                personalEmpresaDTO.Areas.Where(g => g.Seleccionado).ToList().Count,
                imagenIris);
            var irisCodificado = CodificarImagenDeIris(imagenIris);
            personalEmpresa.AgregarImagenDeIrisCodificada(irisCodificado);
            _repositorioOperacion.Agregar(personalEmpresa);
            _repositorioOperacion.GuardarCambios();
            GuardarAreasDelPersonalDeLaEmpresa(personalEmpresa, personalEmpresaDTO.Areas);
            _repositorioOperacion.GuardarCambios();
            return ObtenerPersonalEmpresaDTOEncriptado(personalEmpresa.CodigoPersonalEmpresa);
        }
        /// <summary>
        /// Método que codifica la imagen de iris
        /// </summary>
        /// <param name="imagenOjoBase64">Imagen del ojo en formato base64</param>
        /// <returns>Imagen de iris codificado</returns>
        private byte[] CodificarImagenDeIris(string imagenOjoBase64)
        {
            var segmentacion = _servicioIris.SegmentarIrisEnImagen(imagenOjoBase64);
            return _servicioIris.CodificarIrisEnImagen(segmentacion);
        }
        /// <summary>
        /// Método que guarda las áreas nuevas asignadas al personal de la empresa
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa a quién pertenece las áreas</param>
        /// <param name="areas">Áreas a asignar</param>
        private void GuardarAreasDelPersonalDeLaEmpresa(PersonalEmpresa personalEmpresa,
            List<DtoPersonalEmpresaXArea> areas)
        {
            var areasSeleccionadasDTO = areas.Where(g => g.Seleccionado && g.Nuevo).ToList();
            foreach(var areaDTO in areasSeleccionadasDTO)
            {
                var area = _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<Area>(g =>
                    g.CodigoArea == _servicioDesencriptador.Desencriptar<int>(areaDTO.CodigoArea))
                    .FirstOrDefault();
                var sede = _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<Sede>(g =>
                    g.CodigoSede == area.CodigoSede).FirstOrDefault();
                var relacionAreaCreada = _servicioDominioPersonalEmpresa.CrearRelacionArea(personalEmpresa,
                    sede, area);
                _repositorioOperacion.Agregar(relacionAreaCreada);
            }
        }
        /// <summary>
        /// Método que inactiva la relación entre el personal de la empresa y sus áreas (solo
        /// de áreas que han sido deseleccionadas)
        /// </summary>
        /// <param name="areas">Relaciones de áreas a inactivar</param>
        private void InactivarAreasDelPersonalDeLaEmpresa(List<DtoPersonalEmpresaXArea> areas)
        {
            var areasAInactivarDTO = areas.Where(g => !g.Seleccionado && !g.Nuevo).ToList();
            foreach(var areaDTO in areasAInactivarDTO)
            {
                var relacionAreaAInactivar =
                    _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<PersonalEmpresaXArea>(g =>
                        g.CodigoPersonalEmpresaXArea == _servicioDesencriptador.Desencriptar<int>(
                            areaDTO.CodigoPersonalEmpresaXArea)).FirstOrDefault();
                relacionAreaAInactivar = _servicioDominioPersonalEmpresa.InactivarRelacionArea(
                    relacionAreaAInactivar);
                _repositorioOperacion.Modificar(relacionAreaAInactivar);
            }
        }
        /// <summary>
        /// Método que inhabilita el listado de personal de la empresa
        /// </summary>
        /// <param name="listadoPersonalEmpresaDTO">Listado de personal de la empresa a inhabilitar</param>
        private void InhabilitarListadoPersonalEmpresa(List<DtoPersonalEmpresa> listadoPersonalEmpresaDTO)
        {
            foreach (var personalEmpresaDTO in listadoPersonalEmpresaDTO)
            {
                var personalEmpresa =
                    _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<PersonalEmpresa>(
                        g => g.CodigoPersonalEmpresa == _servicioDesencriptador.Desencriptar<int>(
                            personalEmpresaDTO.CodigoPersonalEmpresa)).FirstOrDefault();
                personalEmpresa = _servicioDominioPersonalEmpresa.InhabilitarPersonalEmpresa(
                    personalEmpresa);
                _repositorioOperacion.Modificar(personalEmpresa);
            }
        }
        /// <summary>
        /// Método que habilita el listado de personal de la empresa
        /// </summary>
        /// <param name="listadoPersonalEmpresaDTO">Listado de personal de la empresa a habilitar</param>
        private void HabilitarListadoPersonalEmpresa(List<DtoPersonalEmpresa> listadoPersonalEmpresaDTO)
        {
            foreach (var personalEmpresaDTO in listadoPersonalEmpresaDTO)
            {
                var personalEmpresa =
                    _repositorioConsulta.ObtenerPorExpresionLimiteNoTracking<PersonalEmpresa>(
                        g => g.CodigoPersonalEmpresa == _servicioDesencriptador.Desencriptar<int>(
                            personalEmpresaDTO.CodigoPersonalEmpresa)).FirstOrDefault();
                personalEmpresa = _servicioDominioPersonalEmpresa.HabilitarPersonalEmpresa(
                    personalEmpresa);
                _repositorioOperacion.Modificar(personalEmpresa);
            }
        }
        /// <summary>
        /// Método que obtiene un personal de la empresa DTO encriptado en base a su código de 
        /// personal
        /// </summary>
        /// <param name="codigoPersonalEmpresa">Código del personal a obtener</param>
        /// <returns>Resultado encriptado con los datos del personal de la empresa encontrado</returns>
        private string ObtenerPersonalEmpresaDTOEncriptado(int codigoPersonalEmpresa)
        {
            var personalEmpresa = _repositorioConsulta.ObtenerPorCodigo<PersonalEmpresa>(
                codigoPersonalEmpresa);
            var personalEmpresaDTO = _servicioMapeoPersonalEmpresaADTO.MapearADTO(personalEmpresa);
            return _servicioEncriptador.Encriptar(personalEmpresaDTO);
        }
        /// <summary>
        /// Método que obtiene un listado de personal de la empresa DTO encriptados
        /// </summary>
        /// <param name="filtrarPorEstado">Si se desea filtrar por el indicador de estado
        /// del personal de la empresa (por defecto está inicializado con el valor FALSE)</param>
        /// <param name="indicadorEstado">Indicador de estado a filtrar (por defecto está inicializado
        /// con el valor TRUE)</param>
        /// <returns>Resultado encriptado con el listado de personal de la empresa encontrados</returns>
        private string ObtenerPersonalEmpresaDTOEncriptados(bool filtrarPorEstado = false,
            bool indicadorEstado = true)
        {
            var personalEmpresa =
                _repositorioConsulta.ObtenerPorExpresionLimite<PersonalEmpresa>(g =>
                    filtrarPorEstado ? g.IndicadorEstado == indicadorEstado : true).ToList();
            var personalEmpresaDTO = personalEmpresa.Select(g =>
                _servicioMapeoPersonalEmpresaADTO.MapearADTO(g)).ToList();
            return _servicioEncriptador.Encriptar(personalEmpresaDTO);
        }
        /// <summary>
        /// Método que obtiene un listado de personal de la empresa DTO encriptados desde un listado
        /// de entidades de personal de la empresa
        /// </summary>
        /// <param name="listadoPersonalEmpresa">Listado de entidades de personal de la empresa
        /// a ser convertido</param>
        /// <returns>Resultado encriptado con el listado de personal de la empresa especificados</returns>
        private string ObtenerPersonalEmpresaDTOEncriptadosDesdeEntidades(
            List<PersonalEmpresa> listadoPersonalEmpresa)
        {
            var listadoPersonalEmpresaDTO = listadoPersonalEmpresa.Select(g =>
                _servicioMapeoPersonalEmpresaADTO.MapearADTO(g)).ToList();
            return _servicioEncriptador.Encriptar(listadoPersonalEmpresaDTO);
        }
        /// <summary>
        /// Método que recorre el dataset que contiene listado del personal de la empresa a registrar
        /// </summary>
        /// <param name="dataset">Dataset con el listado del personal de la empresa a registrar</param>
        /// <param name="columnas">Columnas válidas a considerar en el dataset</param>
        /// <returns>Resultado con el listado del personal de la empresa a registrar</returns>
        private Tuple<string, string, string, List<Tuple<string, string, string>>> 
            RecorrerFilasArchivoRegistroMasivo(DataTableCollection dataset, List<string> columnas)
        {
            var data = dataset[0].DefaultView.ToTable(false, columnas.ToArray());
            Tuple<string, string, string, List<Tuple<string, string, string>>> resultado;
            List<Tuple<string, string, string>> filas = new List<Tuple<string, string, string>>();
            foreach (DataRow fila in data.Rows)
            {
                filas.Add(new Tuple<string, string, string>(
                    fila[0].ToString(),
                    fila[1].ToString(),
                    fila[2].ToString()));
            }
            resultado = new Tuple<string, string, string, List<Tuple<string, string, string>>>(
                columnas[0], columnas[1], columnas[2], filas);
            return resultado;
        }
        #endregion
    }
}
