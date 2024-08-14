using Pratica.Domain.Models;
using Pratica.Domain.Validators.Base;

namespace Pratica.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Response> CreateAsync(OrderModel request);
        Task<Response> UpdateAsync(OrderModel request);
        Task<Response> DeleteAsync(Guid id);
        Task<Response<List<OrderModel>>> GetAllAsync(Guid orderId, Guid clientId, Guid userId);
        Task<Response<OrderModel>> GetByIdAsync(Guid id);
    }
}
