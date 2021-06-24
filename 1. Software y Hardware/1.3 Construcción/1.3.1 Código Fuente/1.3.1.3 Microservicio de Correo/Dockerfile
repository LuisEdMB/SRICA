FROM python:3.6

LABEL maintainer="Luis Eduardo Mamani Bedregal - bedregale@gmail.com"

COPY ["1.3.1.3 Microservicio de Correo/requerimientos.txt", "/"]

RUN pip install -r /requerimientos.txt

COPY ["1.3.1.3 Microservicio de Correo/app", "/app"]

ENV PYTHONPATH "${PYTHONPATH}/app"

ENV PYTHONPATH "${PYTHONPATH}:/app/Endpoint"

ENV PYTHONPATH "${PYTHONPATH}:/app/Servicio"

WORKDIR /app

ENV TZ="America/Lima"

ENTRYPOINT ["python3", "main.py"]