#!/bin/bash
app="srica-cliente-web"
docker build -t ${app} .
docker run -v /${HOME}//.certs://certs --env-file=.env -d -p 8000:443 --name=${app} ${app}