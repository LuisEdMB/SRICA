#!/bin/bash
app="srica-microservicio-deteccion"
docker build -t ${app} .
docker run -v /$HOME//.certs://certs -d -p 8003:443 --name=${app} ${app}