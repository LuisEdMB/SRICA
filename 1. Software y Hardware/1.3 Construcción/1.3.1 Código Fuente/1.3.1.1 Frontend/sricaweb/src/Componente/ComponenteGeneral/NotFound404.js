import React from 'react'

import { makeStyles, Grid, Typography, CardMedia } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        marginTop: 200,
        marginBottom: 300,
        width: '100%'
    },
    centrar: {
        textAlign: 'center',
        justifyContent: 'center',      
        color: '#48525e'
    },
    cuadroImagen: {
        height: 0,
        paddingTop: '52%'
    }
})

export const NotFound404 = () => {
    const claseEstilo = estilos()
    return(
        <Grid
            container
            direction="column"
            alignItems="center"
            justify="center"
            style={{ minHeight: '100%' }}
            className={ claseEstilo.principal }>
            <Grid 
                item xs={ 12 }
                className={ claseEstilo.centrar }>
                <CardMedia
                    className={ claseEstilo.cuadroImagen }
                    title='404'
                    image={ require('../../Imagen/404.svg') }>
                </CardMedia>
                <br/>
                <Typography 
                    variant="h3" 
                    component="h3">
                    <b>Esta página no se encuentra, o no tienes accesos</b>
                </Typography>
            </Grid>      
        </Grid>
    )
}