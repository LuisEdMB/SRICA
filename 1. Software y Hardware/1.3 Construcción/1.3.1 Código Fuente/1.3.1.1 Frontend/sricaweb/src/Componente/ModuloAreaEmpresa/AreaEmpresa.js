import React, { useState, useEffect } from 'react'
import { useDispatch } from 'react-redux'
import * as GeneralAction from '../../Accion/General'
import * as AreaEmpresaAction from '../../Accion/AreaEmpresa'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioAreaEmpresa from '../../Servicio/AreaEmpresa'

import * as Utilitario from '../../Utilitario'

import { BotonRegistrar } from '../ComponenteGeneral/BotonRegistrar'
import { BotonInactivar } from '../ComponenteGeneral/BotonInactivar'
import { BotonActivar } from '../ComponenteGeneral/BotonActivar'
import { BotonModificar } from '../ComponenteGeneral/BotonModificar'
import { Tabla } from '../ComponenteGeneral/Tabla'
import { FormularioAreaEmpresa } from './FormularioAreaEmpresa'

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
        width: 500
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

export const AreaEmpresa = () => {
    const claseEstilo = estilos()
    const [botonInactivarAreaVisible, SetBotonInactivarAreaVisible] = useState(true)
    const [activo, SetActivo] = useState(true)
    const [listadoArea, SetListadoArea] = useState([])
    const [listadoAreaEstado, SetListadoAreaEstado] = useState([])
    const [listadoSede, SetListadoSede] = useState([])
    var areasSeleccionadas = []
    const dispatch = useDispatch()
    const columnasTabla = [
        {name: 'DescripcionArea', label: 'Área', options: { filterType: 'textField' }},
        {name: 'Sede', label: 'Sede', options: { filterType: 'multiselect', filterOptions: {
            names: listadoSede.map((sede) => Utilitario.SinAsignacionATexto(sede.DescripcionSede))
        } } },
        {name: '', label: '', options: { sort: false, filter: false }}
    ]

    useEffect(() => {
        ObtenerListadoAreaEmpresa()
    }, [])

    const ObtenerListadoAreaEmpresa = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioAreaEmpresa.ObtenerListadoAreaEmpresa((respuesta) => {
            respuesta = respuesta.filter((area) => 
                !area.IndicadorRegistroParaSinAsignacion)
            SetListadoArea(respuesta)
            CambiarEstadoListado(activo, respuesta)
            ObtenerListadoSedeEmpresa(() => dispatch(GeneralAction.CerrarBackdrop()))
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const CambiarEstadoListado = (estado, areas = null) => {
        SetActivo(estado)
        var resultado = CrearDatosFilaTablaAreaEmpresa(
            areas === null 
                ? listadoArea
                : areas, estado)
        SetListadoAreaEstado(resultado)
        SetBotonInactivarAreaVisible(estado)
    }

    const CrearDatosFilaTablaAreaEmpresa = (areas, estado) => {
        var resultado = []
        areas.map((area) => {
            if (area.IndicadorEstado === estado)
                resultado.push({
                    CodigoArea: area.CodigoArea,
                    DescripcionArea: area.DescripcionArea,
                    Sede: Utilitario.SinAsignacionATexto(area.DescripcionSede),
                    '': <BotonModificar 
                        key={ area.CodigoArea } 
                        texto='Modificar Área'
                        onClick={ () => AbrirFormularioAreaEmpresaExistente(area.CodigoArea) }/>
                })
        })
        return resultado
    }

    const ObtenerListadoSedeEmpresa = (callback) => {
        ServicioAreaEmpresa.ObtenerListadoSedeEmpresa((respuesta) => {
            respuesta = respuesta.filter((sede) => sede.IndicadorEstado)
            SetListadoSede(respuesta)
            respuesta = respuesta.filter((sede) => !sede.IndicadorRegistroParaSinAsignacion)
            dispatch(AreaEmpresaAction.SetListadoSedeEmpresa(respuesta))
            callback()
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const AbrirFormularioAreaEmpresaNuevo = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ObtenerListadoSedeEmpresa(() => {
            dispatch(AreaEmpresaAction.SetFormularioAreaEmpresaVacio())
            dispatch(AreaEmpresaAction.SetFormularioAreaEmpresaAnterior({}))
            dispatch(AreaEmpresaAction.AbrirFormularioAreaEmpresa())
            dispatch(GeneralAction.CerrarBackdrop())
        })
    }

    const AbrirFormularioAreaEmpresaExistente = (codigoArea) => {
        dispatch(GeneralAction.AbrirBackdrop())
        ObtenerListadoSedeEmpresa(() => {
            ServicioAreaEmpresa.ObtenerAreaEmpresa(codigoArea, (respuesta) => {
                dispatch(AreaEmpresaAction.SetFormularioAreaEmpresa(respuesta))
                dispatch(AreaEmpresaAction.SetFormularioAreaEmpresaAnterior(respuesta))
                dispatch(AreaEmpresaAction.AbrirFormularioAreaEmpresa())
                dispatch(GeneralAction.CerrarBackdrop())
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        })
    }

    const ObtenerAreasEmpresaSeleccionados = (filaSeleccionada, todasLasFilas) => {
        var seleccionados = []
        todasLasFilas.map((seleccionado) => {
            seleccionados.push({
                CodigoArea: listadoAreaEstado[seleccionado.dataIndex].CodigoArea,
                DescripcionArea: listadoAreaEstado[seleccionado.dataIndex].DescripcionArea
            })
        })
        areasSeleccionadas = seleccionados
    }

    const InhabilitarAreasEmpresa = () => {
        AlertaSwal.MensajeConfirmacion({
            texto: '<p>' + 
                        '¿Está seguro de inactivar las áreas: ' + areasSeleccionadas.length + 
                        ' área(s) seleccionada(s)?' + 
                    '</p>' + 
                    '<p style="color: red">' + 
                        'Advertencia: Las relaciones realizadas entre las áreas con los equipos ' + 
                        'biométricos y personal de la empresa, se inactivarán. Así mismo, se ' + 
                        'removerán los accesos del personal asociados a las áreas respectivas.' +
                    '</p>',
            textoBoton: 'Sí, inactivar',
            evento: () => {
                dispatch(GeneralAction.AbrirBackdrop())
                ServicioAreaEmpresa.InhabilitarAreasEmpresa(areasSeleccionadas, () => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    AlertaSwal.MensajeExito({
                        texto: 'Se han inactivado correctamente las áreas seleccionadas.',
                        evento: () => {
                            ObtenerListadoAreaEmpresa()
                        }
                    })
                }, (codigoExcepcion) => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    CerrarSesionUsuario(codigoExcepcion)
                })
            }
        })
    }

    const HabilitarAreasEmpresa = () => {
        AlertaSwal.MensajeConfirmacion({
            texto: '¿Está seguro de activar las áreas: ' + areasSeleccionadas.length + 
                ' área(s) seleccionada(s)?',
            textoBoton: 'Sí, activar',
            evento: () => {
                dispatch(GeneralAction.AbrirBackdrop())
                ServicioAreaEmpresa.HabilitarAreasEmpresa(areasSeleccionadas, () => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    AlertaSwal.MensajeExito({
                        texto: 'Se han activado correctamente las áreas seleccionadas.',
                        evento: () => {
                            ObtenerListadoAreaEmpresa()
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
            xs={ 12 }
            className={ claseEstilo.principal }>
            <Card className= { claseEstilo.tarjeta }>
                <CardContent>
                    <Typography 
                        variant="h5" 
                        component="h2"
                        className={ claseEstilo.centrar }>
                        <b>Áreas de la Empresa</b>
                    </Typography>
                    <br/>
                    <br/>
                    <Grid
                        container
                        className={ claseEstilo.contenido }>
                        <Grid 
                            item 
                            xs={ 12 }>
                            <BotonRegistrar 
                                texto='Registrar Área' 
                                textoVisible={ true }
                                onClick={ AbrirFormularioAreaEmpresaNuevo }/>
                        </Grid>
                        <br/>
                        <Grid
                            item 
                            xs={ 12 }>
                            <FormControl 
                                className={ claseEstilo.selector }>
                                <InputLabel id="selectorEstadoArea">Estado</InputLabel>
                                <Select
                                    labelId="selectorEstadoArea"
                                    defaultValue={ activo }
                                    onChange={ (e) => CambiarEstadoListado(e.target.value) }>
                                    <MenuItem value={ true }>Activo</MenuItem>
                                    <MenuItem value={ false }>Inactivo</MenuItem>
                                </Select>
                            </FormControl>
                        </Grid>
                        <Grid
                            item 
                            xs={ 12 } 
                            className={ claseEstilo.componenteAbajoDerecha }>
                            { 
                                botonInactivarAreaVisible 
                                    ?   <BotonInactivar 
                                            texto='Inactivar Área(s)'
                                            onClick={ InhabilitarAreasEmpresa }/>
                                    :   <BotonActivar 
                                            texto='Activar Área(s)'
                                            onClick={ HabilitarAreasEmpresa }/>
                            }
                        </Grid>
                        <Grid
                            item
                            xs={ 12 }
                            className={ claseEstilo.tabla }>
                            <Tabla 
                                columnas={ columnasTabla } 
                                datos={ listadoAreaEstado }
                                seleccionFila={ true }
                                onRowsSelect={ (rowsSelected, allRows) => 
                                    ObtenerAreasEmpresaSeleccionados(rowsSelected, allRows) }/>
                        </Grid>
                    </Grid>
                </CardContent>
                <FormularioAreaEmpresa RecargarListadoAreaEmpresa={ ObtenerListadoAreaEmpresa }/>
            </Card>
        </Grid>
    )
}