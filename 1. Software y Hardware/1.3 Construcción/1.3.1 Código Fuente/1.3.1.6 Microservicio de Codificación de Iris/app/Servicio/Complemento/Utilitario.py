# coding=utf-8
import numpy as np
import cv2
import base64

class Utilitario:
    """
        Clase utilitario con funcionalidades usadas por los procesos de codificación y 
        reconocimiento de iris.
    """
    def __init__(self):
        """
            Método inicializador de la clase.
        """
        super().__init__()

    def ConvertirBase64ANumpyArray(self, imagen):
        """
            Método que convierte una imagen en formato base64 a un array numpy.

            Args:
                imagen (str): Imagen en formato base64 a convertir.

            Returns:
                imagenNumpy (ndarray): Imagen convertida a numpy array.
        """
        imagenDecodificada = base64.b64decode(imagen)
        imagenNumpy = np.fromstring(imagenDecodificada, np.uint8)
        imagenNumpy = cv2.imdecode(imagenNumpy, cv2.IMREAD_COLOR)
        return imagenNumpy
    
    def AplicarEqualizacionDeHistograma(self, imagen):
        """
            Método que mejora las características de la imagen de iris mediante
            la equalización de histograma.

            Args:
                imagen (ndarray): Imagen a aplicar.
            
            Returns:
                imagen (ndarray): Imagen con equalización de histograma aplicada.
        """
        imagen = cv2.cvtColor(imagen, cv2.COLOR_BGR2GRAY)
        imagen = imagen.astype(np.uint8)
        imagen = cv2.equalizeHist(imagen)
        imagen = cv2.cvtColor(imagen, cv2.COLOR_GRAY2BGR)
        return imagen