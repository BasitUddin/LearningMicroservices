using FluentValidation;

namespace LearningMicroservices.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer Id can not be empty");
            RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("Total Amount must be greater than zero.").LessThan(1000).WithMessage("Total Amount must be less than 1000");
        }
    }
}
