using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Models
{
    public class UserModel : EntityBase
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
