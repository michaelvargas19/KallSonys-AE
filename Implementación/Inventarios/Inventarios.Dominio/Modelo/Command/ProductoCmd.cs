using Inventarios.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inventarios.Dominio.Modelo.Command
{
    public class ProductoCmd
    {
        public string CodigoCatalogo { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string TipoProducto { get; set; }

        public double Prioridad { get; set; }

        public string SKU { get; set; }

        public string CodigoProducto { get; set; }

        public double iva { get; set; }

        public double PesoKg { get; set; }

        public double ValorUnitario { get; set; }


        public TIPO_SEGUIMIENTO TipoSeguimiento { get; set; }

        public int NivelInventario { get; set; }

        public int NivelAdvertencia { get; set; }

        public DescuentoCmd Descuentos { get; set; }

        public MultimediaCmd Multimedia { get; set; }

        public string Marca { get; set; }

        public bool Estado { get; set; }

        public List<string> Sinonimos { get; set; }

        public double Calificacion { get; set; }

        public bool EnAlmacen()
        {
            return (this.NivelInventario > 0) ? true : false;
        }


    }

}
