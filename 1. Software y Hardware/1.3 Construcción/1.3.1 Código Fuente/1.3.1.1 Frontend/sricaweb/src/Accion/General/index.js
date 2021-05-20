import * as TipoAccion from '../Constante'

export const AbrirBackdrop = () => {
    return {
        type: TipoAccion.ABRIR_BACKDROP,
    }
}

export const CerrarBackdrop = () => {
    return {
        type: TipoAccion.CERRAR_BACKDROP
    }
}

export const AbrirMenu = () => {
    return {
        type: TipoAccion.ABRIR_MENU
    }
}

export const CerrarMenu = () => {
    return {
        type: TipoAccion.CERRAR_MENU
    }
}

export const MostrarEncabezado = () => {
    return {
        type: TipoAccion.MOSTRAR_ENCABEZADO
    }
}

export const OcultarEncabezado = () => {
    return {
        type: TipoAccion.OCULTAR_ENCABEZADO
    }
}

export const SetDatosUsuarioLogueado = (datosUsuario) => {
    return {
        type: TipoAccion.SET_DATOS_USUARIO_LOGUEADO,
        payload: datosUsuario
    }
}

export const SetDatosUsuarioNoLogueado = () => {
    return {
        type: TipoAccion.SET_DATOS_USUARIO_NO_LOGUEADO
    }
}

export const MostrarCambioContrasenaOlvidada = (valor) => {
    return {
        type: TipoAccion.MOSTRAR_CAMBIO_CONTRASENA_OLVIDADA,
        payload: valor
    }
}

export const SetTokenCambioContrasenaOlvidada = (token) => {
    return {
        type: TipoAccion.SET_TOKEN_CAMBIO_CONTRASENA_OLVIDADA,
        payload: token
    }
}

export const SetTokenDecodificadoCambioContrasenaOlvidada = (token) => {
    return {
        type: TipoAccion.SET_TOKEN_DECODIFICADO_CAMBIO_CONTRASENA_OLVIDADA,
        payload: token
    }
}