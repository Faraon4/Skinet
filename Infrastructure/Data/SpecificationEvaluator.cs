using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            // evaluate what is inside the specification
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            //Evaluate the Includes
            // current => the current entity TEntity
            // include => our expresion
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
           
           
           
           // Sorting
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
           
            return query;
        }
    }
}