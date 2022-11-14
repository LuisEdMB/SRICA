FROM python:3.6

LABEL maintainer="Luis Eduardo Mamani Bedregal - bedregale@gmail.com"

RUN apt update

RUN apt install libgl1 -y

RUN apt install libgl1-mesa-glx -y

COPY ["1.3.1.5 Microservicio de Segmentación de Iris/requerimientos.txt", "/"]

RUN pip install -r /requerimientos.txt

RUN pip install -U 'git+https://github.com/cocodataset/cocoapi.git#subdirectory=PythonAPI'

RUN pip install -U 'git+https://github.com/facebookresearch/detectron2.git'

COPY ["1.3.1.5 Microservicio de Segmentación de Iris/app", "/app"]

ENV PYTHONPATH "${PYTHONPATH}/app"

ENV PYTHONPATH "${PYTHONPATH}:/app/Endpoint"

ENV PYTHONPATH "${PYTHONPATH}:/app/Servicio"

WORKDIR /app

ENV TZ="America/Lima"

ENTRYPOINT ["python3", "main.py"]