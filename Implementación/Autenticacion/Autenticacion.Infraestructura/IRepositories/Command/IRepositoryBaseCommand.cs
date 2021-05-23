using Autenticacion.Infraestructura.ISpecification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Autenticacion.Infraestructura.IRepositories.Command
{
    public interface IRepositoryBaseCommand<T> where T : class
    {

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);

    }
}
