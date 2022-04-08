# coding=utf-8
import cv2
import RPi.GPIO as GPIO
import time
import requests
import base64
import json
from getmac import get_mac_address
import subprocess
from ControlarComponentes import ControlarComponentes

subprocess.check_call("v4l2-ctl -d /dev/video0 -c exposure_absolute=5000 -c exposure_auto=1 -c sharpness=2 -c contrast=9 -c gain=160", shell = True)

controlarComponentes = ControlarComponentes()

camaraNIR = cv2.VideoCapture("/dev/video0")
camaraNIR.set(3, 1920)
camaraNIR.set(4, 1080)

SENSOR_DISTANCIA_TRIGGER = 23
SENSOR_DISTANCIA_ECHO = 24

DISTANCIA_MINIMA_CM = 10
DISTANCIA_MINIMA_CM_ENCENDER_LUZ = 20
DISTANCIA_MINIMA_CM_CAPTURA_ROSTRO_ACCESO_DENEGADO = 15

URL_API_SRICA = "https://192.168.0.30:8001/"
URL_MICROSERVICIO_DETECCION_IRIS = "https://192.168.0.30:8003/"

GPIO.setmode(GPIO.BCM)
GPIO.setup(SENSOR_DISTANCIA_TRIGGER, GPIO.OUT)
GPIO.setup(SENSOR_DISTANCIA_ECHO, GPIO.IN)

PROCESO_EN_ESPERA = True
PROCESO_CAPTURANDO = True

IMAGEN_ORIGINAL = None
IMAGEN_OJO_NIR = None

def ProcesarDeteccionDeOjos():
    """
        Método que procesa la detección de ojos de la persona.
    """
    global IMAGEN_OJO_NIR
    global IMAGEN_ORIGINAL
    detecciones = ProcesarMicroservicioDeDeteccionDeOjos(IMAGEN_OJO_NIR)
    if detecciones is not None:
        if (detecciones["ImagenOjo"] != ""):
            imagenOriginal = ""
            if IMAGEN_ORIGINAL is not None:
                imagenOriginal = ConvertirNumpyArrayABase64(IMAGEN_ORIGINAL)
            ProcesarReconocimientoDelPersonal(detecciones["ImagenOjo"], imagenOriginal)

def ProcesarMicroservicioDeDeteccionDeOjos(imagen):
    """
        Método que se conecta con el microservicio de detección de ojos para detectar 
        los ojos de la persona.

        Args:
            imagen (ndarray): Imagen a detectar.
        
        Returns:
            request: Resultado de la petición al microservicio.
    """
    global URL_MICROSERVICIO_DETECCION_IRIS
    try:
        respuesta = requests.post(url = URL_MICROSERVICIO_DETECCION_IRIS + "detecciones-iris",
            json = { "Imagen": ConvertirNumpyArrayABase64(imagen) }, verify=False)
        return ManejarResultadoDeDeteccionDeOjos(respuesta)
    except:
        controlarComponentes.ControlarSegunModo(2)
        controlarComponentes.ControlarSegunModo(1)
        controlarComponentes.ControlarSegunModo(8, "Fallo en capturar imágenes.", False)
        time.sleep(1)
        controlarComponentes.ControlarSegunModo(9)

def ProcesarReconocimientoDelPersonal(ojo, imagenOriginal):
    """
        Método que realiza el proceso de reconocimiento de la persona.

        Args:
            ojo (str): Imagen del ojo en formato base64.
            imagenOriginal (str): Imagen original de la cámara en formato base64.
    """
    global URL_API_SRICA
    global IMAGEN_ORIGINAL
    global IMAGEN_OJO_NIR
    try:
        datos = {
            "ImagenOriginal": imagenOriginal,
            "ImagenOjo": ojo,
            "DireccionMacEquipoBiometrico": get_mac_address()
        }
        respuestaReconocimiento = requests.post(url = URL_API_SRICA + "api/iris/equipos-biometricos/reconocimientos",
            json = { "Datos": datos }, verify=False)
        ManejarResultadoDelReconocimiento(respuestaReconocimiento)
    except:
        IMAGEN_ORIGINAL = None
        IMAGEN_OJO_NIR = None
        controlarComponentes.ControlarSegunModo(2)
        controlarComponentes.ControlarSegunModo(1)
        controlarComponentes.ControlarSegunModo(8, "Fallo en reconocimiento.", False)
        time.sleep(1)
        controlarComponentes.ControlarSegunModo(9)

def ConvertirNumpyArrayABase64(imagen):
	"""
		Método que convierte un array numpy a una imagen en formato base64.

		Args:
			imagen (ndarray): Imagen a convertir.

		Returns:
			imagenBase64 (str): Imagen convertida a formato base64.
	"""
	imagenBase64 = ""
	try:
		_, buffer = cv2.imencode(".jpg", imagen)
		imagenBase64 = base64.b64encode(buffer).decode("utf-8")
	except:
		pass
	return imagenBase64

def ManejarResultadoDeDeteccionDeOjos(respuestaDeteccion):
    """
        Método que maneja el resultado del proceso de detección de ojos en la imagen.

        Args:
            respuestaDeteccion: Respuesta dada por el microservicio de detección de ojos.
        
        Returns:
            Resultado de la detección de ojos procesado.
    """
    resultado = json.loads(respuestaDeteccion.text)
    if resultado["Error"] is True:
        raise Exception()
    return resultado["Datos"]

def ManejarResultadoDelReconocimiento(respuestaReconocimiento):
    """
        Método que maneja el resultado del proceso de reconocimiento del personal.

        Args:
            respuestaReconocimiento: Respuesta dada por el servicio de reconocimiento.
    """
    global IMAGEN_ORIGINAL
    global IMAGEN_OJO_NIR
    IMAGEN_ORIGINAL = None
    IMAGEN_OJO_NIR = None
    resultado = json.loads(respuestaReconocimiento.text)
    if resultado["CodigoExcepcion"] == "":
        nombrePersonal = resultado["Datos"]["PersonalEmpresa"]["NombrePersonalEmpresa"].split(" ")[0]
        apellidoPersonal = resultado["Datos"]["PersonalEmpresa"]["ApellidoPersonalEmpresa"].split(" ")[0]
        controlarComponentes.ControlarSegunModo(6)
        controlarComponentes.ControlarSegunModo(0)
        controlarComponentes.ControlarSegunModo(8, "Acceso concedido: " + nombrePersonal + " " + 
            apellidoPersonal, False)
        time.sleep(5)
        controlarComponentes.ControlarSegunModo(1)
    else:
        controlarComponentes.ControlarSegunModo(2)
        controlarComponentes.ControlarSegunModo(1)
        controlarComponentes.ControlarSegunModo(8, "Acceso denegado.", False)
    time.sleep(1)
    controlarComponentes.ControlarSegunModo(9)

def EjecutarProceso():
    global PROCESO_EN_ESPERA
    global SENSOR_DISTANCIA_TRIGGER
    global SENSOR_DISTANCIA_ECHO
    global DISTANCIA_MINIMA_CM_CAPTURA_ROSTRO_ACCESO_DENEGADO
    global IMAGEN_ORIGINAL
    global DISTANCIA_MINIMA_CM
    global PROCESO_CAPTURANDO
    global IMAGEN_OJO_NIR
    global DISTANCIA_MINIMA_CM_ENCENDER_LUZ
    try:
        while True:
            nueva_lectura_distancia = False
            contador_retry_lectura_distancia = 0
            _ = camaraNIR.read()
            if PROCESO_EN_ESPERA is True:
                controlarComponentes.ControlarSegunModo(9)
                PROCESO_EN_ESPERA = False

            time.sleep(0.01)
            GPIO.output(SENSOR_DISTANCIA_TRIGGER, GPIO.HIGH)
            time.sleep(0.0001)
            GPIO.output(SENSOR_DISTANCIA_TRIGGER, GPIO.LOW)

            while GPIO.input(SENSOR_DISTANCIA_ECHO) == 0:
                pass
                contador_retry_lectura_distancia +=1
                if contador_retry_lectura_distancia == 5000:
                    nueva_lectura_distancia = True
                    break
            pulsoSonidoInicio = time.time()
            if nueva_lectura_distancia:
                return False
            while GPIO.input(SENSOR_DISTANCIA_ECHO) == 1:
                pass
            pulsoSonidoFin = time.time()
            
            pulsoSonidoDuracion = pulsoSonidoFin - pulsoSonidoInicio
            distancia = (pulsoSonidoDuracion * 34300) / 2
            distancia = round(distancia, 2)

            if distancia >= DISTANCIA_MINIMA_CM_CAPTURA_ROSTRO_ACCESO_DENEGADO:
                success, imagen = camaraNIR.read()
                if success is True:
                    IMAGEN_ORIGINAL = imagen
            if distancia <= DISTANCIA_MINIMA_CM and distancia > 1:
                if PROCESO_CAPTURANDO is True:
                    controlarComponentes.ControlarSegunModo(8, "/home/pi/beep.mp3", False)
                    controlarComponentes.ControlarSegunModo(5)
                    PROCESO_CAPTURANDO = False
                tiempoUnSegundo = time.time() + 0.6
                while time.time() < tiempoUnSegundo:
                    success, imagen = camaraNIR.read()
                    if success is True:
                        IMAGEN_OJO_NIR = imagen
                controlarComponentes.ControlarSegunModo(8, "/home/pi/beep.mp3", False)
                controlarComponentes.ControlarSegunModo(3)
                ProcesarDeteccionDeOjos()
            else:
                if distancia <= DISTANCIA_MINIMA_CM_ENCENDER_LUZ:
                    PROCESO_CAPTURANDO = True
                    PROCESO_EN_ESPERA = False
                else:
                    PROCESO_CAPTURANDO = True
                    PROCESO_EN_ESPERA = True
    except:
        print("Error en procesamiento del servicio.")
    finally:
        controlarComponentes.ControlarSegunModo(9)
        camaraNIR.release()
        GPIO.cleanup()
        EjecutarProceso()

EjecutarProceso()