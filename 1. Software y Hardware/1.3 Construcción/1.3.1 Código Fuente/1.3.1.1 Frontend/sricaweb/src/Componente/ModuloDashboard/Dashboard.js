import React from 'react'

import { PersonalRegistradoDashboard } from './PersonalRegistradoDashboard'
import { AccesoSedeAreaDashboard } from './AccesoSedeAreaDashboard'
import { EquipoBiometricoRegistradoDashboard } from './EquipoBiometricoRegistradoDashboard'
import { TopPersonalAccesoDashboard } from './TopPersonalAccesoDashboard'
import { TopAreaAccesoDashboard } from './TopAreaAccesoDashboard'

import { Grid, makeStyles } from '@material-ui/core'

const estilos = makeStyles({
    principal: {
        marginTop: 85,
        marginBottom: 22,
        minWidth: '100%',
        minHeight: '100%',
        flexGrow: 1
    }
})

export const Dashboard = () => {
    const claseEstilo = estilos()

    return(
        <div
            className={ claseEstilo.principal }>
            <Grid
                container>
                <Grid 
                    item xs={ 12 } sm={ 4 }>
                    <PersonalRegistradoDashboard/>
                    <Grid
                        container>
                        <Grid 
                            item xs={ 12 } sm={ 12 }>
                            <EquipoBiometricoRegistradoDashboard/>
                        </Grid>
                    </Grid>
                </Grid>
                <Grid 
                    item xs={ 12 } sm={ 8 }>
                    <AccesoSedeAreaDashboard/>
                    <Grid
                        container>
                        <Grid 
                            item xs={ 12 } sm={ 6 }>
                            <TopPersonalAccesoDashboard/>
                        </Grid>
                        <Grid 
                            item xs={ 12 } sm={ 6 }>
                            <TopAreaAccesoDashboard/>
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>
        </div>
    )
}