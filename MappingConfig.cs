using Mapster;
using Application.Orders.Queries;
using Domain.Entities;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Map domain entity to DTO
        config.NewConfig<Order, OrderDto>();

        // Map command to entity
        config.NewConfig<CreateOrderCommand, Order>()
            .Map(dest => dest.Items, src => src.Items);
    }
}
