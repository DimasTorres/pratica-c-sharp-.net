using AutoMapper;
using Pratica.Application.DataContract.Product.Request;
using Pratica.Application.DataContract.Product.Response;
using Pratica.Application.Interfaces;
using Pratica.Application.Validators;
using Pratica.Application.Validators.Base;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

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
        var validate = new CreateProductRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return validateErrors;

        try
        {
            var productModel = _mapper.Map<ProductModel>(request);
            await _productService.CreateAsync(productModel);

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
            var exists = await _productService.ExistByIdAsync(id);
            if (!exists.Data)
            {
                return Response.Unprocessable(ReportError.Create($"Product {id} not found."));
            }

            return await _productService.DeleteAsync(id);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }

    }

    public async Task<Response<List<ProductResponse>>> GetAllAsync(Guid? id, string? description)
    {
        try
        {
            if (id is not null && id != Guid.Empty)
            {
                var exists = await _productService.ExistByIdAsync(id.Value);
                if (!exists.Data)
                {
                    List<ReportError> listError = [ReportError.Create($"Product {id} not found.")];
                    return Response.Unprocessable<List<ProductResponse>>(listError);
                }
            }

            var result = await _productService.GetAllAsync(id, description);

            if (result.ReportErrors.Any())
                return Response.Unprocessable<List<ProductResponse>>(result.ReportErrors);

            var response = _mapper.Map<List<ProductResponse>>(result.Data);

            return Response.OK(response);
        }
        catch (Exception e)
        {
            List<ReportError> listError = [ReportError.Create(e.Message)];
            return Response.Unprocessable<List<ProductResponse>>(listError);
        }
    }

    public async Task<Response> GetByIdAsync(Guid id)
    {
        try
        {
            return await _productService.GetByIdAsync(id);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }

    public async Task<Response> UpdateAsync(UpdateProductRequest request)
    {
        var validate = new UpdateProductRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return validateErrors;

        try
        {
            var exists = await _productService.ExistByIdAsync(request.Id);
            if (!exists.Data)
            {
                return Response.Unprocessable(ReportError.Create($"Product {request.Id} not found."));
            }

            var productModel = _mapper.Map<ProductModel>(request);

            return await _productService.UpdateAsync(productModel);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }
}
