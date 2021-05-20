namespace API.SRICA.Dominio.ServicioExterno.Interfaz
{
    /// <summary>
    /// Interfaz del microservicio de reconocimiento de imágenes de iris
    /// </summary>
    public interface IMicroservicioReconocimientoIris
    {
        /// <summary>
        /// Método que reconoce la imagen de iris del personal
        /// </summary>
        /// <param name="urlServicio">URL del servicio de reconocimiento</param>
        /// <param name="imagenIris">Imagen codificado del iris del personal (formato base64)</param>
        /// <returns>Código del personal reconocido</returns>
        string ReconocerIrisDePersonal(string urlServicio, string imagenIris);
    }
}