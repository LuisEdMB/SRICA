import * as TipoAccion from '../../Accion/Constante'

const valorPorDefecto = {
    ModalVisualizadorImagen: false,
    Imagen: ''
}

export const ReporteReducer = (state = valorPorDefecto, action) => {
    switch (action.type) {
        case TipoAccion.ABRIR_VISUALIZADOR_IMAGEN_PERSONAL_NO_REGISTRADO:
            var estado = {
                ModalVisualizadorImagen: true,
                Imagen: action.payload
            }
            return estado
        case TipoAccion.CERRAR_VISUALIZADOR_IMAGEN_PERSONAL_NO_REGISTRADO:
            var estado = {
                ModalVisualizadorImagen: false,
                Imagen: ''
            }
            return estado
        default:
            return state
    }
}