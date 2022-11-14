namespace API.SRICA.Dominio.ServicioExterno.Interfaz
{
    /// <summary>
    /// Interfaz del microservicio de codificación de imágenes de iris
    /// </summary>
    public interface IMicroservicioCodificacionIris
    {
        /// <summary>
        /// Método que codifica la imagen de iris
        /// </summary>
        /// <param name="imagenIrisSegmentadoBase64">Imagen del iris segmentado
        /// en formato base64</param>
        /// <param name="esAccionPorEquipoBiometrico">Si el proceso es generado por el
        /// equipo biométrico</param>
        /// <returns>Imagen de iris codificado en arreglo de bytes</returns>
        byte[] CodificarIrisEnImagen(string urlServicio, string imagenIrisSegmentadoBase64, 
            bool esAccionPorEquipoBiometrico = false);
    }
}