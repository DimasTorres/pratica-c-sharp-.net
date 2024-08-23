using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task CreateItemAsync(OrderItemModel request);
    Task UpdateItemAsync(OrderItemModel request);
    Task DeleteItemAsync(Guid id);
    Task<List<OrderItemModel>> GetItemByOrderIdAsync(Guid orderId);
}
