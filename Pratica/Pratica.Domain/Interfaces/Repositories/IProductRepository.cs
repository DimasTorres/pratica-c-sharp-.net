using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(ProductModel request);
        Task UpdateAsync(ProductModel request);
        Task ExistByIdAsync(string id);
        Task DeleteAsync(string id);
        Task<List<ProductModel>> GetAllAsync(string id = null, string name = null);
        Task<ProductModel> GetByIdAsync(string id);
    }
}
