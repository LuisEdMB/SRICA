# coding=utf-8
from fastapi import APIRouter
from typing import Optional
from pydantic import BaseModel
from BaseEjecutor import EjecutarProceso
from ServicioDeteccionIris import ServicioDeteccionIris

ruta = APIRouter()

servicioDeteccionIris = ServicioDeteccionIris()

class Modelo(BaseModel):
	Imagen: str

@ruta.post("/detecciones-iris")
def DetectarIris(datos: Modelo):
	"""
		Método de ruta para ejecutar el proceso de detección de las imágenes de iris.

		Args:
			datos (Modelo): Objeto que contiene la imagen a procesar.

		Returns:
			(object): Resultado del éxito o fracaso de la operación.
	"""
	return EjecutarProceso(
		servicioDeteccionIris.DetectarIris,
		datos.Imagen)