using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmazingShop.Core.Entities;
using AmazingShop.Core.Interfaces;
using AmazingShop.Core.Specification;
using AmazingShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Infrastructure
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly AmazingShopContext context;

        public Repository(AmazingShopContext context)
        {
            this.context = context;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await this.context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetListAsync()
        {
            return await this.context.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(this.context.Set<T>().AsQueryable(), spec);
        }
    }
}