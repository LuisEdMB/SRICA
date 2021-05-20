#!/bin/bash
app="srica-microservicio-reconocimiento"
docker build -t ${app} .
docker run --env-file=microservicio-reconocimiento.env -v /$HOME//.certs://certs -d -p 8006:443 --name=${app} ${app}