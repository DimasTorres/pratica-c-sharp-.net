using Pratica.Domain.Interfaces.Services;
using System.Security.Cryptography;

namespace Pratica.Domain.Services;

public class SecurityService : ISecurityService
{
    private const int SaltSize = 16; // Tamanho do salt
    private const int KeySize = 32; // Tamanho do hash
    private const int Iterations = 10000; // Número de iterações para o PBKDF2

    public Task<string> EncryptPassword(string password)
    {
        using (var algorithm = new Rfc2898DeriveBytes(
            password,
            SaltSize,
            Iterations,
            HashAlgorithmName.SHA256))
        {
            var salt = algorithm.Salt;
            var hash = algorithm.GetBytes(KeySize);

            // Concatenar o salt e o hash para armazenamento
            var hashBytes = new byte[SaltSize + KeySize];
            Buffer.BlockCopy(salt, 0, hashBytes, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, hashBytes, SaltSize, KeySize);

            var data = Convert.ToBase64String(hashBytes);
            return Task.FromResult<string>(data);
        }
    }

    public Task<bool> VerifyPassword(string password, string passwordHash)
    {
        var hashBytes = Convert.FromBase64String(passwordHash);

        var salt = new byte[SaltSize];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);

        using (var algorithm = new Rfc2898DeriveBytes(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA256))
        {
            var hash = algorithm.GetBytes(KeySize);

            for (int i = 0; i < KeySize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return Task.FromResult(false);
                }
            }
        }

        return Task.FromResult(true);
    }
}