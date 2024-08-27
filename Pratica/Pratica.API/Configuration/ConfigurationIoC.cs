using Pratica.Application.Applications;
using Pratica.Application.Interfaces;
using Pratica.Domain.Interfaces.Repositories;
using Pratica.Domain.Interfaces.Repositories.Base;
using Pratica.Domain.Interfaces.Services;
using Pratica.Domain.Services;
using Pratica.Infra.Repositories;
using Pratica.Infra.Repositories.Base;

namespace Pratica.API.Configuration;

public static class ConfigurationIoC
{
    public static void ConfigureIoC(this IServiceCollection services)
    {
        //Unit Of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Security
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<ITokenManager, TokenManager>();

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
