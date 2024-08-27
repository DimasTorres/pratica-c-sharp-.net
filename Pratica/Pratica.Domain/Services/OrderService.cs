﻿using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IClientRepository clientRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _clientRepository = clientRepository;
            _userRepository = userRepository;
        }

        public async Task<Response> CreateAsync(OrderModel request)
        {
            var response = new Response();


            var clientExist = await _clientRepository.ExistByIdAsync(request.ClientId.ToString()!);
            if (!clientExist)
                response.ReportErrors.Add(ReportError.Create($"Client {request.ClientId} not found."));

            var userExist = await _userRepository.ExistByIdAsync(request.UserId.ToString()!);
            if (!userExist)
                response.ReportErrors.Add(ReportError.Create($"User {request.UserId} not found."));

            if (response.ReportErrors.Any())
                return response;

            await _orderRepository.CreateAsync(request);
            return response;
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            var response = new Response();

            var exists = await _orderRepository.ExistByIdAsync(id.ToString());
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
                var exists = await _orderRepository.ExistByIdAsync(orderId.Value.ToString());
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

            var exists = await _orderRepository.ExistByIdAsync(id.ToString());
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

            var exists = await _orderRepository.ExistByIdAsync(request.Id);
            if (!exists)
                response.ReportErrors.Add(ReportError.Create($"Order {request.Id} not found."));

            var clientExist = await _clientRepository.ExistByIdAsync(request.ClientId.ToString()!);
            if (!clientExist)
                response.ReportErrors.Add(ReportError.Create($"Client {request.ClientId} not found."));

            var userExist = await _userRepository.ExistByIdAsync(request.UserId.ToString()!);
            if (!userExist)
                response.ReportErrors.Add(ReportError.Create($"User {request.UserId} not found."));

            if (response.ReportErrors.Any())
                return response;

            var orderItems = await _orderItemRepository.GetItemByOrderIdAsync(new Guid(request.Id));
            request.OrderItems = orderItems;

            await _orderRepository.UpdateAsync(request);
            return response;
        }
    }
}
