using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Interfaces.Services;

public interface IOrderItemService
{
    Task<Response<List<OrderItemModel>>> GetItemByOrderIdAsync(Guid orderId);
}
