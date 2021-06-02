using Clientes.Dominio.Modelo;
using Clientes.Dominio.Modelo.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Clientes.Dominio.Models
{
    [BsonCollection("Metodos_Pago")]
    public class Ref_Metodos_Pago: Document
    {
        [Required(ErrorMessage = "Valor obligatorio")]
        public string codigo_metodo_pago { get; set; }
        [Required(ErrorMessage = "Valor obligatorio")]
        public string descripcion_metodo_pago { get; set; }
    }
}
