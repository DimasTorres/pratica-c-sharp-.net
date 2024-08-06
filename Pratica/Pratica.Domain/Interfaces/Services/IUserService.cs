using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> AuthenticationAsync(UserModel user);
        Task CreateAsync(UserModel request);
        Task UpdateAsync(UserModel request);
        Task DeleteAsync(string id);
        Task<List<UserModel>> GetAllAsync(string id = null, string name = null);
        Task<UserModel> GetByIdAsync(string id);
    }
}
