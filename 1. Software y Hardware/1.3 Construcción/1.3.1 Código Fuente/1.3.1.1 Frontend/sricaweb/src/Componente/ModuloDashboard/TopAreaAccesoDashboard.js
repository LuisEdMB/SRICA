import React, { useEffect, useState } from 'react'
import { useDispatch } from 'react-redux'
import * as GeneralAction from '../../Accion/General'
import * as Constante from '../../Constante'

import * as ServicioDashboard from '../../Servicio/Dashboard'

import * as Utilitario from '../../Utilitario'

import { makeStyles, Card, CardContent, Typography, CircularProgress, List, ListItem, ListItemText, Dialog } from '@material-ui/core'
import { PieChart, Pie, Cell } from 'recharts'
import { TituloModal } from '../ComponenteGeneral/TituloModal'
import { ContenidoModal } from '../ComponenteGeneral/ContenidoModal'

const estilos = makeStyles({
    principal: {
        margin: 18,
        color: '#48525e'
    },
    titulo: {
        fontSize: 15,
        fontWeight: 'bold'
    },
    subtitulo: {
        fontSize: 12,
        color: '#8e97a6'
    },
    modal: {
        color: '#48525e'
    }
})

export const TopAreaAccesoDashboard = () => {
    const claseEstilo = estilos()
    const [registrosTop, SetRegistrosTop] = useState(null)
    const [listadoPersonal, SetListadoPersonal] = useState([])
    const [colores, SetColores] = useState([])
    const [abrirModal, SetAbrirModal] = useState(false)
    const [areaSeleccionada, SetAreaSeleccionada] = useState('')
    var listadoArea = []
    var fechaFin = new Date();
    var fechaInicio = Utilitario.DefinirFechaSegunCantidadMes(fechaFin, -6)
    const dispatch = useDispatch()

    useEffect(() => {
        ObtenerListadoPersonalEmpresa()
    }, [])

    const ObtenerListadoPersonalEmpresa = () => {
        ServicioDashboard.ObtenerListadoPersonalEmpresa((respuesta) => {
            respuesta = respuesta.filter((personal) => personal.IndicadorEstado)
            SetListadoPersonal(respuesta)
            ObtenerListadoAreaEmpresa()
        }, (codigoExcepcion) => {
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const ObtenerListadoAreaEmpresa = () => {
        ServicioDashboard.ObtenerListadoAreaEmpresa((respuesta) => {
            respuesta = respuesta.filter((area) => area.IndicadorEstado &&
                !area.IndicadorRegistroParaSinAsignacion)
            listadoArea = respuesta
            SetColores(Utilitario.GenerarColoresSegunCantidad(respuesta.length))
            ObtenerListadoBitacoraAccionEquipoBiometricoDelArea()
        }, (codigoExcepcion) => {
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const ObtenerListadoBitacoraAccionEquipoBiometricoDelArea = () => {
        ServicioDashboard.ObtenerListadoBitacoraAccionEquipoBiometrico((respuesta) => {
            var codigosAreas = listadoArea.map((area) => area.CodigoArea)
            respuesta = Utilitario.FiltrarArrayPorPropiedadSegunOtroArray(respuesta, 
                'CodigoArea', codigosAreas)
            respuesta = Utilitario.FiltrarArrayPorPropiedadSegunRangoFecha(respuesta, 
                'FechaAcceso', fechaInicio.toISOString(), fechaFin.toISOString())
            var datosGrafico = listadoArea.map((area) => {
                return {
                    CodigoArea: area.CodigoArea,
                    DescripcionArea: area.DescripcionSede + ' - ' + area.DescripcionArea,
                    Cantidad: respuesta.filter((bitacora) => 
                        bitacora.CodigoArea === area.CodigoArea &&
                        bitacora.CodigoResultadoAcceso === Constante.RESULTADO_ACCESO_CONCEDIDO).length,
                    Personal: null
                }
            })
            datosGrafico = Utilitario.OrdenarArraySegunPropiedad(datosGrafico, 'Cantidad', false)
            SetRegistrosTop(Utilitario.TomarRegistrosDeArraySegunLimite(datosGrafico, 10))
        }, (codigoExcepcion) => {
            SetRegistrosTop(null)
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

    const AbrirModalDatos = (external) => {
        SetAreaSeleccionada(external.payload.CodigoArea)
        ServicioDashboard.ObtenerListadoBitacoraAccionEquipoBiometrico((respuesta) => {
            respuesta = Utilitario.FiltrarArrayPorPropiedad(respuesta, 
                'CodigoArea', external.payload.CodigoArea)
            respuesta = Utilitario.FiltrarArrayPorPropiedadSegunRangoFecha(respuesta, 
                'FechaAcceso', fechaInicio.toISOString(), fechaFin.toISOString())
            var datos = listadoPersonal.map((personal) => {
                return {
                    Descripcion: personal.NombrePersonalEmpresa + ' ' + 
                        personal.ApellidoPersonalEmpresa + ' (' + personal.DNIPersonalEmpresa + ')',
                    Cantidad: respuesta.filter((bitacora) => 
                        bitacora.CodigoPersonalEmpresa === personal.CodigoPersonalEmpresa &&
                         bitacora.CodigoResultadoAcceso === Constante.RESULTADO_ACCESO_CONCEDIDO).length
                }
            })
            datos = Utilitario.OrdenarArraySegunPropiedad(datos, 'Cantidad', false)
            datos = Utilitario.TomarRegistrosDeArraySegunLimite(datos, 10)
            var registros = registrosTop.map((item) => {
                if (item.CodigoArea === external.payload.CodigoArea)
                    item.Personal = datos
                return item
            })
            SetRegistrosTop(registros)
        }, (codigoExcepcion) => {
            CerrarSesionUsuario(codigoExcepcion)
        })
        SetAbrirModal(true)
    }

    return(
        <Card className={ claseEstilo.principal }>
            <CardContent>
                <Typography className={ claseEstilo.titulo }>
                    Top 10 de Áreas con más Accesos
                </Typography>
                <Typography className={ claseEstilo.subtitulo }>
                    ÚLTIMOS 6 MESES
                </Typography><br/>
                <div 
                    style={{ 
                        overflowY: 'scroll',
                        width: 'auto',
                        height: 'auto',
                        alignContent: 'center'
                    }}>
                    {
                        registrosTop === null
                            ?   <CircularProgress />
                            :   <PieChart width={ 330 } height={ 233 }>
                                    <Pie 
                                        data={ registrosTop } 
                                        dataKey="Cantidad" 
                                        nameKey="DescripcionArea" 
                                        cx="50%" 
                                        cy="50%" 
                                        innerRadius={ 30 }
                                        fill="#54CD86"
                                        paddingAngle={ 5 }
                                        label
                                        isAnimationActive={ false }
                                        onClick={ AbrirModalDatos }>
                                        {
                                            registrosTop.map((a, index) => 
                                                <Cell 
                                                    key={ index } 
                                                    fill={ colores[index] }/>)
                                        }
                                    </Pie>
                                </PieChart>
                    }
                </div>
                <Modal 
                    AbrirModal={ abrirModal } 
                    SetAbrirModal={ SetAbrirModal }
                    AreaSeleccionada={ registrosTop === null 
                        ? undefined 
                        : registrosTop.filter((item) => 
                            item.CodigoArea === areaSeleccionada)[0] }/>
            </CardContent>
        </Card>
    )
}

const Modal = (props) => {
    const claseEstilo = estilos()

    const Cerrar = () => {
        props.SetAbrirModal(false)
    }
    return(
        <Dialog 
            aria-labelledby='tooltip' 
            open={ props.AbrirModal }
            fullWidth={ true }
            maxWidth={ 'xs' }>
            <TituloModal 
                id='tooltip' 
                onClose={ Cerrar }>
            </TituloModal>
            <ContenidoModal className={ claseEstilo.modal }>
                <Typography
                    className={ claseEstilo.titulo }>
                    {
                        props.AreaSeleccionada === undefined
                            ? ''
                            : props.AreaSeleccionada.DescripcionArea
                    }
                </Typography>
                <Typography
                    className={ claseEstilo.subtitulo }>
                    Top 10 de Accesos (Últimos 6 meses)
                </Typography>
                <List>
                    {
                        props.AreaSeleccionada === undefined || props.AreaSeleccionada.Personal === null
                            ? <CircularProgress />
                            : props.AreaSeleccionada.Personal
                                .map((item, index) => 
                                    <ListItem key={ index }>
                                        <ListItemText primary={ 
                                            item.Descripcion + ' : ' + item.Cantidad 
                                        }/>
                                    </ListItem>
                                )
                    }
                </List>
            </ContenidoModal>
        </Dialog>
    )
}