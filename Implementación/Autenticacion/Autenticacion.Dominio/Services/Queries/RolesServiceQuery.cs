using Autenticacion.Dominio.IServices.Command;
using Autenticacion.Dominio.IServices.Queries;
using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using Autenticacion.Dominio.Services.Command;
using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.Specification;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace Autenticacion.Dominio.Services.Queries
{
    public class RolesServiceQuery : IRolesServiceQuery
    {
        private readonly IUnitOfWork _ufw;
        
        public RolesServiceQuery(IConfiguration configuration,
                               IUnitOfWork ufw)
        {
            this._ufw = ufw;
            
        }


        public IEnumerable<RolQuery> rolesPorAplicacion(string idAplicacion)
        {
            List<RolQuery> roles = null;

            try
            {
                Aplicacion app = _ufw.RepositoryQueryAplicacion().Find(new AplicacionSpecification(idAplicacion)).FirstOrDefault();

                if (app == null)
                {
                    throw new Exception("La aplicación es inválida");
                }

                
                roles = new List<RolQuery>();
                
                foreach (Rol r in app.Roles)
                {
                    RolQuery rol = new RolQuery();
                    rol.Id = r.Id;
                    rol.Nombre = r.Display;
                    rol.Descripcion = r.Descripcion;

                    roles.Add(rol);
                }

            }
            catch(Exception e)
            {
                throw e;
            }

            return roles;
        }


        public UsuarioQuery verRolesPorUsuario(string usuario)
        {
            UsuarioQuery usrQ = null;

            try
            {

                Usuario usrE = _ufw.UserIdentityRepository().ReadByNameUser(usuario);

                if (usrE == null)
                {
                    throw new Exception("El usuario es inválido");
                }

                List<Rol> roles =_ufw.RoleIdentityRepository().GetAllRoles(usrE).ToList();


                usrQ = new UsuarioQuery();
                usrQ.IdUsuario = usrE.Id;
                usrQ.Usuario = usrE.UserName;
                usrQ.Nombres = usrE.Nombres;
                usrQ.Apellidos = usrE.Apellidos;
                usrQ.Identificacion = usrE.Identificacion;
                usrQ.TelefonoMovil = usrE.PhoneNumber;
                usrQ.Identificacion = usrE.Identificacion;
                usrQ.Organizacion = usrE.Organizacion;
                usrQ.Cargo = usrE.Cargo;
                usrQ.Description = usrE.Description;
                usrQ.EsExterno = usrE.EsExterno;


                usrQ.Roles = new List<RolQuery>();

                foreach (Rol r in roles)
                {
                    RolQuery rolQ = new RolQuery();
                    rolQ.Id = r.Id;
                    rolQ.Nombre = r.Display;
                    rolQ.Descripcion = r.Descripcion;
                    usrQ.Roles.Add(rolQ);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return usrQ;
        }

        public UsuarioQuery verRolesPorUsuario_Aplicacion(string usuario, string idAplicacion)
        {
            UsuarioQuery usrQ = null;

            try
            {

                Usuario usrE = _ufw.UserIdentityRepository().ReadByNameUser(usuario);

                if (usrE == null)
                {
                    throw new Exception("El usuario es inválido");
                }

                List<Rol> roles = _ufw.RoleIdentityRepository().GetAllRoles(usrE).Where(r=> r.IdAplicacion == idAplicacion).ToList();


                usrQ = new UsuarioQuery();
                usrQ.IdUsuario = usrE.Id;
                usrQ.Usuario = usrE.UserName;
                usrQ.Nombres = usrE.Nombres;
                usrQ.Apellidos = usrE.Apellidos;
                usrQ.Identificacion = usrE.Identificacion;
                usrQ.TelefonoMovil = usrE.PhoneNumber;
                usrQ.Identificacion = usrE.Identificacion;
                usrQ.Organizacion = usrE.Organizacion;
                usrQ.Cargo = usrE.Cargo;
                usrQ.Description = usrE.Description;
                usrQ.EsExterno = usrE.EsExterno;


                usrQ.Roles = new List<RolQuery>();

                foreach (Rol r in roles)
                {
                    RolQuery rolQ = new RolQuery();
                    rolQ.Id = r.Id;
                    rolQ.Nombre = r.Display;
                    rolQ.Descripcion = r.Descripcion;
                    usrQ.Roles.Add(rolQ);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return usrQ;
        }



    }
}
