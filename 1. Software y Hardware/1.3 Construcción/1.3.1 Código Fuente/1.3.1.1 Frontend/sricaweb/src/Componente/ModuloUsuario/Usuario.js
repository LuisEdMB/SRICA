import React, { useState, useEffect } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import * as GeneralAction from '../../Accion/General'
import * as UsuarioAction from '../../Accion/Usuario'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioUsuario from '../../Servicio/Usuario'

import { BotonRegistrar } from '../ComponenteGeneral/BotonRegistrar'
import { BotonInactivar } from '../ComponenteGeneral/BotonInactivar'
import { BotonActivar } from '../ComponenteGeneral/BotonActivar'
import { BotonModificar } from '../ComponenteGeneral/BotonModificar'
import { Tabla } from '../ComponenteGeneral/Tabla'
import { FormularioUsuario }  from './FormularioUsuario'

import { makeStyles, Grid, Card, CardContent, Typography, FormControl, InputLabel, Select, MenuItem } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        marginTop: 85,
        marginBottom: 22,
        minHeight: '100%',
        justifyContent: 'center'
    },
    tarjeta: {
        marginLeft: 17,
        marginRight: 17,
        color: '#48525e',
        width: 700
    },
    contenido: {
        flexGrow: 1
    },
    centrar: {
        fontWeight: 'bold',
        textAlign: 'center',
        justifyContent: 'center'
    },
    componenteAbajoDerecha: {
        textAlign: 'right',
        alignSelf: 'flex-end',
        marginBottom: 5
    },
    selector: {
        marginTop: 30,
        marginBottom: 5,
        minWidth: 120
    },
    tabla: {
        marginTop: 10
    }
})

export const Usuario = () => {
    const claseEstilo = estilos()
    const [botonInactivarUsuarioVisible, SetBotonInactivarUsuarioVisible] = useState(true)
    const [activo, SetActivo] = useState(true)
    const [listadoUsuario, SetListadoUsuario] = useState([])
    const [listadoUsuarioEstado, SetListadoUsuarioEstado] = useState([])
    var usuariosSeleccionados = []
    const usuario = useSelector((store) => store.Usuario)
    const dispatch = useDispatch()
    const columnasTabla = [
        {name: 'Usuario', label: 'Usuario', options: { filterType: 'textField' }},
        {name: 'Nombres', label: 'Nombres', options: { filterType: 'textField' }},
        {name: 'Apellidos', label: 'Apellidos', options: { filterType: 'textField' }},
        {name: 'Rol', label: 'Rol', options: { filterType: 'multiselect', filterOptions: {
            names: usuario.Roles.map((rol) => rol.DescripcionRolUsuario)
        } } },
        {name: '', label: '', options: { sort: false, filter: false }}
    ]

    useEffect(() => {
        ObtenerListadoUsuario()
    }, [])

    const ObtenerListadoUsuario = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioUsuario.ObtenerListadoUsuario((respuesta) => {
            SetListadoUsuario(respuesta)
            CambiarEstadoListado(activo, respuesta)
            ObtenerListadoRolUsuario(() => dispatch(GeneralAction.CerrarBackdrop()))
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const CambiarEstadoListado = (estado, usuarios = null) => {
        SetActivo(estado)
        var resultado = CrearDatosFilaTablaUsuario(
            usuarios === null 
                ? listadoUsuario
                : usuarios, estado)
        SetListadoUsuarioEstado(resultado)
        SetBotonInactivarUsuarioVisible(estado)
    }

    const CrearDatosFilaTablaUsuario = (usuarios, estado) => {
        var resultado = []
        usuarios.map((usuario) => {
            if (usuario.IndicadorEstado === estado)
                resultado.push({
                    CodigoUsuario: usuario.CodigoUsuario,
                    Usuario: usuario.Usuario,
                    Nombres: usuario.Nombre,
                    Apellidos: usuario.Apellido,
                    Rol: usuario.RolUsuario,
                    '': <BotonModificar 
                        key={ usuario.CodigoUsuario } 
                        texto='Modificar Usuario'
                        onClick={ () => AbrirFormularioUsuarioExistente(usuario.CodigoUsuario) }/>
                })
        })
        return resultado
    }

    const ObtenerListadoRolUsuario = (callback) => {
        ServicioUsuario.ObtenerListadoRolUsuario((respuesta) => {
            var rolesUsuario = respuesta.filter((rolUsuario) => rolUsuario.IndicadorEstado)
            dispatch(UsuarioAction.SetListadoRolUsuario(rolesUsuario))
            callback()
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const AbrirFormularioUsuarioNuevo = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ObtenerListadoRolUsuario(() => {
            dispatch(UsuarioAction.SetFormularioUsuarioVacio())
            dispatch(UsuarioAction.SetFormularioUsuarioAnterior({}))
            dispatch(UsuarioAction.AbrirFormularioUsuario())
            dispatch(GeneralAction.CerrarBackdrop())
        })
    }

    const AbrirFormularioUsuarioExistente = (codigoUsuario) => {
        dispatch(GeneralAction.AbrirBackdrop())
        ObtenerListadoRolUsuario(() => {
            ServicioUsuario.ObtenerUsuario(codigoUsuario, (respuesta) => {
                respuesta.EsContrasenaPorDefecto = false
                dispatch(UsuarioAction.SetFormularioUsuario(respuesta))
                dispatch(UsuarioAction.SetFormularioUsuarioAnterior(respuesta))
                dispatch(UsuarioAction.AbrirFormularioUsuario())
                dispatch(GeneralAction.CerrarBackdrop())
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        })
    }

    const ObtenerUsuariosSeleccionados = (filaSeleccionada, todasLasFilas) => {
        console.log(filaSeleccionada)
        console.log(todasLasFilas)
        var seleccionados = []
        todasLasFilas.map((seleccionado) => {
            console.log(seleccionado)
            seleccionados.push({
                 CodigoUsuario: listadoUsuarioEstado[seleccionado.dataIndex].CodigoUsuario,
                 Usuario: listadoUsuarioEstado[seleccionado.dataIndex].Usuario
            })
        })
        usuariosSeleccionados = seleccionados
    }

    const InhabilitarUsuarios = () => {
        AlertaSwal.MensajeConfirmacion({
            texto: '¿Está seguro de inactivar los usuarios: ' + usuariosSeleccionados.length + 
                ' usuario(s) seleccionado(s)?',
            textoBoton: 'Sí, inactivar',
            evento: () => {
                dispatch(GeneralAction.AbrirBackdrop())
                ServicioUsuario.InhabilitarUsuarios(usuariosSeleccionados, () => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    AlertaSwal.MensajeExito({
                        texto: 'Se han inactivado correctamente los usuarios seleccionados.',
                        evento: () => {
                            ObtenerListadoUsuario()
                        }
                    })
                }, (codigoExcepcion) => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    CerrarSesionUsuario(codigoExcepcion)
                })
            }
        })
    }

    const HabilitarUsuarios = () => {
        AlertaSwal.MensajeConfirmacion({
            texto: '¿Está seguro de activar los usuarios: ' + usuariosSeleccionados.length + 
                ' usuario(s) seleccionado(s)?',
            textoBoton: 'Sí, activar',
            evento: () => {
                dispatch(GeneralAction.AbrirBackdrop())
                ServicioUsuario.HabilitarUsuarios(usuariosSeleccionados, () => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    AlertaSwal.MensajeExito({
                        texto: 'Se han activado correctamente los usuarios seleccionados.',
                        evento: () => {
                            ObtenerListadoUsuario()
                        }
                    })
                }, (codigoExcepcion) => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    CerrarSesionUsuario(codigoExcepcion)
                })
            }
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
            className={ claseEstilo.principal }>
            <Card className= { claseEstilo.tarjeta }>
                <CardContent>
                    <Typography 
                        variant="h5" 
                        component="h2"
                        className={ claseEstilo.centrar }>
                        <b>Usuarios del Sistema</b>
                    </Typography>
                    <br/>
                    <br/>
                    <Grid
                        container
                        className={ claseEstilo.contenido }>
                        <Grid 
                            item={ true }
                            xs={ 12 }>
                            <BotonRegistrar 
                                texto='Registrar Usuario' 
                                textoVisible={ true }
                                onClick={ AbrirFormularioUsuarioNuevo }/>
                        </Grid>
                        <br/>
                        <Grid
                            item={ true }
                            xs={ 12 }>
                            <FormControl 
                                className={ claseEstilo.selector }>
                                <InputLabel id="selectorEstadoUsuario">Estado</InputLabel>
                                <Select
                                    labelId="selectorEstadoUsuario"
                                    defaultValue={ activo }
                                    onChange={ (e) => CambiarEstadoListado(e.target.value) }>
                                    <MenuItem value={ true }>Activo</MenuItem>
                                    <MenuItem value={ false }>Inactivo</MenuItem>
                                </Select>
                            </FormControl>
                        </Grid>
                        <Grid
                            item={ true }
                            xs={ 12 }
                            className={ claseEstilo.componenteAbajoDerecha }>
                            { 
                                botonInactivarUsuarioVisible 
                                    ?   <BotonInactivar 
                                            texto='Inactivar Usuario(s)'
                                            onClick={ InhabilitarUsuarios }/> 
                                    :   <BotonActivar 
                                            texto='Activar Usuario(s)'
                                            onClick={ HabilitarUsuarios }/>
                            }
                        </Grid>
                        <Grid
                            item={ true }
                            xs={ 12 }
                            className={ claseEstilo.tabla }>
                            <Tabla 
                                columnas={ columnasTabla } 
                                datos={ listadoUsuarioEstado }
                                seleccionFila={ true }
                                onRowsSelect={ (rowsSelected, allRows) => 
                                    ObtenerUsuariosSeleccionados(rowsSelected, allRows) }/>
                        </Grid>
                    </Grid>
                </CardContent>
                <FormularioUsuario RecargarListadoUsuario={ ObtenerListadoUsuario }/>
            </Card>
        </Grid>
    )
}