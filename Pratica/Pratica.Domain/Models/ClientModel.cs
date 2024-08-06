using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Models
{
    public class ClientModel : EntityBase
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
