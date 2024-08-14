﻿using Pratica.Domain.Models;
using Pratica.Domain.Validators.Base;

namespace Pratica.Domain.Interfaces.Services
{
    public interface IClientService
    {
        Task<Response> CreateAsync(ClientModel request);
        Task<Response> UpdateAsync(ClientModel request);
        Task<Response> DeleteAsync(Guid id);
        Task<Response<List<ClientModel>>> GetAllAsync(Guid id, string name = null);
        Task<Response<ClientModel>> GetByIdAsync(Guid id);
    }
}
