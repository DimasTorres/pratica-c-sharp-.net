using System.Data;

namespace Pratica.Domain.Interfaces.Repositories.DataConnector;

public interface IDbConnector : IDisposable
{
    IDbConnection dbConnection { get; }
    IDbTransaction dbTransaction { get; set; }
    IDbTransaction BeginTransaction(IsolationLevel isolation);
}