using API.SRICA.Dominio.Entidad.SI;
using System;
using System.IO;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.PE;

namespace API.SRICA.Dominio.Entidad.BT
{
    /// <summary>
    /// Entidad Bitácora Acción Equipo Biométrico que representa a la tabla 
    /// BT_BITACORA_ACCION_EQUIPO_BIOMETRICO
    /// </summary>
    public class BitacoraAccionEquipoBiometrico
    {
        /// <summary>
        /// Código interno de la bitácora de acción (primary key)
        /// </summary>
        public int CodigoBitacora { get; private set; }
        /// <summary>
        /// Código del personal de la empresa que realiza la acción
        /// </summary>
        public int CodigoPersonalEmpresa { get; private set; }
        /// <summary>
        /// DNI del personal de la empresa que realiza la acción
        /// </summary>
        public string DNIPersonalEmpresa { get; private set; }
        /// <summary>
        /// Nombre del personal de la empresa que realiza la acción
        /// </summary>
        public string NombrePersonalEmpresa { get; private set; }
        /// <summary>
        /// Apellido del personal de la empresa que realiza la acción
        /// </summary>
        public string ApellidoPersonalEmpresa { get; private set; }
        /// <summary>
        /// Código de sede de acción
        /// </summary>
        public int CodigoSede { get; private set; }
        /// <summary>
        /// Descripción de la sede de acción
        /// </summary>
        public string DescripcionSede { get; private set; }
        /// <summary>
        /// Código de área de acción
        /// </summary>
        public int CodigoArea { get; private set; }
        /// <summary>
        /// Descripción del área de acción
        /// </summary>
        public string DescripcionArea { get; private set; }
        /// <summary>
        /// Nombre del equipo biométrico de acción
        /// </summary>
        public string NombreEquipoBiometrico { get; private set; }
        /// <summary>
        /// Código del resultado de acceso (relación con SI_RESULTADO_ACCESO)
        /// </summary>
        public int CodigoResultadoAcceso { get; private set; }
        /// <summary>
        /// Descripción del resultado de acción
        /// </summary>
        public string DescripcionResultadoAccion { get; private set; }
        /// <summary>
        /// Fecha de acceso
        /// </summary>
        public DateTime FechaAcceso { get; private set; }
        /// <summary>
        /// Imagen de la persona que intenta ingresar a un área, sin que esté registrado
        /// en el sistema
        /// </summary>
        public byte[] ImagenPersonalNoRegistrado { get; private set; }
        /// <summary>
        /// Resultado de acceso que está relacionado con la bitácora de acción de equipos biométricos
        /// </summary>
        public virtual ResultadoAcceso ResultadoAcceso { get; private set; }
        /// <summary>
        /// Método estático que crea la entidad
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa que utiliza el equipo biométrico</param>
        /// <param name="equipoBiometrico">Equipo biométrico origen</param>
        /// <param name="resultadoAcceso">Resultado de acceso</param>
        /// <param name="mensajeAccion">Mensaje de la acción realizada</param>
        /// <param name="fotoPersonalNoRegistrado">Foto del personal no registrado en el sistema
        /// (en arreglo de bytes)</param>
        /// <returns>Entidad creada</returns>
        public static BitacoraAccionEquipoBiometrico CrearBitacora(PersonalEmpresa personalEmpresa,
            EquipoBiometrico equipoBiometrico, ResultadoAcceso resultadoAcceso, string mensajeAccion,
            byte[] fotoPersonalNoRegistrado)
        {
            return new BitacoraAccionEquipoBiometrico
            {
                CodigoPersonalEmpresa = personalEmpresa?.CodigoPersonalEmpresa ?? 0,
                DNIPersonalEmpresa = personalEmpresa?.DNIPersonalEmpresa ?? string.Empty,
                NombrePersonalEmpresa = personalEmpresa?.NombrePersonalEmpresa ?? string.Empty,
                ApellidoPersonalEmpresa = personalEmpresa?.ApellidoPersonalEmpresa ?? string.Empty,
                CodigoSede = equipoBiometrico?.Area?.CodigoSede ?? 0,
                DescripcionSede = equipoBiometrico?.Area?.Sede?.DescripcionSede ?? string.Empty,
                CodigoArea = equipoBiometrico?.CodigoArea ?? 0,
                DescripcionArea = equipoBiometrico?.Area?.DescripcionArea ?? string.Empty,
                NombreEquipoBiometrico = equipoBiometrico?.NombreEquipoBiometrico ?? string.Empty,
                CodigoResultadoAcceso = resultadoAcceso.CodigoResultadoAcceso,
                DescripcionResultadoAccion = mensajeAccion,
                FechaAcceso = DateTime.Now,
                ImagenPersonalNoRegistrado = fotoPersonalNoRegistrado
            };
        }
    }
}
