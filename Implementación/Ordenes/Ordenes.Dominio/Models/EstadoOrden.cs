using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ordenes.Dominio.Modelo;
using Ordenes.Dominio.Modelo.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ordenes.Dominio.Models
{
    [BsonCollection("EstadoOrden")]
    public class EstadoOrden : Document
    {
        //[Key]
        //[Range(0, int.MaxValue, ErrorMessage = "id no valido")]
        //[BsonId]
       
        public int id_estado { get; set; }


        //[Required(ErrorMessage="Por favor ingresar el nombre del cliente")]
        //[StringLength(50, MinimumLength = 1)]
        //[DataType(DataType.Text)]
        public string descripcion_estado { get; set; }

        //[StringLength(50, MinimumLength = 1)]
        //[DataType(DataType.Text)]
        public string tipo_estado { get; set; }
    }
}
