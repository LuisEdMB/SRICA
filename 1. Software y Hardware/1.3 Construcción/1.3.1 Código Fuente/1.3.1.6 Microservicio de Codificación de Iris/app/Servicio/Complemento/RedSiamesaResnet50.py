import keras
import tensorflow as tf
import numpy as np

class RedSiamesaResnet50:
    """
        Clase que representa a la arquitectura de red siamesa Resnet50 a aplicar
        para la codificación de imágenes de iris.
    """
    def __init__(self):
        """
            Método inicializador de la clase.
        """
        self.session = tf.Session()
        self.graph = tf.get_default_graph()
        self.modelo = None
        with self.graph.as_default():
            with self.session.as_default():
                super().__init__()
    
    def CargarModeloResnet50(self):
        """
            Método que carga los pesos para la arquitectura Resnet50.
        """
        with self.graph.as_default():
            with self.session.as_default():
                self.modelo = keras.applications.resnet50.ResNet50(
                    weights = '/app/Servicio/Complemento/Resnet50.h5py',
                    input_shape = (32, 210, 3),
                    classes = 2000)
                self.modelo.summary()
    
    def ExtraerCaracteristicasDeImagen(self, imagen):
        """
            Método que extrae las características de una imagen, según modelo
            y arquitectura ResNet50 (concepto de red siamesa).

            Args:
                imagen (ndarray): Imagen de entrada.
            
            Returns:
                caracteristicas (ndarray): Características de la imagen.
        """
        with self.graph.as_default():
            with self.session.as_default():
                imagen = np.expand_dims(imagen, axis = 0)
                capaResnet50 = keras.Model(inputs = self.modelo.input,
                    outputs = self.modelo.get_layer('res5c_branch2c').output)
                imagen = keras.applications.resnet50.preprocess_input(imagen)
                caracteristicas = capaResnet50.predict(imagen)
                return caracteristicas