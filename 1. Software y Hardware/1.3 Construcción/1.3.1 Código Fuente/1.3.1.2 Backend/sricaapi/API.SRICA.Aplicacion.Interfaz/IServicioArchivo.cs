using System.Data;

namespace API.SRICA.Aplicacion.Interfaz
{
    /// <summary>
    /// Interfaz para el servicio de tratamiento de archivos
    /// </summary>
    public interface IServicioArchivo
    {
        /// <summary>
        /// Método que obtiene la extensión de un archivo
        /// </summary>
        /// <param name="archivoBase64">Archivo en base64</param>
        /// <returns>Extensión del archivo</returns>
        string ObtenerExtension(string archivoBase64);
        /// <summary>
        /// Método que convierte un archivo en base 64 a un dataset con los datos
        /// del archivo (excel)
        /// </summary>
        /// <param name="archivoBase64">Archivo en base64 (con formato excel)</param>
        /// <returns>Dataset del archivo</returns>
        DataTableCollection ConvertirBase64AExcelDataSet(string archivoBase64);
    }
}
