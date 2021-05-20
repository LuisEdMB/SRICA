# coding=utf-8
from fastapi import APIRouter
from pydantic import BaseModel
from BaseEjecutor import EjecutarProceso
from ServicioCodificacionIris import ServicioCodificacionIris

ruta = APIRouter()

servicioCodificacionIris = ServicioCodificacionIris()

class Modelo(BaseModel):
    ImagenIris: str

@ruta.post("/codificaciones-iris")
def CodificarIris(datos: Modelo):
    """
        Método de ruta para ejecutar el proceso de codificación de imágenes de iris.

        Args:
			datos (Modelo): Objeto que contiene la imagen a codificar.

        Returns:
            (object): Resultado del éxito o fracaso de la operación.
    """
    return EjecutarProceso(
        servicioCodificacionIris.CodificarIris,
        datos.ImagenIris)