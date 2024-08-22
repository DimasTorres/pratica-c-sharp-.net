using Pratica.Application.DataContract.Client.Request;
using Pratica.Application.DataContract.Client.Response;
using Pratica.Domain.Validators.Base;

namespace Pratica.Application.Interfaces;

public interface IClientApplication
{
    Task<Response> CreateAsync(CreateClientRequest request);
    Task<Response> UpdateAsync(UpdateClientRequest request);
    Task<Response> DeleteAsync(Guid id);
    Task<Response> GetByIdAsync(Guid id);
    Task<Response<List<ClientResponse>>> GetAllAsync(Guid? id, string? name);
}
