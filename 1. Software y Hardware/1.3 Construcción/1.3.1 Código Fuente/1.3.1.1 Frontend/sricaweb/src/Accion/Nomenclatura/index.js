import * as TipoAccion from '../Constante'

export const AbrirFormularioNomenclatura = () => {
    return {
        type: TipoAccion.ABRIR_FORMULARIO_NOMENCLATURA
    }
}

export const CerrarFormularioNomenclatura = () => {
    return {
        type: TipoAccion.CERRAR_FORMULARIO_NOMENCLATURA
    }
}

export const SetFormularioNomenclatura = (nomenclatura) => {
    return {
        type: TipoAccion.SET_NOMENCLATURA_FORMULARIO_NOMENCLATURA,
        payload: nomenclatura
    }
}

export const SetFormularioNomenclaturaVacio = () => {
    return {
        type: TipoAccion.SET_NOMENCLATURA_FORMULARIO_NOMENCLATURA_VACIO
    }
}

export const SetFormularioNomenclaturaAnterior = (nomenclatura) => {
    return {
        type: TipoAccion.SET_NOMENCLATURA_FORMULARIO_NOMENCLATURA_ANTERIOR,
        payload: nomenclatura
    }
}

export const SetFormularioNomenclaturaPorCampo = (name, value) => {
    return {
        type: TipoAccion.SET_NOMENCLATURA_FORMULARIO_POR_CAMPO,
        name: name, 
        value: value
    }
}