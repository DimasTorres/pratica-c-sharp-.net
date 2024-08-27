using Pratica.Application.DataContract.Product.Request;
using Pratica.Application.DataContract.Product.Response;
using Pratica.Domain.Models.Base;

namespace Pratica.Application.Interfaces;

public interface IProductApplication
{
    Task<Response> CreateAsync(CreateProductRequest request);
    Task<Response> UpdateAsync(UpdateProductRequest request);
    Task<Response> DeleteAsync(Guid id);
    Task<Response> GetByIdAsync(Guid id);
    Task<Response<List<ProductResponse>>> GetAllAsync(Guid? id, string? description);
}
