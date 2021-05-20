import * as TipoAccion from '../../Accion/Constante'

const valorPorDefecto = {
    Sedes: null
}

export const DashboardReducer = (state = valorPorDefecto, action) => {
    switch (action.type) {
        case TipoAccion.SET_DASHBOARD_LISTADO_SEDE:
            return {
                ...state, Sedes: action.payload
            }
        default:
            return state
    }
}