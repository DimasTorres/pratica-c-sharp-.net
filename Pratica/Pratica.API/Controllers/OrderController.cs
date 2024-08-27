using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pratica.Application.DataContract.Order.Request;
using Pratica.Application.Interfaces;

namespace Pratica.API.Controllers;

[Route("api/order")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderApplication _application;

    public OrderController(IOrderApplication application)
    {
        _application = application;
    }

    /// <summary>
    /// Get Order by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Order</returns>
    [HttpGet("id")]
    public async Task<ActionResult> GetById([FromQuery] Guid id)
    {
        var result = await _application.GetByIdAsync(id);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Get Orders
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="clientId"></param>
    /// <param name="userId"></param>
    /// <returns>List of Order</returns>
    [HttpGet]
    public async Task<ActionResult> GetAll(Guid? orderId, Guid? clientId, Guid? userId)
    {
        var result = await _application.GetAllAsync(orderId, clientId, userId);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Create Order
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Status Code</returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var result = await _application.CreateAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Update Order
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Status Code</returns>
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateOrderRequest request)
    {
        var result = await _application.UpdateAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Delete Order
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
