using System;
using API.SRICA.Dominio.Entidad.BT;
using API.SRICA.Dominio.Entidad.EB;
using API.SRICA.Dominio.Entidad.PE;
using API.SRICA.Dominio.Entidad.SI;
using API.SRICA.Dominio.Servicio.Interfaz;

namespace API.SRICA.Dominio.Servicio.Implementacion
{
    /// <summary>
    /// Implementación del servicio de dominio para las bitácoras de acción de equipos biométricos
    /// </summary>
    public class ServicioDominioBitacoraAccionEquipoBiometrico
        : IServicioDominioBitacoraAccionEquipoBiometrico
    {
        /// <summary>
        /// Método que crea la bitácora de acción de un equipo biométrico
        /// </summary>
        /// <param name="personalEmpresa">Personal de la empresa que utiliza el
        /// equipo biométrico</param>
        /// <param name="equipoBiometrico">Equipo biométrico origen</param>
        /// <param name="resultadoAcceso">Resultado de acceso</param>
        /// <param name="mensajeAccion">Mensaje de la acción realizada</param>
        /// <param name="fotoPersonalNoRegistrado">Foto del personal no registrado
        /// en el sistema (formato base64)</param>
        /// <returns>Bitácora de acción del equipo biométrico creado</returns>
        public BitacoraAccionEquipoBiometrico CrearBitacora(PersonalEmpresa personalEmpresa,
            EquipoBiometrico equipoBiometrico, ResultadoAcceso resultadoAcceso,
            string mensajeAccion, string fotoPersonalNoRegistrado)
        {
            return BitacoraAccionEquipoBiometrico.CrearBitacora(personalEmpresa,
                equipoBiometrico, resultadoAcceso, mensajeAccion, 
                Convert.FromBase64String(fotoPersonalNoRegistrado));
        }
    }
}
