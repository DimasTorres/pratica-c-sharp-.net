using Pratica.Domain.Models;
using Pratica.Domain.Validators.Base;

namespace Pratica.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Response> CreateAsync(OrderModel request);
        Task<Response> UpdateAsync(OrderModel request);
        Task<Response> DeleteAsync(string id);
        Task<Response<List<OrderModel>>> GetAllAsync(string orderId = null, string clientId = null, string userId = null);
        Task<Response<OrderModel>> GetByIdAsync(string id);
    }
}
