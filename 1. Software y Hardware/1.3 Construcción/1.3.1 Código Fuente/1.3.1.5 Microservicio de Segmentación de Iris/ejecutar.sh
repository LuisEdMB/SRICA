#!/bin/bash
app="srica-microservicio-segmentacion"
docker build -t ${app} .
docker run -v /$HOME//.certs://certs -d -p 8004:443 --name=${app} ${app}