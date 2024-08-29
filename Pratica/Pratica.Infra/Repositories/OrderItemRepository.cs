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
        var id = Guid.NewGuid();
        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = id.ToString(),
                OrderId = request.OrderId.ToString(),
                ProductId = request.ProductId.ToString(),
                SellValue = request.SellValue,
                Quantity = request.Quantity,
                TotalAmout = request.TotalAmout,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            }, _dbConnector.dbTransaction);
    }

    public async Task UpdateItemAsync(OrderItemModel request)
    {
        var sql = OrderItemStatements.SQL_UPDATE;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = request.Id,
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                SellValue = request.SellValue,
                Quantity = request.Quantity,
                TotalAmout = request.TotalAmout
            }, _dbConnector.dbTransaction);
    }

    public async Task DeleteItemAsync(Guid id)
    {
        var sql = OrderItemStatements.SQL_DELETE;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = id
            }, _dbConnector.dbTransaction);
    }

    public async Task<List<OrderItemModel>> GetItemByOrderIdAsync(Guid orderId)
    {
        var sql = $"{OrderItemStatements.SQL_BASE} AND o.Id = @OrderId ";

        var result = await _dbConnector.dbConnection.QueryAsync<OrderItemModel, OrderModel, ProductModel, OrderItemModel>(sql,
            map: (orderItem, order, product) =>
            {
                orderItem.Order = order;
                orderItem.Product = product;
                return orderItem;
            },
            param: new
            {
                OrderId = orderId.ToString()
            }, _dbConnector.dbTransaction);

        return result.ToList();
    }
}
