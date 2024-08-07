using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Validators;
using Pratica.Domain.Validators.Base;

namespace Pratica.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Response> CreateAsync(OrderModel request)
        {
            var response = new Response();

            var validate = new OrderValidation();
            var validateErrors = validate.Validate(request).GetErrors();
            if (validateErrors.ReportErrors.Any())
                return validateErrors;

            await _orderRepository.CreateAsync(request);
            return response;
        }

        public Task<Response> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<OrderModel>>> GetAllAsync(string orderId = null, string clientId = null, string userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<Response<OrderModel>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateAsync(OrderModel request)
        {
            throw new NotImplementedException();
        }
    }
}
