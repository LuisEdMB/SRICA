import React, { useState, useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { useForm } from 'react-hook-form'
import * as yup from 'yup'
import * as jsondiffpatch from 'jsondiffpatch'
import { save } from 'save-file'
import * as GeneralAction from '../../Accion/General'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioReporte from '../../Servicio/Reporte'

import * as Utilitario from '../../Utilitario'

import { BotonGenerarReporte } from '../ComponenteGeneral/BotonGenerarReporte'
import { Tabla } from '../ComponenteGeneral/Tabla'
import { DatePickerSelector } from '../ComponenteGeneral/DatePickerSelector'

import { makeStyles, Grid, Card, CardContent, Typography, TextField } from '@material-ui/core'
import 'jsondiffpatch/dist/formatters-styles/html.css'

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

export const ReporteAccionSistema = () => {
    const claseEstilo = estilos()
    const [fechaInicio, SetFechaInicio] = useState(new Date())
    const [fechaFin, SetFechaFin] = useState(new Date())
    const [listadoRol, SetListadoRol] = useState([])
    const [listadoModulo, SetListadoModulo] = useState([])
    const [listadoRecurso, SetListadoRecurso] = useState([])
    const [listadoTipoEvento, SetListadoTipoEvento] = useState([])
    const [listadoAccion, SetListadoAccion] = useState([])
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
        {name: 'UsuarioAcceso', label: 'Usuario', options: { filterType: 'textField', filterOptions: {
            logic: (usuario, filters) => {
                if (filters.length){
                    var codigoUsuarioFila = listadoReporte
                        .filter((bitacora) => bitacora.UsuarioAcceso === usuario)[0].CodigoUsuario
                    var codigosUsuariosFiltrados = listadoReporte
                        .filter((bitacora) => bitacora.UsuarioAcceso.includes(filters))
                    return !codigosUsuariosFiltrados.some((bitacora) => 
                        codigoUsuarioFila.includes(bitacora.CodigoUsuario))
                }
                return false
            }
        } }},
        {name: 'NombreUsuario', label: 'Nombres', options: { filterType: 'textField' }},
        {name: 'ApellidoUsuario', label: 'Apellidos', options: { filterType: 'textField' }},
        {name: 'DescripcionRolUsuario', label: 'Rol', options: { filterType: 'multiselect', filterOptions: {
            names: listadoRol.map((rol) => rol.DescripcionRolUsuario)
        } }},
        {name: 'DescripcionModuloSistema', label: 'Modulo', options: { filterType: 'multiselect', 
            filterOptions: { names: listadoModulo.map((modulo) => modulo.Descripcion)
        } }},
        {name: 'DescripcionRecursoSistema', label: 'Recurso', options: { filterType: 'multiselect', 
            filterOptions: { names: listadoRecurso.map((recurso) => recurso.Descripcion)
        } }},
        {name: 'DescripcionTipoEventoSistema', label: 'Tipo de Evento', options: { filterType: 'multiselect', 
            filterOptions: { names: listadoTipoEvento.map((tipoEvento) => tipoEvento.Descripcion)
        } }},
        {name: 'DescripcionAccionSistema', label: 'Acción', options: { filterType: 'multiselect', 
            filterOptions: { names: listadoAccion.map((accion) => accion.Descripcion)
        } }},
        {name: 'DescripcionResultadoAccion', label: 'Descripción de Acción', options: { filter: false }},
        {name: 'ValorActual', label: 'Valor Trazado', options: { filter: false }},
        {name: 'FechaAccion', label: 'Fecha Acción', options: { filter: false }}
    ]

    useEffect(() => {
        dispatch(GeneralAction.AbrirBackdrop())
        ObtenerListadoRol()
    }, [])

    const ObtenerListadoRol = () => {
        ServicioReporte.ObtenerListadoRol((respuesta) => {
            SetListadoRol(respuesta)
            ObtenerListadoModulo()
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const ObtenerListadoModulo = () => {
        ServicioReporte.ObtenerListadoModulo((respuesta) => {
            SetListadoModulo(respuesta)
            ObtenerListadoRecurso()
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const ObtenerListadoRecurso = () => {
        ServicioReporte.ObtenerListadoRecurso((respuesta) => {
            SetListadoRecurso(respuesta)
            ObtenerListadoTipoEvento()
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const ObtenerListadoTipoEvento = () => {
        ServicioReporte.ObtenerListadoTipoEvento((respuesta) => {
            SetListadoTipoEvento(respuesta)
            ObtenerListadoAccion()
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const ObtenerListadoAccion = () => {
        ServicioReporte.ObtenerListadoAccion((respuesta) => {
            SetListadoAccion(respuesta)
            dispatch(GeneralAction.CerrarBackdrop())
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const GenerarReporte = () => {
        SetListadoReporte([])
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioReporte.ObtenerListadoBitacoraAccionSistema((respuesta) => {
            respuesta = Utilitario.FiltrarArrayPorPropiedadSegunRangoFecha(respuesta,
                'FechaAccion', fechaInicio.toISOString(), fechaFin.toISOString())
            var resultado = CrearDatosFilaTablaBitacoraAccionSistema(respuesta)
            SetListadoReporte(resultado)
            dispatch(GeneralAction.CerrarBackdrop())
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const CrearDatosFilaTablaBitacoraAccionSistema = (listado) => {
        var resultado = []
        resultado = listado
            .map((bitacora) => {
                var izquierda = Utilitario.ConvertirTextoAObjetoJSON(bitacora.ValorAnterior)
                var derecha = Utilitario.ConvertirTextoAObjetoJSON(bitacora.ValorActual)
                var deltaActual = jsondiffpatch.diff(izquierda, derecha)
                var valorActual = jsondiffpatch.formatters.html.format(deltaActual, izquierda);
                return {
                    CodigoBitacora: bitacora.CodigoBitacora,
                    CodigoUsuario: bitacora.CodigoUsuario,
                    UsuarioAcceso: bitacora.UsuarioAcceso,
                    NombreUsuario: bitacora.NombreUsuario,
                    ApellidoUsuario: bitacora.ApellidoUsuario,
                    CodigoRolUsuario: bitacora.CodigoRolUsuario,
                    DescripcionRolUsuario: bitacora.DescripcionRolUsuario,
                    CodigoModuloSistema: bitacora.CodigoModuloSistema,
                    DescripcionModuloSistema: bitacora.DescripcionModuloSistema,
                    CodigoRecursoSistema: bitacora.CodigoRecursoSistema,
                    DescripcionRecursoSistema: bitacora.DescripcionRecursoSistema,
                    CodigoTipoEventoSistema: bitacora.CodigoTipoEventoSistema,
                    DescripcionTipoEventoSistema: bitacora.DescripcionTipoEventoSistema,
                    CodigoAccionSistema: bitacora.CodigoAccionSistema,
                    DescripcionAccionSistema: bitacora.DescripcionAccionSistema,
                    DescripcionResultadoAccion: bitacora.DescripcionResultadoAccion,
                    ValorActual: <div dangerouslySetInnerHTML={{ __html: valorActual }}></div>,
                    FechaAccion: bitacora.FechaAccion
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
        ServicioReporte.GuardarAccionExportarReporteAccionSistema(() => {
            var tituloReporte = 'Reporte de Acciones del Sistema'
            var usuario = {
                Usuario: generalUsuarioLogueado.Usuario,
                Nombre: generalUsuarioLogueado.Nombre + ' ' + generalUsuarioLogueado.Apellido
            }
            var nombreArchivo = 'Reporte_Accion_Sistema.html'
            var resultado = Utilitario.GenerarPlantillaReporte(columns, data, {
                columnaTieneObjetoHtml: 9,
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
                        <b>Reporte de Acciones del Sistema</b>
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
                </CardContent>
            </Card>
        </Grid>
    )
}