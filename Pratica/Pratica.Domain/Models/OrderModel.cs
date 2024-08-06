using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Models
{
    public class OrderModel : EntityBase
    {
        public ClientModel Client { get; set; }
        public UserModel User { get; set; }

    }
}
