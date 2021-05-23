using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Command
{
    public class UsuarioCmd
    {
        
        public string Usuario { get; set; }
        
        public string Email { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Contrasena { get; set; }

        public string Identificacion { get; set; }

        public string TelefonoMovil { get; set; }

        public int IdTipoAuth { get; set; }

        public string Organizacion { get; set; }

        public string Cargo { get; set; }

        public string Description { get; set; }

        public bool EsExterno { get; set; }


        public bool EsValido()
        {
            if ((EsExterno))
            {
                if ((Organizacion != null) && (Cargo != null) && (Description != null))
                {
                    return true;
                }
                return false;
            }

            return true;
        }

    }
}
