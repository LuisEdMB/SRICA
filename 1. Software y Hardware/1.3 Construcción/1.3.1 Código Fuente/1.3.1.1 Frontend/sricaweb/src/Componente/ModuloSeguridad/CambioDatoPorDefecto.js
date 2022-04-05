import React, { useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { useForm } from 'react-hook-form'
import * as yup from 'yup'
import * as GeneralAction from '../../Accion/General'
import * as Constante from '../../Constante'

import { BotonGeneral } from '../ComponenteGeneral/BotonGeneral'
import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioSeguridad from '../../Servicio/Seguridad'

import { makeStyles, Grid, Card, CardContent, Typography, FormControl, InputAdornment, IconButton, TextField } from '@material-ui/core'
import VisibilityOutlinedIcon from '@material-ui/icons/VisibilityOutlined'
import VisibilityOffOutlinedIcon from '@material-ui/icons/VisibilityOffOutlined'
import { useHistory } from 'react-router-dom'


const estilos = makeStyles({
    principal: {
        marginTop: 150,
        marginBottom: 80,
        width: 300,
        textAlign: 'center',
        justifyContent: 'center',
        alignContent: 'center',
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

export const CambioDatoPorDefecto = () => {
    const claseEstilo = estilos()
    const [nuevaContrasenaVisible, SetNuevaContrasenaVisible] = useState(false)
    const [confirmarNuevaContrasenaVisible, SetConfirmarNuevaContrasenaVisible] = useState(false)
    const [correoElectronico, SetCorreoElectronico] = useState('')
    const [nuevaContrasena, SetNuevaContrasena] = useState('')
    const [confirmarNuevaContrasena, SetConfirmarNuevaContrasena] = useState('')
    const generalUsuarioLogueado = useSelector(store => store.GeneralUsuarioLogueado)
    const dispatch = useDispatch()
    const history = useHistory();
    const goHome = () => history.push('');
    
    const errores = yup.object().shape({
        CorreoElectronico: yup.string().trim().required('El campo "Correo electrónico" ' + 
            'no debe estar vacío.'),
        NuevaContrasena: yup.string().trim().required('El campo "Nueva contraseña" no ' + 
            'debe estar vacío.'),
        ConfirmarNuevaContrasena: yup.string().trim().required('El campo "Confirmar ' + 
            'nueva contraseña" no debe estar vacío.')
    })
    const erroresSinCorreoElectronico = yup.object().shape({
        NuevaContrasena: yup.string().trim().required('El campo "Nueva contraseña" no debe ' +
            'estar vacío.'),
        ConfirmarNuevaContrasena: yup.string().trim().required('El campo "Confirmar nueva ' +
            'contraseña" no debe estar vacío.')
    })
    const { register, handleSubmit, errors } = useForm({
        mode: 'onBlur',
        validationSchema: generalUsuarioLogueado.EsCorreoElectronicoPorDefecto 
            ? errores 
            : erroresSinCorreoElectronico
    })

    const GuardarCambios = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioSeguridad.CambiarDatosPorDefecto(generalUsuarioLogueado.CodigoUsuario, 
            correoElectronico, nuevaContrasena, confirmarNuevaContrasena, 
            !generalUsuarioLogueado.EsCorreoElectronicoPorDefecto, () => {
            dispatch(GeneralAction.CerrarBackdrop())
            var usuario = JSON.parse(sessionStorage.getItem(Constante.VARIABLE_LOCAL_STORAGE))
            sessionStorage.removeItem(Constante.VARIABLE_LOCAL_STORAGE)
            usuario.EsCorreoElectronicoPorDefecto = false
            usuario.EsContrasenaPorDefecto = false
            sessionStorage.setItem(Constante.VARIABLE_LOCAL_STORAGE, JSON.stringify(usuario))
            AlertaSwal.MensajeExitoPorDefecto(() => {
                dispatch(GeneralAction.SetDatosUsuarioLogueado(usuario))
                dispatch(GeneralAction.MostrarEncabezado())
                goHome()
            })
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            if (codigoExcepcion === Constante.CODIGO_EXCEPCION_ADVERTENCIA_SIMPLE_LOGOUT_USUARIO){
                dispatch(GeneralAction.SetDatosUsuarioNoLogueado())
                dispatch(GeneralAction.OcultarEncabezado())
                sessionStorage.removeItem(Constante.VARIABLE_LOCAL_STORAGE)
            }
        })
    }

    return(
        <Grid
            container
            direction="column"
            alignItems="center"
            justify="center"
            style={{ minHeight: '100%' }}
            >
            <Grid 
                item xs={ 12 }>
                <Card 
                    className={ claseEstilo.principal }>
                    <CardContent>
                        <Typography 
                            variant="h5" 
                            component="h2">
                            <b>Cambio de Datos por Defecto</b>
                        </Typography>
                        {
                            generalUsuarioLogueado.EsCorreoElectronicoPorDefecto
                                ?   (<div>
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
                                                        error = { errors.CorreoElectronico } 
                                                        name='CorreoElectronico'
                                                        label='Correo electrónico'
                                                        type='text'
                                                        value={ correoElectronico }
                                                        onChange={ (e) => SetCorreoElectronico(e.target.value) }
                                                        inputRef={ register }/>
                                                </FormControl>
                                            </Grid>
                                        </Grid>
                                        {
                                            errors.CorreoElectronico
                                                ?   <Grid 
                                                        container
                                                        alignItems='flex-end'>
                                                        <Grid 
                                                            item xs={ 12 }>
                                                            <p className={ claseEstilo.colorError }>
                                                                { errors.CorreoElectronico.message }
                                                            </p> 
                                                        </Grid>
                                                    </Grid>
                                                : <br/>
                                        }
                                    </div>)
                                :   <br/>
                        }
                        {
                            generalUsuarioLogueado.EsContrasenaPorDefecto
                                ?   (<div>
                                        <Grid 
                                            container 
                                            spacing={ 2 } 
                                            alignItems="flex-end">
                                            <Grid 
                                                item xs={ 12 }>
                                                <FormControl 
                                                    className = { claseEstilo.campo }>
                                                    <TextField 
                                                        error = { errors.NuevaContrasena } 
                                                        name='NuevaContrasena'
                                                        label='Nueva contraseña'
                                                        type={ nuevaContrasenaVisible ? 'text' : 'password' }
                                                        value={ nuevaContrasena }
                                                        onChange={ (e) => SetNuevaContrasena(e.target.value) }
                                                        inputRef={ register }
                                                        InputProps={{
                                                            endAdornment: (
                                                                <InputAdornment 
                                                                    position="end">
                                                                    <IconButton
                                                                        onClick = { () => SetNuevaContrasenaVisible(!nuevaContrasenaVisible) }>
                                                                        { nuevaContrasenaVisible ? 
                                                                            <VisibilityOutlinedIcon 
                                                                                className={ claseEstilo.colorIcon }/> : 
                                                                            <VisibilityOffOutlinedIcon 
                                                                                className={ claseEstilo.colorIcon }/> }
                                                                    </IconButton>
                                                                </InputAdornment>
                                                            )
                                                        }}/>
                                                </FormControl>
                                            </Grid>
                                        </Grid>
                                        {
                                            errors.NuevaContrasena
                                                ?   <Grid 
                                                        container
                                                        alignItems='flex-end'>
                                                        <Grid 
                                                            item xs={ 12 }>
                                                            <p className={ claseEstilo.colorError }>
                                                                { errors.NuevaContrasena.message }
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
                                                        error = { errors.ConfirmarNuevaContrasena } 
                                                        name='ConfirmarNuevaContrasena'
                                                        label='Confirmar nueva contraseña'
                                                        type={ confirmarNuevaContrasenaVisible ? 'text' : 'password' }
                                                        value={ confirmarNuevaContrasena }
                                                        onChange={ (e) => SetConfirmarNuevaContrasena(e.target.value) }
                                                        inputRef={ register }
                                                        InputProps={{
                                                            endAdornment: (
                                                                <InputAdornment 
                                                                    position="end">
                                                                    <IconButton
                                                                        onClick = { () => SetConfirmarNuevaContrasenaVisible(!confirmarNuevaContrasenaVisible) }>
                                                                        { confirmarNuevaContrasenaVisible ? 
                                                                            <VisibilityOutlinedIcon 
                                                                                className={ claseEstilo.colorIcon }/> : 
                                                                            <VisibilityOffOutlinedIcon 
                                                                                className={ claseEstilo.colorIcon }/> }
                                                                    </IconButton>
                                                                </InputAdornment>
                                                            )
                                                        }}/>
                                                </FormControl>
                                            </Grid>
                                        </Grid>
                                        {
                                            errors.ConfirmarNuevaContrasena
                                                ?   <Grid 
                                                        container
                                                        alignItems='flex-end'>
                                                        <Grid 
                                                            item xs={ 12 }>
                                                            <p className={ claseEstilo.colorError }>
                                                                { errors.ConfirmarNuevaContrasena.message }
                                                            </p> 
                                                        </Grid>
                                                    </Grid>
                                                : <br/>
                                        }
                                    </div>)
                                : <br/>
                        }
                        <br/>
                        <Grid 
                            container 
                            spacing={ 2 } 
                            alignItems="flex-end">
                            <Grid 
                                item xs={ 12 }>
                                <BotonGeneral
                                    texto='Guardar Cambios'
                                    onClick={ handleSubmit(() => AlertaSwal.MensajeConfirmacionPorDefecto(GuardarCambios)) }/>
                            </Grid>
                        </Grid>
                    </CardContent>
                </Card>
            </Grid>      
        </Grid>
    )
}