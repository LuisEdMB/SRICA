import React, { useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { useForm } from 'react-hook-form'
import * as yup from 'yup'
import * as GeneralAction from '../../Accion/General'
import * as EquipoBiometricoAction from '../../Accion/EquipoBiometrico'
import * as Constante from '../../Constante'

import * as AlertaSwal from '../ComponenteGeneral/Mensaje'

import * as ServicioEquipoBiometrico from '../../Servicio/EquipoBiometrico'

import { TituloModal } from '../ComponenteGeneral/TituloModal'
import { ContenidoModal } from '../ComponenteGeneral/ContenidoModal'
import { SeccionAccionModal } from '../ComponenteGeneral/SeccionAccionModal'
import { BotonGuardarCambios } from '../ComponenteGeneral/BotonGuardarCambios'

import { Dialog, Grid, FormControl, InputLabel, Input, makeStyles, Select, MenuItem, TextField } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        color: '#48525e'
    },
    campo: {
        width: '100%'
    },
    selector: {
        width: '100%'
    },
    colorIcon: {
        color: '#48525e'
    },
    colorError: {
        fontSize: 11,
        fontWeight: 'bold',
        color: 'red',
        textAlign: 'left'
    }
})

export const FormularioEquipoBiometrico = () => {
    const claseEstilo = estilos()
    const equipoBiometrico = useSelector((store) => store.EquipoBiometrico)
    const equipoBiometricoFormulario = useSelector((store) => store.EquipoBiometricoFormulario)
    const [verificadorCargaAreas, SetVerificadorCargaAreas] = useState(false)
    const [areas, SetAreas] = useState([])
    const equipoBiometricoFormularioAnterior = useSelector((store) => 
        store.EquipoBiometricoFormularioAnterior)
    const dispatch = useDispatch()

    const errores = yup.object().shape({
        CodigoNomenclaturaSelector: yup.string().trim().required('Debe seleccionar una nomenclatura ' + 
            'para el equipo biométrico.'),
        NombreEquipoBiometrico: yup.string().trim().required('El campo "Nombre de equipo" no debe ' + 
            'estar vacío.'),
        DireccionRedEquipoBiometrico: yup.string().trim().required('El campo "Dirección de red" ' + 
            'no debe estar vacío.'),
        CodigoSedeSelector: yup.string().trim().required('Debe seleccionar una sede para el ' + 
            'equipo biométrico.'),
        CodigoAreaSelector: yup.string().trim().required('Debe seleccionar un área para el ' + 
            'equipo biométrico.')
    })
    const { register, handleSubmit, clearError, errors } = useForm({
        mode: 'onBlur',
        validationSchema: errores
    })

    const CambiarValorCampo = (e, selector, textSelector) => {
        const { name, value } = e.target
        dispatch(EquipoBiometricoAction.SetFormularioEquipoBiometricoPorCampo(name, value))
        if (selector !== undefined)
            dispatch(EquipoBiometricoAction.SetFormularioEquipoBiometricoPorCampo(selector, textSelector))
    }

    const ObtenerListadoAreaSegunSede = (e, p) => {
        dispatch(GeneralAction.AbrirBackdrop())
        const { name, value } = e.target
        CambiarValorCampo(e, 'DescripcionSede', p.props.children)
        clearError('CodigoSedeSelector')
        ServicioEquipoBiometrico.ObtenerListadoAreaSegunSede(value, (respuesta) => {
            dispatch(EquipoBiometricoAction.SetFormularioEquipoBiometricoPorCampo('CodigoArea', ''))
            SetVerificadorCargaAreas(true)
            SetAreas(respuesta)
            dispatch(GeneralAction.CerrarBackdrop())
        }, (codigoExcepcion) => {
            dispatch(GeneralAction.CerrarBackdrop())
            CerrarSesionUsuario(codigoExcepcion)
        })
    }

    const CerrarFormularioEquipoBiometrico = () => {
        dispatch(EquipoBiometricoAction.CerrarFormularioEquipoBiometrico())
        SetVerificadorCargaAreas(false)
    }

    const GuardarCambios = () => {
        dispatch(GeneralAction.AbrirBackdrop())
        if (equipoBiometricoFormulario.EstadoObjeto === Constante.ESTADO_OBJETO.Modificado){
            ServicioEquipoBiometrico.ModificarEquipoBiometrico(equipoBiometricoFormulario, 
                equipoBiometricoFormularioAnterior, () => {
                dispatch(GeneralAction.CerrarBackdrop())
                AlertaSwal.MensajeExitoPorDefecto(() => {
                    CerrarFormularioEquipoBiometrico()
                })
            }, (codigoExcepcion) => {
                dispatch(GeneralAction.CerrarBackdrop())
                CerrarSesionUsuario(codigoExcepcion)
            })
        }
    }

    const CerrarSesionUsuario = (codigoExcepcion) => {
        if (codigoExcepcion === Constante.CODIGO_EXCEPCION_ADVERTENCIA_SIMPLE_LOGOUT_USUARIO){
            dispatch(GeneralAction.SetDatosUsuarioNoLogueado())
            dispatch(GeneralAction.OcultarEncabezado())
            sessionStorage.removeItem(Constante.VARIABLE_LOCAL_STORAGE)
        }
    }

    return(
        <Dialog 
            aria-labelledby='formularioEquipoBiometrico' 
            open={ equipoBiometricoFormulario.ModalFormulario }
            fullWidth={ true }
            maxWidth={ 'xs' }>
            <TituloModal 
                id='formularioEquipoBiometrico' 
                onClose={ CerrarFormularioEquipoBiometrico }>
                Datos del Equipo Biométrico
            </TituloModal>
            <ContenidoModal dividers className={ claseEstilo.principal }>
                <Grid 
                    container 
                    spacing={ 2 } 
                    alignItems="flex-end">
                    <Grid 
                        item xs={ 12 } 
                        sm={ 6 }>
                        <FormControl
                            className={ claseEstilo.selector }>
                            <InputLabel 
                                id="selectorNomenclatura"
                                error={ errors.CodigoNomenclaturaSelector }>
                                Nomenclatura
                            </InputLabel>
                            <Select
                                labelId="selectorNomenclatura"
                                error={ errors.CodigoNomenclaturaSelector }
                                name='CodigoNomenclatura'
                                value={ equipoBiometricoFormulario.CodigoNomenclatura }
                                onChange={ (e, p) => {
                                    CambiarValorCampo(e, 'DescripcionNomenclatura', p.props.children)
                                    clearError('CodigoNomenclaturaSelector')
                                } }>
                                {
                                    equipoBiometrico.Nomenclaturas
                                        .filter((nomenclatura) => 
                                            !nomenclatura.IndicadorRegistroParaSinAsignacion)
                                        .map((nomenclatura) => 
                                            <MenuItem 
                                                key={ nomenclatura.CodigoNomenclatura }
                                                value={ nomenclatura.CodigoNomenclatura }>
                                                { nomenclatura.DescripcionNomenclatura }
                                            </MenuItem>
                                        )
                                }
                            </Select>
                            <TextField 
                                name='CodigoNomenclaturaSelector'
                                type='hidden'
                                value={ equipoBiometricoFormulario.CodigoNomenclatura }
                                inputRef={ register }/>
                        </FormControl>
                    </Grid>
                </Grid>
                {
                    errors.CodigoNomenclaturaSelector
                        ?   <Grid 
                                container
                                alignItems='flex-end'>
                                <Grid 
                                    item xs={ 12 }>
                                    <p className={ claseEstilo.colorError }>
                                        { errors.CodigoNomenclaturaSelector.message }
                                    </p> 
                                </Grid>
                            </Grid>
                        : <br/>
                }
                <Grid 
                    container 
                    spacing={ 2 } 
                    alignItems="flex-end">
                    <Grid 
                        item xs={ 12 }>
                        <FormControl 
                            className = { claseEstilo.campo }>
                            <TextField 
                                error = { errors.NombreEquipoBiometrico } 
                                name='NombreEquipoBiometrico'
                                label='Nombre de equipo'
                                type='text'
                                value={ equipoBiometricoFormulario.NombreEquipoBiometrico }
                                onChange={ CambiarValorCampo }
                                inputRef={ register }/>
                        </FormControl>
                    </Grid>
                </Grid>
                {
                    errors.NombreEquipoBiometrico
                        ?   <Grid 
                                container
                                alignItems='flex-end'>
                                <Grid 
                                    item xs={ 12 }>
                                    <p className={ claseEstilo.colorError }>
                                        { errors.NombreEquipoBiometrico.message }
                                    </p> 
                                </Grid>
                            </Grid>
                        : <br/>
                }
                <Grid 
                    container 
                    spacing={ 2 } 
                    alignItems="flex-end">
                    <Grid 
                        item xs={ 12 }>
                        <FormControl 
                            className = { claseEstilo.campo }>
                            <TextField 
                                error = { errors.DireccionRedEquipoBiometrico } 
                                name='DireccionRedEquipoBiometrico'
                                label='Dirección de red'
                                type='text'
                                value={ equipoBiometricoFormulario.DireccionRedEquipoBiometrico }
                                onChange={ CambiarValorCampo }
                                inputRef={ register }
                                placeholder='xxx.xxx.xxx.xxx'/>
                        </FormControl>
                    </Grid>
                </Grid>
                {
                    errors.DireccionRedEquipoBiometrico
                        ?   <Grid 
                                container
                                alignItems='flex-end'>
                                <Grid 
                                    item xs={ 12 }>
                                    <p className={ claseEstilo.colorError }>
                                        { errors.DireccionRedEquipoBiometrico.message }
                                    </p> 
                                </Grid>
                            </Grid>
                        : <br/>
                }
                <Grid 
                    container 
                    spacing={ 2 } 
                    alignItems="flex-end">
                    <Grid 
                        item 
                        xs={ 12 } 
                        sm={ 6 }>
                        <FormControl
                            className={ claseEstilo.selector }>
                            <InputLabel 
                                id="selectorSede"
                                error={ errors.CodigoSedeSelector }>
                                Sede
                            </InputLabel>
                            <Select
                                labelId="selectorSede"
                                error={ errors.CodigoSedeSelector }
                                name='CodigoSede'
                                value={ equipoBiometricoFormulario.CodigoSede }
                                onChange={ (e, p) => ObtenerListadoAreaSegunSede(e, p) }>
                                {
                                    equipoBiometrico.Sedes
                                        .filter((sede) => 
                                            !sede.IndicadorRegistroParaSinAsignacion)
                                        .map((sede) => 
                                            <MenuItem 
                                                key={ sede.CodigoSede }
                                                value={ sede.CodigoSede }>
                                                { sede.DescripcionSede }
                                            </MenuItem>
                                        )
                                }
                            </Select>
                            <TextField 
                                name='CodigoSedeSelector'
                                type='hidden'
                                value={ equipoBiometricoFormulario.CodigoSede }
                                inputRef={ register }/>
                        </FormControl>
                    </Grid>
                </Grid>
                {
                    errors.CodigoSedeSelector
                        ?   <Grid 
                                container
                                alignItems='flex-end'>
                                <Grid 
                                    item xs={ 12 }>
                                    <p className={ claseEstilo.colorError }>
                                        { errors.CodigoSedeSelector.message }
                                    </p> 
                                </Grid>
                            </Grid>
                        : <br/>
                }
                <Grid 
                    container 
                    spacing={ 2 } 
                    alignItems="flex-end">
                    <Grid 
                        item xs={ 12 } sm={ 6 }>
                        <FormControl
                            className={ claseEstilo.selector }>
                            <InputLabel 
                                id="selectorArea"
                                error={ errors.CodigoAreaSelector }>
                                Área
                            </InputLabel>
                            <Select
                                labelId="selectorArea"
                                error={ errors.CodigoAreaSelector }
                                name='CodigoArea'
                                value={ equipoBiometricoFormulario.CodigoArea }
                                onChange={ (e, p) => {
                                    CambiarValorCampo(e, 'DescripcionArea', p.props.children)
                                    clearError('CodigoAreaSelector')
                                } }>
                                {
                                    !verificadorCargaAreas
                                        ? equipoBiometrico.Sedes.filter((sede) => 
                                                sede.CodigoSede === equipoBiometricoFormulario.CodigoSede)
                                            .map((sede) => sede.Areas
                                                .filter((area) => 
                                                    area.IndicadorEstado && 
                                                    !area.IndicadorRegistroParaSinAsignacion)
                                                .map((area) => 
                                                    <MenuItem 
                                                        key={ area.CodigoArea }
                                                        value={ area.CodigoArea }>
                                                        { area.DescripcionArea }
                                                    </MenuItem>
                                                )
                                            )
                                        : areas
                                            .filter((area) => 
                                                area.IndicadorEstado && 
                                                !area.IndicadorRegistroParaSinAsignacion)
                                            .map((area) => 
                                                <MenuItem 
                                                    key={ area.CodigoArea }
                                                    value={ area.CodigoArea }>
                                                    { area.DescripcionArea }
                                                </MenuItem>
                                            )
                                }
                            </Select>
                            <TextField 
                                name='CodigoAreaSelector'
                                type='hidden'
                                value={ equipoBiometricoFormulario.CodigoArea }
                                inputRef={ register }/>
                        </FormControl>
                    </Grid>
                </Grid>
                {
                    errors.CodigoAreaSelector
                        ?   <Grid 
                                container
                                alignItems='flex-end'>
                                <Grid 
                                    item xs={ 12 }>
                                    <p className={ claseEstilo.colorError }>
                                        { errors.CodigoAreaSelector.message }
                                    </p> 
                                </Grid>
                            </Grid>
                        : null
                }
            </ContenidoModal>
            <SeccionAccionModal>
                <BotonGuardarCambios
                    onClick={ handleSubmit(() => AlertaSwal.MensajeConfirmacionPorDefecto(GuardarCambios)) }/>
            </SeccionAccionModal>
        </Dialog>
    )
}