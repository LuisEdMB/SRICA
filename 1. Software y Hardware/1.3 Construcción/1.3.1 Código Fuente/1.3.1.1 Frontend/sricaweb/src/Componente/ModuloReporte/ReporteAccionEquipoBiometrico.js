import React, { useState, useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { useForm } from 'react-hook-form'
import * as yup from 'yup'
import { save } from 'save-file'
import * as GeneralAction from '../../Accion/General'
import * as ReporteAction from '../../Accion/Reporte'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioReporte from '../../Servicio/Reporte'

import * as Utilitario from '../../Utilitario'

import { BotonGenerarReporte } from '../ComponenteGeneral/BotonGenerarReporte'
import { BotonVisualizarFoto } from '../ComponenteGeneral/BotonVisualizarFoto'
import { Tabla } from '../ComponenteGeneral/Tabla'
import { DatePickerSelector } from '../ComponenteGeneral/DatePickerSelector'
import { FotoPersonalEquipoBiometrico } from './FotoPersonalEquipoBiometrico'

import { makeStyles, Grid, Card, CardContent, Typography, TextField, FormControl, InputLabel, Select, MenuItem, Checkbox, ListItemText } from '@material-ui/core'

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
    componenteAbajoDerecha: {
        marginTop: 15,
        textAlign: 'right',
        alignSelf: 'flex-end',
        marginBottom: 5
    },
    campoFecha: {
        minWidth: 150,
        marginRight: 17
    },
    botonGenerarReporte: {
        marginTop: 10
    },
    tabla: {
        marginTop: 10
    },
    colorError: {
        fontSize: 11,
        fontWeight: 'bold',
        color: 'red',
        textAlign: 'left'
    }
})

export const ReporteAccionEquipoBiometrico = () => {
    const claseEstilo = estilos()
    const [fechaInicio, SetFechaInicio] = useState(new Date())
    const [fechaFin, SetFechaFin] = useState(new Date())
    const [listadoResultadoAcceso, SetListadoResultadoAcceso] = useState([])
    const [listadoSede, SetListadoSede] = useState([])
    var listadoSedeGrouping = []
    const [listadoReporte, SetListadoReporte] = useState([])
    const generalUsuarioLogueado = useSelector((store) => store.GeneralUsuarioLogueado)
    const dispatch = useDispatch()

    const errores = yup.object().shape({
        FechaInicio: yup.string().required('El campo "Fecha inicio" no debe estar vacío.'),
        FechaFin: yup.string().required('El campo "Fecha fin" no debe estar vacío.')
    })
    const { register, handleSubmit, errors } = useForm({
        mode: 'onChange',
        validationSchema: errores
    })

    const columnasTabla = [
        {name: 'DNIPersonalEmpresa', label: 'DNI', options: { filterType: 'textField', filterOptions: {
            logic: (dni, filters) => {
                if (filters.length){
                    var codigoPersonalEmpresaFila = listadoReporte
                        .filter((bitacora) => bitacora.DNIPersonalEmpresa === dni)[0].CodigoPersonalEmpresa
                    var codigosPersonalEmpresaFiltrados = listadoReporte
                        .filter((bitacora) => bitacora.DNIPersonalEmpresa.includes(filters))
                    return !codigosPersonalEmpresaFiltrados.some((bitacora) => 
                        codigoPersonalEmpresaFila.includes(bitacora.CodigoPersonalEmpresa))
                    }
                return false
            }
        } }},
        {name: 'NombrePersonalEmpresa', label: 'Nombres', options: { filterType: 'textField' }},
        {name: 'ApellidoPersonalEmpresa', label: 'Apellidos', options: { filterType: 'textField' }},
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
                    var areasEncontradas = listadoReporte.filter((bitacora) => 
                        bitacora.CodigoSede === sede.CodigoSede)
                    var areasEncontradasNoDuplicados = 
                        Utilitario.RemoverDuplicadosArrayObjeto(areasEncontradas, 'DescripcionArea')
                    return areasEncontradasNoDuplicados
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
        {name: 'NombreEquipoBiometrico', label: 'Equipo Biométrico', options: { filterType: 'textField' }},
        {name: 'DescripcionResultadoAcceso', label: 'Resultado de Acceso', options: { 
            filterType: 'multiselect', 
            filterOptions: {
                names: listadoResultadoAcceso.map((resultadoAcceso) => resultadoAcceso.Descripcion)
        } }},
        {name: 'DescripcionResultadoAccion', label: 'Descripción del Resultado', options: { filter: false }},
        {name: 'FechaAcceso', label: 'Fecha Acceso', options: { filter: false }},
        {name: 'Foto', label: 'Foto Personal no Registrado', options: { sort: false, filter: false }}
    ]

    useEffect(() => {
        dispatch(GeneralAction.AbrirBackdrop())
        ObtenerListadoResultadoAcceso()
    }, [])

    const ObtenerListadoResultadoAcceso = () => {
        ServicioReporte.ObtenerListadoResultadoAcceso((respuesta) => {
            SetListadoResultadoAcceso(respuesta)
            dispatch(GeneralAction.CerrarBackdrop())
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const GenerarReporte = () => {
        SetListadoReporte([])
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioReporte.ObtenerListadoBitacoraAccionEquipoBiometrico((respuesta) => {
            respuesta = Utilitario.FiltrarArrayPorPropiedadSegunRangoFecha(respuesta,
                'FechaAcceso', fechaInicio.toISOString(), fechaFin.toISOString())
            var sedes = respuesta.map((bitacora) => ({
                CodigoSede: bitacora.CodigoSede,
                DescripcionSede: bitacora.DescripcionSede
            }))
            var sedesSinDuplicados = Utilitario.RemoverDuplicadosArrayObjeto(sedes, 'DescripcionSede')
            SetListadoSede(sedesSinDuplicados)
            var resultado = CrearDatosFilaTablaBitacoraAccionEquipoBiometrico(respuesta)
            SetListadoReporte(resultado)
            dispatch(GeneralAction.CerrarBackdrop())
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const CrearDatosFilaTablaBitacoraAccionEquipoBiometrico = (listado) => {
        var resultado = []
        resultado = listado
            .map((bitacora) => {
                return {
                    CodigoBitacora: bitacora.CodigoBitacora,
                    CodigoPersonalEmpresa: bitacora.CodigoPersonalEmpresa,
                    DNIPersonalEmpresa: bitacora.DNIPersonalEmpresa,
                    NombrePersonalEmpresa: bitacora.NombrePersonalEmpresa,
                    ApellidoPersonalEmpresa: bitacora.ApellidoPersonalEmpresa,
                    CodigoSede: bitacora.CodigoSede,
                    DescripcionSede: bitacora.DescripcionSede,
                    CodigoArea: bitacora.CodigoArea,
                    DescripcionArea: bitacora.DescripcionArea,
                    NombreEquipoBiometrico: bitacora.NombreEquipoBiometrico,
                    CodigoResultadoAcceso: bitacora.CodigoResultadoAcceso,
                    DescripcionResultadoAcceso: bitacora.DescripcionResultadoAcceso,
                    DescripcionResultadoAccion: bitacora.DescripcionResultadoAccion,
                    FechaAcceso: bitacora.FechaAcceso,
                    Foto: bitacora.ImagenPersonalNoRegistrado !== '' 
                        ?   <BotonVisualizarFoto
                                src={ bitacora.ImagenPersonalNoRegistrado }
                                onClick={ () =>  CargarFotoPersonalNoAutorizado(
                                    bitacora.ImagenPersonalNoRegistrado)}/>
                        : ''
                }
            })
        return resultado
    }

    const CargarFotoPersonalNoAutorizado = (imagenPersonalNoRegistrado) => {
        dispatch(ReporteAction.AbrirVisualizadorPersonalNoRegistrado(imagenPersonalNoRegistrado))
    }

    const CerrarSesionUsuario = (codigoExcepcion) => {
        if (codigoExcepcion === Constante.CODIGO_EXCEPCION_ADVERTENCIA_SIMPLE_LOGOUT_USUARIO){
            dispatch(GeneralAction.SetDatosUsuarioNoLogueado())
            dispatch(GeneralAction.OcultarEncabezado())
            sessionStorage.removeItem(Constante.VARIABLE_LOCAL_STORAGE)
        }
    }

    const ExportarReporte = (buildHead, buildBody, columns, data) => {
        var continuar = false
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
        ServicioReporte.GuardarAccionExportarReporteAccionEquipoBiometrico(() => {
            var tituloReporte = 'Reporte de Acciones de Equipos Biométricos'
            var usuario = {
                Usuario: generalUsuarioLogueado.Usuario,
                Nombre: generalUsuarioLogueado.Nombre + ' ' + generalUsuarioLogueado.Apellido
            }
            var nombreArchivo = 'Reporte_Accion_Equipo_Biometrico.html'
            var resultado = Utilitario.GenerarPlantillaReporte(columns, data, {
                titulo: tituloReporte,
                usuario: usuario,
                columnaTieneObjetoHtml: 9,
                esColumnaConImagen: true
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
                        <b>Reporte de Acciones de Equipos Biométricos</b>
                    </Typography>
                    <br/>
                    <br/>
                    <Grid
                        container
                        className={ claseEstilo.contenido }>
                        <div className={ claseEstilo.campoFecha }>
                            <DatePickerSelector 
                                title='Fecha Inicio'
                                value={ fechaInicio } 
                                changeValue={ SetFechaInicio }/>
                            <TextField 
                                name='FechaInicio'
                                type='hidden'
                                value={ fechaInicio }
                                inputRef={ register }/>
                            {
                                errors.FechaInicio
                                    ?   <Grid 
                                            container
                                            alignItems='flex-end'>
                                            <Grid 
                                                item xs={ 12 }>
                                                <p className={ claseEstilo.colorError }>
                                                    { errors.FechaInicio.message }
                                                </p> 
                                            </Grid>
                                        </Grid>
                                    : <br/>
                            }
                        </div>
                        <div className={ claseEstilo.campoFecha }>
                            <DatePickerSelector 
                                title='Fecha Fin'
                                value={ fechaFin } 
                                changeValue={ SetFechaFin }/>
                            <TextField 
                                name='FechaFin'
                                type='hidden'
                                value={ fechaFin }
                                inputRef={ register }/>
                            {
                                errors.FechaFin
                                    ?   <Grid 
                                            container
                                            alignItems='flex-end'>
                                            <Grid 
                                                item xs={ 12 }>
                                                <p className={ claseEstilo.colorError }>
                                                    { errors.FechaFin.message }
                                                </p> 
                                            </Grid>
                                        </Grid>
                                    : <br/>
                            }
                        </div>
                        <div className={ claseEstilo.botonGenerarReporte }>
                            <BotonGenerarReporte
                                onClick={ handleSubmit(() => GenerarReporte()) }/>
                        </div>
                        <Grid
                            item
                            xs={ 12 }
                            className={ claseEstilo.tabla }>
                            <Tabla 
                                columnas={ columnasTabla } 
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
                    </Grid>
                    <FotoPersonalEquipoBiometrico/>
                </CardContent>
            </Card>
        </Grid>
    )
}