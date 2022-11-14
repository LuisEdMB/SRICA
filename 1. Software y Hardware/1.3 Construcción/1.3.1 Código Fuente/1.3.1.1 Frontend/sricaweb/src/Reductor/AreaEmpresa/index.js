import * as TipoAccion from '../../Accion/Constante'
import * as Constante from '../../Constante'

const valorPorDefecto = {
    Sedes: []
}

const valorPorDefectoFormularioAreaEmpresa = {
    ModalFormulario: false,
    CodigoArea: '',
    CodigoSede: '',
    DescripcionArea: '',
    DescripcionSede: '',
    EstadoObjeto: Constante.ESTADO_OBJETO.Nuevo
}

export const AreaEmpresaReducer = (state = valorPorDefecto, action) => {
    switch (action.type) {
        case TipoAccion.SET_AREA_EMPRESA_LISTADO_SEDE_EMPRESA:
            return {
                ...state, Sedes: action.payload
            }
        default:
            return state
    }
}

export const AreaEmpresaFormularioReducer = (state = valorPorDefectoFormularioAreaEmpresa,
    action) => {
    switch (action.type) {
        case TipoAccion.ABRIR_FORMULARIO_AREA_EMPRESA:
            return {
                ...state, ModalFormulario: true
            }
        case TipoAccion.CERRAR_FORMULARIO_AREA_EMPRESA:
            return {
                ...state, ModalFormulario: false
            }
        case TipoAccion.SET_AREA_EMPRESA_FORMULARIO_AREA_EMPRESA:
            var area = {
                ModalFormulario : false,
                CodigoArea: action.payload.CodigoArea,
                CodigoSede: action.payload.IndicadorRegistroSedeParaSinAsignacion 
                    ? ''
                    : action.payload.CodigoSede,
                DescripcionArea: action.payload.DescripcionArea,
                DescripcionSede: action.payload.DescripcionSede,
                EstadoObjeto: Constante.ESTADO_OBJETO.Modificado
            }
            return area
        case TipoAccion.SET_AREA_EMPRESA_FORMULARIO_AREA_EMPRESA_VACIO:
            var area = {
                ModalFormulario : false,
                CodigoArea: '',
                CodigoSede: '',
                DescripcionArea: '',
                DescripcionSede: '',
                EstadoObjeto: Constante.ESTADO_OBJETO.Nuevo
            }
            return area
        case TipoAccion.SET_AREA_EMPRESA_FORMULARIO_POR_CAMPO:
            return {
                ...state, [action.name]: action.value
            }
        default:
            return state
    }
}

export const AreaEmpresaFormularioAnteriorReducer = (state = {}, 
    action) => {
    switch (action.type) {
        case TipoAccion.SET_AREA_EMPRESA_FORMULARIO_AREA_EMPRESA_ANTERIOR:
            return action.payload
        default:
            return state
    }
}