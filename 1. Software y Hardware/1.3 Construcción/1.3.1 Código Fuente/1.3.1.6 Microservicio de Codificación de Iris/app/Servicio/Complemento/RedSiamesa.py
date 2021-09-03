# coding=utf-8
from tensorflow import keras
from PIL import Image
import numpy as np

class RedSiamesa:
    """
        Clase que contiene la configuración del modelo de deep learning a utilizar en el
        proceso de codificación de iris.
    """
    def __init__(self):
        """
            Método inicializador de la clase.
        """
        self.modelo = None

    def InicializarConfiguracion(self):
        """
            Método que inicializa la configuración del modelo.
        """
        modeloCargado = keras.models.load_model('/app/Servicio/Complemento/ModeloCodificacionIris.h5')
        inputs = modeloCargado.inputs
        outputs = modeloCargado.layers[-5].output
        self.modelo = keras.Model(inputs, outputs)
        self.modelo.summary()
    
    def CodificarImagen(self, imagen):
        """
            Método que codifica la imagen de iris.

            Args:
                imagen (ndarray): Imagen a codificar.

            Returns:
                imagenCodificada (ndarray): Imagen codificada.
        """
        imagen = Image.fromarray(imagen)
        imagen = imagen.resize((224, 224))
        imagen = keras.preprocessing.image.img_to_array(imagen)
        imagen = np.expand_dims(imagen, axis=0)
        imagen = keras.applications.xception.preprocess_input(imagen)
        imagenCodificada = self.modelo.predict(imagen)
        return imagenCodificada