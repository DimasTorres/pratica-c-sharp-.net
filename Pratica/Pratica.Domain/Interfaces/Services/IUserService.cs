using Pratica.Domain.Models;
using Pratica.Domain.Validators.Base;

namespace Pratica.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> AuthenticationAsync(UserModel user);
        Task<Response> CreateAsync(UserModel request);
        Task<Response> UpdateAsync(UserModel request);
        Task<Response> DeleteAsync(string id);
        Task<Response<List<UserModel>>> GetAllAsync(string id = null, string name = null);
        Task<Response<UserModel>> GetByIdAsync(string id);
    }
}
