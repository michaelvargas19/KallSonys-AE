using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Queries
{
    public class RolQuery
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }



    }
}
