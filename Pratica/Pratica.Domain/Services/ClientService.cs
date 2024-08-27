using Pratica.Domain.Interfaces.Repositories.Base;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _repository;

        public ClientService(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(ClientModel request)
        {
            await _repository.ClientRepository.CreateAsync(request);
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            var response = new Response();

            var exists = await _repository.ClientRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"Client {id} not found."));
                return response;
            }

            await _repository.ClientRepository.DeleteAsync(id);
            return response;
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _repository.ClientRepository.ExistByIdAsync(id.ToString());
        }

        public async Task<Response<List<ClientModel>>> GetAllAsync(Guid? id, string? name)
        {
            var response = new Response<List<ClientModel>>();

            if (id is not null && id != Guid.Empty)
            {
                var exists = await _repository.ClientRepository.ExistByIdAsync(id!.Value.ToString());
                if (!exists)
                {
                    response.ReportErrors.Add(ReportError.Create($"Client {id} not found."));
                    return response;
                }
            }

            var result = await _repository.ClientRepository.GetAllAsync(id, name);
            response.Data = result;
            return response;
        }

        public async Task<Response<ClientModel>> GetByIdAsync(Guid id)
        {
            var response = new Response<ClientModel>();

            var exists = await _repository.ClientRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"Client {id} not found."));
                return response;
            }

            var result = await _repository.ClientRepository.GetByIdAsync(id);
            response.Data = result;
            return response;
        }

        public async Task<Response> UpdateAsync(ClientModel request)
        {
            var response = new Response();

            var exists = await _repository.ClientRepository.ExistByIdAsync(request.Id);
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"Client {request.Id} not found."));
                return response;
            }

            await _repository.ClientRepository.UpdateAsync(request);
            return response;
        }
    }
}
