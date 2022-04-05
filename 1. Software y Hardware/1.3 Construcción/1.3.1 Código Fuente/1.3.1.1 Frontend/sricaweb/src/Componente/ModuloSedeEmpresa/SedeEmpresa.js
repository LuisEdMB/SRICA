import React, { useState, useEffect } from 'react'
import { useDispatch } from 'react-redux'
import * as GeneralAction from '../../Accion/General'
import * as SedeEmpresaAction from '../../Accion/SedeEmpresa'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioSedeEmpresa from '../../Servicio/SedeEmpresa'

import { BotonRegistrar } from '../ComponenteGeneral/BotonRegistrar'
import { BotonInactivar } from '../ComponenteGeneral/BotonInactivar'
import { BotonActivar } from '../ComponenteGeneral/BotonActivar'
import { BotonModificar } from '../ComponenteGeneral/BotonModificar'
import { Tabla } from '../ComponenteGeneral/Tabla'
import { FormularioSedeEmpresa } from './FormularioSedeEmpresa'

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
        width: 450
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

export const SedeEmpresa = () => {
    const claseEstilo = estilos()
    const [botonInactivarSedeVisible, SetBotonInactivarSedeVisible] = useState(true)
    const [activo, SetActivo] = useState(true)
    const [listadoSede, SetListadoSede] = useState([])
    const [listadoSedeEstado, SetListadoSedeEstado] = useState([])
    var sedesSeleccionadas = []
    const dispatch = useDispatch()
    const columnasTabla = [
        {name: 'DescripcionSede', label: 'Sede', options: { filterType: 'textField' }},
        {name: '', label: '', options: { sort: false, filter: false }}
    ]

    useEffect(() => {
        ObtenerListadoSedeEmpresa()
    }, [])

    const ObtenerListadoSedeEmpresa = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioSedeEmpresa.ObtenerListadoSedeEmpresa((respuesta) => {
            respuesta = respuesta.filter((sede) => 
                !sede.IndicadorRegistroParaSinAsignacion)
            SetListadoSede(respuesta)
            CambiarEstadoListado(activo, respuesta)
            dispatch(GeneralAction.CerrarBackdrop())
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const CambiarEstadoListado = (estado, sedes = null) => {
        SetActivo(estado)
        var resultado = CrearDatosFilaTablaSedeEmpresa(
            sedes === null 
                ? listadoSede
                : sedes, estado)
        SetListadoSedeEstado(resultado)
        SetBotonInactivarSedeVisible(estado)
    }

    const CrearDatosFilaTablaSedeEmpresa = (sedes, estado) => {
        var resultado = []
        sedes.map((sede) => {
            if (sede.IndicadorEstado === estado)
                resultado.push({
                    CodigoSede: sede.CodigoSede,
                    DescripcionSede: sede.DescripcionSede,
                    '': <BotonModificar 
                        key={ sede.CodigoSede } 
                        texto='Modificar Sede'
                        onClick={ () => AbrirFormularioSedeEmpresaExistente(sede.CodigoSede) }/>
                })
        })
        return resultado
    }

    const AbrirFormularioSedeEmpresaNuevo = () => {
        dispatch(SedeEmpresaAction.SetFormularioSedeEmpresaVacio())
        dispatch(SedeEmpresaAction.SetFormularioSedeEmpresaAnterior({}))
        dispatch(SedeEmpresaAction.AbrirFormularioSedeEmpresa())
    }

    const AbrirFormularioSedeEmpresaExistente = (codigoSede) => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioSedeEmpresa.ObtenerSedeEmpresa(codigoSede, (respuesta) => {
            dispatch(SedeEmpresaAction.SetFormularioSedeEmpresa(respuesta))
            dispatch(SedeEmpresaAction.SetFormularioSedeEmpresaAnterior(respuesta))
            dispatch(SedeEmpresaAction.AbrirFormularioSedeEmpresa())
            dispatch(GeneralAction.CerrarBackdrop())
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const ObtenerSedesEmpresaSeleccionados = (filaSeleccionada, todasLasFilas) => {
        var seleccionados = []
        todasLasFilas.map((seleccionado) => {
            seleccionados.push({
                CodigoSede: listadoSedeEstado[seleccionado.dataIndex].CodigoSede,
                DescripcionSede: listadoSedeEstado[seleccionado.dataIndex].DescripcionSede
            })
        })
        sedesSeleccionadas = seleccionados
    }

    const InhabilitarSedesEmpresa = () => {
        AlertaSwal.MensajeConfirmacion({
            texto: '<p>' + 
                        '¿Está seguro de inactivar las sedes: ' + sedesSeleccionadas.length + 
                        ' sede(s) seleccionada(s)?' + 
                    '</p>' + 
                    '<p style="color: red">' + 
                        'Advertencia: Las relaciones realizadas entre las sedes con las ' + 
                        'áreas, equipos biométricos y personal de la empresa, se inactivarán. ' + 
                        'Así mismo, se removerán los accesos del personal asociados a las sedes ' +
                        'respectivas.' +
                    '</p>',
            textoBoton: 'Sí, inactivar',
            evento: () => {
                dispatch(GeneralAction.AbrirBackdrop())
                ServicioSedeEmpresa.InhabilitarSedesEmpresa(sedesSeleccionadas, () => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    AlertaSwal.MensajeExito({
                        texto: 'Se han inactivado correctamente las sedes seleccionadas.',
                        evento: () => {
                            ObtenerListadoSedeEmpresa()
                        }
                    })
                }, (codigoExcepcion) => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    CerrarSesionUsuario(codigoExcepcion)
                })
            }
        })
    }

    const HabilitarSedesEmpresa = () => {
        AlertaSwal.MensajeConfirmacion({
            texto: '¿Está seguro de activar las sedes: ' + sedesSeleccionadas.length + 
                ' sede(s) seleccionada(s)?',
            textoBoton: 'Sí, activar',
            evento: () => {
                dispatch(GeneralAction.AbrirBackdrop())
                ServicioSedeEmpresa.HabilitarSedesEmpresa(sedesSeleccionadas, () => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    AlertaSwal.MensajeExito({
                        texto: 'Se han activado correctamente las sedes seleccionadas.',
                        evento: () => {
                            ObtenerListadoSedeEmpresa()
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
                        <b>Sedes de la Empresa</b>
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
                                texto='Registrar Sede' 
                                textoVisible={ true }
                                onClick={ AbrirFormularioSedeEmpresaNuevo }/>
                        </Grid>
                        <br/>
                        <Grid
                            item={ true }  
                            xs={ 12 }>
                            <FormControl 
                                className={ claseEstilo.selector }>
                                <InputLabel id="selectorEstadoSede">Estado</InputLabel>
                                <Select
                                    labelId="selectorEstadoSede"
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
                                botonInactivarSedeVisible 
                                    ?   <BotonInactivar 
                                            texto='Inactivar Sede(s)'
                                            onClick={ InhabilitarSedesEmpresa }/> 
                                    :   <BotonActivar 
                                            texto='Activar Sede(s)'
                                            onClick={ HabilitarSedesEmpresa }/>
                            }
                        </Grid>
                        <Grid
                            item={ true } 
                            xs={ 12 }
                            className={ claseEstilo.tabla }>
                            <Tabla 
                                columnas={ columnasTabla } 
                                datos={ listadoSedeEstado }
                                seleccionFila={ true }
                                onRowsSelect={ (rowsSelected, allRows) => 
                                    ObtenerSedesEmpresaSeleccionados(rowsSelected, allRows) }/>
                        </Grid>
                    </Grid>
                </CardContent>
                <FormularioSedeEmpresa RecargarListadoSedeEmpresa={ ObtenerListadoSedeEmpresa }/>
            </Card>
        </Grid>
    )
}