import React, { useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Link } from 'react-router-dom'
import * as GeneralAction from '../../Accion/General'

import { Divider, List, ListItem, ListItemIcon, ListItemText, makeStyles, createStyles, Collapse, Typography } from '@material-ui/core'
import DashboardIcon from '@material-ui/icons/Dashboard'
import PeopleAltIcon from '@material-ui/icons/PeopleAlt'
import ApartmentIcon from '@material-ui/icons/Apartment'
import MeetingRoomIcon from '@material-ui/icons/MeetingRoom'
import VisibilityIcon from '@material-ui/icons/Visibility'
import WcIcon from '@material-ui/icons/Wc'
import AssessmentIcon from '@material-ui/icons/Assessment'
import { ExpandLess, ExpandMore } from '@material-ui/icons'
import SendIcon from '@material-ui/icons/Send';

const estilos = makeStyles((tema) => 
    createStyles({
        toolbar: {
            display: 'flex',
            alignItems: 'center',
            padding: tema.spacing(0, 1),
            ...tema.mixins.toolbar,
            justifyContent: 'flex-end'
        },
        subMenu: {
            paddingLeft: tema.spacing(4)
        },
        colorTextoIcono: {
            color: '#48525e',
            fontWeight: 'bold'
        },
        textoCopyright: {
            color: '#48525e',
            fontSize: 12,
            paddingLeft: tema.spacing(2),
            marginTop: 20,
            marginBottom: 20
        },
        link: {
            textDecoration: 'none',
            color: 'inherit'
        }
    })
)

export const MenuEncabezado = () => {
    const claseEstilo = estilos()
    const [subOpcionEquipoBiometricoAbierto, SetSubOpcionEquipoBiometricoAbierto] = useState(false)
    const [subOpcionReporteAbierto, SetSubOpcionReporteAbierto] = useState(false)
    const generalUsuarioLogueado = useSelector(store => store.GeneralUsuarioLogueado)
    const dispatch = useDispatch()
    return(
        <div>
            <div 
                className={ claseEstilo.toolbar } />
            <Divider />
            <List>
                <Link 
                    to='/' 
                    className={ claseEstilo.link }>
                    <ListItem 
                        button 
                        onClick={ () => dispatch(GeneralAction.CerrarMenu()) }>
                        <ListItemIcon>
                            <DashboardIcon className={ claseEstilo.colorTextoIcono }/>
                        </ListItemIcon>
                        <ListItemText 
                            disableTypography
                            primary='Dashboard'
                            className={ claseEstilo.colorTextoIcono }/>
                    </ListItem>
                </Link>
                {
                    generalUsuarioLogueado.EsAdministrador
                        ?   <>
                                <Link 
                                    to='/usuario' 
                                    className={ claseEstilo.link }>
                                    <ListItem 
                                        button 
                                        onClick={ () => dispatch(GeneralAction.CerrarMenu()) }>
                                        <ListItemIcon>
                                            <PeopleAltIcon className={ claseEstilo.colorTextoIcono }/>
                                        </ListItemIcon>
                                        <ListItemText 
                                            disableTypography 
                                            primary='Usuarios del Sistema' 
                                            className={ claseEstilo.colorTextoIcono }/>
                                    </ListItem>
                                </Link>
                                <Link 
                                    to='/sede' 
                                    className={ claseEstilo.link }>
                                    <ListItem 
                                        button 
                                        onClick={ () => dispatch(GeneralAction.CerrarMenu()) }>
                                        <ListItemIcon>
                                            <ApartmentIcon className={ claseEstilo.colorTextoIcono }/>
                                        </ListItemIcon>
                                        <ListItemText 
                                            disableTypography 
                                            primary='Sedes' 
                                            className={ claseEstilo.colorTextoIcono }/>
                                    </ListItem>
                                </Link>
                                <Link 
                                    to='/area' 
                                    className={ claseEstilo.link }>
                                    <ListItem 
                                        button 
                                        onClick={ () => dispatch(GeneralAction.CerrarMenu()) }>
                                        <ListItemIcon>
                                            <MeetingRoomIcon className={ claseEstilo.colorTextoIcono }/>
                                        </ListItemIcon>
                                        <ListItemText 
                                            disableTypography
                                            primary='Áreas'
                                            className={ claseEstilo.colorTextoIcono }/>
                                    </ListItem>
                                </Link>
                            </>
                        :   null
                }
                <ListItem 
                    button
                    onClick={ () =>  SetSubOpcionEquipoBiometricoAbierto(!subOpcionEquipoBiometricoAbierto) }>
                    <ListItemIcon>
                        <VisibilityIcon className={ claseEstilo.colorTextoIcono }/>
                    </ListItemIcon>
                    <ListItemText
                        disableTypography
                        primary='Equipos Biométricos' 
                        className={ claseEstilo.colorTextoIcono }/>
                    { subOpcionEquipoBiometricoAbierto ? <ExpandLess/> : <ExpandMore/> }
                </ListItem>
                <Collapse 
                    in={ subOpcionEquipoBiometricoAbierto } 
                    timeout='auto' 
                    unmountOnExit>
                    <List 
                        component='div'
                        disablePadding>
                            {
                                generalUsuarioLogueado.EsAdministrador
                                    ?   <Link 
                                            to='/nomenclatura' 
                                            className={ claseEstilo.link }>
                                            <ListItem 
                                                button
                                                onClick={ () => dispatch(GeneralAction.CerrarMenu()) }
                                                className={ claseEstilo.subMenu }>
                                                <ListItemIcon>
                                                    <SendIcon className={ claseEstilo.colorTextoIcono }/>
                                                </ListItemIcon>
                                                <ListItemText 
                                                    disableTypography
                                                    primary='Nomenclaturas' 
                                                    className={ claseEstilo.colorTextoIcono } />
                                            </ListItem>
                                        </Link>
                                    :   null
                            }
                        <Link 
                            to='/equipo-biometrico' 
                            className={ claseEstilo.link }>
                            <ListItem 
                                button
                                onClick={ () => dispatch(GeneralAction.CerrarMenu()) }
                                className={ claseEstilo.subMenu }>
                                <ListItemIcon>
                                    <SendIcon className={ claseEstilo.colorTextoIcono }/>
                                </ListItemIcon>
                                <ListItemText 
                                    disableTypography
                                    primary='Equipos Biométricos' 
                                    className={ claseEstilo.colorTextoIcono } />
                            </ListItem>
                        </Link>
                    </List>
                </Collapse>
                <Link 
                    to='/personal-empresa' 
                    className={ claseEstilo.link }>
                    <ListItem 
                        button 
                        onClick={ () => dispatch(GeneralAction.CerrarMenu()) }>
                        <ListItemIcon>
                            <WcIcon className={ claseEstilo.colorTextoIcono }/>
                        </ListItemIcon>
                        <ListItemText 
                            disableTypography
                            primary='Personal de la Empresa'
                            className={ claseEstilo.colorTextoIcono }/>
                    </ListItem>
                </Link>
                <ListItem 
                    button
                    onClick={ () =>  SetSubOpcionReporteAbierto(!subOpcionReporteAbierto) }>
                    <ListItemIcon>
                        <AssessmentIcon className={ claseEstilo.colorTextoIcono }/>
                    </ListItemIcon>
                    <ListItemText
                        disableTypography
                        primary='Reportes'
                        className={ claseEstilo.colorTextoIcono }/>
                    { subOpcionReporteAbierto ? <ExpandLess/> : <ExpandMore/> }
                </ListItem>
                <Collapse 
                    in={ subOpcionReporteAbierto } 
                    timeout='auto' 
                    unmountOnExit>
                    <List 
                        component='div'
                        disablePadding>
                        <Link 
                            to='/reporte-sistema' 
                            className={ claseEstilo.link }>
                            <ListItem 
                                button
                                onClick={ () => dispatch(GeneralAction.CerrarMenu()) }
                                className={ claseEstilo.subMenu }>
                                <ListItemIcon>
                                    <SendIcon className={ claseEstilo.colorTextoIcono }/>
                                </ListItemIcon>
                                <ListItemText 
                                    disableTypography
                                    primary='Reportes del Sistema' 
                                    className={ claseEstilo.colorTextoIcono } />
                            </ListItem>
                        </Link>
                        {
                            generalUsuarioLogueado.EsAdministrador
                                ?   <Link 
                                        to='/reporte-accion-sistema' 
                                        className={ claseEstilo.link }>
                                        <ListItem 
                                            button
                                            onClick={ () => dispatch(GeneralAction.CerrarMenu()) }
                                            className={ claseEstilo.subMenu }>
                                            <ListItemIcon>
                                                <SendIcon className={ claseEstilo.colorTextoIcono }/>
                                            </ListItemIcon>
                                            <ListItemText 
                                                disableTypography
                                                primary='Reportes de Acciones del Sistema' 
                                                className={ claseEstilo.colorTextoIcono } />
                                        </ListItem>
                                    </Link>
                                :   null
                        }
                        <Link 
                            to='/reporte-accion-equipo-biometrico' 
                            className={ claseEstilo.link }>
                            <ListItem 
                                button
                                onClick={ () => dispatch(GeneralAction.CerrarMenu()) }
                                className={ claseEstilo.subMenu }>
                                <ListItemIcon>
                                    <SendIcon className={ claseEstilo.colorTextoIcono }/>
                                </ListItemIcon>
                                <ListItemText 
                                    disableTypography
                                    primary='Reportes de Acciones de Equipos Biométricos' 
                                    className={ claseEstilo.colorTextoIcono } />
                            </ListItem>
                        </Link>
                    </List>
                </Collapse>
            </List>
            <Divider />
            <Typography 
                variant="body1"
                className={ claseEstilo.textoCopyright }>
                {'© ' + new Date().getFullYear() + ' Luis Eduardo Mamani Bedregal. Todos los derechos reservados.' }
            </Typography>
        </div>
    )
}