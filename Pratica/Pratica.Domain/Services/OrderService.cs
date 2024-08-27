using Pratica.Domain.Interfaces.Repositories.Base;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _repository;

    public OrderService(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<Response> CreateAsync(OrderModel request)
    {
        var response = new Response();
        _repository.BeginTransaction();

        try
        {
            var clientExist = await _repository.ClientRepository.ExistByIdAsync(request.ClientId.ToString()!);
            if (!clientExist)
                response.ReportErrors.Add(ReportError.Create($"Client {request.ClientId} not found."));

            var userExist = await _repository.UserRepository.ExistByIdAsync(request.UserId.ToString()!);
            if (!userExist)
                response.ReportErrors.Add(ReportError.Create($"User {request.UserId} not found."));

            if (response.ReportErrors.Any())
            {
                _repository.RollbackTransaction();
                return response;
            }

            await _repository.OrderRepository.CreateAsync(request);

            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        var response = new Response();
        _repository.BeginTransaction();

        try
        {
            var exists = await _repository.OrderRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                _repository.RollbackTransaction();
                response.ReportErrors.Add(ReportError.Create($"Order {id} not found."));
                return response;
            }

            await _repository.OrderRepository.DeleteAsync(id);

            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }

    public async Task<Response<List<OrderModel>>> GetAllAsync(Guid? orderId, Guid? clientId, Guid? userId)
    {
        var response = new Response<List<OrderModel>>();
        _repository.BeginTransaction();

        try
        {
            if (orderId is not null && orderId != Guid.Empty)
            {
                var exists = await _repository.OrderRepository.ExistByIdAsync(orderId.Value.ToString());
                if (!exists)
                {
                    _repository.RollbackTransaction();
                    response.ReportErrors.Add(ReportError.Create($"Order {orderId} not found."));
                    return response;
                }
            }

            var result = await _repository.OrderRepository.GetAllAsync(orderId, clientId, userId);
            response.Data = result;

            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }

    public async Task<Response<OrderModel>> GetByIdAsync(Guid id)
    {
        var response = new Response<OrderModel>();
        _repository.BeginTransaction();

        try
        {
            var exists = await _repository.OrderRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                _repository.RollbackTransaction();
                response.ReportErrors.Add(ReportError.Create($"Order {id} not found."));
                return response;
            }

            var result = await _repository.OrderRepository.GetByIdAsync(id);
            result.OrderItems = await _repository.OrderItemRepository.GetItemByOrderIdAsync(id);

            response.Data = result;

            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }

    public async Task<Response> UpdateAsync(OrderModel request)
    {
        var response = new Response();
        _repository.BeginTransaction();

        try
        {
            var exists = await _repository.OrderRepository.ExistByIdAsync(request.Id);
            if (!exists)
                response.ReportErrors.Add(ReportError.Create($"Order {request.Id} not found."));

            var clientExist = await _repository.ClientRepository.ExistByIdAsync(request.ClientId.ToString()!);
            if (!clientExist)
                response.ReportErrors.Add(ReportError.Create($"Client {request.ClientId} not found."));

            var userExist = await _repository.UserRepository.ExistByIdAsync(request.UserId.ToString()!);
            if (!userExist)
                response.ReportErrors.Add(ReportError.Create($"User {request.UserId} not found."));

            if (response.ReportErrors.Any())
            {
                _repository.RollbackTransaction();
                return response;
            }

            var orderItems = await _repository.OrderItemRepository.GetItemByOrderIdAsync(new Guid(request.Id));
            request.OrderItems = orderItems;

            await _repository.OrderRepository.UpdateAsync(request);

            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }
}