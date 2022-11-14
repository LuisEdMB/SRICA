import * as TipoAccion from '../Constante'

export const SetListadoSedeEmpresa = (sedes) => {
    return {
        type: TipoAccion.SET_AREA_EMPRESA_LISTADO_SEDE_EMPRESA,
        payload: sedes
    }
}

export const AbrirFormularioAreaEmpresa = () => {
    return {
        type: TipoAccion.ABRIR_FORMULARIO_AREA_EMPRESA
    }
}

export const CerrarFormularioAreaEmpresa = () => {
    return {
        type: TipoAccion.CERRAR_FORMULARIO_AREA_EMPRESA
    }
}

export const SetFormularioAreaEmpresa = (area) => {
    return {
        type: TipoAccion.SET_AREA_EMPRESA_FORMULARIO_AREA_EMPRESA,
        payload: area
    }
}

export const SetFormularioAreaEmpresaVacio = () => {
    return {
        type: TipoAccion.SET_AREA_EMPRESA_FORMULARIO_AREA_EMPRESA_VACIO
    }
}

export const SetFormularioAreaEmpresaAnterior = (area) => {
    return {
        type: TipoAccion.SET_AREA_EMPRESA_FORMULARIO_AREA_EMPRESA_ANTERIOR,
        payload: area
    }
}

export const SetFormularioAreaEmpresaPorCampo = (name, value) => {
    return {
        type: TipoAccion.SET_AREA_EMPRESA_FORMULARIO_POR_CAMPO,
        name: name,
        value: value
    }
}