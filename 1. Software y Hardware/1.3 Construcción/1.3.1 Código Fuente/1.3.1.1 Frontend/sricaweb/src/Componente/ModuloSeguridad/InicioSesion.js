import React, { useState } from 'react'
import { useDispatch } from 'react-redux'
import { useForm } from 'react-hook-form'
import * as yup from 'yup'
import * as GeneralAction from '../../Accion/General'
import * as Constante from '../../Constante'

import { BotonGeneral } from '../ComponenteGeneral/BotonGeneral'
import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioSeguridad from '../../Servicio/Seguridad'

import * as Utilitario from '../../Utilitario'

import { makeStyles, Grid, Card, CardContent, Typography, FormControl, InputAdornment, IconButton, TextField, Button } from '@material-ui/core'
import VisibilityOutlinedIcon from '@material-ui/icons/VisibilityOutlined'
import VisibilityOffOutlinedIcon from '@material-ui/icons/VisibilityOffOutlined'
import AccountCircleOutlinedIcon from '@material-ui/icons/AccountCircleOutlined'
import LockOutlinedIcon from '@material-ui/icons/LockOutlined'
import { useHistory } from 'react-router-dom'

const estilos = makeStyles({
    principal: {
        marginTop: 120,
        width: 300,
        textAlign: 'center',
        justifyContent: 'center',
        alignContent: 'center',
        color: '#48525e'
    },
    titulo: {
        fontSize: 20,
        fontWeight: 'bold'
    },
    tituloDetalle: {
        fontSize: 14,
        fontWeight: 'bold'
    },
    campo: {
        width: '100%'
    },
    colorIcon: {
        color: '#48525e'
    },
    textoRecuperarContrasena: {
        fontSize: 11,
        color: '#8e97a6'
    },
    textoRecuperarContrasenaBoton: {
        fontSize: 11,
        color: '#48525e',
        fontWeight: 'bold'
    },
    colorError: {
        fontSize: 11,
        fontWeight: 'bold',
        color: 'red',
        textAlign: 'left'
    }
})

export const InicioSesion = () => {
    const claseEstilo = estilos()
    const [contrasenaVisible, SetContrasenaVisible] = useState(false)
    const [usuario, SetUsuario] = useState('')
    const [contrasena, SetContrasena] = useState('')
    const dispatch = useDispatch()
    const history = useHistory();
    const goHome = () => history.push('');

    const errores = yup.object().shape({
        Usuario: yup.string().trim().required('El campo "Usuario" no debe estar vacío.'),
        Contrasena: yup.string().trim().required('El campo "Contraseña" no debe estar vacío.')
    })
    const { register, handleSubmit, errors } = useForm({
        mode: 'onBlur',
        validationSchema: errores
    })

    const IniciarSesion = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioSeguridad.IniciarSesion(usuario, contrasena, (respuesta) => {
            dispatch(GeneralAction.CerrarBackdrop())
            dispatch(GeneralAction.MostrarEncabezado())
            sessionStorage.setItem(Constante.VARIABLE_LOCAL_STORAGE, JSON.stringify(respuesta))
            dispatch(GeneralAction.SetDatosUsuarioLogueado(respuesta))
            goHome()
        }, () => dispatch(GeneralAction.CerrarBackdrop()))
    }

    const RecuperarContrasenaOlvidada = () => {
        if (usuario === '')
            AlertaSwal.MensajeAlerta({
                titulo: '¡Advertencia!',
                texto: 'El campo "Usuario" no debe estar vacío.',
                icono: 'warning'
            })
        else{
            AlertaSwal.MensajeConfirmacion({
                texto: '¿Está seguro de recuperar la contraseña para el usuario: ' + usuario + '?',
                textoBoton: 'Sí, recuperar contraseña',
                evento: () => {
                    dispatch(GeneralAction.AbrirBackdrop())
                    ServicioSeguridad.RecuperarContrasenaOlvidada(usuario, () => {
                        AlertaSwal.MensajeExito({
                            texto: 'Se ha enviado el correo de recuperación de contraseña al ' + 
                                'usuario indicado.',
                            evento: () => {
                                dispatch(GeneralAction.CerrarBackdrop())
                            }
                        })
                    }, () => dispatch(GeneralAction.CerrarBackdrop()))
                }
            })
        }
    }

    return(
        <Grid
            container
            direction='column'
            alignItems='center'
            justify='center'
            style={{ minHeight: '100%', marginBottom: 78 }}
            >
            <Grid 
                item xs={ 12 }>
                <Card 
                    className={ claseEstilo.principal }>
                    <CardContent>
                        <Typography 
                            component='h6'
                            className={ claseEstilo.titulo }>
                            SRICA
                        </Typography>
                        <Typography 
                            component='h6'
                            className={ claseEstilo.tituloDetalle }>
                            "Sistema de Reconocimiento de Iris para Control de Accesos"
                        </Typography>
                        <br/><br/>
                        <Typography 
                            component='h2'>
                            <b>Iniciar Sesión</b>
                        </Typography>
                        <br/>
                        <Grid 
                            container 
                            spacing={ 2 } 
                            alignItems='flex-end'>
                            <Grid 
                                item>
                                <AccountCircleOutlinedIcon 
                                    style={{ fontSize: 30 }}/>
                            </Grid>
                            <Grid 
                                item xs={ 9 }>
                                <FormControl
                                    className = { claseEstilo.campo }>
                                    <TextField 
                                        error = { errors.Usuario } 
                                        name='Usuario'
                                        label='Usuario'
                                        type='text'
                                        value={ usuario }
                                        onChange={ (e) => SetUsuario(e.target.value) }
                                        onKeyPress={ e => Utilitario.ManejarTeclaEnter(e, handleSubmit(IniciarSesion)) }
                                        inputRef={ register }/>
                                </FormControl>
                            </Grid>
                        </Grid>
                        {
                            errors.Usuario
                                ?   <Grid 
                                        container
                                        alignItems='flex-end'>
                                        <Grid 
                                            item xs={ 2 }>
                                        </Grid>
                                        <Grid 
                                            item xs={ 9 }>
                                            <p className={ claseEstilo.colorError }>
                                                { errors.Usuario.message }
                                            </p> 
                                        </Grid>
                                    </Grid>
                                : <br/>
                        }
                        <Grid 
                            container 
                            spacing={ 2 } 
                            alignItems='flex-end'>
                            <Grid 
                                item>
                                <LockOutlinedIcon 
                                    style={{ fontSize: 30 }}/>
                            </Grid>
                            <Grid 
                                item xs={ 9 }>
                                <FormControl 
                                    className = { claseEstilo.campo }>
                                    <TextField 
                                        error = { errors.Contrasena } 
                                        name='Contrasena'
                                        label='Contraseña'
                                        type={ contrasenaVisible ? 'text' : 'password' }
                                        value={ contrasena }
                                        onChange={ (e) => SetContrasena(e.target.value) }
                                        onKeyPress={ e => Utilitario.ManejarTeclaEnter(e, handleSubmit(IniciarSesion)) }
                                        inputRef={ register }
                                        InputProps={{
                                            endAdornment: (
                                                <InputAdornment 
                                                    position='end'>
                                                    <IconButton
                                                        aria-label='toggle password visibility'
                                                        onClick = { () => SetContrasenaVisible(!contrasenaVisible) }>
                                                        { contrasenaVisible ? 
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
                            errors.Contrasena
                                ?   <Grid 
                                        container
                                        alignItems='flex-end'>
                                        <Grid 
                                            item xs={ 2 }>
                                        </Grid>
                                        <Grid 
                                            item xs={ 9 }>
                                            <p className={ claseEstilo.colorError }>
                                                { errors.Contrasena.message }
                                            </p> 
                                        </Grid>
                                    </Grid>
                                : <br/>
                        }
                        <br/>
                        <Grid 
                            container 
                            spacing={ 2 } 
                            alignItems='flex-end'>
                            <Grid 
                                item xs={ 12 }>
                                <BotonGeneral
                                    texto='Iniciar Sesión'
                                    onClick={ handleSubmit(IniciarSesion) }/>
                            </Grid>
                        </Grid>
                    </CardContent>
                </Card>
                <br/>
            </Grid>
            <Typography 
                className={ claseEstilo.textoRecuperarContrasena }>
                    ¿Has olvidado tu contraseña? 
                    <Button 
                        className={ claseEstilo.textoRecuperarContrasenaBoton }
                        onClick={ () => RecuperarContrasenaOlvidada() }>
                        Recuperar contraseña
                    </Button>
            </Typography>
        </Grid>
    )
}