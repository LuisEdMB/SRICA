# coding=utf-8
from fastapi import APIRouter
from typing import Optional
from pydantic import BaseModel
from BaseEjecutor import EjecutarProceso
from ServicioCorreo import ServicioCorreo

ruta = APIRouter()

servicioCorreo = ServicioCorreo()

class Modelo(BaseModel):
    CorreosDestino: list
    Asunto: str
    Cuerpo: str
    Adjunto: Optional[str] = None

@ruta.post("/correos")
def EnviarCorreo(datos: Modelo):
    """
        Método de ruta para el envío de correos.

        Returns:
            (object): Resultado del éxito o fracaso de la operación.
    """
    return EjecutarProceso(
        servicioCorreo.EnviarCorreo,
        datos.CorreosDestino,
        datos.Asunto,
        datos.Cuerpo,
        datos.Adjunto)