import React from 'react'
import { useSelector } from 'react-redux'

import { makeStyles, Backdrop, CircularProgress } from '@material-ui/core'

const estilos = makeStyles((tema) => ({
    backdrop: {
      zIndex: tema.zIndex.drawer + 9999,
      color: '#fff',
    }
}));

export const BackdropLoad = (props) => {
    const claseEstilo = estilos()
    const general = useSelector((store) => store.General)
    return(
        <Backdrop
            className={ claseEstilo.backdrop }
            open={ general.BackdropAbierto }>
            <CircularProgress color="inherit" />
        </Backdrop>
    )
}