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
			coordenadaCentroPupila = self.utilitario.ObtenerCoordenadaCentralDeLaPupila(prediccionImagen)
			imagenSoloIris = self.utilitario.ObtenerImagenSoloDelIrisSegmentado(imagen, prediccionImagen)
			irisPolar = self.utilitario.TransformarImagenDeCartesianoAPolar(imagenSoloIris, coordenadaCentroPupila)
			irisPolar = self.utilitario.RemoverSeccionPupila(irisPolar, prediccionImagen)
			irisAjustado = self.utilitario.AutoajustarImagen(irisPolar)
			irisRecortado = self.utilitario.RecortarImagenDeIris(irisAjustado)
			irisRecortado = self.utilitario.GirarImagenDeIris(irisRecortado, 3)
			irisSegmentado = self.utilitario.ConvertirNumpyArrayABase64(irisRecortado)
		return irisSegmentado