import * as TipoAccion from '../../Accion/Constante'
import * as Constante from '../../Constante'

const valorPorDefectoFormulario = {
    ModalFormulario: false,
    CodigoNomenclatura: '',
    DescripcionNomenclatura: '',
    EstadoObjeto: Constante.ESTADO_OBJETO.Nuevo
}

export const NomenclaturaFormularioReducer = (state = valorPorDefectoFormulario, action) => {
    switch (action.type) {
        case TipoAccion.ABRIR_FORMULARIO_NOMENCLATURA:
            return {
                ...state, ModalFormulario: true
            }
        case TipoAccion.CERRAR_FORMULARIO_NOMENCLATURA:
            return {
                ...state, ModalFormulario: false
            }
        case TipoAccion.SET_NOMENCLATURA_FORMULARIO_NOMENCLATURA:
            var nomenclatura = {
                ModalFormulario: false,
                CodigoNomenclatura: action.payload.CodigoNomenclatura,
                DescripcionNomenclatura: action.payload.DescripcionNomenclatura,
                EstadoObjeto: Constante.ESTADO_OBJETO.Modificado
            }
            return nomenclatura
        case TipoAccion.SET_NOMENCLATURA_FORMULARIO_NOMENCLATURA_VACIO:
            var nomenclatura = {
                ModalFormulario: false,
                CodigoNomenclatura: '',
                DescripcionNomenclatura: '',
                EstadoObjeto: Constante.ESTADO_OBJETO.Nuevo
            }
            return nomenclatura
        case TipoAccion.SET_NOMENCLATURA_FORMULARIO_POR_CAMPO:
            return {
                ...state, [action.name]: action.value
            }
        default:
            return state
    }
}

export const NomenclaturaFormularioAnteriorReducer = (state = {}, 
    action) => {
    switch (action.type) {
        case TipoAccion.SET_NOMENCLATURA_FORMULARIO_NOMENCLATURA_ANTERIOR:
            return action.payload
        default:
            return state
    }
}