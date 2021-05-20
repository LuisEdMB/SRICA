import * as TipoAccion from '../Constante'

export const AbrirVisualizadorPersonalNoRegistrado = (imagen) => {
    return {
        type: TipoAccion.ABRIR_VISUALIZADOR_IMAGEN_PERSONAL_NO_REGISTRADO,
        payload: imagen
    }
}

export const CerrarVisualizadorPersonalNoRegistrado = () => {
    return {
        type: TipoAccion.CERRAR_VISUALIZADOR_IMAGEN_PERSONAL_NO_REGISTRADO
    }
}