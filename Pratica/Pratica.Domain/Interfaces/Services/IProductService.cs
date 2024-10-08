﻿using Pratica.Domain.Models;
using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Interfaces.Services;

public interface IProductService
{
    Task<Response> CreateAsync(ProductModel request);
    Task<Response> UpdateAsync(ProductModel request);
    Task<Response> DeleteAsync(Guid id);
    Task<Response<List<ProductModel>>> GetAllAsync(Guid? id, string? name);
    Task<Response<ProductModel>> GetByIdAsync(Guid id);
    Task<Response<bool>> ExistByIdAsync(Guid id);
}