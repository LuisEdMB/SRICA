import { EjecutarPeticion, GenerarFinalBitacoraAccionSistema } from '../API'
import * as Crypto from '../ServicioCryptoAES'
import * as Constante from '../../Constante'

var bitacoraAccionSistema = {
    CodigoUsuario: '',
    UsuarioAcceso: '',
    CodigoModuloSistema: Constante.MODULO_AREA_EMPRESA,
    CodigoRecursoSistema: Constante.RECURSO_AREA_EMPRESA,
    CodigoTipoEventoSistema: '',
    CodigoAccionSistema: '',
    DescripcionResultadoAccion: '',
    ValorAnterior: '',
    ValorActual: ''
}

export async function ObtenerListadoAreaEmpresa(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'areas?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
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

export async function ObtenerAreaEmpresa(codigoArea, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'areas/' + encodeURIComponent(codigoArea) + '?bitacoraAccionSistema=' + 
            encodeURIComponent(Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function GuardarAreaEmpresa(area, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_REGISTRO_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.REGISTRO_CORRECTO_DATOS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(area)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'areas',
        Metodo: 'POST',
        Datos: area,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function ModificarAreaEmpresa(area, areaAnterior, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_MODIFICACION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.MODIFICACION_CORRECTA_DATOS
    bitacoraAccionSistema.ValorAnterior = GenerarValorAnteriorOActualParaBitacora(areaAnterior)
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(area)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'areas',
        Metodo: 'PATCH',
        Datos: area,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function InhabilitarAreasEmpresa(areas, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_INACTIVACION_REGISTROS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.INACTIVACION_CORRECTA_REGISTROS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorActualConListadosParaBitacora(areas)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'areas/inhabilitar',
        Metodo: 'PATCH',
        Datos: areas,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function HabilitarAreasEmpresa(areas, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_ACTIVACION_REGISTROS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.ACTIVACION_CORRECTA_REGISTROS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorActualConListadosParaBitacora(areas)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'areas/habilitar',
        Metodo: 'PATCH',
        Datos: areas,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

function GenerarValorAnteriorOActualParaBitacora(area){
    var valor = {
        Area: area.DescripcionArea,
        Sede: area.DescripcionSede
    }
    return valor
}

function GenerarValorActualConListadosParaBitacora(areas){
    var valor = []
    areas.map((area) => {
        valor.push({
            Area: area.DescripcionArea
        })
    })
    return valor
}