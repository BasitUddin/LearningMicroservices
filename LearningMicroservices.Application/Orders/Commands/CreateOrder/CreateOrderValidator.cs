using FluentValidation;

namespace OrderManagement.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer Id can not be empty");
            RuleFor(x => x.Items.Count()).GreaterThan(0).WithMessage("Items can not be empty, please add Items");
        }
    }
}
