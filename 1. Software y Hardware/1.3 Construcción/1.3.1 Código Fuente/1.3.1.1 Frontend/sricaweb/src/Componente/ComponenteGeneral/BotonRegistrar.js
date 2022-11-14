import React from 'react'

import { Button, makeStyles, IconButton, Tooltip } from '@material-ui/core'
import AddCircleOutlineIcon from '@material-ui/icons/AddCircleOutline'

const estilos = makeStyles({
    color: {
        color: 'green'
    }
})

export const BotonRegistrar = (props) => {
    const claseEstilo = estilos()
    if(!props.textoVisible){
        return(
            <Tooltip title={ props.texto } key={ props.key }>
                <IconButton
                    className={ claseEstilo.color }
                    onClick={ props.onClick }>
                    <AddCircleOutlineIcon/>
                </IconButton>
            </Tooltip>
        )
    }
    else{
        return(
            <Button
                variant='outlined'
                startIcon={ <AddCircleOutlineIcon/> }
                className={ claseEstilo.color }              
                onClick={ props.onClick }>
                { props.texto }
            </Button>
        )
    }
}