import * as TipoAccion from '../Constante'

export const AbrirFormularioSedeEmpresa = () => {
    return {
        type: TipoAccion.ABRIR_FORMULARIO_SEDE_EMPRESA
    }
}

export const CerrarFormularioSedeEmpresa = () => {
    return {
        type: TipoAccion.CERRAR_FORMULARIO_SEDE_EMPRESA
    }
}

export const SetFormularioSedeEmpresa = (sede) => {
    return {
        type: TipoAccion.SET_SEDE_EMPRESA_FORMULARIO_SEDE_EMPRESA,
        payload: sede
    }
}

export const SetFormularioSedeEmpresaVacio = () => {
    return {
        type: TipoAccion.SET_SEDE_EMPRESA_FORMULARIO_SEDE_EMPRESA_VACIO
    }
}

export const SetFormularioSedeEmpresaAnterior = (sede) => {
    return {
        type: TipoAccion.SET_SEDE_EMPRESA_FORMULARIO_SEDE_EMPRESA_ANTERIOR,
        payload: sede
    }
}

export const SetFormularioSedeEmpresaPorCampo = (name, value) => {
    return {
        type: TipoAccion.SET_SEDE_EMPRESA_FORMULARIO_POR_CAMPO,
        name: name,
        value: value
    }
}