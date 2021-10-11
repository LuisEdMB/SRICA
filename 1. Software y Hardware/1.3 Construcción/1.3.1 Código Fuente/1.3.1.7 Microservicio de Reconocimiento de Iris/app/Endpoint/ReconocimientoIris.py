# coding=utf-8
from fastapi import APIRouter, Depends
from pydantic import BaseModel
from BaseEjecutor import EjecutarProceso
from ContextoBaseDatos import sesion, engine
from Modelo import PersonalEmpresa
from sqlalchemy.orm import Session
from ServicioReconocimientoIris import ServicioReconocimientoIris

ruta = APIRouter()

servicioReconocimientoIris = ServicioReconocimientoIris()

PersonalEmpresa.base.metadata.create_all(bind = engine)

class Modelo(BaseModel):
    IrisCodificado: str

def ObtenerBaseDatos():
    db = sesion()
    try:
        yield db
    finally:
        db.close()

@ruta.post("/reconocimientos-iris")
def ReconocerIris(datos: Modelo, db: Session = Depends(ObtenerBaseDatos)):
    """
        Método de ruta para ejecutar el proceso de reconocimiento de imágenes de iris.

        Args:
            datos (Modelo): Contiene la imagen de iris codificado para el respectivo
                reconocimiento.

        Returns:
            (object): Resultado del éxito o fracaso de la operación.
    """
    return EjecutarProceso(
        servicioReconocimientoIris.ReconocerIris,
        datos.IrisCodificado,
        db)