import React from 'react'

import MuiDialogTitle from '@material-ui/core/DialogTitle'
import { withStyles, createStyles, Typography, IconButton, Grid } from '@material-ui/core'
import HighlightOffIcon from '@material-ui/icons/HighlightOff'

const estilos = (tema) => 
    createStyles({
        principal: {
            margin: 0,
            padding: tema.spacing(2),
            color: '#48525e'
        },
        texto: {
            fontWeight: 'bold'
        },
        botonCerrar: {
            position: 'absolute',
            right: tema.spacing(1),
            top: tema.spacing(1),
            color: '#48525e'
        },
        contenido: {
            flexGrow: 1
        }
    })

export const TituloModal = withStyles(estilos)((props) => {
    const { children, classes, onClose, ...other } = props
    return(
        <MuiDialogTitle 
            disableTypography 
            className={ classes.principal } 
            { ...other }>
            <Grid
                container
                className={ classes.contenido }>
                <Grid
                    item
                    xs={ 10 }
                    sm={ 10 }>
                    <Typography 
                        variant='h6' 
                        className={ classes.texto }>
                        { children }
                    </Typography>
                </Grid>
                <Grid
                    item
                    xs={ 2 }
                    sm={ 2 }>
                    { onClose ? (
                        <IconButton 
                            aria-label='cerrarModal' 
                            className={ classes.botonCerrar } 
                            onClick={ onClose }>
                            <HighlightOffIcon/>
                        </IconButton>
                    ) : null }
                </Grid>
            </Grid>
        </MuiDialogTitle>
    )
})