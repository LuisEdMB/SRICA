# coding=utf-8
from Complemento.RedSiamesa import RedSiamesa
from Complemento.Utilitario import Utilitario
import json

class ServicioCodificacionIris:
    """
        Clase que representa al servicio de codificación de imágenes de iris.
    """
    def __init__(self):
        """
            Método inicializador de la clase.
        """
        self.redSiamesa = RedSiamesa()
        self.redSiamesa.InicializarConfiguracion()
        self.utilitario = Utilitario()
        super().__init__()
    
    def CodificarIris(self, imagenIris):
        """
            Método que realiza el proceso de codificación de las imágenes de iris.

            Args:
                imagenIris (str): Imagen del iris en formato base64.

            Returns:
                (str): Codificación del iris en formato base64.
        """
        return self.__ProcesarCodificacionDeIris(imagenIris)
    
    def __ProcesarCodificacionDeIris(self, imagen):
        """
            Método que procesa la codificación de iris de una imagen.

            Args:
                imagen (str): Imagen a codificar.

            Returns:
                irisCodificado (str): Iris codificado.
        """
        irisCodificado = ""
        if imagen is not None and imagen != "":
            imagenIris = self.utilitario.ConvertirBase64ANumpyArray(imagen)
            imagenIrisCodificado = self.redSiamesa.CodificarImagen(imagenIris)
            irisCodificado = json.dumps(imagenIrisCodificado.tolist())
        return irisCodificado