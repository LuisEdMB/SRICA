import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { useForm } from 'react-hook-form'
import * as yup from 'yup'
import * as GeneralAction from '../../Accion/General'
import * as PersonalEmpresaAction from '../../Accion/PersonalEmpresa'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioPersonalEmpresa from '../../Servicio/PersonalEmpresa'

import * as Utilitario from '../../Utilitario'

import { TituloModal } from '../ComponenteGeneral/TituloModal'
import { ContenidoModal } from '../ComponenteGeneral/ContenidoModal'
import { SeccionAccionModal } from '../ComponenteGeneral/SeccionAccionModal'
import { BotonSeleccionarArea } from '../ComponenteGeneral/BotonSeleccionarArea'
import { BotonCapturarImagenIris } from '../ComponenteGeneral/BotonCapturarImagenIris'
import { BotonGuardarCambios } from '../ComponenteGeneral/BotonGuardarCambios'
import { SelectorAreaPersonalEmpresa } from './SelectorAreaPersonalEmpresa'
import { CapturaImagenIrisPersonalEmpresa } from './CapturaImagenIrisPersonalEmpresa'

import { Dialog, Grid, FormControl, makeStyles, TextField } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        color: '#48525e'
    },
    campo: {
        width: '100%'
    },
    colorError: {
        fontSize: 11,
        fontWeight: 'bold',
        color: 'red',
        textAlign: 'left'
    }
})

export const FormularioEquipoBiometrico = (props) => {
    const claseEstilo = estilos()
    const personalEmpresa = useSelector((store) => store.PersonalEmpresa)
    const personalEmpresaFormulario = useSelector((store) => store.PersonalEmpresaFormulario)
    const personalEmpresaFormularioAnterior = useSelector((store) => store.PersonalEmpresaFormularioAnterior)
    const dispatch = useDispatch()

    const errores = yup.object().shape({
        DNIPersonalEmpresa: yup.string().trim().required('El campo "DNI" no debe estar vacío.'),
        NombrePersonalEmpresa: yup.string().trim().required('El campo "Nombres" no debe estar vacío.'),
        ApellidoPersonalEmpresa: yup.string().trim().required('El campo "Apellidos" no debe estar vacío.')
    })
    const { register, handleSubmit, errors } = useForm({
        mode: 'onBlur',
        validationSchema: errores
    })

    const CambiarValorCampo = (e) => {
        const { name, value } = e.target
        dispatch(PersonalEmpresaAction.SetFormularioPersonalEmpresaPorCampo(name, value))
    }

    const AbrirSelectorAreas = () => {
        dispatch(PersonalEmpresaAction.AbrirFormularioSelectorArea())
    }

    const AbrirCapturadorIris = () => {
        dispatch(PersonalEmpresaAction.AbrirFormularioCapturadorIris())
    }

    const CerrarFormularioPersonalEmpresa = () => {
        dispatch(PersonalEmpresaAction.CerrarFormularioPersonalEmpresa())
    }

    const GuardarCambios = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        var personal = Object.assign({}, personalEmpresaFormulario)
        personal.Areas = Utilitario.ObtenerListadoArrayDeArray(personalEmpresa.Sedes, 'Areas')
        if (personalEmpresaFormulario.EstadoObjeto === Constante.ESTADO_OBJETO.Nuevo){
            ServicioPersonalEmpresa.GuardarPersonalEmpresa(personal, () => {
                dispatch(GeneralAction.CerrarBackdrop())
                AlertaSwal.MensajeExitoPorDefecto(() => {
                    CerrarFormularioPersonalEmpresa()
                    props.RecargarListadoPersonalEmpresa()
                })
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        }
        else if (personalEmpresaFormulario.EstadoObjeto === Constante.ESTADO_OBJETO.Modificado){
            ServicioPersonalEmpresa.ModificarPersonalEmpresa(personal, 
                personalEmpresaFormularioAnterior, () => {
                dispatch(GeneralAction.CerrarBackdrop())
                AlertaSwal.MensajeExitoPorDefecto(() => {
                    CerrarFormularioPersonalEmpresa()
                    props.RecargarListadoPersonalEmpresa()
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
        <>
            <Dialog 
                aria-labelledby='formularioEquipoBiometrico' 
                open={ personalEmpresaFormulario.ModalFormulario }
                fullWidth={ true }
                maxWidth={ 'xs' }>
                <TituloModal 
                    id='formularioEquipoBiometrico' 
                    onClose={ CerrarFormularioPersonalEmpresa }>
                    Datos del Personal
                </TituloModal>
                <ContenidoModal dividers className={ claseEstilo.principal }>
                    <Grid 
                        container 
                        spacing={ 2 } 
                        alignItems="flex-end">
                        <Grid 
                            item xs={ 4 }>
                            <FormControl 
                                className = { claseEstilo.campo }>
                                <TextField 
                                    error = { errors.DNIPersonalEmpresa } 
                                    name='DNIPersonalEmpresa'
                                    label='DNI'
                                    type='text'
                                    value={ personalEmpresaFormulario.DNIPersonalEmpresa }
                                    onChange={ CambiarValorCampo }
                                    inputRef={ register }/>
                            </FormControl>
                            {
                                errors.DNIPersonalEmpresa
                                    ?   <Grid 
                                            container
                                            alignItems='flex-end'>
                                            <Grid 
                                                item xs={ 12 }>
                                                <p className={ claseEstilo.colorError }>
                                                    { errors.DNIPersonalEmpresa.message }
                                                </p> 
                                            </Grid>
                                        </Grid>
                                    : <br/>
                            }
                        </Grid>
                        <Grid 
                            item xs={ 12 } 
                            sm={ 8 }>
                            <BotonSeleccionarArea
                                onClick={ AbrirSelectorAreas }/>
                        </Grid>
                    </Grid>
                    <br/>
                    <Grid 
                        container 
                        spacing={ 2 } 
                        alignItems="flex-end">
                        <Grid 
                            item xs={ 12 }>
                            <FormControl 
                                className = { claseEstilo.campo }>
                                <TextField 
                                    error = { errors.NombrePersonalEmpresa } 
                                    name='NombrePersonalEmpresa'
                                    label='Nombres'
                                    type='text'
                                    value={ personalEmpresaFormulario.NombrePersonalEmpresa }
                                    onChange={ CambiarValorCampo }
                                    inputRef={ register }/>
                            </FormControl>
                        </Grid>
                    </Grid>
                    {
                        errors.NombrePersonalEmpresa
                            ?   <Grid 
                                    container
                                    alignItems='flex-end'>
                                    <Grid 
                                        item xs={ 12 }>
                                        <p className={ claseEstilo.colorError }>
                                            { errors.NombrePersonalEmpresa.message }
                                        </p> 
                                    </Grid>
                                </Grid>
                            : <br/>
                    }
                    <Grid 
                        container 
                        spacing={ 2 } 
                        alignItems="flex-end">
                        <Grid 
                            item xs={ 12 }>
                            <FormControl 
                                className = { claseEstilo.campo }>
                                <TextField 
                                    error = { errors.ApellidoPersonalEmpresa } 
                                    name='ApellidoPersonalEmpresa'
                                    label='Apellidos'
                                    type='text'
                                    value={ personalEmpresaFormulario.ApellidoPersonalEmpresa }
                                    onChange={ CambiarValorCampo }
                                    inputRef={ register }/>
                            </FormControl>
                        </Grid>
                    </Grid>
                    {
                        errors.ApellidoPersonalEmpresa
                            ?   <Grid 
                                    container
                                    alignItems='flex-end'>
                                    <Grid 
                                        item xs={ 12 }>
                                        <p className={ claseEstilo.colorError }>
                                            { errors.ApellidoPersonalEmpresa.message }
                                        </p> 
                                    </Grid>
                                </Grid>
                            : <br/>
                    }
                    <Grid 
                        container 
                        spacing={ 2 } 
                        alignItems="flex-end">
                        <Grid 
                            item xs={ 4 }>
                        </Grid>
                        <Grid 
                            item xs={ 12 } 
                            sm={ 8 }>
                            <BotonCapturarImagenIris
                                onClick={ AbrirCapturadorIris }/>
                        </Grid>
                        <SelectorAreaPersonalEmpresa/>
                    </Grid>
                </ContenidoModal>
                <SeccionAccionModal>
                    <BotonGuardarCambios
                        onClick={ handleSubmit(() => AlertaSwal.MensajeConfirmacionPorDefecto(GuardarCambios)) }/>
                </SeccionAccionModal>
            </Dialog>
            {
                personalEmpresaFormulario.ModalCapturadorIris && <CapturaImagenIrisPersonalEmpresa/>
            }
        </>
    )
}