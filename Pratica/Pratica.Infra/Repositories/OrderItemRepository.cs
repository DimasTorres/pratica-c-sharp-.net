using Dapper;
using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Repositories.DataConnector;
using Pratica.Domain.Models;
using Pratica.Infra.Statements;

namespace Pratica.Infra.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly IDbConnector _dbConnector;

    public OrderItemRepository(IDbConnector dbConnector)
    {
        _dbConnector = dbConnector;
    }

    public async Task CreateItemAsync(OrderItemModel request)
    {
        var sql = OrderItemStatements.SQL_INSERT;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = Guid.NewGuid(),
                OrderId = request.Order.Id,
                ProductId = request.Product.Id,
                SellValue = request.SellValue,
                Quantity = request.Quantity,
                TotalAmount = request.TotalAmout,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
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

    public async Task DeleteItemAsync(Guid id)
    {
        var sql = OrderItemStatements.SQL_DELETE;

        await _dbConnector.DbConnection.ExecuteAsync(sql,
            new
            {
                Id = id
            }, _dbConnector.DbTransaction);
    }

    public async Task<List<OrderItemModel>> GetItemByOrderIdAsync(Guid orderId)
    {
        var sql = $"{OrderItemStatements.SQL_BASE} AND o.Id = @OrderId ";

        var result = await _dbConnector.DbConnection.QueryAsync<OrderItemModel, OrderModel, ProductModel, OrderItemModel>(sql,
            map: (orderItem, order, product) =>
            {
                orderItem.Order = order;
                orderItem.Product = product;
                return orderItem;
            },
            param: new
            {
                OrderId = orderId
            }, _dbConnector.DbTransaction);

        return result.ToList();
    }
}
