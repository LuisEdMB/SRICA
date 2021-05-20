# coding=utf-8
import smtplib
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText
from email.mime.base import MIMEBase
from ConfiguracionGlobal import ConfiguracionGlobal

class ServicioCorreo:
    """
        Clase que representa al servicio de envío de correos.
    """
    def __init__(self):
        """
            Método inicializador de la clase.
        """
        self.configuracionGlobal = ConfiguracionGlobal()
        super().__init__()

    def EnviarCorreo(self, correosDestino, asunto, cuerpo, adjunto):
        """
            Método que envía el correo indicado.

            Args:
                correosDestino (list): Listado de correos destinos a enviar (array str).
                asunto (str): Asunto del correo.
                cuerpo (str): Cuerpo del correo (en formato HTML).
                adjunto (str): Adjunto a enviar (en formato base64).

            Returns:
                (None): Resultado vacío indicando el éxito de la operación.
        """
        servidor = smtplib.SMTP(self.configuracionGlobal.SERVIDOR_CORREO, 
            self.configuracionGlobal.PUERTO_CORREO)
        servidor.connect(self.configuracionGlobal.SERVIDOR_CORREO, 
            self.configuracionGlobal.PUERTO_CORREO)
        servidor.ehlo()
        servidor.starttls()
        servidor.ehlo()
        servidor.login(self.configuracionGlobal.USUARIO_CORREO, 
            self.configuracionGlobal.CONTRASENA_CORREO)
        mensaje = MIMEMultipart("alternative")
        mensaje["subject"] = asunto
        cuerpoHTML = MIMEText(cuerpo, "html")
        mensaje.attach(cuerpoHTML)
        if adjunto is not None and adjunto != "":
            mensaje = self.__AdjuntarAdjunto(mensaje, adjunto)
        servidor.sendmail(self.configuracionGlobal.CORREO_EMISOR, correosDestino, 
            mensaje.as_string().encode("utf-8"))
        servidor.quit()
        return None

    def __AdjuntarAdjunto(self, mensaje, adjunto):
        """
            Método que adjunta el adjunto indicado al mensaje a enviar.

            Args:
                mensaje (MIMEMultipart): Mensaje que será modificado para adjuntar el adjunto
                    indicado.
                adjunto (str): Adjunto a enviar (en formato base64).

            Returns:
                mensaje (MIMEMultipart): Mensaje modificado con el adjunto indicado.
        """
        nombreArchivo = "adjunto.jpg"
        mimeBase = MIMEBase("image", "jpeg")
        mimeBase.set_payload(adjunto)
        mimeBase.add_header('Content-Transfer-Encoding', 'base64')
        mimeBase['Content-Disposition'] = 'attachment; filename="%s"' % nombreArchivo
        mensaje.attach(mimeBase)
        return mensaje