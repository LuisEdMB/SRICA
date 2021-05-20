#!/bin/bash
mkdir ${HOME}/.certs
openssl req -config srica-ssl.conf -new -x509 -sha256 -newkey rsa:2048 -nodes -keyout ${HOME}/.certs/srica-key.pem -days 365 -out ${HOME}/.certs/srica-cert.pem
openssl pkcs12 -export -out ${HOME}/.certs/srica.pfx -inkey ${HOME}/.certs/srica-key.pem -in ${HOME}/.certs/srica-cert.pem -passout pass:123.-SRICa