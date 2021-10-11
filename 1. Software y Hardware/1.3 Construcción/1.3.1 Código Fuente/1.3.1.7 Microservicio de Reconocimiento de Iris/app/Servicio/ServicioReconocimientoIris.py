# coding=utf-8
import numpy as np
from sqlalchemy.orm import Session
from PersonalEmpresa import PersonalEmpresa
from Complemento.Utilitario import Utilitario

class ServicioReconocimientoIris:
    """
        Clase que representa al servicio de reconocimiento de imágenes de iris.
    """
    def __init__(self):
        """
            Método inicializador de la clase.
        """
        self.utilitario = Utilitario()
        super().__init__()
    
    def ReconocerIris(self, irisCodificado, db: Session):
        """
            Método que reconoce la imagen de iris según la base de datos.

            Args:
                irisCodificado (str): Datos del iris codificado.
                db (Session): Conexión a la base de datos.

            Returns:
                codigoPersonal (str): Código del personal de la empresa reconocido.
        """
        template, mask = None, None
        if irisCodificado is not None and irisCodificado != "":
            dicts = self.utilitario.ConvertirCadenaDiccionarioAObjetoDiccionario(irisCodificado)
            template = np.array(dicts["template"])
            mask = np.array(dicts["mask"])
        listadoPersonal = db.query(PersonalEmpresa).all()
        codigoPersonal = self.utilitario.CalcularDistanciaEntreImagenYBaseDeDatos(template, mask,
            listadoPersonal)
        return codigoPersonal