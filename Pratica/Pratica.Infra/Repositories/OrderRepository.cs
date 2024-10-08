﻿using Dapper;
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
        var id = Guid.NewGuid();

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = id.ToString(),
                ClientId = request.ClientId.ToString(),
                UserId = request.UserId.ToString(),
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            }, _dbConnector.dbTransaction);

        if (request.OrderItems.Any())
        {
            foreach (var item in request.OrderItems)
            {
                item.OrderId = id;
                await _orderItemRepository.CreateItemAsync(item);
            }
        }
    }

    public async Task UpdateAsync(OrderModel request)
    {
        var sql = OrderStatements.SQL_UPDATE;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = request.Id,
                ClientId = request.ClientId,
                UserId = request.UserId
            }, _dbConnector.dbTransaction);

        if (request.OrderItems.Any())
        {
            var sqlItens = OrderItemStatements.SQL_DELETE_BY_ORDERID;

            await _dbConnector.dbConnection.ExecuteAsync(sqlItens,
                param: new
                {
                    OrderId = request.Id
                }, _dbConnector.dbTransaction);

            foreach (var item in request.OrderItems)
            {
                item.OrderId = new Guid(request.Id);
                await _orderItemRepository.CreateItemAsync(item);
            }
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var sql = OrderStatements.SQL_DELETE;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = id
            }, _dbConnector.dbTransaction);
    }

    public async Task<bool> ExistByIdAsync(Guid id)
    {
        var sql = $"{OrderStatements.SQL_EXIST}";

        var result = await _dbConnector.dbConnection.QueryAsync<bool>(sql,
            new
            {
                Id = id.ToString()
            }, _dbConnector.dbTransaction);

        return result.FirstOrDefault();
    }

    public async Task<List<OrderModel>> GetAllAsync(Guid? orderId, Guid? clientId, Guid? userId)
    {
        var sql = $"{OrderStatements.SQL_BASE}";

        if (orderId is not null)
            sql += " AND o.Id = @OrderId ";

        if (clientId is not null)
            sql += " AND c.Id = @ClientId ";

        if (userId is not null)
            sql += " AND u.Id = @UserId ";

        var result = await _dbConnector.dbConnection.QueryAsync<OrderModel, ClientModel, UserModel, OrderModel>(sql,
            map: (order, client, user) =>
            {
                order.Client = client;
                order.User = user;
                return order;
            },
            param: new
            {
                OrderId = orderId,
                ClientId = clientId,
                UserId = userId
            }, _dbConnector.dbTransaction);

        return result.ToList();
    }

    public async Task<OrderModel> GetByIdAsync(Guid id)
    {
        var sql = $"{OrderStatements.SQL_BASE} AND o.Id = @Id ";

        var result = await _dbConnector.dbConnection.QueryAsync<OrderModel, ClientModel, UserModel, OrderModel>(sql,
            map: (order, client, user) =>
            {
                order.Client = client;
                order.User = user;
                return order;
            },
            param: new
            {
                Id = id
            }, _dbConnector.dbTransaction);

        return result.FirstOrDefault()!;
    }
}
