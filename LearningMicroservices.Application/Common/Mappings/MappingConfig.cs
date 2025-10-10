using Mapster;
using LearningMicroservices.Domain.Entities;
using LearningMicroservices.Application.Orders.DTOs;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Map domain entity to DTO
        config.NewConfig<Order, OrderDto>()
            .Map(dest => dest.CustomerName, src => src.Customer != null ? src.Customer.Name : string.Empty);

        // Map command to entity
        //config.NewConfig<CreateOrderCommand, Order>()
        //    .Map(dest => dest.Items, src => src.Items);
    }
}
