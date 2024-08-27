using Pratica.Domain.Interfaces.Repositories.Base;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Services;

public class ClientService : IClientService
{
    private readonly IUnitOfWork _repository;

    public ClientService(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<Response> CreateAsync(ClientModel request)
    {
        var response = new Response<ClientModel>();
        _repository.BeginTransaction();
        try
        {
            await _repository.ClientRepository.CreateAsync(request);
            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        var response = new Response();
        _repository.BeginTransaction();

        try
        {
            var exists = await _repository.ClientRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                _repository.RollbackTransaction();
                response.ReportErrors.Add(ReportError.Create($"Client {id} not found."));
                return response;
            }

            await _repository.ClientRepository.DeleteAsync(id);

            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }

    public async Task<Response<bool>> ExistByIdAsync(Guid id)
    {
        var response = new Response<bool>();
        _repository.BeginTransaction();
        try
        {
            response.Data = await _repository.ClientRepository.ExistByIdAsync(id.ToString());

            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }

    public async Task<Response<List<ClientModel>>> GetAllAsync(Guid? id, string? name)
    {
        var response = new Response<List<ClientModel>>();
        _repository.BeginTransaction();

        try
        {
            if (id is not null && id != Guid.Empty)
            {
                var exists = await _repository.ClientRepository.ExistByIdAsync(id!.Value.ToString());
                if (!exists)
                {
                    _repository.RollbackTransaction();
                    response.ReportErrors.Add(ReportError.Create($"Client {id} not found."));
                    return response;
                }
            }

            var result = await _repository.ClientRepository.GetAllAsync(id, name);
            response.Data = result;

            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }

    public async Task<Response<ClientModel>> GetByIdAsync(Guid id)
    {
        var response = new Response<ClientModel>();
        _repository.BeginTransaction();

        try
        {
            var exists = await _repository.ClientRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                _repository.RollbackTransaction();
                response.ReportErrors.Add(ReportError.Create($"Client {id} not found."));
                return response;
            }

            var result = await _repository.ClientRepository.GetByIdAsync(id);
            response.Data = result;

            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }

    public async Task<Response> UpdateAsync(ClientModel request)
    {
        var response = new Response();
        _repository.BeginTransaction();

        try
        {
            var exists = await _repository.ClientRepository.ExistByIdAsync(request.Id);
            if (!exists)
            {
                _repository.RollbackTransaction();
                response.ReportErrors.Add(ReportError.Create($"Client {request.Id} not found."));
                return response;
            }

            await _repository.ClientRepository.UpdateAsync(request);

            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }
}
