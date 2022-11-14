import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { useForm } from 'react-hook-form'
import * as yup from 'yup'
import * as GeneralAction from '../../Accion/General'
import * as SedeEmpresaAction from '../../Accion/SedeEmpresa'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioSedeEmpresa from '../../Servicio/SedeEmpresa'

import { TituloModal } from '../ComponenteGeneral/TituloModal'
import { ContenidoModal } from '../ComponenteGeneral/ContenidoModal'
import { SeccionAccionModal } from '../ComponenteGeneral/SeccionAccionModal'
import { BotonGuardarCambios } from '../ComponenteGeneral/BotonGuardarCambios'

import { Dialog, Grid, FormControl, makeStyles, TextField } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        color: '#48525e'
    },
    campo: {
        width: '100%'
    },
    colorIcon: {
        color: '#48525e'
    },
    colorError: {
        fontSize: 11,
        fontWeight: 'bold',
        color: 'red',
        textAlign: 'left'
    }
})

export const FormularioSedeEmpresa = (props) => {
    const claseEstilo = estilos()
    const sedeFormulario = useSelector((store) => store.SedeEmpresaFormulario)
    const sedeFormularioAnterior = useSelector((store) => store.SedeEmpresaFormularioAnterior)
    const dispatch = useDispatch()
    
    const errores = yup.object().shape({
        DescripcionSede: yup.string().trim().required('El campo "Sede" no debe estar vacío.')
    })
    const { register, handleSubmit, errors } = useForm({
        mode: 'onBlur',
        validationSchema: errores
    })

    const CambiarValorCampo = (e) => {
        const { name, value } = e.target
        dispatch(SedeEmpresaAction.SetFormularioSedeEmpresaPorCampo(name, value))
    }

    const CerrarFormularioSedeEmpresa = () => {
        dispatch(SedeEmpresaAction.CerrarFormularioSedeEmpresa())
    }

    const GuardarCambios = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        if (sedeFormulario.EstadoObjeto === Constante.ESTADO_OBJETO.Nuevo){
            ServicioSedeEmpresa.GuardarSedeEmpresa(sedeFormulario, () => {
                dispatch(GeneralAction.CerrarBackdrop())
                AlertaSwal.MensajeExitoPorDefecto(() => {
                    CerrarFormularioSedeEmpresa()
                    props.RecargarListadoSedeEmpresa()
                })
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        }
        else if (sedeFormulario.EstadoObjeto === Constante.ESTADO_OBJETO.Modificado){
            ServicioSedeEmpresa.ModificarSedeEmpresa(sedeFormulario, sedeFormularioAnterior, () => {
                dispatch(GeneralAction.CerrarBackdrop())
                AlertaSwal.MensajeExitoPorDefecto(() => {
                    CerrarFormularioSedeEmpresa()
                    props.RecargarListadoSedeEmpresa()
                })
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        }
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
            aria-labelledby='formularioSedeEmpresa' 
            open={ sedeFormulario.ModalFormulario }
            fullWidth={ true }
            maxWidth={ 'xs' }>
            <TituloModal 
                id='formularioSedeEmpresa' 
                onClose={ CerrarFormularioSedeEmpresa }>
                Datos de la Sede
            </TituloModal>
            <ContenidoModal dividers className={ claseEstilo.principal }>
                <Grid 
                    container 
                    spacing={ 2 } 
                    alignItems="flex-end">
                    <Grid 
                        item xs={ 12 }>
                        <FormControl 
                            className = { claseEstilo.campo }>
                            <TextField 
                                error = { errors.DescripcionSede } 
                                name='DescripcionSede'
                                label='Sede'
                                type='text'
                                value={ sedeFormulario.DescripcionSede }
                                onChange={ CambiarValorCampo }
                                inputRef={ register }/>
                        </FormControl>
                    </Grid>
                </Grid>
                {
                    errors.DescripcionSede
                        ?   <Grid 
                                container
                                alignItems='flex-end'>
                                <Grid 
                                    item xs={ 12 }>
                                    <p className={ claseEstilo.colorError }>
                                        { errors.DescripcionSede.message }
                                    </p> 
                                </Grid>
                            </Grid>
                        : <br/>
                }
            </ContenidoModal>
            <SeccionAccionModal>
                <BotonGuardarCambios
                    onClick={ handleSubmit(() => AlertaSwal.MensajeConfirmacionPorDefecto(GuardarCambios)) }/>
            </SeccionAccionModal>
        </Dialog>
    )
}