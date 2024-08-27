using Pratica.Domain.Interfaces.Repositories.Base;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _repository;
    private readonly ISecurityService _securityService;

    public UserService(IUnitOfWork repository, ISecurityService securityService)
    {
        _repository = repository;
        _securityService = securityService;
    }

    public async Task<Response<bool>> AuthenticationAsync(string password, UserModel user)
    {
        var result = await _securityService.VerifyPassword(password, user);
        return new Response<bool>(result);
    }

    public async Task<Response> CreateAsync(UserModel request)
    {
        var response = new Response<UserModel>();
        _repository.BeginTransaction();

        try
        {
            request.PasswordHash = await _securityService.EncryptPassword(request.PasswordHash);

            await _repository.UserRepository.CreateAsync(request);
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
            var exists = await _repository.UserRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"User {id} not found."));
                _repository.RollbackTransaction();
                return response;
            }

            await _repository.UserRepository.DeleteAsync(id);
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
            response.Data = await _repository.UserRepository.ExistByIdAsync(id.ToString());
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

    public async Task<Response<List<UserModel>>> GetAllAsync(Guid? id, string? name)
    {
        var response = new Response<List<UserModel>>();
        _repository.BeginTransaction();

        try
        {
            if (id is not null && id != Guid.Empty)
            {
                var exists = await _repository.UserRepository.ExistByIdAsync(id.Value.ToString());
                if (!exists)
                {
                    _repository.RollbackTransaction();
                    response.ReportErrors.Add(ReportError.Create($"User {id} not found."));
                    return response;
                }
            }

            var result = await _repository.UserRepository.GetAllAsync(id, name);
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

    public async Task<Response<UserModel>> GetByIdAsync(Guid id)
    {
        var response = new Response<UserModel>();
        _repository.BeginTransaction();

        try
        {
            var exists = await _repository.UserRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                _repository.RollbackTransaction();
                response.ReportErrors.Add(ReportError.Create($"User {id} not found."));
                return response;
            }

            var result = await _repository.UserRepository.GetByIdAsync(id);
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

    public async Task<Response<UserModel>> GetByLoginAsync(string login)
    {
        var response = new Response<UserModel>();
        _repository.BeginTransaction();

        try
        {
            var result = await _repository.UserRepository.GetByLoginAsync(login);
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

    public async Task<Response> UpdateAsync(UserModel request)
    {
        var response = new Response();
        _repository.BeginTransaction();

        try
        {
            var exists = await _repository.UserRepository.ExistByIdAsync(request.Id);
            if (!exists)
            {
                _repository.RollbackTransaction();
                response.ReportErrors.Add(ReportError.Create($"User {request.Id} not found."));
                return response;
            }

            request.PasswordHash = await _securityService.EncryptPassword(request.PasswordHash);
            await _repository.UserRepository.UpdateAsync(request);

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