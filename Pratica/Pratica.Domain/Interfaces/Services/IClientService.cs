using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Interfaces.Services
{
    public interface IClientService
    {
        Task CreateAsync(ClientModel request);
        Task<Response> UpdateAsync(ClientModel request);
        Task<bool> ExistByIdAsync(Guid id);
        Task<Response> DeleteAsync(Guid id);
        Task<Response<List<ClientModel>>> GetAllAsync(Guid? id, string? name);
        Task<Response<ClientModel>> GetByIdAsync(Guid id);
    }
}
