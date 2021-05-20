import MuiDialogContent from '@material-ui/core/DialogContent'
import { withStyles } from '@material-ui/core'

export const ContenidoModal = withStyles((tema) => ({
    root: {
        padding: tema.spacing(2)
    }
}))(MuiDialogContent)