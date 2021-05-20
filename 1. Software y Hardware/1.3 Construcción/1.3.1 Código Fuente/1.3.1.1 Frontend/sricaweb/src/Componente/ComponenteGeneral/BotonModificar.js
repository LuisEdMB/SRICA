import React from 'react'

import { makeStyles, IconButton, Tooltip } from '@material-ui/core'
import EditOutlinedIcon from '@material-ui/icons/EditOutlined'

const estilos = makeStyles({
    color: {
        color: '#00A6FF'
    }
})

export const BotonModificar = (props) => {
    const claseEstilo = estilos()
    return(
        <Tooltip title={ props.texto } key={ props.key }>
            <IconButton
                className={ claseEstilo.color }
                onClick={ props.onClick }>
                <EditOutlinedIcon/>
            </IconButton>
        </Tooltip>
    )
}