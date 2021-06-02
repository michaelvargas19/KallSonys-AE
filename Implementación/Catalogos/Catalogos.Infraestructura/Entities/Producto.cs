using Catalogos.Infraestructura.SettinsDB;
using System.Collections.Generic;

namespace Catalogos.Infraestructura.Entities
{
    [BsonCollection("Productos")]
    public class Producto : Document
    {

        public string NombreCatalogo { get; set; }

        public string CodigoCatalogo { get; set; }

        public string Nombre { get; set; }

        public string CodigoProducto { get; set; }

        public Proveedor Proveedor { get; set; }

        public string Descripcion { get; set; }

        public string TipoProducto { get; set; }

        public double Prioridad { get; set; }

        public string SKU { get; set; }

        public double iva { get; set; }

        public double PesoKg { get; set; }

        public double ValorUnitario { get; set; }

        public TIPO_SEGUIMIENTO TipoSeguimiento { get; set; }

        public int NivelInventario { get; set; }

        public int NivelAdvertencia { get; set; }

        public Descuento Descuento { get; set; }

        public Multimedia Multimedia { get; set; }

        public string Marca { get; set; }

        public bool Estado { get; set; }

        public bool EnAlmacen { get; set; }

        public bool IndExterno { get; set; }

        public List<string> Sinonimos { get; set; }

        public double Calificacion { get; set; }



        public bool Valido()
        {
            return false;

        }

    }
}
