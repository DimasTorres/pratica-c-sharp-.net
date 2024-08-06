using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;

namespace Pratica.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task CreateAsync(OrderModel request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderModel>> GetAllAsync(string orderId = null, string clientId = null, string userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<OrderModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(OrderModel request)
        {
            throw new NotImplementedException();
        }
    }
}
