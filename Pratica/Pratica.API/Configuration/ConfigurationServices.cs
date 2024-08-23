using Pratica.Application.Applications;
using Pratica.Application.Interfaces;
using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Repositories.Base;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Services;
using Pratica.Infra.Repositories;
using Pratica.Infra.Repositories.Base;

namespace Pratica.API.Configuration;

public class ConfigurationServices
{
    public static void ConfigureServices(IServiceCollection services)
    {
        //Add Unit Of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Application
        services.AddScoped<IClientApplication, ClientApplication>();
        services.AddScoped<IOrderApplication, OrderApplication>();
        services.AddScoped<IProductApplication, ProductApplication>();
        services.AddScoped<IUserApplication, UserApplication>();

        //Add Services
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();

        //Add Repositories
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
