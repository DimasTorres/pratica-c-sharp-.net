using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;

namespace Pratica.Domain.Services;

public class SecurityService : ISecurityService
{
    public Task<string> EncryptPassword(string password)
    {
        //encrypt data
        var data = BCrypt.Net.BCrypt.HashPassword(password);

        return Task.FromResult<string>(data);
    }

    public Task<string> DecryptPassword(string passwordHash)
    {
        var data = BCrypt.Net.BCrypt.HashString(passwordHash);

        return Task.FromResult<string>(data);
    }

    public Task<bool> VerifyPassword(string password, UserModel user)
    {
        bool validPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        return Task.FromResult(validPassword);
    }
}
