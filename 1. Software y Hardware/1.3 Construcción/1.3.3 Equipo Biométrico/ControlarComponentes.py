# coding=utf-8
import RPi.GPIO as GPIO
import argparse
import pyttsx3
import subprocess
import os
from enum import IntEnum
import neopixel
import board

class ModoControl(IntEnum):
    """
        Clase enum para los modos de control al equipo biométrico.
    """
    AbrirElectroiman = 0
    CerrarElectroiman = 1
    LedColorRojo = 2
    LedColorNaranja = 3
    LedColorAzul = 4
    LedColorBlanco = 5
    LedColorVerde = 6
    LedColorAmarillo = 7
    ReproducirAudio = 8
    NoLuz = 9

class ControlarComponentes():
    """
        Clase que controla el equipo biométrico.
    """
    def __init__(self):
        """
            Método que inicializa la clase.
        """
        self.ELECTROIMAN = 17
        self.LED_RGB_PIN = board.D18
        self.LED_RGB_COUNT = 24
        self.LED_RGB_BRILLO = 0.1
        self.LED_RGB_BLANCO_ARRAY = []
        self.LED_RGB_BLANCO_ARRAY.append((0, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((1, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((2, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((3, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((4, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((9, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((10, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((11, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((12, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((13, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((14, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((15, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((20, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((21, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((22, 4, 4, 4))
        self.LED_RGB_BLANCO_ARRAY.append((23, 4, 4, 4))
        super().__init__()

    def ControlarSegunModo(self, modo, texto = '', esperar = True):
        """
            Método que, según un modo de control, realiza el respectivo control al equipo biométrico.

            Args:
                modo (int): Modo de control a realizar.
                texto (str): Texto a reproducir en el equipo biométrico (audio).
                esperar (bool): Si se desea esperar que el proceso concluya antes de seguir con el flujo.
        """
        if modo == ModoControl.AbrirElectroiman:
            self.__ControlarElectroiman(GPIO.LOW)
        elif modo == ModoControl.CerrarElectroiman:
            self.__ControlarElectroiman(GPIO.HIGH)
        elif modo == ModoControl.LedColorRojo:
            self.__ControlarLed(255, 0, 0, brillo = self.LED_RGB_BRILLO, array = range(0, self.LED_RGB_COUNT))
        elif modo == ModoControl.LedColorNaranja:
            self.__ControlarLed(255, 165, 0, brillo = 0.01, array = range(0, self.LED_RGB_COUNT))
        elif modo == ModoControl.LedColorAzul:
            self.__ControlarLed(0, 0, 255, brillo = self.LED_RGB_BRILLO, array = range(0, self.LED_RGB_COUNT))
        elif modo == ModoControl.LedColorBlanco:
            self.__ControlarLedConMatiz(self.LED_RGB_BLANCO_ARRAY, brillo = 1)
        elif modo == ModoControl.LedColorVerde:
            self.__ControlarLed(0, 255, 0, brillo = self.LED_RGB_BRILLO, array = range(0, self.LED_RGB_COUNT))
        elif modo == ModoControl.LedColorAmarillo:
            self.__ControlarLed(255, 255, 0, brillo = self.LED_RGB_BRILLO, array = range(0, self.LED_RGB_COUNT))
        elif modo == ModoControl.ReproducirAudio:
            self.__ReproducirAudio(texto, esperar)
        elif modo == ModoControl.NoLuz:
            self.__ControlarLed(0,0,0, brillo = self.LED_RGB_BRILLO, array = range(0, self.LED_RGB_COUNT))
    
    def __ControlarElectroiman(self, voltaje):
        """
            Método que controla el electroiman.

            Args:
                voltaje (int): Voltaje a establecer para controlar el electroiman.
        """
        GPIO.setmode(GPIO.BCM)
        GPIO.setup(self.ELECTROIMAN, GPIO.OUT)
        GPIO.output(self.ELECTROIMAN, voltaje)
    
    def __ControlarLed(self, rojo, verde, azul, brillo, array):
        """
            Método que controla el led RGB.

            Args:
                rojo (float): Color frecuencia rojo.
                verde (float): Color frecuencia verde.
                azul (float): Color frecuencia azul.
                brillo (float): Cantidad de brillo a aplicar en los LED's.
                array (array): Lista que contiene la numeración de LED's.
        """
        ledRgb = neopixel.NeoPixel(self.LED_RGB_PIN, self.LED_RGB_COUNT, brightness = brillo, auto_write = False)
        for x in array:
            ledRgb[x] = (rojo, verde, azul)
        ledRgb.show()
    
    def __ControlarLedConMatiz(self, leds, brillo):
        """
        """
        ledRgb = neopixel.NeoPixel(self.LED_RGB_PIN, self.LED_RGB_COUNT, brightness = brillo, auto_write = False)
        for led, rojo, verde, azul in leds:
            ledRgb[led] = (rojo, verde, azul)
        ledRgb.show()

    
    def __ReproducirAudio(self, texto, esperar):
        """
            Método que reproduce un audio en el equipo biométrico.

            Args:
                texto (str): Texto a reproducir en el equipo biométrico (audio).
                esperar (bool): Si se desea esperar que el proceso concluya antes de seguir con el flujo.
        """
        if os.path.isfile(texto) is True:
            if esperar is True:
                subprocess.Popen(["mpg321", "-g 2000", "-q", texto]).wait()
            else:
                subprocess.Popen(["mpg321", "-g 2000", "-q", texto])
        else:
            engine = pyttsx3.init()
            engine.setProperty("volume", 1.0)
            engine.setProperty("voice", "spanish-latin-am")
            engine.say(texto)
            engine.runAndWait()

parser = argparse.ArgumentParser(description="Controlar componentes del equipo biométrico.")
parser.add_argument('--modo', dest="modo", type=int)
parser.add_argument('--archivo', dest="archivo", type=str, default = '')

args = parser.parse_args()

controlador = ControlarComponentes()

if args.modo is not None:
    controlador.ControlarSegunModo(args.modo, args.archivo)