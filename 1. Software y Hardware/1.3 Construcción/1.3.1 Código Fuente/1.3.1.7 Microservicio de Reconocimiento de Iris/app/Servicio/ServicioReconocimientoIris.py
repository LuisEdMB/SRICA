# coding=utf-8
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
    
    def ReconocerIris(self, imagenIris, db: Session):
        """
            Método que reconoce la imagen de iris según la base de datos.

            Args:
                imagenIris (str): Imagen codificada del iris.
                db (Session): Conexión a la base de datos.

            Returns:
                codigoPersonal (str): Código del personal de la empresa reconocido.
        """
        imagenIrisNumpy = None
        if imagenIris is not None and imagenIris != "":
            imagenIrisNumpy = self.utilitario.ConvertirCadenaNumpyANumpyArray(imagenIris)
        listadoPersonalActivo = db.query(PersonalEmpresa).filter(
            PersonalEmpresa.IndicadorEstado == True).all()
        codigoPersonal = self.utilitario.CalcularDistanciaEntreImagenYBaseDeDatos(imagenIrisNumpy, 
            listadoPersonalActivo)
        return codigoPersonal