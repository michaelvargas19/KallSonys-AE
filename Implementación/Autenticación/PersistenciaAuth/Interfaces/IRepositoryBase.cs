using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PersistenciaAuth.Interfaces
{
    public interface IRepositoryBase<T>
    {
        T Crear(T entity);
        T Borrar(T entity);
        T BuscarPrimero();
        T BuscarPrimero(Expression<Func<T, bool>> expression);
        IEnumerable<T> BuscarTodos();
        IEnumerable<T> BuscarPorCondicion(Expression<Func<T, bool>> expression);
        IEnumerable<T> BuscarPor(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Buscar(Expression<Func<T, bool>> expression, string[] includes);
        T Actualizar(T entity);
    }
}
