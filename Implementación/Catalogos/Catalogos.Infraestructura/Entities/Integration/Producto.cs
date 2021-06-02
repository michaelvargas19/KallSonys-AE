using Catalogos.Infraestructura.SettinsDB;
using System.Collections.Generic;

namespace Catalogos.Infraestructura.Entities.Integration
{
    [BsonCollection("Productos")]
    public class ProductoInt 
    {
        public string availability { get; set; }

        public string brand_id { get; set; }

        public string brand_name { get; set; }

        public string code { get; set; }
        
        public string condition { get; set; }
        
        public string description { get; set; }
        public bool is_free_shipping { get; set; }
        public string name { get; set; }
        public int order_quantity_maximum { get; set; }
        public int order_quantity_minimum { get; set; }
        public int price { get; set; }
        public int sale_price { get; set; }
        public string SKU { get; set; }
        public string type { get; set; }

    }
}
