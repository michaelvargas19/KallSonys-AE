sudo docker build . -t api-integracion-img:1.0.0
#sudo docker images
sudo docker login
#medplusmp Holamundo123!
sudo docker tag api-integracion-img:1.0.0 medplusmp/api-integracion-img:v1
sudo docker push medplusmp/api-integracion-img:v1


