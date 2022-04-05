using System;
using System.Linq;
using System.Text;
using API.SRICA.Aplicacion.DTO;
using API.SRICA.Aplicacion.Interfaz;
using API.SRICA.Aplicacion.Interfaz.MapeoDTO;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.PE;
using API.SRICA.Dominio.Excepcion;
using API.SRICA.Dominio.Interfaz;
using API.SRICA.Dominio.Servicio.Interfaz;
using API.SRICA.Dominio.ServicioExterno.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación del servicio para el tratamiento de imágenes de iris
    /// (procesos de segmentación, codificación, y reconocimiento de iris)
    /// </summary>
    public class ServicioIris : IServicioIris
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
        /// Microservicio de segmentación de iris
        /// </summary>
        private readonly IMicroservicioSegmentacionIris _microservicioSegmentacionIris;
        /// <summary>
        /// Microservicio de codificación de iris
        /// </summary>
        private readonly IMicroservicioCodificacionIris _microservicioCodificacionIris;
        /// <summary>
        /// Microservicio de reconocimiento de iris
        /// </summary>
        private readonly IMicroservicioReconocimientoIris _microservicioReconocimientoIris;
        /// <summary>
        /// Servicio de dominio de equipos biométricos
        /// </summary>
        private readonly IServicioDominioEquipoBiometrico _servicioDominioEquipoBiometrico;
        /// <summary>
        /// Servicio de dominio de personal de la empresa
        /// </summary>
        private readonly IServicioDominioPersonalEmpresa _servicioDominioPersonalEmpresa;
        /// <summary>
        /// Servicio de mapeo a DTO para el personal de la empresa
        /// </summary>
        private readonly IServicioMapeoPersonalEmpresaADto _servicioMapeoPersonalEmpresaADto;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="configuracion">Configuración del proyecto</param>
        /// <param name="servicioEncriptador">Servicio para la encriptación de datos</param>
        /// <param name="servicioDesencriptador">Servicio para la desencriptación de datos</param>
        /// <param name="repositorioConsulta">Repositorio de consultas a la base de datos</param>
        /// <param name="microservicioSegmentacionIris">Microservicio de segmentación de iris</param>
        /// <param name="microservicioCodificacionIris">Microservicio de codificación de iris</param>
        /// <param name="microservicioReconocimientoIris">Microservicio de reconocimiento de iris</param>
        /// <param name="servicioDominioEquipoBiometrico">Servicio de dominio de equipos biométricos</param>
        /// <param name="servicioDominioPersonalEmpresa">Servicio de dominio de personal de la
        /// empresa</param>
        /// <param name="servicioMapeoPersonalEmpresaADto">Servicio de mapeo a DTO para el personal
        /// de la empresa</param>
        public ServicioIris(IConfiguration configuracion,
            IServicioEncriptador servicioEncriptador,
            IServicioDesencriptador servicioDesencriptador,
            IRepositorioConsulta repositorioConsulta,
            IMicroservicioSegmentacionIris microservicioSegmentacionIris,
            IMicroservicioCodificacionIris microservicioCodificacionIris,
            IMicroservicioReconocimientoIris microservicioReconocimientoIris,
            IServicioDominioEquipoBiometrico servicioDominioEquipoBiometrico,
            IServicioDominioPersonalEmpresa servicioDominioPersonalEmpresa,
            IServicioMapeoPersonalEmpresaADto servicioMapeoPersonalEmpresaADto)
        {
            _configuracion = configuracion;
            _servicioEncriptador = servicioEncriptador;
            _servicioDesencriptador = servicioDesencriptador;
            _repositorioConsulta = repositorioConsulta;
            _microservicioSegmentacionIris = microservicioSegmentacionIris;
            _microservicioCodificacionIris = microservicioCodificacionIris;
            _microservicioReconocimientoIris = microservicioReconocimientoIris;
            _servicioDominioEquipoBiometrico = servicioDominioEquipoBiometrico;
            _servicioDominioPersonalEmpresa = servicioDominioPersonalEmpresa;
            _servicioMapeoPersonalEmpresaADto = servicioMapeoPersonalEmpresaADto;
        }
        /// <summary>
        /// Método que segmenta la imagen de iris
        /// </summary>
        /// <param name="imagenOjoBase64">Imagen del ojo en formato base64</param>
        /// <param name="esAccionPorEquipoBiometrico">Si el proceso es generado por el
        /// equipo biométrico</param>
        /// <returns>Imagen de iris segmentado en base64</returns>
        public string SegmentarIrisEnImagen(string imagenOjoBase64, bool esAccionPorEquipoBiometrico = false)
        {
            return _microservicioSegmentacionIris.SegmentarIrisEnImagen(
                _configuracion["MICROSERVICIO_SEGMENTACION_IRIS_URL"],
                imagenOjoBase64 ?? string.Empty, esAccionPorEquipoBiometrico);
        }
        /// <summary>
        /// Método que codifica la imagen de iris
        /// </summary>
        /// <param name="imagenIrisSegmentadoBase64">Imagen del iris segmentado en formato base64</param>
        /// <param name="esAccionPorEquipoBiometrico">Si el proceso es generado por el
        /// equipo biométrico</param>
        /// <returns>Imagen del iris codificado en arreglo de bytes</returns>
        public byte[] CodificarIrisEnImagen(string imagenIrisSegmentadoBase64, 
            bool esAccionPorEquipoBiometrico = false)
        {
            return _microservicioCodificacionIris.CodificarIrisEnImagen(
                _configuracion["MICROSERVICIO_CODIFICACION_IRIS_URL"],
                imagenIrisSegmentadoBase64 ?? string.Empty, esAccionPorEquipoBiometrico);
        }
        /// <summary>
        /// Método que procesa el iris para reconocer al personal, desde el equipo biométrico
        /// </summary>
        /// <param name="encriptado">Objeto encriptado que contiene las imágenes de iris a utilizar
        /// en el proceso de reconocimiento, y la MAC del equipo biométrico</param>
        /// <returns>Resultado encriptado con el código del personal reconocido</returns>
        public DtoPersonalEmpresaReconocimiento ReconocerPersonalPorElIrisViaEquipoBiometrico(JToken datos)
        {
            EquipoBiometrico equipoBiometrico = null;
            PersonalEmpresa personalEmpresa = null;
            var fotoPersonal = string.Empty;
            try
            {
                var personalReconocimiento = JsonConvert.DeserializeObject<DtoPersonalEmpresaReconocimiento>(
                    datos.ToString());
                fotoPersonal = personalReconocimiento.ImagenOriginal;
                equipoBiometrico = ObtenerEquipoBiometricoParaReconocimiento(
                    personalReconocimiento.DireccionMacEquipoBiometrico);
                _servicioDominioEquipoBiometrico.ValidarEquipoBiometricoParaReconocimientoDePersonal(
                    equipoBiometrico, personalReconocimiento.DireccionMacEquipoBiometrico);
                var irisSegmentado = SegmentarIrisEnImagen(personalReconocimiento.ImagenOjo, true);
                var irisCodificado = CodificarIrisEnImagen(irisSegmentado, true);
                var codigoPersonalReconocido = _microservicioReconocimientoIris.ReconocerIrisDePersonal(
                    _configuracion["MICROSERVICIO_RECONOCIMIENTO_IRIS_URL"],
                    Encoding.UTF8.GetString(irisCodificado));
                personalEmpresa = ObtenerPersonalParaReconocimiento(codigoPersonalReconocido);
                _servicioDominioPersonalEmpresa.ValidarPersonalDeLaEmpresaParaReconocimientoDePersonal(
                    personalEmpresa, equipoBiometrico);
                return _servicioMapeoPersonalEmpresaADto.MapearADTO(personalEmpresa, equipoBiometrico);
            }
            catch (ExcepcionAplicacionEquipoBiometricoPersonalizada excepcion)
            {
                throw new ExcepcionAplicacionEquipoBiometricoPersonalizada(excepcion.Message,
                    excepcion.CodigoExcepcion, personalEmpresa, equipoBiometrico, fotoPersonal);
            }
            catch (ExcepcionEquipoBiometricoPersonalizada excepcion)
            {
                throw new ExcepcionEquipoBiometricoPersonalizada(excepcion.Message,
                    personalEmpresa, equipoBiometrico, excepcion.InnerException);
            }
        }

        /// <summary>
        /// Método que procesa el iris para reconocer al personal (con token), desde la web
        /// </summary>
        /// <param name="encriptado">Objeto encriptado que contiene las imágenes de iris a utilizar
        /// en el proceso de reconocimiento</param>
        /// <returns>Resultado encriptado con el código del personal reconocido</returns>
        public string ReconocerPersonalPorElIrisViaWeb(JToken datos)
        {
            var personalReconocimiento = _servicioDesencriptador.Desencriptar<DtoPersonalEmpresaReconocimiento>(datos.ToString());
            var irisSegmentado = SegmentarIrisEnImagen(personalReconocimiento.ImagenOjo, true);
            var irisCodificado = CodificarIrisEnImagen(irisSegmentado, true);
            var codigoPersonalReconocido = _microservicioReconocimientoIris.ReconocerIrisDePersonal(
                _configuracion["MICROSERVICIO_RECONOCIMIENTO_IRIS_URL"],
                Encoding.UTF8.GetString(irisCodificado));
            var personalEmpresa = ObtenerPersonalParaReconocimiento(codigoPersonalReconocido);
            var personalEmpresaDto = _servicioMapeoPersonalEmpresaADto.MapearADTO(personalEmpresa, null);
            return _servicioEncriptador.Encriptar(personalEmpresaDto);
        }
        #region Métodos privados
        /// <summary>
        /// Método que obtiene el equipo biométrico que realiza el proceso de reconocimiento, mediante
        /// su dirección MAC física
        /// </summary>
        /// <param name="direccionMac">Dirección MAC del equipo biométrico</param>
        /// <returns>Datos del equipo biométrico</returns>
        private EquipoBiometrico ObtenerEquipoBiometricoParaReconocimiento(string direccionMac)
        {
            var direccionMacEncriptado = _servicioEncriptador.Encriptar(direccionMac.ToUpper());
            var equiposBiometricos = 
                _repositorioConsulta.ObtenerPorExpresionLimite<EquipoBiometrico>().ToList();
            return equiposBiometricos.FirstOrDefault(g => 
                g.DireccionFisicaEquipoBiometrico.Equals(direccionMacEncriptado));
        }
        /// <summary>
        /// Método que obtiene un personal de la empresa para el proceso de reconocimiento, mediante
        /// su código de registro
        /// </summary>
        /// <param name="codigoPersonal">Código del personal</param>
        /// <returns>Datos del personal</returns>
        private PersonalEmpresa ObtenerPersonalParaReconocimiento(string codigoPersonal)
        {
            var codigoPersonalInt = Convert.ToInt32(string.IsNullOrEmpty(codigoPersonal) 
                ? "0" : codigoPersonal);
            return _repositorioConsulta.ObtenerPorCodigo<PersonalEmpresa>(codigoPersonalInt);
        }
        #endregion
    }
}