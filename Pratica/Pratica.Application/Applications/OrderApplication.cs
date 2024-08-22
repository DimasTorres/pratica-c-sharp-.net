using AutoMapper;
using Pratica.Application.DataContract.Order.Request;
using Pratica.Application.DataContract.Order.Response;
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
        var orderModel = new OrderModel()
        {
            ClientId = request.ClientId,
            UserId = request.UserId,
            OrderItems = new List<OrderItemModel>()
        };

        return await _orderService.CreateAsync(orderModel);
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        return await _orderService.DeleteAsync(id);
    }

    public async Task<Response<List<OrderResponse>>> GetAllAsync(Guid? orderId, Guid? clientId, Guid? userId)
    {
        var result = await _orderService.GetAllAsync(orderId, clientId, userId);

        if (result.ReportErrors.Any())
            return Response.Unprocessable<List<OrderResponse>>(result.ReportErrors);

        var response = _mapper.Map<List<OrderResponse>>(result.Data);

        return Response.OK(response);
    }

    public async Task<Response> GetByIdAsync(Guid id)
    {
        return await _orderService.GetByIdAsync(id);
    }

    public async Task<Response> UpdateAsync(UpdateOrderRequest request)
    {
        var orderModel = new OrderModel()
        {
            Id = request.Id.ToString(),
            ClientId = request.ClientId,
            UserId = request.UserId
        };

        return await _orderService.UpdateAsync(orderModel);
    }
}
