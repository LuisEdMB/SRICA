#!/bin/bash
mkdir certs
openssl req -config srica-ssl.conf -new -x509 -sha256 -newkey rsa:2048 -nodes -keyout certs/srica-key.pem -days 365 -out certs/srica-cert.pem
openssl pkcs12 -export -out certs/srica.pfx -inkey certs/srica-key.pem -in certs/srica-cert.pem -passout pass:123.-SRICa