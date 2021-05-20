# coding=utf-8

def EjecutarProceso(action, *args):
    """
        Método que ejecuta todos los procesos del microservicio.

        Args:
            action (function): Función de acción a ejecutar.
            *args: Parámetros de la función de acción a ejecutar.

        Returns:
            (object): Respuesta del proceso de la función de acción ejecutada.
    """
    try:
        respuesta = action(*args)
        return {
            "Datos": respuesta,
            "Error": False,
            "Mensaje": ""
        }
    except Exception as ex:
        return {
            "Datos:": None,
            "Error": True,
            "Mensaje": str(ex)
        }
    except:
        print('Ocurrió un error!')