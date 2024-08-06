using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task CreateAsync(OrderModel request);
        Task UpdateAsync(OrderModel request);
        Task DeleteAsync(string id);
        Task<List<OrderModel>> GetAllAsync(string orderId = null, string clientId = null, string userId = null);
        Task<OrderModel> GetByIdAsync(string id);
    }
}
