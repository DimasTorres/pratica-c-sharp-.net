namespace Pratica.Application.DataContract.Product.Response;

public sealed class ProductSimpleResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public decimal SellValue { get; set; }
}
