import { EjecutarPeticion, GenerarFinalBitacoraAccionSistema, ConectarHub } from '../API'
import * as Crypto from '../ServicioCryptoAES'
import * as Constante from '../../Constante'

var bitacoraAccionSistema = {
    CodigoUsuario: '',
    UsuarioAcceso: '',
    CodigoModuloSistema: Constante.MODULO_EQUIPO_BIOMETRICO,
    CodigoRecursoSistema: Constante.RECURSO_EQUIPO_BIOMETRICO,
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

export async function RecibirListadoDeEquiposBiometricosDeLaRedEmpresarialHub(callbackExito){
    return ConectarHub('equipos-biometricos', "RecibirListadoDeEquiposBiometricosDeLaRedEmpresarial", 
        callbackExito)
}

export async function RecibirListadoDeEquiposBiometricosRegistradosHub(callbackExito){
    return ConectarHub('equipos-biometricos', "RecibirListadoDeEquiposBiometricosRegistrados", 
        callbackExito)
}

export async function GuardarEquipoBiometrico(equipoBiometrico, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_REGISTRO_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.REGISTRO_CORRECTO_DATOS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(equipoBiometrico)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'equipos-biometricos',
        Metodo: 'POST',
        Datos: equipoBiometrico,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function ObtenerEquipoBiometrico(codigoEquipoBiometrico, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'equipos-biometricos/' + encodeURIComponent(codigoEquipoBiometrico) + 
            '?bitacoraAccionSistema=' + encodeURIComponent(Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function ObtenerListadoAreaSegunSede(codigoSede, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'areas?codigoSedeEncriptado=' + encodeURIComponent(codigoSede) + 
            '&bitacoraAccionSistema=' + encodeURIComponent(
                Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function ModificarEquipoBiometrico(equipoBiometrico, equipoBiometricoAnterior, callbackExito, 
    callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_MODIFICACION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.MODIFICACION_CORRECTA_DATOS
    bitacoraAccionSistema.ValorAnterior = GenerarValorAnteriorOActualParaBitacora(equipoBiometricoAnterior)
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(equipoBiometrico)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'equipos-biometricos',
        Metodo: 'PATCH',
        Datos: equipoBiometrico,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function AbrirPuertaEquipoBiometrico(equipoBiometrico, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_APERTURA_PUERTA_ACCESO
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.APERTURA_PUERTA_ACCESO_CORRECTA
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(equipoBiometrico)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'equipos-biometricos/' + encodeURIComponent(equipoBiometrico.CodigoEquipoBiometrico) + 
            '/aperturas-puertas',
        Metodo: 'PATCH',
        Datos: null,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function EnviarSenalEquipoBiometrico(equipoBiometrico, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_APERTURA_PUERTA_ACCESO
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.APERTURA_PUERTA_ACCESO_CORRECTA
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(equipoBiometrico)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'equipos-biometricos/' + encodeURIComponent(equipoBiometrico.CodigoEquipoBiometrico) + 
            '/senales',
        Metodo: 'PATCH',
        Datos: null,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function InhabilitarEquiposBiometricos(equiposBiometricos, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_INACTIVACION_REGISTROS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.INACTIVACION_CORRECTA_REGISTROS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorActualConListadosParaBitacora(equiposBiometricos)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'equipos-biometricos/inhabilitar',
        Metodo: 'PATCH',
        Datos: equiposBiometricos,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function HabilitarEquiposBiometricos(equiposBiometricos, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_ACTIVACION_REGISTROS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.ACTIVACION_CORRECTA_REGISTROS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorActualConListadosParaBitacora(equiposBiometricos)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'equipos-biometricos/habilitar',
        Metodo: 'PATCH',
        Datos: equiposBiometricos,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

function GenerarValorAnteriorOActualParaBitacora(equipoBiometrico){
    var valor = {
        Nomenclatura: equipoBiometrico.DescripcionNomenclatura,
        NombreEquipo: equipoBiometrico.NombreEquipoBiometrico,
        DireccionRed: equipoBiometrico.DireccionRedEquipoBiometrico,
        Sede: equipoBiometrico.DescripcionSede,
        Area: equipoBiometrico.DescripcionArea
    }
    return valor
}

function GenerarValorActualConListadosParaBitacora(equiposBiometricos){
    var valor = []
    equiposBiometricos.map((equipoBiometrico) => {
        valor.push({
            NombreEquipo: equipoBiometrico.NombreEquipoBiometrico
        })
    })
    return valor
}