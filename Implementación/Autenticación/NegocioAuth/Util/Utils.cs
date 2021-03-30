using DominioAuth.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace NegocioAuth.Util
{
    public static class Utils
    {
        public static string GenerarCodigoAplicacion()
        {
            Random obj = new Random();
            int longitud = 5;
            int bloques = 5;
            string sCadena = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int opciones = sCadena.Length;
            char cletra;
            string sNuevacadena = string.Empty;

            for (int b = 0; b < bloques; b++) {

                if(b>0)
                    sNuevacadena += "-";

                for (int i = 0; i < longitud; i++)
                {
                    cletra = sCadena[obj.Next(opciones)];
                    sNuevacadena += cletra.ToString();
                }
                
            }

            return sNuevacadena;

        }

        public static string GenerarCodigoRol()
        {
            Random obj = new Random();
            int longitud = 4;
            int bloques = 5;
            string sCadena = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char cletra;
            string sNuevacadena = string.Empty;

            for (int b = 0; b < bloques; b++)
            {

                if (b > 0)
                    sNuevacadena += "-";

                for (int i = 0; i < longitud; i++)
                {
                    cletra = sCadena[obj.Next(longitud)];
                    sNuevacadena += cletra.ToString();
                }

            }

            return sNuevacadena;

        }


        //------------- [Aplicaciones]
        public static Configuracion obtenerConfiguracion(Aplicacion aplicacion, string issuer, TimeSpan clockSkew)
        {
            Configuracion conf = new Configuracion();

            conf.IdAplicacion = aplicacion.IdAplicacion;
            conf.Nombre = aplicacion.Nombre;
            conf.Algoritmo = aplicacion.AlgoritmoDeSeguridad;
            conf.MinutosDeVida = (aplicacion.MinutosDeVida == null) ? 0 : aplicacion.MinutosDeVida.Value;
            conf.Issuer = issuer;
            conf.Audience = aplicacion.IdAplicacion;
            conf.ClockSkew = clockSkew;
            conf.Roles = new List<string>();

            foreach (Rol rol in aplicacion.Roles)
                conf.Roles.Add(rol.Display);

            return conf;
        }

    }
}
