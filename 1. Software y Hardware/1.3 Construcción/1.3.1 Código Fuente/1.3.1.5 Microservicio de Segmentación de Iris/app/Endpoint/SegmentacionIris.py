# coding=utf-8
from fastapi import APIRouter
from pydantic import BaseModel
from BaseEjecutor import EjecutarProceso
from ServicioSegmentacionIris import ServicioSegmentacionIris

ruta = APIRouter()

servicioSegmentacionIris = ServicioSegmentacionIris()

class Modelo(BaseModel):
	ImagenOjo: str

@ruta.post("/segmentaciones-iris")
def SegmentarIris(datos: Modelo):
	"""
		Método de ruta para ejecutar el proceso de segmentación de las imágenes de iris.

		Args:
			datos (Modelo): Objeto que contiene las imagenes de ojos a segmentar.

		Returns:
			(object): Resultado del éxito o fracaso de la operación.
	"""
	return EjecutarProceso(
		servicioSegmentacionIris.SegmentarIris,
		datos.ImagenOjo)
