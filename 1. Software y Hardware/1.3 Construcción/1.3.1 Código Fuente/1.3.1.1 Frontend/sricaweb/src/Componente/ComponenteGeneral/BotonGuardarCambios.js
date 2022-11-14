import React from 'react'

import { Button, makeStyles } from '@material-ui/core'
import SaveOutlinedIcon from '@material-ui/icons/SaveOutlined'

const estilos = makeStyles({
    color: {
        color: 'green'
    }
})

export const BotonGuardarCambios = (props) => {
    const claseEstilo = estilos()
    return(
        <Button
            variant='outlined'
            startIcon={ <SaveOutlinedIcon/> }
            className={ claseEstilo.color }
            onClick={ props.onClick }>
            Guardar Cambios
        </Button>
    )
}