# coding=utf-8
import cv2
import numpy as np

class PrediccionDeteccionIris(object):
	"""
		Clase para el proceso de predicción de las imágenes de iris
		(detección)
	"""

	def __init__(self, redNeuronal):
		"""
			Método inicializador de la clase.

			Args:
				redNeuronal (cv2.dnn_Net): Red neuronal de detección de ojos.
		"""
		self.CONFIDENCIA_MINIMA = 0.2
		self.SUPRESION_NO_MAXIMA_MINIMA = 0.4
		self.modelo = cv2.dnn_DetectionModel(redNeuronal)
		self.modelo.setInputParams(size=(416, 416), scale=1/255)

	def PredecirImagen(self, imagen):
		"""
			Método que predice la imagen del iris (detecciones de ojos).

			Args:
				imagen (ndarray): Imagen para la predicción.

			Returns:
				clases (list), puntajes (list), cajas (list): Clases, puntajes 
					y cajas, de la predicción realizada a la imagen.
		"""
		clases, puntajes, cajas = self.modelo.detect(imagen, self.CONFIDENCIA_MINIMA, 
			self.SUPRESION_NO_MAXIMA_MINIMA)
		return clases, puntajes, cajas