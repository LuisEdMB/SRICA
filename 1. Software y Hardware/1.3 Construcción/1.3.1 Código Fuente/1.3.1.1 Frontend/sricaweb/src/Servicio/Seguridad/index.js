import { GenerarToken, EjecutarPeticion, GenerarFinalBitacoraAccionSistema, EjecutarPeticionSinToken } from '../API'
import * as Crypto from '../ServicioCryptoAES'
import * as Constante from '../../Constante'

var bitacoraAccionSistema = {
    CodigoUsuario: '',
    UsuarioAcceso: '',
    CodigoModuloSistema: Constante.MODULO_SEGURIDAD_SISTEMA,
    CodigoRecursoSistema: Constante.RECURSO_SEGURIDAD_SISTEMA,
    CodigoTipoEventoSistema: '',
    CodigoAccionSistema: '',
    DescripcionResultadoAccion: '',
    ValorAnterior: '',
    ValorActual: ''
}

export async function IniciarSesion(usuario, contrasena, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoUsuario = ''
    bitacoraAccionSistema.UsuarioAcceso = usuario
    bitacoraAccionSistema.CodigoModuloSistema = Constante.MODULO_SEGURIDAD_SISTEMA
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_SEGURIDAD_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_ACCESO_SISTEMA
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.ACCESO_CORRECTO
    bitacoraAccionSistema.CodigoTipoEventoSistema = ''
    bitacoraAccionSistema.ValorActual = usuario
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return GenerarToken({
        URL: 'token',
        Datos: {
            Usuario: usuario,
            Contrasena: contrasena
        },
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function CerrarSesion(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoUsuario = ''
    bitacoraAccionSistema.UsuarioAcceso = ''
    bitacoraAccionSistema.CodigoModuloSistema = Constante.MODULO_SEGURIDAD_SISTEMA
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_SEGURIDAD_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_SESION_FINALIZADA
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.SESION_FINALIZADA_CORRECTAMENTE
    bitacoraAccionSistema.CodigoTipoEventoSistema = Constante.TIPO_EVENTO_CORRECTO
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'bitacoras/acciones-sistemas',
        Metodo: 'POST',
        Datos: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function RecuperarContrasenaOlvidada(usuario, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoUsuario = ''
    bitacoraAccionSistema.UsuarioAcceso = usuario
    bitacoraAccionSistema.CodigoModuloSistema = Constante.MODULO_SEGURIDAD_SISTEMA
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_SEGURIDAD_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_RECUPERACION_CONTRASENA
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.RECUPERACION_CORRECTA_CONTRASENA
    bitacoraAccionSistema.CodigoTipoEventoSistema = ''
    bitacoraAccionSistema.ValorActual = usuario
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticionSinToken({
        URL: 'alertas/contrasenas-olvidadas',
        Metodo: 'POST',
        Datos: {
            Usuario: usuario
        },
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function CambiarContrasenaOlvidada(token, codigoUsuario, nuevaContrasena, 
    confirmarNuevaContrasena, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoUsuario = Crypto.Encriptar(codigoUsuario)
    bitacoraAccionSistema.UsuarioAcceso = ''
    bitacoraAccionSistema.CodigoModuloSistema = Constante.MODULO_SEGURIDAD_SISTEMA
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_SEGURIDAD_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_CAMBIO_CONTRASENA_OLVIDADA
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.CAMBIO_CORRECTO_CONTRASENA_OLVIDADA
    bitacoraAccionSistema.CodigoTipoEventoSistema = ''
    bitacoraAccionSistema.ValorActual = ''
    return EjecutarPeticion({
        URL: 'usuarios',
        Metodo: 'PATCH',
        Datos: {
            CodigoUsuario: Crypto.Encriptar(codigoUsuario),
            Contrasena: nuevaContrasena,
            ConfirmarContrasena: confirmarNuevaContrasena,
            TipoOperacion: Constante.TIPO_OPERACION_ACTUALIZAR_CONTRASENA
        },
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError, false, token)
}

export async function CambiarDatosPorDefecto(codigoUsuario, correoElectronico, 
    nuevaContrasena, confirmarNuevaContrasena, noValidarCorreoElectronico, 
    callbackExito, callbackError){
    bitacoraAccionSistema.CodigoUsuario = ''
    bitacoraAccionSistema.UsuarioAcceso = ''
    bitacoraAccionSistema.CodigoModuloSistema = Constante.MODULO_SEGURIDAD_SISTEMA
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_SEGURIDAD_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_CAMBIO_DATOS_POR_DEFECTO
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.CAMBIO_CORRECTO_DATOS_POR_DEFECTO
    bitacoraAccionSistema.CodigoTipoEventoSistema = ''
    bitacoraAccionSistema.ValorActual = noValidarCorreoElectronico ? '' : correoElectronico
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'usuarios',
        Metodo: 'PATCH',
        Datos: {
            CodigoUsuario: codigoUsuario,
            CorreoElectronico: correoElectronico,
            Contrasena: nuevaContrasena,
            ConfirmarContrasena: confirmarNuevaContrasena,
            NoValidarCorreoElectronico: noValidarCorreoElectronico,
            TipoOperacion: Constante.TIPO_OPERACION_CAMBIAR_DATOS_POR_DEFECTO
        },
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError, false)
}

export async function GuardarBitacoraDeErrorDelSistema(modulo, recurso, accion, error){
    bitacoraAccionSistema.CodigoUsuario = ''
    bitacoraAccionSistema.CodigoModuloSistema = modulo
    bitacoraAccionSistema.CodigoRecursoSistema = recurso
    bitacoraAccionSistema.CodigoAccionSistema = accion
    bitacoraAccionSistema.CodigoTipoEventoSistema = Constante.TIPO_EVENTO_ERROR
    bitacoraAccionSistema.DescripcionResultadoAccion = error.Error + '. ' + error.Detalle
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'bitacoras/acciones-sistemas',
        Metodo: 'POST',
        Datos: bitacoraAccionSistema
    }, _ => {}, _ => {})
}