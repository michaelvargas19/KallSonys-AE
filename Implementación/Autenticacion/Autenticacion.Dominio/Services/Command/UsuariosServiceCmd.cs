using Autenticacion.Dominio.IServices.Command;
using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.IRepositories.Command;
using Autenticacion.Infraestructura.Specification;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Autenticacion.Dominio.Services.Command
{
    public class UsuariosServiceCmd : IUsuariosServiceCmd
    {
        private readonly string IdApp;
        private readonly string Issuer;
        private readonly IUnitOfWork _ufw;
        
        public UsuariosServiceCmd(IConfiguration configuration,
                               IUnitOfWork ufwAplicacion)
        {
            
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["JwtConfig:issuer"];
            this._ufw = ufwAplicacion;
            
        }

        public UsuarioQuery registrarUsuario(UsuarioCmd usuario)
        {
            UsuarioQuery usrQ = null;

            try
            {

                if (!usuario.EsValido())
                {
                    throw new Exception("La información no es válida");
                }

                if ( (_ufw.RepositoryQueryUsuario().Find(new UsuarioSpecification(usuario.Usuario, usuario.Email)).ToList().Count > 0 ) )
                {
                    throw new Exception("Ya existe un usuario registrado con el Usuario/Email");
                }

                if ( (_ufw.RepositoryQueryTipoAuth().Find(new TipoAuthSpecification(usuario.IdTipoAuth)).ToList().Count == 0 ) )
                {
                    throw new Exception("El Tipo de Autenticación es inválido");
                }

                Usuario usrE = new Usuario();
                usrE.UserName = usuario.Usuario;
                usrE.Nombres = usuario.Nombres;
                usrE.Apellidos = usuario.Apellidos;
                usrE.Identificacion = usuario.Identificacion;
                usrE.PhoneNumber = usuario.TelefonoMovil;
                usrE.IdTipoAuth = usuario.IdTipoAuth;
                usrE.Organizacion = usuario.Organizacion;
                usrE.Cargo = usuario.Cargo;
                usrE.Description = usuario.Description;
                usrE.EsExterno = usuario.EsExterno;

                _ufw.BeginTransaction();
                _ufw.UserIdentityRepository().CreateUser(usrE, usuario.Contrasena);
                _ufw.SaveChanges();
                _ufw.Commit();

                usrE = _ufw.RepositoryQueryUsuario().Find(new UsuarioSpecification(usrE.UserName)).FirstOrDefault();

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


                //usrE.Roles = new List<Rol   >();

                //foreach (Rol r in usrE.Roles)
                //{
                //    RolQuery rolQ = new RolQuery();
                //    rolQ.Id = r.Id;
                //    rolQ.Nombre = r.Name;
                //    rolQ.Descripcion = r.Descripcion;
                //    usrQ.Roles.Add(rolQ);
                //}
                
                
            }
            catch (Exception e)
            {
                _ufw.Rollback();
                throw e;
            }


            return usrQ;

        }

        public UsuarioQuery actualizarUsuario(UsuarioCmd usuario)
        {
            throw new NotImplementedException();
        }

        public ResultadoCmd asignarRol(AsignarRolUserCmd rol)
        {
            try
            {
                ResultadoCmd result = new ResultadoCmd();
                result.Proceso = "Asignar Rol";
                result.Exitoso = false;

                IdentityResult rest = this._ufw.UserIdentityRepository().AsigRoleUser(rol.Usuario, rol.IdRol);

                if (rest.Succeeded)
                {
                    result.Exitoso = true;
                    result.Mensaje = "Rol asignado";
                }
                else
                {
                    result.Mensaje = "Ha habido un problema en la asignación";
                }

                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public ResultadoCmd RemoverAsignarRol(AsignarRolUserCmd rol)
        {
            try
            {
                ResultadoCmd result = new ResultadoCmd();
                result.Proceso = "Remover Rol-Usuario";
                result.Exitoso = false;

                IdentityResult rest = this._ufw.UserIdentityRepository().RemoveAsigRoleUser(rol.Usuario, rol.IdRol);

                if (rest.Succeeded)
                {
                    result.Exitoso = true;
                    result.Mensaje = "Rol removido";
                }
                else
                {
                    result.Mensaje = "Ha habido un problema";
                }

                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
        }



    }
}
