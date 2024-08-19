using AutoMapper;
using Pratica.Application.DataContract.Order.Request;
using Pratica.Application.Interfaces;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Validators.Base;

namespace Pratica.Application.Applications;

public class OrderApplication : IOrderApplication
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderApplication(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    public async Task<Response> CreateAsync(CreateOrderRequest request)
    {
        var orderModel = _mapper.Map<OrderModel>(request);

        return await _orderService.CreateAsync(orderModel);
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        return await _orderService.DeleteAsync(id);
    }

    public async Task<Response> GetAllAsync(Guid? orderId, Guid? clientId, Guid? userId)
    {
        return await _orderService.GetAllAsync(orderId, clientId, userId);
    }

    public async Task<Response> GetByIdAsync(Guid id)
    {
        return await _orderService.GetByIdAsync(id);
    }

    public async Task<Response> UpdateAsync(UpdateOrderRequest request)
    {
        var orderModel = _mapper.Map<OrderModel>(request);

        return await _orderService.UpdateAsync(orderModel);
    }
}
