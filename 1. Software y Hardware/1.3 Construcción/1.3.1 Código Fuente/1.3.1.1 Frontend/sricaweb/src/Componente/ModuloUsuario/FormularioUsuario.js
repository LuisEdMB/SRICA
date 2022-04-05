import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { useForm } from 'react-hook-form'
import * as yup from 'yup'
import * as GeneralAction from '../../Accion/General'
import * as UsuarioAction from '../../Accion/Usuario'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioUsuario from '../../Servicio/Usuario'

import { TituloModal } from '../ComponenteGeneral/TituloModal'
import { ContenidoModal } from '../ComponenteGeneral/ContenidoModal'
import { SeccionAccionModal } from '../ComponenteGeneral/SeccionAccionModal'
import { BotonGuardarCambios } from '../ComponenteGeneral/BotonGuardarCambios'
import { BotonGenerarContraseña } from '../ComponenteGeneral/BotonGenerarContraseña'

import { Dialog, Grid, FormControl, InputLabel, Input, makeStyles, Select, MenuItem, TextField } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        color: '#48525e'
    },
    campo1: {
        width: '100%'
    },
    campo2: {
        width: '50%'
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

export const FormularioUsuario = (props) => {
    const claseEstilo = estilos()
    const usuario = useSelector((store) => store.Usuario)
    const usuarioFormulario = useSelector((store) => store.UsuarioFormulario)
    const usuarioFormularioAnterior = useSelector((store) => store.UsuarioFormularioAnterior)
    const dispatch = useDispatch()

    const errores = yup.object().shape({
        Usuario: yup.string().trim().required('El campo "Usuario" no debe estar vacío.'),
        Nombre: yup.string().trim().required('El campo "Nombres" no debe estar vacío.'),
        Apellido: yup.string().trim().required('El campo "Apellidos" no debe estar vacío.'),
        CodigoRolUsuarioSelector: yup.string().trim().required(
            'Debe seleccionar un "Rol" para el usuario.')
    })
    const { register, handleSubmit, clearError, errors } = useForm({
        mode: 'onBlur',
        validationSchema: errores
    })

    const CambiarValorCampo = (e, selector, textSelector) => {
        const { name, value } = e.target
        dispatch(UsuarioAction.SetFormularioUsuarioPorCampo(name, value))
        if (selector !== undefined)
            dispatch(UsuarioAction.SetFormularioUsuarioPorCampo(selector, textSelector))
    }

    const CerrarFormularioUsuario = () => {
        dispatch(UsuarioAction.CerrarFormularioUsuario())
    }

    const GuardarCambios = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        if (usuarioFormulario.EstadoObjeto === Constante.ESTADO_OBJETO.Nuevo){
            ServicioUsuario.GuardarUsuario(usuarioFormulario, () => {
                dispatch(GeneralAction.CerrarBackdrop())
                AlertaSwal.MensajeExitoPorDefecto(() => {
                    CerrarFormularioUsuario()
                    props.RecargarListadoUsuario()
                })
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        }
        else if (usuarioFormulario.EstadoObjeto === Constante.ESTADO_OBJETO.Modificado){
            ServicioUsuario.ModificarUsuario(usuarioFormulario, usuarioFormularioAnterior, () => {
                dispatch(GeneralAction.CerrarBackdrop())
                AlertaSwal.MensajeExitoPorDefecto(() => {
                    CerrarFormularioUsuario()
                    props.RecargarListadoUsuario()
                })
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        }
    }

    const CambiarContrasenaPorDefecto = () => {
        dispatch(UsuarioAction.SetFormularioUsuarioPorCampo('EsContrasenaPorDefecto', true))
        AlertaSwal.MensajeAlerta({
            titulo: '¡Advertencia!',
            texto: 'Se ha generado la contraseña por defecto. Guarde los cambios para confirmar.',
            icono: 'warning'
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
            aria-labelledby='formularioUsuario' 
            open={ usuarioFormulario.ModalFormulario }
            fullWidth={ true }
            maxWidth={ 'xs' }>
            <TituloModal 
                id='formularioUsuario' 
                onClose={ CerrarFormularioUsuario }>
                Datos del Usuario
            </TituloModal>
            <ContenidoModal dividers className={ claseEstilo.principal }>
                <Grid 
                    container 
                    spacing={ 2 } 
                    alignItems="flex-end">
                    <Grid 
                        item xs={ 12 } 
                        sm={ 4 }>
                        <FormControl 
                            className = { claseEstilo.campo1 }>
                            <TextField 
                                error = { errors.Usuario } 
                                name='Usuario'
                                label='Usuario'
                                type='text'
                                value={ usuarioFormulario.Usuario }
                                onChange={ CambiarValorCampo }
                                inputRef={ register }/>
                        </FormControl>
                        {
                            errors.Usuario
                                ?   <Grid 
                                        container
                                        alignItems='flex-end'>
                                        <Grid 
                                            item xs={ 12 }>
                                            <p className={ claseEstilo.colorError }>
                                                { errors.Usuario.message }
                                            </p> 
                                        </Grid>
                                    </Grid>
                                : <br/>
                        }
                    </Grid>
                    {
                        usuarioFormulario.MostrarBotonGenerarContrasena
                            ?   <Grid 
                                    item xs={ 12 } 
                                    sm={ 8 }>
                                    <BotonGenerarContraseña
                                        onClick={ CambiarContrasenaPorDefecto }/>
                                </Grid>
                            : null
                    }
                </Grid>
                <br/>
                <Grid 
                    container 
                    spacing={ 2 } 
                    alignItems="flex-end">
                    <Grid 
                        item xs={ 12 }>
                        <FormControl 
                            className = { claseEstilo.campo1 }>
                            <TextField 
                                error = { errors.Nombre } 
                                name='Nombre'
                                label='Nombres'
                                type='text'
                                value={ usuarioFormulario.Nombre }
                                onChange={ CambiarValorCampo }
                                inputRef={ register }/>
                        </FormControl>
                    </Grid>
                </Grid>
                {
                    errors.Nombre
                        ?   <Grid 
                                container
                                alignItems='flex-end'>
                                <Grid 
                                    item xs={ 12 }>
                                    <p className={ claseEstilo.colorError }>
                                        { errors.Nombre.message }
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
                            className = { claseEstilo.campo1 }>
                            <TextField 
                                error = { errors.Apellido } 
                                name='Apellido'
                                label='Apellidos'
                                type='text'
                                value={ usuarioFormulario.Apellido }
                                onChange={ CambiarValorCampo }
                                inputRef={ register }/>
                        </FormControl>
                    </Grid>
                </Grid>
                {
                    errors.Apellido
                        ?   <Grid 
                                container
                                alignItems='flex-end'>
                                <Grid 
                                    item xs={ 12 }>
                                    <p className={ claseEstilo.colorError }>
                                        { errors.Apellido.message }
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
                        item xs={ 12 } 
                        sm={ 6 }>
                        <FormControl
                            className={ claseEstilo.selector }>
                            <InputLabel 
                                id="selectorRolUsuario" 
                                error={ errors.CodigoRolUsuarioSelector }>
                                Rol
                            </InputLabel>
                            <Select
                                labelId="selectorRolUsuario"
                                error={ errors.CodigoRolUsuarioSelector }
                                name='CodigoRolUsuario'
                                value={ usuarioFormulario.CodigoRolUsuario }
                                onChange={ (e, p) => { 
                                    CambiarValorCampo(e, 'RolUsuario', p.props.children)
                                    clearError('CodigoRolUsuarioSelector')
                                } }>
                                {
                                    usuario.Roles.map((rol) => 
                                        <MenuItem
                                            key={ rol.CodigoRolUsuario }
                                            value={ rol.CodigoRolUsuario }>
                                            { rol.DescripcionRolUsuario }
                                        </MenuItem>)
                                }
                            </Select>
                            <TextField 
                                name='CodigoRolUsuarioSelector'
                                type='hidden'
                                value={ usuarioFormulario.CodigoRolUsuario }
                                inputRef={ register }/>
                        </FormControl>
                    </Grid>
                </Grid>
                {
                    errors.CodigoRolUsuarioSelector
                        ?   <Grid 
                                container
                                alignItems='flex-end'>
                                <Grid 
                                    item xs={ 12 } 
                                    sm={ 6 }>
                                    <p className={ claseEstilo.colorError }>
                                        { errors.CodigoRolUsuarioSelector.message }
                                    </p> 
                                </Grid>
                            </Grid>
                        : <br/>
                }
                {
                    usuarioFormulario.CodigoUsuario === ''
                        ?   null
                        :   <Grid 
                                container 
                                spacing={ 2 } 
                                alignItems="flex-end">
                                <Grid 
                                    item xs={ 12 }>
                                    <FormControl 
                                        className = { claseEstilo.campo1 }>
                                        <InputLabel 
                                            htmlFor="correoElectronico">
                                            Correo electrónico
                                        </InputLabel>
                                        <Input
                                            id="correoElectronico"
                                            type='text'
                                            value={ usuarioFormulario.CorreoElectronico }
                                            readOnly/>
                                    </FormControl>
                                </Grid>
                            </Grid>
                }
            </ContenidoModal>
            <SeccionAccionModal>
                <BotonGuardarCambios 
                    onClick={ handleSubmit(() => AlertaSwal.MensajeConfirmacionPorDefecto(GuardarCambios)) }/>
            </SeccionAccionModal>
        </Dialog>
    )
}