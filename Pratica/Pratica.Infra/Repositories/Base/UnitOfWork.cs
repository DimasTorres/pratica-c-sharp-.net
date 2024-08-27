using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Repositories.Base;
using Pratica.Domain.Interfaces.Repositories.DataConnector;
using System.Data;

namespace Pratica.Infra.Repositories.Base;

public class UnitOfWork : IUnitOfWork
{
    private IClientRepository _clientRepository;
    private IOrderRepository _orderRepository;
    private IOrderItemRepository _orderItemRepository;
    private IProductRepository _productRepository;
    private IUserRepository _userRepository;

    public UnitOfWork(IDbConnector dbConnector)
    {
        DbConnector = dbConnector;
    }

    public IClientRepository ClientRepository => _clientRepository ?? (_clientRepository = new ClientRepository(DbConnector));
    public IOrderItemRepository OrderItemRepository => _orderItemRepository ?? (_orderItemRepository = new OrderItemRepository(DbConnector));
    public IOrderRepository OrderRepository => _orderRepository ?? (_orderRepository = new OrderRepository(DbConnector, OrderItemRepository));
    public IProductRepository ProductRepository => _productRepository ?? (_productRepository = new ProductRepository(DbConnector));
    public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(DbConnector));
    public IDbConnector DbConnector { get; }

    public void BeginTransaction()
    {
        DbConnector.BeginTransaction(IsolationLevel.ReadUncommitted);
    }

    public void CommitTransaction()
    {
        if (DbConnector.dbConnection.State == ConnectionState.Open)
            DbConnector.dbTransaction.Commit();
    }

    public void RollbackTransaction()
    {
        if (DbConnector.dbConnection.State == ConnectionState.Open)
            DbConnector.dbTransaction.Rollback();
    }
}
