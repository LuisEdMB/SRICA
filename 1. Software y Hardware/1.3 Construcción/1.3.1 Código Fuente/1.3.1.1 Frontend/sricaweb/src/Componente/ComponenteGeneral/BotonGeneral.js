import React from 'react'

import { Button } from '@material-ui/core'

export const BotonGeneral = (props) => {
    return(
        <Button 
            variant='contained' 
            style = {{ backgroundColor: '#48525e', color: 'white' }}
            onClick={ props.onClick }>
            { props.texto }
        </Button>
    )
}