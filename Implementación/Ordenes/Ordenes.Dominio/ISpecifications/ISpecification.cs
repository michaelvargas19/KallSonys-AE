﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ordenes.Dominio.ISpecification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criterio { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
