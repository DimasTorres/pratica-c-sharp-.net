using AutoMapper;
using Pratica.Application.DataContract.Client.Request;
using Pratica.Application.Interfaces;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Validators.Base;

namespace Pratica.Application.Applications
{
    public class ClienteApplication : IClientApplication
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        public ClienteApplication(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        public async Task<Response> CreateAsync(CreateClientRequest request)
        {
            var clientModel = _mapper.Map<ClientModel>(request);

            return await _clientService.CreateAsync(clientModel);
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            return await _clientService.DeleteAsync(id);
        }

        public async Task<Response> GetAllAsync(Guid id, string name)
        {
            return await _clientService.GetAllAsync(id, name);
        }

        public async Task<Response> GetByIdAsync(Guid id)
        {
            return await _clientService.GetByIdAsync(id);
        }

        public async Task<Response> UpdateAsync(UpdateClientRequest request)
        {
            var clientModel = _mapper.Map<ClientModel>(request);

            return await _clientService.UpdateAsync(clientModel);
        }
    }
}
