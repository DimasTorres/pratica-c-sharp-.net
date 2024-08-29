using AutoMapper;
using Pratica.Application.DataContract.Order.Request;
using Pratica.Application.DataContract.Order.Response;
using Pratica.Application.Interfaces;
using Pratica.Application.Validators;
using Pratica.Application.Validators.Base;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Application.Applications;

public class OrderApplication : IOrderApplication
{
    private readonly IOrderService _orderService;
    private readonly IClientService _clientService;
    private readonly IUserService _userService;
    private readonly IProductService _productService;
    private readonly IOrderItemService _orderItemService;
    private readonly IMapper _mapper;

    public OrderApplication(
        IOrderService orderService,
        IClientService clientService,
        IUserService userService,
        IProductService productService,
        IOrderItemService orderItemService,
        IMapper mapper)
    {
        _orderService = orderService;
        _clientService = clientService;
        _userService = userService;
        _productService = productService;
        _orderItemService = orderItemService;
        _mapper = mapper;
    }

    public async Task<Response> CreateAsync(CreateOrderRequest request)
    {
        var validate = new CreateOrderRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return validateErrors;

        try
        {
            List<ReportError> reportErrors = new();

            var clientExist = await _clientService.ExistByIdAsync(request.ClientId);
            if (!clientExist.Data)
                reportErrors.Add(ReportError.Create($"Client {request.ClientId} not found."));

            var userExist = await _userService.ExistByIdAsync(request.UserId);
            if (!userExist.Data)
                reportErrors.Add(ReportError.Create($"User {request.UserId} not found."));

            List<OrderItemModel> orderItems = new();
            foreach (var item in request.OrderItems)
            {
                var itemExist = await _productService.ExistByIdAsync(item.ProductId);
                if (!itemExist.Data)
                {
                    reportErrors.Add(ReportError.Create($"Product {item.ProductId} not found."));
                }
                else
                {
                    var product = await _productService.GetByIdAsync(item.ProductId);

                    var orderItem = _mapper.Map<OrderItemModel>(item);
                    orderItem.SellValue = product.Data.SellValue;
                    orderItem.TotalAmout = product.Data.SellValue * item.Quantity;
                }
            }

            if (reportErrors.Any())
            {
                return Response.Unprocessable(reportErrors);
            }

            var orderModel = new OrderModel()
            {
                ClientId = request.ClientId,
                UserId = request.UserId,
                OrderItems = orderItems
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
            var exists = await _orderService.ExistByIdAsync(id);
            if (!exists.Data)
            {
                return Response.Unprocessable(ReportError.Create($"Order {id} not found."));
            }

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
            List<ReportError> reportErrors = new();
            if (orderId is not null && orderId != Guid.Empty)
            {
                var exists = await _orderService.ExistByIdAsync(orderId.Value);
                if (!exists.Data)
                {
                    reportErrors.Add(ReportError.Create($"Order {orderId} not found."));
                }
            }

            if (clientId is not null && clientId != Guid.Empty)
            {
                var exists = await _clientService.ExistByIdAsync(clientId.Value);
                if (!exists.Data)
                {
                    reportErrors.Add(ReportError.Create($"Client {clientId.Value} not found."));
                }
            }

            if (userId is not null && userId != Guid.Empty)
            {
                var exists = await _userService.ExistByIdAsync(userId.Value);
                if (!exists.Data)
                {
                    reportErrors.Add(ReportError.Create($"User {userId} not found."));
                }
            }

            if (reportErrors.Any())
                return Response.Unprocessable<List<OrderResponse>>(reportErrors);

            var result = await _orderService.GetAllAsync(orderId, clientId, userId);

            if (result.ReportErrors.Any())
                return Response.Unprocessable<List<OrderResponse>>(result.ReportErrors);

            foreach (var item in result.Data)
            {
                var orderItems = await _orderItemService.GetItemByOrderIdAsync(new Guid(item.Id));
                item.OrderItems = orderItems.Data;
            }

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
            var result = await _orderService.GetByIdAsync(id);

            var response = _mapper.Map<OrderResponse>(result.Data);

            return Response.OK(response);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }

    public async Task<Response> UpdateAsync(UpdateOrderRequest request)
    {
        var validate = new UpdateOrderRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return validateErrors;

        try
        {
            List<ReportError> reportErrors = new();

            var exists = await _orderService.ExistByIdAsync(request.Id);
            if (!exists.Data)
                reportErrors.Add(ReportError.Create($"Order {request.Id} not found."));

            var clientExist = await _clientService.ExistByIdAsync(request.ClientId);
            if (!clientExist.Data)
                reportErrors.Add(ReportError.Create($"Client {request.ClientId} not found."));

            var userExist = await _userService.ExistByIdAsync(request.UserId);
            if (!userExist.Data)
                reportErrors.Add(ReportError.Create($"User {request.UserId} not found."));

            if (reportErrors.Any())
            {
                return Response.Unprocessable(reportErrors);
            }

            List<OrderItemModel> orderItems = new List<OrderItemModel>();

            if (request.OrderItems is null)
            {
                var orderItemsModelOld = await _orderItemService.GetItemByOrderIdAsync(request.Id);
                orderItems = orderItemsModelOld.Data;
            }
            else
            {
                orderItems = _mapper.Map<List<OrderItemModel>>(request.OrderItems); ;
            }

            var orderModel = new OrderModel()
            {
                Id = request.Id.ToString(),
                ClientId = request.ClientId,
                UserId = request.UserId,
                OrderItems = orderItems
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
