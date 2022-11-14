import { EjecutarPeticion, GenerarFinalBitacoraAccionSistema, EjecutarPeticionSinToken, EjecutarPeticionMicroservicio } from '../API'
import * as Crypto from '../ServicioCryptoAES'
import * as Constante from '../../Constante'

var bitacoraAccionSistema = {
    CodigoUsuario: '',
    UsuarioAcceso: '',
    CodigoModuloSistema: Constante.MODULO_PERSONAL_EMPRESA,
    CodigoRecursoSistema: '',
    CodigoTipoEventoSistema: '',
    CodigoAccionSistema: '',
    DescripcionResultadoAccion: '',
    ValorAnterior: '',
    ValorActual: ''
}

export async function ObtenerListadoPersonalEmpresa(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_PERSONAL_EMPRESA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'personal-empresa?bitacoraAccionSistema=' + encodeURIComponent(
            Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function ObtenerListadoSedeEmpresa(callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_PERSONAL_EMPRESA
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

export async function ObtenerPersonalEmpresa(codigoPersonalEmpresa, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_PERSONAL_EMPRESA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_OBTENCION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = ''
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'personal-empresa/' + encodeURIComponent(codigoPersonalEmpresa === '0'
                ? Crypto.Encriptar(codigoPersonalEmpresa)
                : codigoPersonalEmpresa) + 
            '?bitacoraAccionSistema=' + encodeURIComponent(Crypto.Encriptar(bitacoraAccionSistema)),
        Metodo: 'GET',
        Datos: null
    }, callbackExito, callbackError)
}

export async function ObtenerListadoAreaSegunSede(codigoSede, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_PERSONAL_EMPRESA
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

export async function GuardarPersonalEmpresa(personalEmpresa, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_PERSONAL_EMPRESA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_REGISTRO_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.REGISTRO_CORRECTO_DATOS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(personalEmpresa)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'personal-empresa',
        Metodo: 'POST',
        Datos: personalEmpresa,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function RegistrarPersonalEmpresaMasivo(archivoBase64, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_PERSONAL_EMPRESA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_REGISTRO_MASIVO_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.REGISTRO_MASIVO_CORRECTO_DATOS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = ''
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'personal-empresa',
        Metodo: 'POST',
        Datos: {
            EsRegistroMasivo: true,
            ArchivoRegistroMasivo: archivoBase64
        },
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function ModificarPersonalEmpresa(personalEmpresa, personalEmpresaAnterior,
    callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_PERSONAL_EMPRESA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_MODIFICACION_DATOS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.MODIFICACION_CORRECTA_DATOS
    bitacoraAccionSistema.ValorAnterior = GenerarValorAnteriorOActualParaBitacora(personalEmpresaAnterior)
    bitacoraAccionSistema.ValorActual = GenerarValorAnteriorOActualParaBitacora(personalEmpresa)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'personal-empresa',
        Metodo: 'PATCH',
        Datos: personalEmpresa,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function InhabilitarPersonalEmpresa(personalEmpresa, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_PERSONAL_EMPRESA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_INACTIVACION_REGISTROS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.INACTIVACION_CORRECTA_REGISTROS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorActualConListadosParaBitacora(personalEmpresa)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'personal-empresa/inhabilitar',
        Metodo: 'PATCH',
        Datos: personalEmpresa,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function HabilitarPersonalEmpresa(personalEmpresa, callbackExito, callbackError){
    bitacoraAccionSistema.CodigoRecursoSistema = Constante.RECURSO_PERSONAL_EMPRESA
    bitacoraAccionSistema.CodigoAccionSistema = Constante.ACCION_ACTIVACION_REGISTROS
    bitacoraAccionSistema.DescripcionResultadoAccion = Constante.ACTIVACION_CORRECTA_REGISTROS
    bitacoraAccionSistema.ValorAnterior = ''
    bitacoraAccionSistema.ValorActual = GenerarValorActualConListadosParaBitacora(personalEmpresa)
    bitacoraAccionSistema = GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema)
    return EjecutarPeticion({
        URL: 'personal-empresa/habilitar',
        Metodo: 'PATCH',
        Datos: personalEmpresa,
        BitacoraAccionSistema: bitacoraAccionSistema
    }, callbackExito, callbackError)
}

export async function DetectarIrisEnImagen(imagenBase64, callbackExito, callbackError){
    return EjecutarPeticionMicroservicio({
        URL: process.env.REACT_APP_MICROSERVICIO_DETECCION_IRIS_URL + 'detecciones-iris',
        Metodo: 'POST',
        Datos: {
            Imagen: imagenBase64
        }
    }, callbackExito, callbackError, 'detección de imágenes de iris')
}

export async function ReconocerPersonalPorElIris(imagenBase64, callbackExito, callbackError){
    return EjecutarPeticion({
        URL: 'iris/web/reconocimientos',
        Metodo: 'POST',
        Datos: {
            ImagenOjo: imagenBase64,
            EsProcesoPorWeb: true
        }
    }, callbackExito, callbackError)
}

function GenerarValorAnteriorOActualParaBitacora(personalEmpresa){
    var valor = {
        DNI: personalEmpresa.DNIPersonalEmpresa,
        Nombre: personalEmpresa.NombrePersonalEmpresa,
        Apellido: personalEmpresa.ApellidoPersonalEmpresa,
        ImagenOjo: personalEmpresa.ImagenIris !== "" ? "SÍ" : "NO",
        Areas: personalEmpresa.Areas
            .filter((area) => area.Seleccionado)
            .map((area) => {
                return {
                    Sede: area.DescripcionSede,
                    Area: area.DescripcionArea
                }
        })
    }
    return valor
}

function GenerarValorActualConListadosParaBitacora(personalEmpresa){
    var valor = {
        Personal: []
    }
    personalEmpresa.map((personal) => {
        valor.Personal.push({
            DNI: personal.DNIPersonalEmpresa
        })
    })
    return valor
}