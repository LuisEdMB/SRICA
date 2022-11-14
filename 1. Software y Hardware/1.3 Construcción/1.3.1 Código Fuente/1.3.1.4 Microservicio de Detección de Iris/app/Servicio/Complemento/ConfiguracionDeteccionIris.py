# coding=utf-8
import cv2

class ConfiguracionDeteccionIris:
	"""
		Clase para la inicialización de la configuración del proceso de detección de iris.
	"""

	def __init__(self):
		"""
			Método inicializador de la clase.
		"""
		super().__init__()

	def InicializarConfiguracion(self):
		"""
			Método que inicializa la configuración para el proceso de
			detección de imágenes de iris.

			Returns:
				redNeuronal (cv2.dnn_Net): Red neuronal de detección de ojos.
		"""
		redNeuronal = cv2.dnn.readNet(
			"/app/Servicio/Complemento/ModeloDeteccionOjo.weights",
			"/app/Servicio/Complemento/ConfiguracionModeloDeteccionOjo.cfg")
		return redNeuronal
