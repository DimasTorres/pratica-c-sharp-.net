using Pratica.Application.DataContract.User.Request;
using Pratica.Application.DataContract.User.Response;
using Pratica.Domain.Models.Base;

namespace Pratica.Application.Interfaces;

public interface IUserApplication
{
    Task<Response<AuthResponse>> AutheticationAsync(AuthRequest request);
    Task<Response> CreateAsync(CreateUserRequest request);
    Task<Response> UpdateAsync(UpdateUserRequest request);
    Task<Response> DeleteAsync(Guid id);
    Task<Response> GetByIdAsync(Guid id);
    Task<Response<List<UserResponse>>> GetAllAsync(Guid? id, string? name);
}
