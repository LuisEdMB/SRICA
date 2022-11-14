# coding=utf-8
import cv2
import numpy as np
import base64

class UtilitarioDeteccionIris:
	"""
		Clase con métodos utilitarios.
	"""

	def __init__(self):
		"""
			Método inicializador de la clase.
		"""
		self.CLASES = ["ojo"]
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

	def ConvertirNumpyArrayABase64(self, imagen):
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

	def ProcesarDetecciones(self, imagen, clases, puntajes, cajas):
		"""
			Método que realiza el procesamiento de las detecciones realizadas
			a una imagen.

			Args:
				imagen (ndarray): Imagen original a procesar.
				clases (list): Clases obtenidas de la predicción de la imagen original.
				puntajes (list): Puntajes de las clases obtenidas de la predicción de la imagen original.
				cajas (list): Cajas de las clases obtenidas de la predicción de la imagen original.
			
			Returns:
				imagenConDetecciones (ndarray), imagenOjo (ndarray): Resultado
				del procesamiento de las detecciones en la imagen original: imagen con predicciones,
				imagen recortada con el ojo detectado.
		"""
		imagenOriginal = imagen
		imagenConDetecciones = imagen
		for (clase, puntaje, caja) in zip(clases, puntajes, cajas):
			color = (0, 255, 255)
			cv2.rectangle(imagenConDetecciones, caja, color, 2)
		imagenOjo = self.__ObtenerSoloOjosDeImagen(imagenOriginal, cajas)
		return imagenConDetecciones, imagenOjo
	
	def BlurImagen(self, imagen):
		"""
			Método que aplica Blur a una imagen.

			Args:
				imagen (ndarray): Imagen original de referencia.
			
			Returns:
				(ndarray): Imagen con efecto Blur aplicado.
		"""
		return cv2.GaussianBlur(imagen, (7, 7), 0)
		
	def __ObtenerSoloOjosDeImagen(self, imagen, cajas):
		"""
			Método que recorta la imagen original para obtener solo la porción
			de ojos detectados.

			Args:
				imagen (ndarray): Imagen original de referencia.
				cajas (list): Lista que contiene las cajas a recortar.
			
			Returns:
				imagenOjo (ndarray): Imagen del ojo detectado.
		"""
		imagenOjo = None
		if len(cajas) > 0:
			x, y, ancho, alto = cajas[0]
			imagenOjo = imagen[y:y + alto, x:x + ancho]
		return imagenOjo