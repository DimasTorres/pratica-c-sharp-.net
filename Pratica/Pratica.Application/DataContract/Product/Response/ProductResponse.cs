namespace Pratica.Application.DataContract.Product.Response;

public sealed class ProductResponse
{
    public Guid ProductId { get; set; }
    public string Description { get; set; }
    public decimal SellValue { get; set; }
    public int Stock { get; set; }
}
