import React, { useRef, useState, useEffect } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import * as PersonalEmpresaAction from '../../Accion/PersonalEmpresa'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioPersonalEmpresa from '../../Servicio/PersonalEmpresa'
import * as ServicioSeguridad from '../../Servicio/Seguridad'

import * as Utilitario from '../../Utilitario'

import { TituloModal } from '../ComponenteGeneral/TituloModal'
import { ContenidoModal } from '../ComponenteGeneral/ContenidoModal'
import { SeccionAccionModal } from '../ComponenteGeneral/SeccionAccionModal'
import { BotonIniciarProceso } from '../ComponenteGeneral/BotonIniciarProceso'

import { Dialog, Grid, makeStyles, Typography } from '@material-ui/core'
import Webcam from 'react-webcam'

const estilos = makeStyles({
    principal: {
        color: '#48525e',
        overflow: 'auto',
        width: 920
    },
    cuadroImagen: {
        display: 'flex',
        position: 'relative',
        justifyContent: 'center',
        alignItems: 'center'
    },
    imagen: {
        height: 450,
        width: 600
    },
    plantillaImagenIris: {
        position: 'absolute',
        border: '2px solid green',
        width: 240,
        height: 150,
        margin: '0 auto'
    },
    cuadroImagenIris: {
        height: 200,
        width: 300
    }
})

export const CapturaImagenIrisPersonalEmpresa = () => {
    const claseEstilo = estilos()
    const personalEmpresaFormulario = useSelector((store) => store.PersonalEmpresaFormulario)
    const dispatch = useDispatch()
    const webcam = useRef(null)
    const [imagenPrincipal, SetImagenPrincipal] = useState("")
    var imagenPrincipalBackup = ""

    useEffect(() => {
        const interval = setInterval(() => {
            CapturarImagenes()
        }, 0)
        return () => clearInterval(interval)
    }, [])

    const CapturarImagenes = () => {
        try{
            var imagen = webcam.current.getScreenshot()
            SetImagenPrincipal(imagen)
        }
        catch{ }
    }

    const CerrarCapturadorIris = () => {
        dispatch(PersonalEmpresaAction.CerrarFormularioCapturadorIris())
    }

    const CapturarImagenesDeIrisDetectados = () => {
        var imagenBase64 = imagenPrincipalBackup.split(',')[1].trim()
        if (imagenBase64.length !== 0){
            ServicioPersonalEmpresa.DetectarIrisEnImagen(imagenBase64, detecciones => {
                dispatch(PersonalEmpresaAction.SetFormularioPersonalEmpresaPorCampo("ImagenIris", 
                    Utilitario.AgregarPrefijoBase64(detecciones.ImagenOjo)))
                }, error => {
                    ServicioSeguridad.GuardarBitacoraDeErrorDelSistema(Constante.MODULO_PERSONAL_EMPRESA, 
                        Constante.RECURSO_PERSONAL_EMPRESA, 
                        personalEmpresaFormulario.CodigoPersonalEmpresa === '0' 
                            ? Constante.ACCION_REGISTRO_DATOS
                            : Constante.ACCION_MODIFICACION_DATOS, error)
                    CerrarCapturadorIris()
            })
        }
    }

    return(
        <>
            <Dialog 
                aria-labelledby='capturaImagenIrisPersonalEmpresa' 
                open={ personalEmpresaFormulario.ModalCapturadorIris }
                fullWidth={ true }
                maxWidth={ 'md' }>
                <TituloModal 
                    id='capturaImagenIrisPersonalEmpresa' 
                    onClose={ CerrarCapturadorIris }>
                    Captura de Imágenes de Iris
                </TituloModal>
                <ContenidoModal dividers className={ claseEstilo.principal }>
                    <Grid 
                        container 
                        spacing={ 1 } 
                        alignItems="flex-end">
                        <Grid 
                            item xs={ 8 }>
                                <div
                                    className={ claseEstilo.cuadroImagen }>
                                        <div 
                                            className={ claseEstilo.plantillaImagenIris } />
                                        <img
                                            alt=''
                                            className={ claseEstilo.imagen } 
                                            src={ imagenPrincipal }/>
                                </div>
                        </Grid>
                        <Grid 
                            item xs={ 4 }>
                            <Typography>Iris:</Typography>
                            <img
                                alt=''
                                className={ claseEstilo.cuadroImagenIris } 
                                src={ personalEmpresaFormulario.ImagenIris }/>
                        </Grid>
                    </Grid>
                </ContenidoModal>
                <SeccionAccionModal>
                    <BotonIniciarProceso 
                        texto='Iniciar Captura de las Imágenes de Iris'
                        onClick={ () => {
                            imagenPrincipalBackup = imagenPrincipal
                            AlertaSwal.MensajeConfirmacion({
                                texto: '¿Está seguro de iniciar la captura de las imágenes de iris?',
                                textoBoton: 'Sí, iniciar',
                                evento: () => CapturarImagenesDeIrisDetectados()
                            })
                        } }/>
                </SeccionAccionModal>
            </Dialog>
            {
                personalEmpresaFormulario.ModalCapturadorIris
                    ?   <Webcam
                            style={{ visibility: 'hidden' }}
                            audio={ false }
                            height={ 1080 }
                            ref={ webcam }
                            screenshotQuality={ 1 }
                            screenshotFormat='image/jpeg'
                            width={ 1920 }
                            videoConstraints={ {
                                width: 1920,
                                height: 1080,
                                facingMode: 'user'
                            } }
                        />
                    : <></>
            }
        </>
    )
}