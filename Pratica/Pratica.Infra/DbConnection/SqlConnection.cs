using Pratica.Domain.Interfaces.Repositories.DataConnector;
using System.Data;
using System.Data.SqlClient;
namespace Pratica.Infra.DbConnection;

public class SqlConnection : IDbConnector
{
    public SqlConnection(string connectionString)
    {
        DbConnection = SqlClientFactory.Instance.CreateConnection();
        DbConnection.ConnectionString = connectionString;
    }

    public IDbConnection DbConnection { get; }
    public IDbTransaction DbTransaction { get; set; }

    public void Dispose()
    {
        DbConnection?.Dispose();
        DbTransaction?.Dispose();
    }
}
