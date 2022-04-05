import React, { useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import * as GeneralAction from '../../Accion/General'
import * as Constante from '../../Constante'

import * as ServicioDashboard from '../../Servicio/Dashboard'

import * as Utilitario from '../../Utilitario'

import { Card, CardContent, makeStyles, Typography, FormControl, createStyles, InputLabel, Select, MenuItem, Input, Checkbox, ListItemText, Grid, CircularProgress } from '@material-ui/core'
import { BarChart, CartesianGrid, XAxis, YAxis, Tooltip, Legend, Bar } from 'recharts'

const estilos = makeStyles((tema) =>
    createStyles({
        principal: {
            margin: 18,
            color: '#48525e'
        },
        titulo: {
            fontSize: 15,
            fontWeight: 'bold',
            textAlign: 'right'
        },
        subtitulo: {
            fontSize: 12,
            color: '#8e97a6',
            textAlign: 'right'
        },
        selector: {
            margin: tema.spacing(1),
            minWidth: 120
        }
    })
)

export const AccesoSedeAreaDashboard = () => {
    const claseEstilo = estilos()
    const [mostrarProgressBar, SetMostrarProgressBar] = useState(false)
    const [areas, SetAreas] = useState([])
    const [areasSeleccionadasEstado, SetAreasSeleccionadasEstado] = useState([])
    var areasSeleccionadas = []
    const [datosGrafico, SetDatosGrafico] = useState([])
    const dashboard = useSelector((store) => store.Dashboard)
    const dispatch = useDispatch()

    const ObtenerListadoAreaSegunSede = (e) => {
        SetMostrarProgressBar(true)
        SetAreasSeleccionadasEstado([])
        ServicioDashboard.ObtenerListadoAreaSegunSede(e.target.value, (respuesta) => {
            respuesta = respuesta.filter((area) => area.IndicadorEstado)
            SetMostrarProgressBar(false)
            SetAreas(respuesta)
        }, (codigoExcepcion) => {
            SetMostrarProgressBar(false)
            SetAreas([])
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

    const SeleccionarArea = (e) => {
        SetAreasSeleccionadasEstado(e.target.value)
        areasSeleccionadas = e.target.value
        ObtenerListadoBitacoraAccionEquipoBiometricoPorArea();
    }

    const ObtenerListadoBitacoraAccionEquipoBiometricoPorArea = () => {
        SetMostrarProgressBar(true)
        var codigosAreas = areasSeleccionadas.map((area) => area.CodigoArea)
        ServicioDashboard.ObtenerListadoBitacoraAccionEquipoBiometrico((respuesta) => {
            respuesta = Utilitario.FiltrarArrayPorPropiedadSegunOtroArray(respuesta,
                'CodigoArea', codigosAreas)
            SetDatosGrafico([])
            ArmarDatosDelGrafico(respuesta)
        }, (codigoExcepcion) => {
            SetMostrarProgressBar(false)
            SetDatosGrafico([])
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const ArmarDatosDelGrafico = (bitacora) => {
        var data = areasSeleccionadas.map((area) => {
            return {
                DescripcionArea: area.DescripcionArea,
                Concedido: bitacora.filter((item) => item.CodigoArea === area.CodigoArea && 
                    item.CodigoResultadoAcceso === Constante.RESULTADO_ACCESO_CONCEDIDO).length,
                Denegado: bitacora.filter((item) => item.CodigoArea === area.CodigoArea && 
                    item.CodigoResultadoAcceso === Constante.RESULTADO_ACCESO_DENEGADO).length,
                Error: bitacora.filter((item) => item.CodigoArea === area.CodigoArea && 
                    item.CodigoResultadoAcceso === Constante.RESULTADO_ACCESO_ERROR).length,
                Validacion: bitacora.filter((item) => item.CodigoArea === area.CodigoArea && 
                    item.CodigoResultadoAcceso === Constante.RESULTADO_ACCESO_VALIDACION).length
            }
        })
        SetDatosGrafico(data)
        SetMostrarProgressBar(false)
    }

    return(
        <Card className={ claseEstilo.principal }>
            <CardContent>
                <Grid
                    container>
                    <Grid
                        xs={ 2 }>
                        {
                            (dashboard.Sedes === null  || dashboard.Sedes === undefined ) || 
                            mostrarProgressBar
                                ? <CircularProgress />
                                : null
                        }
                    </Grid>
                    <Grid
                        xs={ 10 }>
                        <Typography className={ claseEstilo.titulo }>
                            Accesos por Sede y Áreas
                        </Typography>
                        <Typography className={ claseEstilo.subtitulo }>
                            HOY
                        </Typography>
                    </Grid>
                </Grid>
                <FormControl className={ claseEstilo.selector }>
                    <InputLabel id="selectorSede">Sede</InputLabel>
                    <Select
                        labelId="selectorSede"
                        onChange={ ObtenerListadoAreaSegunSede }>
                        {
                            (dashboard.Sedes === null  || dashboard.Sedes === undefined )
                                ? null
                                : dashboard.Sedes.map((sede) => 
                                    <MenuItem 
                                        key={ sede.CodigoSede } 
                                        value={ sede.CodigoSede }>
                                        { sede.DescripcionSede }
                                    </MenuItem>
                                )
                        }
                    </Select>
                </FormControl>
                <FormControl className={ claseEstilo.selector }>
                    <InputLabel id="selectorArea">Área(s)</InputLabel>
                    <Select
                        labelId="selectorArea"
                        multiple
                        value={ areasSeleccionadasEstado }
                        onChange={ SeleccionarArea }
                        input={ <Input /> }
                        renderValue={ (selected) => {
                            var resultado = []
                            selected.map((e) => {
                                resultado.push(e.DescripcionArea)
                            })
                            return resultado.join('; ')
                        } }>
                        {
                            areas === null
                                ? null
                                : areas.map((area) => 
                                    <MenuItem 
                                        key={ area.CodigoArea } 
                                        value={ area }>
                                        <Checkbox 
                                            color='primary'
                                            checked={ areasSeleccionadasEstado.indexOf(area) > -1 }/>
                                        <ListItemText primary={ area.DescripcionArea } />
                                    </MenuItem>
                                )
                        }
                    </Select>
                </FormControl><br/>
                <div style={{ overflowY: 'scroll', overflowY: 'hidden', width: 'auto', height: 'auto' }}>
                    <BarChart data={ datosGrafico } width={ 800 } height={ 300 }>
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis dataKey="DescripcionArea" />
                        <YAxis />
                        <Tooltip />
                        <Legend />
                        <Bar dataKey="Concedido" fill="#1DA220" />
                        <Bar dataKey="Denegado" fill="#FF0000" />
                        <Bar dataKey="Error" fill="#FF6A00" />
                    </BarChart>
                </div>
            </CardContent>
        </Card>
    )
}