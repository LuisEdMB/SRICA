import React, { useState, useRef, useEffect } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { Link } from 'react-router-dom'
import * as GeneralAction from '../../Accion/General'
import * as Constante from '../../Constante'

import * as ServicioSeguridad from '../../Servicio/Seguridad'

import { makeStyles, Button, Typography, Popper, Grow, Paper, ClickAwayListener, MenuList, MenuItem, ListItemIcon } from '@material-ui/core'
import { ExpandLess, ExpandMore } from '@material-ui/icons'
import ExitToAppIcon from '@material-ui/icons/ExitToApp'
import AccountBoxIcon from '@material-ui/icons/AccountBox'

const estilos = makeStyles({
    botonMenu: {
        color: '#48525e'
    },
    seccionUsuarioLogueado: {
        marginLeft: 'auto',
        marginRight: -11,
        color: '#48525e'
    },
    textoUsuarioLogueado: {
        fontSize: 10,
        textAlign: 'right',
        fontWeight: 'bold',
        color: '#48525e'
    },
    link: {
        textDecoration: 'none',
        color: 'inherit'
    }
})

export const MenuUsuarioLogueadoEncabezado = () => {
    const claseEstilo = estilos()
    const [opcionUsuarioLogueadoAbierto, SetOpcionUsuarioLogueadoAbierto] = useState(false)
    const prevOpcionUsuarioLogueadoAbierto = useRef(opcionUsuarioLogueadoAbierto);
    const generalUsuarioLogueado = useSelector(store => store.GeneralUsuarioLogueado)
    const dispatch = useDispatch()
    const anchorRef = useRef(null)

    const CerrarOpcionUsuarioLogueado = (event) => {
        if (anchorRef.current && 
            anchorRef.current.contains(event.target)) {
          return
        }   
        SetOpcionUsuarioLogueadoAbierto(false)
    };
    
    function CerrarOpcionUsuarioLogueadoConTeclaTab(event) {
        if (event.key === 'Tab') {
          event.preventDefault()
          SetOpcionUsuarioLogueadoAbierto(false)
        }
    }

    const CerrarSesion = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioSeguridad.CerrarSesion(() => {
            dispatch(GeneralAction.SetDatosUsuarioNoLogueado())
            dispatch(GeneralAction.OcultarEncabezado())
            sessionStorage.removeItem(Constante.VARIABLE_LOCAL_STORAGE)
            dispatch(GeneralAction.CerrarBackdrop())
        }, () => true)
    }

    useEffect(() => {
        if (prevOpcionUsuarioLogueadoAbierto.current === true && 
            opcionUsuarioLogueadoAbierto === false) {
            anchorRef.current.focus()
        }

        prevOpcionUsuarioLogueadoAbierto.current = opcionUsuarioLogueadoAbierto;
    }, [opcionUsuarioLogueadoAbierto])

    return(
        <section 
            className={claseEstilo.seccionUsuarioLogueado}>
            <Button
                onClick={ () => SetOpcionUsuarioLogueadoAbierto(!opcionUsuarioLogueadoAbierto) }
                ref={ anchorRef }
                aria-controls={ opcionUsuarioLogueadoAbierto ? 'listaOpcionUsuarioLogueado' : undefined }
                aria-haspopup="true">
                <Typography 
                    variant="h6" 
                    noWrap className={ claseEstilo.textoUsuarioLogueado }>
                    { generalUsuarioLogueado.Nombre }<br/>
                    { generalUsuarioLogueado.Apellido }<br/>
                    { generalUsuarioLogueado.Usuario }
                </Typography>
                { opcionUsuarioLogueadoAbierto ? 
                    <ExpandLess className={ claseEstilo.botonMenu }/> : 
                    <ExpandMore className={ claseEstilo.botonMenu }/> }
            </Button>
            <Popper 
                open={ opcionUsuarioLogueadoAbierto } 
                anchorEl={ anchorRef.current } 
                role={ undefined } 
                transition 
                disablePortal>
                {({ TransitionProps, placement }) => (
                    <Grow
                        {...TransitionProps}
                        style={{ transformOrigin: placement === 'bottom' ? 'center top' : 'center bottom' }}>
                        <Paper>
                            <ClickAwayListener onClickAway={ CerrarOpcionUsuarioLogueado }>
                                <MenuList 
                                    autoFocusItem={ opcionUsuarioLogueadoAbierto } 
                                    id="listaOpcionUsuarioLogueado" 
                                    onKeyDown={ CerrarOpcionUsuarioLogueadoConTeclaTab }>
                                    <MenuItem 
                                        onClick={ CerrarOpcionUsuarioLogueado }
                                        >
                                        <Link 
                                            to='/perfil' 
                                            className={ claseEstilo.link }>
                                            <ListItemIcon>
                                                <AccountBoxIcon 
                                                    fontSize="small" 
                                                    className={ claseEstilo.botonMenu } />
                                            </ListItemIcon>
                                            Mi Perfil
                                        </Link>
                                    </MenuItem>
                                    <MenuItem onClick={ CerrarSesion }>
                                        <ListItemIcon>
                                            <ExitToAppIcon 
                                                fontSize="small" 
                                                className={ claseEstilo.botonMenu } />
                                        </ListItemIcon>
                                        Cerrar Sesión
                                    </MenuItem>
                                </MenuList>
                            </ClickAwayListener>
                        </Paper>
                    </Grow>
                )}
            </Popper>
        </section>
    )
}