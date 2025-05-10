using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.Core.Entities;

namespace UniversityApp.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
