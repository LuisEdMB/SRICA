import { EjecutarPeticion, GenerarFinalBitacoraAccionSistema } from '../API'
import * as Crypto from '../ServicioCryptoAES'
import * as Constante from '../../Constante'

var bitacoraAccionSistema = {
    CodigoUsuario: '',
    UsuarioAcceso: '',
    CodigoModuloSistema: Constante.MODULO_EQUIPO_BIOMETRICO,
    CodigoRecursoSistema: Constante.RECURSO_NOMENCLATURA,
    CodigoTipoEventoSistema: '',
    CodigoAccionSistema: '',
    DescripcionResultadoAccion: '',
    ValorAnterior: '',
    ValorActual: ''
}

export async function ObtenerListadoNomenclatura(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'nomenclaturas?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function ObtenerNomenclatura(codigoNomenclatura, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'nomenclaturas/' + encodeURIComponent(codigoNomenclatura) + '?bitacoraAccionSistema=' + 
            encodeURIComponent(Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function GuardarNomenclatura(nomenclatura, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_REGISTRO_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.REGISTRO_CORRECTO_DATOS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(nomenclatura)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'nomenclaturas',
        Metodo: 'POST',
        Datos: nomenclatura,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function ModificarNomenclatura(nomenclatura, nomenclaturaAnterior, callbackExito, 
    callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_MODIFICACION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.MODIFICACION_CORRECTA_DATOS
    bitacoraAccionSistema.ValorAnterior = GenerarValorAnteriorOActualParaBitacora(nomenclaturaAnterior)
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(nomenclatura)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'nomenclaturas',
        Metodo: 'PATCH',
        Datos: nomenclatura,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function InhabilitarNomenclaturas(nomenclaturas, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_INACTIVACION_REGISTROS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.INACTIVACION_CORRECTA_REGISTROS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorActualConListadosParaBitacora(nomenclaturas)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'nomenclaturas/inhabilitar',
        Metodo: 'PATCH',
        Datos: nomenclaturas,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function HabilitarNomenclaturas(nomenclaturas, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_ACTIVACION_REGISTROS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.ACTIVACION_CORRECTA_REGISTROS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorActualConListadosParaBitacora(nomenclaturas)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'nomenclaturas/habilitar',
        Metodo: 'PATCH',
        Datos: nomenclaturas,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

function GenerarValorAnteriorOActualParaBitacora(nomenclatura){
    var valor = {
        Nomenclatura: nomenclatura.DescripcionNomenclatura
    }
    return valor
}

function GenerarValorActualConListadosParaBitacora(nomenclaturas){
    var valor = []
    nomenclaturas.map((nomenclatura) => {
        valor.push({
            Nomenclatura: nomenclatura.DescripcionNomenclatura
        })
    })
    return valor
}