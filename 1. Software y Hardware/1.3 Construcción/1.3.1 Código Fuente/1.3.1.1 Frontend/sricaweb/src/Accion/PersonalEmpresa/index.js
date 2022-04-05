import * as TipoAccion from '../Constante'

export const SetListadoSedeEmpresa = (sedes) => {
    return {
        type: TipoAccion.SET_PERSONAL_EMPRESA_LISTADO_SEDE_EMPRESA,
        payload: sedes
    }
}

export const SetListadoSedeEmpresaAreaEmpresa = (codigoSede, areas) => {
    return {
        type: TipoAccion.SET_PERSONAL_EMPRESA_LISTADO_SEDE_EMPRESA_AREA_EMPRESA,
        codigoSede: codigoSede,
        payload: areas
    }
}

export const SetListadoAreaEmpresaSeleccionado = (codigoSede, codigoArea, seleccionado,
    setObjetoNoNuevo = false) => {
    return {
        type: TipoAccion.SET_PERSONAL_EMPRESA_LISTADO_AREA_EMPRESA_SELECCIONADO,
        codigoSede: codigoSede,
        codigoArea: codigoArea,
        setObjetoNoNuevo: setObjetoNoNuevo,
        payload: seleccionado
    }
}

export const SetListadoAreaEmpresaAbiertoCerrado = (codigoSede, esAbierto) => {
    return {
        type: TipoAccion.SET_PERSONAL_EMPRESA_LISTADO_AREA_EMPRESA_ABIERTO_CERRADO,
        codigoSede: codigoSede,
        esAbierto: esAbierto
    }
}

export const AbrirFormularioPersonalEmpresa = () => {
    return {
        type: TipoAccion.ABRIR_FORMULARIO_PERSONAL_EMPRESA
    }
}

export const CerrarFormularioPersonalEmpresa = () => {
    return {
        type: TipoAccion.CERRAR_FORMULARIO_PERSONAL_EMPRESA
    }
}

export const SetFormularioPersonalEmpresa = (personalEmpresa) => {
    return {
        type: TipoAccion.SET_PERSONAL_EMPRESA_FORMULARIO_PERSONAL_EMPRESA,
        payload: personalEmpresa
    }
}

export const SetFormularioPersonalEmpresaVacio = () => {
    return {
        type: TipoAccion.SET_PERSONAL_EMPRESA_FORMULARIO_PERSONAL_EMPRESA_VACIO
    }
}

export const SetFormularioPersonalEmpresaAnterior = (personalEmpresa) => {
    return {
        type: TipoAccion.SET_PERSONAL_EMPRESA_FORMULARIO_PERSONAL_EMPRESA_ANTERIOR,
        payload: personalEmpresa
    }
}

export const SetFormularioPersonalEmpresaPorCampo = (name, value) => {
    return {
        type: TipoAccion.SET_PERSONAL_EMPRESA_FORMULARIO_POR_CAMPO,
        name: name,
        value: value
    }
}

export const AbrirFormularioSelectorArea = () => {
    return {
        type: TipoAccion.ABRIR_FORMULARIO_PERSONAL_EMPRESA_SELECTOR_AREA
    }
}

export const AbrirFormularioCapturadorIris = () => {
    return {
        type: TipoAccion.ABRIR_FORMULARIO_PERSONAL_EMPRESA_CAPTURADOR_IRIS
    }
}

export const CerrarFormularioCapturadorIris = () => {
    return {
        type: TipoAccion.CERRAR_FORMULARIO_PERSONAL_EMPRESA_CAPTURADOR_IRIS
    }
}

export const CerrarFormularioSelectorArea = () => {
    return {
        type: TipoAccion.CERRAR_FORMULARIO_PERSONAL_EMPRESA_SELECTOR_AREA
    }
}

export const AbrirComprobarReconocimientoIris = () => {
    return {
        type: TipoAccion.ABRIR_COMPROBAR_RECONOCIMIENTO_IRIS
    }
}

export const CerrarComprobarReconocimientoIris = () => {
    return {
        type: TipoAccion.CERRAR_COMPROBAR_RECONOCIMIENTO_IRIS
    }
}