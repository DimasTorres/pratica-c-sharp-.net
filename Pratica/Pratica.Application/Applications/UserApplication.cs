using AutoMapper;
using Pratica.Application.DataContract.User.Request;
using Pratica.Application.DataContract.User.Response;
using Pratica.Application.Interfaces;
using Pratica.Application.Validators;
using Pratica.Application.Validators.Base;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Application.Applications;

public class UserApplication : IUserApplication
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ITokenManager _tokenManager;
    public UserApplication(IUserService userService, IMapper mapper, ITokenManager tokenManager)
    {
        _userService = userService;
        _mapper = mapper;
        _tokenManager = tokenManager;
    }

    public async Task<Response<AuthResponse>> AutheticationAsync(AuthRequest request)
    {
        var validate = new AuthRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return Response.Unprocessable<AuthResponse>(validateErrors.ReportErrors);

        try
        {
            var user = await _userService.GetByLoginAsync(request.Login);
            if (user.ReportErrors.Any())
                return Response.Unprocessable<AuthResponse>(user.ReportErrors);

            var isAuthenticated = await _userService.AuthenticationAsync(request.Password, user.Data);
            if (!isAuthenticated.Data)
                return Response.Unprocessable<AuthResponse>(ReportError.Create("Password is incorrect."));

            var response = await _tokenManager.GenerateTokenAsync(user.Data);
            return Response.OK(response);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable<AuthResponse>(responseError);
        }
    }

    public async Task<Response> CreateAsync(CreateUserRequest request)
    {
        var validate = new CreateUserRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return validateErrors;

        try
        {
            var userModel = _mapper.Map<UserModel>(request);

            await _userService.CreateAsync(userModel);

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
            return await _userService.DeleteAsync(id);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }

    public async Task<Response<List<UserResponse>>> GetAllAsync(Guid? id, string? name)
    {
        try
        {
            var result = await _userService.GetAllAsync(id, name);

            if (result.ReportErrors.Any())
                return Response.Unprocessable<List<UserResponse>>(result.ReportErrors);

            var response = _mapper.Map<List<UserResponse>>(result.Data);

            return Response.OK(response);
        }
        catch (Exception e)
        {
            List<ReportError> listError = [ReportError.Create(e.Message)];
            return Response.Unprocessable<List<UserResponse>>(listError);
        }
    }

    public async Task<Response> GetByIdAsync(Guid id)
    {
        try
        {
            return await _userService.GetByIdAsync(id);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }

    public async Task<Response> UpdateAsync(UpdateUserRequest request)
    {
        var validate = new UpdateUserRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return validateErrors;

        try
        {
            var userModel = _mapper.Map<UserModel>(request);

            return await _userService.UpdateAsync(userModel);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }
}
