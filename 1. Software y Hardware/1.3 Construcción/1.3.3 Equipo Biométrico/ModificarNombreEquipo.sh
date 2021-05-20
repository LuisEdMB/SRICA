#!/bin/bash
nombreEquipo=$1
sudo rm -r /etc/hostname
sudo echo "$nombreEquipo" > /etc/hostname
sudo echo "127.0.0.1	localhost
::1		localhost ip6-localhost ip6-loopback
ff02::1		ip6-allnodes
ff02::2		ip6-allrouters
127.0.1.1	${nombreEquipo}" > /etc/hosts
