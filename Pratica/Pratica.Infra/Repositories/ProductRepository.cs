using Dapper;
using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Repositories.DataConnector;
using Pratica.Domain.Models;
using Pratica.Infra.Statements;

namespace Pratica.Infra.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IDbConnector _dbConnector;
    public ProductRepository(IDbConnector dbConnector)
    {
        _dbConnector = dbConnector;
    }

    public async Task CreateAsync(ProductModel request)
    {
        var sql = ProductStatements.SQL_INSERT;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = Guid.NewGuid(),
                Description = request.Description,
                SellValue = request.SellValue,
                Stock = request.Stock,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            }, _dbConnector.DbTransaction);
    }
    public async Task UpdateAsync(ProductModel request)
    {
        var sql = ProductStatements.SQL_UPDATE;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = request.Id,
                Description = request.Description,
                SellValue = request.SellValue,
                Stock = request.Stock
            }, _dbConnector.DbTransaction);
    }

    public async Task DeleteAsync(Guid id)
    {
        var sql = ProductStatements.SQL_DELETE;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = id
            }, _dbConnector.DbTransaction);
    }

    public async Task<bool> ExistByIdAsync(string id)
    {
        var sql = $"{ProductStatements.SQL_EXIST}";

        var result = await _dbConnector.DbConnection.QueryAsync<bool>(sql,
            new
            {
                Id = id
            }, _dbConnector.DbTransaction);

        return result.FirstOrDefault();
    }

    public async Task<List<ProductModel>> GetAllAsync(Guid? id, string? description)
    {
        var sql = $"{ProductStatements.SQL_BASE}";

        if (id is not null)
            sql += " AND Id = @Id ";

        if (!string.IsNullOrWhiteSpace(description))
            sql += " AND Description LIKE @Description ";

        var result = await _dbConnector.DbConnection.QueryAsync<ProductModel>(sql,
            new
            {
                Id = id,
                Description = "%" + description + "%"
            }, _dbConnector.DbTransaction);

        return result.ToList();
    }

    public async Task<ProductModel> GetByIdAsync(Guid id)
    {
        var sql = $"{ProductStatements.SQL_BASE} AND Id = @Id ";

        var result = await _dbConnector.DbConnection.QueryAsync<ProductModel>(sql,
            new
            {
                Id = id
            }, _dbConnector.DbTransaction);

        return result.FirstOrDefault()!;
    }
}
