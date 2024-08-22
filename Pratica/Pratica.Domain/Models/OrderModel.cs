using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Models
{
    public class OrderModel : EntityBase
    {
        public Guid? ClientId { get; set; }
        public ClientModel Client { get; set; }
        public Guid? UserId { get; set; }
        public UserModel User { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
