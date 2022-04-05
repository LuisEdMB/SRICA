import React from 'react'
import { useDispatch, useSelector } from 'react-redux'
import * as GeneralAction from '../../Accion/General'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as EquipoBiometricoAction from '../../Accion/EquipoBiometrico'

import * as ServicioEquipoBiometrico from '../../Servicio/EquipoBiometrico'

import { TituloModal } from '../ComponenteGeneral/TituloModal'
import { ContenidoModal } from '../ComponenteGeneral/ContenidoModal'
import { SeccionAccionModal } from '../ComponenteGeneral/SeccionAccionModal'
import { BotonEnviarSeñal } from '../ComponenteGeneral/BotonEnviarSeñal'
import { BotonAbrirPuerta } from '../ComponenteGeneral/BotonAbrirPuerta'

import { Dialog, Grid, makeStyles, Typography, CardMedia } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        color: '#48525e'
    },
    texto: {
        fontSize: 14,
        textAlign: 'center'
    },
    cuadroImagen: {
        height: 450,
        width: 565
    }
})

export const AperturaEquipoBiometrico = () => {
    const claseEstilo = estilos()
    const equipoBiometrico = useSelector((store) => store.EquipoBiometricoAperturaPuerta)
    const dispatch = useDispatch()

    const CerrarModal = () => {
        dispatch(EquipoBiometricoAction.CerrarModalAperturaPuertaEquipoBiometrico())
    }

    const AbrirPuertaEquipoBiometrico = () => {
        AlertaSwal.MensajeConfirmacion({
            texto: '¿Está seguro de abrir la puerta de acceso?',
            textoBoton: 'Sí, abrir puerta de acceso',
            evento: () => {
                dispatch(GeneralAction.AbrirBackdrop())
                ServicioEquipoBiometrico.AbrirPuertaEquipoBiometrico(equipoBiometrico.EquipoBiometrico, _ => {
                    AlertaSwal.MensajeExito({
                        texto: 'La puerta ha sido abierta.',
                        evento: () => {
                            dispatch(GeneralAction.CerrarBackdrop())
                        }
                    })
                }, (codigoExcepcion) => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    CerrarSesionUsuario(codigoExcepcion)
                })
            }
        })
    }

    const EnviarSenalEquipoBiometrico = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioEquipoBiometrico.EnviarSenalEquipoBiometrico(equipoBiometrico.EquipoBiometrico, _ => {
            AlertaSwal.MensajeExito({
                texto: 'Señal enviada.',
                evento: () => {
                    dispatch(GeneralAction.CerrarBackdrop())
                }
            })
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const CerrarSesionUsuario = (codigoExcepcion) => {
        if (codigoExcepcion === Constante.CODIGO_EXCEPCION_ADVERTENCIA_SIMPLE_LOGOUT_USUARIO){
            dispatch(GeneralAction.SetDatosUsuarioNoLogueado())
            dispatch(GeneralAction.OcultarEncabezado())
            sessionStorage.removeItem(Constante.VARIABLE_LOCAL_STORAGE)
        }
    }

    return(
        <Dialog 
            aria-labelledby='aperturaEquipoBiometrico' 
            open={ equipoBiometrico.AbrirModal }
            fullWidth={ true }
            maxWidth={ 'sm' }>
            <TituloModal 
                id='aperturaEquipoBiometrico' 
                onClose={ CerrarModal }>
                Equipo Biométrico
            </TituloModal>
            <ContenidoModal dividers className={ claseEstilo.principal }>
                <Grid 
                    container 
                    spacing={ 2 } 
                    alignItems="flex-end">
                    <Grid 
                        item xs={ 12 }>
                        <Typography
                            className={ claseEstilo.texto }>
                            Nombre de equipo: { equipoBiometrico.EquipoBiometrico.NombreEquipoBiometrico } <br/>
                            IP: { equipoBiometrico.EquipoBiometrico.DireccionRedEquipoBiometrico }
                        </Typography>
                    </Grid>
                </Grid>
            </ContenidoModal>
            <SeccionAccionModal>
                <BotonEnviarSeñal
                    onClick={ EnviarSenalEquipoBiometrico }/>
                <BotonAbrirPuerta 
                    modal={ false }
                    onClick= { AbrirPuertaEquipoBiometrico }/>
            </SeccionAccionModal>
        </Dialog>
    )
}