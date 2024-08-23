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
        var userModel = _mapper.Map<UserModel>(request);

        return await _userService.CreateAsync(userModel);
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        return await _userService.DeleteAsync(id);
    }

    public async Task<Response<List<UserResponse>>> GetAllAsync(Guid? id, string? name)
    {
        var result = await _userService.GetAllAsync(id, name);

        if (result.ReportErrors.Any())
            return Response.Unprocessable<List<UserResponse>>(result.ReportErrors);

        var response = _mapper.Map<List<UserResponse>>(result.Data);

        return Response.OK(response);
    }

    public async Task<Response> GetByIdAsync(Guid id)
    {
        return await _userService.GetByIdAsync(id);
    }

    public async Task<Response> UpdateAsync(UpdateUserRequest request)
    {
        var userModel = _mapper.Map<UserModel>(request);

        return await _userService.UpdateAsync(userModel);
    }
}
