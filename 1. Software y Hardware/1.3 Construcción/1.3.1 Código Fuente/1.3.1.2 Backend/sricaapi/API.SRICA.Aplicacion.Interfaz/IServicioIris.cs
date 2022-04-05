using API.SRICA.Aplicacion.DTO;
using Newtonsoft.Json.Linq;

namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz del servicio para el tratamiento de imágenes de iris
    /// (procesos de segmentación, codificación, y reconocimiento de iris)
    /// </summary>
    public interface IServicioIris
    {
        /// <summary>
        /// Método que segmenta la imagen de iris
        /// </summary>
        /// <param name="imagenOjoBase64">Imagen del ojo en formato base64</param>
        /// <param name="esAccionPorEquipoBiometrico">Si el proceso es generado por el
        /// equipo biométrico</param>
        /// <returns>Imagen de iris segmentado en base64</returns>
        string SegmentarIrisEnImagen(string imagenOjoBase64, bool esAccionPorEquipoBiometrico = false);
        /// <summary>
        /// Método que codifica la imagen de iris
        /// </summary>
        /// <param name="imagenIrisSegmentadoBase64">Imagen del iris segmentado en formato base64</param>
        /// <param name="esAccionPorEquipoBiometrico">Si el proceso es generado por el
        /// equipo biométrico</param>
        /// <returns>Imagen del iris codificado en arreglo de bytes</returns>
        byte[] CodificarIrisEnImagen(string imagenIrisSegmentadoBase64, 
            bool esAccionPorEquipoBiometrico = false);
        /// <summary>
        /// Método que procesa el iris para reconocer al personal, desde el equipo biométrico
        /// </summary>
        /// <param name="encriptado">Objeto encriptado que contiene las imágenes de iris a utilizar
        /// en el proceso de reconocimiento, y la MAC del equipo biométrico</param>
        /// <returns>Resultado encriptado con el código del personal reconocido</returns>
        DtoPersonalEmpresaReconocimiento ReconocerPersonalPorElIrisViaEquipoBiometrico(JToken datos);
        /// <summary>
        /// Método que procesa el iris para reconocer al personal (con token), desde la web
        /// </summary>
        /// <param name="encriptado">Objeto encriptado que contiene las imágenes de iris a utilizar
        /// en el proceso de reconocimiento</param>
        /// <returns>Resultado encriptado con el código del personal reconocido</returns>
        string ReconocerPersonalPorElIrisViaWeb(JToken datos);
    }
}