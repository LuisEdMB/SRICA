# coding=utf-8
import numpy as np
import json

class Utilitario:
    """
        Clase utilitario con funcionalidades usadas por los procesos de codificación y 
        reconocimiento de iris.
    """
    def __init__(self):
        """
            Método inicializador de la clase.
        """
        self.UMBRAL = 0.365
        self.DISTANCIA_NO_CALCULADA = 10000.00
        super().__init__()

    def ConvertirCadenaDiccionarioAObjetoDiccionario(self, cadenaDiccionario):
        """
            Método que convierte una cadena string que contiene un diccionario, a un objeto diccionario.

            Args:
                cadenaDiccionario (str): Cadena diccionario a convertir.

            Returns:
                dicts (diccionario): Diccionario convertido.
        """
        dicts = json.loads(cadenaDiccionario)
        return dicts

    def CalcularDistanciaEntreImagenYBaseDeDatos(self, template, mask, baseDatos):
        """
            Método que calcula la distancia "Distancia Hamming" entre una imagen codificada y
            las imágenes codificadas existentes (base de datos).

            Args:
                template (ndarray): Plantilla del iris codificada.
                mask (ndarray): Máscara del iris codificada.
                baseDatos (list): Listado de registros de personal existente, en estado activo.

            Returns:
                codigoPersonal (str): Código del personal de la empresa reconocido.
        """
        codigosPersonal = []
        distanciasIris = []
        for personal in baseDatos:
            codigosPersonal.append(personal.CodigoPersonal)
            distanciaIris = self.__CalcularDistancia(template, mask, personal.ImagenIrisCodificado)
            distanciasIris.append(distanciaIris)
        print(codigosPersonal)
        print(distanciasIris)
        codigoPersonal = self.__ObtenerCodigoDePersonalSegunDistanciaDeIrisConUmbral(codigosPersonal, 
            distanciasIris)
        return codigoPersonal

    def __CalcularDistancia(self, template, mask, irisBaseDatos):
        """
            Método que calcula la distancia entre dos imágenes.

            Args:
                template (ndarray): Plantilla del iris codificada.
                mask (ndarray): Máscara del iris codificada.
                irisBaseDatos (bytearray): Iris codificado de la base de datos.

            Returns:
                distancia (float): Distancia calculada entre las dos imágenes.
        """
        if template is None or mask is None or irisBaseDatos is None:
            return self.DISTANCIA_NO_CALCULADA
        irisBaseDatosDiccionario = self.ConvertirCadenaDiccionarioAObjetoDiccionario(irisBaseDatos.decode("utf-8"))
        templateBaseDatos = np.array(irisBaseDatosDiccionario["template"])
        maskBaseDatos = np.array(irisBaseDatosDiccionario["mask"])
        distancia = self.__HallarDistanciaHamming(templateBaseDatos, maskBaseDatos, template, mask)
        if distancia is None or np.isnan(distancia):
            return self.DISTANCIA_NO_CALCULADA
        return distancia
    
    def __HallarDistanciaHamming(self, template1, mask1, template2, mask2):
        """
            Método que calcula la distancia Hamming entre el template de entrada (que representa a una imagen de iris) 
            con el template de la base de datos (imagen de iris de la base de datos).

            Args:
                template1 (ndarray): Plantilla del iris codificada de la base de datos.
                mask1 (ndarray): Máscara del iris codificada de la base de datos.
                template2 (ndarray): Plantilla del iris codificada de entrada.
                mask2 (ndarray): Máscara del iris codificada de entrada.

            Returns:
                hd (float): Distancia calculada entre las dos imágenes.
        """
        hd = np.nan
        for shifts in range(-8,9):
            template1s = self.__ShiftBits(template1, shifts)
            mask1s = self.__ShiftBits(mask1, shifts)
            mask = np.logical_or(mask1s, mask2)
            nummaskbits = np.sum(mask==1)
            totalbits = template1s.size - nummaskbits
            C = np.logical_xor(template1s, template2)
            C = np.logical_and(C, np.logical_not(mask))
            bitsdiff = np.sum(C==1)
            if totalbits==0:
                hd = np.nan
            else:
                hd1 = bitsdiff / totalbits
                if hd1 < hd or np.isnan(hd):
                    hd = hd1
        return hd
    
    def __ShiftBits(self, template, noshifts):
        """
            Método que realiza un intercambio de bits en un template de imagen de iris.

            Args:
                template (ndarray): Plantilla del iris codificada.
                noshifts (int): Número de intercambio a aplicar.

            Returns:
                templateNew (ndarray): Nueva plantilla del iris codificada con los bits intercambiados.
        """
        templatenew = np.zeros(template.shape)
        width = template.shape[1]
        s = 2 * np.abs(noshifts)
        p = width - s
        if noshifts == 0:
            templatenew = template
        elif noshifts < 0:
            x = np.arange(p)
            templatenew[:, x] = template[:, s + x]
            x = np.arange(p, width)
            templatenew[:, x] = template[:, x - p]
        else:
            x = np.arange(s, width)
            templatenew[:, x] = template[:, x - s]
            x = np.arange(s)
            templatenew[:, x] = template[:, p + x]
        return templatenew

    def __ObtenerCodigoDePersonalSegunDistanciaDeIrisConUmbral(self, codigosPersonal, distancias):
        """
            Método que obtiene el código de personal según la distancia calculada del iris y 
            un umbral.

            Args:
                codigosPersonal (list): Contiene todos los códigos de personal registrados.
                distancias (list): Distancias calculadas del iris.
            
            Returns:
                codigoPersonal (str): Código del personal correspondiente al iris.
        """
        codigoPersonal = None
        if len(distancias) > 0:
            indiceMinimo = distancias.index(min(distancias))
            if distancias[indiceMinimo] <= self.UMBRAL:
                codigoPersonal = codigosPersonal[indiceMinimo]
        return codigoPersonal