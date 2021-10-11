# coding=utf-8
import numpy as np
from PIL import Image
import cv2
import base64
import math

class Utilitario:
    """
        Clase utilitario con funcionalidades usadas por los procesos de codificación y 
        reconocimiento de iris.
    """
    def __init__(self):
        """
            Método inicializador de la clase.
        """
        super().__init__()

    def ConvertirBase64ANumpyArray(self, imagen):
        """
            Método que convierte una imagen en formato base64 a un array numpy.

            Args:
                imagen (str): Imagen en formato base64 a convertir.

            Returns:
                imagenNumpy (ndarray): Imagen convertida a numpy array.
        """
        imagenDecodificada = base64.b64decode(imagen)
        imagenNumpy = np.fromstring(imagenDecodificada, np.uint8)
        imagenNumpy = cv2.imdecode(imagenNumpy, cv2.IMREAD_COLOR)
        return imagenNumpy
    
    def CambiarTamanioDeImagen(self, imagen):
        """
            Método que cambia el tamaño de una imagen a 224 x 224.

            Args:
                imagen (ndarray): Imagen en formato numpy a cambiar de tamaño.

            Returns:
                imagenConvertida (ndarray): Imagen con tamaño cambiado a 224 x 224.
        """
        imagen = Image.fromarray(imagen)
        imagen = imagen.resize((224, 224))
        imagenConvertida = np.asarray(imagen)
        return imagenConvertida
    
    def AplicarCLAHE(self, imagen):
        """
		    Método que aplica la técnica "ecualización de histograma CLAHE" a una imagen
            para mejorar sus características.

            Args:
                imagen (ndarray): Imagen en numpy array.
            
            Returns:
                claheConversion (CLAHE): Imagen convertida según técnica aplicada.
        """
        imagen = cv2.cvtColor(imagen, cv2.COLOR_BGR2GRAY)
        clahe = cv2.createCLAHE(clipLimit = 4.0, tileGridSize = (8,8))
        claheConversion = clahe.apply(imagen)
        return claheConversion
    
    def AplicarSobelEdgeDetector(self, imagen):
        """
		    Método que aplica la técnica "Sobel Edge Detector" a una imagen
            para extraer sus características.

            Args:
                imagen (ndarray): Imagen en numpy array.
            
            Returns:
                imagenEdgeHorizontal, imagenEdgeVertical, imagenGradiente (tuple): Tres tipos de imágenes:
                    imagen con bordes horizontales, imagen con bordes verticales, imagen con efecto gradiente.
        """
        TAMANIO_FILTRO_GAUSSIAN = 9
        filtros = np.array([[-1, 0, 1], [-1, 0, 1], [-1, 0, 1]])
        imagen = self.__AplicarGaussianBlur(imagen, TAMANIO_FILTRO_GAUSSIAN)
        imagenEdgeHorizontal, imagenEdgeVertical, imagenGradiente = self.__AplicarSobelDetection(imagen, filtros)
        return imagenEdgeHorizontal, imagenEdgeVertical, imagenGradiente
    
    def AplicarLogGaborEncoding(self, imagen):
        """
		    Método que aplica la técnica "1-D Log Gabor" a una imagen para extraer sus características.

            Args:
                imagen (ndarray): Imagen en numpy array.
            
            Returns:
                template, mask (tuple): Dos tipos de imágenes: la plantilla de la imagen de iris
                    codificado, y la máscara obtenida de la imágen de iris.
        """
        MIN_WAVELET_GABOR_LENGTH = 40
        SIGMA = 0.9
        filterbank = self.__CalcularConvolucionLogGabor(imagen, MIN_WAVELET_GABOR_LENGTH, SIGMA)
        length = imagen.shape[1]
        template = np.zeros([imagen.shape[0], 2 * length])
        h = np.arange(imagen.shape[0])
        mask = np.zeros(template.shape)
        eleFilt = filterbank[:, :]
        H1 = np.real(eleFilt) > 0
        H2 = np.imag(eleFilt) > 0
        H3 = np.abs(eleFilt) < 0.0001
        for i in range(length):
            ja = 2 * i
            template[:, ja] = H1[:, i]
            template[:, ja + 1] = H2[:, i]
        return template, mask

    def __AplicarGaussianBlur(self, imagen, tamanioKernelFiltroGaussian):
        """
		    Método que aplica el efecto "Gaussian Blur" a una imagen.

            Args:
                imagen (ndarray): Imagen en numpy array.
                tamanioKernelFiltroGaussian (int): Número de filtros a aplicar.
            
            Returns:
                imagenGaussianBlur (ndarray): Imagen con efecto Gaussian Blur.
        """
        filtro = self.__ObtenerFiltroGaussianBlur(tamanioKernelFiltroGaussian, math.sqrt(tamanioKernelFiltroGaussian))
        imagenGaussianBlur = self.__AplicarConvolucionALaImagen(imagen, filtro)
        return imagenGaussianBlur
    
    def __AplicarSobelDetection(self, imagen, filtros):
        """
		    Método que aplica el algoritmo de detección de bordes "Sobel" a una imagen.

            Args:
                imagen (ndarray): Imagen en numpy array.
                filtros (ndarray): Filtros a aplicar.
            
            Returns:
                imagenEdgeHorizontal, imagenEdgeVertical, imagenGradiente (tuple): Tres tipos de imágenes:
                    imagen con bordes horizontales, imagen con bordes verticales, imagen con efecto gradiente.
        """
        imagenEdgeHorizontal = self.__AplicarConvolucionALaImagen(imagen, filtros)
        imagenEdgeVertical = self.__AplicarConvolucionALaImagen(imagen, np.flip(filtros.T, axis = 0))
        imagenGradiente = np.sqrt(np.square(imagenEdgeHorizontal) + np.square(imagenEdgeVertical))
        imagenGradiente *= 255.0 / imagenGradiente.max()
        return imagenEdgeHorizontal, imagenEdgeVertical, imagenGradiente
    
    def __ObtenerFiltroGaussianBlur(self, tamanioKernelFiltroGaussian, sigma):
        """
		    Método que obtiene el filtro a utilizar para el efecto Gaussian Blur.

            Args:
                tamanioKernelFiltroGaussian (int): Tamaño del kernel a aplicar.
                sigma (float): Sigma a aplicar.
            
            Returns:
                filtro2D (float): Filtro en 2D para el efecto Gaussian Blur.
        """
        filtro1D = np.linspace(-(tamanioKernelFiltroGaussian // 2), tamanioKernelFiltroGaussian // 2, tamanioKernelFiltroGaussian)
        for i in range(tamanioKernelFiltroGaussian):
            filtro1D[i] = self.__CalcularDistribucionNormal(filtro1D[i], 0, sigma)
        filtro2D = np.outer(filtro1D.T, filtro1D.T)
        filtro2D *= 1.0 / filtro2D.max()
        return filtro2D
    
    def __CalcularDistribucionNormal(self, x, mu, sd):
        """
		    Método que calcula la distribución normal a un valor "x".

            Args:
                x (float): Valor inicial para ser calculado.
                mu (float): Valor "mu" a aplicar.
                sd (float): Valor "sigma" a aplicar.
            
            Returns:
                valor (float): Valor calculado.
        """
        valor = 1 / (np.sqrt(2 * np.pi) * sd) * np.e ** (-np.power((x - mu) / sd, 2) / 2)
        return valor
    
    def __CalcularConvolucionLogGabor(self, imagen, minWaveletGaborLength, sigma):
        """
		    Método que calcula la convolución de una imagen con la técnica 1-D Log Gabor.

            Args:
                imagen (ndarray): Imagen en numpy array.
                minWaveletGaborLength (int): Tamaño de wavelet para el filtro Log Gabor.
                sigma (float): Valor "sigma" a aplicar.
            
            Returns:
                filterbank (ndarray): Filtros calculados según la convolución de la imagen
                    con técnica Log Gabor.
        """
        rows, ndata = imagen.shape
        logGabor = np.zeros(ndata)
        filterbank = np.zeros([rows, ndata], dtype=complex)
        radius = np.arange(ndata/2 + 1) / (ndata/2) / 2
        radius[0] = 1
        wavelength = minWaveletGaborLength
        fo = 1 / wavelength
        logGabor[0 : int(ndata/2) + 1] = np.exp((-(np.log(radius/fo))**2) / (2 * np.log(sigma)**2))
        logGabor[0] = 0
        for r in range(rows):
            signal = imagen[r, 0:ndata]
            imagefft = np.fft.fft(signal)
            filterbank[r , :] = np.fft.ifft(imagefft * logGabor)
        return filterbank

    def __AplicarConvolucionALaImagen(self, imagen, filtro):
        """
		    Método que aplica una convolución a una imagen.

            Args:
                imagen (ndarray): Imagen en numpy array.
                filtro (ndarray): Filtros a aplicar.
            
            Returns:
                output (ndarray): Convolución aplicada a la imagen.
        """
        if len(imagen.shape) == 3:
            imagen = cv2.cvtColor(imagen, cv2.COLOR_BGR2GRAY)
        imagenRow, imagenCol = imagen.shape
        filtroRow, filtroCol = filtro.shape
        output = np.zeros(imagen.shape)
        paddingHeight = int((filtroRow - 1) / 2)
        paddingWidth = int((filtroCol - 1) / 2)
        paddedImagen = np.zeros((imagenRow + (2 * paddingHeight), imagenCol + (2 * paddingWidth)))
        paddedImagen[paddingHeight:paddedImagen.shape[0] - paddingHeight, paddingWidth:paddedImagen.shape[1] - paddingWidth] = imagen
        for row in range(imagenRow):
            for col in range(imagenCol):
                output[row, col] = np.sum(filtro * paddedImagen[row:row + filtroRow, col:col + filtroCol])
                output[row, col] /= filtro.shape[0] * filtro.shape[1]
        return output