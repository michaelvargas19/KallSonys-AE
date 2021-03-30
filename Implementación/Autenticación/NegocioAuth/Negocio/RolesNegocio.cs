using DominioAuth.Modelo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NegocioAuth.Interfaces;
using PersistenciaAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;

namespace NegocioAuth.Negocio
{
    public class RolesNegocio : IRolesNegocio
    {
        private readonly IRepositoryWrapper repository;
        private readonly RoleManager<Rol> managerRole;
        private readonly UserManager<Usuario> managerUser;
        private string IdApp;
        private string Issuer;

        public RolesNegocio(IRepositoryWrapper repository,
                                  IConfiguration configuration,
                                  RoleManager<Rol> managerRole,
                                  UserManager<Usuario> managerUser)
        {
            this.repository = repository;
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["JwtConfig:issuer"];
            this.managerRole = managerRole;
            this.managerUser = managerUser;
        }

        public Rol Actualizar(Rol Rol)
        {
            return repository.Roles.Actualizar(Rol);
        }

        public Rol Borrar(Rol Rol)
        {
            return repository.Roles.Borrar(Rol);
        }

        public IEnumerable<Rol> Buscar(Expression<Func<Rol, bool>> expression, string[] includes)
        {
            return repository.Roles.Buscar(expression, includes);
        }

        public IEnumerable<Rol> BuscarPor(Expression<Func<Rol, bool>> predicate, params Expression<Func<Rol, object>>[] includes)
        {
            return repository.Roles.BuscarPor(predicate, includes);
        }

        public IEnumerable<Rol> BuscarPorCondicion(Expression<Func<Rol, bool>> expression)
        {
            return repository.Roles.BuscarPorCondicion(expression);
        }

        public Rol BuscarPrimero()
        {
            return repository.Roles.BuscarPrimero();
        }

        public Rol BuscarPrimero(Expression<Func<Rol, bool>> expression)
        {
            return repository.Roles.BuscarPrimero(expression);
        }

        public IEnumerable<Rol> BuscarTodos()
        {
            return repository.Roles.BuscarTodos();
        }

        public Rol Crear(Rol rol)
        {
            try
            {
                
                rol = repository.Roles.Crear(rol);
            
            }
            catch (Exception e)
            {
                throw e;
            }

            return rol;
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

        public Aplicacion verRoles(string idAplicacion)
        {
            Aplicacion aplicacion = null;

            try
            {
                aplicacion = repository.ContextDB.Aplicaciones.Where(a => a.IdAplicacion == idAplicacion && a.Estado == true && a.PermiteJWT == true)
                                                                         .Include(a => a.Roles).FirstOrDefault();

            }
            catch (Exception e)
            {
                throw e;
            }
            return aplicacion;
        }
    }
}
