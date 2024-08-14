namespace Pratica.Application.DataContract.User.Request;

public sealed class UpdateUserRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
}
