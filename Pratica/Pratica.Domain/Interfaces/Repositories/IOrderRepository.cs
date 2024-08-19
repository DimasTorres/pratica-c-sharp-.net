using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(OrderModel request);
        Task CreateItemAsync(OrderItemModel request);
        Task UpdateAsync(OrderModel request);
        Task UpdateItemAsync(OrderItemModel request);
        Task<bool> ExistByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task DeleteItemAsync(Guid id);
        Task<OrderModel> GetByIdAsync(Guid id);
        Task<List<OrderModel>> GetAllAsync(Guid? orderId, Guid? clientId, Guid? userId);
        Task<List<OrderItemModel>> GetItemByOrderIdAsync(Guid orderId);
    }
}
