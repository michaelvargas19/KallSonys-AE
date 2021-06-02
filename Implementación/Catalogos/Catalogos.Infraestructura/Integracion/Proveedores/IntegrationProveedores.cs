using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.Entities.Integration;
using Catalogos.Infraestructura.Util;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Catalogos.Infraestructura.Integracion.Proveedores
{
    public class IntegrationProveedores : IIntegrationProveedores
    {

        private readonly HttpClient clientAPI;
        private string HOST_PROVEEDORES;
        private readonly IUtilsInfra _util;
        
        public IntegrationProveedores(IConfiguration configuration,
                                      IUtilsInfra util)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            this.clientAPI = new HttpClient(clientHandler);
            this.HOST_PROVEEDORES = configuration["HostMicroservicios:Proveedores"];
            this._util = util;


        }


        public Producto consultarProducto(string sku)
        {
            Producto producto = new Producto();

            try
            {
                using (HttpResponseMessage response = clientAPI.GetAsync(this.HOST_PROVEEDORES + "/catalog/find?SKU="+sku).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = response.Content.ReadAsStringAsync().Result;
                        ProductoInt productoInt = JsonConvert.DeserializeObject<ProductoInt>(jsonString);
                        producto = this._util.Convert_ProductoInt_To_Producto(productoInt);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return producto;

        }

        public IEnumerable<Producto> consultarProductosProveedor(string idProveedor)
        {
            IEnumerable<Producto> productos = null;

            try
            {
                using (HttpResponseMessage response = clientAPI.GetAsync(this.HOST_PROVEEDORES + "/catalog/list?idProveddor=" + idProveedor).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = response.Content.ReadAsStringAsync().Result;
                        List<ProductoInt> productosInt = JsonConvert.DeserializeObject<List<ProductoInt>>(jsonString);
                        productos = this._util.ConvertList_ProductoInt_To_Producto(productosInt);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return productos;
        }

    }
}
