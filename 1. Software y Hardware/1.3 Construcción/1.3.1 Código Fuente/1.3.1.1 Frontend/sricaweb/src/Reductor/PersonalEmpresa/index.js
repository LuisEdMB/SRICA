import * as TipoAccion from '../../Accion/Constante'
import * as Constante from '../../Constante'

const valorPorDefecto = {
    Sedes: []
}

const valorPorDefectoFormularioPersonalEmpresa = {
    ModalFormulario: false,
    ModalSelectorArea: false,
    ModalCapturadorIris: false,
    ModalComprobarReconocimientoIris: false,
    CodigoPersonalEmpresa : '0',
    DNIPersonalEmpresa: '',
    NombrePersonalEmpresa: '',
    ApellidoPersonalEmpresa: '',
    Areas: [],
    ImagenIris: '',
    EstadoObjeto: Constante.ESTADO_OBJETO.Nuevo
}

export const PersonalEmpresaReducer = (state = valorPorDefecto, action) => {
    switch (action.type) {
        case TipoAccion.SET_PERSONAL_EMPRESA_LISTADO_SEDE_EMPRESA:
            return {
                ...state, Sedes: action.payload
            }
        case TipoAccion.SET_PERSONAL_EMPRESA_LISTADO_SEDE_EMPRESA_AREA_EMPRESA:
            var resultado = state.Sedes.map((sede) => {
                if (sede.CodigoSede === action.codigoSede) sede.Areas = action.payload
                return sede
            })
            return {
                ...state, Sedes: resultado
            }
        case TipoAccion.SET_PERSONAL_EMPRESA_LISTADO_AREA_EMPRESA_SELECCIONADO:
            var resultado = state.Sedes.map((sede) => {
                if (sede.CodigoSede === action.codigoSede)
                    sede.Areas = sede.Areas.map((area) => {
                        if (area.CodigoArea === action.codigoArea){
                            if (action.setObjetoNoNuevo) area.Nuevo = false
                            area.Seleccionado = action.payload
                        }
                        return area
                    })
                return sede
            })
            return {
                ...state, Sedes: resultado
            }
        case TipoAccion.SET_PERSONAL_EMPRESA_LISTADO_AREA_EMPRESA_ABIERTO_CERRADO:
            var resultado = state.Sedes.map(sede => {
                if (sede.Abierto === undefined) sede.Abierto = false
                if (sede.CodigoSede === action.codigoSede) sede.Abierto = action.esAbierto
                return sede
            })
            return {
                ...state, Sedes: resultado
            }
        default:
            return state
    }
}

export const PersonalEmpresaFormularioReducer = (state = valorPorDefectoFormularioPersonalEmpresa, 
    action) => {
    switch (action.type) {
        case TipoAccion.ABRIR_FORMULARIO_PERSONAL_EMPRESA:
            return {
                ...state, ModalFormulario: true
            }
        case TipoAccion.CERRAR_FORMULARIO_PERSONAL_EMPRESA:
            return {
                ...state, ModalFormulario: false
            }
        case TipoAccion.SET_PERSONAL_EMPRESA_FORMULARIO_PERSONAL_EMPRESA:
            var personalEmpresa = {
                ModalFormulario: false,
                ModalSelectorArea: false,
                ModalCapturadorIris: false,
                CodigoPersonalEmpresa : action.payload.CodigoPersonalEmpresa,
                DNIPersonalEmpresa: action.payload.DNIPersonalEmpresa,
                NombrePersonalEmpresa: action.payload.NombrePersonalEmpresa,
                ApellidoPersonalEmpresa: action.payload.ApellidoPersonalEmpresa,
                Areas: [],
                EstadoObjeto: Constante.ESTADO_OBJETO.Modificado
            }
            return personalEmpresa
        case TipoAccion.SET_PERSONAL_EMPRESA_FORMULARIO_PERSONAL_EMPRESA_VACIO:
            var personalEmpresa = {
                ModalFormulario: false,
                ModalSelectorArea: false,
                ModalCapturadorIris: false,
                CodigoPersonalEmpresa : '0',
                DNIPersonalEmpresa: '',
                NombrePersonalEmpresa: '',
                ApellidoPersonalEmpresa: '',
                Areas: [],
                EstadoObjeto: Constante.ESTADO_OBJETO.Nuevo
            }
            return personalEmpresa
        case TipoAccion.SET_PERSONAL_EMPRESA_FORMULARIO_POR_CAMPO:
            return {
                ...state, [action.name]: action.value
            }
        case TipoAccion.ABRIR_FORMULARIO_PERSONAL_EMPRESA_SELECTOR_AREA:
            return {
                ...state, ModalSelectorArea: true
            }
        case TipoAccion.CERRAR_FORMULARIO_PERSONAL_EMPRESA_SELECTOR_AREA:
            return {
                ...state, ModalSelectorArea: false
            }
        case TipoAccion.ABRIR_FORMULARIO_PERSONAL_EMPRESA_CAPTURADOR_IRIS:
            return {
                ...state, ModalCapturadorIris: true
            }
        case TipoAccion.CERRAR_FORMULARIO_PERSONAL_EMPRESA_CAPTURADOR_IRIS:
            return {
                ...state, ModalCapturadorIris: false
            }
        case TipoAccion.ABRIR_COMPROBAR_RECONOCIMIENTO_IRIS:
            return {
                ...state, ModalComprobarReconocimientoIris: true
            }
        case TipoAccion.CERRAR_COMPROBAR_RECONOCIMIENTO_IRIS:
            return {
                ...state, ModalComprobarReconocimientoIris: false
            }
        default:
            return state
    }
}

export const PersonalEmpresaFormularioAnteriorReducer = (state = {}, action) => {
    switch (action.type) {
        case TipoAccion.SET_PERSONAL_EMPRESA_FORMULARIO_PERSONAL_EMPRESA_ANTERIOR:
            return action.payload
        default:
            return state
    }
}