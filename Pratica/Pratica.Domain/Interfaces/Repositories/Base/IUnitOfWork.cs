using Pratica.Domain.Interfaces.Repositories.DataConnector;

namespace Pratica.Domain.Interfaces.Repositories.Base;

public interface IUnitOfWork
{
    IClientRepository ClientRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderItemRepository OrderItemRepository { get; }
    IProductRepository ProductRepository { get; }
    IUserRepository UserRepository { get; }
    IDbConnector DbConnector { get; }
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
