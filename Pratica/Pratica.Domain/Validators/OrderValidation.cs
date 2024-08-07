using FluentValidation;
using Pratica.Domain.Models;

namespace Pratica.Domain.Validators
{
    public class OrderValidation : AbstractValidator<OrderModel>
    {
        public OrderValidation()
        {
            RuleFor(x => x.User)
                .NotNull();

            RuleFor(x => x.Client)
                .NotNull();
        }
    }
}
