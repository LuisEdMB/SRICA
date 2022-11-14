import React from 'react'

import { Button, makeStyles, IconButton, Tooltip } from '@material-ui/core'
import CheckCircle from '@material-ui/icons/CheckCircle'

const estilos = makeStyles({
    color: {
        color: 'blue'
    }
})

export const BotonComprobarReconocimientoIris = (props) => {
    const claseEstilo = estilos()
    if(!props.textoVisible){
        return(
            <Tooltip title={ props.texto } key={ props.key }>
                <IconButton
                    className={ claseEstilo.color }
                    onClick={ props.onClick }>
                    <CheckCircle/>
                </IconButton>
            </Tooltip>
        )
    }
    else{
        return(
            <Button
                variant='outlined'
                startIcon={ <CheckCircle/> }
                className={ claseEstilo.color }              
                onClick={ props.onClick }>
                { props.texto }
            </Button>
        )
    }
}