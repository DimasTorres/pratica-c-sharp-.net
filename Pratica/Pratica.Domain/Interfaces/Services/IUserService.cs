using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Interfaces.Services;

public interface IUserService
{
    Task<Response<bool>> AuthenticationAsync(string password, UserModel user);
    Task<Response> CreateAsync(UserModel request);
    Task<Response> UpdateAsync(UserModel request);
    Task<Response<bool>> ExistByIdAsync(Guid id);
    Task<Response<UserModel>> GetByLoginAsync(string login);
    Task<Response> DeleteAsync(Guid id);
    Task<Response<List<UserModel>>> GetAllAsync(Guid? id, string? name);
    Task<Response<UserModel>> GetByIdAsync(Guid id);
}
