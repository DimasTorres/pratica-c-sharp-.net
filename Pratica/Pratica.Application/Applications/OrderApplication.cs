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
        try
        {
            var orderModel = new OrderModel()
            {
                ClientId = request.ClientId,
                UserId = request.UserId,
                OrderItems = new List<OrderItemModel>()
            };

            return await _orderService.CreateAsync(orderModel);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        try
        {
            return await _orderService.DeleteAsync(id);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }

    public async Task<Response<List<OrderResponse>>> GetAllAsync(Guid? orderId, Guid? clientId, Guid? userId)
    {
        try
        {
            var result = await _orderService.GetAllAsync(orderId, clientId, userId);

            if (result.ReportErrors.Any())
                return Response.Unprocessable<List<OrderResponse>>(result.ReportErrors);

            var response = _mapper.Map<List<OrderResponse>>(result.Data);

            return Response.OK(response);
        }
        catch (Exception e)
        {
            List<ReportError> listError = [ReportError.Create(e.Message)];
            return Response.Unprocessable<List<OrderResponse>>(listError);
        }
    }

    public async Task<Response> GetByIdAsync(Guid id)
    {
        try
        {
            return await _orderService.GetByIdAsync(id);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }

    public async Task<Response> UpdateAsync(UpdateOrderRequest request)
    {
        try
        {
            var orderModel = new OrderModel()
            {
                Id = request.Id.ToString(),
                ClientId = request.ClientId,
                UserId = request.UserId
            };

            return await _orderService.UpdateAsync(orderModel);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }
}
