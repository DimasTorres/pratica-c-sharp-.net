using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(OrderModel request);
        Task UpdateAsync(OrderModel request);
        Task<bool> ExistByIdAsync(string id);
        Task DeleteAsync(Guid id);
        Task<OrderModel> GetByIdAsync(Guid id);
        Task<List<OrderModel>> GetAllAsync(Guid? orderId, Guid? clientId, Guid? userId);
    }
}
