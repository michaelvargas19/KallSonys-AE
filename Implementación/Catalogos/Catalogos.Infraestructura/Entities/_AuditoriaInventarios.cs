using Catalogos.Infraestructura.SettinsDB;
using System;
using System.ComponentModel.DataAnnotations;

namespace Catalogos.Infraestructura.Entities
{
    [BsonCollection("_AuditoriaCatalogos")]
    public class _AuditoriaCatalogos : Document
    {

        public _AuditoriaCatalogos(string tipo, string referencia, bool esError, string usuario, string metodo, string entidad, string request, string response, string mensaje, string parametros)
        {
            Tipo = tipo;
            Referencia = referencia;
            EsError = esError;
            Usuario = usuario;
            FechaRegistro = DateTime.Now;
            Metodo = metodo;
            Entidad = entidad;
            Request = request;
            Response = response;
            Mensaje = mensaje;
            Parametros = parametros;
        }



        [Required]
        public string Tipo { get; set; }

        [Required]
        public string Referencia { get; set; }

        [Required]
        public bool EsError { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaRegistro { get; set; }

        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Request { get; set; }

        [Required]
        public string Response { get; set; }

        [Required]
        public string Metodo { get; set; }

        [Required]
        public string Entidad { get; set; }

        [Required]
        public string Mensaje { get; set; }

        [Required]
        public string Parametros { get; set; }

    }
}
