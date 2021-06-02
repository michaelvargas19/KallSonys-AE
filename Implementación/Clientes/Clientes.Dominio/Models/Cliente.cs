using Clientes.Dominio.Modelo;
using Clientes.Dominio.Modelo.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Clientes.Dominio.Models
{
    [BsonCollection("Cliente")]
    public class Cliente : Document
    {
        [Required(ErrorMessage = "Valor obligatorio")]
        public string correo_cliente { get; set; }
        //[Required(ErrorMessage = "Valor obligatorio")]
        public string nombres_cliente { get; set; }
        //[Required(ErrorMessage = "Valor obligatorio")]
        public string nombre_usuario { get; set; }
        //[Required(ErrorMessage = "Valor obligatorio")]
        public string password_cliente { get; set; }

        //[Required(ErrorMessage = "Valor obligatorio")]
        public string direccion_cliente { get; set; }

        //[Required(ErrorMessage = "Valor obligatorio")]
        public string telefono_cliente { get; set; }
        public string ciudad_cliente { get; set; }
        public string pais_cliente { get; set; }
        public Cliente_Metodo_Pago metodo_pago { get; set; }
    }
}
