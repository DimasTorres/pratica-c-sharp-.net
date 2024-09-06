namespace Pratica.Domain.Interfaces.Services;

public interface ISecurityService
{
    Task<string> EncryptPassword(string password);
    Task<bool> VerifyPassword(string password, string passwordHash);
}
