import MuiDialogActions from '@material-ui/core/DialogActions'
import { withStyles } from '@material-ui/core'

export const SeccionAccionModal = withStyles((tema) => ({
    root: {
        margin: 0,
        padding: tema.spacing(1),
        justifyContent: 'center'
    }
}))(MuiDialogActions)