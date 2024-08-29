namespace Pratica.Application.DataContract.Order.Request;

public sealed class CreateOrderRequest
{
    public Guid ClientId { get; set; }
    public Guid UserId { get; set; }
    public List<CreateOrderItemRequest> OrderItems { get; set; }
}
