import React from 'react'

import { Button, makeStyles } from '@material-ui/core'
import CameraAltOutlinedIcon from '@material-ui/icons/CameraAltOutlined'

const estilos = makeStyles({
    color: {
        color: '#48525e'
    }
})

export const BotonCapturarImagenIris = (props) => {
    const claseEstilo = estilos()
    return(
        <Button
            variant='outlined'
            startIcon={ <CameraAltOutlinedIcon/> }
            className={ claseEstilo.color }
            onClick={ props.onClick }>
            Capturar Imagen de Iris
        </Button>
    )
}