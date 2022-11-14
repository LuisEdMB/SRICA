import { EjecutarPeticion, GenerarFinalBitacoraAccionSistema } from '../API'
import * as Crypto from '../ServicioCryptoAES'
import * as Constante from '../../Constante'

var bitacoraAccionSistema = {
    CodigoUsuario: '',
    UsuarioAcceso: '',
    CodigoModuloSistema: Constante.MODULO_SEDE_EMPRESA,
    CodigoRecursoSistema: Constante.RECURSO_SEDE_EMPRESA,
    CodigoTipoEventoSistema: '',
    CodigoAccionSistema: '',
    DescripcionResultadoAccion: '',
    ValorAnterior: '',
    ValorActual: ''
}

export async function ObtenerListadoSedeEmpresa(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'sedes?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function ObtenerSedeEmpresa(codigoSede, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'sedes/' + encodeURIComponent(codigoSede) + '?bitacoraAccionSistema=' + 
            encodeURIComponent(Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function GuardarSedeEmpresa(sede, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_REGISTRO_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.REGISTRO_CORRECTO_DATOS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(sede)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'sedes',
        Metodo: 'POST',
        Datos: sede,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function ModificarSedeEmpresa(sede, sedeAnterior, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_MODIFICACION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.MODIFICACION_CORRECTA_DATOS
    bitacoraAccionSistema.ValorAnterior = GenerarValorAnteriorOActualParaBitacora(sedeAnterior)
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(sede)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'sedes',
        Metodo: 'PATCH',
        Datos: sede,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function InhabilitarSedesEmpresa(sedes, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_INACTIVACION_REGISTROS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.INACTIVACION_CORRECTA_REGISTROS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorActualConListadosParaBitacora(sedes)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'sedes/inhabilitar',
        Metodo: 'PATCH',
        Datos: sedes,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function HabilitarSedesEmpresa(sedes, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_ACTIVACION_REGISTROS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.ACTIVACION_CORRECTA_REGISTROS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorActualConListadosParaBitacora(sedes)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'sedes/habilitar',
        Metodo: 'PATCH',
        Datos: sedes,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

function GenerarValorAnteriorOActualParaBitacora(sede){
    var valor = {
        Sede: sede.DescripcionSede
    }
    return valor
}

function GenerarValorActualConListadosParaBitacora(sedes){
    var valor = []
    sedes.map((sede) => {
        valor.push({
            Sede: sede.DescripcionSede
        })
    })
    return valor
}