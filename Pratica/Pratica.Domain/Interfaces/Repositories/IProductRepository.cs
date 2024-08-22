using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(ProductModel request);
        Task UpdateAsync(ProductModel request);
        Task<bool> ExistByIdAsync(string id);
        Task DeleteAsync(Guid id);
        Task<List<ProductModel>> GetAllAsync(Guid? id, string? description);
        Task<ProductModel> GetByIdAsync(Guid id);
    }
}
