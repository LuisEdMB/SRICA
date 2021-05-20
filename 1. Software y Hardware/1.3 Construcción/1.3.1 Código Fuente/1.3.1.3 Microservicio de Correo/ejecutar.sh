#!/bin/bash
app="srica-microservicio-correo"
docker build -t ${app} .
docker run --env-file=microservicio-correo.env -v /$HOME//.certs://certs -d -p 8002:443 --name=${app} ${app}