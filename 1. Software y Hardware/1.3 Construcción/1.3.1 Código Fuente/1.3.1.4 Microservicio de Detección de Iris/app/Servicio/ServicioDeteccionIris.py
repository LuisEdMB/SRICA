# coding=utf-8
from Complemento.ConfiguracionDeteccionIris import ConfiguracionDeteccionIris
from Complemento.PrediccionDeteccionIris import PrediccionDeteccionIris
from Complemento.UtilitarioDeteccionIris import UtilitarioDeteccionIris

class ServicioDeteccionIris:
	"""
		Clase que representa al servicio de detección de imágenes de iris.
	"""

	def __init__(self):
		"""
			Método inicializador de la clase.
		"""
		self.configuracion = ConfiguracionDeteccionIris()
		self.redNeuronal = self.configuracion.InicializarConfiguracion()
		self.prediccion = PrediccionDeteccionIris(self.redNeuronal)
		self.utilitario = UtilitarioDeteccionIris()
		super().__init__()

	def DetectarIris(self, imagen):
		"""
			Método que realiza el proceso de detección de las imágenes de iris.

			Args:
				imagen (str): Imagen en formato base64.
			
			Returns:
				resultadoDeteccion (dict): Resultado con las detecciones
					realizadas (imagen general, imagen del ojo detectado).
		"""
		imagen = self.utilitario.ConvertirBase64ANumpyArray(imagen)
		imagenCopia = self.utilitario.BlurImagen(imagen)
		clases, puntajes, cajas = self.prediccion.PredecirImagen(imagenCopia)
		imagenConDetecciones, imagenOjo = self.utilitario.ProcesarDetecciones(imagen, 
			clases, puntajes, cajas)
		resultadoDeteccion = {
			"Imagen": self.utilitario.ConvertirNumpyArrayABase64(imagenConDetecciones),
			"ImagenOjo": self.utilitario.ConvertirNumpyArrayABase64(imagenOjo)
		}
		return resultadoDeteccion
