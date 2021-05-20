#!/bin/bash
app="srica-api"
docker build -t ${app} .
docker run -v /${HOME}//.certs://https --env-file=sricaapi.env -d -p 8001:443 --name=${app} ${app}