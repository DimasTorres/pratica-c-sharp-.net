using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pratica.Application.DataContract.Client.Request;
using Pratica.Application.Interfaces;

namespace Pratica.API.Controllers;

[Route("api/client")]
[ApiController]
[Authorize]
public class ClientController : ControllerBase
{
    private readonly IClientApplication _application;

    public ClientController(IClientApplication application)
    {
        _application = application;
    }

    /// <summary>
    /// Get Client by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Client</returns>
    [HttpGet("id")]
    public async Task<ActionResult> GetById([FromQuery] Guid id)
    {
        var result = await _application.GetByIdAsync(id);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Get Clients
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns>List of Clients</returns>
    [HttpGet]
    public async Task<ActionResult> GetAll(Guid? id, string? name)
    {
        var result = await _application.GetAllAsync(id, name);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Create Client
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Status Code</returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateClientRequest request)
    {
        var result = await _application.CreateAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Update Client
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Status Code</returns>
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateClientRequest request)
    {
        var result = await _application.UpdateAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Delete Client 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Status Code</returns>
    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] Guid id)
    {
        var result = await _application.DeleteAsync(id);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }
}