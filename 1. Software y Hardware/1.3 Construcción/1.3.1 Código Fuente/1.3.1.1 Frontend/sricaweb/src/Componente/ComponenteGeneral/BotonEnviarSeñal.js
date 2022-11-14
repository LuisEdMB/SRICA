import React from 'react'

import { Button, makeStyles } from '@material-ui/core'
import SignalWifi3BarIcon from '@material-ui/icons/SignalWifi3Bar'

const estilos = makeStyles({
    color: {
        color: '#00A6FF'
    }
})

export const BotonEnviarSeñal = (props) => {
    const claseEstilo = estilos()
    return(
        <Button
            variant='outlined'
            startIcon={ <SignalWifi3BarIcon/> }
            className={ claseEstilo.color }
            onClick={ props.onClick }>
            Enviar Señal
        </Button>
    )
}