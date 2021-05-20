import Swal from 'sweetalert2'

export const MensajeAlerta = (props) => {
    return Swal.fire({
        title: props.titulo,
        html: props.texto,
        icon: props.icono,
        showCancelButton: false,
        confirmButtonColor: '#48525e',
        confirmButtonText: 'Aceptar',
        allowOutsideClick: false,
        customClass: {
            container: 'swal-container'
        }
    })
}

export const MensajeConfirmacionPorDefecto = (evento) => {
    return Swal.fire({
        title: '¡Confirmación!',
        html: '¿Está seguro de guardar los cambios?',
        icon: 'question',
        confirmButtonColor: '#48525e',
        confirmButtonText: 'Sí, guardar cambios',
        cancelButtonColor: '#aaa',
        cancelButtonText: 'Cancelar',
        showCancelButton: true,
        allowOutsideClick: false,
        reverseButtons: true,
        customClass: {
            container: 'swal-container'
        }
    })
    .then((resultadoBoton) => {
        if(resultadoBoton.value)
            evento()
    })
}

export const MensajeExitoPorDefecto = (evento) => {
    return Swal.fire({
        title: 'Éxito!',
        html: 'Se han guardado los cambios correctamente.',
        icon: 'success',
        confirmButtonColor: '#48525e',
        confirmButtonText: 'Aceptar',
        allowOutsideClick: false,
        customClass: {
            container: 'swal-container'
        }
    })
    .then((resultadoBoton) => {
        if(resultadoBoton.value)
            evento()
    })
}

export const MensajeConfirmacion = (props) => {
    return Swal.fire({
        title: '¡Confirmación!',
        html: props.texto,
        icon: 'question',
        confirmButtonColor: '#48525e',
        confirmButtonText: props.textoBoton,
        cancelButtonColor: '#aaa',
        cancelButtonText: 'Cancelar',
        showCancelButton: true,
        allowOutsideClick: false,
        reverseButtons: true,
        customClass: {
            container: 'swal-container'
        }
    })
    .then((resultadoBoton) => {
        if(resultadoBoton.value)
            props.evento()
    })
}

export const MensajeExito = (props) => {
    return Swal.fire({
        title: 'Éxito!',
        html: props.texto,
        icon: 'success',
        confirmButtonColor: '#48525e',
        confirmButtonText: 'Aceptar',
        allowOutsideClick: false,
        customClass: {
            container: 'swal-container'
        }
    })
    .then((resultadoBoton) => {
        if(resultadoBoton.value)
            props.evento()
    })
}