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

namespace NegocioAuth.Negocio
{
    public class UsuariosNegocio : IUsuariosNegocio
    {
        private readonly IRepositoryWrapper repository;
        private string IdApp;
        private string Issuer;

        public UsuariosNegocio(IRepositoryWrapper repository,
                                  IConfiguration configuration)
        {
            this.repository = repository;
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["JwtConfig:issuer"];
        }

        public Usuario Actualizar(Usuario usuario)
        {
            return repository.Usuarios.Actualizar(usuario);
        }

        public Usuario Borrar(Usuario usuario)
        {
            return repository.Usuarios.Borrar(usuario);
        }

        public IEnumerable<Usuario> Buscar(Expression<Func<Usuario, bool>> expression, string[] includes)
        {
            return repository.Usuarios.Buscar(expression, includes);
        }

        public IEnumerable<Usuario> BuscarPor(Expression<Func<Usuario, bool>> predicate, params Expression<Func<Usuario, object>>[] includes)
        {
            return repository.Usuarios.BuscarPor(predicate, includes);
        }

        public IEnumerable<Usuario> BuscarPorCondicion(Expression<Func<Usuario, bool>> expression)
        {
            return repository.Usuarios.BuscarPorCondicion(expression);
        }

        public Usuario BuscarPrimero()
        {
            return repository.Usuarios.BuscarPrimero();
        }

        public Usuario BuscarPrimero(Expression<Func<Usuario, bool>> expression)
        {
            return repository.Usuarios.BuscarPrimero(expression);
        }

        public IEnumerable<Usuario> BuscarTodos()
        {
            return repository.Usuarios.BuscarTodos();
        }

        public Usuario Crear(Usuario usuario)
        {
            return repository.Usuarios.Crear(usuario);
        }

        public IdentityResult Crear(Usuario usuario, Credenciales credenciales)
        {
            try {
                
                return repository.Usuarios.Crear(usuario, credenciales);

            } catch (Exception e) 
            {
                throw e;
            }
            
        }

    }
}
