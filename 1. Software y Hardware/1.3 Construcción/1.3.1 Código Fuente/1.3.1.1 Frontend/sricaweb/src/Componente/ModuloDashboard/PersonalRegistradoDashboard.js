import React, { useEffect, useState } from 'react'
import { useDispatch } from 'react-redux'
import * as GeneralAction from '../../Accion/General'
import * as Constante from '../../Constante'

import * as ServicioDashboard from '../../Servicio/Dashboard'

import { makeStyles, Card, CardContent, Typography, CircularProgress } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        height: 230,
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
    textoCantidadRegistro: {
        fontSize: 100,
        fontWeight: 'bold',
        textAlign: 'center'
    }
})

export const PersonalRegistradoDashboard = () => {
    const claseEstilo = estilos()
    const [personalRegistrado, SetPersonalRegistrado] = useState('')
    const dispatch = useDispatch()

    useEffect(() => {
        ServicioDashboard.ObtenerListadoPersonalRegistrado(
            (respuesta) => {
                var personalActivos = respuesta.filter((personalEmpresa) => personalEmpresa.IndicadorEstado)
                SetPersonalRegistrado(personalActivos.length)
            }, (codigoExcepcion) => {
                SetPersonalRegistrado('0')
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
                    Personal Registrado
                </Typography>
                <Typography className={ claseEstilo.subtitulo }>
                    HOY
                </Typography>
                <Typography className={ claseEstilo.textoCantidadRegistro }>
                    { 
                        personalRegistrado === ''
                            ? <CircularProgress />
                            : personalRegistrado
                    }
                </Typography>
            </CardContent>
        </Card>
    )
}