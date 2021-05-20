using API.SRICA.Aplicacion.Interfaz;
using ExcelDataReader;
using System;
using System.Data;
using System.IO;

namespace API.SRICA.Aplicacion.Implementacion
{
    /// <summary>
    /// Implementación para el servicio de tratamiento de archivos
    /// </summary>
    public class ServicioArchivo : IServicioArchivo
    {
        /// <summary>
        /// Método que obtiene la extensión de un archivo
        /// </summary>
        /// <param name="archivoBase64">Archivo en base64</param>
        /// <returns>Extensión del archivo</returns>
        public string ObtenerExtension(string archivoBase64)
        {
            char[] delimitadores = { ':', ';', };
            var mimeType = archivoBase64.Split(delimitadores);
            return mimeType.Length > 1 ? mimeType[1] : string.Empty;
        }
        /// <summary>
        /// Método que convierte un archivo en base 64 a un dataset con los datos
        /// del archivo (excel)
        /// </summary>
        /// <param name="archivoBase64">Archivo en base64 (con formato excel)</param>
        /// <returns>Dataset del archivo</returns>
        public DataTableCollection ConvertirBase64AExcelDataSet(string archivoBase64)
        {
            DataTableCollection datatable;
            char[] delimitadores = { ':', ';', ','};
            var archivo = archivoBase64.Split(delimitadores)[3];
            using var stream = new MemoryStream(Convert.FromBase64String(archivo));
            using IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
            DataSet resultado = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            datatable = resultado.Tables;
            return datatable;
        }
    }
}
