namespace Pratica.Application.DataContract.User.Request;

public sealed class UpdateUserRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
}
