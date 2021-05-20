import React from 'react'

import { Button, makeStyles, IconButton, Tooltip } from '@material-ui/core'
import LockOpenOutlinedIcon from '@material-ui/icons/LockOpenOutlined'

const estilos = makeStyles({
    color1: {
        color: 'orange'
    },
    color2: {
        color: 'green'
    }
})

export const BotonAbrirPuerta = (props) => {
    const claseEstilo = estilos()
    if(props.modal){
        return(
            <Tooltip title={ props.texto ?? '' }>
                <IconButton
                    className={ claseEstilo.color1 }
                    onClick={ props.onClick }>
                    <LockOpenOutlinedIcon/>
                </IconButton>
            </Tooltip>
        )
    }
    else{
        return(
            <Button
                variant='outlined'
                startIcon={ <LockOpenOutlinedIcon/> }
                className={ claseEstilo.color2 }
                onClick={ props.onClick }>
                Abrir Puerta
            </Button>
        )
    }
}