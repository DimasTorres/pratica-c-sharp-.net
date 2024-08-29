using Pratica.Application.DataContract.Product.Response;

namespace Pratica.Application.DataContract.Order.Response;

public sealed class OrderItemResponse
{
    public Guid Id { get; set; }
    public OrderSimpleResponse Order { get; set; }
    public ProductSimpleResponse Product { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmout { get; set; }
}
