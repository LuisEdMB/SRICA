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

export const TopPersonalAccesoDashboard = () => {
    const claseEstilo = estilos()
    const [registrosTop, SetRegistrosTop] = useState(null)
    const [listadoArea, SetListadoArea] = useState([])
    const [colores, SetColores] = useState([])
    const [abrirModal, SetAbrirModal] = useState(false)
    const [personalSeleccionado, SetPersonalSeleccionado] = useState('')
    var listadoPersonal = []
    var fechaFin = new Date();
    var fechaInicio = Utilitario.DefinirFechaSegunCantidadMes(fechaFin, -6)
    const dispatch = useDispatch()

    useEffect(() => {
        ObtenerListadoAreaEmpresa()
    }, [])

    const ObtenerListadoAreaEmpresa = () => {
        ServicioDashboard.ObtenerListadoAreaEmpresa((respuesta) => {
            respuesta = respuesta.filter((area) => area.IndicadorEstado &&
                !area.IndicadorRegistroParaSinAsignacion)
            SetListadoArea(respuesta)
            ObtenerListadoPersonalEmpresa()
        }, (codigoExcepcion) => {
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const ObtenerListadoPersonalEmpresa = () => {
        ServicioDashboard.ObtenerListadoPersonalEmpresa((respuesta) => {
            respuesta = respuesta.filter((personal) => personal.IndicadorEstado)
            listadoPersonal = respuesta
            SetColores(Utilitario.GenerarColoresSegunCantidad(respuesta.length))
            ObtenerListadoBitacoraAccionEquipoBiometricoDelPersonal()
        }, (codigoExcepcion) => {
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const ObtenerListadoBitacoraAccionEquipoBiometricoDelPersonal = () => {
        ServicioDashboard.ObtenerListadoBitacoraAccionEquipoBiometrico((respuesta) => {
            var codigosPersonal = listadoPersonal.map((personal) => personal.CodigoPersonalEmpresa)
            respuesta = Utilitario.FiltrarArrayPorPropiedadSegunOtroArray(respuesta, 
                'CodigoPersonalEmpresa', codigosPersonal)
            respuesta = Utilitario.FiltrarArrayPorPropiedadSegunRangoFecha(respuesta, 
                'FechaAcceso', fechaInicio.toISOString(), fechaFin.toISOString())
            var datosGrafico = listadoPersonal.map((personal) => {
                return {
                    CodigoPersonalEmpresa: personal.CodigoPersonalEmpresa,
                    NombreApellidoPersonal: personal.NombrePersonalEmpresa + ' ' + 
                        personal.ApellidoPersonalEmpresa + ' (' + personal.DNIPersonalEmpresa + ')',
                    Cantidad: respuesta.filter((bitacora) => 
                        bitacora.CodigoPersonalEmpresa === personal.CodigoPersonalEmpresa &&
                        bitacora.CodigoResultadoAcceso === Constante.RESULTADO_ACCESO_CONCEDIDO).length,
                    Areas: null
                }
            })
            datosGrafico = Utilitario.OrdenarArraySegunPropiedad(datosGrafico, 'Cantidad', false)
            SetRegistrosTop(Utilitario.TomarRegistrosDeArraySegunLimite(datosGrafico, 10))
        }, (codigoExcepcion) => {
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
        SetPersonalSeleccionado(external.payload.CodigoPersonalEmpresa)
        ServicioDashboard.ObtenerListadoBitacoraAccionEquipoBiometrico((respuesta) => {
            respuesta = Utilitario.FiltrarArrayPorPropiedad(respuesta, 
                'CodigoPersonalEmpresa', external.payload.CodigoPersonalEmpresa)
            respuesta = Utilitario.FiltrarArrayPorPropiedadSegunRangoFecha(respuesta, 
                'FechaAcceso', fechaInicio.toISOString(), fechaFin.toISOString())
            var datos = listadoArea.map((area) => {
                return {
                    Descripcion: area.DescripcionSede + ' - ' + area.DescripcionArea,
                    Cantidad: respuesta.filter((bitacora) => 
                        bitacora.CodigoArea === area.CodigoArea &&
                        bitacora.CodigoResultadoAcceso === Constante.RESULTADO_ACCESO_CONCEDIDO).length
                }
            })
            datos = Utilitario.OrdenarArraySegunPropiedad(datos, 'Cantidad', false)
            datos = Utilitario.TomarRegistrosDeArraySegunLimite(datos, 10)
            var registros = registrosTop.map((item) => {
                if (item.CodigoPersonalEmpresa === external.payload.CodigoPersonalEmpresa)
                    item.Areas = datos
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
                    Top 10 del Personal con más Accesos
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
                                        nameKey="NombreApellidoPersonal" 
                                        cx="50%" 
                                        cy="50%"
                                        innerRadius={ 30 }
                                        fill="#54CD86"
                                        isAnimationActive={ false }
                                        paddingAngle={ 5 }
                                        label
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
                    PersonalSeleccionado={ registrosTop === null 
                        ? undefined 
                        : registrosTop.filter((item) => 
                            item.CodigoPersonalEmpresa === personalSeleccionado)[0] }/>
            </CardContent>
        </Card>
    )
}

const Modal  = (props) => {
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
                        props.PersonalSeleccionado === undefined
                            ? ''
                            : props.PersonalSeleccionado.NombreApellidoPersonal
                    }
                </Typography>
                <Typography
                    className={ claseEstilo.subtitulo }>
                    Top 10 de Accesos (Últimos 6 meses)
                </Typography>
                <List>
                    {
                        props.PersonalSeleccionado === undefined || props.PersonalSeleccionado.Areas === null
                            ? <CircularProgress />
                            : props.PersonalSeleccionado.Areas
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