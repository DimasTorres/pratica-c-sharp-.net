using Microsoft.AspNetCore.Mvc;
using Pratica.Application.DataContract.User.Request;
using Pratica.Application.Interfaces;

namespace Pratica.API.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserApplication _application;

    public UserController(IUserApplication application)
    {
        _application = application;
    }

    [HttpGet("id")]
    public async Task<ActionResult> GetById([FromQuery] Guid id)
    {
        var result = await _application.GetByIdAsync(id);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll(Guid? id, string name)
    {
        var result = await _application.GetAllAsync(id, name);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateUserRequest request)
    {
        var result = await _application.CreateAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateUserRequest request)
    {
        var result = await _application.UpdateAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] Guid id)
    {
        var result = await _application.DeleteAsync(id);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }
}
