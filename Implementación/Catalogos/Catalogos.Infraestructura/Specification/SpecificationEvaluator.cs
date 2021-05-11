using Catalogos.Dominio.ISpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogos.Infraestructura.Specification
{
    public class SpecificationEvaluator<TDocument>
    {
        public static IQueryable<TDocument> GetQuery(IQueryable<TDocument> inputQuery, ISpecification<TDocument> specification)
        {
            var query = inputQuery;

            // modify the IQueryable using the specification's criteria expression
            if (specification.Criterio != null)
            {
                query = query.Where(specification.Criterio);
            }

            // Includes all expression-based includes
            //query = specification.Includes.Aggregate(query,
            //                        (current, include) => current.Include(include));

            // Include any string-based include statements
            //query = specification.IncludeStrings.Aggregate(query,
            //                        (current, include) => current.Include(include));

            // Apply ordering if expressions are set
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                             .Take(specification.Take);
            }
            return query;
        }
    }
}
