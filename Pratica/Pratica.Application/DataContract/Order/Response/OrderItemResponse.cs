namespace Pratica.Application.DataContract.Order.Response;

public sealed class OrderItemResponse
{
    public Guid OrderItemId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public decimal SellValue { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmout { get; set; }
}
