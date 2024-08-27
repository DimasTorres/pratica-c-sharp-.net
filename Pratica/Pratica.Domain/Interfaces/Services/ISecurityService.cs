using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Services;

public interface ISecurityService
{
    Task<string> EncryptPassword(string password);
    Task<string> DecryptPassword(string passwordHash);
    Task<bool> VerifyPassword(string password, UserModel user);
}
