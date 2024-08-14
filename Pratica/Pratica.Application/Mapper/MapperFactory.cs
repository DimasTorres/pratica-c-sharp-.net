using AutoMapper;
using Pratica.Application.DataContract.Client.Request;
using Pratica.Application.DataContract.Client.Response;
using Pratica.Application.DataContract.Order.Request;
using Pratica.Application.DataContract.Order.Response;
using Pratica.Application.DataContract.Product.Request;
using Pratica.Application.DataContract.Product.Response;
using Pratica.Application.DataContract.User.Request;
using Pratica.Application.DataContract.User.Response;
using Pratica.Domain.Models;

namespace Pratica.Application.Mapper;

public class MapperFactory : Profile
{
    public MapperFactory()
    {
        ClientMap();
        OrderMap();
    }

    private void ClientMap()
    {
        CreateMap<ClientModel, ClientResponse>();
        CreateMap<CreateClientRequest, ClientModel>();
        CreateMap<UpdateClientRequest, ClientModel>();
    }
    private void OrderMap()
    {
        CreateMap<OrderModel, OrderResponse>();
        CreateMap<CreateOrderRequest, OrderModel>();
        CreateMap<UpdateOrderRequest, OrderModel>();
        CreateMap<OrderItemModel, OrderItemResponse>();
        CreateMap<CreateOrderItemRequest, OrderItemModel>();
    }
    private void ProductMap()
    {
        CreateMap<ProductModel, ProductResponse>();
        CreateMap<CreateProductRequest, ProductModel>();
        CreateMap<UpdateProductRequest, ProductModel>();
    }

    private void UserMap()
    {
        CreateMap<UserModel, UserResponse>();
        CreateMap<CreateUserRequest, UserModel>();
        CreateMap<UpdateUserRequest, UserModel>();
    }

}
