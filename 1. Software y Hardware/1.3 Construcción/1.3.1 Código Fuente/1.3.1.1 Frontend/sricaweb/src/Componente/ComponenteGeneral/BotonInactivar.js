import React from 'react'

import { Button, makeStyles } from '@material-ui/core'
import RemoveCircleOutlineIcon from '@material-ui/icons/RemoveCircleOutline'

const estilos = makeStyles({
    color: {
        color: 'red'
    }
})

export const BotonInactivar = (props) => {
    const claseEstilo = estilos()
    return(
        <Button
            variant='outlined'
            startIcon={ <RemoveCircleOutlineIcon/> }
            className={ claseEstilo.color }
            onClick={ props.onClick }>
            { props.texto }
        </Button>
    )
}