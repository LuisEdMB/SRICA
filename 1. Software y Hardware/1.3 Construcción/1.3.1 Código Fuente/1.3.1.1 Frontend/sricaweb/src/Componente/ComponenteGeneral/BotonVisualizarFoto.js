import React from 'react'

import { makeStyles, IconButton, Tooltip } from '@material-ui/core'
import ImageOutlinedIcon from '@material-ui/icons/ImageOutlined'

const estilos = makeStyles({
    color: {
        color: 'orange'
    }
})

export const BotonVisualizarFoto = (props) => {
    const claseEstilo = estilos()
    return(
        <Tooltip title='Visualizar Foto'>
            <IconButton
                className={ claseEstilo.color }
                onClick={ props.onClick }>
                <ImageOutlinedIcon/>
            </IconButton>
        </Tooltip>
    )
}