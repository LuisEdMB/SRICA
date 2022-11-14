import axios from 'axios'
import * as signalR from '@microsoft/signalr'
import * as Crypto from './ServicioCryptoAES'
import * as AlertaSwal from '../Componente/ComponenteGeneral/Mensaje'
import * as Constante from '../Constante'

export async function GenerarToken(peticion, callbackExito, 
    callbackError){
    peticion.Datos["AudienciaPermitida"] = process.env.REACT_APP_AUDIENCIAPERMITIDA
    return await axios({
        url: process.env.REACT_APP_API + 'api/' + peticion.URL,
        method: 'POST',
        data: {
            Datos: Crypto.Encriptar(peticion.Datos),
            BitacoraAccionSistema: Crypto.Encriptar(peticion.BitacoraAccionSistema)
        },
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(respuesta => ProcesarRespuesta(respuesta.data, callbackExito, callbackError))
    .catch(error => {
        callbackError()
        if(error.response === undefined)
            AlertaSwal.MensajeAlerta({
                titulo: '¡Error!',
                texto: 'Se ha perdido la conexión con el servidor. Verifique la ' + 
                    'existencia y comunicación con el servidor.',
                icono: 'error'
            })
        else{
            AlertaSwal.MensajeAlerta({
                titulo: '¡Error!',
                texto: 'Ha ocurrido un error en el sistema. Verifique la bitácora ' + 
                    'de acciones del sistema para obtener más detalles.',
                icono: 'error'
            })
        }
        console.log(error)
    })
}

export async function EjecutarPeticionSinToken(peticion, callbackExito, callbackError){
    return await axios({
        url: process.env.REACT_APP_API + 'api/' + peticion.URL,
        method: peticion.Metodo,
        data: {
            Datos: Crypto.Encriptar(peticion.Datos),
            BitacoraAccionSistema: Crypto.Encriptar(peticion.BitacoraAccionSistema)
        },
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(respuesta => ProcesarRespuesta(respuesta.data, callbackExito, callbackError))
    .catch(error => {
        callbackError()
        if(error.response === undefined)
            AlertaSwal.MensajeAlerta({
                titulo: '¡Error!',
                texto: 'Se ha perdido la conexión con el servidor. Verifique la ' + 
                    'existencia y comunicación con el servidor.',
                icono: 'error'
            })
        else{
            AlertaSwal.MensajeAlerta({
                titulo: '¡Error!',
                texto: 'Ha ocurrido un error en el sistema. Verifique la bitácora ' + 
                    'de acciones del sistema para obtener más detalles.',
                icono: 'error'
            })
        }
        console.log(error)
    })
}

export async function EjecutarPeticion(peticion, callbackExito, callbackError, generarToken = true, 
    tokenTemporal = '', mostrarMensajeErrorAlterno = false){
    var token = tokenTemporal === '' ? JSON.parse(sessionStorage.getItem(
        Constante.VARIABLE_LOCAL_STORAGE)).Token : tokenTemporal
    return await axios({
        url: process.env.REACT_APP_API + 'api/' + peticion.URL,
        method: peticion.Metodo,
        data: {
            Datos: Crypto.Encriptar(peticion.Datos),
            BitacoraAccionSistema: Crypto.Encriptar(peticion.BitacoraAccionSistema)
        },
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
        }
    })
    .then(respuesta => ProcesarRespuesta(respuesta.data, callbackExito, callbackError, 
        mostrarMensajeErrorAlterno))
    .catch(error => {
        if(error.response === undefined){
            callbackError()
            AlertaSwal.MensajeAlerta({
                titulo: '¡Error!',
                texto: 'Se ha perdido la conexión con el servidor. Verifique la ' + 
                    'existencia y comunicación con el servidor.',
                icono: 'error'
            })
        }
        else if(error.response.status === 401)
            if(generarToken)
                GenerarToken({ 
                    URL: 'token-refresco',
                    Datos: {
                        Token: JSON.parse(sessionStorage.getItem(
                            Constante.VARIABLE_LOCAL_STORAGE)).Token
                    }
                }, (respuesta) => {
                    var storage = JSON.parse(sessionStorage.getItem(Constante.VARIABLE_LOCAL_STORAGE))
                    storage.Token = respuesta
                    sessionStorage.removeItem(Constante.VARIABLE_LOCAL_STORAGE)
                    sessionStorage.setItem(Constante.VARIABLE_LOCAL_STORAGE, JSON.stringify(storage))
                    EjecutarPeticion(peticion, callbackExito, callbackError, generarToken)
                }, () => null)
            else{
                callbackError(Constante.CODIGO_EXCEPCION_ADVERTENCIA_SIMPLE_LOGOUT_USUARIO)
                AlertaSwal.MensajeAlerta({
                    titulo: 'Advertencia!',
                    texto: 'El tiempo de realización ha terminado (2 minutos). ' + 
                        'Deberá realizar el proceso nuevamente.',
                    icono: 'warning'
                })
            }
        else{
            callbackError()
            AlertaSwal.MensajeAlerta({
                titulo: '¡Error!',
                texto: mostrarMensajeErrorAlterno 
                    ?   'Ha ocurrido un error en el sistema. Verifique la ' + 
                        'bitácora o log (archivo de texto) de acciones del sistema ' + 
                        'para obtener más detalles.'
                    :   'Ha ocurrido un error en el sistema. Verifique la bitácora ' + 
                        'de acciones del sistema para obtener más detalles.',
                icono: 'error'
            })
        }
    })
}

export async function EjecutarPeticionMicroservicio(peticion, callbackExito, callbackError, nombreServicio){
    return await axios({
        url: peticion.URL,
        method: peticion.Metodo,
        data: peticion.Datos
    })
    .then(respuesta => {
        if (!respuesta.data.Error) return ProcesarRespuesta(respuesta.data, callbackExito, callbackError, false, 
            false)
        let respuestaError = {
            Error: 'El servicio de ' + nombreServicio + ' ha fallado. Verifique la bitácora de ' +
            'acciones del sistema para obtener más detalles.',
            Detalle: respuesta.data.Mensaje
        }
        callbackError(respuestaError)
        AlertaSwal.MensajeAlerta({
            titulo: '¡Error!',
            texto: respuestaError.Error,
            icono: 'error'
        })
    })
    .catch(error => {
        let respuestaError = {
            Error: 'No se ha podido conectar al servicio de ' + nombreServicio + '. Verifique la ' +
            'bitácora de acciones del sistema para obtener más detalles.',
            Detalle: error
        }
        callbackError(respuestaError)
        AlertaSwal.MensajeAlerta({
            titulo: '¡Error!',
            texto: respuestaError.Error,
            icono: 'error'
        })
    })
}

export async function ConectarHub(url, metodoHub, callbackExito){
    var protocolo = new signalR.JsonHubProtocol()
    var transporte = signalR.HttpTransportType.WebSockets
    const opcionesHub = {
        transporte,
        logMessageContent: true,
        logger: signalR.LogLevel.None
    }
    var hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(process.env.REACT_APP_API + 'hubs/' + url, opcionesHub)
        .withAutomaticReconnect()
        .withHubProtocol(protocolo)
        .build()
    hubConnection.serverTimeoutInMilliseconds = 100000
    hubConnection
        .start()
        .then(() => {
            hubConnection.on(metodoHub, (data) => callbackExito(Crypto.Desencriptar(data)))
        })
        .catch(() => {
            AlertaSwal.MensajeAlerta({
                titulo: '¡Error!',
                texto: 'Ha ocurrido un error en el sistema. Verifique la bitácora ' + 
                    'de acciones del sistema para obtener más detalles.',
                icono: 'error'
            })
        })
    return hubConnection
}

function ProcesarRespuesta(respuesta, callbackExito, callbackError, mostrarMensajeErrorAlterno = false, 
    respuestaEncriptada = true){
    if(respuesta.Error){
        callbackError(respuesta.CodigoExcepcion)
        if (respuesta.CodigoExcepcion === Constante.CODIGO_EXCEPCION_SIMPLE){
            AlertaSwal.MensajeAlerta({
                titulo: '¡Error!',
                texto: respuesta.Mensaje,
                icono: 'error'
            })
            return
        }
        AlertaSwal.MensajeAlerta({
            titulo: '¡Error!',
            texto: mostrarMensajeErrorAlterno 
                    ?   'Ha ocurrido un error en el sistema. Verifique la ' + 
                        'bitácora o log (archivo de texto) de acciones del sistema ' + 
                        'para obtener más detalles.'
                    :   'Ha ocurrido un error en el sistema. Verifique la bitácora ' + 
                        'de acciones del sistema para obtener más detalles.',
            icono: 'error'
        })
    }
    else if(respuesta.Validacion){
        callbackError(respuesta.CodigoExcepcion)
        AlertaSwal.MensajeAlerta({
            titulo: 'Advertencia!',
            texto: respuesta.Mensaje,
            icono: 'warning',
        })
    }
    else{
        var datos = respuestaEncriptada ? Crypto.Desencriptar(respuesta.Datos) : respuesta.Datos
        callbackExito(datos)
    }
}

export function GenerarFinalBitacoraAccionSistema(bitacoraAccionSistema){
    const usuarioLogueado = JSON.parse(sessionStorage.getItem(Constante.VARIABLE_LOCAL_STORAGE))
    if (bitacoraAccionSistema !== undefined){
        bitacoraAccionSistema.CodigoUsuario = usuarioLogueado === null 
            ? ''
            : usuarioLogueado.CodigoUsuario
        return bitacoraAccionSistema
    }
    return ''
}