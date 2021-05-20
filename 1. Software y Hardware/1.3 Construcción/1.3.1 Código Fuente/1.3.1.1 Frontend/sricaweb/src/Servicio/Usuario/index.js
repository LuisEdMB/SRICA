import { EjecutarPeticion, GenerarFinalBitacoraAccionSistema } from '../API'
import * as Crypto from '../ServicioCryptoAES'
import * as Constante from '../../Constante'

var bitacoraAccionSistema = {
    CodigoUsuario: '',
    UsuarioAcceso: '',
    CodigoModuloSistema: Constante.MODULO_USUARIO_SISTEMA,
    CodigoRecursoSistema: Constante.RECURSO_USUARIO_SISTEMA,
    CodigoTipoEventoSistema: '',
    CodigoAccionSistema: '',
    DescripcionResultadoAccion: '',
    ValorAnterior: '',
    ValorActual: ''
}

export async function ObtenerListadoRolUsuario(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'roles-usuarios?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function ObtenerListadoUsuario(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'usuarios?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function ObtenerUsuario(codigoUsuario, callbackExito, callbackError){
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

export async function GuardarUsuario(usuario, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_REGISTRO_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.REGISTRO_CORRECTO_DATOS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(usuario)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'usuarios',
        Metodo: 'POST',
        Datos: usuario,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function ModificarUsuario(usuario, usuarioAnterior, callbackExito, callbackError){
    usuario['TipoOperacion'] = Constante.TIPO_OPERACION_MODIFICAR_USUARIO
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_MODIFICACION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.MODIFICACION_CORRECTA_DATOS
    bitacoraAccionSistema.ValorAnterior = GenerarValorAnteriorOActualParaBitacora(usuarioAnterior)
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(usuario)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'usuarios',
        Metodo: 'PATCH',
        Datos: usuario,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function InhabilitarUsuarios(usuarios, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_INACTIVACION_REGISTROS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.INACTIVACION_CORRECTA_REGISTROS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorActualConListadosParaBitacora(usuarios)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'usuarios/inhabilitar',
        Metodo: 'PATCH',
        Datos: usuarios,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function HabilitarUsuarios(usuarios, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_ACTIVACION_REGISTROS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.ACTIVACION_CORRECTA_REGISTROS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorActualConListadosParaBitacora(usuarios)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'usuarios/habilitar',
        Metodo: 'PATCH',
        Datos: usuarios,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

function GenerarValorAnteriorOActualParaBitacora(usuario){
    var valor = {
        Usuario: usuario.Usuario,
        ContraseñaGeneradaPorDefecto: usuario.EsContrasenaPorDefecto || usuario.CodigoUsuario === '' 
            ? 'Sí' : 'No',
        Nombre: usuario.Nombre,
        Apellido: usuario.Apellido,
        RolDeUsuario: usuario.RolUsuario,
        CorreoElectronico: usuario.CorreoElectronico
    }
    return valor
}

function GenerarValorActualConListadosParaBitacora(usuarios){
    var valor = []
    usuarios.map((usuario) => {
        valor.push({
            Usuario: usuario.Usuario
        })
    })
    return valor
}