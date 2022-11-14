import React from 'react'

import { Button, makeStyles } from '@material-ui/core'
import CheckCircleOutlinedIcon from '@material-ui/icons/CheckCircleOutlined'

const estilos = makeStyles({
    color: {
        color: '#00A6FF'
    }
})

export const BotonSeleccionarArea = (props) => {
    const claseEstilo = estilos()
    return(
        <Button
            variant='outlined'
            startIcon={ <CheckCircleOutlinedIcon/> }
            className={ claseEstilo.color }
            onClick={ props.onClick }>
            Seleccionar Áreas
        </Button>
    )
}