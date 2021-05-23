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
    public class LogServiceCmd : ILogServiceCmd
    {
        private readonly string IdApp;
        private readonly string Issuer;
        private readonly IUnitOfWork _ufw;
        
        public LogServiceCmd(IConfiguration configuration,
                               IUnitOfWork ufwAplicacion)
        {
            
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["JwtConfig:issuer"];
            this._ufw = ufwAplicacion;
            
        }

        public void AgregarLog(LogCmd log)
        {

            try
            {
                _LogAutenticacionAPI logE = new _LogAutenticacionAPI(log.Tipo, log.Usuario, log.Aplicacion, log.Metodo, log.Entidad, log.Request, log.Response, log.EsExcepcion, log.Mensaje, log.Parametros);
                _ufw.InsertarLog(logE);
            }
            catch (Exception e)
            {
                _ufw.Rollback();
                throw e;
            }

        }
                    
    }
}
