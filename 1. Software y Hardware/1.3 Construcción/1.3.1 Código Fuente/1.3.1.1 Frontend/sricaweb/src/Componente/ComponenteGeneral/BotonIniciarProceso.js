import React from 'react'

import { Button, makeStyles } from '@material-ui/core'
import PlayCircleFilledWhiteOutlinedIcon from '@material-ui/icons/PlayCircleFilledWhiteOutlined';

const estilos = makeStyles({
    color: {
        color: 'green'
    }
})

export const BotonIniciarProceso = (props) => {
    const claseEstilo = estilos()
    return(
        <Button
            variant='outlined'
            startIcon={ <PlayCircleFilledWhiteOutlinedIcon/> }
            className={ claseEstilo.color }              
            onClick={ props.onClick }>
            { props.texto }
        </Button>
    )
}