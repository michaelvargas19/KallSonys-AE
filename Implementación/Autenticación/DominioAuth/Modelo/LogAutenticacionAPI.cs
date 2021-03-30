using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DominioAuth.Modelo
{
    [Table("_LogAutenticacionAPI")]
    public class _LogAutenticacionAPI
    {
        public _LogAutenticacionAPI(string tipo, string usuario, string aplicacion, string metodo, string entidad, bool esExcepcion, string mensaje, string parametros)
        {
            Tipo = tipo;
            FechaRegistro = DateTime.Now;
            Request = "";
            Response = "";
            Aplicacion = aplicacion;
            Metodo = metodo;
            Entidad = entidad;
            EsExcepcion = esExcepcion;
            Mensaje = mensaje;
            Parametros = parametros;
            Usuario = usuario;
        }

        public _LogAutenticacionAPI(string tipo, string usuario, string aplicacion, string metodo, string entidad, string request, string response, bool esExcepcion, string mensaje, string parametros)
        {
            Tipo = tipo;
            FechaRegistro = DateTime.Now;
            Aplicacion = aplicacion;
            Metodo = metodo;
            Entidad = entidad;
            EsExcepcion = esExcepcion;
            Request = request;
            Response = response;
            Mensaje = mensaje;
            Parametros = parametros;
            Usuario = usuario;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLog { get; set; }

        
        [Required]
        public string Tipo { get; set; }

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
        public string Aplicacion { get; set; }

        [Required]
        public string Metodo { get; set; }

        [Required]
        public string Entidad { get; set; }

        [Required]
        public bool EsExcepcion { get; set; }

        [Required]
        public string Mensaje { get; set; }

        [Required]
        public string Parametros { get; set; }
    }
}
