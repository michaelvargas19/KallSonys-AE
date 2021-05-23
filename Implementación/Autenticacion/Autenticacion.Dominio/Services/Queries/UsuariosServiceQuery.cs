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
    public class UsuariosServiceQuery : IUsuariosServiceQuery
    {
        private readonly IUnitOfWork _ufw;
        
        public UsuariosServiceQuery(IConfiguration configuration,
                               IUnitOfWork ufw)
        {
            this._ufw = ufw;
            
        }

        public UsuarioQuery consultarUsuario(string usuario)
        {
            UsuarioQuery usrQ = null;

            try
            {

                Usuario usrE = _ufw.UserIdentityRepository().ReadByNameUser(usuario);
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

                

            }
            catch (Exception e)
            {
                throw e;
            }
            return usrQ;
        }

        public UsuarioQuery consultarUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsuarioQuery> getUsuarios(string usuario)
        {
            throw new NotImplementedException();
        }



    }
}
