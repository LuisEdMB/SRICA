import { EjecutarPeticion, GenerarFinalBitacoraAccionSistema } from '../API'
import * as Crypto from '../ServicioCryptoAES'
import * as Constante from '../../Constante'

var bitacoraAccionSistema = {
    CodigoUsuario: '',
    UsuarioAcceso: '',
    CodigoModuloSistema: Constante.MODULO_DASHBOARD_SISTEMA,
    CodigoRecursoSistema: Constante.RECURSO_DASHBOARD_SISTEMA,
    CodigoTipoEventoSistema: '',
    CodigoAccionSistema: Constante.ACCION_OBTENCION_DATOS,
    DescripcionResultadoAccion: '',
    ValorAnterior: '',
    ValorActual: ''
}

export async function ObtenerListadoPersonalRegistrado(callbackExito, callbackError){
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'personal-empresa?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function ObtenerListadoSede(callbackExito, callbackError){
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'sedes?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function ObtenerListadoAreaSegunSede(codigoSede, callbackExito, callbackError){
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'areas?codigoSedeEncriptado=' + encodeURIComponent(codigoSede) + 
            '&bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function ObtenerListadoBitacoraAccionEquipoBiometrico(callbackExito, callbackError){
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'bitacoras/acciones-equipos-biometricos?bitacoraAccionSistema=' + 
            encodeURIComponent(Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function ObtenerListadoPersonalEmpresa(callbackExito, callbackError){
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'personal-empresa?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function ObtenerListadoAreaEmpresa(callbackExito, callbackError){
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'areas?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}