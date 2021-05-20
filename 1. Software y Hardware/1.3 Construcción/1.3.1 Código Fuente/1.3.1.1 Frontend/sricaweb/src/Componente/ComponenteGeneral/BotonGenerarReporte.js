import React from 'react'

import { Button, makeStyles } from '@material-ui/core'
import CachedIcon from '@material-ui/icons/Cached'

const estilos = makeStyles({
    color: {
        color: '#48525e'
    }
})

export const BotonGenerarReporte = (props) => {
    const claseEstilo = estilos()
    return(
        <Button
            variant='outlined'
            startIcon={ <CachedIcon/> }
            className={ claseEstilo.color }
            onClick={ props.onClick }>
            Generar Reporte
        </Button>
    )
}