using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Validators;
using Pratica.Domain.Validators.Base;

namespace Pratica.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Response> CreateAsync(ClientModel request)
        {
            var response = new Response();

            var validate = new ClientValidation();
            var validateErrors = validate.Validate(request).GetErrors();
            if (validateErrors.ReportErrors.Any())
                return validateErrors;

            await _clientRepository.CreateAsync(request);
            return response;
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            var response = new Response();

            var exists = await _clientRepository.ExistByIdAsync(id);
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"Client {id} not found."));
                return response;
            }

            await _clientRepository.DeleteAsync(id);
            return response;
        }

        public async Task<Response<List<ClientModel>>> GetAllAsync(Guid id, string name = null)
        {
            var response = new Response<List<ClientModel>>();

            if (id != Guid.Empty)
            {
                var exists = await _clientRepository.ExistByIdAsync(id);
                if (!exists)
                {
                    response.ReportErrors.Add(ReportError.Create($"Client {id} not found."));
                    return response;
                }
            }

            var result = await _clientRepository.GetAllAsync(id, name);
            response.Data = result;
            return response;
        }

        public async Task<Response<ClientModel>> GetByIdAsync(Guid id)
        {
            var response = new Response<ClientModel>();

            var exists = await _clientRepository.ExistByIdAsync(id);
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"Client {id} not found."));
                return response;
            }

            await _clientRepository.GetByIdAsync(id);
            return response;
        }

        public async Task<Response> UpdateAsync(ClientModel request)
        {
            var response = new Response();

            var validate = new ClientValidation();
            var validateErrors = validate.Validate(request).GetErrors();
            if (validateErrors.ReportErrors.Any())
                return validateErrors;

            var exists = await _clientRepository.ExistByIdAsync(request.Id);
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"Client {request.Id} not found."));
                return response;
            }

            await _clientRepository.UpdateAsync(request);
            return response;
        }
    }
}
