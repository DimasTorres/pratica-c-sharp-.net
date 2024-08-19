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

        public async Task<Response> DeleteAsync(Guid id)
        {
            var response = new Response();

            var exists = await _orderRepository.ExistByIdAsync(id);
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"Order {id} not found."));
                return response;
            }

            await _orderRepository.DeleteAsync(id);
            return response;
        }

        public async Task<Response<List<OrderModel>>> GetAllAsync(Guid? orderId, Guid? clientId, Guid? userId)
        {
            var response = new Response<List<OrderModel>>();

            if (orderId is not null && orderId != Guid.Empty)
            {
                var exists = await _orderRepository.ExistByIdAsync(orderId.Value);
                if (!exists)
                {
                    response.ReportErrors.Add(ReportError.Create($"Order {orderId} not found."));
                    return response;
                }
            }

            var result = await _orderRepository.GetAllAsync(orderId, clientId, userId);
            response.Data = result;
            return response;
        }

        public async Task<Response<OrderModel>> GetByIdAsync(Guid id)
        {
            var response = new Response<OrderModel>();

            var exists = await _orderRepository.ExistByIdAsync(id);
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"Order {id} not found."));
                return response;
            }

            var result = await _orderRepository.GetByIdAsync(id);
            response.Data = result;

            return response;
        }

        public async Task<Response> UpdateAsync(OrderModel request)
        {
            var response = new Response();

            var validate = new OrderValidation();
            var validateErrors = validate.Validate(request).GetErrors();
            if (validateErrors.ReportErrors.Any())
                return validateErrors;

            var exists = await _orderRepository.ExistByIdAsync(request.Id);
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"Order {request.Id} not found."));
                return response;
            }

            await _orderRepository.UpdateAsync(request);
            return response;
        }
    }
}
