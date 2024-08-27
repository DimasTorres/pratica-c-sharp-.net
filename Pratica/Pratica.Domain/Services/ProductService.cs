using Pratica.Domain.Interfaces.Repositories.Base;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _repository;

    public ProductService(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<Response> CreateAsync(ProductModel request)
    {
        var response = new Response<ProductModel>();
        _repository.BeginTransaction();
        try
        {
            await _repository.ProductRepository.CreateAsync(request);
            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        var response = new Response();
        _repository.BeginTransaction();

        try
        {
            var exists = await _repository.ProductRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                _repository.RollbackTransaction();
                response.ReportErrors.Add(ReportError.Create($"Product {id} not found."));
                return response;
            }

            await _repository.ProductRepository.DeleteAsync(id);
            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }

    public async Task<Response<List<ProductModel>>> GetAllAsync(Guid? id, string? description)
    {
        var response = new Response<List<ProductModel>>();
        _repository.BeginTransaction();

        try
        {
            if (id is not null && id != Guid.Empty)
            {
                var exists = await _repository.ProductRepository.ExistByIdAsync(id.Value.ToString());
                if (!exists)
                {
                    _repository.RollbackTransaction();
                    response.ReportErrors.Add(ReportError.Create($"Product {id} not found."));
                    return response;
                }
            }

            var result = await _repository.ProductRepository.GetAllAsync(id, description);
            response.Data = result;
            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }

    public async Task<Response<ProductModel>> GetByIdAsync(Guid id)
    {
        var response = new Response<ProductModel>();
        _repository.BeginTransaction();

        try
        {
            var exists = await _repository.ProductRepository.ExistByIdAsync(id.ToString());
            if (!exists)
            {
                _repository.RollbackTransaction();
                response.ReportErrors.Add(ReportError.Create($"Product {id} not found."));
                return response;
            }

            var result = await _repository.ProductRepository.GetByIdAsync(id);
            response.Data = result;

            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }

    public async Task<Response> UpdateAsync(ProductModel request)
    {
        var response = new Response();
        _repository.BeginTransaction();

        try
        {

            var exists = await _repository.ProductRepository.ExistByIdAsync(request.Id);
            if (!exists)
            {
                _repository.RollbackTransaction();
                response.ReportErrors.Add(ReportError.Create($"Product {request.Id} not found."));
                return response;
            }

            await _repository.ProductRepository.UpdateAsync(request);
            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }
}