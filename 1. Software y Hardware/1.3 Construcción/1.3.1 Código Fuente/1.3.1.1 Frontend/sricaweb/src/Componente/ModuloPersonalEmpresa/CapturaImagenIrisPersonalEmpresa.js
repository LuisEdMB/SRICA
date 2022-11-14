import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import * as PersonalEmpresaAction from '../../Accion/PersonalEmpresa'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioPersonalEmpresa from '../../Servicio/PersonalEmpresa'
import * as ServicioSeguridad from '../../Servicio/Seguridad'

import * as Utilitario from '../../Utilitario'

import { useCamera } from '../../Hooks/useCamera'

import { TituloModal } from '../ComponenteGeneral/TituloModal'
import { ContenidoModal } from '../ComponenteGeneral/ContenidoModal'
import { SeccionAccionModal } from '../ComponenteGeneral/SeccionAccionModal'
import { BotonIniciarProceso } from '../ComponenteGeneral/BotonIniciarProceso'

import { Dialog, Grid, makeStyles, Typography } from '@material-ui/core'

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
    plantillaImagenIris: {
        position: 'absolute',
        border: '2px solid green',
        width: 115,
        height: 80,
        margin: '0 auto'
    },
    cuadroImagenIris: {
        height: 200,
        width: 300
    }
})

export const CapturaImagenIrisPersonalEmpresa = () => {
    const [ cameraRef, takeImage ] = useCamera()
    const claseEstilo = estilos()
    const personalEmpresaFormulario = useSelector((store) => store.PersonalEmpresaFormulario)
    const dispatch = useDispatch()

    const CerrarCapturadorIris = () => {
        dispatch(PersonalEmpresaAction.CerrarFormularioCapturadorIris())
    }

    const CapturarImagenesDeIrisDetectados = (imagenBase64) => {
        if (imagenBase64) {
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
                                    <video
                                        ref={ cameraRef }
                                        style={{ width: 600 }} />
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
                    onClick={ () => 
                        takeImage()?.then(base64 => 
                            AlertaSwal.MensajeConfirmacion({
                                texto: '¿Está seguro de iniciar la captura de las imágenes de iris?',
                                textoBoton: 'Sí, iniciar',
                                evento: () => CapturarImagenesDeIrisDetectados(base64)
                            }))
                    }/>
            </SeccionAccionModal>
        </Dialog>
    )
}