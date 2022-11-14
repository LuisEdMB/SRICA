using API.SRICA.Dominio.Entidad.AR;
using API.SRICA.Dominio.Entidad.PE;
using API.SRICA.Dominio.Entidad.SE;
using System;
using System.Collections.Generic;
using API.SRICA.Dominio.Entidad.EB;

namespace API.SRICA.Dominio.Servicio.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de dominio para el personal de la empresa
    /// </summary>
    public interface IServicioDominioPersonalEmpresa
    {
        /// <summary>
        /// Método que crea la entidad personal de la empresa
        /// </summary>
        /// <param name="personalEmpresaRegistrados">Listado de personal de la empresa
        /// registrados, tanto activos como inactivos (para validación de duplicidad)</param>
        /// <param name="dni">DNI del personal de la empresa</param>
        /// <param name="nombre">Nombre del personal de la empresa</param>
        /// <param name="apellido">Apellido del personal de la empresa</param>
        /// <param name="cantidadAreasSeleccionadas">Cantidad de áreas seleccionadas para personal
        /// de la empresa</param>
        /// <param name="imagenIris">Imagen del iris capturado del personal (en base 64)</param>
        /// <returns>Personal de la empresa creado</returns>
        PersonalEmpresa CrearPersonalEmpresa(List<PersonalEmpresa> personalEmpresaRegistrados,
            string dni, string nombre, string apellido, int cantidadAreasSeleccionadas, 
            string imagenIris);
        /// <summary>
        /// Método que modifica la entidad personal de la empresa
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa a modificar</param>
        /// <param name="personalEmpresaRegistrados">Listado de personal de la empresa
        /// registrados, tanto activos como inactivos (para validación de duplicidad)</param>
        /// <param name="dni">DNI del personal de la empresa</param>
        /// <param name="nombre">Nombre del personal de la empresa</param>
        /// <param name="apellido">Apellido del personal de la empresa</param>
        /// <param name="cantidadAreasSeleccionadas">Cantidad de áreas seleccionadas para personal
        /// de la empresa</param>
        /// <returns>Personal de la empresa modificado</returns>
        PersonalEmpresa ModificarPersonalEmpresa(PersonalEmpresa personalEmpresa, 
            List<PersonalEmpresa> personalEmpresaRegistrados,
            string dni, string nombre, string apellido,
            int cantidadAreasSeleccionadas);
        /// <summary>
        /// Método que valida que se haya seleccionado, por lo menos, un personal a ser
        /// inhabilitado o habilitado
        /// </summary>
        /// <param name="cantidadPersonalEmpresaSeleccionados">Cantidad de personal seleccionado</param>
        void ValidarPersonalEmpresaSeleccionados(int cantidadPersonalEmpresaSeleccionados);
        /// <summary>
        /// Método que valida la extensión del archivo que contiene el listado de personal de la 
        /// empresa a registrar (solo se permite .xls .xlsx)
        /// </summary>
        /// <param name="extension">Extensión de archivo a validar</param>
        void ValidarExtensionArchivoRegistroMasivo(string extension);
        /// <summary>
        /// Método que valida las columnas del archivo que contiene el listado de personal de la 
        /// empresa a registrar
        /// </summary>
        /// <param name="columnas">Columnas a validar</param>
        /// <returns>Columnas validadas</returns>
        List<string> ValidarColumnasArchivoRegistroMasivo(List<string> columnas);
        /// <summary>
        /// Método que crea la entidad personal de la empresa desde un listado
        /// </summary>
        /// <param name="listadoPersonalEmpresa">Listado de personal de la empresa a crear</param>
        /// <param name="personalEmpresaRegistrados">Listado de personal de la empresa
        /// registrados, tanto activos como inactivos (para omitir la duplicidad)</param>
        /// <returns>Listado de personal de la empresa creados</returns>
        List<PersonalEmpresa> CrearPersonalEmpresaDesdeListado(
            Tuple<string, string, string, List<Tuple<string, string, string>>> listadoPersonalEmpresa,
            List<PersonalEmpresa> personalEmpresaRegistrados);
        /// <summary>
        /// Método que crea la entidad que relaciona el personal de la empresa con sus áreas
        /// correspondientes
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa a quién pertenece el área</param>
        /// <param name="sede">Sede del área seleccionada</param>
        /// <param name="area">Área seleccionada</param>
        /// <returns>Relación creada</returns>
        PersonalEmpresaXArea CrearRelacionArea(PersonalEmpresa personalEmpresa, Sede sede,
            Area area);
        /// <summary>
        /// Método que inactiva la entidad que relaciona el personal de la empresa con sus áreas
        /// correspondientes
        /// </summary>
        /// <param name="relacionArea">Relación a inactivar</param>
        /// <returns>Relación inactivada</returns>
        PersonalEmpresaXArea InactivarRelacionArea(PersonalEmpresaXArea relacionArea);
        /// <summary>
        /// Método que inhabilita la entidad personal de la empresa
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa a inhabilitar</param>
        /// <returns>Personal de la empresa inhabilitado</returns>
        PersonalEmpresa InhabilitarPersonalEmpresa(PersonalEmpresa personalEmpresa);
        /// <summary>
        /// Método que habilita la entidad personal de la empresa
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa a habilitar</param>
        /// <returns>Personal de la empresa habilitado</returns>
        PersonalEmpresa HabilitarPersonalEmpresa(PersonalEmpresa personalEmpresa);
        /// <summary>
        /// Método que valida el personal de la empresa para el proceso de reconocimiento
        /// </summary>
        /// <param name="personalEmpresa">Personal a validar</param>
        /// <param name="equipoBiometrico">Equipo biométrico de donde se realiza el reconocimiento</param>
        void ValidarPersonalDeLaEmpresaParaReconocimientoDePersonal(PersonalEmpresa personalEmpresa,
            EquipoBiometrico equipoBiometrico);
    }
}
