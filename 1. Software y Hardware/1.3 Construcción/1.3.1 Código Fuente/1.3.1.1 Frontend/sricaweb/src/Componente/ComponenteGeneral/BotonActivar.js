import React from 'react'

import { Button, makeStyles } from '@material-ui/core'
import CheckCircleOutlineIcon from '@material-ui/icons/CheckCircleOutline'

const estilos = makeStyles({
    color: {
        color: 'orange'
    }
})

export const BotonActivar = (props) => {
    const claseEstilo = estilos()
    return(
        <Button
            variant='outlined'
            startIcon={ <CheckCircleOutlineIcon/> }
            className={ claseEstilo.color }
            onClick={ props.onClick }>
            { props.texto }
        </Button>
    )
}