import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import * as PersonalEmpresaAction from '../../Accion/PersonalEmpresa'

import * as Utilitario from '../../Utilitario'

import { Grid, makeStyles, FormLabel, FormControlLabel, Checkbox } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        margin: 10
    },
    tituloArea: {
        marginBottom: 10,
        marginLeft: 2
    }
})

export const SubSelectorAreaPersonalEmpresa = (props) => {
    const claseEstilo = estilos()
    const personalEmpresa = useSelector((store) => store.PersonalEmpresa)

    return(
        <Grid 
            container
            className={ claseEstilo.principal }>
            <Grid
                item
                xs={ 12 }
                className={ claseEstilo.tituloArea }>
                <FormLabel 
                    component="legend" 
                    style={{ fontWeight: 'bold', color: 'white' }}>
                    Áreas
                </FormLabel>
            </Grid>
            {
                personalEmpresa.Sedes
                    .find((sede) => sede.CodigoSede === props.CodigoSede)
                        .Areas.map((area) => 
                        <FilaSeleccionArea 
                            key={ area.CodigoArea }
                            { ...area }/>)
            }
        </Grid>
    )
}

const FilaSeleccionArea = (props) => {
    const dispatch = useDispatch()
    const SeleccionarArea = (e, codigoSede, codigoArea) => {
        dispatch(PersonalEmpresaAction.SetListadoAreaEmpresaSeleccionado(
            codigoSede, codigoArea, e.target.checked
        ))
    }
    return(
        <div>
            <FormControlLabel
                control={
                    <Checkbox 
                        name='selectorArea'
                        checked={ props.Seleccionado }
                        onChange={ (e) => SeleccionarArea(e, props.CodigoSede, props.CodigoArea) }/>
                }
                label={ props.DescripcionArea }/>
        </div>
    )
}