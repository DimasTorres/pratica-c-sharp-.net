using Dapper;
using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Repositories.DataConnector;
using Pratica.Domain.Models;
using Pratica.Infra.Statements;

namespace Pratica.Infra.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnector _dbConnector;
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderRepository(IDbConnector dbConnector, IOrderItemRepository orderItemRepository)
    {
        _dbConnector = dbConnector;
        _orderItemRepository = orderItemRepository;
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

        if (request.OrderItems.Any())
        {
            foreach (var item in request.OrderItems)
            {
                await _orderItemRepository.CreateItemAsync(item);
            }
        }
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

        if (request.OrderItems.Any())
        {
            var sqlItens = OrderItemStatements.SQL_DELETE_BY_ORDERID;

            await _dbConnector.DbConnection.ExecuteAsync(sqlItens,
                param: new
                {
                    OrderId = request.Id
                }, _dbConnector.DbTransaction);

            foreach (var item in request.OrderItems)
            {
                await _orderItemRepository.CreateItemAsync(item);
            }
        }
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

        var result = await _dbConnector.DbConnection.QueryAsync<OrderModel, ClientModel, UserModel, OrderModel>(sql,
            map: (order, client, user) =>
            {
                order.Client = client;
                order.User = user;
                return order;
            },
            param: new
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

        var result = await _dbConnector.DbConnection.QueryAsync<OrderModel, ClientModel, UserModel, OrderModel>(sql,
            map: (order, client, user) =>
            {
                order.Client = client;
                order.User = user;
                return order;
            },
            param: new
            {
                Id = id
            }, _dbConnector.DbTransaction);

        return result.FirstOrDefault()!;
    }


}
