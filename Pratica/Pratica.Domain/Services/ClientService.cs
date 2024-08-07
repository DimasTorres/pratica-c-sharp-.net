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

        public Task<Response> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<ClientModel>>> GetAllAsync(string id = null, string name = null)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ClientModel>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateAsync(ClientModel request)
        {
            throw new NotImplementedException();
        }
    }
}
