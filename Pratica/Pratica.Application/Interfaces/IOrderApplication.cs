using Pratica.Application.DataContract.Order.Request;
using Pratica.Domain.Validators.Base;

namespace Pratica.Application.Interfaces;

public interface IOrderApplication
{
    Task<Response> CreateAsync(CreateOrderRequest request);
    Task<Response> UpdateAsync(UpdateOrderRequest request);
    Task<Response> DeleteAsync(Guid id);
    Task<Response> GetByIdAsync(Guid id);
    Task<Response> GetAllAsync(Guid orderId, Guid clientId, Guid userId);
}
