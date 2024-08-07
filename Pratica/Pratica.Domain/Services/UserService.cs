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

        public Task<Response> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<UserModel>>> GetAllAsync(string id = null, string name = null)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UserModel>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateAsync(UserModel request)
        {
            throw new NotImplementedException();
        }
    }
}
