using Pratica.Application.DataContract.Product.Request;
using Pratica.Domain.Validators.Base;

namespace Pratica.Application.Interfaces;

public interface IProductApplication
{
    Task<Response> CreateAsync(CreateProductRequest request);
    Task<Response> UpdateAsync(UpdateProductRequest request);
    Task<Response> DeleteAsync(Guid id);
    Task<Response> GetByIdAsync(Guid id);
    Task<Response> GetAllAsync(Guid id, string name);
}
