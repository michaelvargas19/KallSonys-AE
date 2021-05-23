using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.IRepositories.Command;
using Autenticacion.Infraestructura.ISpecification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Autenticacion.Infraestructura.Repositories.Queries
{
    public class RepositoryBaseCommand<T> : IRepositoryBaseCommand<T> where T : class
    {
        protected readonly ContextoAuthDB _context;

        public RepositoryBaseCommand(ContextoAuthDB context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }

}
