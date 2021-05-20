import * as TipoAccion from '../Constante'

export const SetDashboardListadoSede = (sedes) => {
    return {
        type: TipoAccion.SET_DASHBOARD_LISTADO_SEDE,
        payload: sedes
    }
}