using API.SRICA.Dominio.Entidad.SI;
using API.SRICA.Dominio.Entidad.US;
using System;

namespace API.SRICA.Dominio.Entidad.BT
{
    /// <summary>
    /// Entidad Bitácora Acción Sistema que representa a la tabla BT_BITACORA_ACCION_SISTEMA
    /// </summary>
    public class BitacoraAccionSistema
    {
        /// <summary>
        /// Código interno de la bitácora de acción (primary key)
        /// </summary>
        public int CodigoBitacora { get; private set; }
        /// <summary>
        /// Código del usuario de acción
        /// </summary>
        public int CodigoUsuario { get; private set; }
        /// <summary>
        /// Usuario de acción
        /// </summary>
        public string UsuarioAcceso { get; private set; }
        /// <summary>
        /// Nombre del usuario de acción
        /// </summary>
        public string NombreUsuario { get; private set; }
        /// <summary>
        /// Apellido del usuario de acción
        /// </summary>
        public string ApellidoUsuario { get; private set; }
        /// <summary>
        /// Código de rol del usuario de acción (relación con US_ROL_USUARIO)
        /// </summary>
        public int CodigoRolUsuario { get; private set; }
        /// <summary>
        /// Código del módulo de acción (relación con SI_MODULO_SISTEMA)
        /// </summary>
        public int CodigoModuloSistema { get; private set; }
        /// <summary>
        /// Código del recurso de acción (relación con SI_RECURSO_SISTEMA)
        /// </summary>
        public int CodigoRecursoSistema { get; private set; }
        /// <summary>
        /// Código del tipo de evento de acción (relación con SI_TIPO_EVENTO_SISTEMA)
        /// </summary>
        public int CodigoTipoEventoSistema { get; private set; }
        /// <summary>
        /// Código de acción (relación con SI_ACCION_SISTEMA)
        /// </summary>
        public int CodigoAccionSistema { get; private set; }
        /// <summary>
        /// Descripción del resultado de acción
        /// </summary>
        public string DescripcionResultadoAccion { get; private set; }
        /// <summary>
        /// Valor anterior
        /// </summary>
        public string ValorAnterior { get; private set; }
        /// <summary>
        /// Valor actual
        /// </summary>
        public string ValorActual { get; private set; }
        /// <summary>
        /// Fecha de acción
        /// </summary>
        public DateTime FechaAccion { get; private set; }
        /// <summary>
        /// Rol de usuario que está relacionado con la bitácora de acción del sistema
        /// </summary>
        public virtual RolUsuario RolUsuario { get; private set; }
        /// <summary>
        /// Módulo de sistema que está relacionado con la bitácora de acción del sistema
        /// </summary>
        public virtual ModuloSistema ModuloSistema { get; private set; }
        /// <summary>
        /// Recurso de sistema que está relacionado con la bitácora de acción del sistema
        /// </summary>
        public virtual RecursoSistema RecursoSistema { get; private set; }
        /// <summary>
        /// Tipo de evento de sistema que está relacionado con la bitácora de acción del sistema
        /// </summary>
        public virtual TipoEventoSistema TipoEventoSistema { get; private set; }
        /// <summary>
        /// Acción de sistema que está relacionado con la bitácora de acción del sistema
        /// </summary>
        public virtual AccionSistema AccionSistema { get; private set; }
        /// <summary>
        /// Método estático que crea la entidad bitácora de acción del sistema
        /// </summary>
        /// <param name="usuario">Usuario de acción</param>
        /// <param name="moduloSistema">Modulo de acción del sistema</param>
        /// <param name="recursoSistema">Recurso de acción del sistema</param>
        /// <param name="tipoEventoSistema">Tipo de evento de acción del sistema</param>
        /// <param name="accionSistema">Acción del sistema</param>
        /// <param name="descripcionResultado">Descripción del resultado de la acción</param>
        /// <param name="valorAnterior">Valor anterior</param>
        /// <param name="valorActual">Valor actual</param>
        /// <returns>Bitácora de acción del sistema creada</returns>
        public static BitacoraAccionSistema CrearBitacora(Usuario usuario, ModuloSistema moduloSistema, 
            RecursoSistema recursoSistema, TipoEventoSistema tipoEventoSistema, 
            AccionSistema accionSistema, string descripcionResultado, string valorAnterior, 
            string valorActual)
        {
            return new BitacoraAccionSistema
            {
                CodigoUsuario = usuario?.CodigoUsuario ?? 0,
                UsuarioAcceso = usuario?.UsuarioAcceso ?? string.Empty,
                NombreUsuario = usuario?.NombreUsuario ?? string.Empty,
                ApellidoUsuario = usuario?.ApellidoUsuario ?? string.Empty,
                CodigoRolUsuario = usuario?.RolUsuario?.CodigoRolUsuario ?? RolUsuario.SinRol,
                CodigoModuloSistema = moduloSistema.CodigoModuloSistema,
                CodigoRecursoSistema = recursoSistema.CodigoRecursoSistema,
                CodigoTipoEventoSistema = tipoEventoSistema.CodigoTipoEventoSistema,
                CodigoAccionSistema = accionSistema.CodigoAccionSistema,
                DescripcionResultadoAccion = descripcionResultado.Trim(),
                ValorAnterior = valorAnterior,
                ValorActual = valorActual,
                FechaAccion = DateTime.Now
            };
        }
    }
}
