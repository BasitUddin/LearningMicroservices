using FluentValidation;

namespace LearningMicroservices.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.TotalAmount).GreaterThan(0);
        }
    }
}
