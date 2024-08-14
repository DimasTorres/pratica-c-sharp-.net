using Microsoft.AspNetCore.Mvc;
using Pratica.Application.DataContract.Client.Request;
using Pratica.Application.Interfaces;

namespace Pratica.API.Controllers;

[Route("api/client")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientApplication _application;

    public ClientController(IClientApplication application)
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
    public async Task<ActionResult> GetAll([FromForm] Guid id, [FromForm] string name)
    {
        var result = await _application.GetAllAsync(id, name);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateClientRequest request)
    {
        var result = await _application.CreateAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateClientRequest request)
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
