import { EjecutarPeticion, GenerarFinalBitacoraAccionSistema } from '../API'
import * as Crypto from '../ServicioCryptoAES'
import * as Constante from '../../Constante'

var bitacoraAccionSistema = {
    CodigoUsuario: '',
    UsuarioAcceso: '',
    CodigoModuloSistema: Constante.MODULO_PERFIL_USUARIO,
    CodigoRecursoSistema: Constante.RECURSO_PERFIL_USUARIO,
    CodigoTipoEventoSistema: '',
    CodigoAccionSistema: '',
    DescripcionResultadoAccion: '',
    ValorAnterior: '',
    ValorActual: ''
}

export async function ObtenerPerfilUsuario(codigoUsuario, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'usuarios/' + encodeURIComponent(codigoUsuario) + '?bitacoraAccionSistema=' + 
            encodeURIComponent(Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function ModificarPerfilUsuario(usuario, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_MODIFICACION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.MODIFICACION_CORRECTA_DATOS
    bitacoraAccionSistema.ValorAnterior = usuario.UsuarioAnterior.CorreoElectronico
    bitacoraAccionSistema.ValorActual = usuario.Usuario.CorreoElectronico
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'usuarios',
        Metodo: 'PATCH',
        Datos: {
            CodigoUsuario: usuario.Usuario.CodigoUsuario,
            CorreoElectronico: usuario.Usuario.CorreoElectronico,
            Contrasena: usuario.Usuario.Contrasena,
            ConfirmarContrasena: usuario.Usuario.ConfirmarContrasena,
            TipoOperacion: Constante.TIPO_OPERACION_MODIFICAR_PERFIL_USUARIO
        },
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}