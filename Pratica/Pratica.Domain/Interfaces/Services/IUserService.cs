using Pratica.Domain.Models;
using Pratica.Domain.Validators.Base;

namespace Pratica.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> AuthenticationAsync(UserModel user);
        Task<Response> CreateAsync(UserModel request);
        Task<Response> UpdateAsync(UserModel request);
        Task<Response> DeleteAsync(Guid id);
        Task<Response<List<UserModel>>> GetAllAsync(Guid id, string name = null);
        Task<Response<UserModel>> GetByIdAsync(Guid id);
    }
}
