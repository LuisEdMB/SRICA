import React, { useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { useForm } from 'react-hook-form'
import * as yup from 'yup'
import * as GeneralAction from '../../Accion/General'
import * as Constante from '../../Constante'

import * as Utilitario from '../../Utilitario'

import { BotonGeneral } from '../ComponenteGeneral/BotonGeneral'
import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioSeguridad from '../../Servicio/Seguridad'

import { Grid, CardContent, Card, makeStyles, FormControl, InputAdornment, IconButton, Typography, TextField } from '@material-ui/core' 
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

export const CambioContraseñaOlvidada = () => {
    const general = useSelector(store => store.General)
    const dispatch = useDispatch()
    const claseEstilo = estilos()
    const [nuevaContrasenaVisible, SetNuevaContrasenaVisible] = useState(false)
    const [confirmarNuevaContrasenaVisible, SetConfirmarNuevaContrasenaVisible] = useState(false)
    const [nuevaContrasena, SetNuevaContrasena] = useState('')
    const [confirmarNuevaContrasena, SetConfirmarNuevaContrasena] = useState('')
    const history = useHistory();
    const goHome = () => history.push('');

    const errores = yup.object().shape({
        NuevaContrasena: yup.string().trim().required('El campo "Nueva contraseña" no ' + 
            'debe estar vacío.'),
        ConfirmarNuevaContrasena: yup.string().trim().required('El campo "Confirmar ' + 
            'nueva contraseña" no debe estar vacío.')
    })
    const { register, handleSubmit, errors } = useForm({
        mode: 'onBlur',
        validationSchema: errores
    })

    const GuardarCambios = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioSeguridad.CambiarContrasenaOlvidada(general.TokenCambioContrasenaOlvidada,
            general.TokenDecodificadoCambioContrasenaOlvidada.Usuario, nuevaContrasena,
            confirmarNuevaContrasena, () => {
                dispatch(GeneralAction.CerrarBackdrop())
                AlertaSwal.MensajeExito({
                    texto: 'Se ha actualizado la contraseña.',
                    evento: () => {
                        RemoverTokenTemporal()
                        goHome()
                    }
                })
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                if (codigoExcepcion === Constante.CODIGO_EXCEPCION_ADVERTENCIA_SIMPLE_LOGOUT_USUARIO){
                    dispatch(GeneralAction.SetDatosUsuarioNoLogueado())
                    dispatch(GeneralAction.OcultarEncabezado())
                    sessionStorage.removeItem(Constante.VARIABLE_LOCAL_STORAGE)
                    RemoverTokenTemporal()
                }
            })
    }

    const RemoverTokenTemporal = () => {
        Utilitario.RemoverParametroDeURL()
        dispatch(GeneralAction.SetTokenCambioContrasenaOlvidada(''))
        dispatch(GeneralAction.SetTokenDecodificadoCambioContrasenaOlvidada(''))
        dispatch(GeneralAction.MostrarCambioContrasenaOlvidada(false))
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
                            <b>Cambio de Contraseña Olvidada</b>
                        </Typography>
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
                        <br/>
                        <Grid 
                            container 
                            spacing={ 2 } 
                            alignItems="flex-end">
                            <Grid 
                                item xs={ 12 }>
                                <BotonGeneral
                                    texto='Actualizar Contraseña'
                                    onClick={ handleSubmit(() => AlertaSwal.MensajeConfirmacion({
                                        texto: '¿Está seguro de actualizar la contraseña?',
                                        textoBoton: 'Sí, actualizar contraseña',
                                        evento: () => GuardarCambios()
                                    })) }/>
                            </Grid>
                        </Grid>
                    </CardContent>
                </Card>
            </Grid>      
        </Grid>
    )
}