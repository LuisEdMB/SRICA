using API.SRICA.Dominio.Entidad.AR;
using API.SRICA.Dominio.Entidad.PE;
using API.SRICA.Dominio.Entidad.SE;
using API.SRICA.Dominio.Excepcion;
using API.SRICA.Dominio.Servicio.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using API.SRICA.Dominio.Entidad.EB;

namespace API.SRICA.Dominio.Servicio.Implementacion
{
    /// <summary>
    /// Implementación del servicio de dominio para el personal de la empresa
    /// </summary>
    public class ServicioDominioPersonalEmpresa : IServicioDominioPersonalEmpresa
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
        public PersonalEmpresa CrearPersonalEmpresa(List<PersonalEmpresa> personalEmpresaRegistrados,
            string dni, string nombre, string apellido, int cantidadAreasSeleccionadas, 
            string imagenIris)
        {
            ValidarCantidadAreasSeleccionadas(cantidadAreasSeleccionadas);
            ValidarImagenDeIris(imagenIris);
            ValidarDNI(dni, personalEmpresaRegistrados);
            ValidarNombre(nombre);
            ValidarApellido(apellido);
            return PersonalEmpresa.CrearPersonalEmpresa(dni, nombre, apellido);
        }
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
        public PersonalEmpresa ModificarPersonalEmpresa(PersonalEmpresa personalEmpresa, 
            List<PersonalEmpresa> personalEmpresaRegistrados,
            string dni, string nombre, string apellido,
            int cantidadAreasSeleccionadas)
        {
            ValidarCantidadAreasSeleccionadas(cantidadAreasSeleccionadas);
            personalEmpresaRegistrados = personalEmpresaRegistrados.Where(g =>
                g.CodigoPersonalEmpresa != personalEmpresa.CodigoPersonalEmpresa).ToList();
            ValidarDNI(dni, personalEmpresaRegistrados);
            ValidarNombre(nombre);
            ValidarApellido(apellido);
            personalEmpresa.ModificarPersonalEmpresa(dni, nombre, apellido);
            return personalEmpresa;
        }
        /// <summary>
        /// Método que valida que se haya seleccionado, por lo menos, un personal a ser
        /// inhabilitado o habilitado
        /// </summary>
        /// <param name="cantidadPersonalEmpresaSeleccionados">Cantidad de personal seleccionado</param>
        public void ValidarPersonalEmpresaSeleccionados(int cantidadPersonalEmpresaSeleccionados)
        {
            if (cantidadPersonalEmpresaSeleccionados == 0)
                throw new ExcepcionAplicacionPersonalizada("Debe seleccionar, al menos, un personal " +
                    "de la lista.");
        }
        /// <summary>
        /// Método que valida la extensión del archivo que contiene el listado de personal de la 
        /// empresa a registrar (solo se permite .xls .xlsx)
        /// </summary>
        /// <param name="extension">Extensión de archivo a validar</param>
        public void ValidarExtensionArchivoRegistroMasivo(string extension)
        {
            if (!extension.Equals(PersonalEmpresa.ExcelVersionAnterior) &&
                !extension.Equals(PersonalEmpresa.ExcelVersionActual))
                throw new ExcepcionAplicacionPersonalizada("El tipo o tamaño del archivo seleccionado es " +
                    "incorrecto. Solo está permitido los siguientes tipos de archivos: .xls, .xlsx; " +
                    "con tamaño de hasta 100mb.");
        }
        /// <summary>
        /// Método que valida las columnas del archivo que contiene el listado de personal de la 
        /// empresa a registrar
        /// </summary>
        /// <param name="columnas">Columnas a validar</param>
        /// <returns>Columnas validadas</returns>
        public List<string> ValidarColumnasArchivoRegistroMasivo(List<string> columnas)
        {
            var columnasValidadas = columnas.Where(columna =>
                columna.Trim().ToLower().Equals(PersonalEmpresa.ColumnaDNI) ||
                columna.Trim().ToLower().Equals(PersonalEmpresa.ColumnaNombre) ||
                columna.Trim().ToLower().Equals(PersonalEmpresa.ColumnaApellido)).Distinct().ToList();
            if (columnasValidadas.Count != 3)
                throw new ExcepcionAplicacionPersonalizada("El formato del archivo seleccionado " +
                    "es incorrecto. El archivo debe contener el siguiente formato: dni, nombres, " +
                    "apellidos.");
            return columnasValidadas;
        }
        /// <summary>
        /// Método que crea la entidad personal de la empresa desde un listado
        /// </summary>
        /// <param name="listadoPersonalEmpresa">Listado de personal de la empresa a crear</param>
        /// <param name="personalEmpresaRegistrados">Listado de personal de la empresa
        /// registrados, tanto activos como inactivos (para omitir la duplicidad)</param>
        /// <returns>Listado de personal de la empresa creados</returns>
        public List<PersonalEmpresa> CrearPersonalEmpresaDesdeListado(
            Tuple<string, string, string, List<Tuple<string, string, string>>> listadoPersonalEmpresa,
            List<PersonalEmpresa> personalEmpresaRegistrados)
        {
            List<PersonalEmpresa> listadoPersonalEmpresaCreados = new List<PersonalEmpresa>();
            ValidarListadoPersonalEmpresaARegistrar(listadoPersonalEmpresa, personalEmpresaRegistrados);
            foreach(var fila in listadoPersonalEmpresa.Item4)
            {
                if (OmitirDuplicidadDeDNI(listadoPersonalEmpresa.Item1, listadoPersonalEmpresa.Item2,
                    listadoPersonalEmpresa.Item3, fila.Item1, fila.Item2, fila.Item3,
                    personalEmpresaRegistrados))
                    continue;
                var dni = ObtenerValorPorColumna(PersonalEmpresa.ColumnaDNI,
                    listadoPersonalEmpresa.Item1, listadoPersonalEmpresa.Item2, 
                    listadoPersonalEmpresa.Item3, fila.Item1, fila.Item2, fila.Item3);
                var nombre = ObtenerValorPorColumna(PersonalEmpresa.ColumnaNombre,
                    listadoPersonalEmpresa.Item1, listadoPersonalEmpresa.Item2,
                    listadoPersonalEmpresa.Item3, fila.Item1, fila.Item2, fila.Item3);
                var apellido = ObtenerValorPorColumna(PersonalEmpresa.ColumnaApellido,
                    listadoPersonalEmpresa.Item1, listadoPersonalEmpresa.Item2,
                    listadoPersonalEmpresa.Item3, fila.Item1, fila.Item2, fila.Item3);
                listadoPersonalEmpresaCreados.Add(PersonalEmpresa.CrearPersonalEmpresa(
                    dni, nombre, apellido));
            }
            return listadoPersonalEmpresaCreados;
        }
        /// <summary>
        /// Método que crea la entidad que relaciona el personal de la empresa con sus áreas
        /// correspondientes
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa a quién pertenece el área</param>
        /// <param name="sede">Sede del área seleccionada</param>
        /// <param name="area">Área seleccionada</param>
        /// <returns>Relación creada</returns>
        public PersonalEmpresaXArea CrearRelacionArea(PersonalEmpresa personalEmpresa, Sede sede,
            Area area)
        {
            ValidarSedeDelArea(sede);
            ValidarArea(area);
            return PersonalEmpresaXArea.CrearPersonalEmpresaXArea(personalEmpresa, area);
        }
        /// <summary>
        /// Método que inactiva la entidad que relaciona el personal de la empresa con sus áreas
        /// correspondientes
        /// </summary>
        /// <param name="relacionArea">Relación a inactivar</param>
        /// <returns>Relación inactivada</returns>
        public PersonalEmpresaXArea InactivarRelacionArea(PersonalEmpresaXArea relacionArea)
        {
            relacionArea.Inactivar();
            return relacionArea;
        }
        /// <summary>
        /// Método que inhabilita la entidad personal de la empresa
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa a inhabilitar</param>
        /// <returns>Personal de la empresa inhabilitado</returns>
        public PersonalEmpresa InhabilitarPersonalEmpresa(PersonalEmpresa personalEmpresa)
        {
            personalEmpresa.InhabilitarPersonalEmpresa();
            return personalEmpresa;
        }
        /// <summary>
        /// Método que habilita la entidad personal de la empresa
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa a habilitar</param>
        /// <returns>Personal de la empresa habilitado</returns>
        public PersonalEmpresa HabilitarPersonalEmpresa(PersonalEmpresa personalEmpresa)
        {
            personalEmpresa.HabilitarPersonalEmpresa();
            return personalEmpresa;
        }
        /// <summary>
        /// Método que valida el personal de la empresa para el proceso de reconocimiento
        /// </summary>
        /// <param name="personalEmpresa">Personal a validar</param>
        /// <param name="equipoBiometrico">Equipo biométrico de donde se realiza el reconocimiento</param>
        public void ValidarPersonalDeLaEmpresaParaReconocimientoDePersonal(PersonalEmpresa personalEmpresa,
            EquipoBiometrico equipoBiometrico)
        {
            if (personalEmpresa == null)
                throw new ExcepcionAplicacionEquipoBiometricoPersonalizada("Hubo un intento de acceso " +
                    "por personal no registrado en el sistema.", 
                    ExcepcionAplicacionEquipoBiometricoPersonalizada
                        .CodigoExcepcionPersonalizado.DenegadoConFoto);
            if (!personalEmpresa.IndicadorEstado)
                throw new ExcepcionAplicacionEquipoBiometricoPersonalizada("El personal se encuentra " +
                    "inactivo.", 
                    ExcepcionAplicacionEquipoBiometricoPersonalizada
                        .CodigoExcepcionPersonalizado.DenegadoSinFoto);
            if (!personalEmpresa.AreasAsignadas.Any(g => 
                    g.CodigoArea.Equals(equipoBiometrico.CodigoArea) && g.IndicadorEstado))
                throw new ExcepcionAplicacionEquipoBiometricoPersonalizada("El personal no tiene accesos" +
                    ". Verifique el(las) área(s) asignada(s) al personal.", 
                    ExcepcionAplicacionEquipoBiometricoPersonalizada
                        .CodigoExcepcionPersonalizado.DenegadoSinFoto);
        }
        #region Métodos privados
        /// <summary>
        /// Método que valida que se haya seleccionado, por lo menos, un área para el 
        /// personal de la empresa
        /// </summary>
        /// <param name="cantidadAreasSeleccionadas">Cantidad de áreas seleccionadas para personal
        /// de la empresa</param>
        private void ValidarCantidadAreasSeleccionadas(int cantidadAreasSeleccionadas)
        {
            if (cantidadAreasSeleccionadas == 0)
                throw new ExcepcionAplicacionPersonalizada("Debe seleccionar una o " +
                    "varias áreas para el personal.");
        }
        /// <summary>
        /// Método que valida que la imagen de iris no esté vacía
        /// </summary>
        /// <param name="imagenIris">Imagen del iris en base64</param>
        private void ValidarImagenDeIris(string imagenIris)
        {
            if (string.IsNullOrEmpty(imagenIris))
                throw new ExcepcionAplicacionPersonalizada("Debe capturar la respectiva " +
                    "imagen de iris para el personal.");
        }
        /// <summary>
        /// Método que valida el DNI del personal de la empresa
        /// </summary>
        /// <param name="dni">DNI a validar</param>
        /// <param name="personalEmpresaRegistrados">Listado de personal de la empresa
        /// registrados, tanto activos como inactivos (para validación de duplicidad)</param>
        /// <param name="mostrarMensajeValidacion">Si se desea mostrar el mensaje de validación</param>
        /// <returns>TRUE: Validación correcta, FALSE: Validación con errores</returns>
        private bool ValidarDNI(string dni, List<PersonalEmpresa> personalEmpresaRegistrados,
            bool mostrarMensajeValidacion = true)
        {
            if (!dni.ValidarCadenaDeTextoSoloNumeros())
            {
                return mostrarMensajeValidacion
                    ? throw new ExcepcionAplicacionPersonalizada("El DNI debe ser de tipo numérico.")
                    : false;
            }
            if (!dni.ValidarCantidadCaracteres(8, 8))
            {
                return mostrarMensajeValidacion
                    ? throw new ExcepcionAplicacionPersonalizada("La cantidad de dígitos del DNI debe " +
                        "ser 8 dígitos.")
                    : false;
            }
            if (personalEmpresaRegistrados.Any(g => g.DNIPersonalEmpresa.Equals(dni)))
            {
                return mostrarMensajeValidacion
                    ? throw new ExcepcionAplicacionPersonalizada("El DNI ingresado ya existe. " +
                        "Verifique el listado del personal en estado activo o inactivo para " +
                        "encontrar al personal duplicado.")
                    : false;
            }
            return true;
        }
        /// <summary>
        /// Método que valida el nombre del personal de la empresa
        /// </summary>
        /// <param name="nombre">Nombre a validar</param>
        /// <param name="mostrarMensajeValidacion">Si se desea mostrar el mensaje de validación</param>
        /// <returns>TRUE: Validación correcta, FALSE: Validación con errores</returns>
        private bool ValidarNombre(string nombre, bool mostrarMensajeValidacion = true)
        {
            if (!nombre.ValidarCantidadCaracteres(1, 40))
            {
                return mostrarMensajeValidacion
                    ? throw new ExcepcionAplicacionPersonalizada("La cantidad máxima de caracteres " +
                        "para el nombre es de 40 caracteres.")
                    : false;
            }
            if (!nombre.ValidarCadenaDeTextoSoloLetras())
            {
                return mostrarMensajeValidacion
                    ? throw new ExcepcionAplicacionPersonalizada("El nombre ingresado debe contener " +
                        "solo letras.")
                    : false;
            }
            return true;
        }
        /// <summary>
        /// Método que valida el apellido del personal de la empresa
        /// </summary>
        /// <param name="apellido">Apellido a validar</param>
        /// <param name="mostrarMensajeValidacion">Si se desea mostrar el mensaje de validación</param>
        /// <returns>TRUE: Validación correcta, FALSE: Validación con errores</returns>
        private bool ValidarApellido(string apellido, bool mostrarMensajeValidacion = true)
        {
            if (!apellido.ValidarCantidadCaracteres(1, 40))
            {
                return mostrarMensajeValidacion
                    ? throw new ExcepcionAplicacionPersonalizada("La cantidad máxima de caracteres " +
                        "para el apellido es de 40 caracteres.")
                    : false;
            }
            if (!apellido.ValidarCadenaDeTextoSoloLetras())
            {
                return mostrarMensajeValidacion
                    ? throw new ExcepcionAplicacionPersonalizada("El apellido ingresado debe " +
                        "contener solo letras.")
                    : false;
            }
            return true;
        }
        /// <summary>
        /// Método que valida la sede del área seleccionada para el personal de la empresa
        /// </summary>
        /// <param name="sede"></param>
        private void ValidarSedeDelArea(Sede sede)
        {
            if (sede.EsSedeInactivo)
                throw new ExcepcionAplicacionPersonalizada("La(s) sede(s): \"" +
                    sede.DescripcionSede + "\", se encuentra(n) en estado INACTIVO.");
        }
        /// <summary>
        /// Método que valida el área seleccionada para el personal de la empresa
        /// </summary>
        /// <param name="area"></param>
        private void ValidarArea(Area area)
        {
            if (area.EsAreaInactivo)
                throw new ExcepcionAplicacionPersonalizada("El(las) área(s) seleccionada(s): \"" +
                    area.DescripcionArea + "\", se encuentra(n) en estado INACTIVO.");
        }
        /// <summary>
        /// Método que valida el listado de personal de la empresa a crear
        /// </summary>
        /// <param name="listadoPersonalEmpresa">Listado del personal de la empresa a validar</param>
        /// <param name="personalEmpresaRegistrados">Listado de personal de la empresa
        /// registrados, tanto activos como inactivos (para omitir la duplicidad)</param>
        private void ValidarListadoPersonalEmpresaARegistrar(
            Tuple<string, string, string, List<Tuple<string, string, string>>> listadoPersonalEmpresa,
            List<PersonalEmpresa> personalEmpresaRegistrados)
        {
            List<string> erroresGeneralesAVisualizar = new List<string>();
            List<string> erroresDNIDuplicadosAVisualizar = new List<string>();
            erroresGeneralesAVisualizar.AddRange(ValidarSoloValidacionesGeneralesDelPersonal(
                listadoPersonalEmpresa, personalEmpresaRegistrados));
            erroresDNIDuplicadosAVisualizar.AddRange(ObtenerDNIDuplicadoEnListadoDePersonal(
                listadoPersonalEmpresa, personalEmpresaRegistrados));
            if (erroresGeneralesAVisualizar.Count > 0 || erroresDNIDuplicadosAVisualizar.Count > 0)
                throw new ExcepcionAplicacionPersonalizada("<p>Los siguientes registros del archivo " +
                    "seleccionado han sido detectados como incorrectos:</p><br/><hr><br/>" +
                    (erroresGeneralesAVisualizar.Count > 0 
                        ? "<p>Validaciones Generales:</p>" + 
                            string.Join("<br/>", erroresGeneralesAVisualizar)
                        : "") +
                    (erroresDNIDuplicadosAVisualizar.Count > 0
                        ? "<p>DNIs validados, pero duplicados en el archivo:</p>" +
                            string.Join("<br/>", erroresDNIDuplicadosAVisualizar)
                        : "") + "<br/><br/><hr><br/>" +
                    "<p>Los registros deben tener en cuenta las siguientes validaciones:</p>" +
                    "<p>_ El DNI no debe estar vacío, debe tener 8 dígitos, ser de tipo " +
                    "numérico, y ser valor único.</p>" +
                    "<p>_ El nombre debe cumplir la cantidad máxima de 40 caracteres, y contener solo " +
                    "letras.</p>" +
                    "<p>_ El apellido debe cumplir la cantidad máxima de 40 caracteres, y contener solo " +
                    "letras.</p>");
        }
        /// <summary>
        /// Método que ejecuta una acción de validación según el nombre de columna
        /// </summary>
        /// <param name="columna">Columna que representa la validación a ejecutar</param>
        /// <param name="fila">Dato a validar</param>
        /// <returns>TRUE: Validación correcta, FALSE: Validación con errores</returns>
        private bool AccionarValidacionSegunColumna(string columna, string fila)
        {
            switch (columna.Trim().ToLower())
            {
                case PersonalEmpresa.ColumnaDNI:
                    return ValidarDNI(fila, new List<PersonalEmpresa>(), false);
                case PersonalEmpresa.ColumnaNombre:
                    return ValidarNombre(fila, false);
                case PersonalEmpresa.ColumnaApellido:
                    return ValidarApellido(fila, false);
                default:
                    return false;
            }
        }
        /// <summary>
        /// Método que omite una fila donde el DNI ya existe (usa todas las columnas para
        /// conocer cuál columna contiene los datos del DNI)
        /// </summary>
        /// <param name="columna1">Columna supuesta que representa a la columna "dni"</param>
        /// <param name="columna2">Columna supuesta que representa a la columna "dni"</param>
        /// <param name="columna3">Columna supuesta que representa a la columna "dni"</param>
        /// <param name="fila1">Fila supuesta que representa a la fila que contiene el DNI</param>
        /// <param name="fila2">Fila supuesta que representa a la fila que contiene el DNI</param>
        /// <param name="fila3">Fila supuesta que representa a la fila que contiene el DNI</param>
        /// <param name="personalEmpresaRegistrados">Listado de personal de la empresa
        /// registrados, tanto activos como inactivos (para omitir la duplicidad)</param>
        /// <returns>TRUE: Se omite el registro, FALSE: No se omite el registro</returns>
        private bool OmitirDuplicidadDeDNI(string columna1, string columna2, string columna3,
            string fila1, string fila2, string fila3, List<PersonalEmpresa> personalEmpresaRegistrados)
        {
            if (VerificarColumnaFilaDuplicidadDNI(columna1, fila1, personalEmpresaRegistrados))
                return true;
            if (VerificarColumnaFilaDuplicidadDNI(columna2, fila2, personalEmpresaRegistrados))
                return true;
            if (VerificarColumnaFilaDuplicidadDNI(columna3, fila3, personalEmpresaRegistrados))
                return true;
            return false;
        }
        /// <summary>
        /// Método que verifica que la columna supuesta que representa a la columna "dni", sea
        /// la correcta
        /// </summary>
        /// <param name="columna">Columna supuesta a verificar</param>
        /// <param name="fila">Fila supuesta con el dato del DNI a verificar</param>
        /// <param name="personalEmpresaRegistrados">Listado de personal de la empresa
        /// registrados, tanto activos como inactivos (para omitir la duplicidad)</param>
        /// <returns>TRUE: DNI ya existe, FALSE: DNI inexistente</returns>
        private bool VerificarColumnaFilaDuplicidadDNI(string columna, string fila,
            List<PersonalEmpresa> personalEmpresaRegistrados)
        {
            if (columna.Trim().ToLower().Equals(PersonalEmpresa.ColumnaDNI))
                if (personalEmpresaRegistrados.Any(g => g.DNIPersonalEmpresa.Equals(fila.Trim())))
                    return true;
            return false;
        }
        /// <summary>
        /// Método que valida el listado de personal a registrar solo con validaciones generales
        /// (no verifica la duplicidad del DNI en el listado a registrar, pero sí de los que se 
        /// encuentran ya registrados en la base de datos)
        /// </summary>
        /// <param name="listadoPersonalEmpresa">Listado del personal de la empresa a validar</param>
        /// <param name="personalEmpresaRegistrados">Listado de personal de la empresa
        /// registrados, tanto activos como inactivos (para omitir la duplicidad)</param>
        /// <returns>Listado de las validaciones accionadas como excepción</returns>
        private List<string> ValidarSoloValidacionesGeneralesDelPersonal(
            Tuple<string, string, string, List<Tuple<string, string, string>>> listadoPersonalEmpresa,
            List<PersonalEmpresa> personalEmpresaRegistrados)
        {
            List<string> erroresAVisualizar = new List<string>();
            var indice = 0;
            foreach (var filas in listadoPersonalEmpresa.Item4)
            {
                indice++;
                List<string> errores = new List<string>();
                if (OmitirDuplicidadDeDNI(
                    listadoPersonalEmpresa.Item1, listadoPersonalEmpresa.Item2,
                    listadoPersonalEmpresa.Item3, filas.Item1,
                    filas.Item2, filas.Item3,
                    personalEmpresaRegistrados))
                    continue;
                if (!AccionarValidacionSegunColumna(
                    listadoPersonalEmpresa.Item1,
                    filas.Item1))
                    errores.Add(listadoPersonalEmpresa.Item1.Trim().ToUpper() + ": " + filas.Item1);
                if (!AccionarValidacionSegunColumna(
                    listadoPersonalEmpresa.Item2,
                    filas.Item2))
                    errores.Add(listadoPersonalEmpresa.Item2.Trim().ToUpper() + ": " + filas.Item2);
                if (!AccionarValidacionSegunColumna(
                    listadoPersonalEmpresa.Item3,
                    filas.Item3))
                    errores.Add(listadoPersonalEmpresa.Item3.Trim().ToUpper() + ": " + filas.Item3);
                if (errores.Count > 0)
                    erroresAVisualizar.Add("-> FILA: " + indice.ToString() + " = " +
                        string.Join("; ", errores));
            }
            return erroresAVisualizar;
        }
        /// <summary>
        /// Método que obtiene el listado de DNI's duplicados en el listado de personal de la
        /// empresa a registrar
        /// </summary>
        /// <param name="listadoPersonalEmpresa">Listado del personal de la empresa a validar</param>
        /// <param name="personalEmpresaRegistrados">Listado de personal de la empresa
        /// registrados, tanto activos como inactivos (para omitir la duplicidad)</param>
        /// <returns>Listado de DNI's duplicados en el listado de personal de la empresa
        /// a registrar</returns>
        private List<string> ObtenerDNIDuplicadoEnListadoDePersonal(
            Tuple<string, string, string, List<Tuple<string, string, string>>> listadoPersonalEmpresa,
            List<PersonalEmpresa> personalEmpresaRegistrados)
        {
            var valoresColumnaDNI = ObtenerValoresPorColumna(PersonalEmpresa.ColumnaDNI, 
                listadoPersonalEmpresa);
            var listadoDNIValidadoCorrecto = new List<string>();
            foreach(var dni in valoresColumnaDNI)
            {
                if (VerificarColumnaFilaDuplicidadDNI(
                    PersonalEmpresa.ColumnaDNI, dni, personalEmpresaRegistrados))
                    continue;
                if (AccionarValidacionSegunColumna(PersonalEmpresa.ColumnaDNI, dni))
                    listadoDNIValidadoCorrecto.Add(dni);
            }
            if (listadoDNIValidadoCorrecto.Count > 0)
            {
                var duplicados = ObtenerValoresDuplicadosEnLista(listadoDNIValidadoCorrecto);
                return duplicados.Select(g => "-> " + g).ToList();
            }
            return new List<string>();
        }
        /// <summary>
        /// Método que obtiene los valores que pertenecen a una columna
        /// </summary>
        /// <param name="columna">Columna a buscar</param>
        /// <param name="listadoPersonalEmpresa">Listado del personal de la empresa que contiene los
        /// datos para la columna</param>
        /// <returns>Valores encontrados</returns>
        private List<string> ObtenerValoresPorColumna(string columna,
            Tuple<string, string, string, List<Tuple<string, string, string>>> listadoPersonalEmpresa)
        {
            var valores = new List<string>();
            foreach(var fila in listadoPersonalEmpresa.Item4)
            {
                if (listadoPersonalEmpresa.Item1.Trim().ToLower().Equals(columna.Trim().ToLower()))
                    valores.Add(fila.Item1);
                else if (listadoPersonalEmpresa.Item2.Trim().ToLower().Equals(columna.Trim().ToLower()))
                    valores.Add(fila.Item2);
                else if (listadoPersonalEmpresa.Item3.Trim().ToLower().Equals(columna.Trim().ToLower()))
                    valores.Add(fila.Item3);
            }
            return valores;
        }
        /// <summary>
        /// Método que obtiene el valor que corresponde a una columna específica
        /// </summary>
        /// <param name="columna">Columna a considerar</param>
        /// <param name="columna1">Columna 1</param>
        /// <param name="columna2">Columna 2</param>
        /// <param name="columna3">Columna 3</param>
        /// <param name="fila1">Fila 1</param>
        /// <param name="fila2">Fila 2</param>
        /// <param name="fila3">Fila 3</param>
        /// <returns>Valor perteneciente a la columna especificada</returns>
        private string ObtenerValorPorColumna(string columna, string columna1, string columna2, 
            string columna3, string fila1, string fila2, string fila3)
        {
            if (columna1.Trim().ToLower().Equals(columna.Trim().ToLower()))
                return fila1;
            if (columna2.Trim().ToLower().Equals(columna.Trim().ToLower()))
                return fila2;
            if (columna3.Trim().ToLower().Equals(columna.Trim().ToLower()))
                return fila3;
            return string.Empty;
        }
        /// <summary>
        /// Método que obtiene el listado de valores duplicados en una lista
        /// </summary>
        /// <param name="valores">Listado con los valores supuestos duplicados</param>
        /// <returns>Listado con los valores duplicados encontrados</returns>
        private List<string> ObtenerValoresDuplicadosEnLista(List<string> valores)
        {
            return valores.GroupBy(valor => valor).Where(grupo => grupo.Count() > 1)
                .Select(grupo => grupo.Key).ToList();
        }
        #endregion
    }
}
