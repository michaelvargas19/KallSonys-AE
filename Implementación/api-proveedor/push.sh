sudo docker build . -t api-proveedor-img:1.0.0
#sudo docker images
#sudo docker login / medplusmp Holamundo123!
sudo docker tag api-proveedor-img:1.0.0 medplusmp/api-proveedor-img:v1
sudo docker push medplusmp/api-proveedor-img:v1


