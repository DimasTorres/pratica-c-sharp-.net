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
            var exists = await _repository.OrderRepository.ExistByIdAsync(id);
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
    public async Task<Response<bool>> ExistByIdAsync(Guid id)
    {
        var response = new Response<bool>();
        try
        {
            response.Data = await _repository.OrderRepository.ExistByIdAsync(id);

            return response;
        }
        catch (Exception ex)
        {
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }
}