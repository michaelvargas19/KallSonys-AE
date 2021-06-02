sudo docker build . -t api-service-registry-img:1.0.0
#sudo docker images
#sudo docker login / medplusmp Holamundo123!
sudo docker tag api-service-registry-img:1.0.0 medplusmp/api-service-registry-img:v1
sudo docker push medplusmp/api-service-registry-img:v1


