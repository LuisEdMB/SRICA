import React, { useState, useEffect } from 'react'
import { save } from 'save-file'
import { useDispatch, useSelector } from 'react-redux'
import * as GeneralAction from '../../Accion/General'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioReporte from '../../Servicio/Reporte'

import * as Utilitario from '../../Utilitario'

import { BotonGenerarReporte } from '../ComponenteGeneral/BotonGenerarReporte'
import { Tabla } from '../ComponenteGeneral/Tabla'

import { makeStyles, Grid, Card, CardContent, Typography, FormControl, InputLabel, Select, MenuItem, Checkbox, ListItemText } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        marginTop: 85,
        minHeight: '100%',
        justifyContent: 'center',
        marginBottom: 22
    },
    tarjeta: {
        marginLeft: 17,
        marginRight: 17,
        color: '#48525e',
        width: '100vw'
    },
    contenido: {
        flexGrow: 1
    },
    centrar: {
        fontWeight: 'bold',
        textAlign: 'center',
        justifyContent: 'center'
    },
    selector: {
        width: 200,
        marginRight: 15
    },
    botonGenerarReporte: {
        marginTop: 15
    },
    tabla: {
        marginTop: 10
    }
})

export const ReporteSistema = () => {
    const claseEstilo = estilos()
    const [reporteEquipoBiometricoVisible, SetReporteEquipoBiometricoVisible] = useState(true)
    const [listadoNomenclatura, SetListadoNomenclatura] = useState([])
    const [listadoSede, SetListadoSede] = useState([])
    var listadoSedeGrouping = []
    const [listadoReporte, SetListadoReporte] = useState([])
    const generalUsuarioLogueado = useSelector((store) => store.GeneralUsuarioLogueado)
    const dispatch = useDispatch()
    const columnasTablaReporteEquipoBiometrico = [
        {name: 'DescripcionSede', label: 'Sede', options: { filterType: 'custom', filterOptions: {
            logic: (sede, filters) => {
                if (filters.length) return !filters.some(filter => sede === filter)
                return false;
            },
            display: (filterList, onChange, index, column) => {
                listadoSedeGrouping = listadoSede.filter((sede) => filterList[index].includes(sede.DescripcionSede))
                return (
                    <FormControl>
                        <InputLabel>
                            Sede
                        </InputLabel>
                        <Select
                            multiple
                            value={ filterList[index] }
                            renderValue={ selected => selected.join('; ') }
                            onChange={ event => {
                                filterList[index] = event.target.value;
                                onChange(filterList[index], index, column);
                            } }
                        >
                        {
                            listadoSede.map((sede) => {
                                return (
                                    <MenuItem 
                                        key={ sede.CodigoSede } 
                                        value={ sede.DescripcionSede }>
                                        <Checkbox
                                            color='primary'
                                            checked={ filterList[index].indexOf(sede.DescripcionSede) > -1 }
                                        />
                                        <ListItemText primary={ sede.DescripcionSede } />
                                    </MenuItem>
                                )
                            })
                        }
                        </Select>
                    </FormControl>
                );
            }
        } }},
        {name: 'DescripcionArea', label: 'Área', options: { filterType: 'custom', filterOptions: {
            logic: (area, filters) => {
                if (filters.length) return !filters.some(filter => area === filter)
                return false;
              },
            display: (filterList, onChange, index, column) => {
                var areas = []
                listadoSedeGrouping.map((sede) => {
                    areas.push({
                        Codigo: '',
                        Descripcion: sede.DescripcionSede,
                        ItemDeshabilitado: true
                    })
                    return sede.Areas
                        .filter((area) => area.IndicadorEstado)
                        .map((area) => {
                            areas.push({
                                Codigo: area.CodigoArea,
                                Descripcion: area.DescripcionArea,
                                ItemDeshabilitado: false
                            })
                        })
                })
                filterList[index] = filterList[index].filter((filtro) => areas.some((area) => 
                    area.Descripcion === filtro))
                return (
                    <FormControl>
                        <InputLabel>
                            Área
                        </InputLabel>
                        <Select
                            multiple
                            value={ filterList[index] }
                            renderValue={ selected => selected.join('; ') }
                            onChange={ event => {
                                filterList[index] = event.target.value;
                                onChange(filterList[index], index, column);
                            } }
                        >
                        {
                            areas.map((area) => {
                                return (
                                    <MenuItem 
                                        key={ area.Codigo } 
                                        value={ Utilitario.SinAsignacionATexto(area.Descripcion) }
                                        disabled={ area.ItemDeshabilitado }>
                                        {
                                            area.ItemDeshabilitado
                                                ? null
                                                : <Checkbox
                                                    color='primary'
                                                    checked={ filterList[index].indexOf(Utilitario.SinAsignacionATexto(area.Descripcion)) > -1 }/>
                                        }
                                        <ListItemText primary={ Utilitario.SinAsignacionATexto(area.Descripcion) } />
                                    </MenuItem>
                                )
                            })
                        }
                        </Select>
                    </FormControl>
                );
            }
        } }},
        {name: 'DescripcionNomenclatura', label: 'Nomenclatura', options: { filterType: 'multiselect', filterOptions: {
            names: listadoNomenclatura
                .map((nomenclatura) => nomenclatura.DescripcionNomenclatura)
        } }},
        {name: 'NombreEquipoBiometrico', label: 'Nombre de Equipo', options: { filterType: 'textField' }},
        {name: 'DireccionRed', label: 'IP', options: { filterType: 'textField' }},
        {name: 'IndicadorEstado', label: 'Estado de Registro', options: { filterType: 'multiselect', filterOptions:{
            names: ['Activo', 'Inactivo']
        } }}
    ]
    const columnasTablaReportePersonalEmpresa = [
        {name: 'DescripcionSede', label: 'Sede', options: { filterType: 'custom', filterOptions: {
            logic: (sede, filters) => {
                if (filters.length) return !filters.some(filter => sede.includes(filter))
                return false;
            },
            display: (filterList, onChange, index, column) => {
                listadoSedeGrouping = listadoSede.filter((sede) => filterList[index].includes(sede.DescripcionSede))
                return (
                    <FormControl>
                        <InputLabel>
                            Sede
                        </InputLabel>
                        <Select
                            multiple
                            value={ filterList[index] }
                            renderValue={ selected => selected.join('; ') }
                            onChange={ event => {
                                filterList[index] = event.target.value;
                                onChange(filterList[index], index, column);
                            } }
                        >
                        {
                            listadoSede.map((sede) => {
                                return (
                                    <MenuItem 
                                        key={ sede.CodigoSede } 
                                        value={ sede.DescripcionSede }>
                                        <Checkbox
                                            color='primary'
                                            checked={ filterList[index].indexOf(sede.DescripcionSede) > -1 }
                                        />
                                        <ListItemText primary={ sede.DescripcionSede } />
                                    </MenuItem>
                                )
                            })
                        }
                        </Select>
                    </FormControl>
                );
            }
        } }},
        {name: 'DescripcionArea', label: 'Área', options: { filterType: 'custom', filterOptions: {
            logic: (area, filters) => {
                if (filters.length) return !filters.some(filter => area.includes(filter))
                return false;
              },
            display: (filterList, onChange, index, column) => {
                var areas = []
                listadoSedeGrouping.map((sede) => {
                    areas.push({
                        Codigo: '',
                        Descripcion: sede.DescripcionSede,
                        ItemDeshabilitado: true
                    })
                    return sede.Areas
                        .filter((area) => area.IndicadorEstado)
                        .map((area) => {
                            areas.push({
                                Codigo: area.CodigoArea,
                                Descripcion: area.DescripcionArea,
                                ItemDeshabilitado: false
                            })
                        })
                })
                filterList[index] = filterList[index].filter((filtro) => areas.some((area) => 
                    area.Descripcion === filtro))
                return (
                    <FormControl>
                        <InputLabel>
                            Área
                        </InputLabel>
                        <Select
                            multiple
                            value={ filterList[index] }
                            renderValue={ selected => selected.join('; ') }
                            onChange={ event => {
                                filterList[index] = event.target.value;
                                onChange(filterList[index], index, column);
                            } }
                        >
                        {
                            areas.map((area) => {
                                return (
                                    <MenuItem 
                                        key={ area.Codigo } 
                                        value={ Utilitario.SinAsignacionATexto(area.Descripcion) }
                                        disabled={ area.ItemDeshabilitado }>
                                        {
                                            area.ItemDeshabilitado
                                                ? null
                                                : <Checkbox
                                                    color='primary'
                                                    checked={ filterList[index].indexOf(Utilitario.SinAsignacionATexto(area.Descripcion)) > -1 }/>
                                        }
                                        <ListItemText primary={ Utilitario.SinAsignacionATexto(area.Descripcion) } />
                                    </MenuItem>
                                )
                            })
                        }
                        </Select>
                    </FormControl>
                );
            }
        } }},
        {name: 'DNIPersonalEmpresa', label: 'DNI', options: { filterType: 'textField' }},
        {name: 'NombrePersonalEmpresa', label: 'Nombres', options: { filterType: 'textField' }},
        {name: 'ApellidoPersonalEmpresa', label: 'Apellidos', options: { filterType: 'textField' }},
        {name: 'Iris', label: 'Iris Capturado', options: { filterType: 'multiselect', filterOptions: {
            names: ['Sí', 'No']
        } }},
        {name: 'IndicadorEstado', label: 'Estado de Registro', options: { filterType: 'multiselect', filterOptions:{
            names: ['Activo', 'Inactivo']
        } }}
    ]

    useEffect(() => {
        dispatch(GeneralAction.AbrirBackdrop())
        ObtenerListadoNomenclatura()
    }, [])

    const ObtenerListadoNomenclatura = () => {
        ServicioReporte.ObtenerListadoNomenclatura((respuesta) => {
            respuesta = respuesta
                .filter((nomenclatura) => nomenclatura.IndicadorEstado)
                .map((nomenclatura) => {
                    nomenclatura.DescripcionNomenclatura = Utilitario.SinAsignacionATexto(
                        nomenclatura.DescripcionNomenclatura)
                    return nomenclatura
                })
            SetListadoNomenclatura(respuesta)
            ObtenerListadoSede();
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const ObtenerListadoSede = () => {
        ServicioReporte.ObtenerListadoSede((respuesta) => {
            respuesta = respuesta
                .filter((sede) => sede.IndicadorEstado)
                .map((sede) => {
                    sede.DescripcionSede = Utilitario.SinAsignacionATexto(sede.DescripcionSede)
                    return sede
                })
            SetListadoSede(respuesta)
            dispatch(GeneralAction.CerrarBackdrop())
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const CambiarTipoReporte = () => {
        SetListadoReporte([])
        SetReporteEquipoBiometricoVisible(!reporteEquipoBiometricoVisible)
    }

    const GenerarReporte = () => {
        SetListadoReporte([])
        dispatch(GeneralAction.AbrirBackdrop())
        if (reporteEquipoBiometricoVisible){
            ServicioReporte.ObtenerListadoEquipoBiometrico((respuesta) => {
                var resultado = CrearDatosFilaTablaEquipoBiometrico(respuesta)
                SetListadoReporte(resultado)
                dispatch(GeneralAction.CerrarBackdrop())
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        }
        else{
            ServicioReporte.ObtenerListadoPersonalEmpresa((respuesta) => {
                var resultado = CrearDatosFilaTablaPersonalEmpresa(respuesta)
                SetListadoReporte(resultado)
                dispatch(GeneralAction.CerrarBackdrop())
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        }
    }

    const CrearDatosFilaTablaEquipoBiometrico = (listado) => {
        var resultado = []
        resultado = listado
            .map((equipoBiometrico) => {
                return {
                    DescripcionSede: Utilitario.SinAsignacionATexto(equipoBiometrico.DescripcionSede),
                    DescripcionArea: Utilitario.SinAsignacionATexto(equipoBiometrico.DescripcionArea),
                    DescripcionNomenclatura: Utilitario.SinAsignacionATexto(
                        equipoBiometrico.DescripcionNomenclatura),
                    NombreEquipoBiometrico: equipoBiometrico.NombreEquipoBiometrico,
                    DireccionRed: equipoBiometrico.DireccionRedEquipoBiometrico,
                    IndicadorEstado: Utilitario.EstadoATexto(equipoBiometrico.IndicadorEstado)
                }
            })
        return resultado
    }

    const CrearDatosFilaTablaPersonalEmpresa = (listado) => {
        var resultado = []
        resultado = listado
            .map((personalEmpresa) => {
                return {
                    DescripcionSede: personalEmpresa.DescripcionSedes === ''
                        ? Utilitario.SinAsignacionATexto('---')
                        : personalEmpresa.DescripcionSedes,
                    DescripcionArea: personalEmpresa.DescripcionAreas === ''
                        ? Utilitario.SinAsignacionATexto('---')
                        : personalEmpresa.DescripcionAreas,
                    DNIPersonalEmpresa: personalEmpresa.DNIPersonalEmpresa,
                    NombrePersonalEmpresa: personalEmpresa.NombrePersonalEmpresa,
                    ApellidoPersonalEmpresa: personalEmpresa.ApellidoPersonalEmpresa,
                    Iris: personalEmpresa.TieneIrisRegistrado
                        ? "Sí"
                        : "No",
                    IndicadorEstado: Utilitario.EstadoATexto(personalEmpresa.IndicadorEstado)
                }
            })
        return resultado
    }

    const CerrarSesionUsuario = (codigoExcepcion) => {
        if (codigoExcepcion === Constante.CODIGO_EXCEPCION_ADVERTENCIA_SIMPLE_LOGOUT_USUARIO){
            dispatch(GeneralAction.SetDatosUsuarioNoLogueado())
            dispatch(GeneralAction.OcultarEncabezado())
            sessionStorage.removeItem(Constante.VARIABLE_LOCAL_STORAGE)
        }
    }

    const ExportarReporte = (buildHead, buildBody, columns, data) => {
        dispatch(GeneralAction.AbrirBackdrop())
        if (listadoReporte.length === 0){
            dispatch(GeneralAction.CerrarBackdrop())
            AlertaSwal.MensajeAlerta({
                titulo: '¡Advertencia!',
                texto: 'Debe generar el reporte antes de exportarlo.',
                icono: 'warning'
            })
            return false
        }
        ServicioReporte.GuardarAccionExportarReporteSistema(
            reporteEquipoBiometricoVisible 
                ? 'Reporte de Equipos Biométricos' 
                : 'Reporte de Personal de la Empresa',
            () => {
                var tituloReporte = reporteEquipoBiometricoVisible
                    ? 'Reporte de Equipos Biométricos'
                    : 'Reporte de Personal de la Empresa'
                var usuario = {
                    Usuario: generalUsuarioLogueado.Usuario,
                    Nombre: generalUsuarioLogueado.Nombre + ' ' + generalUsuarioLogueado.Apellido
                }
                var nombreArchivo = reporteEquipoBiometricoVisible
                    ? 'Reporte_Equipo_Biometrico.html'
                    : 'Reporte_Personal_Empresa.html'
                var resultado = Utilitario.GenerarPlantillaReporte(columns, data, {
                    titulo: tituloReporte,
                    usuario: usuario
                })
                dispatch(GeneralAction.CerrarBackdrop())
                save(resultado, nombreArchivo)
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        return false
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
                        <b>Reporte del Sistema</b>
                    </Typography>
                    <br/>
                    <br/>
                    <Grid
                        container
                        className={ claseEstilo.contenido }>
                        <div className={ claseEstilo.selector }>
                            <FormControl>
                                <InputLabel id="selectorTipoReporte">Reporte</InputLabel>
                                <Select
                                    labelId="selectorTipoReporte"
                                    defaultValue={ reporteEquipoBiometricoVisible }
                                    onChange={ () => CambiarTipoReporte() }>
                                    <MenuItem value={ true }>Equipos Biométricos</MenuItem>
                                    <MenuItem value={ false }>Personal de la Empresa</MenuItem>
                                </Select>
                            </FormControl>
                        </div>
                        <div className={ claseEstilo.botonGenerarReporte }>
                            <BotonGenerarReporte
                                onClick={ () => GenerarReporte() }/>
                        </div>
                        {
                            reporteEquipoBiometricoVisible
                                ?   <Grid
                                        item
                                        xs={ 12 }
                                        className={ claseEstilo.tabla }>
                                        <Tabla 
                                            columnas={ columnasTablaReporteEquipoBiometrico } 
                                            datos={ listadoReporte }
                                            seleccionFila={ false }
                                            download={ true }
                                            downloadOptions={{
                                                filterOptions: {
                                                    useDisplayedColumnsOnly: true,
                                                    useDisplayedRowsOnly: true
                                                }
                                            }}
                                            onDownload={ (buildHead, buildBody, columns, data) => 
                                                ExportarReporte(buildHead, buildBody, columns, data) }/>
                                    </Grid>
                                :   <Grid
                                        item
                                        xs={ 12 }
                                        className={ claseEstilo.tabla }>
                                        <Tabla 
                                            columnas={ columnasTablaReportePersonalEmpresa } 
                                            datos={ listadoReporte }
                                            seleccionFila={ false }
                                            download={ true }
                                            downloadOptions={{
                                                filterOptions: {
                                                    useDisplayedColumnsOnly: true,
                                                    useDisplayedRowsOnly: true
                                                }
                                            }}
                                            onDownload={ (buildHead, buildBody, columns, data) => 
                                                ExportarReporte(buildHead, buildBody, columns, data) }/>
                                    </Grid>
                        }
                    </Grid>
                </CardContent>
            </Card>
        </Grid>
    )
}
var asd = {
    asd: {

    }
}