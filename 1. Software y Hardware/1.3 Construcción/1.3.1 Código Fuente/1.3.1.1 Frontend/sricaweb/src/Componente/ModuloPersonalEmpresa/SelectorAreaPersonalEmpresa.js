import React, { useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import * as GeneralAction from '../../Accion/General'
import * as PersonalEmpresaAction from '../../Accion/PersonalEmpresa'
import * as Constante from '../../Constante'

import * as ServicioPersonalEmpresa from '../../Servicio/PersonalEmpresa'

import * as Utilitario from '../../Utilitario'

import { TituloModal } from '../ComponenteGeneral/TituloModal'
import { ContenidoModal } from '../ComponenteGeneral/ContenidoModal'
import { SubSelectorAreaPersonalEmpresa } from './SubSelectorAreaPersonalEmpresa'

import { Dialog, Grid, makeStyles, FormLabel, Tooltip, Typography, Divider, IconButton, ClickAwayListener } from '@material-ui/core'
import ChevronRightOutlinedIcon from '@material-ui/icons/ChevronRightOutlined'

const estilos = makeStyles({
    principal: {
        color: '#48525e',
        flexGrow: 1
    },
    tituloSede: {
        marginBottom: 10
    },
    textoSede: {
        fontSize:14,
        marginTop: 14
    },
    tamañoFilaSede: {
        width: '98%'
    },
    derechaComponente: {
        textAlign: 'right'
    }
})

export const SelectorAreaPersonalEmpresa = () => {
    const claseEstilo = estilos()
    const personalEmpresaFormulario = useSelector((store) => store.PersonalEmpresaFormulario)
    const dispatch = useDispatch()

    const CerrarSelectorArea = () => {
        dispatch(PersonalEmpresaAction.CerrarFormularioSelectorArea())
    }

    return(
        <Dialog 
            aria-labelledby='selectorAreaPersonalEmpresa' 
            open={ personalEmpresaFormulario.ModalSelectorArea }
            fullWidth={ true }
            maxWidth={ 'xs' }>
            <TituloModal 
                id='selectorAreaPersonalEmpresa' 
                onClose={ CerrarSelectorArea }>
            </TituloModal>
            <ContenidoModal className={ claseEstilo.principal }>
                <Grid 
                    container>
                    <Grid
                        item
                        xs={ 12 }
                        className={ claseEstilo.tituloSede }>
                        <FormLabel 
                            component="legend" 
                            style={{ fontWeight: 'bold' }}>
                            Sedes
                        </FormLabel>
                    </Grid>
                    <FilaSede/>
                </Grid>
            </ContenidoModal>
        </Dialog>
    )
}

const FilaSede = () => {
    const claseEstilo = estilos()
    const personalEmpresa = useSelector((store) => store.PersonalEmpresa)
    const personalEmpresaFormulario = useSelector((store) => store.PersonalEmpresaFormulario)
    const dispatch = useDispatch()

    const AbrirSubSelectorArea = (codigoSede) => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioPersonalEmpresa.ObtenerListadoAreaSegunSede(codigoSede, (respuesta) => {
            respuesta = respuesta.filter((area) => area.IndicadorEstado && 
                !area.IndicadorRegistroParaSinAsignacion)
            ObtenerListadoSeleccionAreaPersonalEmpresa(codigoSede, respuesta)
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const ObtenerListadoSeleccionAreaPersonalEmpresa = (codigoSede, areas) => {
        dispatch(GeneralAction.AbrirBackdrop())
        ServicioPersonalEmpresa.ObtenerPersonalEmpresa(
            personalEmpresaFormulario.CodigoPersonalEmpresa, (respuesta) => {
            respuesta = respuesta.Areas.filter((registro) => registro.IndicadorEstado)
            var resultado = CambiarSeleccionDeAreas(codigoSede, areas, respuesta)
            dispatch(PersonalEmpresaAction.SetListadoSedeEmpresaAreaEmpresa(codigoSede, resultado))
            dispatch(PersonalEmpresaAction.SetListadoAreaEmpresaAbiertoCerrado(codigoSede, true))
            dispatch(GeneralAction.CerrarBackdrop())
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const CambiarSeleccionDeAreas = (codigoSede, areas, areasSeleccionPersonal) => {
        var resultado = areas.map((area) => {
            var areaPersonalEncontrado = Utilitario.EncontrarEnArray(
                    areasSeleccionPersonal, 'CodigoArea', area.CodigoArea)
            if (areaPersonalEncontrado !== undefined){
                area["CodigoPersonalEmpresaXArea"] = areaPersonalEncontrado.CodigoPersonalEmpresaXArea
                area.Seleccionado = areaPersonalEncontrado.Seleccionado
                area.Nuevo = areaPersonalEncontrado.Nuevo
            }
            return area
        })
        var sede = personalEmpresa.Sedes.find((sede) => sede.CodigoSede === codigoSede)
        resultado = resultado.map((area) => {
            area.Seleccionado = Utilitario.EncontrarPropiedadEnArray(
                sede.Areas, 'CodigoArea', area.CodigoArea, 'Seleccionado')
            return area
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

    return(
        <Grid 
            container
            className={ claseEstilo.tamañoFilaSede }>
            {
                personalEmpresa.Sedes
                    .filter((sede) => !sede.IndicadorRegistroParaSinAsignacion)
                    .map((sede) => 
                        <Grid
                            key={ sede.CodigoSede }
                            container>
                            <Grid
                                item
                                xs={ 11 }>
                                <Typography className={ claseEstilo.textoSede }>
                                    { sede.DescripcionSede }
                                </Typography>
                            </Grid>
                            <ClickAwayListener onClickAway={ () => 
                                dispatch(PersonalEmpresaAction.SetListadoAreaEmpresaAbiertoCerrado(sede.CodigoSede, false)) }>
                                <Grid
                                    item
                                    xs={ 1 }
                                    className={ claseEstilo.derechaComponente }>
                                    <Tooltip 
                                        title={ 
                                            <SubSelectorAreaPersonalEmpresa
                                                CodigoSede={ sede.CodigoSede }/> } 
                                        interactive
                                        placement="right"
                                        arrow
                                        open={ sede.Abierto ?? false }
                                        onClose={ () => 
                                            dispatch(PersonalEmpresaAction.SetListadoAreaEmpresaAbiertoCerrado(sede.CodigoSede, false)) }
                                        disableFocusListener>
                                        <Tooltip title='Visualizar Áreas'>
                                            <IconButton onClick={ () => AbrirSubSelectorArea(sede.CodigoSede) }>
                                                <ChevronRightOutlinedIcon/>
                                            </IconButton>
                                        </Tooltip>
                                    </Tooltip>
                                </Grid>
                            </ClickAwayListener>
                            <Grid
                                item
                                xs={ 12 }>
                                <Divider />
                            </Grid>
                        </Grid>
                    )
            }
        </Grid>
    )
}