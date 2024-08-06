using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(UserModel request);
        Task UpdateAsync(UserModel request);
        Task ExistByIdAsync(string id);
        Task DeleteAsync(string id);
        Task<List<UserModel>> GetAllAsync(string id = null, string name = null);
        Task<UserModel> GetByIdAsync(string id);
    }
}
