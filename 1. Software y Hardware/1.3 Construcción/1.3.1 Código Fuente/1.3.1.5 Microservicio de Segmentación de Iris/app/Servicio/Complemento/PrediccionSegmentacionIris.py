# coding=utf-8
from detectron2.data import MetadataCatalog
from detectron2.engine.defaults import DefaultPredictor
from detectron2.utils.visualizer import ColorMode, Visualizer

class PrediccionSegmentacionIris(object):
	"""
		Clase para el proceso de predicción de las imágenes de iris
		(segmentación)
	"""

	def __init__(
			self,
			configuracion):
		"""
			Método inicializador de la clase.

			Args:
				configuracion (CfgNode): Configuración del modelo entrenado.
		"""
		self.metadata = MetadataCatalog.get("iris")
		self.predictor = DefaultPredictor(configuracion)

	def PredecirImagen(self, imagen):
		"""
			Método que predice la imagen del iris (segmentaciones).

			Args:
				imagen (ndarray): Imagen de iris para la predicción.

			Returns:
				prediccion (DefaultPredictor): Prediccion de la imagen.
		"""
		prediccion = self.predictor(imagen)
		return prediccion

	def ObtenerVisualizadorDePrediccion(self, imagen, prediccion):
		"""
			Método que obtiene el visualizador de la imagen de iris predicha.

			Args:
				imagen (ndarray): Imagen de iris original.
				prediccion (DefaultPredictor): Predicción realizado a
					la imagen de iris original.

			Returns:
				visualizadorPrediccion (VisImage): Visualizador de la
					imagen predicha.
		"""
		visualizadorPrediccion = Visualizer(
			imagen,
			metadata=self.metadata,
			scale=1,
			instance_mode=ColorMode.IMAGE)
		visualizadorPrediccion = visualizadorPrediccion.draw_instance_predictions(
			prediccion["instances"].to("cpu"))
		return visualizadorPrediccion
