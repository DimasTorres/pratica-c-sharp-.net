using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task CreateAsync(ProductModel request);
        Task UpdateAsync(ProductModel request);
        Task DeleteAsync(string id);
        Task<List<ProductModel>> GetAllAsync(string id = null, string name = null);
        Task<ProductModel> GetByIdAsync(string id);
    }
}
