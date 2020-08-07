using System.Collections.Generic;
using System.Threading.Tasks;
using AmazingShop.Core.Entities;
using AmazingShop.Core.Specification;

namespace AmazingShop.Core.Interfaces
{
    public interface IRepository<T> where T: BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetListAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}