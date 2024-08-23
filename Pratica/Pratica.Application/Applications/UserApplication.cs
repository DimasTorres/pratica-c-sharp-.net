using AutoMapper;
using Pratica.Application.DataContract.User.Request;
using Pratica.Application.DataContract.User.Response;
using Pratica.Application.Interfaces;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Validators.Base;

namespace Pratica.Application.Applications;

public class UserApplication : IUserApplication
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserApplication(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<Response> CreateAsync(CreateUserRequest request)
    {
        try
        {
            var userModel = _mapper.Map<UserModel>(request);

            return await _userService.CreateAsync(userModel);
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
