using Dapper;
using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Repositories.DataConnector;
using Pratica.Domain.Models;
using Pratica.Infra.Statements;

namespace Pratica.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnector _dbConnector;
    public UserRepository(IDbConnector dbConnector)
    {
        _dbConnector = dbConnector;
    }

    public async Task CreateAsync(UserModel request)
    {
        var sql = UserStatements.SQL_INSERT;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Login = request.Login,
                PasswordHash = request.PasswordHash,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            }, _dbConnector.dbTransaction);
    }

    public async Task UpdateAsync(UserModel request)
    {
        var sql = UserStatements.SQL_UPDATE;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                Login = request.Login,
                PasswordHash = request.PasswordHash
            }, _dbConnector.dbTransaction);
    }

    public async Task DeleteAsync(Guid id)
    {
        var sql = UserStatements.SQL_DELETE;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = id
            }, _dbConnector.dbTransaction);
    }

    public async Task<bool> ExistByIdAsync(string id)
    {
        var sql = $"{UserStatements.SQL_EXIST_BY_ID}";

        var result = await _dbConnector.dbConnection.QueryAsync<bool>(sql,
            new
            {
                Id = id
            }, _dbConnector.dbTransaction);

        return result.FirstOrDefault();
    }

    public async Task<List<UserModel>> GetAllAsync(Guid? id, string? name)
    {
        var sql = $"{UserStatements.SQL_BASE}";

        if (id is not null)
            sql += " AND Id = @Id ";

        if (!string.IsNullOrWhiteSpace(name))
            sql += " AND Name LIKE @Name ";

        var result = await _dbConnector.dbConnection.QueryAsync<UserModel>(sql,
            new
            {
                Id = id,
                Name = "%" + name + "%"
            }, _dbConnector.dbTransaction);

        return result.ToList();
    }

    public async Task<UserModel> GetByIdAsync(Guid id)
    {
        var sql = $"{UserStatements.SQL_BASE} AND Id = @Id ";

        var result = await _dbConnector.dbConnection.QueryAsync<UserModel>(sql,
            new
            {
                Id = id
            }, _dbConnector.dbTransaction);

        return result.FirstOrDefault()!;
    }

    public async Task<UserModel> GetByLoginAsync(string login)
    {
        var sql = $"{UserStatements.SQL_BASE} AND Login LIKE @Login ";

        var result = await _dbConnector.dbConnection.QueryAsync<UserModel>(sql,
            new
            {
                Login = login
            }, _dbConnector.dbTransaction);

        return result.FirstOrDefault()!;
    }
}
