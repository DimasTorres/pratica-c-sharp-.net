namespace Pratica.Application.DataContract.Order.Request;

public class CreateOrderItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
