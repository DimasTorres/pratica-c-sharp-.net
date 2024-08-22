using Pratica.Domain.Interfaces.Repositories.DataConnector;
using Pratica.Infra.DbConnection;

namespace Pratica.API.Configuration;

public class DbConnectionConfiguration
{
    public static void DbConnectionConfigure(WebApplicationBuilder builder)
    {
        //Add Connection DB
        var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                    .Build();

        var defaultConnectionString = config.GetConnectionString("Default");

        builder.Services.AddScoped<IDbConnector>(db => new SqlConnection(defaultConnectionString!));
    }
}
