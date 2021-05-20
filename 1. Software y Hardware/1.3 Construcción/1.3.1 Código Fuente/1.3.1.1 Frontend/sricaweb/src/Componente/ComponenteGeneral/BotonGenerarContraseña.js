import React from 'react'

import { Button, makeStyles } from '@material-ui/core'
import LockOutlinedIcon from '@material-ui/icons/LockOutlined'

const estilos = makeStyles({
    color: {
        color: '#00A6FF'
    }
})

export const BotonGenerarContraseña = (props) => {
    const claseEstilo = estilos()
    return(
        <Button
            variant='outlined'
            startIcon={ <LockOutlinedIcon/> }
            className={ claseEstilo.color }
            onClick={ props.onClick }>
            Generar Contraseña por Defecto
        </Button>
    )
}