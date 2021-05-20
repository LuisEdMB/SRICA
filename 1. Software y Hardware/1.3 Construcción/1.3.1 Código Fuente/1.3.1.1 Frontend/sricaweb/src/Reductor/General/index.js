import * as TipoAccion from '../../Accion/Constante'

const valorPorDefecto = {
    BackdropAbierto: false,
    EncabezadoVisible: false,
    MenuAbierto: false,
    EsCambioContrasenaOlvidada: false,
    TokenCambioContrasenaOlvidada : '',
    TokenDecodificadoCambioContrasenaOlvidada : ''
}

const valorPorDefectoUsuarioLogueado = {
    CodigoUsuario: '',
    Nombre: '',
    Apellido: '',
    Usuario: '',
    EsAdministrador: false,
    EsUsuarioBasico: false,
    EsCorreoElectronicoPorDefecto: false,
    EsContrasenaPorDefecto: false,
    CambiarDatosPorDefecto: false
}

export const GeneralReducer = (state = valorPorDefecto, action) => {
    switch (action.type) {
        case TipoAccion.ABRIR_BACKDROP:
            return {
                ...state, BackdropAbierto: true
            }
        case TipoAccion.CERRAR_BACKDROP:
            return {
                ...state, BackdropAbierto: false
            }
        case TipoAccion.ABRIR_MENU:
            return {
                ...state, MenuAbierto: true
            }
        case TipoAccion.CERRAR_MENU:
            return {
                ...state, MenuAbierto: false
            }
        case TipoAccion.MOSTRAR_ENCABEZADO:
            return {
                ...state, EncabezadoVisible: true
            }
        case TipoAccion.OCULTAR_ENCABEZADO:
            return {
                ...state, EncabezadoVisible: false
            }
        case TipoAccion.MOSTRAR_CAMBIO_CONTRASENA_OLVIDADA:
            return {
                ...state, EsCambioContrasenaOlvidada : action.payload
            }
        case TipoAccion.SET_TOKEN_CAMBIO_CONTRASENA_OLVIDADA:
            return {
                ...state, TokenCambioContrasenaOlvidada: action.payload
            }
        case TipoAccion.SET_TOKEN_DECODIFICADO_CAMBIO_CONTRASENA_OLVIDADA:
            return {
                ...state, TokenDecodificadoCambioContrasenaOlvidada: action.payload
            }
        default:
            return state
    }
}

export const GeneralUsuarioLogueadoReducer = (state = valorPorDefectoUsuarioLogueado, action) => {
    switch (action.type) {
        case TipoAccion.SET_DATOS_USUARIO_LOGUEADO:
            var usuarioLogueado = {
                CodigoUsuario: action.payload.CodigoUsuario,
                Nombre: action.payload.Nombre,
                Apellido: action.payload.Apellido,
                Usuario: action.payload.Usuario,
                EsAdministrador: action.payload.EsAdministrador,
                EsUsuarioBasico: action.payload.EsUsuarioBasico,
                EsCorreoElectronicoPorDefecto: action.payload.EsCorreoElectronicoPorDefecto,
                EsContrasenaPorDefecto: action.payload.EsContrasenaPorDefecto,
                CambiarDatosPorDefecto: 
                    action.payload.EsCorreoElectronicoPorDefecto || 
                    action.payload.EsContrasenaPorDefecto
                        ? true : false
            }
            return usuarioLogueado
        case TipoAccion.SET_DATOS_USUARIO_NO_LOGUEADO:
            var usuarioLogueadoVacio = {
                CodigoUsuario: '',
                Nombre: '',
                Apellido: '',
                Usuario: '',
                EsAdministrador: false,
                EsUsuarioBasico: false,
                EsCorreoElectronicoPorDefecto: false,
                EsContrasenaPorDefecto: false,
                CambiarDatosPorDefecto: false
            }
            return usuarioLogueadoVacio
        default:
            return state
    }
}