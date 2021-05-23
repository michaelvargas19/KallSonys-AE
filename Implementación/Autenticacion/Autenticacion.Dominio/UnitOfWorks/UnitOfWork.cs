using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Infraestructura;
using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.IRepositories.Command;
using Autenticacion.Infraestructura.IRepositories.Queries;
using Autenticacion.Infraestructura.ISpecification;
using Autenticacion.Infraestructura.Repositories.Command;
using Autenticacion.Infraestructura.Repositories.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContextoAuthDB _contexto;
        private IRepositoryBaseCommand<Usuario> _repoCommandUsuario;
        private IRepositoryBaseCommand<Rol> _repoCommandRol;
        private IRoleIdentityRepository _repoRolIdentity;
        private IUserIdentityRepository _repoUsuarioIdentity;
        private IRepositoryBaseCommand<Aplicacion> _repoCommandAplicacion;
        private IRepositoryBaseCommand<AlgoritmoDeSeguridad> _repoCommandAlgoritmo;
        private IRepositoryBaseCommand<Token> _repoCommandToken;
        private IRepositoryBaseCommand<_LogAutenticacionAPI> _repoCommandLog;
        private readonly IRepositorySessionesCmd _repoSessionCmd;


        private readonly IConfiguration configuration;
        private readonly RoleManager<Rol> roleManager;
        private readonly UserManager<Usuario> userManager;


        private IRepositoryBaseQuery<Rol> _repoQueryRol;
        private IRepositoryBaseQuery<Usuario> _repoQueryUsuario;
        private IRepositoryBaseQuery<TipoAutenticacion> _repoQueryTipoAuth;
        private IRepositoryBaseQuery<Aplicacion> _repoQueryAplicacion;
        private IRepositoryBaseQuery<AlgoritmoDeSeguridad> _repoQueryAlgoritmo;
        private IRepositoryBaseQuery<Token> _repoQueryToken;
        private IRepositoryBaseQuery<_LogAutenticacionAPI> _repoQueryLog;
        private readonly IRepositorySessionesQueries _repoSessionQueries;


        public UnitOfWork(ContextoAuthDB contexto,
                          IRepositorySessionesCmd repoSession,
                          IRepositorySessionesQueries repoSessionQueries,
                          IConfiguration configuration,
                          RoleManager<Rol> managerR,
                          UserManager<Usuario> managerU)
        {
            this._contexto = contexto;
            this._repoSessionCmd = repoSession;
            this._repoSessionQueries = repoSessionQueries;

            this.configuration = configuration;
            this.roleManager = managerR;
            this.userManager = managerU;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            if (this._contexto.Database.CurrentTransaction == null)
                this._contexto.Database.BeginTransaction();
        }
        public void SaveChanges()
        {
            this._contexto.SaveChanges();
        }
        public void Commit()
        {
            if (this._contexto.Database.CurrentTransaction != null)
                this._contexto.Database.CommitTransaction();
        }
        public void Rollback()
        {
            if(this._contexto.Database.CurrentTransaction != null)
                this._contexto.Database.RollbackTransaction();
        }

        public ContextoAuthDB ContextoAuthDB()
        {
            return this._contexto;
        }


        public IRepositoryBaseCommand<Usuario> RepositoryCommandUsuario()
        {
            if (this._repoCommandUsuario == null)
            {
                this._repoCommandUsuario = new RepositoryBaseCommand<Usuario>(this._contexto);
            }
            return this._repoCommandUsuario;
        }

        public IRepositoryBaseCommand<Rol> RepositoryCommandRol()
        {

            if (this._repoCommandRol == null)
            {
                this._repoCommandRol = new RepositoryBaseCommand<Rol>(this._contexto);
            }
            return this._repoCommandRol;
        }



        public IUserIdentityRepository UserIdentityRepository()
        {

            if (this._repoUsuarioIdentity == null)
            {
                this._repoUsuarioIdentity = new UserIdentityRepository(this.configuration, this.userManager, this.roleManager, this._contexto);
            }
            return this._repoUsuarioIdentity;
        }
        public IRoleIdentityRepository RoleIdentityRepository()
        {

            if (this._repoRolIdentity == null)
            {
                this._repoRolIdentity = new RoleIdentityRepository(this.configuration, this.roleManager, this.userManager, this._contexto);
            }
            return this._repoRolIdentity;
        }

        public IRepositoryBaseCommand<Aplicacion> RepositoryCommandAplicacion()
        {
            if (this._repoCommandAplicacion == null)
            {
                this._repoCommandAplicacion = new RepositoryBaseCommand<Aplicacion>(this._contexto);
            }
            return this._repoCommandAplicacion;
        }

        public IRepositoryBaseCommand<AlgoritmoDeSeguridad> RepositoryCommandAlgoritmo()
        {
            if (this._repoCommandAlgoritmo == null)
            {
                this._repoCommandAlgoritmo = new RepositoryBaseCommand<AlgoritmoDeSeguridad>(this._contexto);
            }
            return this._repoCommandAlgoritmo;
        }

        public IRepositoryBaseCommand<Token> RepositoryCommandToken()
        {
            if (this._repoCommandToken == null)
            {
                this._repoCommandToken = new RepositoryBaseCommand<Token>(this._contexto);
            }
            return this._repoCommandToken;
        }

       
        public IRepositoryBaseCommand<_LogAutenticacionAPI> RepositoryCommandLog()
        {
            if (this._repoCommandLog == null)
            {
                this._repoCommandLog = new RepositoryBaseCommand<_LogAutenticacionAPI>(this._contexto);
            }
            return this._repoCommandLog;
        }


        public IRepositorySessionesCmd RepositorySessionesCmd()
        {
            return this._repoSessionCmd;
        }







        //  Queries

        public IRepositorySessionesQueries RepositorySessionesQueries()
        {
            return this._repoSessionQueries;
        }


        #region Query
        public IRepositoryBaseQuery<Rol> RepositoryQueryRol()
        {
            if (this._repoQueryRol == null)
            {
                this._repoQueryRol = new RepositoryBaseQuery<Rol>(this._contexto);
            }
            return this._repoQueryRol;
        }



        public IRepositoryBaseQuery<TipoAutenticacion> RepositoryQueryTipoAuth()
        {

            if (this._repoQueryTipoAuth == null)
            {
                this._repoQueryTipoAuth = new RepositoryBaseQuery<TipoAutenticacion>(this._contexto);
            }
            return this._repoQueryTipoAuth;
        }


        public IRepositoryBaseQuery<Usuario> RepositoryQueryUsuario()
        {
            if (this._repoQueryUsuario == null)
            {
                this._repoQueryUsuario = new RepositoryBaseQuery<Usuario>(this._contexto);
            }
            return this._repoQueryUsuario;
        }

        public IRepositoryBaseQuery<Aplicacion> RepositoryQueryAplicacion()
        {
            if (this._repoQueryAplicacion == null)
            {
                this._repoQueryAplicacion = new RepositoryBaseQuery<Aplicacion>(this._contexto);
            }
            return this._repoQueryAplicacion;
        }

        public IRepositoryBaseQuery<AlgoritmoDeSeguridad> RepositoryQueryAlgoritmo()
        {
            if (this._repoQueryAlgoritmo == null)
            {
                this._repoQueryAlgoritmo = new RepositoryBaseQuery<AlgoritmoDeSeguridad>(this._contexto);
            }
            return this._repoQueryAlgoritmo;
        }

        public IRepositoryBaseQuery<Token> RepositoryQueryToken()
        {
            if (this._repoQueryToken == null)
            {
                this._repoQueryToken = new RepositoryBaseQuery<Token>(this._contexto);
            }
            return this._repoQueryToken;
        }


        public IRepositoryBaseQuery<_LogAutenticacionAPI> RepositoryQueryLog()
        {
            if (this._repoQueryLog == null)
            {
                this._repoQueryLog = new RepositoryBaseQuery<_LogAutenticacionAPI>(this._contexto);
            }
            return this._repoQueryLog;
        }


        #endregion





        public void InsertarLog(_LogAutenticacionAPI log)
        {
            try
            {

                this.BeginTransaction();
                this._contexto.Logs.Add(log);
                this._contexto.SaveChanges();
                this.Commit();

            }
            catch (Exception e)
            {
                this.Rollback();
                throw e;
            }
        }

    }
}
