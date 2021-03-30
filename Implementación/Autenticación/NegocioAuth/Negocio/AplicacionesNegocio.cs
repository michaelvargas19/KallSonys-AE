using DominioAuth.Modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NegocioAuth.Interfaces;
using PersistenciaAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NegocioAuth.Negocio
{
    public class AplicacionesNegocio : IAplicacionesNegocio
    {
        private readonly IRepositoryWrapper repository;
        private string IdApp;
        private string Issuer;

        public AplicacionesNegocio(IRepositoryWrapper repository,
                                  IConfiguration configuration)
        {
            this.repository = repository;
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["JwtConfig:issuer"];
        }

        public Aplicacion Actualizar(Aplicacion aplicacion)
        {
            return repository.Aplicaciones.Actualizar(aplicacion);
        }

        public Aplicacion Borrar(Aplicacion aplicacion)
        {
            return repository.Aplicaciones.Borrar(aplicacion);
        }

        public IEnumerable<Aplicacion> Buscar(Expression<Func<Aplicacion, bool>> expression, string[] includes)
        {
            return repository.Aplicaciones.Buscar(expression, includes);
        }

        public IEnumerable<Aplicacion> BuscarPor(Expression<Func<Aplicacion, bool>> predicate, params Expression<Func<Aplicacion, object>>[] includes)
        {
            return repository.Aplicaciones.BuscarPor(predicate, includes);
        }

        public IEnumerable<Aplicacion> BuscarPorCondicion(Expression<Func<Aplicacion, bool>> expression)
        {
            return repository.Aplicaciones.BuscarPorCondicion(expression);
        }

        public Aplicacion BuscarPrimero()
        {
            return repository.Aplicaciones.BuscarPrimero();
        }

        public Aplicacion BuscarPrimero(Expression<Func<Aplicacion, bool>> expression)
        {
            return repository.Aplicaciones.BuscarPrimero(expression);
        }

        public IEnumerable<Aplicacion> BuscarTodos()
        {
            return repository.Aplicaciones.BuscarTodos();
        }

        public Aplicacion Crear(Aplicacion aplicacion)
        {
            do 
            { 
                aplicacion.IdAplicacion = Util.Utils.GenerarCodigoAplicacion();

            } while( !(repository.Aplicaciones.BuscarPorCondicion(a=> a.IdAplicacion == aplicacion.IdAplicacion).FirstOrDefault() == null ) );
            
            return repository.Aplicaciones.Crear(aplicacion);
        }

        public Configuracion verConfiguracion(string IdAplicacion)
        {
            Configuracion configuracion = null;

            try
            {
                Aplicacion aplicacion = repository.ContextDB.Aplicaciones.Where(a => a.IdAplicacion == IdAplicacion && a.Estado == true && a.PermiteJWT == true)
                                                                         .Include(a=> a.Roles).FirstOrDefault();

                if (aplicacion != null)
                {
                    configuracion = Util.Utils.obtenerConfiguracion(aplicacion, this.Issuer, TimeSpan.Zero);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return configuracion;
        }

    }
}
