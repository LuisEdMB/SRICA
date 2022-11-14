import * as TipoAccion from '../../Accion/Constante'
import * as Constante from '../../Constante'

const valorPorDefectoFormularioSedeEmpresa = {
    ModalFormulario: false,
    CodigoSede: '',
    DescripcionSede: '',
    EstadoObjeto: Constante.ESTADO_OBJETO.Nuevo
}

export const SedeEmpresaFormularioReducer = (state = valorPorDefectoFormularioSedeEmpresa, action) => {
    switch (action.type) {
        case TipoAccion.ABRIR_FORMULARIO_SEDE_EMPRESA:
            return {
                ...state, ModalFormulario: true
            }
        case TipoAccion.CERRAR_FORMULARIO_SEDE_EMPRESA:
            return {
                ...state, ModalFormulario: false
            }
        case TipoAccion.SET_SEDE_EMPRESA_FORMULARIO_SEDE_EMPRESA:
            var sedeEmpresa = {
                ModalFormulario: false,
                CodigoSede: action.payload.CodigoSede,
                DescripcionSede: action.payload.DescripcionSede,
                EstadoObjeto: Constante.ESTADO_OBJETO.Modificado
            }
            return sedeEmpresa
        case TipoAccion.SET_SEDE_EMPRESA_FORMULARIO_SEDE_EMPRESA_VACIO:
            var sedeEmpresaVacio = {
                ModalFormulario: false,
                CodigoSede: '',
                DescripcionSede: '',
                EstadoObjeto: Constante.ESTADO_OBJETO.Nuevo
            }
            return sedeEmpresaVacio
        case TipoAccion.SET_SEDE_EMPRESA_FORMULARIO_POR_CAMPO:
            return {
                ...state, [action.name]: action.value
            }
        default:
            return state
    }
}

export const SedeEmpresaFormularioAnteriorReducer = (state = {}, 
    action) => {
    switch (action.type) {
        case TipoAccion.SET_SEDE_EMPRESA_FORMULARIO_SEDE_EMPRESA_ANTERIOR:
            return action.payload
        default:
            return state
    }
}