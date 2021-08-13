# coding=utf-8
import tensorflow as tf
from keras.applications import Xception
import numpy as np
import cv2
import logging

logger = logging.getLogger("root")

class RedSiamesaXception:
    """
        Clase que contiene la configuración del modelo Xception a utilizar en el
        proceso de codificación de iris.
    """
    def __init__(self):
        """
            Método inicializador de la clase.
        """
        self.session = tf.Session()
        self.graph = tf.get_default_graph()
        self.modeloXception = None
        with self.graph.as_default():
            with self.session.as_default():
                logging.info("Configuración codificación inicializada ...")
                super().__init__()

    def InicializarConfiguracion(self):
        """
            Método que inicializa la configuración del modelo Xception.
        """
        with self.graph.as_default():
            with self.session.as_default():
                self.modeloXception = Xception(include_top = False)
                self.modeloXception.compile(
                    optimizer = "adam", 
                    loss = self.__PerdidaTriplete, 
                    metrics = ["accuracy"])
    
    def CodificarImagen(self, imagen):
        """
            Método que codifica la imagen de iris.

            Args:
                imagen (ndarray): Imagen a codificar.

            Returns:
                imagenCodificada (ndarray): Imagen codificada.
        """
        with self.graph.as_default():
            with self.session.as_default():
                imagen = cv2.resize(imagen, (224, 224))
                imagen = imagen[..., ::-1]
                imagen = np.around(imagen/255.0, decimals = 12)
                imagenEntrada = np.array([imagen])
                imagenCodificada = self.modeloXception.predict_on_batch(imagenEntrada)
                return imagenCodificada
    
    def __PerdidaTriplete(self, _, y_pred, alpha = 0.2):
        """
            Método que calcula la pérdida de triplete, escencial para la compilación
            del modelo de Xception.

            Args:
                y_true (list): Listado que contiene las etiquetas originales de la predicción.
                y_pred (list): Listado que contiene las predicciones de salida.
                alpha (float): Hiperparámetro que representa al margen para la función de 
                    pérdida de triplete.

            Returns:
                perdida (float): Pérdida de triplete calculada.
        """
        ancla, positivo, negativo = y_pred[0], y_pred[1], y_pred[2]
        distanciaAnclaYPositivo = tf.reduce_sum(tf.square(tf.subtract(ancla, positivo)), axis = -1)
        distanciaAnclaYNegativo = tf.reduce_sum(tf.square(tf.subtract(ancla, negativo)), axis = -1)
        perdidaBasica = tf.add(tf.subtract(distanciaAnclaYPositivo, distanciaAnclaYNegativo), alpha)
        perdida = tf.reduce_sum(tf.maximum(perdidaBasica, 0.0))
        return perdida