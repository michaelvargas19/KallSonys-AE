using Clientes.Dominio.Modelo;
using Clientes.Dominio.Modelo.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Clientes.Dominio.Models
{
    [BsonCollection(" Cliente_Metodo_Pago")]
    public class Cliente_Metodo_Pago : Document
    {
        [Required(ErrorMessage = "Valor obligatorio")]
        public string codigo_metodo_pago { get; set; }
        public string numero_tarjeta_credito { get; set; }
 
        public string detalles_metodo_pago { get; set; }
    }
}
