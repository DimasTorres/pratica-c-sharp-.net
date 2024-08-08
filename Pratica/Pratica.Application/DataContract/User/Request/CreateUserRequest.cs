namespace Pratica.Application.DataContract.User.Request;

public sealed class CreateUserRequest
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
}
