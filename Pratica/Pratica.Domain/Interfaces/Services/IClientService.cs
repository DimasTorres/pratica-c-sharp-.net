using Pratica.Domain.Models;

namespace Pratica.Domain.Interfaces.Services
{
    public interface IClientService
    {
        Task CreateAsync(ClientModel request);
        Task UpdateAsync(ClientModel request);
        Task DeleteAsync(string id);
        Task<List<ClientModel>> GetAllAsync(string id = null, string name = null);
        Task<ClientModel> GetByIdAsync(string id);
    }
}
