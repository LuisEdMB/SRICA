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