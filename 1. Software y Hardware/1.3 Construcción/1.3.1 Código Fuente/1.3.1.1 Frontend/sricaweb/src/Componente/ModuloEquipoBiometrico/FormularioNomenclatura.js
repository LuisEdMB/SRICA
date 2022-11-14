import React from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { useForm } from 'react-hook-form'
import * as yup from 'yup'
import * as GeneralAction from '../../Accion/General'
import * as NomenclaturaAction from '../../Accion/Nomenclatura'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioNomenclatura from '../../Servicio/Nomenclatura'

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

export const FormularioNomenclatura = (props) => {
    const claseEstilo = estilos()
    const nomenclaturaFormulario = useSelector((store) => store.NomenclaturaFormulario)
    const nomenclaturaFormularioAnterior = useSelector((store) => store.NomenclaturaFormularioAnterior)
    const dispatch = useDispatch()

    const errores = yup.object().shape({
        DescripcionNomenclatura: yup.string().trim().required('El campo "Nomenclatura" no debe estar vacío.')
    })
    const { register, handleSubmit, errors } = useForm({
        mode: 'onBlur',
        validationSchema: errores
    })

    const CambiarValorCampo = (e) => {
        const { name, value } = e.target
        dispatch(NomenclaturaAction.SetFormularioNomenclaturaPorCampo(name, value))
    }

    const CerrarFormularioNomenclatura = () => {
        dispatch(NomenclaturaAction.CerrarFormularioNomenclatura())
    }

    const GuardarCambios = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        if (nomenclaturaFormulario.EstadoObjeto === Constante.ESTADO_OBJETO.Nuevo){
            ServicioNomenclatura.GuardarNomenclatura(nomenclaturaFormulario, () => {
                dispatch(GeneralAction.CerrarBackdrop())
                AlertaSwal.MensajeExitoPorDefecto(() => {
                    CerrarFormularioNomenclatura()
                    props.RecargarListadoNomenclatura()
                })
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        }
        else if (nomenclaturaFormulario.EstadoObjeto === Constante.ESTADO_OBJETO.Modificado){
            ServicioNomenclatura.ModificarNomenclatura(nomenclaturaFormulario, 
                nomenclaturaFormularioAnterior, () => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    AlertaSwal.MensajeExitoPorDefecto(() => {
                        CerrarFormularioNomenclatura()
                        props.RecargarListadoNomenclatura()
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
            aria-labelledby='formularioNomenclatura' 
            open={ nomenclaturaFormulario.ModalFormulario }
            fullWidth={ true }
            maxWidth={ 'xs' }>
            <TituloModal 
                id='formularioNomenclatura' 
                onClose={ CerrarFormularioNomenclatura }>
                Datos de la Nomenclatura
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
                                error = { errors.DescripcionNomenclatura } 
                                name='DescripcionNomenclatura'
                                label='Nomenclatura'
                                type='text'
                                value={ nomenclaturaFormulario.DescripcionNomenclatura }
                                onChange={ CambiarValorCampo }
                                inputRef={ register }/>
                        </FormControl>
                    </Grid>
                </Grid>
                {
                    errors.DescripcionNomenclatura
                        ?   <Grid 
                                container
                                alignItems='flex-end'>
                                <Grid 
                                    item xs={ 12 }>
                                    <p className={ claseEstilo.colorError }>
                                        { errors.DescripcionNomenclatura.message }
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