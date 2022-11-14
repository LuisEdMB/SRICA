import React, { useState, useEffect } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { useForm } from 'react-hook-form'
import * as yup from 'yup'
import * as GeneralAction from '../../Accion/General'
import * as Constante from '../../Constante'

import { BotonGeneral } from '../ComponenteGeneral/BotonGeneral'
import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioPerfilUsuario from '../../Servicio/PerfilUsuario'

import { makeStyles, Grid, Card, CardContent, Typography, FormControl, InputLabel, Input, InputAdornment, IconButton, TextField } from '@material-ui/core'
import VisibilityOutlinedIcon from '@material-ui/icons/VisibilityOutlined'
import VisibilityOffOutlinedIcon from '@material-ui/icons/VisibilityOffOutlined'

const estilos = makeStyles({
    principal: {
        marginTop: 100,
        marginBottom: 20,
        width: 350,
        color: '#48525e'
    },
    centrar: {
        textAlign: 'center',
        justifyContent: 'center'
    },
    campo1: {
        width: '30%'
    },
    campo2: {
        width: '100%'
    },
    campo3: {
        width: '70%'
    },
    campo4: {
        width: '50%'
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

export const MiPerfil = () => {
    const claseEstilo = estilos()
    const [contrasenaVisible, SetContrasenaVisible] = useState(false)
    const [confirmarContrasenaVisible, SetConfirmarContrasenaVisible] = useState(false)
    const [usuario, SetUsuario] = useState({
        CodigoUsuario: '',
        Usuario: '',
        Nombre: '',
        Apellido: '',
        Contrasena: '',
        ConfirmarContrasena: '',
        CorreoElectronico: '',
        RolUsuario: '',
        UltimosAccesos: []
    })
    const [usuarioAnterior, SetUsuarioAnterior] = useState(usuario)
    const generalUsuarioLogueado = useSelector(store => store.GeneralUsuarioLogueado)
    const dispatch = useDispatch()

    const errores = yup.object().shape({
        CorreoElectronico: yup.string().trim().required('El campo "Correo electrónico" ' + 
            'no debe estar vacío.'),
        Contrasena: yup.string().trim().required('El campo "Contraseña" no debe estar vacío.'),
        ConfirmarContrasena: yup.string().trim().required('El campo "Confirmar contraseña" ' + 
            'no debe estar vacío.')
    })
    const erroresSinContrasena = yup.object().shape({
        CorreoElectronico: yup.string().trim().required('El campo "Correo electrónico" ' + 
            'no debe estar vacío.')
    })
    const { register, handleSubmit, errors } = useForm({
        mode: (usuario.Contrasena === '' && usuario.ConfirmarContrasena === '') ||
            (usuario.Contrasena === null && usuario.ConfirmarContrasena === null) ||
            (usuario.Contrasena === '' && usuario.ConfirmarContrasena === null) ||
            (usuario.Contrasena === null && usuario.ConfirmarContrasena === '')
            ? 'onChange'
            : 'onBlur',
        validationSchema: 
            (usuario.Contrasena === '' && usuario.ConfirmarContrasena === '') ||
            (usuario.Contrasena === null && usuario.ConfirmarContrasena === null) ||
            (usuario.Contrasena === '' && usuario.ConfirmarContrasena === null) ||
            (usuario.Contrasena === null && usuario.ConfirmarContrasena === '')
                ? erroresSinContrasena
                : errores
    })

    useEffect(() => {
        ObtenerPerfilUsuario()
    }, [])

    const ObtenerPerfilUsuario = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioPerfilUsuario.ObtenerPerfilUsuario(generalUsuarioLogueado.CodigoUsuario,
            (respuesta) => {
                dispatch(GeneralAction.CerrarBackdrop())
                SetUsuario(respuesta)
                SetUsuarioAnterior(respuesta)
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
    }

    const CambiarValorCampo = (e) => {
        const { name, value } = e.target
        SetUsuario({ ...usuario, [name]: value })
    }

    const GuardarCambios = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioPerfilUsuario.ModificarPerfilUsuario({
            Usuario: usuario,
            UsuarioAnterior: usuarioAnterior
        }, () => {
            dispatch(GeneralAction.CerrarBackdrop())
            AlertaSwal.MensajeExitoPorDefecto(() => {
                if (usuario.Contrasena === null || usuario.Contrasena === ''){
                    ObtenerPerfilUsuario()
                }
                else{
                    CerrarSesionUsuario(Constante.CODIGO_EXCEPCION_ADVERTENCIA_SIMPLE_LOGOUT_USUARIO)
                }
            })
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
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
                            component="h2"
                            className={ claseEstilo.centrar }>
                            <b>Mi Perfil</b>
                        </Typography>
                        <br/>
                        <Grid 
                            container 
                            spacing={ 2 } 
                            alignItems="flex-end">
                            <Grid 
                                item xs={ 12 }>
                                <FormControl 
                                    className = { claseEstilo.campo1 }>
                                    <InputLabel 
                                        htmlFor="usuario">
                                        Usuario
                                    </InputLabel>
                                    <Input
                                        id="usuario"
                                        type='text'
                                        readOnly
                                        value={ usuario.Usuario }
                                    />
                                </FormControl>
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
                                    className = { claseEstilo.campo2 }>
                                    <InputLabel 
                                        htmlFor="nombreApellido">
                                        Nombres y apellidos
                                    </InputLabel>
                                    <Input
                                        id="nombreApellido"
                                        type='text'
                                        readOnly
                                        value={ usuario.Nombre + ' ' + usuario.Apellido }
                                    />
                                </FormControl>
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
                                    className = { claseEstilo.campo3 }>
                                    <TextField 
                                        error = { errors.CorreoElectronico } 
                                        name='CorreoElectronico'
                                        label='Correo electrónico'
                                        type='text'
                                        value={ usuario.CorreoElectronico }
                                        onChange={ (e) => CambiarValorCampo(e) }
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
                        <Grid 
                            container 
                            spacing={ 2 } 
                            alignItems="flex-end">
                            <Grid 
                                item xs={ 12 }>
                                <FormControl 
                                    className = { claseEstilo.campo3 }>
                                    <TextField 
                                        error = { errors.Contrasena } 
                                        name='Contrasena'
                                        label='Contraseña'
                                        type={ contrasenaVisible ? 'text' : 'password' }
                                        value={ usuario.Contrasena }
                                        onChange={ (e) => CambiarValorCampo(e) }
                                        inputRef={ register }
                                        InputProps={{
                                            endAdornment: (
                                                <InputAdornment 
                                                    position="end">
                                                    <IconButton
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
                                            item xs={ 12 }>
                                            <p className={ claseEstilo.colorError }>
                                                { errors.Contrasena.message }
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
                                    className = { claseEstilo.campo3 }>
                                    <TextField 
                                        error = { errors.ConfirmarContrasena } 
                                        name='ConfirmarContrasena'
                                        label='Confirmar contraseña'
                                        type={ confirmarContrasenaVisible ? 'text' : 'password' }
                                        value={ usuario.ConfirmarContrasena }
                                        onChange={ (e) => CambiarValorCampo(e) }
                                        inputRef={ register }
                                        InputProps={{
                                            endAdornment: (
                                                <InputAdornment 
                                                    position="end">
                                                    <IconButton
                                                        onClick = { () => SetConfirmarContrasenaVisible(!confirmarContrasenaVisible) }>
                                                        { confirmarContrasenaVisible ? 
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
                            errors.ConfirmarContrasena
                                ?   <Grid 
                                        container
                                        alignItems='flex-end'>
                                        <Grid 
                                            item xs={ 12 }>
                                            <p className={ claseEstilo.colorError }>
                                                { errors.ConfirmarContrasena.message }
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
                                    className = { claseEstilo.campo4 }>
                                    <InputLabel 
                                        htmlFor="rolUsuario">
                                        Rol del usuario
                                    </InputLabel>
                                    <Input
                                        id="rolUsuario"
                                        type='text'
                                        readOnly
                                        value={ usuario.RolUsuario }
                                    />
                                </FormControl>
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
                                    className = { claseEstilo.campo3 }>
                                    <InputLabel 
                                        htmlFor="accesosUsuario">
                                        Últimos accesos
                                    </InputLabel>
                                    <Input
                                        id="accesosUsuario"
                                        type='text'
                                        rows={ 5 }
                                        multiline
                                        readOnly
                                        value={ usuario.UltimosAccesos.join('\n') }
                                    />
                                </FormControl>
                            </Grid>
                        </Grid>
                        <br/><br/>
                        <Grid 
                            container 
                            spacing={ 2 } 
                            alignItems="flex-end"
                            className={ claseEstilo.centrar }>
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