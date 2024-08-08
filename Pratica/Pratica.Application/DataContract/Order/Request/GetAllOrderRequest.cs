namespace Pratica.Application.DataContract.Order.Request;

public class GetAllOrderRequest
{
    public Guid? Id { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? UserId { get; set; }
}
