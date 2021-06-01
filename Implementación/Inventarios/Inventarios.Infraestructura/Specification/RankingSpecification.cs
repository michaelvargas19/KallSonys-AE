using System;
using System.Linq.Expressions;

namespace Inventarios.Infraestructura.Specification
{
    public class RankingSpecification<T> : BaseSpecification<T>
    {

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