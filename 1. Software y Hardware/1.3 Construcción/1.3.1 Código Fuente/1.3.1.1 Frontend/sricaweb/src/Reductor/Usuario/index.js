import * as TipoAccion from '../../Accion/Constante'
import * as Constante from '../../Constante'

const valorPorDefecto = {
    Roles: []
}

const valorPorDefectoFormularioUsuario = {
    ModalFormulario: false,
    CodigoUsuario: '',
    Usuario: '',
    MostrarBotonGenerarContrasena: false,
    EsContrasenaPorDefecto: false,
    Nombre: '',
    Apellido: '',
    CodigoRolUsuario: '',
    RolUsuario: '',
    CorreoElectronico: '',
    EstadoObjeto: Constante.ESTADO_OBJETO.Nuevo
}

export const UsuarioReducer = (state = valorPorDefecto, action) => {
    switch (action.type) {
        case TipoAccion.SET_USUARIO_LISTADO_ROL_USUARIO:
            return {
                ...state, Roles: action.payload
            }
        default:
            return state
    }
}

export const UsuarioFormularioReducer = (state = valorPorDefectoFormularioUsuario, action) => {
    switch (action.type) {
        case TipoAccion.ABRIR_FORMULARIO_USUARIO:
            return {
                ...state, ModalFormulario: true
            }
        case TipoAccion.CERRAR_FORMULARIO_USUARIO:
            return {
                ...state, ModalFormulario: false
            }
        case TipoAccion.SET_USUARIO_FORMULARIO_USUARIO:
            var usuario = {
                CodigoUsuario: action.payload.CodigoUsuario,
                Usuario: action.payload.Usuario,
                MostrarBotonGenerarContrasena: true,
                EsContrasenaPorDefecto: false,
                Nombre: action.payload.Nombre,
                Apellido: action.payload.Apellido,
                CodigoRolUsuario: action.payload.CodigoRolUsuario,
                RolUsuario: action.payload.RolUsuario,
                CorreoElectronico: action.payload.CorreoElectronico,
                EstadoObjeto: Constante.ESTADO_OBJETO.Modificado
            }
            return usuario
        case TipoAccion.SET_USUARIO_FORMULARIO_USUARIO_VACIO:
            var usuarioVacio = {
                CodigoUsuario: '',
                Usuario: '',
                MostrarBotonGenerarContrasena: false,
                EsContrasenaPorDefecto: false,
                Nombre: '',
                Apellido: '',
                CodigoRolUsuario: '',
                RolUsuario: '',
                CorreoElectronico: '',
                EstadoObjeto: Constante.ESTADO_OBJETO.Nuevo
            }
            return usuarioVacio
        case TipoAccion.SET_USUARIO_FORMULARIO_POR_CAMPO:
            return {
                ...state, [action.name]: action.value
            }
        default:
            return state
    }
}

export const UsuarioFormularioAnteriorReducer = (state = {}, 
    action) => {
    switch (action.type) {
        case TipoAccion.SET_USUARIO_FORMULARIO_USUARIO_ANTERIOR:
            return action.payload
        default:
            return state
    }
}