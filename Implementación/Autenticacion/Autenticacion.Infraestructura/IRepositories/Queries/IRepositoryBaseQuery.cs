using Autenticacion.Infraestructura.ISpecification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Autenticacion.Infraestructura.IRepositories.Queries
{
    public interface IRepositoryBaseQuery<T> where T : class
    {
        T FindById(int id);

        IEnumerable<T> Find(ISpecification<T> specification = null);

        bool Contains(ISpecification<T> specification = null);
        bool Contains(Expression<Func<T, bool>> predicate);

        int Count(ISpecification<T> specification = null);
        int Count(Expression<Func<T, bool>> predicate);
    }
}
