using FluentValidation;
using Pratica.Domain.Models;

namespace Pratica.Domain.Validators
{
    public class OrderItemValidation : AbstractValidator<OrderItemModel>
    {
        public OrderItemValidation()
        {
            RuleFor(x => x.Order)
                .NotNull();
        }
    }
}
