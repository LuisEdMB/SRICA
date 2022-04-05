import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { useForm } from 'react-hook-form'
import * as yup from 'yup'
import * as GeneralAction from '../../Accion/General'
import * as AreaEmpresaAction from '../../Accion/AreaEmpresa'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioAreaEmpresa from '../../Servicio/AreaEmpresa'

import { TituloModal } from '../ComponenteGeneral/TituloModal'
import { ContenidoModal } from '../ComponenteGeneral/ContenidoModal'
import { SeccionAccionModal } from '../ComponenteGeneral/SeccionAccionModal'
import { BotonGuardarCambios } from '../ComponenteGeneral/BotonGuardarCambios'

import { Dialog, Grid, FormControl, InputLabel, makeStyles, Select, MenuItem, TextField } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        color: '#48525e'
    },
    campo: {
        width: '100%'
    },
    selector: {
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

export const FormularioAreaEmpresa = (props) => {
    const claseEstilo = estilos()
    const area = useSelector((store) => store.AreaEmpresa)
    const areaFormulario = useSelector((store) => store.AreaEmpresaFormulario)
    const areaFormularioAnterior = useSelector((store) => store.AreaEmpresaFormularioAnterior)
    const dispatch = useDispatch();

    const errores = yup.object().shape({
        DescripcionArea: yup.string().trim().required('El campo "Área" no debe estar vacío.'),
        CodigoSedeSelector: yup.string().trim().required('Debe seleccionar una sede para el área.')
    })
    const { register, handleSubmit, clearError, errors } = useForm({
        mode: 'onBlur',
        validationSchema: errores
    })
    
    const CambiarValorCampo = (e, selector, textSelector) => {
        const { name, value } = e.target
        dispatch(AreaEmpresaAction.SetFormularioAreaEmpresaPorCampo(name, value))
        if (selector !== undefined)
            dispatch(AreaEmpresaAction.SetFormularioAreaEmpresaPorCampo(selector, textSelector))
    }

    const CerrarFormularioAreaEmpresa = () => {
        dispatch(AreaEmpresaAction.CerrarFormularioAreaEmpresa())
    }

    const GuardarCambios = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        if (areaFormulario.EstadoObjeto === Constante.ESTADO_OBJETO.Nuevo){
            ServicioAreaEmpresa.GuardarAreaEmpresa(areaFormulario, () => {
                dispatch(GeneralAction.CerrarBackdrop())
                AlertaSwal.MensajeExitoPorDefecto(() => {
                    CerrarFormularioAreaEmpresa()
                    props.RecargarListadoAreaEmpresa()
                })
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        }
        else if (areaFormulario.EstadoObjeto === Constante.ESTADO_OBJETO.Modificado){
            ServicioAreaEmpresa.ModificarAreaEmpresa(areaFormulario, areaFormularioAnterior, () => {
                dispatch(GeneralAction.CerrarBackdrop())
                AlertaSwal.MensajeExitoPorDefecto(() => {
                    CerrarFormularioAreaEmpresa()
                    props.RecargarListadoAreaEmpresa()
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
            aria-labelledby='formularioAreaEmpresa' 
            open={ areaFormulario.ModalFormulario }
            fullWidth={ true }
            maxWidth={ 'xs' }>
            <TituloModal 
                id='formularioAreaEmpresa' 
                onClose={ CerrarFormularioAreaEmpresa }>
                Datos del Área
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
                                error = { errors.DescripcionArea } 
                                name='DescripcionArea'
                                label='Área'
                                type='text'
                                value={ areaFormulario.DescripcionArea }
                                onChange={ CambiarValorCampo }
                                inputRef={ register }/>
                        </FormControl>
                    </Grid>
                </Grid>
                {
                    errors.DescripcionArea
                        ?   <Grid 
                                container
                                alignItems='flex-end'>
                                <Grid 
                                    item xs={ 12 }>
                                    <p className={ claseEstilo.colorError }>
                                        { errors.DescripcionArea.message }
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
                            className={ claseEstilo.selector }>
                            <InputLabel 
                                id="selectorSedeArea"
                                error={ errors.CodigoSedeSelector }>
                                Sede
                            </InputLabel>
                            <Select
                                labelId="selectorSedeArea"
                                error={ errors.CodigoSedeSelector }
                                name='CodigoSede'
                                value={ areaFormulario.CodigoSede }
                                onChange={ (e, p) => {
                                    CambiarValorCampo(e, 'DescripcionSede', p.props.children)
                                    clearError('CodigoSedeSelector')
                                } }>
                                {
                                    area.Sedes.map((sede) => 
                                        <MenuItem 
                                            key={ sede.CodigoSede }
                                            value={ sede.CodigoSede }>
                                            { sede.DescripcionSede }
                                        </MenuItem>)
                                }
                            </Select>
                            <TextField 
                                name='CodigoSedeSelector'
                                type='hidden'
                                value={ areaFormulario.CodigoSede }
                                inputRef={ register }/>
                        </FormControl>
                    </Grid>
                </Grid>
                {
                    errors.CodigoSedeSelector
                        ?   <Grid 
                                container
                                alignItems='flex-end'>
                                <Grid 
                                    item xs={ 12 }>
                                    <p className={ claseEstilo.colorError }>
                                        { errors.CodigoSedeSelector.message }
                                    </p> 
                                </Grid>
                            </Grid>
                        : null
                }
            </ContenidoModal>
            <SeccionAccionModal>
                <BotonGuardarCambios
                    onClick={ handleSubmit(() => AlertaSwal.MensajeConfirmacionPorDefecto(GuardarCambios)) }/>
            </SeccionAccionModal>
        </Dialog>
    )
}