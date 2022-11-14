# coding=utf-8
import os

class ConfiguracionGlobal:
    """
        Clase que contiene los parámetros globales de configuración del microservicio.
    """
    def __init__(self):
        """
            Método inicializador de la clase.
        """
        self.SERVIDOR_BASE_DATOS = os.environ["SERVIDOR_BASE_DATOS"]
        self.PUERTO_BASE_DATOS = os.environ["PUERTO_BASE_DATOS"]
        self.BASE_DATOS = os.environ["BASE_DATOS"]
        self.USUARIO_BASE_DATOS = os.environ["USUARIO_BASE_DATOS"]
        self.CONTRASENA_BASE_DATOS = os.environ["CONTRASENA_BASE_DATOS"]
        super().__init__()