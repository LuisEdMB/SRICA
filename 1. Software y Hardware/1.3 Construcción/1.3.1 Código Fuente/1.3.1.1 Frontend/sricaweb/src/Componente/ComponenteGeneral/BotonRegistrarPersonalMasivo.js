import React from 'react'

import { Button, makeStyles } from '@material-ui/core'
import PublishOutlinedIcon from '@material-ui/icons/PublishOutlined'

const estilos = makeStyles({
    color: {
        color: '#48525e'
    }
})

export const BotonRegistrarPersonalMasivo = (props) => {
    const claseEstilo = estilos()
    return(
        <Button
            variant='outlined'
            startIcon={ <PublishOutlinedIcon/> }
            className={ claseEstilo.color }
            onClick={ props.onClick }>
            Registrar Personal Masivamente
        </Button>
    )
}