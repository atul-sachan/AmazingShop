using System.Linq;
using AmazingShop.Core.Entities;
using AmazingShop.Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Infrastructure
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (current, include) => { return current.Include(include); });
            return query;
        }
    }
}