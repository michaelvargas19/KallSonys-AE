#!/bin/bash
sudo docker build . -t javeriana-aes-pica/challengers/api-proveedor-img:1.0.0
sudo sudo docker run -d -p 8080:8080 -p 3306:3306 -p 9092:9092 --name api-proveedor javeriana-aes-pica/challengers/api-proveedor-img:1.0.0