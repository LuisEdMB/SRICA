import React, { useEffect } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import jwt_decode from 'jwt-decode'
import { BrowserRouter, Route, Switch } from 'react-router-dom'
import * as GeneralAction from '../Accion/General'
import * as Constante from '../Constante'

import * as Utilitario from '../Utilitario'

import { PiePagina } from './ComponenteGeneral/PiePagina'
import { Encabezado } from './ComponenteGeneral/Encabezado'
import { InicioSesion } from './ModuloSeguridad/InicioSesion'
import { CambioContraseñaOlvidada } from './ModuloSeguridad/CambioContraseñaOlvidada'
import { CambioDatoPorDefecto } from './ModuloSeguridad/CambioDatoPorDefecto'
import { Dashboard } from './ModuloDashboard/Dashboard'
import { MiPerfil } from './ModuloPerfilUsuario/MiPerfil'
import { Usuario } from './ModuloUsuario/Usuario'
import { SedeEmpresa } from './ModuloSedeEmpresa/SedeEmpresa'
import { AreaEmpresa } from './ModuloAreaEmpresa/AreaEmpresa'
import { Nomenclatura } from './ModuloEquipoBiometrico/Nomenclatura'
import { EquipoBiometrico } from './ModuloEquipoBiometrico/EquipoBiometrico'
import { PersonalEmpresa } from './ModuloPersonalEmpresa/PersonalEmpresa'
import { ReporteSistema } from './ModuloReporte/ReporteSistema'
import { ReporteAccionSistema } from './ModuloReporte/ReporteAccionSistema'
import { ReporteAccionEquipoBiometrico } from './ModuloReporte/ReporteAccionEquipoBiometrico'
import { NotFound404 } from './ComponenteGeneral/NotFound404'
import { BackdropLoad } from './ComponenteGeneral/Backdrop'

import Grid from '@material-ui/core/Grid'

export const Index = () => {
    const general = useSelector(store => store.General)
    const generalUsuarioLogueado = useSelector(store => store.GeneralUsuarioLogueado)
    const dispatch = useDispatch()
    useEffect(() => {
        var usuario = JSON.parse(sessionStorage.getItem(Constante.VARIABLE_LOCAL_STORAGE))
        if(usuario === null || usuario.Usuario === ''){
            dispatch(GeneralAction.SetDatosUsuarioNoLogueado())
            dispatch(GeneralAction.OcultarEncabezado())
        }
        else{
            dispatch(GeneralAction.SetDatosUsuarioLogueado(usuario))
            dispatch(GeneralAction.MostrarEncabezado())
        }
        const url = window.location.search;
        const parametro = new URLSearchParams(url);
        const parametroId = parametro.get('id')
        if (parametroId !== null)
            if (parametroId !== ''){
                try{
                    var tokenDecodificado = jwt_decode(parametroId)
                    var horaActual = Utilitario.QuitarMilisegundosAlTiempo(new Date().getTime());
                    if (tokenDecodificado.exp >= horaActual){
                        ColocarValoresSegunTokenObtenido(parametroId, tokenDecodificado, true)
                        return
                    }
                }
                catch{
                    Utilitario.RemoverParametroDeURL()
                    ColocarValoresSegunTokenObtenido('', '', false)
                }
            }
        Utilitario.RemoverParametroDeURL()
        ColocarValoresSegunTokenObtenido('', '', false)
    }, [])

    const ColocarValoresSegunTokenObtenido = (token, tokenDecodificado, valor) => {
        dispatch(GeneralAction.SetTokenCambioContrasenaOlvidada(token))
        dispatch(GeneralAction.SetTokenDecodificadoCambioContrasenaOlvidada(tokenDecodificado))
        dispatch(GeneralAction.MostrarCambioContrasenaOlvidada(valor))
    }

    return(
        <BrowserRouter>
            <Grid
                container
                direction="column"
                alignItems="center"
                justify="center"
                >
                <Grid 
                    item={ true } 
                    xs={ 12 }>
                    <Encabezado/>
                    <div style={{ marginBottom: 95 }}>
                    {
                        generalUsuarioLogueado === null || 
                        generalUsuarioLogueado.Usuario === ''
                            ?   (!general.EsCambioContrasenaOlvidada 
                                    ? <InicioSesion/> 
                                    : <CambioContraseñaOlvidada/>)
                            :   generalUsuarioLogueado.EsCorreoElectronicoPorDefecto ||
                                generalUsuarioLogueado.EsContrasenaPorDefecto
                                    ?   <CambioDatoPorDefecto/>
                                    :   generalUsuarioLogueado.EsAdministrador
                                        ?   <Switch>
                                                <Route exact path='/' component={ Dashboard }/>
                                                <Route exact path='/perfil' component={ MiPerfil }/>
                                                
                                                <Route exact path='/usuario' component={ Usuario }/>
                                                <Route exact path='/sede' component={ SedeEmpresa }/>
                                                <Route exact path='/area' component={ AreaEmpresa }/>
                                                <Route exact path='/nomenclatura' component={ Nomenclatura }/>
                                                <Route exact path='/equipo-biometrico' component={ EquipoBiometrico }/>
                                                <Route exact path='/personal-empresa' component={ PersonalEmpresa }/>
                                                <Route exact path='/reporte-sistema' component={ ReporteSistema }/>
                                                <Route exact path='/reporte-accion-sistema' component={ ReporteAccionSistema }/>
                                                <Route exact path='/reporte-accion-equipo-biometrico' component={ ReporteAccionEquipoBiometrico }/>
                                                
                                                <Route component={ NotFound404 }/>
                                            </Switch>
                                        :   <Switch >
                                                <Route exact path='/' component={ Dashboard }/>
                                                <Route exact path='/perfil' component={ MiPerfil }/>
                                                
                                                <Route exact path='/equipo-biometrico' component={ EquipoBiometrico }/>
                                                <Route exact path='/personal-empresa' component={ PersonalEmpresa }/>
                                                <Route exact path='/reporte-sistema' component={ ReporteSistema }/>
                                                <Route exact path='/reporte-accion-equipo-biometrico' component={ ReporteAccionEquipoBiometrico }/>
                                                
                                                <Route component={ NotFound404 }/>
                                            </Switch>
                    }
                    </div>                  
                    <PiePagina/>
                    <BackdropLoad/>
                </Grid>
            </Grid>
        </BrowserRouter>
    )
}