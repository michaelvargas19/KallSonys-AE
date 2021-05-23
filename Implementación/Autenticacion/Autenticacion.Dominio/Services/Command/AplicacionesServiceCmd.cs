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
    public class AplicacionesServiceCmd : IAplicacionesServiceCmd
    {
        private readonly string IdApp;
        private readonly string Issuer;
        private readonly IUnitOfWork _ufw;
        
        public AplicacionesServiceCmd(IConfiguration configuration,
                               IUnitOfWork ufwAplicacion)
        {
            
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["JwtConfig:issuer"];
            this._ufw = ufwAplicacion;
            
        }

        public AplicacionQuery actualizarAplicacion(AplicacionCmd aplicacion)
        {
            AplicacionQuery app = null;

            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                throw e;
            }

            return app;
        }

        public ResultadoCmd asignarRol(AsignarRolAppCmd request)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                throw e;
            }

            return null;
        }

        public AplicacionQuery registrarAplicacion(AplicacionCmd aplicacion)
        {
            AplicacionQuery appQ = null;
            AlgoritmoDeSeguridad algoritmo = null;

            try
            {
                if (aplicacion.EsValido())
                {
                    Aplicacion otherApp = _ufw.RepositoryQueryAplicacion().Find(new AplicacionSpecification(aplicacion.IdAplicacion)).FirstOrDefault();

                    if (otherApp == null)
                    {
                        if (aplicacion.PermiteJWT)
                        {
                            algoritmo = _ufw.RepositoryQueryAlgoritmo().Find(new AlgoritmoSpecification(aplicacion.AlgoritmoDeSeguridad)).FirstOrDefault();

                            if (algoritmo == null)
                                throw new Exception("El algoritmo es inválido");
                        }


                        Aplicacion app = new Aplicacion();
                        app.IdAplicacion = aplicacion.IdAplicacion;
                        app.Nombre = aplicacion.Nombre;
                        app.EmailContacto = aplicacion.EmailContacto;
                        app.Estado = true;
                        app.PermiteJWT = aplicacion.PermiteJWT; 
                        app.AlgoritmoDeSeguridad = aplicacion.AlgoritmoDeSeguridad;
                        app.LlaveSecreta = aplicacion.LlaveSecreta;
                        app.MinutosDeVida = aplicacion.MinutosDeVida;
                        app.FechaExpiracionLlave = aplicacion.FechaExpiracionLlave;
                        app.EstadoLlave = (aplicacion.PermiteJWT && (aplicacion.LlaveSecreta != null)) ? true : false;

                        _ufw.BeginTransaction();
                        _ufw.RepositoryCommandAplicacion().Add(app);
                        
                        foreach(RolQuery r in aplicacion.Roles)
                        {
                            _ufw.RoleIdentityRepository().CreateRoleAsync(aplicacion.IdAplicacion, r.Nombre, r.Nombre, r.Descripcion);
                        }

                        _ufw.SaveChanges();
                        _ufw.Commit();
                        app = _ufw.RepositoryQueryAplicacion().Find(new AplicacionSpecification(app.IdAplicacion)).FirstOrDefault();

                        appQ = new AplicacionQuery();
                        appQ.IdAplicacion = app.IdAplicacion;
                        appQ.Nombre = app.Nombre;
                        appQ.EmailContacto = app.EmailContacto;
                        appQ.PermiteJWT = app.PermiteJWT;
                        appQ.AlgoritmoDeSeguridad = app.AlgoritmoDeSeguridad;
                        appQ.NombreAlgoritmo = (algoritmo != null) ? algoritmo.Valor : "";
                        appQ.LlaveSecreta = app.LlaveSecreta;
                        appQ.MinutosDeVida = app.MinutosDeVida;
                        appQ.FechaExpiracionLlave = app.FechaExpiracionLlave;
                        appQ.Roles = new List<RolQuery>();
                        
                        foreach(Rol r in app.Roles)
                        {
                            RolQuery rolQ = new RolQuery();
                            rolQ.Id = r.Id;
                            rolQ.Nombre = r.Display;
                            rolQ.Descripcion = r.Descripcion;
                            appQ.Roles.Add(rolQ);
                        }
                    }
                    else
                    {
                        throw new Exception("Ya existe una Aplicación con ese Identificador");
                    }

                }
                else
                {
                    throw new Exception("La configuración es inválida");
                }
            }
            catch (Exception e)
            {
                _ufw.Rollback();
                throw e;
            }
            
            
            return appQ;
        }
    }
}
