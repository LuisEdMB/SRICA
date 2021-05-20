import * as TipoAccion from '../../Accion/Constante'
import * as Constante from '../../Constante'

const valorPorDefecto = {
    Sedes: [],
    Nomenclaturas: []
}

const valorPorDefectoFormularioEquipoBiometrico = {
    ModalFormulario: false,
    CodigoEquipoBiometrico: '',
    CodigoNomenclatura: '',
    DescripcionNomenclatura: '',
    NombreEquipoBiometrico: '',
    DireccionRedEquipoBiometrico: '',
    CodigoSede: '',
    DescripcionSede: '',
    CodigoArea: '',
    DescripcionArea: '',
    EstadoObjeto: Constante.ESTADO_OBJETO.Nuevo
}

const valorPorDefectoAperturaEquipoBiometrico = {
    AbrirModal: false,
    EquipoBiometrico: {}
}

export const EquipoBiometricoReducer = (state = valorPorDefecto, action) => {
    switch (action.type) {
        case TipoAccion.SET_EQUIPO_BIOMETRICO_LISTADO_SEDE_EMPRESA:
            return {
                ...state, Sedes: action.payload
            }
        case TipoAccion.SET_EQUIPO_BIOMETRICO_LISTADO_NOMENCLATURA:
            return {
                ...state, Nomenclaturas: action.payload
            }
        default:
            return state
    }
}

export const EquipoBiometricoFormularioReducer = (state = valorPorDefectoFormularioEquipoBiometrico, 
    action) => {
    switch (action.type) {
        case TipoAccion.ABRIR_FORMULARIO_EQUIPO_BIOMETRICO:
            return {
                ...state, ModalFormulario: true
            }
        case TipoAccion.CERRAR_FORMULARIO_EQUIPO_BIOMETRICO:
            return {
                ...state, ModalFormulario: false
            }
        case TipoAccion.SET_EQUIPO_BIOMETRICO_FORMULARIO_EQUIPO_BIOMETRICO:
            var equipoBiometrico = {
                ModalFormulario: false,
                CodigoEquipoBiometrico: action.payload.CodigoEquipoBiometrico,
                CodigoNomenclatura: action.payload.IndicadorRegistroNomenclaturaParaSinAsignacion 
                    ? '' 
                    : action.payload.CodigoNomenclatura,
                DescripcionNomenclatura: action.payload.DescripcionNomenclatura,
                NombreEquipoBiometrico: action.payload.NombreEquipoBiometrico.substring(
                    4, action.payload.NombreEquipoBiometrico.length),
                DireccionRedEquipoBiometrico: action.payload.DireccionRedEquipoBiometrico,
                CodigoSede: action.payload.IndicadorRegistroSedeParaSinAsignacion
                    ? ''
                    : action.payload.CodigoSede,
                DescripcionSede: action.payload.DescripcionSede,
                CodigoArea: action.payload.IndicadorRegistroAreaParaSinAsignacion
                    ? ''
                    : action.payload.CodigoArea,
                DescripcionArea: action.payload.DescripcionArea,
                EstadoObjeto: Constante.ESTADO_OBJETO.Modificado
            }
            return equipoBiometrico
        case TipoAccion.SET_EQUIPO_BIOMETRICO_FORMULARIO_EQUIPO_BIOMETRICO_VACIO:
            var equipoBiometrico = {
                ModalFormulario: false,
                CodigoEquipoBiometrico: '',
                CodigoNomenclatura: '',
                DescripcionNomenclatura: '',
                NombreEquipoBiometrico: '',
                DireccionRedEquipoBiometrico: '',
                CodigoSede: '',
                DescripcionSede: '',
                CodigoArea: '',
                DescripcionArea: '',
                EstadoObjeto: Constante.ESTADO_OBJETO.Nuevo
            }
            return equipoBiometrico
        case TipoAccion.SET_EQUIPO_BIOMETRICO_FORMULARIO_POR_CAMPO:
            return {
                ...state, [action.name]: action.value
            }
        default:
            return state
    }
}

export const EquipoBiometricoFormularioAnteriorReducer = (state = {}, 
    action) => {
    switch (action.type) {
        case TipoAccion.SET_EQUIPO_BIOMETRICO_FORMULARIO_EQUIPO_BIOMETRICO_ANTERIOR:
            var equipoBiometricoAnterior = {
                CodigoEquipoBiometrico: action.payload.CodigoEquipoBiometrico,
                CodigoNomenclatura: action.payload.IndicadorRegistroNomenclaturaParaSinAsignacion 
                    ? '' 
                    : action.payload.CodigoNomenclatura,
                DescripcionNomenclatura: action.payload.DescripcionNomenclatura,
                NombreEquipoBiometrico: action.payload.NombreEquipoBiometrico.substring(
                    4, action.payload.NombreEquipoBiometrico.length),
                DireccionRedEquipoBiometrico: action.payload.DireccionRedEquipoBiometrico,
                CodigoSede: action.payload.IndicadorRegistroSedeParaSinAsignacion
                    ? ''
                    : action.payload.CodigoSede,
                DescripcionSede: action.payload.DescripcionSede,
                CodigoArea: action.payload.IndicadorRegistroAreaParaSinAsignacion
                    ? ''
                    : action.payload.CodigoArea,
                DescripcionArea: action.payload.DescripcionArea
            }
            return equipoBiometricoAnterior
        default:
            return state
    }
}

export const EquipoBiometricoAperturaPuertaReducer = (state = valorPorDefectoAperturaEquipoBiometrico,
    action) => {
    switch(action.type){
        case TipoAccion.ABRIR_MODAL_APERTURA_PUERTA_EQUIPO_BIOMETRICO:
            return {
                ...state, AbrirModal: true
            }
        case TipoAccion.CERRAR_MODAL_APERTURA_PUERTA_EQUIPO_BIOMETRICO:
            return {
                ...state, AbrirModal: false
            }
        case TipoAccion.SET_DATOS_APERTURA_PUERTA_EQUIPO_BIOMETRICO:
            return {
                ...state, EquipoBiometrico: action.payload
            }
        default:
            return state
    }
}