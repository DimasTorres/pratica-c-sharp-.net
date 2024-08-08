namespace Pratica.Application.DataContract.Order.Response;

public sealed class OrderResponse
{
    public Guid ClientId { get; set; }
    public Guid UserId { get; set; }
    public List<OrderItemResponse> OrderItems { get; set; }
}
