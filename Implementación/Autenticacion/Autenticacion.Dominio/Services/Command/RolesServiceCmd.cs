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
    public class RolesServiceCmd : IRolesServiceCmd
    {
        private readonly string IdApp;
        private readonly string Issuer;
        private readonly IUnitOfWork _ufw;
        
        public RolesServiceCmd(IConfiguration configuration,
                               IUnitOfWork ufwAplicacion)
        {
            
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["JwtConfig:issuer"];
            this._ufw = ufwAplicacion;
            
        }

        public RolCmd actualizarRol(RolCmd rol)
        {
            throw new NotImplementedException();
        }

        public RolCmd registrarRol(RolCmd rol)
        {
            throw new NotImplementedException();
        }
    }
}
