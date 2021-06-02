#!/bin/bash
sudo docker build . -t api-integracion-img:1.0.0
sudo docker run -d -p 8888:8888 --name api-integracion api-integracion-img:1.0.0