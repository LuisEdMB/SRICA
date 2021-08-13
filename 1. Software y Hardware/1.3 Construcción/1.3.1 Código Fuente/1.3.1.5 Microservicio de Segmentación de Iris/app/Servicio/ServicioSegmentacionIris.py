# coding=utf-8
from Complemento.ConfiguracionSegmentacionIris import ConfiguracionSegmentacionIris
from Complemento.PrediccionSegmentacionIris import PrediccionSegmentacionIris
from Complemento.UtilitarioSegmentacionIris import UtilitarioSegmentacionIris

class ServicioSegmentacionIris:
	"""
		Clase que representa al servicio de segmentación de imágenes de iris.
	"""

	def __init__(self):
		"""
			Método inicializador de la clase.
		"""
		self.configuracion = ConfiguracionSegmentacionIris()
		self.prediccion = PrediccionSegmentacionIris(self.configuracion.InicializarConfiguracion())
		self.utilitario = UtilitarioSegmentacionIris()
		super().__init__()

	def SegmentarIris(self, imagenOjo):
		"""
			Método que realiza el proceso de segmentación de las imágenes de iris.

			Args:
				imagenOjo (str): Imagen del ojo en formato base64.
			
			Returns:
				irisSegmentado (str): Iris segmentado en formato base64.
		"""
		irisSegmentado = ""
		if imagenOjo is not None and imagenOjo != "":
			imagen = self.utilitario.ConvertirBase64ANumpyArray(imagenOjo)
			prediccionImagen = self.prediccion.PredecirImagen(imagen)
			imagenSoloIris = self.utilitario.ObtenerImagenSoloDelIrisSegmentado(
				imagen, prediccionImagen)
			imagenIrisAjustado = self.utilitario.AutoajustarImagen(
				imagenSoloIris)
			imagenIrisAjustado = self.utilitario.AplicarCLAHE(imagenIrisAjustado)
			irisSegmentado = self.utilitario.ConvertirNumpyArrayABase64(imagenIrisAjustado)
		return irisSegmentado