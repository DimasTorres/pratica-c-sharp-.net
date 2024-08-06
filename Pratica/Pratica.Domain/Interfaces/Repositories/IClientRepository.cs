using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task CreateAsync(ClientModel request);
        Task UpdateAsync(ClientModel request);
        Task ExistByIdAsync(string id);
        Task DeleteAsync(string id);
        Task<List<ClientModel>> GetAllAsync(string id = null, string name = null);
        Task<ClientModel> GetByIdAsync(string id);
    }
}
