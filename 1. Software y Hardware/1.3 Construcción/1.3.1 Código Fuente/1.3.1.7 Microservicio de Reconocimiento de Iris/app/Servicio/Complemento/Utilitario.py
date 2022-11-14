# coding=utf-8
import numpy as np
import json

class Utilitario:
    """
        Clase utilitario con funcionalidades usadas por el proceso de reconocimiento de iris.
    """
    def __init__(self):
        """
            Método inicializador de la clase.
        """
        self.TAU = 8.00
        self.DISTANCIA_NO_CALCULADA = 10000.00
        super().__init__()

    def ConvertirCadenaNumpyANumpyArray(self, cadenaNumpy):
        """
            Método que convierte una cadena numpy a numpy array.

            Args:
                cadenaNumpy (str): Cadena numpy a convertir.

            Returns:
                numpyArray (ndarray): Numpy array convertido.
        """
        dicts = json.loads(cadenaNumpy)
        return np.array(dicts)

    def CalcularDistanciaEntreImagenYBaseDeDatos(self, imagenIris, baseDatos):
        """
            Método que calcula la distancia entre una imagen codificada y las imágenes codificadas 
            existentes (base de datos).

            Args:
                imagenIris (ndarray): Imagen codificada del iris.
                baseDatos (list): Listado de registros de personal existente.

            Returns:
                codigoPersonal (str): Código del personal de la empresa reconocido.
        """
        codigosPersonal = []
        distanciasIris = []
        for personal in baseDatos:
            codigosPersonal.append(personal.CodigoPersonal)
            distanciaIris = self.__CalcularDistancia(imagenIris, personal.ImagenIrisCodificado)
            distanciasIris.append(distanciaIris)
        print(codigosPersonal)
        print(distanciasIris)
        codigoPersonal = self.__ObtenerCodigoDePersonalSegunDistanciaDeIrisConTau(codigosPersonal, 
            distanciasIris)
        return codigoPersonal

    def __CalcularDistancia(self, imagenIris, imagenIrisBaseDatos):
        """
            Método que calcula la distancia entre dos imágenes.

            Args:
                imagenIris (ndarray): Imagen de iris de entrada.
                imagenIrisBaseDatos (bytearray): Imagen de iris de la base de datos.

            Returns:
                distancia (float): Distancia calculada entre las dos imágenes.
        """
        if imagenIris is None or imagenIrisBaseDatos is None:
            return self.DISTANCIA_NO_CALCULADA
        imagenIrisBaseDatosNumpy = self.ConvertirCadenaNumpyANumpyArray(imagenIrisBaseDatos.decode("utf-8"))
        distancia = np.linalg.norm(imagenIris - imagenIrisBaseDatosNumpy)
        if distancia is None:
            return self.DISTANCIA_NO_CALCULADA
        return distancia
    
    def __ObtenerCodigoDePersonalSegunDistanciaDeIrisConTau(self, codigosPersonal, distancias):
        """
            Método que obtiene el código de personal según la distancia calculada del iris y 
            el hiperparámetro TAU.

            Args:
                codigosPersonal (list): Contiene todos los códigos de personal registrados.
                distancias (list): Distancias calculadas del iris.
            
            Returns:
                codigoPersonal (str): Código del personal correspondiente al iris.
        """
        codigoPersonal = None
        if len(distancias) > 0:
            indiceMinimo = distancias.index(min(distancias))
            if distancias[indiceMinimo] <= self.TAU:
                codigoPersonal = codigosPersonal[indiceMinimo]
        return codigoPersonal