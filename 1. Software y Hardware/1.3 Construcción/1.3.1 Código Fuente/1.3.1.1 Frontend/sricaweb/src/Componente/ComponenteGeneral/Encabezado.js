import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import * as GeneralAction from '../../Accion/General'

import { MenuEncabezado } from './MenuEncabezado'
import { MenuUsuarioLogueadoEncabezado } from './MenuUsuarioLogueadoEncabezado'

import { AppBar, Toolbar, Typography, makeStyles, createStyles, IconButton, Hidden, Drawer, useTheme } from '@material-ui/core'
import MenuIcon from '@material-ui/icons/Menu'

const estilos = makeStyles((tema) => 
    createStyles({
        principal: {
            display: 'flex'
        },
        barraEncabezado: {
            zIndex: 888,
            background: '#FFFFFF'
        },
        botonMenu: {
            marginRight: tema.spacing(2),
            color: '#48525e'
        },
        tituloSRICA: {
            ...tema.typography.button,
            backgroundColor: '#48525e',
            color: 'white',
            padding: tema.spacing(1),
            fontWeight: 'bold',
            borderRadius: 10
        },
        drawer: {
            width: 240,
            flexShrink: 0
        },
        drawerPaper: {
            width: 240
        }
    })
)

export const Encabezado = () => {
    const claseEstilo = estilos()
    const tema = useTheme()
    const general = useSelector(store => store.General)
    const generalUsuarioLogueado = useSelector(store => store.GeneralUsuarioLogueado)
    const dispatch = useDispatch()
    return(
        <div 
            className={ claseEstilo.principal }>
            <AppBar 
                position="fixed" 
                className={ claseEstilo.barraEncabezado }>
                <Toolbar>
                    {
                        general.EncabezadoVisible && !generalUsuarioLogueado.CambiarDatosPorDefecto
                            ?   <IconButton
                                    color="inherit"
                                    edge="start"
                                    onClick={ () => dispatch(GeneralAction.AbrirMenu()) }
                                    className={ claseEstilo.botonMenu }>
                                    <MenuIcon />
                                </IconButton>
                            :   null

                    }
                    <Typography 
                        variant="h6" 
                        noWrap 
                        className={ claseEstilo.tituloSRICA }>
                        SRICA
                    </Typography>
                    {
                        general.EncabezadoVisible && !generalUsuarioLogueado.CambiarDatosPorDefecto
                            ?   <MenuUsuarioLogueadoEncabezado/>
                            :   null
                    }
                </Toolbar>
            </AppBar>
            <nav 
                className={claseEstilo.drawer}>
                <Hidden 
                    xsDown
                    implementation="css">
                    <Drawer
                        variant="temporary"
                        anchor={ tema.direction === 'rtl' ? 'right' : 'left' }
                        open = { general.MenuAbierto }
                        onClose={ () => dispatch(GeneralAction.CerrarMenu()) }
                        classes={ {
                            paper: claseEstilo.drawerPaper,
                        } }
                        ModalProps={ {
                        keepMounted: true,
                        } }
                    >
                        <MenuEncabezado/>
                    </Drawer>
                </Hidden>
            </nav>
        </div>
    )
}