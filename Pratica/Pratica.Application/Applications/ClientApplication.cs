using AutoMapper;
using Pratica.Application.DataContract.Client.Request;
using Pratica.Application.DataContract.Client.Response;
using Pratica.Application.Interfaces;
using Pratica.Application.Validators;
using Pratica.Application.Validators.Base;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Application.Applications
{
    public class ClientApplication : IClientApplication
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        public ClientApplication(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        public async Task<Response> CreateAsync(CreateClientRequest request)
        {
            var validate = new CreateClientRequestValidator();
            var validateErrors = validate.Validate(request).GetErrors();
            if (validateErrors.ReportErrors.Any())
                return validateErrors;

            try
            {
                var clientModel = _mapper.Map<ClientModel>(request);

                await _clientService.CreateAsync(clientModel);

                return Response.OK();
            }
            catch (Exception e)
            {
                var responseError = ReportError.Create(e.Message);
                return Response.Unprocessable(responseError);
            }
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            try
            {
                var exists = await _clientService.ExistByIdAsync(id);
                if (!exists.Data)
                    return Response.Unprocessable(ReportError.Create($"Client {id} not found."));

                return await _clientService.DeleteAsync(id);
            }
            catch (Exception e)
            {
                var responseError = ReportError.Create(e.Message);
                return Response.Unprocessable(responseError);
            }
        }

        public async Task<Response<List<ClientResponse>>> GetAllAsync(Guid? id, string? name)
        {
            try
            {
                var result = await _clientService.GetAllAsync(id, name);

                if (result.ReportErrors.Any())
                    return Response.Unprocessable<List<ClientResponse>>(result.ReportErrors);

                var response = _mapper.Map<List<ClientResponse>>(result.Data);

                return Response.OK(response);
            }
            catch (Exception e)
            {
                List<ReportError> listError = [ReportError.Create(e.Message)];
                return Response.Unprocessable<List<ClientResponse>>(listError);
            }
        }

        public async Task<Response> GetByIdAsync(Guid id)
        {
            try
            {
                return await _clientService.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                var responseError = ReportError.Create(e.Message);
                return Response.Unprocessable(responseError);
            }
        }

        public async Task<Response> UpdateAsync(UpdateClientRequest request)
        {
            var validate = new UpdateClientRequestValidator();
            var validateErrors = validate.Validate(request).GetErrors();
            if (validateErrors.ReportErrors.Any())
                return validateErrors;

            try
            {
                var exists = await _clientService.ExistByIdAsync(request.Id);
                if (!exists.Data)
                {
                    return Response.Unprocessable(ReportError.Create($"Client {request.Id} not found."));
                }

                var clientModel = _mapper.Map<ClientModel>(request);

                return await _clientService.UpdateAsync(clientModel);
            }
            catch (Exception e)
            {
                var responseError = ReportError.Create(e.Message);
                return Response.Unprocessable(responseError);
            }
        }
    }
}
