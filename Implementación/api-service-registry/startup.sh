#!/bin/bash
sudo docker build . -t javeriana-aes-pica/challengers/api-service-registry-img:1.0.0
sudo docker run -d -p 9000:9000 -p 3306:3306 --name api-service-registry javeriana-aes-pica/challengers/api-service-registry-img:1.0.0