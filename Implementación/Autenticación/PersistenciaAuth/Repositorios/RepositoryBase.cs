using Microsoft.EntityFrameworkCore;
using PersistenciaAuth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PersistenciaAuth.Repositorios
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private ContextoAuthDB repositoryContext;

        public RepositoryBase(ContextoAuthDB dbContext)
        {
            this.repositoryContext = dbContext;
        }

        public T Crear(T entity)
        {
            T objet = repositoryContext.Set<T>().Add(entity).Entity;
            repositoryContext.SaveChanges();
            return objet;

        }

        public T Borrar(T entity)
        {
            T objet = repositoryContext.Set<T>().Remove(entity).Entity;
            repositoryContext.SaveChanges();
            return objet;

        }

        public T BuscarPrimero()
        {
            return repositoryContext.Set<T>().FirstOrDefault<T>();
        }

        public T BuscarPrimero(Expression<Func<T, bool>> expression)
        {
            return repositoryContext.Set<T>().Where(expression).FirstOrDefault<T>();
        }

        public IEnumerable<T> BuscarTodos()
        {
            return repositoryContext.Set<T>().AsNoTracking();
        }

        public IEnumerable<T> BuscarPorCondicion(Expression<Func<T, bool>> expression)
        {
            return repositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        public IEnumerable<T> BuscarPor(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = repositoryContext.Set<T>().Where(predicate);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public IEnumerable<T> Buscar(Expression<Func<T, bool>> expression, string[] includes)
        {
            var dbSet = repositoryContext.Set<T>().Where(expression);
            IEnumerable<T> query = new List<T>();


            foreach (string include in includes)
            {
                query = dbSet.Include(include);
            }

            return query ?? dbSet;
        }


        public T Actualizar(T entity)
        {
            return repositoryContext.Set<T>().Update(entity).Entity;
        }
    }

}
