using Pratica.Domain.Interfaces.Repositories.Base;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IUnitOfWork _repository;

    public OrderItemService(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<Response<List<OrderItemModel>>> GetItemByOrderIdAsync(Guid orderId)
    {
        var response = new Response<List<OrderItemModel>>();
        _repository.BeginTransaction();
        try
        {
            response.Data = await _repository.OrderItemRepository.GetItemByOrderIdAsync(orderId);

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
