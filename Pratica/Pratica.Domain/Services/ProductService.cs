using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Validators;
using Pratica.Domain.Validators.Base;

namespace Pratica.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response> CreateAsync(ProductModel request)
        {
            var response = new Response();

            var validate = new ProductValidation();
            var validateErrors = validate.Validate(request).GetErrors();
            if (validateErrors.ReportErrors.Any())
                return validateErrors;

            await _productRepository.CreateAsync(request);
            return response;
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            var response = new Response();

            var exists = await _productRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"Product {id} not found."));
                return response;
            }

            await _productRepository.DeleteAsync(id);
            return response;
        }

        public async Task<Response<List<ProductModel>>> GetAllAsync(Guid? id, string? description)
        {
            var response = new Response<List<ProductModel>>();

            if (id is not null && id != Guid.Empty)
            {
                var exists = await _productRepository.ExistByIdAsync(id.Value.ToString());
                if (!exists)
                {
                    response.ReportErrors.Add(ReportError.Create($"Product {id} not found."));
                    return response;
                }
            }

            var result = await _productRepository.GetAllAsync(id, description);
            response.Data = result;
            return response;
        }

        public async Task<Response<ProductModel>> GetByIdAsync(Guid id)
        {
            var response = new Response<ProductModel>();

            var exists = await _productRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"Product {id} not found."));
                return response;
            }

            var result = await _productRepository.GetByIdAsync(id);
            response.Data = result;

            return response;
        }

        public async Task<Response> UpdateAsync(ProductModel request)
        {
            var response = new Response();

            var validate = new ProductValidation();
            var validateErrors = validate.Validate(request).GetErrors();
            if (validateErrors.ReportErrors.Any())
                return validateErrors;

            var exists = await _productRepository.ExistByIdAsync(request.Id);
            if (!exists)
            {
                response.ReportErrors.Add(ReportError.Create($"Product {request.Id} not found."));
                return response;
            }

            await _productRepository.UpdateAsync(request);
            return response;
        }
    }
}
