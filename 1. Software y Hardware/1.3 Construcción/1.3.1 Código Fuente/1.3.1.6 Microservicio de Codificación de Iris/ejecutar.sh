#!/bin/bash
app="srica-microservicio-codificacion"
docker build -t ${app} .
docker run -v /$HOME//.certs://certs -d -p 8005:443 --name=${app} ${app}