namespace Pratica.Application.DataContract.Product.Request;

public sealed class UpdateProductRequest
{
    public Guid ProductId { get; set; }
    public string Description { get; set; }
    public decimal SellValue { get; set; }
    public int Stock { get; set; }
}
