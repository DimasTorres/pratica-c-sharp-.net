using Microsoft.AspNetCore.Mvc;
using Pratica.Application.DataContract.Product.Request;
using Pratica.Application.Interfaces;

namespace Pratica.API.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductApplication _application;

    public ProductController(IProductApplication application)
    {
        _application = application;
    }

    /// <summary>
    /// Get Product by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Product</returns>
    [HttpGet("id")]
    public async Task<ActionResult> GetById([FromQuery] Guid id)
    {
        var result = await _application.GetByIdAsync(id);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Get Products
    /// </summary>
    /// <param name="id"></param>
    /// <param name="description"></param>
    /// <returns>List of Products</returns>
    [HttpGet]
    public async Task<ActionResult> GetAll(Guid? id, string? description)
    {
        var result = await _application.GetAllAsync(id, description);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Create Products
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Status Code</returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateProductRequest request)
    {
        var result = await _application.CreateAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Update Product
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateProductRequest request)
    {
        var result = await _application.UpdateAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Delete Product
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
