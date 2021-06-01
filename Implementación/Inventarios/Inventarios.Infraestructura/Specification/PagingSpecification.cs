using System;
using System.Linq.Expressions;

namespace Inventarios.Infraestructura.Specification
{
    public class PagingSpecification<T> : BaseSpecification<T>
    {

        public PagingSpecification(int skip, int take) : base()
        {
            ApplyPaging(skip, take);
        }

        public PagingSpecification(int skip, int take, Expression<Func<T, object>> expression, bool descending) : base()
        {
            if (!descending)
            {
                ApplyOrderBy(expression);
            }
            else
            {
                ApplyOrderByDescending(expression);
            }

            ApplyPaging(skip, take);
        }


    }
}