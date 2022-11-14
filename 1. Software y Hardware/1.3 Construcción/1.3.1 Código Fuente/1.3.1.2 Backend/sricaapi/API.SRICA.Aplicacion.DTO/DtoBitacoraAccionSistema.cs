using System;

namespace API.SRICA.Aplicacion.DTO
{
    /// <summary>
    /// DTO para la bitácora de acciones del sistema
    /// </summary>
    public class DtoBitacoraAccionSistema
    {
        /// <summary>
        /// Código de la bitácora de acción
        /// </summary>
        public string CodigoBitacora { get; set; }
        /// <summary>
        /// Código del usuario de acción
        /// </summary>
        public string CodigoUsuario { get; set; }
        /// <summary>
        /// Usuario de acción
        /// </summary>
        public string UsuarioAcceso { get; set; }
        /// <summary>
        /// Nombre del usuario de acción
        /// </summary>
        public string NombreUsuario { get; set; }
        /// <summary>
        /// Apellido del usuario de acción
        /// </summary>
        public string ApellidoUsuario { get; set; }
        /// <summary>
        /// Código de rol del usuario de acción
        /// </summary>
        public string CodigoRolUsuario { get; set; }
        /// <summary>
        /// Descripción del rol del usuario de acción
        /// </summary>
        public string DescripcionRolUsuario { get; set; }
        /// <summary>
        /// Código del módulo de acción
        /// </summary>
        public string CodigoModuloSistema { get; set; }
        /// <summary>
        /// Descripción del módulo de acción
        /// </summary>
        public string DescripcionModuloSistema { get; set; }
        /// <summary>
        /// Código del recurso de acción
        /// </summary>
        public string CodigoRecursoSistema { get; set; }
        /// <summary>
        /// Descripción del recurso de acción
        /// </summary>
        public string DescripcionRecursoSistema { get; set; }
        /// <summary>
        /// Código del tipo de evento de acción
        /// </summary>
        public string CodigoTipoEventoSistema { get; set; }
        /// <summary>
        /// Descripción del tipo de evento de acción
        /// </summary>
        public string DescripcionTipoEventoSistema { get; set; }
        /// <summary>
        /// Código de acción
        /// </summary>
        public string CodigoAccionSistema { get; set; }
        /// <summary>
        /// Descripción de la acción
        /// </summary>
        public string DescripcionAccionSistema { get; set; }
        /// <summary>
        /// Descripción del resultado de acción
        /// </summary>
        public string DescripcionResultadoAccion { get; set; }
        /// <summary>
        /// Valor anterior
        /// </summary>
        public object ValorAnterior { get; set; }
        /// <summary>
        /// Valor actual
        /// </summary>
        public object ValorActual { get; set; }
        /// <summary>
        /// Fecha de acción
        /// </summary>
        public string FechaAccion { get; set; }
    }
}
