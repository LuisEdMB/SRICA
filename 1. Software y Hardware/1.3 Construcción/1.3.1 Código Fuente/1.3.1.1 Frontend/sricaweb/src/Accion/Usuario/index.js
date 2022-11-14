import * as TipoAccion from '../Constante'

export const SetListadoRolUsuario = (rolesUsuario) => {
    return {
        type: TipoAccion.SET_USUARIO_LISTADO_ROL_USUARIO,
        payload: rolesUsuario
    }
}

export const AbrirFormularioUsuario = () => {
    return {
        type: TipoAccion.ABRIR_FORMULARIO_USUARIO
    }
}

export const CerrarFormularioUsuario = () => {
    return {
        type: TipoAccion.CERRAR_FORMULARIO_USUARIO
    }
}

export const SetFormularioUsuario = (usuario) => {
    return {
        type: TipoAccion.SET_USUARIO_FORMULARIO_USUARIO,
        payload: usuario
    }
}

export const SetFormularioUsuarioVacio = () => {
    return {
        type: TipoAccion.SET_USUARIO_FORMULARIO_USUARIO_VACIO
    }
}

export const SetFormularioUsuarioAnterior = (usuario) => {
    return {
        type: TipoAccion.SET_USUARIO_FORMULARIO_USUARIO_ANTERIOR,
        payload: usuario
    }
}

export const SetFormularioUsuarioPorCampo = (name, value) => {
    return {
        type: TipoAccion.SET_USUARIO_FORMULARIO_POR_CAMPO,
        name: name,
        value: value
    }
}