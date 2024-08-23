﻿using Microsoft.AspNetCore.Mvc;
using Pratica.Application.DataContract.Order.Request;
using Pratica.Application.Interfaces;

namespace Pratica.API.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderApplication _application;

    public OrderController(IOrderApplication application)
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
    public async Task<ActionResult> GetAll(Guid? orderId, Guid? clientId, Guid? userId)
    {
        var result = await _application.GetAllAsync(orderId, clientId, userId);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var result = await _application.CreateAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateOrderRequest request)
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
