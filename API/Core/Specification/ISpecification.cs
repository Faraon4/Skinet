using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria {get;}
        List<Expression<Func<T, object>>> Includes {get;}


        // Sorting part
        Expression<Func<T, object>> OrderBy {get;}
        Expression<Func<T, object>> OrderByDescending{get; }


        // Propteries for pagination

        int Take {get;}  // ifwe take five items from seond page
        int Skip{get;} // we will skipp five items from the first page

        bool IsPagingEnabled{get; }

    }
}