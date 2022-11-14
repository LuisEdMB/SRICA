# coding=utf-8
from detectron2 import model_zoo
from detectron2.config import get_cfg

class ConfiguracionSegmentacionIris:
	"""
		Clase para la inicialización de la configuración del proceso de segmentación de iris.
	"""

	def __init__(self):
		"""
			Método inicializador de la clase.
		"""
		super().__init__()

	def InicializarConfiguracion(self):
		"""
			Método que inicializa la configuración para el proceso de
			segmentación de imágenes de iris.

			Returns:
				configuracion (CfgNode): Configuración del modelo entrenado.
		"""
		configuracion = get_cfg()
		configuracion.merge_from_file(model_zoo.get_config_file(
			"COCO-InstanceSegmentation/mask_rcnn_R_101_FPN_3x.yaml"))
		configuracion.DATASETS.TRAIN = ("iris",)
		configuracion.DATASETS.TEST = ("iris",)
		configuracion.MODEL.WEIGHTS = (
			"/app/Servicio/Complemento/ModeloSegmentacionIris.pth")
		configuracion.MODEL.ROI_HEADS.NUM_CLASSES = 3  # Ojo, Iris, Pupila
		configuracion.MODEL.ROI_HEADS.SCORE_THRESH_TEST = 0.7
		configuracion.MODEL.DEVICE = "cpu"
		configuracion.INPUT.MIN_SIZE_TRAIN = (150,)
		configuracion.INPUT.MAX_SIZE_TRAIN = 200
		configuracion.INPUT.MIN_SIZE_TEST = 150
		configuracion.INPUT.MAX_SIZE_TEST = 200
		return configuracion
