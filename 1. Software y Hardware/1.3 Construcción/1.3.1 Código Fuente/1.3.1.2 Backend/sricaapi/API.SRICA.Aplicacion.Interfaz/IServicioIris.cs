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
        /// Método que reconoce a un personal registrado mediante las imágenes de iris
        /// </summary>
        /// <param name="datos">Objeto encriptado que contiene las imágenes de iris</param>
        /// <returns>Datos del personal reconocido</returns>
        DtoPersonalEmpresaReconocimiento ReconocerPersonalPorElIris(JToken datos);
    }
}