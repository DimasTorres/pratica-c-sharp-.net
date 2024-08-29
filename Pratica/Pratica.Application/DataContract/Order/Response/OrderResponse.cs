using Pratica.Application.DataContract.Client.Response;
using Pratica.Application.DataContract.User.Response;

namespace Pratica.Application.DataContract.Order.Response;

public sealed class OrderResponse
{
    public Guid Id { get; set; }
    public ClientSimpleResponse Client { get; set; }
    public UserSimpleResponse User { get; set; }
    public List<OrderItemResponse> OrderItems { get; set; }
}
