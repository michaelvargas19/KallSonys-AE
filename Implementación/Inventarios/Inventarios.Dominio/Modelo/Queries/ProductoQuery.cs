using Inventarios.Dominio.Modelo.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventarios.Dominio.Modelo.Queries
{
    public class ProductoQuery
    {
        public string Id { get; set; }
        
        public string CodigoCatalogo { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string TipoProducto { get; set; }

        public string sku { get; set; }

        public string CodigoProducto { get; set; }

        public double iva { get; set; }

        public double PesoKg { get; set; }

        public double ValorUnitario { get; set; }
        
        public int NivelInventario { get; set; }
        
        public ProveedorQuery Proveedor { get; set; }

        public DescuentoQuery Descuentos { get; set; }

        public MultimediaQuery Multimedia { get; set; }

        public string Marca { get; set; }
        
        public bool EnAlmacen { get; set; }

        public bool IndExterno { get; set; }

        public double Calificacion { get; set; }

        public bool EstaEnAlmacen()
        {
            return (this.NivelInventario > 0) ? true : false;
        }



    }
}
