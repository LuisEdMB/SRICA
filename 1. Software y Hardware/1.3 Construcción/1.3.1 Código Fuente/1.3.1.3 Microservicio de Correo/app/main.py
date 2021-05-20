# coding=utf-8
from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from Endpoint import Correo
import uvicorn

app = FastAPI()
app.include_router(Correo.ruta)

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=False,
    allow_methods=["*"],
    allow_headers=["*"],
)

@app.get("/")
def Inicio():
    """
        Método de ruta principal.

        Returns:
            (str): Mensaje que indica la ejecución correcta del servicio.
    """
    return "Microservicio de Correo is running!"

if __name__ == "__main__":
	uvicorn.run(app, host="0.0.0.0", port=443, ssl_keyfile="/certs/srica-key.pem", 
        ssl_certfile="/certs/srica-cert.pem")