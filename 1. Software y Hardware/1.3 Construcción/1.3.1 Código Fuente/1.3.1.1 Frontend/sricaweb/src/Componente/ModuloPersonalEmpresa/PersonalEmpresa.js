import React, { useState, useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import * as GeneralAction from '../../Accion/General'
import * as PersonalEmpresaAction from '../../Accion/PersonalEmpresa'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioPersonalEmpresa from '../../Servicio/PersonalEmpresa'

import * as Utilitario from '../../Utilitario'

import { BotonModificar } from '../ComponenteGeneral/BotonModificar'
import { BotonRegistrar } from '../ComponenteGeneral/BotonRegistrar'
import { BotonRegistrarPersonalMasivo } from '../ComponenteGeneral/BotonRegistrarPersonalMasivo'
import { BotonComprobarReconocimientoIris } from '../ComponenteGeneral/BotonComprobarReconocimientoIris'
import { BotonActivar } from '../ComponenteGeneral/BotonActivar'
import { BotonInactivar } from '../ComponenteGeneral/BotonInactivar'
import { Tabla } from '../ComponenteGeneral/Tabla'
import { FormularioEquipoBiometrico } from './FormularioPersonalEmpresa'
import { CapturaReconocimientoIrisPersonalEmpresa } from './CapturaReconocimientoIrisPersonalEmpresa'

import { makeStyles, Grid, Card, CardContent, Typography, FormControl, InputLabel, Select, MenuItem, Checkbox, ListItemText } from '@material-ui/core'
import { DropzoneDialog } from 'material-ui-dropzone'

const estilos = makeStyles({
    principal: {
        marginTop: 85,
        marginBottom: 17,
        minHeight: '100%',
        justifyContent: 'center'
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
        marginTop: 5,
        textAlign: 'right',
        alignSelf: 'flex-end',
        marginBottom: 5
    },
    selector: {
        marginTop: 30,
        marginBottom: 5,
        minWidth: 150
    },
    botonRegistrar: {
        marginTop: 10,
        marginRight: 17
    },
    botonComprobarReconocimientoIris: {
        display: 'block',
        marginLeft: 'auto',
        marginRight: 0,
        marginTop: 10
    },
    checkboxBorrarDatosBiometricos: {
        color: 'red'
    },
    tabla: {
        marginTop: 10
    }
})

export const PersonalEmpresa = () => {
    const claseEstilo = estilos()
    const [selectorArchivoRegistroMasivoVisible, SetSelectorArchivoRegistroMasivoVisible] = useState(false)
    const [botonInactivarPersonalVisible, SetBotonInactivarPersonalVisible] = useState(true)
    const [activo, SetActivo] = useState(true)
    var personalEmpresaSeleccionado = []
    const [listadoPersonalEmpresa, SetListadoPersonalEmpresa] = useState([])
    const [listadoPersonalEmpresaEstado, SetListadoPersonalEmpresaEstado] = useState([])
    const [listadoSede, SetListadoSede] = useState([])
    var listadoSedeGrouping = []
    const personalEmpresaFormulario = useSelector((store) => store.PersonalEmpresaFormulario)
    const dispatch = useDispatch()

    const columnasTabla = [
        {name: 'DNIPersonalEmpresa', label: 'DNI', options: { filterType: 'textField' }},
        {name: 'NombrePersonalEmpresa', label: 'Nombres', options: { filterType: 'textField' }},
        {name: 'ApellidoPersonalEmpresa', label: 'Apellidos', options: { filterType: 'textField' }},
        {name: 'DescripcionSede', label: 'Sede', options: { filterType: 'custom' , filterOptions: {
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
        }}},
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
        }}},
        {name: 'Iris', label: 'Iris Capturado', options: { filterType: 'multiselect', filterOptions: {
            names: ['Sí', 'No']
        } }},
        {name: '', label: '', options: { sort: false, filter: false }}
    ]

    useEffect(() => {
        ObtenerListadoPersonalEmpresa()
    }, [])

    const ObtenerListadoPersonalEmpresa = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioPersonalEmpresa.ObtenerListadoPersonalEmpresa((respuesta) => {
            SetListadoPersonalEmpresa(respuesta)
            CambiarEstadoListado(activo, respuesta)
            ObtenerListadoSedeEmpresa(() => dispatch(GeneralAction.CerrarBackdrop()))
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const CambiarEstadoListado = (estado, personalEmpresa = null) => {
        SetActivo(estado)
        var resultado = CrearDatosFilaTablaPersonalEmpresa(
            personalEmpresa === null
                ? listadoPersonalEmpresa
                : personalEmpresa, estado)
        SetListadoPersonalEmpresaEstado(resultado)
        SetBotonInactivarPersonalVisible(estado)
    }

    const CrearDatosFilaTablaPersonalEmpresa = (personalEmpresa, estado) => {
        var resultado = []
        resultado = personalEmpresa
            .filter((personal) => personal.IndicadorEstado === estado)
            .map((personal) => {
                return {
                    CodigoPersonalEmpresa: personal.CodigoPersonalEmpresa,
                    DNIPersonalEmpresa: personal.DNIPersonalEmpresa,
                    NombrePersonalEmpresa: personal.NombrePersonalEmpresa,
                    ApellidoPersonalEmpresa: personal.ApellidoPersonalEmpresa,
                    DescripcionSede: personal.DescripcionSedes === ''
                        ? Utilitario.SinAsignacionATexto('---')
                        : personal.DescripcionSedes,
                    DescripcionArea: personal.DescripcionAreas === ''
                        ? Utilitario.SinAsignacionATexto('---')
                        : personal.DescripcionAreas,
                    Iris: personal.TieneIrisRegistrado
                        ? "Sí"
                        : "No",
                    '': <BotonModificar 
                            key={ personal.CodigoPersonalEmpresa }
                            texto='Modificar Personal'
                            onClick={ () => AbrirFormularioPersonalEmpresaExistente(personal.CodigoPersonalEmpresa) }/>
                }
            })
        return resultado
    }

    const ObtenerListadoSedeEmpresa = (callback) => {
        ServicioPersonalEmpresa.ObtenerListadoSedeEmpresa((respuesta) => {
            respuesta = respuesta
                .filter((sede) => sede.IndicadorEstado)
                .map((sede) => {
                    sede.DescripcionSede = Utilitario.SinAsignacionATexto(sede.DescripcionSede)
                    return sede
                })
            SetListadoSede(respuesta)
            respuesta = respuesta.filter((sede) => !sede.IndicadorRegistroParaSinAsignacion)
            dispatch(PersonalEmpresaAction.SetListadoSedeEmpresa(respuesta))
            callback()
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const AbrirFormularioPersonalEmpresaNuevo = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        ObtenerListadoSedeEmpresa(() => {
            dispatch(PersonalEmpresaAction.SetFormularioPersonalEmpresaVacio())
            dispatch(PersonalEmpresaAction.SetFormularioPersonalEmpresaAnterior({}))
            dispatch(PersonalEmpresaAction.AbrirFormularioPersonalEmpresa())
            dispatch(GeneralAction.CerrarBackdrop())
        })
    }

    const RegistrarMasivamentePersonalEmpresa = (archivo) => {
        Utilitario.ArchivoABase64(archivo[0], (base64) => {
            AlertaSwal.MensajeConfirmacion({
                texto:  '<p>' + 
                            '¿Está seguro de registrar masivamente al personal?' + 
                        '</p>' + 
                        '<p style="color: red">' + 
                            'Advertencia: Si el archivo contiene DNIs de personal ' + 
                            'existente, éstos no serán registrados.' +
                        '</p>',
                textoBoton: 'Sí, registrar al personal',
                evento: () => {
                    dispatch(GeneralAction.AbrirBackdrop())
                    ServicioPersonalEmpresa.RegistrarPersonalEmpresaMasivo(base64, (respuesta) => {
                        dispatch(GeneralAction.CerrarBackdrop())
                        AlertaSwal.MensajeExito({
                            texto: 'Se ha registrado al personal correctamente: ' + respuesta.length + 
                                ' registrado(s).',
                            evento: () => {
                                SetSelectorArchivoRegistroMasivoVisible(false)
                                ObtenerListadoPersonalEmpresa()
                            }
                        })
                    }, (codigoExcepcion) => {
                        dispatch(GeneralAction.CerrarBackdrop())
                        CerrarSesionUsuario(codigoExcepcion)
                    })
                }
            })
        })   
    }

    const AbrirModalCapturaReconocimientoIris = () => {
        dispatch(PersonalEmpresaAction.AbrirComprobarReconocimientoIris())
    }

    const AbrirFormularioPersonalEmpresaExistente = (codigoPersonalEmpresa) => {
        dispatch(GeneralAction.AbrirBackdrop())
        ObtenerListadoSedeEmpresa(() => {
            ServicioPersonalEmpresa.ObtenerPersonalEmpresa(codigoPersonalEmpresa, (respuesta) => {
                SetAreasDelPersonalAArraySedes(respuesta.Areas);
                dispatch(PersonalEmpresaAction.SetFormularioPersonalEmpresa(respuesta))
                dispatch(PersonalEmpresaAction.SetFormularioPersonalEmpresaAnterior(respuesta))
                dispatch(PersonalEmpresaAction.AbrirFormularioPersonalEmpresa())
                dispatch(GeneralAction.CerrarBackdrop())
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        })
    }
    
    const SetAreasDelPersonalAArraySedes = (areasPersonal) => {
        areasPersonal.map((area) => {
            dispatch(PersonalEmpresaAction.SetListadoAreaEmpresaSeleccionado(
                area.CodigoSede, area.CodigoArea, area.Seleccionado, true
            ))
        })
    }

    const ObtenerPersonalEmpresaSeleccionado = (filaSeleccionada, todasLasFilas) => {
        var seleccionados = []
        todasLasFilas.map((seleccionado) => {
            seleccionados.push({
                CodigoPersonalEmpresa: listadoPersonalEmpresaEstado[seleccionado.dataIndex]
                    .CodigoPersonalEmpresa,
                DNIPersonalEmpresa: listadoPersonalEmpresaEstado[seleccionado.dataIndex]
                    .DNIPersonalEmpresa
            })
        })
        personalEmpresaSeleccionado = seleccionados
    }

    const InhabilitarPersonalEmpresa = () => {
        AlertaSwal.MensajeConfirmacion({
            texto:  '<p>' + 
                        '¿Está seguro de inactivar al personal: ' + personalEmpresaSeleccionado.length + 
                        ' registro(s) de personal seleccionado?' + 
                    '</p>' + 
                    '<p style="color: red">' + 
                        'Advertencia: Si inactiva al personal, no podrán acceder a las áreas.' +
                    '</p>',
            textoBoton: 'Sí, inactivar',
            evento: () => {
                dispatch(GeneralAction.AbrirBackdrop())
                ServicioPersonalEmpresa.InhabilitarPersonalEmpresa(personalEmpresaSeleccionado, () => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    AlertaSwal.MensajeExito({
                        texto: 'Se ha inactivado correctamente al personal seleccionado.',
                        evento: () => {
                            ObtenerListadoPersonalEmpresa()
                        }
                    })
                }, (codigoExcepcion) => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    CerrarSesionUsuario(codigoExcepcion)
                })
            }
        })
    }

    const HabilitarPersonalEmpresa = () => {
        AlertaSwal.MensajeConfirmacion({
            texto: '¿Está seguro de activar al personal: ' + personalEmpresaSeleccionado.length + 
                ' registro(s) de personal seleccionado?',
            textoBoton: 'Sí, activar',
            evento: () => {
                dispatch(GeneralAction.AbrirBackdrop())
                ServicioPersonalEmpresa.HabilitarPersonalEmpresa(personalEmpresaSeleccionado, () => {
                    dispatch(GeneralAction.CerrarBackdrop())
                    AlertaSwal.MensajeExito({
                        texto: 'Se ha activado correctamente al personal seleccionado.',
                        evento: () => {
                            ObtenerListadoPersonalEmpresa()
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
                        <b>Personal de la Empresa</b>
                    </Typography>
                    <br/>
                    <br/>
                    <Grid
                        container
                        className={ claseEstilo.contenido }>
                        <div className={ claseEstilo.botonRegistrar }>
                            <BotonRegistrar 
                                texto='Registrar Personal' 
                                textoVisible={ true }
                                onClick={ () => AbrirFormularioPersonalEmpresaNuevo() }/>
                        </div>
                        <div className={ claseEstilo.botonRegistrar }>
                            <BotonRegistrarPersonalMasivo
                                onClick={ () => SetSelectorArchivoRegistroMasivoVisible(true) }/>
                            <DropzoneDialog
                                open={ selectorArchivoRegistroMasivoVisible }
                                filesLimit={ 1 }
                                dropzoneText='Arrastra y suelta un archivo aquí, o haz click'
                                onSave={ (e) => RegistrarMasivamentePersonalEmpresa(e) }
                                acceptedFiles={ ['.xls', '.xlsx'] }
                                showPreviews={ true }
                                maxFileSize={ 104857600 }
                                cancelButtonText='Cancelar'
                                submitButtonText='Registrar'
                                dialogTitle='Selecciona un archivo (.xls, .xlsx) (100mb máx.):'
                                onClose={ () => SetSelectorArchivoRegistroMasivoVisible(false) }/>
                        </div>
                        <div className={ claseEstilo.botonComprobarReconocimientoIris }>
                            <BotonComprobarReconocimientoIris
                                texto='Verificar Reconocimiento'
                                textoVisible={ true }
                                onClick={ () => AbrirModalCapturaReconocimientoIris() }/>
                        </div>
                        <Grid
                            item 
                            xs={ 12 }>
                        </Grid>
                        <Grid
                            item 
                            xs={ 12 } sm={ 3 }>
                            <FormControl 
                                className={ claseEstilo.selector }>
                                <InputLabel id="selectorEstadoPersonalEmpresa">Estado</InputLabel>
                                <Select
                                    labelId="selectorEstadoPersonalEmpresa"
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
                                botonInactivarPersonalVisible 
                                    ?   <BotonInactivar 
                                            texto='Inactivar Personal' 
                                            onClick={ () => InhabilitarPersonalEmpresa() }/>
                                    :   <BotonActivar 
                                            texto='Activar Personal' 
                                            onClick={ () => HabilitarPersonalEmpresa() }/>
                            }
                        </Grid>
                        <Grid
                            item
                            xs={ 12 }
                            className={ claseEstilo.tabla }>
                            <Tabla 
                                columnas={ columnasTabla } 
                                datos={ listadoPersonalEmpresaEstado }
                                seleccionFila={ true }
                                onRowsSelect={ (rowsSelected, allRows) => 
                                    ObtenerPersonalEmpresaSeleccionado(rowsSelected, allRows) }/>
                        </Grid>
                    </Grid>
                </CardContent>
                <FormularioEquipoBiometrico
                    RecargarListadoPersonalEmpresa={ ObtenerListadoPersonalEmpresa }/>
                {
                    personalEmpresaFormulario.ModalComprobarReconocimientoIris && <CapturaReconocimientoIrisPersonalEmpresa/>   
                }
            </Card>
        </Grid>
    )
}