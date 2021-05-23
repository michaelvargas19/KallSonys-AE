using Autenticacion.Infraestructura;
using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.IRepositories.Command;
using Autenticacion.Infraestructura.IRepositories.Queries;
using Autenticacion.Infraestructura.ISpecification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.IUnitOfWorks
{
    public interface IUnitOfWork : IDisposable 
    {

        // Persistencia
        void BeginTransaction();
        void SaveChanges();
        void Commit();
        void Rollback();


        public ContextoAuthDB ContextoAuthDB();


        //  Command
        IRepositoryBaseCommand<Usuario> RepositoryCommandUsuario();
        IRepositoryBaseCommand<Rol> RepositoryCommandRol();
        IRoleIdentityRepository RoleIdentityRepository();
        IRepositoryBaseCommand<Aplicacion> RepositoryCommandAplicacion();
        IRepositoryBaseCommand<AlgoritmoDeSeguridad> RepositoryCommandAlgoritmo();
        IRepositoryBaseCommand<Token> RepositoryCommandToken();
        IRepositoryBaseCommand<_LogAutenticacionAPI> RepositoryCommandLog();
        IRepositorySessionesCmd RepositorySessionesCmd();


        //  Queries
        IRepositoryBaseQuery<Usuario> RepositoryQueryUsuario();
        IRepositoryBaseQuery<Rol> RepositoryQueryRol();
        IRepositoryBaseQuery<TipoAutenticacion> RepositoryQueryTipoAuth();
        IRepositoryBaseQuery<Aplicacion> RepositoryQueryAplicacion();
        IRepositoryBaseQuery<AlgoritmoDeSeguridad> RepositoryQueryAlgoritmo();
        IRepositoryBaseQuery<Token> RepositoryQueryToken();
        IRepositoryBaseQuery<_LogAutenticacionAPI> RepositoryQueryLog();
        IRepositorySessionesQueries RepositorySessionesQueries();
        IUserIdentityRepository UserIdentityRepository();

        //Log
        void InsertarLog(_LogAutenticacionAPI log);
    }
}
