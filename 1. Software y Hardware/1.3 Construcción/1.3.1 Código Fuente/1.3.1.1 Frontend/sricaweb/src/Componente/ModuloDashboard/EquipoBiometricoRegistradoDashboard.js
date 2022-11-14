import React, { useEffect, useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import * as GeneralAction from '../../Accion/General'
import * as DashboardAction from '../../Accion/Dashboard'
import * as Constante from '../../Constante'

import * as ServicioDashboard from '../../Servicio/Dashboard'

import { makeStyles, Card, CardContent, Typography, List, ListItem, ListItemText, CircularProgress } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        margin: 18,
        color: '#48525e'
    },
    tamañoLista: {
        height: 485,
        overflow: 'auto',
        position: 'relative'
    },
    titulo: {
        fontSize: 15,
        fontWeight: 'bold'
    },
    subtitulo: {
        fontSize: 12,
        color: '#8e97a6'
    }
})

export const EquipoBiometricoRegistradoDashboard = () => {
    const claseEstilo = estilos()
    const [equiposBiometricos, SetEquiposBiometricos] = useState(null)
    const dispatch = useDispatch()

    useEffect(() => {
        ServicioDashboard.ObtenerListadoSede(
            (respuesta) => {
                var resultado = []
                var sedesActivas = respuesta.filter((sede) => sede.IndicadorEstado &&
                    !sede.IndicadorRegistroParaSinAsignacion);
                dispatch(DashboardAction.SetDashboardListadoSede(sedesActivas))
                sedesActivas.map((sede) => {
                    var areasActivas = sede.Areas.filter((area) => area.IndicadorEstado &&
                        !area.IndicadorRegistroParaSinAsignacion)
                    sede.Areas = areasActivas    
                })
                sedesActivas.map((sede) => {
                    var cantidadEquiposBiometricos = 0
                    sede.Areas.map((area) => {
                        var equiposBiometricosActivos = area.EquiposBiometricos.filter((equipoBiometrico) => 
                            equipoBiometrico.IndicadorEstado)
                        cantidadEquiposBiometricos += equiposBiometricosActivos.length
                    })
                    resultado.push({
                        CodigoSede: sede.CodigoSede,
                        Sede: sede.DescripcionSede,
                        EquiposBiometricos: cantidadEquiposBiometricos
                    })
                })
                resultado = resultado.sort((a, b) => a.EquiposBiometricos > b.EquiposBiometricos ? 1 : -1)
                SetEquiposBiometricos(resultado)
            }, (codigoExcepcion) => {
                dispatch(DashboardAction.SetDashboardListadoSede([]))
                SetEquiposBiometricos([])
                CerrarSesionUsuario(codigoExcepcion)
            })
    }, [])

    const CerrarSesionUsuario = (codigoExcepcion) => {
        if (codigoExcepcion === Constante.CODIGO_EXCEPCION_ADVERTENCIA_SIMPLE_LOGOUT_USUARIO){
            dispatch(GeneralAction.SetDatosUsuarioNoLogueado())
            dispatch(GeneralAction.OcultarEncabezado())
            sessionStorage.removeItem(Constante.VARIABLE_LOCAL_STORAGE)
        }
    }

    return(
        <Card className={ claseEstilo.principal }>
            <CardContent>
                <Typography className={ claseEstilo.titulo }>
                    Equipos Biométricos Registrados
                </Typography>
                <Typography className={ claseEstilo.subtitulo }>
                    HOY
                </Typography>
                <div>
                    <List className={ claseEstilo.tamañoLista }>
                        { 
                            equiposBiometricos === null
                                ? <CircularProgress />
                                : equiposBiometricos.map((valor) =>
                                    <ListItem key={ valor.CodigoSede }>
                                        <ListItemText
                                            primary={ valor.Sede }
                                            secondary={ valor.EquiposBiometricos + ' equipos registrados' }/>
                                    </ListItem>
                                )
                        }
                    </List>
                </div>
            </CardContent>
        </Card>
    )
}