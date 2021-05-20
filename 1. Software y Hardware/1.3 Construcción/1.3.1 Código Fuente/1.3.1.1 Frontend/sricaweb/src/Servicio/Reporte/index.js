import { EjecutarPeticion, GenerarFinalBitacoraAccionSistema } from '../API'
import * as Crypto from '../ServicioCryptoAES'
import * as Constante from '../../Constante'

var bitacoraAccionSistema = {
    CodigoUsuario: '',
    UsuarioAcceso: '',
    CodigoModuloSistema: Constante.MODULO_REPORTE,
    CodigoRecursoSistema: '',
    CodigoTipoEventoSistema: '',
    CodigoAccionSistema: '',
    DescripcionResultadoAccion: '',
    ValorAnterior: '',
    ValorActual: ''
}

export function ObtenerListadoNomenclatura(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_SISTEMA
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

export function ObtenerListadoSede(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_SISTEMA
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

export function ObtenerListadoEquipoBiometrico(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_GENERACION_REPORTE
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.GENERACION_CORRECTA_REPORTE
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = '{ "Tipo": "Reporte de Equipos Biométricos." }'
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'equipos-biometricos?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)) + '&guardarBitacora=true',
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export function ObtenerListadoPersonalEmpresa(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_GENERACION_REPORTE
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.GENERACION_CORRECTA_REPORTE
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = '{ "Tipo": "Reporte de Personal de la Empresa." }'
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'personal-empresa?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)) + '&guardarBitacora=true',
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export function GuardarAccionExportarReporteSistema(valorActual, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_EXPORTACION_REPORTE
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.EXPORTACION_CORRECTA_REPORTE
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = '{ "Tipo": "' + valorActual + '." }'
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'bitacoras/acciones-sistemas',
        Metodo: 'POST',
        Datos: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export function ObtenerListadoRol(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_ACCION_SISTEMA
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
    }, callbackExito, callbackError, true, '', true)
}

export function ObtenerListadoModulo(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_ACCION_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'sistemas/modulos?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError, true, '', true)
}

export function ObtenerListadoRecurso(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_ACCION_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'sistemas/recursos?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError, true, '', true)
}

export function ObtenerListadoTipoEvento(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_ACCION_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'sistemas/tipos-eventos?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError, true, '', true)
}

export function ObtenerListadoAccion(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_ACCION_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'sistemas/acciones?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError, true, '', true)
}

export function ObtenerListadoBitacoraAccionSistema(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_ACCION_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_GENERACION_REPORTE
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.GENERACION_CORRECTA_REPORTE
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'bitacoras/acciones-sistemas?bitacoraAccionSistema=' + 
            encodeURIComponent(Crypto.Encriptar(bitacoraAccionSistema)) + 
            '&guardarBitacora=true',
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError, true, '', true)
}

export function GuardarAccionExportarReporteAccionSistema(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_ACCION_SISTEMA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_EXPORTACION_REPORTE
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.EXPORTACION_CORRECTA_REPORTE
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'bitacoras/acciones-sistemas',
        Metodo: 'POST',
        Datos: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export function ObtenerListadoResultadoAcceso(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_ACCION_EQUIPO_BIOMETRICO
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'sistemas/resultados-accesos?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export function ObtenerListadoBitacoraAccionEquipoBiometrico(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_ACCION_EQUIPO_BIOMETRICO
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_GENERACION_REPORTE
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.GENERACION_CORRECTA_REPORTE
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'bitacoras/acciones-equipos-biometricos?bitacoraAccionSistema=' + 
            encodeURIComponent(Crypto.Encriptar(bitacoraAccionSistema)) + 
            '&guardarBitacora=true',
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export function GuardarAccionExportarReporteAccionEquipoBiometrico(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_REPORTE_ACCION_EQUIPO_BIOMETRICO
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_EXPORTACION_REPORTE
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.EXPORTACION_CORRECTA_REPORTE
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'bitacoras/acciones-sistemas',
        Metodo: 'POST',
        Datos: bitacoraAccionSistema
    }, callbackExito, callbackError)
}