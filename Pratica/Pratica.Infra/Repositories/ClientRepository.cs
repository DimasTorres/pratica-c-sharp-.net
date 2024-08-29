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

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            }, _dbConnector.dbTransaction);
    }

    public async Task UpdateAsync(ClientModel request)
    {
        var sql = ClientStatements.SQL_UPDATE;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = request.Id.ToString(),
                Name = request.Name,
                Address = request.Address,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            }, _dbConnector.dbTransaction);
    }

    public async Task DeleteAsync(Guid id)
    {
        var sql = ClientStatements.SQL_DELETE;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = id.ToString()
            }, _dbConnector.dbTransaction);
    }

    public async Task<bool> ExistByIdAsync(Guid id)
    {
        var sql = $"{ClientStatements.SQL_EXIST}";

        var result = await _dbConnector.dbConnection.QueryAsync<bool>(sql,
            new
            {
                Id = id.ToString()
            }, _dbConnector.dbTransaction);

        return result.FirstOrDefault();
    }

    public async Task<List<ClientModel>> GetAllAsync(Guid? id, string? name)
    {
        var sql = $"{ClientStatements.SQL_BASE}";

        if (id is not null)
            sql += " AND Id = @Id ";

        if (!string.IsNullOrWhiteSpace(name))
            sql += " AND Name LIKE @Name ";


        var result = await _dbConnector.dbConnection.QueryAsync<ClientModel>(sql,
            new
            {
                Id = id.ToString(),
                Name = "%" + name + "%"
            }, _dbConnector.dbTransaction);

        return result.ToList();
    }

    public async Task<ClientModel> GetByIdAsync(Guid id)
    {
        var sql = $"{ClientStatements.SQL_BASE} AND Id = @Id ";

        var result = await _dbConnector.dbConnection.QueryAsync<ClientModel>(sql,
            new
            {
                Id = id.ToString()
            }, _dbConnector.dbTransaction);

        return result.FirstOrDefault()!;
    }
}
