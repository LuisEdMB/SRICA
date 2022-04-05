# coding=utf-8
import cv2
import numpy as np
import base64
from PIL import Image
from skimage.transform import warp_polar
from skimage.util import img_as_float

class UtilitarioSegmentacionIris:
	"""
		Clase con métodos utilitarios.
	"""

	def __init__(self):
		"""
			Método inicializador de la clase.
		"""
		self.OJO_NUMERO_CLASE = 0
		self.IRIS_NUMERO_CLASE = 1
		self.PUPILA_NUMERO_CLASE = 2
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

	def ObtenerCoordenadaCentralDeLaPupila(self, prediccion):
		"""
			Método que obtiene la coordenada del punto central de la pupila
			de la imagen de iris.

			Args:
				prediccion (DefaultPredictor): Contiene la predicción de
					la imagen original.

			Returns:
				coordenada (tuple): Coordenada del punto central de la pupila
					de la imagen de iris.
		"""
		clasesPredichas = prediccion["instances"].to("cpu").pred_classes
		cajasPredichas = prediccion["instances"].to("cpu").pred_boxes
		posicionArrayPupila = self.__ObtenerIndiceDeArraySegunValor(clasesPredichas,
			self.PUPILA_NUMERO_CLASE)
		cajaPupila = cajasPredichas[posicionArrayPupila].tensor[0]
		coordenada = ((cajaPupila[1].numpy() + cajaPupila[3].numpy()) / 2,
			(cajaPupila[0].numpy() + cajaPupila[2].numpy()) / 2)
		return coordenada

	def ObtenerImagenSoloDelIrisSegmentado(self, imagen, prediccion):
		"""
			Método que obtiene la imagen solo del iris (no ojo, no pupila)
			de la imagen original.

			Args:
				imagen (ndarray): Imagen original.
				prediccion (DefaultPredictor): Contiene la predicción de
					la imagen original.

			Returns:
				ndarray: Imagen solo del iris segmentado.
		"""
		clasesPredichas = prediccion["instances"].to("cpu").pred_classes
		mascarasPredichas = prediccion["instances"].to("cpu").pred_masks
		posicionArrayIris = self.__ObtenerIndiceDeArraySegunValor(clasesPredichas,
			self.IRIS_NUMERO_CLASE)
		posicionArrayPupila = self.__ObtenerIndiceDeArraySegunValor(clasesPredichas,
			self.PUPILA_NUMERO_CLASE)
		mascaras = self.__ConvertirArrayTensorAArrayNumpy(mascarasPredichas)
		mascaras = self.__ConvertirValoresDelArrayATipoDeDatoEntero(mascaras)
		canalColorImagen = self.__ObtenerCanalDeColorDeUnaImagen(imagen)
		if canalColorImagen == 0:
			return self.__RemoverPorcionesNoIrisDeLaImagenNIR(imagen, mascaras, posicionArrayIris)
		else:
			return self.__RemoverPorcionesNoIrisDeLaImagenRGB(imagen, mascaras, posicionArrayIris, 
				canalColorImagen)
		
	def ObtenerImagenSoloDelIrisSegmentadoTransparente(self, imagen, autoajustar=True):
		"""
			Método que obtiene la imagen del iris en transparencia
			(sin fondos negros). Así mismo, se puede o no autoajustar la
			imagen de acorde a sus bordes límites (ancho, alto).

			Args:
				imagen (ndarray): Imagen a realizar la transparencia.
				autoajustar (bool): Si se desea autoajustar la imagen según
					el ancho y alto del mismo.

			Returns:
				ndarray: Imagen con transparencia (y autoajustado).
		"""
		imagenTransparente = self.__TransformarImagenATransparente(imagen)
		if autoajustar:
			imagenTransparente = self.AutoajustarImagen(imagenTransparente)
		return imagenTransparente

	def AutoajustarImagen(self, imagen):
		"""
			Método que recorta la imagen (autoajustar) de acuerdo a sus límites bordes
			(ancho, alto).

			Args:
				imagen (ndarray): Imagen a autoajustar.

			Returns:
				imagen (ndarray): Imagen autoajustada.
		"""
		dataImagen = np.asarray(imagen)
		dataImagenBlancoNegro = dataImagen.max(axis=2)
		columnasNoVaciasImagen = np.where(dataImagenBlancoNegro.max(axis=0) > 0)[0]
		filasNoVaciasImagen = np.where(dataImagenBlancoNegro.max(axis=1) > 0)[0]
		cuadroRecortado = (min(filasNoVaciasImagen), max(filasNoVaciasImagen),
			min(columnasNoVaciasImagen), max(columnasNoVaciasImagen))
		imagen = dataImagen[cuadroRecortado[0]:cuadroRecortado[1] + 1,
		         cuadroRecortado[2]:cuadroRecortado[3] + 1, :]
		return imagen

	def TransformarImagenDeCartesianoAPolar(self, imagen, centroCoordenada):
		"""
			Método que transforma una imagen de coordenadas cartesianas a
			coordenadas polares.

			Args:
				imagen (ndarray): Imagen a transformar.
				centroCoordenada (tuple): Coordenada de punto central.

			Returns:
				imagenPolar (ndarray): Imagen transformado a coordenadas
					polares.
		"""
		imagen = img_as_float(imagen)
		imagenPolar = warp_polar(imagen, center = centroCoordenada, multichannel = True)
		return imagenPolar * 255
	
	def RemoverSeccionPupila(self, imagen, prediccion):
		"""
			Método que remueve la sección de la pupila desde la imagen polar transformada.

			Args:
				imagen (ndarray): Imagen polar a recortar.
				prediccion (DefaultPredictor): Contiene la predicción de
					la imagen original.

			Returns:
				imagenSinPupila (ndarray): Imagen polar sin la sección de pupila.
		"""
		clasesPredichas = prediccion["instances"].to("cpu").pred_classes
		cajasPredichas = prediccion["instances"].to("cpu").pred_boxes
		posicionArrayPupila = self.__ObtenerIndiceDeArraySegunValor(clasesPredichas,
			self.PUPILA_NUMERO_CLASE)
		cajaPupila = cajasPredichas[posicionArrayPupila].tensor[0]
		alto, ancho, _ = imagen.shape
		anchoPupila = (cajaPupila[3] - cajaPupila[1]) / 2
		imagenSinPupila = imagen[0: alto, int(anchoPupila): ancho, :]
		return imagenSinPupila
	
	def RecortarImagenDeIris(self, imagen):
		"""
			Método que recorta una imagen al tamaño 32 (ancho) x 210 (alto) (giro anti-horario).

			Args:
				imagen (ndarray): Imagen a recortar.
			
			Returns:
				imagenRecortada (ndarray): Imagen recortada con el tamaño 32 x 210.
		"""
		imagen = Image.fromarray(imagen.astype(np.uint8))
		imagenRecortada = np.array(imagen.crop((6, 0, 38, 210)))
		return imagenRecortada

	def GirarImagenDeIris(self, imagen, cantidadGiros):
		"""
			Método que gira 90° una imagen, según una cantidad de veces.

			Args:
				imagen (ndarray): Imagen a girar 90°.
				cantidadGiros (int): Cantidad de giros.
			
			Returns:
				imagen (ndarray): Cantidad con rotación.
		"""
		for _ in range(0, cantidadGiros):
			imagen = np.rot90(imagen)
		return imagen

	def __ObtenerIndiceDeArraySegunValor(self, array, valor):
		"""
			Método que obtiene el índice dentro de un array según
			un valor.

			Args:
				array (Tensor): Array que contiene el valor a encontrar.
				valor (int): Valor a encontrar.

			Returns:
				int: Indice del valor encontrado.
		"""
		return (array == valor).nonzero().flatten().tolist()[0]

	def __ConvertirArrayTensorAArrayNumpy(self, array):
		"""
			Método que convierte un array de tipo Tensor a un array de tipo numpy.

			Args:
				array (Tensor): Array Tensor a convertir.

			Returns:
				ndarray: Array convertido a numpy.
		"""
		return array.float().permute(1, 2, 0).numpy()

	def __ConvertirValoresDelArrayATipoDeDatoEntero(self, array):
		"""
			Método que convierte los valores de un array de tipo numpy
			al tipo de dato int.

			Args:
				array (ndarray): Array con los valores a convertir.

			Returns:
				ndarray: Array con valores convertidos a int.
		"""
		return array.astype(int)

	def __ObtenerCanalDeColorDeUnaImagen(self, imagen):
		"""
			Método que obtiene el canal de color (RGB o Blanco-Negro NIR), de una
			imagen.

			Args:
				imagen (ndarray): Imagen.

			Returns:
				int: Número del canal de color de la imagen.
		"""
		if np.size(imagen.shape) == 3:
			return imagen.shape[2]
		return 0

	def __RemoverPorcionesNoIrisDeLaImagenNIR(self, imagen, mascaras,
		posicionArrayIris):
		"""
			Método que remueve los pixeles con las porciones no iris (ojo)
			de la imagen segmentada, para imagenes Blanco-Negro (NIR).

			Args:
				imagen (ndarray): Imagen original.
				mascaras (Tensor): Array Tensor que contiene las máscaras
					(segmentaciones) predichas de la imagen de iris.
				posicionArrayIris (int): Indice de posicionamiento donde
					está ubicado la segmentación del iris.

			Returns:
				ndarray: Imagen sin porciones no iris.
		"""
		imagen[:, :] = imagen[:, :] * mascaras[:, :, posicionArrayIris]
		return imagen

	def __RemoverPorcionesNoIrisDeLaImagenRGB(self, imagen, mascaras,
		posicionArrayIris, canalColorImagen):
		"""
			Método que remueve los pixeles con las porciones no iris (ojo)
			de la imagen segmentada, para imagenes RGB.

			Args:
				imagen (ndarray): Imagen original.
				mascaras (Tensor): Array Tensor que contiene las máscaras
					(segmentaciones) predichas de la imagen de iris.
				posicionArrayIris (int): Indice de posicionamiento donde
					está ubicado la segmentación del iris.
				canalColorImagen (int): Número del canal de color de
					la imagen.

			Returns:
				ndarray: Imagen sin porciones no iris.
		"""
		for i in range(canalColorImagen):
			imagen[:, :, i] = imagen[:, :, i] * mascaras[:, :, posicionArrayIris]
		return imagen

	def __TransformarImagenATransparente(self, imagen):
		"""
			Método que transforma una imagen a transparencia (sin fondos negros).

			Args:
				imagen (ndarray): Imagen a realizar la transparencia.

			Returns:
				ndarray: Imagen con transparencia.
		"""
		alto, ancho = imagen.shape[:2]
		imagen = np.dstack((imagen, np.zeros((alto, ancho), dtype=np.uint8) + 255))
		porcionColorNegroImagen = (imagen[:, :, 0:3] == [0, 0, 0]).all(2)
		imagen[porcionColorNegroImagen] = (0, 0, 0, 0)
		return imagen