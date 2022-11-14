# coding=utf-8
from Complemento.RedSiamesaResnet50 import RedSiamesaResnet50
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
        self.resnet50 = RedSiamesaResnet50()
        self.resnet50.CargarModeloResnet50()
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
            imagenIris = self.utilitario.BlurImagen(imagenIris)
            imagenIris = self.utilitario.AutoContraste(imagenIris)
            imagenIrisMejorado = self.utilitario.AplicarEqualizacionDeHistograma(imagenIris)
            caracteristicasIris = self.resnet50.ExtraerCaracteristicasDeImagen(imagenIrisMejorado)
            irisCodificado = json.dumps(caracteristicasIris.tolist())
        return irisCodificado