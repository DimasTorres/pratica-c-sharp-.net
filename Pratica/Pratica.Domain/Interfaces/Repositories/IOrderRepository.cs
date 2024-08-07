using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(OrderModel request);
        Task CreateItemAsync(OrderItemModel request);
        Task UpdateAsync(OrderModel request);
        Task UpdateItemAsync(OrderItemModel request);
        Task ExistByIdAsync(string id);
        Task DeleteAsync(string id);
        Task DeleteItemAsync(string id);
        Task<OrderModel> GetByIdAsync(string id);
        Task<List<OrderModel>> GetAllAsync(string orderId = null, string clientId = null, string userId = null);
        Task<List<OrderItemModel>> GetItemByOrderIdAsync(string orderId);

    }
}
