using OrderManagement.Application.Orders.Commands.CreateOrder;
using OrderManagement.Application.Orders.DTOs;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.ValueObjects;
using Mapster;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Map domain entity to DTO
        config.NewConfig<Order, OrderDto>()
            .Map(dest => dest.CustomerName, src => src.Customer != null ? src.Customer.Name : string.Empty);

        config.NewConfig<CreateOrderCommand, Order>()
            .ConstructUsing(src => new Order(
                src.CustomerId,
                new Address(src.ShippingAddress.Street, src.ShippingAddress.City, src.ShippingAddress.Country),
                src.Items.Select(i =>
                        new OrderItems(i.ProductId, i.Quantity, new Money(i.Amount, "PKR"))
                    ).ToList()
            ));
        // Map command to entity
        //config.NewConfig<CreateOrderCommand, Order>()
        //    .Map(dest => dest.Items, src => src.Items);
    }
}
