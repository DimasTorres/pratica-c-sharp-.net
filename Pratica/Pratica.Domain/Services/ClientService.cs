using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Validators;

namespace Pratica.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Task CreateAsync(ClientModel request)
        {
            var validate = new ClientValidation();
            var resultValidation = validate.Validate(request);

            if (!resultValidation.IsValid)
            {
                foreach (var error in resultValidation.Errors)
                {

                }
            }

            _clientRepository.CreateAsync(request);
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ClientModel>> GetAllAsync(string id = null, string name = null)
        {
            throw new NotImplementedException();
        }

        public Task<ClientModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ClientModel request)
        {
            throw new NotImplementedException();
        }
    }
}
