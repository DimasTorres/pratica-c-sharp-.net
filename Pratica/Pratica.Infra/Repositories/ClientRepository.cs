using Dapper;
using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Repositories.DataConnector;
using Pratica.Domain.Models;
using Pratica.Infra.Statements;

namespace Pratica.Infra.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly IDbConnector _dbConnector;
    public ClientRepository(IDbConnector dbConnector)
    {
        _dbConnector = dbConnector;
    }
    public async Task CreateAsync(ClientModel request)
    {
        var sql = ClientStatements.SQL_INSERT;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = request.Id,
                Name = request.Name,
                Address = request.Address,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IsDeleted = request.IsDeleted,
                CreatedAt = request.CreatedAt
            }, _dbConnector.DbTransaction);
    }

    public async Task UpdateAsync(ClientModel request)
    {
        var sql = ClientStatements.SQL_UPDATE;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = request.Id,
                Name = request.Name,
                Address = request.Address,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            }, _dbConnector.DbTransaction);
    }

    public async Task DeleteAsync(Guid id)
    {
        var sql = ClientStatements.SQL_DELETE;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = id
            }, _dbConnector.DbTransaction);
    }

    public async Task<bool> ExistByIdAsync(Guid id)
    {
        var sql = $"{ClientStatements.SQL_EXIST}";

        var result = await _dbConnector.DbConnection.QueryAsync<bool>(sql,
            new
            {
                Id = id
            }, _dbConnector.DbTransaction);

        return result.FirstOrDefault();
    }

    public async Task<List<ClientModel>> GetAllAsync(Guid? id, string? name = null)
    {
        var sql = $"{ClientStatements.SQL_BASE}";

        if (id is not null)
            sql += " AND Id = @Id ";

        if (string.IsNullOrWhiteSpace(name))
            sql += " AND Name LIKE @Name ";


        var result = await _dbConnector.DbConnection.QueryAsync<ClientModel>(sql,
            new
            {
                Id = id,
                Name = "%" + name + "%"
            }, _dbConnector.DbTransaction);

        return result.ToList();
    }

    public async Task<ClientModel> GetByIdAsync(Guid id)
    {
        var sql = $"{ClientStatements.SQL_BASE} AND Id = @Id ";

        var result = await _dbConnector.DbConnection.QueryAsync<ClientModel>(sql,
            new
            {
                Id = id
            }, _dbConnector.DbTransaction);

        return result.FirstOrDefault()!;
    }
}
