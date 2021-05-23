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
    public class AplicacionesServiceQuery : IAplicacionesServiceQuery
    {
        private readonly IUnitOfWork _ufw;
        
        public AplicacionesServiceQuery(IConfiguration configuration,
                               IUnitOfWork ufw)
        {
            this._ufw = ufw;
            
        }


        public AplicacionQuery consultarAplicacion(string idAplicacion)
        {
            AplicacionQuery appQ = null;

            try
            {
                Aplicacion app = _ufw.RepositoryQueryAplicacion().Find(new AplicacionSpecification(idAplicacion)).FirstOrDefault();

                if (app == null)
                {
                    throw new Exception("La aplicación es inválida");
                }
                
                appQ = new AplicacionQuery();
                appQ.Roles = new List<RolQuery>();
                
                foreach (Rol r in app.Roles)
                {
                    RolQuery rolQ = new RolQuery();
                    rolQ.Id = r.Id;
                    rolQ.Nombre = r.Display;
                    rolQ.Descripcion = r.Descripcion;
                    appQ.Roles.Add(rolQ);
                }

            }
            catch(Exception e)
            {
                throw e;
            }

            return appQ;
        }



    }
}
