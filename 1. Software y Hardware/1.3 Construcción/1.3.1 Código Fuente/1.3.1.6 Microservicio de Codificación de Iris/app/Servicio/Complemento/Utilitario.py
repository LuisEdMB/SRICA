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

    def BlurImagen(self, imagen):
        """
			Método que aplica Blur a una imagen.

			Args:
				imagen (ndarray): Imagen original de referencia.
			
			Returns:
				(ndarray): Imagen con efecto Blur aplicado.
		"""
        return cv2.GaussianBlur(imagen, (7, 7), 0)

    def AutoContraste(self, imagen):
        """
            Método que realiza un autocontraste según la imagen de entrada.

            Args:
                imagen (ndarray): Imagen original de referencia.

            Returns:
                imagen (ndarray): Imagen con autocontraste aplicado.
        """
        if self.__EsImagenIluminada(imagen):
            contraste = 200
            imagen = np.int16(imagen)
            imagen = imagen * (contraste / 127 + 1) - contraste
            imagen = np.clip(imagen, 0, 255)
            imagen = np.uint8(imagen)
        return imagen

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
    
    def __EsImagenIluminada(self, imagen):
        """
            Método que verifica si una imagen está iluminada o no.

            Args:
                imagen (ndarray): 

            Returns:
                (boolean): True: Imagen con iluminación; False: Imagen con poca iluminación.
        """
        return np.mean(imagen) > 80