using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task CreateAsync(ClientModel request);
        Task UpdateAsync(ClientModel request);
        Task<bool> ExistByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<List<ClientModel>> GetAllAsync(Guid? id, string? name);
        Task<ClientModel> GetByIdAsync(Guid id);
    }
}
