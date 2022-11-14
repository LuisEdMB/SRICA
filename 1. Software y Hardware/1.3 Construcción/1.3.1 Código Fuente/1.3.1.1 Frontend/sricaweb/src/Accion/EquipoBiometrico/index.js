import * as TipoAccion from '../Constante'

export const SetListadoSedeEmpresa = (sedes) => {
    return {
        type: TipoAccion.SET_EQUIPO_BIOMETRICO_LISTADO_SEDE_EMPRESA,
        payload: sedes
    }
}

export const SetListadoNomenclatura = (nomenclaturas) => {
    return {
        type: TipoAccion.SET_EQUIPO_BIOMETRICO_LISTADO_NOMENCLATURA,
        payload: nomenclaturas
    }
}

export const AbrirFormularioEquipoBiometrico = () => {
    return {
        type: TipoAccion.ABRIR_FORMULARIO_EQUIPO_BIOMETRICO
    }
}

export const CerrarFormularioEquipoBiometrico = () => {
    return {
        type: TipoAccion.CERRAR_FORMULARIO_EQUIPO_BIOMETRICO
    }
}

export const SetFormularioEquipoBiometrico = (equipoBiometrico) => {
    return {
        type: TipoAccion.SET_EQUIPO_BIOMETRICO_FORMULARIO_EQUIPO_BIOMETRICO,
        payload: equipoBiometrico
    }
}

export const SetFormularioEquipoBiometricoVacio = () => {
    return {
        type: TipoAccion.SET_EQUIPO_BIOMETRICO_FORMULARIO_EQUIPO_BIOMETRICO_VACIO
    }
}

export const SetFormularioEquipoBiometricoAnterior = (equipoBiometrico) => {
    return {
        type: TipoAccion.SET_EQUIPO_BIOMETRICO_FORMULARIO_EQUIPO_BIOMETRICO_ANTERIOR,
        payload: equipoBiometrico
    }
}

export const SetFormularioEquipoBiometricoPorCampo = (name, value) => {
    return {
        type: TipoAccion.SET_EQUIPO_BIOMETRICO_FORMULARIO_POR_CAMPO,
        name: name,
        value: value
    }
}

export const AbrirModalAperturaPuertaEquipoBiometrico = () => {
    return {
        type: TipoAccion.ABRIR_MODAL_APERTURA_PUERTA_EQUIPO_BIOMETRICO
    }
}

export const CerrarModalAperturaPuertaEquipoBiometrico = () => {
    return {
        type: TipoAccion.CERRAR_MODAL_APERTURA_PUERTA_EQUIPO_BIOMETRICO
    }
}

export const SetDatosAperturaPuertaEquipoBiometrico = (equipoBiometrico) => {
    return {
        type: TipoAccion.SET_DATOS_APERTURA_PUERTA_EQUIPO_BIOMETRICO,
        payload: equipoBiometrico
    }
}