import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import * as ReporteAction from '../../Accion/Reporte'

import * as Constante from '../../Constante'

import { TituloModal } from '../ComponenteGeneral/TituloModal'
import { ContenidoModal } from '../ComponenteGeneral/ContenidoModal'

import { Dialog, Grid, makeStyles } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        color: '#48525e',
        marginTop: 30
    },
    cuadroImagen: {
        height: '100%',
        width: '100%'
    }
})

export const FotoPersonalEquipoBiometrico = () => {
    const claseEstilo = estilos()
    const reporte = useSelector((store) => store.Reporte)
    const dispatch = useDispatch()

    const CerrarVisualizador = () => {
        dispatch(ReporteAction.CerrarVisualizadorPersonalNoRegistrado())
    }

    return(
        <Dialog 
            aria-labelledby='fotoPersonalEquipoBiometrico' 
            open={ reporte.ModalVisualizadorImagen }
            fullWidth={ true }
            maxWidth={ 'lg' }>
            <TituloModal 
                id='fotoPersonalEquipoBiometrico' 
                onClose={ CerrarVisualizador }>               
            </TituloModal>
            <ContenidoModal dividers className={ claseEstilo.principal }>
                <Grid 
                    container 
                    spacing={ 2 } 
                    alignItems="flex-end">
                    <Grid 
                        item xs={ 12 }>
                        <img 
                            src={ 'data:' + Constante.MIME_TYPE_IMAGEN + ';base64,' + reporte.Imagen } 
                            className={ claseEstilo.cuadroImagen } />
                    </Grid>
                </Grid>
            </ContenidoModal>
        </Dialog>
    )
}