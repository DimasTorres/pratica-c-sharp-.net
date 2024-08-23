using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Validators;
using Pratica.Domain.Validators.Base;

namespace Pratica.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AuthenticationAsync(UserModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> CreateAsync(UserModel request)
        {
            var response = new Response();

            var validate = new UserValidation();
            var validateErrors = validate.Validate(request).GetErrors();
            if (validateErrors.ReportErrors.Any())
                return validateErrors;

            await _userRepository.CreateAsync(request);
            return response;
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            var response = new Response();

            var exists = await _userRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"User {id} not found."));
                return response;
            }

            await _userRepository.DeleteAsync(id);
            return response;
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _userRepository.ExistByIdAsync(id.ToString());
        }

        public async Task<Response<List<UserModel>>> GetAllAsync(Guid? id, string? name)
        {
            var response = new Response<List<UserModel>>();

            if (id is not null && id != Guid.Empty)
            {
                var exists = await _userRepository.ExistByIdAsync(id.Value.ToString());
                if (!exists)
                {
                    response.ReportErrors.Add(ReportError.Create($"User {id} not found."));
                    return response;
                }
            }

            var result = await _userRepository.GetAllAsync(id, name);
            response.Data = result;
            return response;
        }

        public async Task<Response<UserModel>> GetByIdAsync(Guid id)
        {
            var response = new Response<UserModel>();

            var exists = await _userRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"User {id} not found."));
                return response;
            }

            var result = await _userRepository.GetByIdAsync(id);
            response.Data = result;

            return response;
        }

        public async Task<Response> UpdateAsync(UserModel request)
        {
            var response = new Response();

            var validate = new UserValidation();
            var validateErrors = validate.Validate(request).GetErrors();
            if (validateErrors.ReportErrors.Any())
                return validateErrors;

            var exists = await _userRepository.ExistByIdAsync(request.Id);
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"User {request.Id} not found."));
                return response;
            }

            await _userRepository.UpdateAsync(request);
            return response;
        }
    }
}
