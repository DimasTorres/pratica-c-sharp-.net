using Dapper;
using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Repositories.DataConnector;
using Pratica.Domain.Models;
using Pratica.Infra.Statements;

namespace Pratica.Infra.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnector _dbConnector;

    public OrderRepository(IDbConnector dbConnector)
    {
        _dbConnector = dbConnector;
    }

    public async Task CreateAsync(OrderModel request)
    {
        var sql = OrderStatements.SQL_INSERT;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = request.Id,
                ClientId = request.Client.Id,
                UserId = request.User.Id,
                IsDeleted = request.IsDeleted,
                CreatedAt = request.CreatedAt
            }, _dbConnector.DbTransaction);
    }

    public async Task CreateItemAsync(OrderItemModel request)
    {
        var sql = OrderItemStatements.SQL_INSERT;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = request.Id,
                OrderId = request.Order.Id,
                ProductId = request.Product.Id,
                SellValue = request.SellValue,
                Quantity = request.Quantity,
                TotalAmount = request.TotalAmout,
                IsDeleted = request.IsDeleted,
                CreatedAt = request.CreatedAt
            }, _dbConnector.DbTransaction);
    }

    public async Task UpdateAsync(OrderModel request)
    {
        var sql = OrderStatements.SQL_UPDATE;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = request.Id,
                ClientId = request.Client.Id,
                UserId = request.User.Id
            }, _dbConnector.DbTransaction);
    }

    public async Task UpdateItemAsync(OrderItemModel request)
    {
        var sql = OrderItemStatements.SQL_UPDATE;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = request.Id,
                OrderId = request.Order.Id,
                ProductId = request.Product.Id,
                SellValue = request.SellValue,
                Quantity = request.Quantity,
                TotalAmount = request.TotalAmout
            }, _dbConnector.DbTransaction);
    }

    public async Task DeleteAsync(Guid id)
    {
        var sql = OrderStatements.SQL_DELETE;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = id
            }, _dbConnector.DbTransaction);
    }

    public async Task DeleteItemAsync(Guid id)
    {
        var sql = OrderItemStatements.SQL_DELETE;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = id
            }, _dbConnector.DbTransaction);
    }

    public async Task<bool> ExistByIdAsync(Guid id)
    {
        var sql = $"{OrderStatements.SQL_EXIST}";

        var result = await _dbConnector.DbConnection.QueryAsync<bool>(sql,
            new
            {
                Id = id
            }, _dbConnector.DbTransaction);

        return result.FirstOrDefault();
    }

    public async Task<List<OrderModel>> GetAllAsync(Guid? orderId, Guid? clientId, Guid? userId)
    {
        var sql = $"{OrderStatements.SQL_BASE}";

        if (orderId is not null)
            sql += " AND Id = @Id ";

        if (clientId is not null)
            sql += " AND ClientId = @ClientId ";

        if (userId is not null)
            sql += " AND UserId = @UserId ";

        var result = await _dbConnector.DbConnection.QueryAsync<OrderModel>(sql,
            new
            {
                Id = orderId,
                ClientId = clientId,
                UserId = userId
            }, _dbConnector.DbTransaction);

        return result.ToList();
    }

    public async Task<OrderModel> GetByIdAsync(Guid id)
    {
        var sql = $"{OrderStatements.SQL_BASE} AND Id = @Id ";

        var result = await _dbConnector.DbConnection.QueryAsync<OrderModel>(sql,
            new
            {
                Id = id
            }, _dbConnector.DbTransaction);

        return result.FirstOrDefault()!;
    }

    public async Task<List<OrderItemModel>> GetItemByOrderIdAsync(Guid orderId)
    {
        var sql = $"{OrderItemStatements.SQL_BASE} AND OrderId = @OrderId ";

        var result = await _dbConnector.DbConnection.QueryAsync<OrderItemModel>(sql,
            new
            {
                OrderId = orderId
            }, _dbConnector.DbTransaction);

        return result.ToList();
    }
}
