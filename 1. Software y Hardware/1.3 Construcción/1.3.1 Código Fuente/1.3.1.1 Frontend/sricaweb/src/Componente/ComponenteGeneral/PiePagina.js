import React from 'react'

import { makeStyles, createStyles, Typography } from '@material-ui/core'

const estilos = makeStyles((tema) => 
    createStyles({
        principal: {
            display: 'flex',
            flexDirection: 'column',
            backgroundColor: '#48525e',
            width: '100%',           
            position: 'absolute',
            left: 0,
            bottom: 0,
            textAlign: 'center'
        },
        piePagina: {
            padding: tema.spacing(2, 2),
            marginTop: 'auto'
        },
        textoPiePagina: {
            color: 'white',
            fontSize: 12
        }
    })
)

export const PiePagina = () => {
    const claseEstilo = estilos()
    return(
        <div className={ claseEstilo.principal }>
            <footer className={ claseEstilo.piePagina }>
                <Typography 
                    variant="body1" 
                    className={ claseEstilo.textoPiePagina }>
                    {'© ' + new Date().getFullYear() + ' Luis Eduardo Mamani Bedregal. Todos los derechos reservados.' }
                </Typography>
            </footer>
        </div>
    )
}