using Pratica.Application.DataContract.User.Response;
using Pratica.Domain.Models;

namespace Pratica.Application.Interfaces;

public interface ITokenManager
{
    Task<AuthResponse> GenerateTokenAsync(UserModel user);
}
