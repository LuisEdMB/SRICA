import Crypto from 'crypto-js'

export function Encriptar(datosDesencriptados){
    var claveSecreta = Crypto.SHA256(process.env.REACT_APP_CLAVESECRETA)
    var vectorIV = Crypto.enc.Base64.parse(process.env.REACT_APP_VECTORIV)
    var datosEncriptados = Crypto.AES.encrypt(JSON.stringify(datosDesencriptados), 
        claveSecreta, { iv: vectorIV }).toString()
    return datosEncriptados
}

export function Desencriptar(datosEncriptados){
    var claveSecreta = Crypto.SHA256(process.env.REACT_APP_CLAVESECRETA)
    var vectorIV = Crypto.enc.Base64.parse(process.env.REACT_APP_VECTORIV)
    var bytesDatosDesencriptados = Crypto.AES.decrypt(datosEncriptados, claveSecreta, 
        { iv: vectorIV })
    var datosDesencriptados = JSON.parse(bytesDatosDesencriptados.toString(Crypto.enc.Utf8))
    return datosDesencriptados
}