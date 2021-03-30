using DominioAuth.Modelo;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PersistenciaAuth.Interfaces;
using PersistenciaAuth.Repositorios;
using System;

namespace PersistenciaAuth
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ContextoAuthDB ContextoAuthDB;
        private SignInManager<Usuario> signInManager;
        private UserManager<Usuario> userManager;
        private RoleManager<Rol> rolesManager;
        private ILogRepository repositoryLogin;
        private ISesionesRepository repositorySesiones;
        private IAplicacionesRepository repositoryAplicaciones;
        private IRolesRepository repositoryRoles;
        private IUsuariosRepository repositoryUsuarios;
        
        public RepositoryWrapper(ContextoAuthDB dbContext,
                                  SignInManager<Usuario> signInManager,
                                  UserManager<Usuario> userManager,
                                  RoleManager<Rol> rolesManager)
        {
            this.ContextoAuthDB = dbContext;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.rolesManager = rolesManager;
        }

        public ContextoAuthDB ContextDB 
        {
            get
            {
                return ContextoAuthDB;
            }
        }
        public ILogRepository Log
        {
            get
            {
                if (repositoryLogin == null)
                {
                    this.repositoryLogin = new LogRepository(this.ContextoAuthDB);
                }
                return repositoryLogin;
            }
        }
        public ISesionesRepository Sesiones
        {
            get
            {
                if (repositorySesiones == null)
                {
                    repositorySesiones = new SesionesRepository(this.ContextoAuthDB, this.signInManager, this.userManager);
                }
                return repositorySesiones;
            }
        }


        public IAplicacionesRepository Aplicaciones
        {
            get
            {
                if (repositoryAplicaciones == null)
                {
                    repositoryAplicaciones = new AplicacionesRepository(this.ContextoAuthDB);
                }
                return repositoryAplicaciones;
            }
        }

        public IRolesRepository Roles
        {
            get
            {
                if (repositoryRoles == null)
                {
                    repositoryRoles = new RolesRepository(this.ContextoAuthDB, this.rolesManager, this.userManager);
                }
                return repositoryRoles;
            }
        }
        
        public IUsuariosRepository Usuarios
        {
            get
            {
                if (repositoryUsuarios == null)
                {
                    repositoryUsuarios = new UsuariosRepository(this.ContextoAuthDB, userManager);
                }
                return repositoryUsuarios;
            }
        }
        
        public void InsertarLog(_LogAutenticacionAPI log)
        {
            this.ContextoAuthDB.Log(log);

        }
    }
}
