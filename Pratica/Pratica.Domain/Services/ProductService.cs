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

        public Task<Response> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<ProductModel>>> GetAllAsync(string id = null, string name = null)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ProductModel>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateAsync(ProductModel request)
        {
            throw new NotImplementedException();
        }
    }
}
