using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Models
{
    public class objActalizarEstado
    {
        public string id { get; set; }
        public string nuevoEstado { get; set; }
        public string idProducto { get; set; }
        
    }
    public class ObjEntradaProducto
    {
      
        public string id_proveedor { get; set; }
        public string id_cliente { get; set; }
        public string id_producto { get; set; }
        public int cantidad { get; set; }
        public int precio_unitario { get; set; }
        public string estado_item { get; set; }
    }
    public class DatosEntrada
    {
        public DatosEntradaCompra objEntradaCompra { get; set; }
        public List<DatosEntradaProducto> objEntradaProductos { get; set; }
     

    }
    public class DatosEntradaCompra
    {
        public string id_orden { get; set; }
        public string id_proveedor { get; set; }
        public string id_cliente { get; set; }
       
    }
    public class DatosEntradaProducto
    {
        public string id_producto { get; set; }
        public int cantidad { get; set; }
        public int precio_unitario { get; set; }
        public string estado_item { get; set; }
    }
}
