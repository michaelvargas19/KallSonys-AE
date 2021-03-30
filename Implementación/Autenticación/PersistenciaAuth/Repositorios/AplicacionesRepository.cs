using DominioAuth.Modelo;
using DominioAuth.Request;
using Microsoft.AspNetCore.Identity;
using PersistenciaAuth.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PersistenciaAuth.Repositorios
{
    public class AplicacionesRepository : RepositoryBase<Aplicacion>, IAplicacionesRepository
    {
        private ContextoAuthDB DBContext;

        public AplicacionesRepository(ContextoAuthDB ContextDB)
            : base(ContextDB)
        {
        }

        
    }
}
