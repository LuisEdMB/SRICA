FROM python:3.6

LABEL maintainer="Luis Eduardo Mamani Bedregal - bedregale@gmail.com"

RUN apt update

RUN apt install libgl1 -y

RUN apt install libgl1-mesa-glx -y

ADD requerimientos.txt /

RUN pip install -r /requerimientos.txt

COPY ./app /app

ENV PYTHONPATH "${PYTHONPATH}/app"

ENV PYTHONPATH "${PYTHONPATH}:/app/Endpoint"

ENV PYTHONPATH "${PYTHONPATH}:/app/Servicio"

WORKDIR /app

ENV TZ="America/Lima"

ENTRYPOINT ["python3", "main.py"]