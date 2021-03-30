using DominioAuth.Modelo;
using Microsoft.Extensions.Configuration;
using NegocioAuth.Interfaces;
using NegocioAuth.Negocio;
using PersistenciaAuth;
using System;
using System.Collections.Generic;
using System.Text;

namespace NegocioAuth
{
    public class NegocioWrapper : INegocioWrapper
    {
        private IRepositoryWrapper repository;
        private ISesionesNegocio negocioSesiones;
        private IAplicacionesNegocio negocioAplicaciones;
        private ILogNegocio negocioLog;
        private IConfiguration configuration;
        private string IdApp;
        private string Issuer;
        private int LifeMinutes;

        public NegocioWrapper(IRepositoryWrapper repositoryWrapper,
                                  IConfiguration configuration)
        {
            this.repository = repositoryWrapper;
            this.configuration = configuration;
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["JwtConfig:issuer"];
            this.LifeMinutes = Convert.ToInt32(configuration["JwtConfig:lifeMinutes"].Trim());
        }


        public void InsertarLog(_LogAutenticacionAPI log)
        {
            this.repository.InsertarLog(log);

        }


        public ISesionesNegocio Sesiones 
        {
            get
            {
                if (negocioSesiones == null)
                {
                    negocioSesiones = new SesionesNegocio(this.repository, configuration);
                }
                return negocioSesiones;
            }
        }


        public IAplicacionesNegocio Aplicaciones
        {
            get
            {
                if (negocioAplicaciones == null)
                {
                    negocioAplicaciones = new AplicacionesNegocio(this.repository, configuration);
                }
                return negocioAplicaciones;
            }
        }

        public ILogNegocio Log
        {
            get
            {
                if (negocioLog == null)
                {
                    negocioLog = new LogNegocio(this.repository);
                }
                return negocioLog;
            }
        }
    }
}
