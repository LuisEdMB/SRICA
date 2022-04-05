import React, { useState, useEffect } from 'react'
import { useDispatch } from 'react-redux'
import * as GeneralAction from '../../Accion/General'
import * as NomenclaturaAction from '../../Accion/Nomenclatura'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioNomenclatura from '../../Servicio/Nomenclatura'

import { BotonRegistrar } from '../ComponenteGeneral/BotonRegistrar'
import { BotonInactivar } from '../ComponenteGeneral/BotonInactivar'
import { BotonActivar } from '../ComponenteGeneral/BotonActivar'
import { BotonModificar } from '../ComponenteGeneral/BotonModificar'
import { Tabla } from '../ComponenteGeneral/Tabla'
import { FormularioNomenclatura } from './FormularioNomenclatura'

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
        marginBottom: 5,
        marginTop: 5
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

export const Nomenclatura = () => {
    const claseEstilo = estilos()
    const [botonInactivarNomenclaturaVisible, SetBotonInactivarNomenclaturaVisible] = useState(true)
    const [activo, SetActivo] = useState(true)
    const [listadoNomenclatura, SetListadoNomenclatura] = useState([])
    const [listadoNomenclaturaEstado, SetListadoNomenclaturaEstado] = useState([])
    var nomenclaturasSeleccionadas = []
    const dispatch = useDispatch()
    const columnasTabla = [
        {name: 'DescripcionNomenclatura', label: 'Nomenclatura', options: { filterType: 'textField' }},
        {name: '', label: '', options: { sort: false, filter: false }}
    ]

    useEffect(() => {
        ObtenerListadoNomenclatura()
    }, [])

    const ObtenerListadoNomenclatura = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioNomenclatura.ObtenerListadoNomenclatura((respuesta) => {
            respuesta = respuesta.filter((nomenclatura) => 
                !nomenclatura.IndicadorRegistroParaSinAsignacion)
            SetListadoNomenclatura(respuesta)
            CambiarEstadoListado(activo, respuesta)
            dispatch(GeneralAction.CerrarBackdrop())
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const CambiarEstadoListado = (estado, nomenclaturas = null) => {
        SetActivo(estado)
        var resultado = CrearDatosFilaTablaNomenclatura(
            nomenclaturas === null 
                ? listadoNomenclatura
                : nomenclaturas, estado)
        SetListadoNomenclaturaEstado(resultado)
        SetBotonInactivarNomenclaturaVisible(estado)
    }

    const CrearDatosFilaTablaNomenclatura = (nomenclaturas, estado) => {
        var resultado = []
        nomenclaturas.map((nomenclatura) => {
            if (nomenclatura.IndicadorEstado === estado)
                resultado.push({
                    CodigoNomenclatura: nomenclatura.CodigoNomenclatura,
                    DescripcionNomenclatura: nomenclatura.DescripcionNomenclatura,
                    '': <BotonModificar 
                        key={ nomenclatura.CodigoNomenclatura } 
                        texto='Modificar Nomenclatura'
                        onClick={ () => AbrirFormularioNomenclaturaExistente(
                            nomenclatura.CodigoNomenclatura) }/>
                })
        })
        return resultado
    }

    const AbrirFormularioNomenclaturaNuevo = () => {
        dispatch(NomenclaturaAction.SetFormularioNomenclaturaVacio())
        dispatch(NomenclaturaAction.SetFormularioNomenclaturaAnterior({}))
        dispatch(NomenclaturaAction.AbrirFormularioNomenclatura())
    }

    const AbrirFormularioNomenclaturaExistente = (codigoNomenclatura) => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioNomenclatura.ObtenerNomenclatura(codigoNomenclatura, (respuesta) => {
            dispatch(NomenclaturaAction.SetFormularioNomenclatura(respuesta))
            dispatch(NomenclaturaAction.SetFormularioNomenclaturaAnterior(respuesta))
            dispatch(NomenclaturaAction.AbrirFormularioNomenclatura())
            dispatch(GeneralAction.CerrarBackdrop())
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const ObtenerNomenclaturasSeleccionados = (filaSeleccionada, todasLasFilas) => {
        var seleccionados = []
        todasLasFilas.map((seleccionado) => {
            seleccionados.push({
                CodigoNomenclatura: listadoNomenclaturaEstado[seleccionado.dataIndex].CodigoNomenclatura,
                DescripcionNomenclatura: 
                    listadoNomenclaturaEstado[seleccionado.dataIndex].DescripcionNomenclatura
            })
        })
        nomenclaturasSeleccionadas = seleccionados
    }

    const InhabilitarNomenclaturas = () => {
        AlertaSwal.MensajeConfirmacion({
            texto: '<p>' + 
                        '¿Está seguro de inactivar las nomenclaturas: ' + nomenclaturasSeleccionadas.length +
                        ' nomenclatura(s) seleccionada(s)?' + 
                    '</p>' + 
                    '<p style="color: red">' + 
                        'Advertencia: Las relaciones realizadas entre las nomenclaturas con los equipos ' + 
                        'biométricos, se inactivarán.' +
                    '</p>',
            textoBoton: 'Sí, inactivar',
            evento: () => {
                dispatch(GeneralAction.AbrirBackdrop())
                ServicioNomenclatura.InhabilitarNomenclaturas(nomenclaturasSeleccionadas, () => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    AlertaSwal.MensajeExito({
                        texto: 'Se han inactivado correctamente las nomenclaturas seleccionadas.',
                        evento: () => {
                            ObtenerListadoNomenclatura()
                        }
                    })
                }, (codigoExcepcion) => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    CerrarSesionUsuario(codigoExcepcion)
                })
            }
        })
    }

    const HabilitarNomenclaturas = () => {
        AlertaSwal.MensajeConfirmacion({
            texto: '¿Está seguro de activar las nomenclaturas: ' + nomenclaturasSeleccionadas.length + 
                ' nomenclatura(s) seleccionada(s)?',
            textoBoton: 'Sí, activar',
            evento: () => {
                dispatch(GeneralAction.AbrirBackdrop())
                ServicioNomenclatura.HabilitarNomenclaturas(nomenclaturasSeleccionadas, () => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    AlertaSwal.MensajeExito({
                        texto: 'Se han activado correctamente las nomenclaturas seleccionadas.',
                        evento: () => {
                            ObtenerListadoNomenclatura()
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
                        <b>Nomenclaturas de Equipos Biométricos</b>
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
                                texto='Registrar Nomenclatura' 
                                textoVisible={ true }
                                onClick={ AbrirFormularioNomenclaturaNuevo }/>
                        </Grid>
                        <br/>
                        <Grid
                            item 
                            xs={ 12 }>
                            <FormControl 
                                className={ claseEstilo.selector }>
                                <InputLabel id="selectorEstadoNomenclatura">Estado</InputLabel>
                                <Select
                                    labelId="selectorEstadoNomenclatura"
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
                                botonInactivarNomenclaturaVisible 
                                    ?   <BotonInactivar 
                                            texto='Inactivar Nomenclatura(s)'
                                            onClick={ InhabilitarNomenclaturas }/> 
                                    :   <BotonActivar 
                                            texto='Activar Nomenclatura(s)'
                                            onClick={ HabilitarNomenclaturas }/>
                            }
                        </Grid>
                        <Grid
                            item
                            xs={ 12 }
                            className={ claseEstilo.tabla }>
                            <Tabla 
                                columnas={ columnasTabla } 
                                datos={ listadoNomenclaturaEstado }
                                seleccionFila={ true }
                                onRowsSelect={ (rowsSelected, allRows) => 
                                    ObtenerNomenclaturasSeleccionados(rowsSelected, allRows) }/>
                        </Grid>
                    </Grid>
                </CardContent>
                <FormularioNomenclatura RecargarListadoNomenclatura={ ObtenerListadoNomenclatura }/>
            </Card>
        </Grid>
    )
}