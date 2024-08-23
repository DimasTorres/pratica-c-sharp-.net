using AutoMapper;
using Pratica.Application.DataContract.Product.Request;
using Pratica.Application.DataContract.Product.Response;
using Pratica.Application.Interfaces;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Validators.Base;

namespace Pratica.Application.Applications;

public class ProductApplication : IProductApplication
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductApplication(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    public async Task<Response> CreateAsync(CreateProductRequest request)
    {
        var productModel = _mapper.Map<ProductModel>(request);

        return await _productService.CreateAsync(productModel);
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        return await _productService.DeleteAsync(id);
    }

    public async Task<Response<List<ProductResponse>>> GetAllAsync(Guid? id, string? description)
    {
        var result = await _productService.GetAllAsync(id, description);

        if (result.ReportErrors.Any())
            return Response.Unprocessable<List<ProductResponse>>(result.ReportErrors);

        var response = _mapper.Map<List<ProductResponse>>(result.Data);

        return Response.OK(response);
    }

    public async Task<Response> GetByIdAsync(Guid id)
    {
        return await _productService.GetByIdAsync(id);
    }

    public async Task<Response> UpdateAsync(UpdateProductRequest request)
    {
        var productModel = _mapper.Map<ProductModel>(request);

        return await _productService.UpdateAsync(productModel);
    }
}
