import React from 'react'

import { createMuiTheme, MuiThemeProvider } from '@material-ui/core'
import { MuiPickersUtilsProvider, DatePicker } from '@material-ui/pickers'

import MomentUtils from '@date-io/moment'
import 'moment/locale/es'
import moment from "moment"

export const DatePickerSelector = (props) => {
    moment.locale("es");
    const color = createMuiTheme({
        palette: {
            primary: {
                main: '#48525e'
            }
        },
    })
    return(
        <MuiThemeProvider theme={ color }>
            <MuiPickersUtilsProvider 
                locale={ 'es' }     
                moment={ moment } 
                utils={ MomentUtils }>
                <DatePicker
                    autoOk
                    variant="inline"
                    label={ props.title }
                    format="DD/MM/yyyy"
                    value={ props.value }
                    onChange={ date => props.changeValue(date) }
                    cancelLabel='Cancelar'
                    okLabel='Aceptar'
                    todayLabel='Hoy'/>
            </MuiPickersUtilsProvider>
        </MuiThemeProvider>
    )
}