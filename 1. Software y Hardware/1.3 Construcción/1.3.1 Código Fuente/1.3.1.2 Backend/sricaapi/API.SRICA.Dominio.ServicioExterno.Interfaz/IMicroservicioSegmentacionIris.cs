namespace API.SRICA.Dominio.ServicioExterno.Interfaz
{
    /// <summary>
    /// Interfaz del microservicio de segmentación de imágenes de iris
    /// </summary>
    public interface IMicroservicioSegmentacionIris
    {
        /// <summary>
        /// Método que segmenta la imagen de iris
        /// </summary>
        /// <param name="urlServicio">URL del microservicio</param>
        /// <param name="imagenOjoBase64">Imagen del ojo en formato base64</param>
        /// <param name="esAccionPorEquipoBiometrico">Si el proceso es generado por el
        /// equipo biométrico</param>
        /// <returns>Imagen del iris segmentado en base64</returns>
        string SegmentarIrisEnImagen(string urlServicio, string imagenOjoBase64, 
            bool esAccionPorEquipoBiometrico = false);
    }
}