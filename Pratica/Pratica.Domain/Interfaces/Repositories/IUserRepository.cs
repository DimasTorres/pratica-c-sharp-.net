using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(UserModel request);
        Task UpdateAsync(UserModel request);
        Task<bool> ExistByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<List<UserModel>> GetAllAsync(Guid? id, string name = null);
        Task<UserModel> GetByIdAsync(Guid id);
    }
}
