import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import * as GeneralAction from '../../Accion/General'
import * as PersonalEmpresaAction from '../../Accion/PersonalEmpresa'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioPersonalEmpresa from '../../Servicio/PersonalEmpresa'

import { useCamera } from '../../Hooks/useCamera'

import { TituloModal } from '../ComponenteGeneral/TituloModal'
import { ContenidoModal } from '../ComponenteGeneral/ContenidoModal'
import { SeccionAccionModal } from '../ComponenteGeneral/SeccionAccionModal'
import { BotonIniciarProceso } from '../ComponenteGeneral/BotonIniciarProceso'

import { Dialog, Grid, makeStyles } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        color: '#48525e',
        overflow: 'auto'
    },
    cuadroImagen: {
        display: 'flex',
        position: 'relative',
        justifyContent: 'center',
        alignItems: 'center'
    },
    plantillaImagenIris: {
        position: 'absolute',
        border: '2px solid green',
        width: 115,
        height: 80,
        margin: '0 auto'
    }
})

export const CapturaReconocimientoIrisPersonalEmpresa = () => {
    const [ cameraRef, takeImage ] = useCamera()
    const claseEstilo = estilos()
    const personalEmpresaFormulario = useSelector((store) => store.PersonalEmpresaFormulario)
    const dispatch = useDispatch()

    const CerrarCapturadorIris = () => {
        dispatch(PersonalEmpresaAction.CerrarComprobarReconocimientoIris())
    }

    const ComprobarReconocimientoIris = (imagenBase64) => {
        dispatch(GeneralAction.AbrirBackdrop())
        if (imagenBase64) {
            ServicioPersonalEmpresa.DetectarIrisEnImagen(imagenBase64, detecciones => {
                const imagenOjo = detecciones.ImagenOjo
                if (imagenOjo) ReconocerPersonalPorElIris(imagenOjo)
                else {
                    dispatch(GeneralAction.CerrarBackdrop())
                    AlertaSwal.MensajeAlerta({
                        titulo: '¡Advertencia!',
                        texto: 'No se ha detectado algún iris en la imagen. Intente otra vez.',
                        icono: 'warning'
                    })
                }}, _ => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    CerrarCapturadorIris()
                })
        }
    }

    const ReconocerPersonalPorElIris = (imagenIris) => {
        ServicioPersonalEmpresa.ReconocerPersonalPorElIris(imagenIris, personal => {
            dispatch(GeneralAction.CerrarBackdrop())
            const datosPersona = { ...personal?.PersonalEmpresa }
            if (personal?.PersonalEmpresa?.DNIPersonalEmpresa) AlertaSwal.MensajeExito({
                texto: `Verificación Exitosa: DNI - ${ datosPersona?.DNIPersonalEmpresa } 
                    :: Nombres - ${ datosPersona?.NombrePersonalEmpresa } ${ datosPersona?.ApellidoPersonalEmpresa }`,
                evento: () => { }
            })
            else AlertaSwal.MensajeAlerta({
                titulo: '¡Advertencia!',
                texto: 'No se ha podido reconocer a la persona. Intente otra vez.',
                icono: 'warning'
            })
        }, () => dispatch(GeneralAction.CerrarBackdrop()))
    }

    return (
        <Dialog 
            aria-labelledby='capturaImagenIrisPersonalEmpresa' 
            open={ personalEmpresaFormulario.ModalComprobarReconocimientoIris }
            fullWidth={ true }
            maxWidth={ 'sm' }>
            <TituloModal 
                id='capturaImagenIrisPersonalEmpresa' 
                onClose={ CerrarCapturadorIris }>
                Verificación de Iris
            </TituloModal>
            <ContenidoModal dividers className={ claseEstilo.principal }>
                <Grid 
                    container 
                    spacing={ 1 } 
                    alignItems="flex-end">
                    <Grid 
                        item xs={ 12 }>
                            <div
                                className={ claseEstilo.cuadroImagen }>
                                    <div 
                                        className={ claseEstilo.plantillaImagenIris } />
                                    <video
                                        ref={ cameraRef }
                                        style={{ width: 600 }} />
                            </div>
                    </Grid>
                </Grid>
            </ContenidoModal>
            <SeccionAccionModal>
                <BotonIniciarProceso 
                    texto='Iniciar Verificación de Iris'
                    onClick={ () => 
                        takeImage()?.then(base64 => 
                            AlertaSwal.MensajeConfirmacion({
                                texto: '¿Está seguro de iniciar con la verificación del iris?',
                                textoBoton: 'Sí, verificar',
                                evento: () => ComprobarReconocimientoIris(base64)
                            }))
                    }/>
            </SeccionAccionModal>
        </Dialog>   
    )
}