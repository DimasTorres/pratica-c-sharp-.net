using Pratica.Domain.Models;
using Pratica.Domain.Validators.Base;

namespace Pratica.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<Response> CreateAsync(ProductModel request);
        Task<Response> UpdateAsync(ProductModel request);
        Task<Response> DeleteAsync(string id);
        Task<Response<List<ProductModel>>> GetAllAsync(string id = null, string name = null);
        Task<Response<ProductModel>> GetByIdAsync(string id);
    }
}
