using System;
using System.Collections.Generic;
using System.Text;

namespace Inventarios.Dominio.Modelo.Command
{
    public class CatalogoCmd
    {

        public string CodigoCatalogo { get; set; }

        public string Nombre { get; set; }
        
        public string Descripcion { get; set; }
        
        public ProveedorCmd Proveedor { get; set; }

        public MultimediaCmd Multimedia { get; set; }

        public string[] Categorias { get; set; }

        public bool Estado { get; set; }

        public bool IndExterno { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public string[] Sinonimos { get; set; }

        public double Calificacion { get; set; }

        public IEnumerable<ProductoCmd> Productos { get; set; }


        public void Validar()
        {
            bool valido = false;

            if ((IndExterno))
            {
                if ((Proveedor == null) || ((Proveedor.IdProveedor.Length == 0) && (Proveedor.Nombre.Length == 0)))
                {
                    throw new Exception("Los catálogos externos requieren proveedor");
                }
            }

        }

    }
}
