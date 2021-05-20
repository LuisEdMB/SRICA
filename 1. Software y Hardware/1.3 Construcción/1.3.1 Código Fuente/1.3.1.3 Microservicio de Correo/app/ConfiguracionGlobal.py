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
        self.SERVIDOR_CORREO = os.environ["SERVIDOR_CORREO"]
        self.PUERTO_CORREO = os.environ["PUERTO_CORREO"]
        self.USUARIO_CORREO = os.environ["USUARIO_CORREO"]
        self.CONTRASENA_CORREO = os.environ["CONTRASENA_CORREO"]
        self.CORREO_EMISOR = os.environ["CORREO_EMISOR"]
        super().__init__()