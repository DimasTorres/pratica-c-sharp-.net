namespace Pratica.Application.DataContract.Product.Response;

public sealed class ProductResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public decimal SellValue { get; set; }
    public int Stock { get; set; }
}
