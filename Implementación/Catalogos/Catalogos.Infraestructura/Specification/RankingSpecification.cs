using System;
using System.Linq.Expressions;

namespace Catalogos.Infraestructura.Specification
{
    public class RankingSpecification<T> : BaseSpecification<T>
    {

        public RankingSpecification(ProductoFullTextSpecification productoFullTextSpecification, int skip, int take) : base()
        {
            ApplyPaging(skip, take);
        }

        public RankingSpecification(int skip, int take, Expression<Func<T, object>> expression, bool descending) : base()
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